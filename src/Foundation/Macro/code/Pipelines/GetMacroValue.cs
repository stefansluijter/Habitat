namespace Sitecore.Foundation.Macro.Pipelines
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;

    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
    using Sitecore.Foundation.Macro.Macro;
    using Sitecore.Pipelines.RenderField;

    public class GetMacroValue
    {
        public void Process(RenderFieldArgs args)
        {
            Assert.ArgumentNotNull(args, "args");
            Assert.ArgumentNotNull(args.Result, "args.Result");
            if (args.Result.FirstPart != string.Empty && Context.Site.DisplayMode != Sites.DisplayMode.Edit)
            {
                var value = args.FieldValue;
                var fieldName = args.FieldName;
                args.Result.FirstPart = GetValue(args.Item, fieldName, value);
            }
        }

        private string GetValue(Item item, string fieldName, string fieldValue)
        {
            var regex = @"{%[^@]*%}"; // TODO /gU

            if (ValueContainsMacro(fieldValue))
            {
                // TODO MATCH REGEX UNGREEDY
                // SO MULTIPLE MATCHES ARE POSSIBLE
                MatchCollection mcol = Regex.Matches(fieldValue, regex);
                foreach (Match m in mcol)
                {
                    var macroValue = m.ToString();

                    IMacro macro = CreateMacroClassObject(RemoveMacroSyntaxString(macroValue));
                    
                    fieldValue = fieldValue.Replace(m.ToString(), macro.Execute(item,fieldName, fieldValue));
                }
                
            }

            return fieldValue;

        }

        private bool CheckMacroHash(string hash)
        {
            return true;
        }

        private string RemoveMacroSyntaxString(string input)
        {
            return input.Length < 4 ? input : input.Substring(2, input.Length - 4).Trim();
        }

        // TODO TEST WITH ASSEMBLIES/NAMESPACES
        private IMacro CreateMacroClassObject(string macroValue)
        {
            var className = macroValue;
            if (string.IsNullOrEmpty(macroValue))
            {
                // TODO Maybe return a new "EmptyMacro" that returns the (input) value
                return null;
            }

            if (macroValue.Contains("."))
            {
                className = macroValue.Split('.')[0];
            }
            
            // ParTechMacro or ParTech both work
            // Either create class with the same name or prefix with 'Macro' 
            var type = GetType(className + "Macro") ?? GetType(className);
            return (IMacro)Activator.CreateInstance(type);
        }

        private bool ValueContainsMacro(string value)
        {
            return value.Contains("{%") && value.Contains("%}");

            // List of different macro's

            // return list of macros?
        }

        // TODO DONT CHECK ALL ASSEMBLIES, configure through config
        public static Type GetType(string typeName)
        {
            //var type = Type.GetType(typeName);
            //if (type != null) return type;
            foreach (var a in AppDomain.CurrentDomain.GetAssemblies().ToList()) 
            {
                try
                {
                    var type = a.GetTypes().FirstOrDefault(x => x.Name.Equals(typeName, StringComparison.OrdinalIgnoreCase));
                    if (type != null)
                    {
                        return type;
                    }
                }
                catch
                {
                    // ignored
                }
            }
            return null;
        }
    }
}
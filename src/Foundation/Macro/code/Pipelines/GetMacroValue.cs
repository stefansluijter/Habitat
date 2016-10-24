namespace Sitecore.Foundation.Macro.Pipelines
{
    using Sitecore.Data.Items;
    using Sitecore.Diagnostics;
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
                args.Result.FirstPart = GetValue(args.Item, fieldName);
            }
        }

        private string GetValue(Item item, string value)
        {
            return value + "test";
            // Item fallbackItem = item.Database.GetItem(item.ID, Globalization.Language.Parse(fallbackLanguage));
            // return fallbackItem[fieldName];
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Macro.Macro
{
    using Sitecore.Data.Items;

    public class ParTechMacro : IMacro
    {
        public string Execute(Item item, string fieldName, string fieldValue)
        {
            return "ParTech Macro Module";
        }
    }
}
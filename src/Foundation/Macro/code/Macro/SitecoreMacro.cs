using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Macro.Macro
{
    using Sitecore.Data.Items;

    public class SitecoreMacro : IMacro
    {
        public string Execute(Item item, string fieldName, string fieldValue)
        {
            return "Sitecore Macro Module";
        }
    }
}
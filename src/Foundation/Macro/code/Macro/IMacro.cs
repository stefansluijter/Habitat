using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.Foundation.Macro.Macro
{
    using Sitecore.Data.Items;

    public interface IMacro
    {
        string Execute(Item item, string fieldName, string fieldValue);
    }
}
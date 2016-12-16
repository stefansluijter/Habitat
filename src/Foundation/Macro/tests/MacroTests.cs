namespace Sitecore.Foundation.Macro.Tests
{
    using System.Collections.Specialized;

    using FluentAssertions;

    using Sitecore.FakeDb;
    using Sitecore.FakeDb.AutoFixture;
    using Sitecore.Foundation.Macro.Pipelines;
    using Sitecore.Foundation.Testing.Attributes;

    using Xunit;

    public class GetMacroValuePipelineTests
    {
        [Theory]
        [AutoDbData]
        public void GetMacroValue_MultipleMacrosInOneField_MultipleValuesProcessed(Db db, DbItem item, DbField field1)
        {
            var gmv = new GetMacroValuePipeline();
            
            item.Add(field1);
            field1.Value = "{% ParTech %},Modules,{% Sitecore %}";
            db.Add(item);
            var testItem = db.GetItem(item.ID);

            var result = gmv.GetValue(testItem, field1.Name, field1.Value);
            Assert.False(result.Contains("{%") && result.Contains("%}"));
        }

    }
}
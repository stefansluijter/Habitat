namespace Sitecore.Foundation.Installer
{
  using System.Collections.Specialized;
  using System.Configuration;
  using System.Data.SqlClient;
  using System.IO;
  using Sitecore.Foundation.Installer.XmlTransform;
  using Sitecore.Install.Framework;

  public class PostStep : IPostStep
  {
    private readonly IXdtTransformEngine xdtTransformEngine;
    private readonly IFilePathResolver filePathResolver;

    public PostStep() : this(new XdtTransformEngine(),new FilePathResolver())
    {
    }

    public PostStep(IXdtTransformEngine xdtTransformEngine, IFilePathResolver filePathResolver)
    {
      this.xdtTransformEngine = xdtTransformEngine;
      this.filePathResolver = filePathResolver;
    }

    public void Run(ITaskOutput output, NameValueCollection metaData)
    {
      this.ApplyTransform();
      this.ApplyScript();
    }

    public void ApplyTransform()
    {
      var webConfig = this.filePathResolver.MapPath("~/web.config");
      var webConfigTransform = this.filePathResolver.MapPath("~/web.config.transform");
      if (webConfigTransform == null)
      {
        return;
      }

      this.xdtTransformEngine.ApplyConfigTransformation(webConfig, webConfigTransform, webConfig);
    }

    public void ApplyScript()
    {
      var connectionstring = ConfigurationManager.ConnectionStrings["reporting"];
      var refreshAnalytics = this.filePathResolver.MapPath("~/RefreshAnalytics.sql");
      var reader = new StreamReader(new FileInfo(refreshAnalytics).OpenRead());
      var query = reader.ReadToEnd();
      var connection = new SqlConnection(connectionstring.ConnectionString);
      var command = new SqlCommand(query, connection);
      command.ExecuteNonQuery();
    }
  }
}
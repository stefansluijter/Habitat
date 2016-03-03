namespace Sitecore.Feature.DemoTools.Commands
{
  using Sitecore.Shell.Framework.Commands;
  using Sitecore.Text;
  using Sitecore.Web.UI.Sheer;

  public class UpdateAnalytics : Command
  {
    public override void Execute(CommandContext context)
    {

      UrlString str = new UrlString(UIUtil.GetUri("control:UpdateAnalytics"));

      SheerResponse.ShowModalDialog(str.ToString());
    }
  }
}
namespace Sitecore.Feature.DemoTools.sitecore.shell.Applications.Dialogs
{
  using System;
  using Sitecore;
  using Sitecore.Feature.DemoTools.Repositories;
  using Sitecore.Globalization;
  using Sitecore.Jobs;
  using Sitecore.Web.UI.Sheer;

  class UpdateAnalytics : WizardStatusForm
  {
    protected void StartJob()
    {
      this.JobHandle = RunUpdateAnalyticsVisitsOnToday().ToString();
      SheerResponse.Timer("CheckStatus", 500);
    }

    public static Handle RunUpdateAnalyticsVisitsOnToday()
    {
      var options = new JobOptions("UpdateAnalyticsVisitsOnToday", "Visits", "website", new UpdateAnalyticsVisitsRepository(), "Run", new object[] { })
      {
        ContextUser = Context.User,
        ClientLanguage = Language.Parse("en"),
        AfterLife = TimeSpan.FromSeconds(1),
        Priority = System.Threading.ThreadPriority.BelowNormal
      };

      var job = JobManager.Start(options);

      return job.Handle;
    }
  }
}

using System.Collections.Generic;
using System.Linq;
using Stryker.Abstractions.Baseline.Providers;
using Stryker.Abstractions;
using Stryker.Abstractions.Reporters.Html;
using Stryker.Abstractions.Reporters.Json;
using Stryker.Abstractions.Reporters.Progress;
using Stryker.Abstractions.Reporting;

namespace Stryker.Abstractions.Reporters
{
    public interface IReporterFactory
    {
        IReporter Create(StrykerOptions options, IGitInfoProvider branchProvider = null);
    }

    public class ReporterFactory : IReporterFactory
    {
        public IReporter Create(StrykerOptions options, IGitInfoProvider branchProvider = null)
        {
            return new BroadcastReporter(DetermineEnabledReporters(options.Reporters.ToList(), CreateReporters(options)));
        }

        private IDictionary<Reporter, IReporter> CreateReporters(StrykerOptions options)
        {
            return new Dictionary<Reporter, IReporter>
            {
                { Reporter.Dots, new ConsoleDotProgressReporter() },
                { Reporter.Progress, CreateProgressReporter() },
                { Reporter.ClearText, new ClearTextReporter(options) },
                { Reporter.ClearTextTree, new ClearTextTreeReporter(options) },
                { Reporter.Json, new JsonReporter(options) },
                { Reporter.Html, new HtmlReporter(options) },
                { Reporter.Dashboard, new DashboardReporter(options) },
                { Reporter.RealTimeDashboard, new DashboardReporter(options) },
                { Reporter.Markdown, new MarkdownSummaryReporter(options) },
                { Reporter.Baseline, new BaselineReporter(options) }
            };
        }

        private IEnumerable<IReporter> DetermineEnabledReporters(IList<Reporter> enabledReporters, IDictionary<Reporter, IReporter> possibleReporters)
        {
            if (enabledReporters.Contains(Reporter.All))
            {
                return possibleReporters.Values;
            }

            return possibleReporters.Where(reporter => enabledReporters.Contains(reporter.Key))
                .Select(reporter => reporter.Value);
        }

        private ProgressReporter CreateProgressReporter()
        {
            var progressBarReporter = new ProgressBarReporter(new ProgressBar(), new StopWatchProvider());

            return new ProgressReporter(progressBarReporter);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Monitoring
{
	// Token: 0x02000443 RID: 1091
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class FailureAnalyzersConfiguration : ConfigurationClass
	{
		// Token: 0x17000515 RID: 1301
		// (get) Token: 0x060021D1 RID: 8657 RVA: 0x0007D4C7 File Offset: 0x0007B6C7
		// (set) Token: 0x060021D2 RID: 8658 RVA: 0x0007D4CF File Offset: 0x0007B6CF
		[ConfigurationProperty]
		public ConfigurationCollection<FailureAnalyzerStreamConfig> FailureAnalyzerStreamConfigList { get; set; }

		// Token: 0x060021D3 RID: 8659 RVA: 0x0007D4D8 File Offset: 0x0007B6D8
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.FailureAnalyzerStreamConfigList.Select((FailureAnalyzerStreamConfig s) => s.ToString());
			return string.Format(CultureInfo.InvariantCulture, "Number of streams: {0}. Individual stream configs in following lines: {1} {2}", new object[]
			{
				this.FailureAnalyzerStreamConfigList.Count,
				Environment.NewLine,
				string.Join(Environment.NewLine, enumerable.ToArray<string>())
			});
		}

		// Token: 0x060021D4 RID: 8660 RVA: 0x0007D554 File Offset: 0x0007B754
		public void ValidateConfiguration([NotNull] IEnumerable<string> existingIds)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<IEnumerable<string>>(existingIds, "existingIds");
			IEnumerable<string> enumerable = from id in existingIds
				let newIds = this.FailureAnalyzerStreamConfigList.Select((FailureAnalyzerStreamConfig streamConfig) => streamConfig.StreamId)
				where !newIds.Contains(id)
				select id;
			if (enumerable.Count<string>() > 0)
			{
				throw new CCSValidationException(string.Format(CultureInfo.InvariantCulture, "There is an existing stream id for which there is no config. ID: {0}", new object[] { enumerable.First<string>() }));
			}
			FailureAnalyzerConfigurationHelper.ValidateUniqueness<string>(this.FailureAnalyzerStreamConfigList.Select((FailureAnalyzerStreamConfig streamConfig) => streamConfig.StreamId), "stream id");
			foreach (FailureAnalyzerStreamConfig failureAnalyzerStreamConfig in this.FailureAnalyzerStreamConfigList)
			{
				failureAnalyzerStreamConfig.ValidateConfiguration();
			}
		}
	}
}

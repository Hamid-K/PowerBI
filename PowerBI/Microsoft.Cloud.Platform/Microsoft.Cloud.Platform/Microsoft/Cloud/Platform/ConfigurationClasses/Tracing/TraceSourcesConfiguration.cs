using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.ConfigurationManagement;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationClasses.Tracing
{
	// Token: 0x0200043B RID: 1083
	[ConfigurationRoot(Consumers = ".*", Options = ConfigurationOptions.AutoReconfigure)]
	[Serializable]
	public sealed class TraceSourcesConfiguration : ConfigurationClass
	{
		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x0600219C RID: 8604 RVA: 0x0007D03B File Offset: 0x0007B23B
		// (set) Token: 0x0600219D RID: 8605 RVA: 0x0007D043 File Offset: 0x0007B243
		[ConfigurationProperty]
		public ConfigurationCollection<TraceSourceConfig> TraceSourceConfigList { get; set; }

		// Token: 0x0600219E RID: 8606 RVA: 0x0007D04C File Offset: 0x0007B24C
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.TraceSourceConfigList.Select((TraceSourceConfig s) => s.ToString());
			return string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
			{
				Environment.NewLine,
				string.Join(Environment.NewLine, enumerable.ToArray<string>())
			});
		}

		// Token: 0x0600219F RID: 8607 RVA: 0x0007D0B4 File Offset: 0x0007B2B4
		public void ValidateConfiguration()
		{
			HashSet<TraceSourceIdentifier> hashSet = new HashSet<TraceSourceIdentifier>();
			foreach (TraceSourceIdentifier traceSourceIdentifier in this.TraceSourceConfigList.Select((TraceSourceConfig traceConfig) => traceConfig.ID))
			{
				if (!hashSet.Add(traceSourceIdentifier))
				{
					throw new CCSValidationException("Trace source id: {0} appears more than once".FormatWithInvariantCulture(new object[] { traceSourceIdentifier }));
				}
			}
		}
	}
}

using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.ApplicationInsights
{
	// Token: 0x0200001F RID: 31
	public sealed class MetricConfigurations
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00006DD7 File Offset: 0x00004FD7
		private MetricConfigurations()
		{
		}

		// Token: 0x0400008F RID: 143
		[SuppressMessage("Microsoft.Security", "CA2104:DoNotDeclareReadOnlyMutableReferenceTypes", Justification = "Singleton is intended.")]
		public static readonly MetricConfigurations Common = new MetricConfigurations();
	}
}

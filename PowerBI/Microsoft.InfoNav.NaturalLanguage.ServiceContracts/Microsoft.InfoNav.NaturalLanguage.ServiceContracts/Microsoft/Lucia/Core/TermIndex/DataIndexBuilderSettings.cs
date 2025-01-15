using System;
using System.ComponentModel;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000153 RID: 339
	[ImmutableObject(true)]
	public sealed class DataIndexBuilderSettings
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0000B7D8 File Offset: 0x000099D8
		public DataIndexBuilderSettings(int? maxStringLength = null, int? maxInstanceCount = null, int? maxSamplesPerColumn = null)
		{
			this.MaxStringLength = maxStringLength ?? 100;
			this.MaxInstanceCount = maxInstanceCount ?? 5000000;
			this.MaxSamplesPerColumn = maxSamplesPerColumn ?? 100;
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060006B4 RID: 1716 RVA: 0x0000B842 File Offset: 0x00009A42
		public int MaxStringLength { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060006B5 RID: 1717 RVA: 0x0000B84A File Offset: 0x00009A4A
		public int MaxInstanceCount { get; }

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060006B6 RID: 1718 RVA: 0x0000B852 File Offset: 0x00009A52
		public int MaxSamplesPerColumn { get; }

		// Token: 0x04000677 RID: 1655
		public static readonly DataIndexBuilderSettings Default = new DataIndexBuilderSettings(null, null, null);
	}
}

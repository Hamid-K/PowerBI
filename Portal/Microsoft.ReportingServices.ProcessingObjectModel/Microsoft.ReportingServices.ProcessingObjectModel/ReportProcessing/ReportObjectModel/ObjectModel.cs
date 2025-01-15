using System;

namespace Microsoft.ReportingServices.ReportProcessing.ReportObjectModel
{
	// Token: 0x0200000D RID: 13
	public abstract class ObjectModel : MarshalByRefObject
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001E RID: 30
		public abstract Fields Fields { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001F RID: 31
		public abstract Parameters Parameters { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000020 RID: 32
		public abstract Globals Globals { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000021 RID: 33
		public abstract User User { get; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000022 RID: 34
		public abstract ReportItems ReportItems { get; }

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000023 RID: 35
		public abstract Aggregates Aggregates { get; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000024 RID: 36
		public abstract DataSets DataSets { get; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000025 RID: 37
		public abstract DataSources DataSources { get; }

		// Token: 0x06000026 RID: 38
		public abstract bool InScope(string scope);

		// Token: 0x06000027 RID: 39
		public abstract int RecursiveLevel(string scope);

		// Token: 0x06000028 RID: 40
		public abstract string CreateDrillthroughContext();
	}
}

using System;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200011C RID: 284
	[Serializable]
	public class ValidationError
	{
		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06001241 RID: 4673 RVA: 0x00080857 File Offset: 0x0007EA57
		// (set) Token: 0x06001242 RID: 4674 RVA: 0x0008085F File Offset: 0x0007EA5F
		public string Message
		{
			get
			{
				return this.message;
			}
			internal set
			{
				this.message = value;
			}
		}

		// Token: 0x1700047A RID: 1146
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x00080868 File Offset: 0x0007EA68
		// (set) Token: 0x06001244 RID: 4676 RVA: 0x00080870 File Offset: 0x0007EA70
		public MetadataObject Source
		{
			get
			{
				return this.source;
			}
			internal set
			{
				this.source = value;
			}
		}

		// Token: 0x040002E4 RID: 740
		private string message;

		// Token: 0x040002E5 RID: 741
		[NonSerialized]
		private MetadataObject source;
	}
}

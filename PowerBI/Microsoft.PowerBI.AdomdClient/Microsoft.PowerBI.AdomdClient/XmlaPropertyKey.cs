using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F4 RID: 244
	internal class XmlaPropertyKey : IXmlaPropertyKey
	{
		// Token: 0x06000D61 RID: 3425 RVA: 0x000307A5 File Offset: 0x0002E9A5
		internal XmlaPropertyKey(string propertyName, string propertyNamespace)
		{
			this.propertyName = propertyName;
			this.propertyNamespace = propertyNamespace;
		}

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x06000D62 RID: 3426 RVA: 0x000307BB File Offset: 0x0002E9BB
		// (set) Token: 0x06000D63 RID: 3427 RVA: 0x000307C3 File Offset: 0x0002E9C3
		public string Name
		{
			get
			{
				return this.propertyName;
			}
			set
			{
				this.propertyName = value;
			}
		}

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x06000D64 RID: 3428 RVA: 0x000307CC File Offset: 0x0002E9CC
		// (set) Token: 0x06000D65 RID: 3429 RVA: 0x000307D4 File Offset: 0x0002E9D4
		public string Namespace
		{
			get
			{
				return this.propertyNamespace;
			}
			set
			{
				this.propertyNamespace = value;
			}
		}

		// Token: 0x0400084C RID: 2124
		private string propertyName;

		// Token: 0x0400084D RID: 2125
		private string propertyNamespace;
	}
}

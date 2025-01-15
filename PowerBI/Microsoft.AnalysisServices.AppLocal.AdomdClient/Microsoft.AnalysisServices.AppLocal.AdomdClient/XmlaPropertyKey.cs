using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F4 RID: 244
	internal class XmlaPropertyKey : IXmlaPropertyKey
	{
		// Token: 0x06000D6E RID: 3438 RVA: 0x00030AD5 File Offset: 0x0002ECD5
		internal XmlaPropertyKey(string propertyName, string propertyNamespace)
		{
			this.propertyName = propertyName;
			this.propertyNamespace = propertyNamespace;
		}

		// Token: 0x17000523 RID: 1315
		// (get) Token: 0x06000D6F RID: 3439 RVA: 0x00030AEB File Offset: 0x0002ECEB
		// (set) Token: 0x06000D70 RID: 3440 RVA: 0x00030AF3 File Offset: 0x0002ECF3
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

		// Token: 0x17000524 RID: 1316
		// (get) Token: 0x06000D71 RID: 3441 RVA: 0x00030AFC File Offset: 0x0002ECFC
		// (set) Token: 0x06000D72 RID: 3442 RVA: 0x00030B04 File Offset: 0x0002ED04
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

		// Token: 0x04000859 RID: 2137
		private string propertyName;

		// Token: 0x0400085A RID: 2138
		private string propertyNamespace;
	}
}

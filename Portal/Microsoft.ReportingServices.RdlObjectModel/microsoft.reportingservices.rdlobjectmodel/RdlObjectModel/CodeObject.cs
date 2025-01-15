using System;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A4 RID: 164
	public class CodeObject : ReportObject
	{
		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000727 RID: 1831 RVA: 0x0001AD1C File Offset: 0x00018F1C
		// (set) Token: 0x06000728 RID: 1832 RVA: 0x0001AD24 File Offset: 0x00018F24
		public string Code
		{
			get
			{
				return this.m_code;
			}
			set
			{
				this.m_code = value;
			}
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001AD2D File Offset: 0x00018F2D
		public CodeObject()
		{
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001AD35 File Offset: 0x00018F35
		public CodeObject(string code)
		{
			this.m_code = code;
		}

		// Token: 0x04000120 RID: 288
		private string m_code;
	}
}

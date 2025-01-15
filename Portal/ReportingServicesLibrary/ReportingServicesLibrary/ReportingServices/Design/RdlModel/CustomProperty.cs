using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x02000400 RID: 1024
	public class CustomProperty
	{
		// Token: 0x060020B7 RID: 8375 RVA: 0x000025F4 File Offset: 0x000007F4
		public CustomProperty()
		{
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0007FA45 File Offset: 0x0007DC45
		public CustomProperty(string name, string value)
		{
			this.m_name = name;
			this.m_value = value;
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x0007FA5B File Offset: 0x0007DC5B
		// (set) Token: 0x060020BA RID: 8378 RVA: 0x0007FA6C File Offset: 0x0007DC6C
		public string Name
		{
			get
			{
				return this.m_name ?? string.Empty;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x060020BB RID: 8379 RVA: 0x0007FA75 File Offset: 0x0007DC75
		// (set) Token: 0x060020BC RID: 8380 RVA: 0x0007FA86 File Offset: 0x0007DC86
		public string Value
		{
			get
			{
				return this.m_value ?? string.Empty;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x04000E4C RID: 3660
		private string m_name;

		// Token: 0x04000E4D RID: 3661
		private string m_value;
	}
}

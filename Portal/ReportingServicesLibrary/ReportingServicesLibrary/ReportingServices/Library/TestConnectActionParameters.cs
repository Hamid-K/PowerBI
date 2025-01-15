using System;
using Microsoft.ReportingServices.DataExtensions;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x02000228 RID: 552
	internal abstract class TestConnectActionParameters : RSSoapActionParameters
	{
		// Token: 0x170005CE RID: 1486
		// (get) Token: 0x060013D7 RID: 5079 RVA: 0x0004B03C File Offset: 0x0004923C
		// (set) Token: 0x060013D8 RID: 5080 RVA: 0x0004B044 File Offset: 0x00049244
		public DataSourceInfo DSInfo
		{
			get
			{
				return this.m_dsInfo;
			}
			set
			{
				this.m_dsInfo = value;
			}
		}

		// Token: 0x170005CF RID: 1487
		// (get) Token: 0x060013D9 RID: 5081 RVA: 0x0004B04D File Offset: 0x0004924D
		// (set) Token: 0x060013DA RID: 5082 RVA: 0x0004B055 File Offset: 0x00049255
		public string UserName
		{
			get
			{
				return this.m_userName;
			}
			set
			{
				this.m_userName = value;
			}
		}

		// Token: 0x170005D0 RID: 1488
		// (get) Token: 0x060013DB RID: 5083 RVA: 0x0004B05E File Offset: 0x0004925E
		// (set) Token: 0x060013DC RID: 5084 RVA: 0x0004B066 File Offset: 0x00049266
		public string Password
		{
			get
			{
				return this.m_password;
			}
			set
			{
				this.m_password = value;
			}
		}

		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060013DD RID: 5085 RVA: 0x0004B06F File Offset: 0x0004926F
		// (set) Token: 0x060013DE RID: 5086 RVA: 0x0004B077 File Offset: 0x00049277
		public string ConnectionError
		{
			get
			{
				return this.m_connectionerror;
			}
			set
			{
				this.m_connectionerror = value;
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060013DF RID: 5087 RVA: 0x0004B080 File Offset: 0x00049280
		// (set) Token: 0x060013E0 RID: 5088 RVA: 0x0004B088 File Offset: 0x00049288
		public bool ConnectionSuccess
		{
			get
			{
				return this.m_connectionSuccess;
			}
			set
			{
				this.m_connectionSuccess = value;
			}
		}

		// Token: 0x0400070F RID: 1807
		private DataSourceInfo m_dsInfo;

		// Token: 0x04000710 RID: 1808
		private string m_userName;

		// Token: 0x04000711 RID: 1809
		private string m_password;

		// Token: 0x04000712 RID: 1810
		private string m_connectionerror;

		// Token: 0x04000713 RID: 1811
		private bool m_connectionSuccess;
	}
}

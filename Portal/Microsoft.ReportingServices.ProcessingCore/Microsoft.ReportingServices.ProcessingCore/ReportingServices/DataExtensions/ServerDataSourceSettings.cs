using System;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B2 RID: 1458
	public sealed class ServerDataSourceSettings
	{
		// Token: 0x0600526E RID: 21102 RVA: 0x0015B9B4 File Offset: 0x00159BB4
		public ServerDataSourceSettings(bool isSurrogatePresent, bool allowIntegratedSecurity)
		{
			this.m_isSurrogatePresent = isSurrogatePresent;
			this.m_allowIntegratedSecurity = allowIntegratedSecurity;
		}

		// Token: 0x17001EA9 RID: 7849
		// (get) Token: 0x0600526F RID: 21103 RVA: 0x0015B9D1 File Offset: 0x00159BD1
		public bool IsSurrogatePresent
		{
			get
			{
				return this.m_isSurrogatePresent;
			}
		}

		// Token: 0x17001EAA RID: 7850
		// (get) Token: 0x06005270 RID: 21104 RVA: 0x0015B9D9 File Offset: 0x00159BD9
		public bool AllowIntegratedSecurity
		{
			get
			{
				return this.m_allowIntegratedSecurity;
			}
		}

		// Token: 0x04002997 RID: 10647
		private bool m_allowIntegratedSecurity = true;

		// Token: 0x04002998 RID: 10648
		private bool m_isSurrogatePresent;
	}
}

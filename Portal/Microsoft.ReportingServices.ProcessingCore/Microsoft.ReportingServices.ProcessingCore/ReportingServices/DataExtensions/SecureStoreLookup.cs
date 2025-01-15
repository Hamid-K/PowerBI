using System;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x020005B6 RID: 1462
	public sealed class SecureStoreLookup
	{
		// Token: 0x060052FD RID: 21245 RVA: 0x0015D571 File Offset: 0x0015B771
		internal SecureStoreLookup(SecureStoreLookup.LookupContextOptions lookupContext, string targetApplicationId)
		{
			this.m_lookUpContext = lookupContext;
			this.m_targetApplicationId = targetApplicationId;
		}

		// Token: 0x17001ED4 RID: 7892
		// (get) Token: 0x060052FE RID: 21246 RVA: 0x0015D587 File Offset: 0x0015B787
		public SecureStoreLookup.LookupContextOptions LookupContext
		{
			get
			{
				return this.m_lookUpContext;
			}
		}

		// Token: 0x17001ED5 RID: 7893
		// (get) Token: 0x060052FF RID: 21247 RVA: 0x0015D58F File Offset: 0x0015B78F
		public string TargetApplicationId
		{
			get
			{
				return this.m_targetApplicationId;
			}
		}

		// Token: 0x040029D3 RID: 10707
		public readonly string m_targetApplicationId;

		// Token: 0x040029D4 RID: 10708
		public readonly SecureStoreLookup.LookupContextOptions m_lookUpContext;

		// Token: 0x02000C09 RID: 3081
		public enum LookupContextOptions
		{
			// Token: 0x04004802 RID: 18434
			AuthenticatedUser,
			// Token: 0x04004803 RID: 18435
			Unattended
		}
	}
}

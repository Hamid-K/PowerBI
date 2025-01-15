using System;

namespace Microsoft.ReportingServices.RdlObjectModel.ExpressionParser
{
	// Token: 0x0200021A RID: 538
	internal sealed class DefaultEnvironmentFilter : IEnvironmentFilter
	{
		// Token: 0x0600122F RID: 4655 RVA: 0x00028E9E File Offset: 0x0002709E
		private DefaultEnvironmentFilter()
		{
		}

		// Token: 0x06001230 RID: 4656 RVA: 0x00028EA6 File Offset: 0x000270A6
		public bool IsAllowedType(Type type, out bool allowNew, out bool allowNewArray)
		{
			allowNew = true;
			allowNewArray = true;
			return true;
		}

		// Token: 0x06001231 RID: 4657 RVA: 0x00028EAF File Offset: 0x000270AF
		public bool IsAllowedMember(string memberName)
		{
			return true;
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x06001232 RID: 4658 RVA: 0x00028EB2 File Offset: 0x000270B2
		internal static IEnvironmentFilter Instance
		{
			get
			{
				return DefaultEnvironmentFilter.m_instance;
			}
		}

		// Token: 0x040005C8 RID: 1480
		private static readonly IEnvironmentFilter m_instance = new DefaultEnvironmentFilter();
	}
}

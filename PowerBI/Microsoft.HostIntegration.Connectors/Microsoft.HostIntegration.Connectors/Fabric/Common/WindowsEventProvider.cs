using System;
using System.Diagnostics.Eventing;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200043A RID: 1082
	internal static class WindowsEventProvider
	{
		// Token: 0x060025AD RID: 9645 RVA: 0x00073689 File Offset: 0x00071889
		public static bool EventFederationLeaseExpiration(string param1, string param2, string param3, string param4)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatessss(ref WindowsEventProvider.FederationLeaseExpiration, param1, param2, param3, param4);
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x000736AC File Offset: 0x000718AC
		public static bool EventAdminCreateNamespace(string param1, string param2)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatess(ref WindowsEventProvider.AdminCreateNamespace, param1, param2);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x000736CD File Offset: 0x000718CD
		public static bool EventAdminDeleteNamespace(string param1, string param2)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatess(ref WindowsEventProvider.AdminDeleteNamespace, param1, param2);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x000736EE File Offset: 0x000718EE
		public static bool EventSQLStoreOpenFailed(string param1, string param2)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatess(ref WindowsEventProvider.SQLStoreOpenFailed, param1, param2);
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x0007370F File Offset: 0x0007190F
		public static bool EventExternalStoreOpenFailed(string param1, string param2)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatess(ref WindowsEventProvider.ExternalStoreOpenFailed, param1, param2);
		}

		// Token: 0x060025B2 RID: 9650 RVA: 0x00073730 File Offset: 0x00071930
		public static bool EventLookupTableRefresh(string param1, string param2)
		{
			return !WindowsEventProvider.m_provider.IsEnabled() || WindowsEventProvider.m_provider.Templatess(ref WindowsEventProvider.LookupTableRefresh, param1, param2);
		}

		// Token: 0x040016C3 RID: 5827
		private static EventProviderVersionTwo m_provider = new EventProviderVersionTwo(new Guid("751c9dc0-4f51-44f6-920a-a620c7c2d13e"));

		// Token: 0x040016C4 RID: 5828
		private static EventDescriptor FederationLeaseExpiration = new EventDescriptor(1, 0, 16, 2, 0, 0, long.MinValue);

		// Token: 0x040016C5 RID: 5829
		private static EventDescriptor AdminCreateNamespace = new EventDescriptor(2, 0, 16, 2, 0, 0, long.MinValue);

		// Token: 0x040016C6 RID: 5830
		private static EventDescriptor AdminDeleteNamespace = new EventDescriptor(3, 0, 16, 2, 0, 0, long.MinValue);

		// Token: 0x040016C7 RID: 5831
		private static EventDescriptor SQLStoreOpenFailed = new EventDescriptor(4, 0, 16, 2, 0, 0, long.MinValue);

		// Token: 0x040016C8 RID: 5832
		private static EventDescriptor ExternalStoreOpenFailed = new EventDescriptor(5, 0, 16, 2, 0, 0, long.MinValue);

		// Token: 0x040016C9 RID: 5833
		private static EventDescriptor LookupTableRefresh = new EventDescriptor(6, 0, 16, 3, 0, 0, long.MinValue);
	}
}

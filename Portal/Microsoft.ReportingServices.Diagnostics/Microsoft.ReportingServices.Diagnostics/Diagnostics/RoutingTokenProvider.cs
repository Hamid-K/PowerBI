using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000059 RID: 89
	internal sealed class RoutingTokenProvider
	{
		// Token: 0x060002BF RID: 703 RVA: 0x00002E32 File Offset: 0x00001032
		private RoutingTokenProvider()
		{
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x0000C479 File Offset: 0x0000A679
		internal static void InitRoutingTokenFactory(IRoutingTokenFactory routingTokenFactory)
		{
			RoutingTokenProvider.m_routingTokenFactory = routingTokenFactory;
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000C481 File Offset: 0x0000A681
		internal static IRoutingTokenFactory CurrentRoutingTokenFactory
		{
			get
			{
				return RoutingTokenProvider.m_routingTokenFactory;
			}
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x0000C488 File Offset: 0x0000A688
		public static string GenerateRoutingToken(string sessionId)
		{
			if (RoutingTokenProvider.m_routingTokenFactory == null)
			{
				return null;
			}
			return RoutingTokenProvider.m_routingTokenFactory.GenerateRoutingToken(sessionId);
		}

		// Token: 0x040002D1 RID: 721
		private static IRoutingTokenFactory m_routingTokenFactory;
	}
}

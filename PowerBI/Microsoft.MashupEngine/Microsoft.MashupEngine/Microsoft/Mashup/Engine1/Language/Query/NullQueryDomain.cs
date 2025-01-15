using System;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x020017FA RID: 6138
	internal class NullQueryDomain : IQueryDomain
	{
		// Token: 0x06009AF9 RID: 39673 RVA: 0x00201000 File Offset: 0x001FF200
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			return domain == NullQueryDomain.Instance;
		}

		// Token: 0x170027DE RID: 10206
		// (get) Token: 0x06009AFA RID: 39674 RVA: 0x00002105 File Offset: 0x00000305
		public bool CanIndex
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06009AFB RID: 39675 RVA: 0x0000A6A5 File Offset: 0x000088A5
		public Query Optimize(Query query)
		{
			return query;
		}

		// Token: 0x040051FF RID: 20991
		public static readonly IQueryDomain Instance = new NullQueryDomain();
	}
}

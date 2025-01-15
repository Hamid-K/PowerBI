using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Salesforce
{
	// Token: 0x020001E6 RID: 486
	internal class SoqlQueryDomain : IQueryDomain
	{
		// Token: 0x060009A6 RID: 2470 RVA: 0x00014043 File Offset: 0x00012243
		public SoqlQueryDomain(string identifier)
		{
			this.identifier = identifier;
		}

		// Token: 0x170002CA RID: 714
		// (get) Token: 0x060009A7 RID: 2471 RVA: 0x00014052 File Offset: 0x00012252
		public string Identifier
		{
			get
			{
				return this.identifier;
			}
		}

		// Token: 0x060009A8 RID: 2472 RVA: 0x0001405C File Offset: 0x0001225C
		public bool IsCompatibleWith(IQueryDomain domain)
		{
			SoqlQueryDomain soqlQueryDomain = domain as SoqlQueryDomain;
			return soqlQueryDomain != null && soqlQueryDomain.identifier == this.identifier;
		}

		// Token: 0x170002CB RID: 715
		// (get) Token: 0x060009A9 RID: 2473 RVA: 0x00002139 File Offset: 0x00000339
		public bool CanIndex
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060009AA RID: 2474 RVA: 0x00014086 File Offset: 0x00012286
		public Query Optimize(Query query)
		{
			return new SoqlOptimizingQueryVisitor().Optimize(query);
		}

		// Token: 0x040005CE RID: 1486
		private readonly string identifier;
	}
}

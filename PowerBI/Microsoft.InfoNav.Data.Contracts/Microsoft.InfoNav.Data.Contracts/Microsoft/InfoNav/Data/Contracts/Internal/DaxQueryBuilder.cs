using System;
using System.Text;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000175 RID: 373
	public sealed class DaxQueryBuilder
	{
		// Token: 0x060009B9 RID: 2489 RVA: 0x00013C0C File Offset: 0x00011E0C
		public DaxQueryBuilder(int capacity = 200)
		{
			this.m_queryBuilder = new StringBuilder((capacity <= 0) ? 200 : capacity);
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x00013C2B File Offset: 0x00011E2B
		public static string CreateEvaluate(string expression, int capacity = 200)
		{
			return new DaxQueryBuilder(capacity).Evaluate(expression).ToString();
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x00013C3E File Offset: 0x00011E3E
		public DaxQueryBuilder Evaluate(string expression)
		{
			this.m_queryBuilder.Append("EVALUATE");
			this.m_queryBuilder.Append(' ');
			this.m_queryBuilder.Append(expression);
			return this;
		}

		// Token: 0x060009BC RID: 2492 RVA: 0x00013C6D File Offset: 0x00011E6D
		public override string ToString()
		{
			return this.m_queryBuilder.ToString();
		}

		// Token: 0x0400057A RID: 1402
		private const int DefaultCapacity = 200;

		// Token: 0x0400057B RID: 1403
		private readonly StringBuilder m_queryBuilder;
	}
}

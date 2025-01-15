using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011E9 RID: 4585
	internal sealed class PagingClause
	{
		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x060078E0 RID: 30944 RVA: 0x001A245C File Offset: 0x001A065C
		// (set) Token: 0x060078E1 RID: 30945 RVA: 0x001A2464 File Offset: 0x001A0664
		public long? FetchExpression { get; set; }

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x060078E2 RID: 30946 RVA: 0x001A246D File Offset: 0x001A066D
		// (set) Token: 0x060078E3 RID: 30947 RVA: 0x001A2475 File Offset: 0x001A0675
		public long OffsetExpression { get; set; }

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x060078E4 RID: 30948 RVA: 0x001A247E File Offset: 0x001A067E
		// (set) Token: 0x060078E5 RID: 30949 RVA: 0x001A2486 File Offset: 0x001A0686
		public bool SuppressOrderBy { get; set; }

		// Token: 0x060078E6 RID: 30950 RVA: 0x001A248F File Offset: 0x001A068F
		public PagingClause ShallowCopy()
		{
			return new PagingClause
			{
				OffsetExpression = this.OffsetExpression,
				FetchExpression = this.FetchExpression,
				SuppressOrderBy = this.SuppressOrderBy
			};
		}
	}
}

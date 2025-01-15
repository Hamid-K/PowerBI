using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001216 RID: 4630
	internal abstract class SqlQueryExpression : SqlExpression
	{
		// Token: 0x1700215B RID: 8539
		// (get) Token: 0x06007A24 RID: 31268 RVA: 0x0014213C File Offset: 0x0014033C
		public static int QueryPrecedence
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700215C RID: 8540
		// (get) Token: 0x06007A25 RID: 31269 RVA: 0x001A6C31 File Offset: 0x001A4E31
		public override int Precedence
		{
			get
			{
				return SqlQueryExpression.QueryPrecedence;
			}
		}
	}
}

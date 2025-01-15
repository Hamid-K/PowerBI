using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011D2 RID: 4562
	internal sealed class FromQuery : FromItem
	{
		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06007874 RID: 30836 RVA: 0x001A143F File Offset: 0x0019F63F
		// (set) Token: 0x06007875 RID: 30837 RVA: 0x001A1447 File Offset: 0x0019F647
		public SqlQueryExpression Query { get; set; }

		// Token: 0x06007876 RID: 30838 RVA: 0x001A1450 File Offset: 0x0019F650
		public override FromItem ShallowCopy()
		{
			return new FromQuery
			{
				Alias = base.Alias,
				Query = this.Query,
				RotationClause = base.RotationClause
			};
		}

		// Token: 0x06007877 RID: 30839 RVA: 0x001A147B File Offset: 0x0019F67B
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(SqlQueryExpression.QueryPrecedence, this.Query);
			base.WriteRotationClause(writer);
			if (base.Alias != null)
			{
				writer.WriteFromAlias(base.Alias);
			}
		}
	}
}

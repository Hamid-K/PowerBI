using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011CF RID: 4559
	internal sealed class FieldAccessExpression : ScalarExpression
	{
		// Token: 0x06007864 RID: 30820 RVA: 0x001A1361 File Offset: 0x0019F561
		public FieldAccessExpression(ColumnReference column, ConstantSqlString field)
		{
			this.column = column;
			this.field = field;
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06007865 RID: 30821 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007866 RID: 30822 RVA: 0x001A1377 File Offset: 0x0019F577
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.column.WriteCreateScript(writer);
			writer.Write(SqlLanguageSymbols.DotSqlString);
			writer.Write(this.field);
		}

		// Token: 0x040041AB RID: 16811
		private readonly ColumnReference column;

		// Token: 0x040041AC RID: 16812
		private readonly ConstantSqlString field;
	}
}

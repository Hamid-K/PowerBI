using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011C2 RID: 4546
	internal sealed class ColumnTypeExpression : ScalarExpression
	{
		// Token: 0x06007830 RID: 30768 RVA: 0x001A0E99 File Offset: 0x0019F099
		public ColumnTypeExpression(SqlDataType columnType)
		{
			this.columnType = columnType;
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06007831 RID: 30769 RVA: 0x00002105 File Offset: 0x00000305
		public override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06007832 RID: 30770 RVA: 0x001A0EA8 File Offset: 0x0019F0A8
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.columnType.WriteCreateScript(writer);
		}

		// Token: 0x0400415F RID: 16735
		private readonly SqlDataType columnType;
	}
}

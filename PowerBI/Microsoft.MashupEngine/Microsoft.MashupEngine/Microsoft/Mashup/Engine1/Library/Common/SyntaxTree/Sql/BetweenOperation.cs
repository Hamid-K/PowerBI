using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011B8 RID: 4536
	internal sealed class BetweenOperation : Condition
	{
		// Token: 0x060077F5 RID: 30709 RVA: 0x001A073D File Offset: 0x0019E93D
		public BetweenOperation(SqlExpression value, SqlExpression minimum, SqlExpression maximum)
		{
			this.Value = value;
			this.Minimum = minimum;
			this.Maximum = maximum;
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x060077F6 RID: 30710 RVA: 0x001A075A File Offset: 0x0019E95A
		// (set) Token: 0x060077F7 RID: 30711 RVA: 0x001A0762 File Offset: 0x0019E962
		public SqlExpression Maximum { get; private set; }

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x060077F8 RID: 30712 RVA: 0x001A076B File Offset: 0x0019E96B
		// (set) Token: 0x060077F9 RID: 30713 RVA: 0x001A0773 File Offset: 0x0019E973
		public SqlExpression Minimum { get; private set; }

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x060077FA RID: 30714 RVA: 0x000024ED File Offset: 0x000006ED
		public override int Precedence
		{
			get
			{
				return 8;
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x060077FB RID: 30715 RVA: 0x001A077C File Offset: 0x0019E97C
		// (set) Token: 0x060077FC RID: 30716 RVA: 0x001A0784 File Offset: 0x0019E984
		public SqlExpression Value { get; private set; }

		// Token: 0x060077FD RID: 30717 RVA: 0x001A0790 File Offset: 0x0019E990
		public override void WriteCreateScript(ScriptWriter writer)
		{
			writer.WriteSubexpression(this.Precedence, this.Value);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.BetweenSqlString);
			writer.WriteSubexpression(this.Precedence, this.Minimum);
			writer.WriteSpaceBeforeAndAfter(SqlLanguageStrings.AndSqlString);
			writer.WriteSubexpression(this.Precedence, this.Maximum);
		}
	}
}

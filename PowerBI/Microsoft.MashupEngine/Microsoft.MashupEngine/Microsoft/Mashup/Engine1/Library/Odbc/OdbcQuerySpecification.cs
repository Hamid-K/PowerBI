using System;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x02000644 RID: 1604
	internal sealed class OdbcQuerySpecification : QuerySpecification
	{
		// Token: 0x1700125F RID: 4703
		// (get) Token: 0x060032FF RID: 13055 RVA: 0x000A3765 File Offset: 0x000A1965
		// (set) Token: 0x06003300 RID: 13056 RVA: 0x000A376D File Offset: 0x000A196D
		public OdbcLimitClause LimitClause { get; set; }

		// Token: 0x06003301 RID: 13057 RVA: 0x000A3776 File Offset: 0x000A1976
		public OdbcQuerySpecification ShallowCopy()
		{
			return base.ShallowCopyTo<OdbcQuerySpecification>(new OdbcQuerySpecification
			{
				LimitClause = this.LimitClause
			});
		}

		// Token: 0x06003302 RID: 13058 RVA: 0x000A3790 File Offset: 0x000A1990
		public override void WriteCreateScript(ScriptWriter writer)
		{
			if (this.LimitClause != null && this.LimitClause.Location == OdbcLimitClauseLocation.BeforeQuerySpecification)
			{
				this.LimitClause.WriteCreateScript(writer);
				writer.WriteLine();
			}
			base.WriteCreateScript(writer);
			if (this.LimitClause != null && this.LimitClause.Location == OdbcLimitClauseLocation.AfterQuerySpecification)
			{
				writer.WriteLine();
				this.LimitClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x06003303 RID: 13059 RVA: 0x000A37F4 File Offset: 0x000A19F4
		protected override void WriteSelectModifiers(ScriptWriter writer)
		{
			if (this.LimitClause != null && this.LimitClause.Location == OdbcLimitClauseLocation.AfterSelectBeforeModifiers)
			{
				this.LimitClause.WriteCreateScript(writer);
				writer.WriteLine();
			}
			base.WriteSelectModifiers(writer);
			if (this.LimitClause != null && this.LimitClause.Location == OdbcLimitClauseLocation.AfterSelect)
			{
				this.LimitClause.WriteCreateScript(writer);
				writer.WriteLine();
			}
		}
	}
}

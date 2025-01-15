using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001207 RID: 4615
	internal sealed class SqlDeleteStatement : SqlStatement
	{
		// Token: 0x060079BA RID: 31162 RVA: 0x001A4ACD File Offset: 0x001A2CCD
		public SqlDeleteStatement(TableReference table, OutputClause outputClause, Condition whereClause = null)
		{
			this.table = table;
			this.whereClause = whereClause;
			this.outputClause = outputClause;
		}

		// Token: 0x1700214E RID: 8526
		// (get) Token: 0x060079BB RID: 31163 RVA: 0x001A4AEA File Offset: 0x001A2CEA
		public TableReference Table
		{
			get
			{
				return this.table;
			}
		}

		// Token: 0x1700214F RID: 8527
		// (get) Token: 0x060079BC RID: 31164 RVA: 0x001A4AF2 File Offset: 0x001A2CF2
		public Condition WhereClause
		{
			get
			{
				return this.whereClause;
			}
		}

		// Token: 0x17002150 RID: 8528
		// (get) Token: 0x060079BD RID: 31165 RVA: 0x001A4AFA File Offset: 0x001A2CFA
		public OutputClause OutputClause
		{
			get
			{
				return this.outputClause;
			}
		}

		// Token: 0x060079BE RID: 31166 RVA: 0x001A4B04 File Offset: 0x001A2D04
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.outputClause.WritePrefixScript(writer);
			writer.WriteSpaceAfter(writer.Settings.DeleteCommand);
			this.table.WriteCreateScript(writer);
			this.outputClause.WriteCreateScript(writer);
			if (this.whereClause != null)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
				this.whereClause.WriteCreateScript(writer);
			}
			this.outputClause.WriteSuffixScript(writer);
		}

		// Token: 0x0400426F RID: 17007
		private readonly TableReference table;

		// Token: 0x04004270 RID: 17008
		private readonly Condition whereClause;

		// Token: 0x04004271 RID: 17009
		private readonly OutputClause outputClause;
	}
}

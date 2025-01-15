using System;
using System.Collections.Generic;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000038 RID: 56
	internal class SqlSelectClauseBuilder : SqlBuilder
	{
		// Token: 0x06000586 RID: 1414 RVA: 0x00019737 File Offset: 0x00017937
		internal void AddOptionalColumn(OptionalColumn column)
		{
			if (this.m_optionalColumns == null)
			{
				this.m_optionalColumns = new List<OptionalColumn>();
			}
			this.m_optionalColumns.Add(column);
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x00019758 File Offset: 0x00017958
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x00019760 File Offset: 0x00017960
		internal TopClause Top
		{
			get
			{
				return this.m_top;
			}
			set
			{
				this.m_top = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x00019769 File Offset: 0x00017969
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x00019771 File Offset: 0x00017971
		internal SkipClause Skip
		{
			get
			{
				return this.m_skip;
			}
			set
			{
				this.m_skip = value;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0001977A File Offset: 0x0001797A
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x00019782 File Offset: 0x00017982
		internal bool IsDistinct { get; set; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0001978B File Offset: 0x0001798B
		public override bool IsEmpty
		{
			get
			{
				return base.IsEmpty && (this.m_optionalColumns == null || this.m_optionalColumns.Count == 0);
			}
		}

		// Token: 0x0600058E RID: 1422 RVA: 0x000197AF File Offset: 0x000179AF
		internal SqlSelectClauseBuilder(Func<bool> isPartOfTopMostStatement)
		{
			this.m_isPartOfTopMostStatement = isPartOfTopMostStatement;
		}

		// Token: 0x0600058F RID: 1423 RVA: 0x000197C0 File Offset: 0x000179C0
		public override void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			writer.Write("SELECT ");
			if (this.IsDistinct)
			{
				writer.Write("DISTINCT ");
			}
			if (this.Top != null && this.Skip == null)
			{
				this.Top.WriteSql(writer, sqlGenerator);
			}
			if (this.IsEmpty)
			{
				writer.Write("*");
				return;
			}
			bool flag = this.WriteOptionalColumns(writer, sqlGenerator);
			if (!base.IsEmpty)
			{
				if (flag)
				{
					writer.Write(", ");
				}
				base.WriteSql(writer, sqlGenerator);
				return;
			}
			if (!flag)
			{
				this.m_optionalColumns[0].MarkAsUsed();
				this.m_optionalColumns[0].WriteSqlIfUsed(writer, sqlGenerator, "");
			}
		}

		// Token: 0x06000590 RID: 1424 RVA: 0x00019874 File Offset: 0x00017A74
		private bool WriteOptionalColumns(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			if (this.m_optionalColumns == null)
			{
				return false;
			}
			if (this.m_isPartOfTopMostStatement() || this.IsDistinct)
			{
				foreach (OptionalColumn optionalColumn in this.m_optionalColumns)
				{
					optionalColumn.MarkAsUsed();
				}
			}
			string text = "";
			bool flag = false;
			using (List<OptionalColumn>.Enumerator enumerator = this.m_optionalColumns.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.WriteSqlIfUsed(writer, sqlGenerator, text))
					{
						flag = true;
						text = ", ";
					}
				}
			}
			return flag;
		}

		// Token: 0x0400010B RID: 267
		private List<OptionalColumn> m_optionalColumns;

		// Token: 0x0400010C RID: 268
		private TopClause m_top;

		// Token: 0x0400010D RID: 269
		private SkipClause m_skip;

		// Token: 0x0400010F RID: 271
		private readonly Func<bool> m_isPartOfTopMostStatement;
	}
}

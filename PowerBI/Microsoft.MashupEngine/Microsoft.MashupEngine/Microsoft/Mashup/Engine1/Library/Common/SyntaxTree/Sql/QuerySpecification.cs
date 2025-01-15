using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x020011F1 RID: 4593
	internal class QuerySpecification : SqlQueryExpression
	{
		// Token: 0x0600790D RID: 30989 RVA: 0x001A2FBF File Offset: 0x001A11BF
		public QuerySpecification()
		{
			this.RepeatedRowOption = RepeatedRowOption.All;
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x0600790E RID: 30990 RVA: 0x001A2FCE File Offset: 0x001A11CE
		// (set) Token: 0x0600790F RID: 30991 RVA: 0x001A2FD6 File Offset: 0x001A11D6
		public IList<SelectItem> SelectItems { get; set; }

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06007910 RID: 30992 RVA: 0x001A2FDF File Offset: 0x001A11DF
		// (set) Token: 0x06007911 RID: 30993 RVA: 0x001A2FE7 File Offset: 0x001A11E7
		public IList<FromItem> FromItems { get; set; }

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06007912 RID: 30994 RVA: 0x001A2FF0 File Offset: 0x001A11F0
		// (set) Token: 0x06007913 RID: 30995 RVA: 0x001A2FF8 File Offset: 0x001A11F8
		public GroupByClause GroupByClause { get; set; }

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06007914 RID: 30996 RVA: 0x001A3001 File Offset: 0x001A1201
		// (set) Token: 0x06007915 RID: 30997 RVA: 0x001A3009 File Offset: 0x001A1209
		public Condition HavingClause { get; set; }

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x06007916 RID: 30998 RVA: 0x001A3012 File Offset: 0x001A1212
		// (set) Token: 0x06007917 RID: 30999 RVA: 0x001A301A File Offset: 0x001A121A
		public Condition WhereClause { get; set; }

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x06007918 RID: 31000 RVA: 0x001A3023 File Offset: 0x001A1223
		// (set) Token: 0x06007919 RID: 31001 RVA: 0x001A302B File Offset: 0x001A122B
		public OrderByClause OrderByClause { get; set; }

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x0600791A RID: 31002 RVA: 0x001A3034 File Offset: 0x001A1234
		// (set) Token: 0x0600791B RID: 31003 RVA: 0x001A303C File Offset: 0x001A123C
		public RepeatedRowOption RepeatedRowOption { get; set; }

		// Token: 0x0600791C RID: 31004 RVA: 0x001A3045 File Offset: 0x001A1245
		public override void WriteCreateScript(ScriptWriter writer)
		{
			this.WriteSelectClause(writer);
			this.WriteFromClause(writer);
			this.WriteWhereClause(writer);
			this.WriteGroupByClause(writer);
			this.WriteHavingClause(writer);
			this.WriteOrderByClause(writer);
		}

		// Token: 0x0600791D RID: 31005 RVA: 0x001A3074 File Offset: 0x001A1274
		protected T ShallowCopyTo<T>(T querySpecification) where T : QuerySpecification
		{
			querySpecification.SelectItems = this.SelectItems;
			querySpecification.FromItems = this.FromItems;
			querySpecification.GroupByClause = this.GroupByClause;
			querySpecification.HavingClause = this.HavingClause;
			querySpecification.OrderByClause = this.OrderByClause;
			querySpecification.RepeatedRowOption = this.RepeatedRowOption;
			querySpecification.WhereClause = this.WhereClause;
			return querySpecification;
		}

		// Token: 0x0600791E RID: 31006 RVA: 0x001A30FC File Offset: 0x001A12FC
		protected virtual void WriteSelectColumnList(ScriptWriter writer)
		{
			bool flag = false;
			foreach (SelectItem selectItem in this.SelectItems)
			{
				flag = writer.WriteLineCommaIfNeeded(flag);
				selectItem.WriteCreateScript(writer);
			}
		}

		// Token: 0x0600791F RID: 31007 RVA: 0x001A3154 File Offset: 0x001A1354
		protected virtual void WriteSelectModifiers(ScriptWriter writer)
		{
			if (this.RepeatedRowOption == RepeatedRowOption.Distinct)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.DistinctSqlString);
			}
		}

		// Token: 0x06007920 RID: 31008 RVA: 0x001A316A File Offset: 0x001A136A
		protected virtual void WriteSelectClause(ScriptWriter writer)
		{
			if (this.SelectItems.Count > 0)
			{
				writer.WriteSpaceAfter(SqlLanguageStrings.SelectSqlString);
				this.WriteSelectModifiers(writer);
				writer.Indent();
				this.WriteSelectColumnList(writer);
				writer.Unindent();
			}
		}

		// Token: 0x06007921 RID: 31009 RVA: 0x001A31A0 File Offset: 0x001A13A0
		protected virtual void WriteFromClause(ScriptWriter writer)
		{
			if (this.FromItems.Count > 0)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.FromSqlString);
				bool flag = false;
				foreach (FromItem fromItem in this.FromItems)
				{
					flag = writer.WriteLineCommaIfNeeded(flag);
					fromItem.WriteCreateScript(writer);
				}
			}
		}

		// Token: 0x06007922 RID: 31010 RVA: 0x001A3214 File Offset: 0x001A1414
		protected virtual void WriteWhereClause(ScriptWriter writer)
		{
			if (this.WhereClause != null)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.WhereSqlString);
				this.WhereClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x06007923 RID: 31011 RVA: 0x001A323B File Offset: 0x001A143B
		protected virtual void WriteGroupByClause(ScriptWriter writer)
		{
			if (this.GroupByClause != null)
			{
				writer.WriteLine();
				this.GroupByClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x06007924 RID: 31012 RVA: 0x001A3257 File Offset: 0x001A1457
		protected virtual void WriteOrderByClause(ScriptWriter writer)
		{
			if (this.OrderByClause != null)
			{
				writer.WriteLine();
				this.OrderByClause.WriteCreateScript(writer);
			}
		}

		// Token: 0x06007925 RID: 31013 RVA: 0x001A3273 File Offset: 0x001A1473
		protected virtual void WriteHavingClause(ScriptWriter writer)
		{
			if (this.HavingClause != null)
			{
				writer.WriteLine();
				writer.WriteSpaceAfter(SqlLanguageStrings.HavingSqlString);
				this.HavingClause.WriteCreateScript(writer);
			}
		}
	}
}

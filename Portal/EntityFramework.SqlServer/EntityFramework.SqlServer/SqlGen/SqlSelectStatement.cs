using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x02000039 RID: 57
	internal sealed class SqlSelectStatement : ISqlFragment
	{
		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x00019938 File Offset: 0x00017B38
		// (set) Token: 0x06000592 RID: 1426 RVA: 0x00019940 File Offset: 0x00017B40
		internal bool OutputColumnsRenamed { get; set; }

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x00019949 File Offset: 0x00017B49
		// (set) Token: 0x06000594 RID: 1428 RVA: 0x00019951 File Offset: 0x00017B51
		internal Dictionary<string, Symbol> OutputColumns { get; set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000595 RID: 1429 RVA: 0x0001995A File Offset: 0x00017B5A
		// (set) Token: 0x06000596 RID: 1430 RVA: 0x00019962 File Offset: 0x00017B62
		internal List<Symbol> AllJoinExtents { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0001996B File Offset: 0x00017B6B
		internal List<Symbol> FromExtents
		{
			get
			{
				if (this.fromExtents == null)
				{
					this.fromExtents = new List<Symbol>();
				}
				return this.fromExtents;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x00019986 File Offset: 0x00017B86
		internal Dictionary<Symbol, bool> OuterExtents
		{
			get
			{
				if (this.outerExtents == null)
				{
					this.outerExtents = new Dictionary<Symbol, bool>();
				}
				return this.outerExtents;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x000199A1 File Offset: 0x00017BA1
		internal SqlSelectClauseBuilder Select
		{
			get
			{
				return this.select;
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x000199A9 File Offset: 0x00017BA9
		internal SqlBuilder From
		{
			get
			{
				return this.from;
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x000199B1 File Offset: 0x00017BB1
		internal SqlBuilder Where
		{
			get
			{
				if (this.where == null)
				{
					this.where = new SqlBuilder();
				}
				return this.where;
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x000199CC File Offset: 0x00017BCC
		internal SqlBuilder GroupBy
		{
			get
			{
				if (this.groupBy == null)
				{
					this.groupBy = new SqlBuilder();
				}
				return this.groupBy;
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x000199E7 File Offset: 0x00017BE7
		public SqlBuilder OrderBy
		{
			get
			{
				if (this.orderBy == null)
				{
					this.orderBy = new SqlBuilder();
				}
				return this.orderBy;
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00019A02 File Offset: 0x00017C02
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00019A0A File Offset: 0x00017C0A
		internal bool IsTopMost { get; set; }

		// Token: 0x060005A0 RID: 1440 RVA: 0x00019A13 File Offset: 0x00017C13
		internal SqlSelectStatement()
		{
			this.select = new SqlSelectClauseBuilder(() => this.IsTopMost);
		}

		// Token: 0x060005A1 RID: 1441 RVA: 0x00019A40 File Offset: 0x00017C40
		public void WriteSql(SqlWriter writer, SqlGenerator sqlGenerator)
		{
			List<string> list = null;
			if (this.outerExtents != null && 0 < this.outerExtents.Count)
			{
				foreach (Symbol symbol in this.outerExtents.Keys)
				{
					JoinSymbol joinSymbol = symbol as JoinSymbol;
					if (joinSymbol != null)
					{
						using (List<Symbol>.Enumerator enumerator2 = joinSymbol.FlattenedExtentList.GetEnumerator())
						{
							while (enumerator2.MoveNext())
							{
								Symbol symbol2 = enumerator2.Current;
								if (list == null)
								{
									list = new List<string>();
								}
								list.Add(symbol2.NewName);
							}
							continue;
						}
					}
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(symbol.NewName);
				}
			}
			List<Symbol> list2 = this.AllJoinExtents ?? this.fromExtents;
			if (list2 != null)
			{
				foreach (Symbol symbol3 in list2)
				{
					if (list != null && list.Contains(symbol3.Name))
					{
						int num = sqlGenerator.AllExtentNames[symbol3.Name];
						string text;
						do
						{
							num++;
							text = symbol3.Name + num.ToString(CultureInfo.InvariantCulture);
						}
						while (sqlGenerator.AllExtentNames.ContainsKey(text));
						sqlGenerator.AllExtentNames[symbol3.Name] = num;
						symbol3.NewName = text;
						sqlGenerator.AllExtentNames[text] = 0;
					}
					if (list == null)
					{
						list = new List<string>();
					}
					list.Add(symbol3.NewName);
				}
			}
			writer.Indent++;
			this.select.WriteSql(writer, sqlGenerator);
			writer.WriteLine();
			writer.Write("FROM ");
			this.From.WriteSql(writer, sqlGenerator);
			if (this.where != null && !this.Where.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("WHERE ");
				this.Where.WriteSql(writer, sqlGenerator);
			}
			if (this.groupBy != null && !this.GroupBy.IsEmpty)
			{
				writer.WriteLine();
				writer.Write("GROUP BY ");
				this.GroupBy.WriteSql(writer, sqlGenerator);
			}
			if (this.orderBy != null && !this.OrderBy.IsEmpty && (this.IsTopMost || this.Select.Top != null || this.Select.Skip != null))
			{
				writer.WriteLine();
				writer.Write("ORDER BY ");
				this.OrderBy.WriteSql(writer, sqlGenerator);
			}
			if (this.Select.Skip != null)
			{
				writer.WriteLine();
				SqlSelectStatement.WriteOffsetFetch(writer, this.Select.Top, this.Select.Skip, sqlGenerator);
			}
			int num2 = writer.Indent - 1;
			writer.Indent = num2;
		}

		// Token: 0x060005A2 RID: 1442 RVA: 0x00019D48 File Offset: 0x00017F48
		private static void WriteOffsetFetch(SqlWriter writer, TopClause top, SkipClause skip, SqlGenerator sqlGenerator)
		{
			skip.WriteSql(writer, sqlGenerator);
			if (top != null)
			{
				writer.Write("FETCH NEXT ");
				top.TopCount.WriteSql(writer, sqlGenerator);
				writer.Write(" ROWS ONLY ");
			}
		}

		// Token: 0x04000113 RID: 275
		private List<Symbol> fromExtents;

		// Token: 0x04000114 RID: 276
		private Dictionary<Symbol, bool> outerExtents;

		// Token: 0x04000115 RID: 277
		private readonly SqlSelectClauseBuilder select;

		// Token: 0x04000116 RID: 278
		private readonly SqlBuilder from = new SqlBuilder();

		// Token: 0x04000117 RID: 279
		private SqlBuilder where;

		// Token: 0x04000118 RID: 280
		private SqlBuilder groupBy;

		// Token: 0x04000119 RID: 281
		private SqlBuilder orderBy;
	}
}

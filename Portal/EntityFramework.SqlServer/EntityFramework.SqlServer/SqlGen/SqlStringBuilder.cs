using System;
using System.Text;

namespace System.Data.Entity.SqlServer.SqlGen
{
	// Token: 0x0200003A RID: 58
	internal class SqlStringBuilder
	{
		// Token: 0x060005A4 RID: 1444 RVA: 0x00019D80 File Offset: 0x00017F80
		public SqlStringBuilder()
		{
			this._sql = new StringBuilder();
		}

		// Token: 0x060005A5 RID: 1445 RVA: 0x00019D93 File Offset: 0x00017F93
		public SqlStringBuilder(int capacity)
		{
			this._sql = new StringBuilder(capacity);
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x00019DA7 File Offset: 0x00017FA7
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x00019DAF File Offset: 0x00017FAF
		public bool UpperCaseKeywords { get; set; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x00019DB8 File Offset: 0x00017FB8
		internal StringBuilder InnerBuilder
		{
			get
			{
				return this._sql;
			}
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x00019DC0 File Offset: 0x00017FC0
		public SqlStringBuilder AppendKeyword(string keyword)
		{
			this._sql.Append(this.UpperCaseKeywords ? keyword.ToUpperInvariant() : keyword.ToLowerInvariant());
			return this;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x00019DE5 File Offset: 0x00017FE5
		public SqlStringBuilder AppendLine()
		{
			this._sql.AppendLine();
			return this;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x00019DF4 File Offset: 0x00017FF4
		public SqlStringBuilder AppendLine(string s)
		{
			this._sql.AppendLine(s);
			return this;
		}

		// Token: 0x060005AC RID: 1452 RVA: 0x00019E04 File Offset: 0x00018004
		public SqlStringBuilder Append(string s)
		{
			this._sql.Append(s);
			return this;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x00019E14 File Offset: 0x00018014
		public int Length
		{
			get
			{
				return this._sql.Length;
			}
		}

		// Token: 0x060005AE RID: 1454 RVA: 0x00019E21 File Offset: 0x00018021
		public override string ToString()
		{
			return this._sql.ToString();
		}

		// Token: 0x0400011B RID: 283
		private readonly StringBuilder _sql;
	}
}

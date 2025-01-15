using System;

namespace Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql
{
	// Token: 0x02001218 RID: 4632
	internal class SqlQueryOptions
	{
		// Token: 0x1700215D RID: 8541
		// (get) Token: 0x06007A2B RID: 31275 RVA: 0x001A6C72 File Offset: 0x001A4E72
		// (set) Token: 0x06007A2C RID: 31276 RVA: 0x001A6C7A File Offset: 0x001A4E7A
		public int? MaxDegreeOfParallelism { get; set; }

		// Token: 0x06007A2D RID: 31277 RVA: 0x001A6C84 File Offset: 0x001A4E84
		public void WriteOptions(ScriptWriter writer)
		{
			if (this.MaxDegreeOfParallelism != null)
			{
				writer.WriteLine();
				writer.Write(SqlLanguageStrings.OptionSqlString);
				writer.Write(SqlLanguageSymbols.OpenParenthesisSqlString);
				writer.WriteSpaceAfter(SqlLanguageStrings.MaxDOPSqlString);
				writer.WriteInt(this.MaxDegreeOfParallelism.Value);
				writer.Write(SqlLanguageSymbols.CloseParenthesisSqlString);
			}
		}
	}
}

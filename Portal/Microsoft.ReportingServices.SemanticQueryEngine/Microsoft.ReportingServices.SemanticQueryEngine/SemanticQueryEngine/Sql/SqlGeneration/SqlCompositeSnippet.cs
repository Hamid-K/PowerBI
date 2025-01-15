using System;

namespace Microsoft.ReportingServices.SemanticQueryEngine.Sql.SqlGeneration
{
	// Token: 0x0200003B RID: 59
	internal sealed class SqlCompositeSnippet : ISqlSnippet
	{
		// Token: 0x06000288 RID: 648 RVA: 0x0000C3A9 File Offset: 0x0000A5A9
		internal SqlCompositeSnippet(params ISqlSnippet[] parts)
		{
			this.m_parts = parts;
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000C3B8 File Offset: 0x0000A5B8
		internal void Prepend(ISqlSnippet sqlSnippet)
		{
			if (this.m_prependedParts == null)
			{
				this.m_prependedParts = new SqlSnippetCollection();
			}
			this.m_prependedParts.Insert(0, sqlSnippet);
		}

		// Token: 0x0600028A RID: 650 RVA: 0x0000C3DA File Offset: 0x0000A5DA
		internal void Append(ISqlSnippet sqlSnippet)
		{
			if (this.m_appendedParts == null)
			{
				this.m_appendedParts = new SqlSnippetCollection();
			}
			this.m_appendedParts.Add(sqlSnippet);
		}

		// Token: 0x0600028B RID: 651 RVA: 0x0000C3FC File Offset: 0x0000A5FC
		void ISqlSnippet.Compile(FormattedStringWriter fsw)
		{
			if (this.m_prependedParts != null)
			{
				for (int i = 0; i < this.m_prependedParts.Count; i++)
				{
					this.m_prependedParts[i].Compile(fsw);
				}
			}
			for (int j = 0; j < this.m_parts.Length; j++)
			{
				this.m_parts[j].Compile(fsw);
			}
			if (this.m_appendedParts != null)
			{
				for (int k = 0; k < this.m_appendedParts.Count; k++)
				{
					this.m_appendedParts[k].Compile(fsw);
				}
			}
		}

		// Token: 0x040000ED RID: 237
		private readonly ISqlSnippet[] m_parts;

		// Token: 0x040000EE RID: 238
		private SqlSnippetCollection m_prependedParts;

		// Token: 0x040000EF RID: 239
		private SqlSnippetCollection m_appendedParts;
	}
}

using System;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x02000198 RID: 408
	internal sealed class KeywordGenerator : TokenGenerator
	{
		// Token: 0x06002195 RID: 8597 RVA: 0x0015E5CB File Offset: 0x0015C7CB
		public KeywordGenerator(TSqlTokenType keywordId)
			: this(keywordId, false)
		{
		}

		// Token: 0x06002196 RID: 8598 RVA: 0x0015E5D5 File Offset: 0x0015C7D5
		public KeywordGenerator(TSqlTokenType keywordId, bool appendSpace)
			: base(appendSpace)
		{
			this._keywordId = keywordId;
		}

		// Token: 0x06002197 RID: 8599 RVA: 0x0015E5E5 File Offset: 0x0015C7E5
		public override void Generate(ScriptWriter writer)
		{
			writer.AddKeyword(this._keywordId);
			base.AppendSpaceIfRequired(writer);
		}

		// Token: 0x040019B9 RID: 6585
		private TSqlTokenType _keywordId;
	}
}

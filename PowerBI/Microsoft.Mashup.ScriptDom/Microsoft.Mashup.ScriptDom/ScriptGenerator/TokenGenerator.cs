using System;

namespace Microsoft.Mashup.ScriptDom.ScriptGenerator
{
	// Token: 0x0200018F RID: 399
	internal abstract class TokenGenerator
	{
		// Token: 0x06002165 RID: 8549 RVA: 0x0015DDD2 File Offset: 0x0015BFD2
		public TokenGenerator(bool appendSpace)
		{
			this._appendSpace = appendSpace;
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x0015DDE1 File Offset: 0x0015BFE1
		protected void AppendSpaceIfRequired(ScriptWriter writer)
		{
			if (this._appendSpace)
			{
				writer.AddToken(ScriptGeneratorSupporter.CreateWhitespaceToken(1));
			}
		}

		// Token: 0x06002167 RID: 8551
		public abstract void Generate(ScriptWriter writer);

		// Token: 0x040019A5 RID: 6565
		private bool _appendSpace;
	}
}

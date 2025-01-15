using System;
using Microsoft.ProgramSynthesis.DslLibrary;
using Microsoft.ProgramSynthesis.Extraction.Web.Semantics;

namespace Microsoft.ProgramSynthesis.Extraction.Web
{
	// Token: 0x02000FAC RID: 4012
	internal class DumbDownTokenScoreForWebLearning : IDisposable
	{
		// Token: 0x06006EE0 RID: 28384 RVA: 0x0016AE5C File Offset: 0x0016905C
		private void PushTokenScore(string tokenName, out int pushVar, int value)
		{
			Token staticTokenByName = Semantics.GetStaticTokenByName(tokenName);
			pushVar = staticTokenByName.Score;
			staticTokenByName.Score = value;
		}

		// Token: 0x06006EE1 RID: 28385 RVA: 0x0016AE7F File Offset: 0x0016907F
		private void PopTokenScore(string tokenName, int value)
		{
			Semantics.GetStaticTokenByName(tokenName).Score = value;
		}

		// Token: 0x06006EE2 RID: 28386 RVA: 0x0016AE8D File Offset: 0x0016908D
		public DumbDownTokenScoreForWebLearning()
		{
			this.PushTokenScore("Line Separator", out this._lineSeparatorScore, 1);
			this.PushTokenScore("WhiteSpace", out this._whiteSpaceScore, 1);
			this.PushTokenScore("Alphanumeric", out this._alphaNumericScore, 1);
		}

		// Token: 0x06006EE3 RID: 28387 RVA: 0x0016AECB File Offset: 0x001690CB
		public void Dispose()
		{
			this.PopTokenScore("Line Separator", this._lineSeparatorScore);
			this.PopTokenScore("WhiteSpace", this._whiteSpaceScore);
			this.PopTokenScore("Alphanumeric", this._alphaNumericScore);
		}

		// Token: 0x04003045 RID: 12357
		private readonly int _lineSeparatorScore;

		// Token: 0x04003046 RID: 12358
		private readonly int _whiteSpaceScore;

		// Token: 0x04003047 RID: 12359
		private readonly int _alphaNumericScore;
	}
}

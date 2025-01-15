using System;
using System.Runtime.CompilerServices;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Rules;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007D7 RID: 2007
	public class FeatureUndefinedException : Exception
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x00077FD7 File Offset: 0x000761D7
		public FeatureInfo Feature { get; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x06002ABF RID: 10943 RVA: 0x00077FDF File Offset: 0x000761DF
		public GrammarRule GrammarRule { get; }

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x00077FE7 File Offset: 0x000761E7
		public Symbol Symbol { get; }

		// Token: 0x06002AC1 RID: 10945 RVA: 0x00077FF0 File Offset: 0x000761F0
		public FeatureUndefinedException(FeatureInfo feature, GrammarRule grammarRule = null, Symbol symbol = null)
		{
			string text = "Undefined feature {0} when computing feature on {1}.";
			object[] array = new object[2];
			array[0] = feature;
			int num = 1;
			string text2;
			if ((text2 = ((grammarRule != null) ? grammarRule.ToString() : null)) == null)
			{
				text2 = ((symbol != null) ? symbol.ToString() : null) ?? "(unknown)";
			}
			array[num] = text2;
			base..ctor(FormattableString.Invariant(FormattableStringFactory.Create(text, array)));
			this.Feature = feature;
			this.GrammarRule = grammarRule;
			this.Symbol = symbol;
		}

		// Token: 0x06002AC2 RID: 10946 RVA: 0x00078059 File Offset: 0x00076259
		public FeatureUndefinedException(FeatureInfo feature, ProgramNode programNode)
			: this(feature, programNode.GrammarRule, programNode.Symbol)
		{
		}
	}
}

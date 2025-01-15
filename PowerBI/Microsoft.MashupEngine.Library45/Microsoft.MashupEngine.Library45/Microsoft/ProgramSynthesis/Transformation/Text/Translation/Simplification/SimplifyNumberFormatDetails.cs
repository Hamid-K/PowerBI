using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Transformation.Text.Build;
using Microsoft.ProgramSynthesis.Transformation.Text.Build.NodeTypes;
using Microsoft.ProgramSynthesis.Transformation.Text.Semantics.Numbers;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001DA1 RID: 7585
	internal class SimplifyNumberFormatDetails : TTextAlternativeSelector
	{
		// Token: 0x0600FE99 RID: 65177 RVA: 0x00366488 File Offset: 0x00364688
		private SimplifyNumberFormatDetails()
		{
			this._pattern = this.rule.ParseNumber(this.hole.SS, this.hole.numberFormatDetails).Node;
		}

		// Token: 0x0600FE9A RID: 65178 RVA: 0x00366509 File Offset: 0x00364709
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(p, this._pattern);
			if (readOnlyDictionary != null)
			{
				NumberFormatDetails value = numberFormatDetails.CreateUnsafe(readOnlyDictionary[(Hole)this.hole.numberFormatDetails.Node]).Value;
				if (!value.SeparatorChar.HasValue)
				{
					yield break;
				}
				NumberFormatDetails numberFormatDetails = value.With(Optional<char>.Nothing);
				yield return this.rule.ParseNumber(this.builders.SS(readOnlyDictionary[(Hole)this.hole.SS.Node]), this.rule.numberFormatDetails(numberFormatDetails)).Node;
			}
			yield break;
		}

		// Token: 0x17002A68 RID: 10856
		// (get) Token: 0x0600FE9B RID: 65179 RVA: 0x00366520 File Offset: 0x00364720
		public static SimplifyNumberFormatDetails Instance { get; } = new SimplifyNumberFormatDetails();

		// Token: 0x04005F69 RID: 24425
		private readonly ProgramNode _pattern;

		// Token: 0x04005F6A RID: 24426
		private readonly GrammarBuilders.Nodes.NodeRules rule = Language.Build.Node.Rule;

		// Token: 0x04005F6B RID: 24427
		private readonly GrammarBuilders.Nodes.NodeHoles hole = Language.Build.Node.Hole;

		// Token: 0x04005F6C RID: 24428
		private readonly GrammarBuilders.Nodes.NodeUnsafe builders = Language.Build.Node.Unsafe;
	}
}

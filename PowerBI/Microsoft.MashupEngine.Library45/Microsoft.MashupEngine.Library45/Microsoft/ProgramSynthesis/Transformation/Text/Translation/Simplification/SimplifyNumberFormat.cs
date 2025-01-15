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
	// Token: 0x02001D9F RID: 7583
	internal class SimplifyNumberFormat : TTextAlternativeSelector
	{
		// Token: 0x0600FE8D RID: 65165 RVA: 0x00366080 File Offset: 0x00364280
		private SimplifyNumberFormat()
		{
			this._pattern = this.rule.FormatNumber(this.rule.RoundNumber(this.hole.inputNumber, this.hole.roundingSpec), this.unnamedConversion.numberFormat_numberFormatLiteral(this.hole.numberFormatLiteral)).Node;
		}

		// Token: 0x0600FE8E RID: 65166 RVA: 0x00366122 File Offset: 0x00364322
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			IReadOnlyDictionary<Hole, ProgramNode> readOnlyDictionary = ProgramSetRewriter.ExtractMappings(p, this._pattern);
			Optional<uint>? optional;
			if (readOnlyDictionary == null)
			{
				numberFormatLiteral numberFormatLiteral;
				if (Language.Build.Node.Is.numberFormatLiteral(p, out numberFormatLiteral))
				{
					NumberFormat value = numberFormatLiteral.Value;
					if (value != null)
					{
						if (value.MinLeadingZeros.HasValue && value.MinLeadingZeros.Value == 1U)
						{
							GrammarBuilders.Nodes.NodeRules nodeRules = this.rule;
							NumberFormat numberFormat = value;
							optional = new Optional<uint>?(Optional<uint>.Nothing);
							yield return nodeRules.numberFormatLiteral(numberFormat.With(null, null, null, optional, null, null)).Node;
							goto IL_02B0;
						}
						if (value.MinLeadingZerosAndWhitespace.HasValue && value.MinLeadingZerosAndWhitespace.Value == 1U)
						{
							GrammarBuilders.Nodes.NodeRules nodeRules2 = this.rule;
							NumberFormat numberFormat2 = value;
							optional = new Optional<uint>?(Optional<uint>.Nothing);
							yield return nodeRules2.numberFormatLiteral(numberFormat2.With(null, null, null, null, optional, null)).Node;
							goto IL_02B0;
						}
						goto IL_02B0;
					}
				}
				yield break;
			}
			NumberFormat value2 = numberFormatLiteral.CreateUnsafe(readOnlyDictionary[(Hole)this.hole.numberFormatLiteral.Node]).Value;
			if (!value2.MaxTrailingZeros.HasValue)
			{
				yield break;
			}
			NumberFormat numberFormat3 = value2;
			optional = new Optional<uint>?(Optional<uint>.Nothing);
			NumberFormat numberFormat4 = numberFormat3.With(null, optional, null, null, null, null);
			yield return this.rule.FormatNumber(this.rule.RoundNumber(inputNumber.CreateUnsafe(readOnlyDictionary[(Hole)this.hole.inputNumber.Node]), roundingSpec.CreateUnsafe(readOnlyDictionary[(Hole)this.hole.roundingSpec.Node])), this.unnamedConversion.numberFormat_numberFormatLiteral(this.rule.numberFormatLiteral(numberFormat4))).Node;
			IL_02B0:
			yield break;
		}

		// Token: 0x17002A65 RID: 10853
		// (get) Token: 0x0600FE8F RID: 65167 RVA: 0x00366139 File Offset: 0x00364339
		public static SimplifyNumberFormat Instance { get; } = new SimplifyNumberFormat();

		// Token: 0x04005F5E RID: 24414
		private readonly ProgramNode _pattern;

		// Token: 0x04005F5F RID: 24415
		private readonly GrammarBuilders.Nodes.NodeRules rule = Language.Build.Node.Rule;

		// Token: 0x04005F60 RID: 24416
		private readonly GrammarBuilders.Nodes.NodeHoles hole = Language.Build.Node.Hole;

		// Token: 0x04005F61 RID: 24417
		private readonly GrammarBuilders.Nodes.NodeUnnamedConversionRules unnamedConversion = Language.Build.Node.UnnamedConversion;
	}
}

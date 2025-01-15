using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Utils;
using Microsoft.ProgramSynthesis.VersionSpace;

namespace Microsoft.ProgramSynthesis.Transformation.Text.Translation.Simplification
{
	// Token: 0x02001D92 RID: 7570
	internal class ErreRewrite : TTextAlternativeSelector
	{
		// Token: 0x0600FE57 RID: 65111 RVA: 0x00364F88 File Offset: 0x00363188
		private ErreRewrite()
		{
			this._localRule = Rewriter.GenerateRegexPositionPairRewriteRule().Reverse();
		}

		// Token: 0x17002A60 RID: 10848
		// (get) Token: 0x0600FE58 RID: 65112 RVA: 0x00364FAE File Offset: 0x003631AE
		public static ErreRewrite Instance { get; } = new ErreRewrite();

		// Token: 0x0600FE59 RID: 65113 RVA: 0x00364FB5 File Offset: 0x003631B5
		protected override IEnumerable<ProgramNode> GetAlternatives(ProgramNode p)
		{
			ProgramNode programNode = ProgramSetRewriter.TopRewrite(p, this._localRule);
			if (programNode == null)
			{
				return null;
			}
			return programNode.Yield<ProgramNode>();
		}

		// Token: 0x04005F31 RID: 24369
		private readonly RewriteRule _localRule;
	}
}

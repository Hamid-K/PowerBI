using System;
using Microsoft.ProgramSynthesis.AST;
using Microsoft.ProgramSynthesis.Extraction.Web.Build.NodeTypes;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS
{
	// Token: 0x0200119D RID: 4509
	public static class CSSTranslator
	{
		// Token: 0x06008657 RID: 34391 RVA: 0x001C37B0 File Offset: 0x001C19B0
		public static string TranslateToCssSelector(ProgramNode programNode)
		{
			resultSequence resultSequence;
			if (!Language.Build.Node.Is.resultSequence(programNode, out resultSequence))
			{
				return string.Empty;
			}
			if (resultSequence.Is_Union(Language.Build))
			{
				string text = CSSTranslator.TranslateToCssSelector(programNode.Children[0]);
				string text2 = CSSTranslator.TranslateToCssSelector(programNode.Children[1]);
				return text + ", " + text2;
			}
			return CSSTranslator.TranslateNonUnionProgramToCssSelector(programNode);
		}

		// Token: 0x06008658 RID: 34392 RVA: 0x001C3818 File Offset: 0x001C1A18
		private static string TranslateNonUnionProgramToCssSelector(ProgramNode programNode)
		{
			string explictlySpecifiedCssSelector = CSSTranslator.GetExplictlySpecifiedCssSelector(programNode);
			if (explictlySpecifiedCssSelector != null)
			{
				return explictlySpecifiedCssSelector;
			}
			CssSelector cssSelector = new CssSelector();
			CssTranslationVisitor cssTranslationVisitor = new CssTranslationVisitor();
			programNode.AcceptVisitor<CssSelector, CssSelector>(cssTranslationVisitor, cssSelector);
			return cssSelector.ToString();
		}

		// Token: 0x06008659 RID: 34393 RVA: 0x001C384C File Offset: 0x001C1A4C
		private static string GetExplictlySpecifiedCssSelector(ProgramNode p)
		{
			cssSelector? cssSelector = Language.Build.Node.As.cssSelector(p);
			if (cssSelector != null)
			{
				return cssSelector.Value.Value;
			}
			ProgramNode[] children = p.Children;
			for (int i = 0; i < children.Length; i++)
			{
				string explictlySpecifiedCssSelector = CSSTranslator.GetExplictlySpecifiedCssSelector(children[i]);
				if (explictlySpecifiedCssSelector != null)
				{
					return explictlySpecifiedCssSelector;
				}
			}
			return null;
		}
	}
}

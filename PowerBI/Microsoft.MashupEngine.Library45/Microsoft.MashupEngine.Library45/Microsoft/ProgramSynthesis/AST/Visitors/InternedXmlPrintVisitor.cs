using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST.Visitors
{
	// Token: 0x020008EF RID: 2287
	public class InternedXmlPrintVisitor : XmlPrintVisitor
	{
		// Token: 0x06003172 RID: 12658 RVA: 0x00091D8C File Offset: 0x0008FF8C
		public InternedXmlPrintVisitor(Dictionary<object, int> identityCache)
			: base(ASTSerializationSettings.Xml)
		{
			this._identityCache = identityCache;
		}

		// Token: 0x06003173 RID: 12659 RVA: 0x00091DA0 File Offset: 0x0008FFA0
		private XElement VisitInterned<T>(Func<T, XElement> callback, T node)
		{
			int count;
			if (this._identityCache.TryGetValue(node, out count))
			{
				return new XElement("Reference", count);
			}
			XElement xelement = callback(node);
			count = this._identityCache.Count;
			this._identityCache[node] = count;
			return xelement.WithAttribute("ObjectID", count);
		}

		// Token: 0x06003174 RID: 12660 RVA: 0x00091E0D File Offset: 0x0009000D
		public override XElement VisitNonterminal(NonterminalNode node)
		{
			return this.VisitInterned<NonterminalNode>((NonterminalNode n) => base.VisitNonterminal(n), node);
		}

		// Token: 0x06003175 RID: 12661 RVA: 0x00091E22 File Offset: 0x00090022
		public override XElement VisitLet(LetNode node)
		{
			return this.VisitInterned<LetNode>((LetNode n) => base.VisitLet(n), node);
		}

		// Token: 0x06003176 RID: 12662 RVA: 0x00091C55 File Offset: 0x0008FE55
		public override XElement VisitLambda(LambdaNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x06003177 RID: 12663 RVA: 0x00091E37 File Offset: 0x00090037
		public override XElement VisitLiteral(LiteralNode node)
		{
			return this.VisitInterned<LiteralNode>((LiteralNode n) => base.VisitLiteral(n), node);
		}

		// Token: 0x06003178 RID: 12664 RVA: 0x00091E4C File Offset: 0x0009004C
		public override XElement VisitVariable(VariableNode node)
		{
			return this.VisitInterned<VariableNode>((VariableNode n) => base.VisitVariable(n), node);
		}

		// Token: 0x06003179 RID: 12665 RVA: 0x00091E61 File Offset: 0x00090061
		public override XElement VisitHole(Hole node)
		{
			return this.VisitInterned<Hole>((Hole n) => base.VisitHole(n), node);
		}

		// Token: 0x0400189D RID: 6301
		private readonly Dictionary<object, int> _identityCache;
	}
}

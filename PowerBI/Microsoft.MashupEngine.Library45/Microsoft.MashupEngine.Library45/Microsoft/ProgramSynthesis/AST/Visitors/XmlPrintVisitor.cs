using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST.Visitors
{
	// Token: 0x020008EE RID: 2286
	public class XmlPrintVisitor : ProgramNodeVisitor<XElement>
	{
		// Token: 0x0600316A RID: 12650 RVA: 0x00091B4F File Offset: 0x0008FD4F
		public XmlPrintVisitor(ASTSerializationSettings settings)
		{
			this._settings = settings;
			if (settings.HasOmitLiterals)
			{
				this._uniqueLiterals = new List<object>();
			}
		}

		// Token: 0x0600316B RID: 12651 RVA: 0x00091B74 File Offset: 0x0008FD74
		public override XElement VisitNonterminal(NonterminalNode node)
		{
			ProgramNode programNode = node;
			ConceptRule conceptRule = node.Rule as ConceptRule;
			if (conceptRule != null)
			{
				programNode = conceptRule.BuildDslASTFromConceptAST(node);
			}
			XElement xelement = this.BuildXElementTemplate(node);
			foreach (ProgramNode programNode2 in programNode.Children)
			{
				xelement.Add(programNode2.AcceptVisitor<XElement>(this));
			}
			return xelement.WithAttribute("rule", node.Rule);
		}

		// Token: 0x0600316C RID: 12652 RVA: 0x00091BE0 File Offset: 0x0008FDE0
		public override XElement VisitLet(LetNode node)
		{
			XElement xelement = this.BuildXElementTemplate(node);
			LetRule letRule = node.LetRule;
			XElement xelement2 = node.ValueNode.AcceptVisitor<XElement>(this);
			xelement.Add(new XElement("Variable", xelement2).WithAttribute("symbol", letRule.Variable));
			xelement.Add(node.BodyNode.AcceptVisitor<XElement>(this));
			return xelement.WithAttribute("id", node.Rule.Id);
		}

		// Token: 0x0600316D RID: 12653 RVA: 0x00091C55 File Offset: 0x0008FE55
		public override XElement VisitLambda(LambdaNode node)
		{
			return this.VisitNonterminal(node);
		}

		// Token: 0x0600316E RID: 12654 RVA: 0x00091C60 File Offset: 0x0008FE60
		public override XElement VisitLiteral(LiteralNode node)
		{
			XElement xelement = this.BuildXElementTemplate(node);
			if (this._settings.HasOmitLiterals)
			{
				int num = 0;
				while (num < this._uniqueLiterals.Count && !ValueEquality.Comparer.Equals(node.Value, this._uniqueLiterals[num]))
				{
					num++;
				}
				if (num == this._uniqueLiterals.Count)
				{
					this._uniqueLiterals.Add(node.Value);
				}
				xelement = xelement.WithAttribute("distinctValueId", num);
			}
			else
			{
				xelement.Add(XmlUtils.ObjectToXml(node.Value));
			}
			return xelement;
		}

		// Token: 0x0600316F RID: 12655 RVA: 0x00091CFB File Offset: 0x0008FEFB
		public override XElement VisitVariable(VariableNode node)
		{
			return this.BuildXElementTemplate(node);
		}

		// Token: 0x06003170 RID: 12656 RVA: 0x00091D04 File Offset: 0x0008FF04
		public override XElement VisitHole(Hole node)
		{
			XElement xelement = this.BuildXElementTemplate(node);
			if (node.HoleId != null)
			{
				xelement = xelement.WithAttribute("holeId", node.HoleId);
			}
			return xelement;
		}

		// Token: 0x06003171 RID: 12657 RVA: 0x00091D34 File Offset: 0x0008FF34
		private XElement BuildXElementTemplate(ProgramNode node)
		{
			XElement xelement = new XElement(node.GetType().Name).WithAttribute("symbol", node.Symbol);
			if (this._settings.HasIds)
			{
				xelement = xelement.WithAttribute("nodeId", node.Id);
			}
			return xelement;
		}

		// Token: 0x0400189B RID: 6299
		private ASTSerializationSettings _settings;

		// Token: 0x0400189C RID: 6300
		private readonly List<object> _uniqueLiterals;
	}
}

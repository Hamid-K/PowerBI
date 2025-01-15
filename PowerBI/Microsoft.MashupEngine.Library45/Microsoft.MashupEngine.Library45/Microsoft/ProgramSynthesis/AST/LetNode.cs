using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST.Extensions;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008CF RID: 2255
	public class LetNode : NonterminalNode
	{
		// Token: 0x1700088A RID: 2186
		// (get) Token: 0x06003079 RID: 12409 RVA: 0x0008EA6E File Offset: 0x0008CC6E
		public ProgramNode ValueNode
		{
			get
			{
				return this.Children[0];
			}
		}

		// Token: 0x1700088B RID: 2187
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x0008EB1A File Offset: 0x0008CD1A
		public ProgramNode BodyNode
		{
			get
			{
				return this.Children[1];
			}
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x0600307B RID: 12411 RVA: 0x0008EB24 File Offset: 0x0008CD24
		public LetRule LetRule
		{
			get
			{
				return (LetRule)base.Rule;
			}
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x0008EB31 File Offset: 0x0008CD31
		public LetNode(LetRule rule, ProgramNode valueNode, ProgramNode bodyNode)
			: base(rule, new ProgramNode[] { valueNode, bodyNode })
		{
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x0008EB48 File Offset: 0x0008CD48
		protected override object Evaluate(State state)
		{
			return this.BodyNode.Invoke(state.Bind(this.LetRule.Variable, this.Children[0].Invoke(state)));
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x0008EB74 File Offset: 0x0008CD74
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitLet(this);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0008EB7D File Offset: 0x0008CD7D
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitLet(this, args);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x0008EB87 File Offset: 0x0008CD87
		public override ProgramNode Clone()
		{
			return new LetNode(base.Rule as LetRule, this.ValueNode.Clone(), this.BodyNode.Clone());
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x06003081 RID: 12417 RVA: 0x0008EBAF File Offset: 0x0008CDAF
		private static string InternalLetVariable
		{
			get
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("_LetV{0}", new object[] { LetNode._letCount++ }));
			}
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x0008EBDC File Offset: 0x0008CDDC
		internal new static LetNode ParseXML(XElement node, Type expectedType, ParseContext context)
		{
			if (node.Name != typeof(LetNode).Name)
			{
				return null;
			}
			XAttribute xattribute = node.Attribute("symbol");
			string text = ((xattribute != null) ? xattribute.Value : null);
			if (text == null)
			{
				return null;
			}
			Dictionary<string, Type> dictionary = context.Grammar.Symbols.Values.Where((Symbol s) => s.IsVariable).ToDictionary((Symbol s) => s.Name, (Symbol s) => s.ResolvedType);
			XElement xelement = (from e in node.Elements()
				where e.Name == "Variable"
				select e).OnlyOrDefault<XElement>();
			if (xelement == null)
			{
				throw new NotImplementedException("Let nodes must have exactly one binding.");
			}
			XAttribute xattribute2 = xelement.Attribute("symbol");
			string text2 = ((xattribute2 != null) ? xattribute2.Value : null);
			if (text2 == null)
			{
				return null;
			}
			if (xelement.Elements().Count<XElement>() != 1)
			{
				return null;
			}
			XElement xelement2 = xelement.Elements().Single<XElement>();
			Type type;
			dictionary.TryGetValue(text2, out type);
			ProgramNode programNode = ProgramNode.ParseXML(xelement2, type, context);
			if (programNode == null)
			{
				return null;
			}
			Symbol symbol = LetNode.CreateSymbolIfNotExists(text2, programNode.GetType(), context.Grammar);
			XElement xelement3 = node.Elements().SingleOrDefault((XElement e) => e.Name != "Variable");
			string text3;
			if (xelement3 == null)
			{
				text3 = null;
			}
			else
			{
				XAttribute xattribute3 = xelement3.Attribute("symbol");
				text3 = ((xattribute3 != null) ? xattribute3.Value : null);
			}
			string text4 = text3;
			ProgramNode programNode2;
			if (text4 == null)
			{
				if (!(((xelement3 != null) ? xelement3.Name.LocalName : null) == "Reference"))
				{
					return null;
				}
				programNode2 = ProgramNode.TryResolveReference(xelement3, context.Grammar, context.IdentityCache) as LetNode;
				if (programNode2 == null)
				{
					throw new ArgumentException("Invalid XML encountered during FromInternedXML().");
				}
				text4 = programNode2.Symbol.Name;
			}
			else
			{
				ImmutableStack<ScopeElement> immutableStack = context.Scope.Push(ScopeElement.Define(symbol));
				programNode2 = ProgramNode.ParseXML(xelement3, expectedType, context.WithScope(immutableStack));
			}
			if (programNode2 == null)
			{
				return null;
			}
			XAttribute xattribute4 = node.Attribute("id");
			string text5 = ((xattribute4 != null) ? xattribute4.Value : null);
			LetRule letRule = ((text5 == null) ? null : (context.Grammar.Rule(text5) as LetRule));
			foreach (LetRule letRule2 in ((letRule != null) ? Seq.Of<LetRule>(new LetRule[] { letRule }) : context.Grammar.Rules.OfType<LetRule>()))
			{
				if ((!(text != letRule2.Head.Name) || LetRule.LikelyIsInternalLetName(text)) && !(programNode2.Symbol.GrammarType != letRule2.ReturnGrammarType))
				{
					Symbol symbol2 = context.Grammar.Symbol(text4);
					if (symbol2 == null && !LetRule.LikelyIsInternalLetName(text4))
					{
						break;
					}
					ProgramNode programNode3 = programNode2;
					if (symbol2 != letRule2.LetBody)
					{
						programNode3 = programNode2.AddConversionRules(letRule2.LetBody);
						if (programNode3 == null || (programNode3.Symbol != letRule2.LetBody && !LetRule.LikelyIsInternalLetName(text4)))
						{
							continue;
						}
					}
					if (symbol.Equals(letRule2.Variable))
					{
						if (programNode.Symbol.Equals(letRule2.Value))
						{
							return new LetNode(letRule2, programNode, programNode3);
						}
						IImmutableList<ConversionRule> immutableList = programNode.Symbol.ConversionRulesTo(letRule2.Value);
						if (immutableList != null)
						{
							return new LetNode(letRule2, programNode.AddConversionRules(immutableList), programNode3);
						}
					}
				}
			}
			Symbol symbol3 = LetNode.CreateSymbolIfNotExists(text, expectedType, context.Grammar);
			Symbol symbol4 = symbol;
			GrammarRule grammarRule = LetNode.CreateConversionRule(programNode.Symbol);
			GrammarRule grammarRule2 = LetNode.CreateConversionRule(programNode2.Symbol);
			LetRule letRule3 = LetRule.Create(symbol3, symbol4, grammarRule, grammarRule2);
			context.Grammar.AddRule(letRule3);
			return new LetNode(letRule3, programNode, programNode2);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x0008F05C File Offset: 0x0008D25C
		private static GrammarRule CreateConversionRule(Symbol node)
		{
			return new ConversionRule(new Symbol(node.Grammar, node.GrammarType, LetNode.InternalLetVariable, false), node, null);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x0008F07C File Offset: 0x0008D27C
		private static Symbol CreateSymbolIfNotExists(string name, Type type, Grammar grammar)
		{
			Symbol symbol;
			if (!grammar.Symbols.TryGetValue(name, out symbol))
			{
				symbol = new Symbol(grammar, new ResolvedType(type), name, false);
			}
			return symbol;
		}

		// Token: 0x0400185E RID: 6238
		private static int _letCount;
	}
}

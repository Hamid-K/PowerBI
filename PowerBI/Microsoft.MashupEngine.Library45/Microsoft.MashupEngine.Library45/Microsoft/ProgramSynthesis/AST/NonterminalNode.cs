using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.ProgramSynthesis.AST.Extensions;
using Microsoft.ProgramSynthesis.Rules;
using Microsoft.ProgramSynthesis.Rules.Concepts;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008D3 RID: 2259
	public class NonterminalNode : ProgramNode
	{
		// Token: 0x06003096 RID: 12438 RVA: 0x0008F2FC File Offset: 0x0008D4FC
		public NonterminalNode(NonterminalRule rule, params ProgramNode[] children)
		{
			this.Rule = rule;
			this._children = children;
		}

		// Token: 0x06003097 RID: 12439 RVA: 0x0008F312 File Offset: 0x0008D512
		internal NonterminalNode(int id, NonterminalRule rule, params ProgramNode[] children)
			: base(id)
		{
			this.Rule = rule;
			this._children = children;
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0008F329 File Offset: 0x0008D529
		public NonterminalRule Rule { get; }

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06003099 RID: 12441 RVA: 0x0008F331 File Offset: 0x0008D531
		public override ProgramNode[] Children
		{
			get
			{
				return this._children;
			}
		}

		// Token: 0x0600309A RID: 12442 RVA: 0x0008F33C File Offset: 0x0008D53C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override object Evaluate(State state)
		{
			ProgramNode[] children = this.Children;
			ConversionRule conversionRule = this.Rule as ConversionRule;
			if (conversionRule != null)
			{
				return children[0].Invoke(conversionRule.ApplySubstitutions(state, false));
			}
			int num = children.Length;
			object[] array = new object[num];
			for (int i = 0; i < num; i++)
			{
				object obj = children[i].Invoke(state);
				array[i] = obj;
			}
			return this.Rule.Evaluate(array);
		}

		// Token: 0x0600309B RID: 12443 RVA: 0x0008F3A9 File Offset: 0x0008D5A9
		public override ProgramNode Clone()
		{
			return new NonterminalNode(this.Rule, this.Children.Select((ProgramNode n) => n.Clone()).ToArray<ProgramNode>());
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x0008F3E5 File Offset: 0x0008D5E5
		public sealed override Symbol Symbol
		{
			get
			{
				return this.Rule.Head;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x0600309D RID: 12445 RVA: 0x0008F3F2 File Offset: 0x0008D5F2
		public override GrammarRule GrammarRule
		{
			get
			{
				return this.Rule;
			}
		}

		// Token: 0x0600309E RID: 12446 RVA: 0x0008F3FA File Offset: 0x0008D5FA
		public override T AcceptVisitor<T>(ProgramNodeVisitor<T> visitor)
		{
			return visitor.VisitNonterminal(this);
		}

		// Token: 0x0600309F RID: 12447 RVA: 0x0008F403 File Offset: 0x0008D603
		public override TResult AcceptVisitor<TResult, TArgs>(ProgramNodeVisitor<TResult, TArgs> visitor, TArgs args)
		{
			return visitor.VisitNonterminal(this, args);
		}

		// Token: 0x060030A0 RID: 12448 RVA: 0x0008F410 File Offset: 0x0008D610
		public override bool Equals(ProgramNode other)
		{
			if (other == null)
			{
				return false;
			}
			if (this == other)
			{
				return true;
			}
			if (other.GetType() != base.GetType())
			{
				return false;
			}
			NonterminalNode nonterminalNode = other as NonterminalNode;
			return this._children.SequenceEqual(nonterminalNode._children) && object.Equals(this.Rule, nonterminalNode.Rule);
		}

		// Token: 0x060030A1 RID: 12449 RVA: 0x0008F46C File Offset: 0x0008D66C
		public override int GetHashCode()
		{
			if (this._hashCode != null)
			{
				return this._hashCode.Value;
			}
			int num = this.Symbol.GetHashCode();
			num = this._children.Aggregate(num, (int acc, ProgramNode child) => (acc * 322261) ^ ((child != null) ? child.GetHashCode() : 0));
			int num2 = num * 322261;
			NonterminalRule rule = this.Rule;
			num = num2 ^ ((rule != null) ? rule.GetHashCode() : 0);
			this._hashCode = new int?(num);
			return num;
		}

		// Token: 0x060030A2 RID: 12450 RVA: 0x0008F4F4 File Offset: 0x0008D6F4
		internal new static ProgramNode ParseXML(XElement node, Type expectedType, ParseContext context)
		{
			NonterminalNode.<>c__DisplayClass19_0 CS$<>8__locals1 = new NonterminalNode.<>c__DisplayClass19_0();
			if (node.Name != typeof(NonterminalNode).Name)
			{
				return null;
			}
			NonterminalNode.<>c__DisplayClass19_0 CS$<>8__locals2 = CS$<>8__locals1;
			XAttribute xattribute = node.Attribute("rule");
			CS$<>8__locals2.ruleName = ((xattribute != null) ? xattribute.Value : null);
			if (CS$<>8__locals1.ruleName == null)
			{
				return null;
			}
			XElement[] array = node.Elements().ToArray<XElement>();
			OperatorRule operatorRule = context.Grammar.Rules.OfType<OperatorRule>().SingleOrDefault((OperatorRule r) => r.Id == CS$<>8__locals1.ruleName) ?? context.Grammar.Rules.OfType<ConversionRule>().FirstOrDefault((ConversionRule r) => r.Name == CS$<>8__locals1.ruleName);
			if (operatorRule != null && operatorRule.Body.Count == array.Length)
			{
				ConceptRule conceptRule = operatorRule as ConceptRule;
				List<ProgramNode> list = new List<ProgramNode>(array.Length);
				for (int i = 0; i < array.Length; i++)
				{
					ImmutableStack<ScopeElement> immutableStack = context.Scope;
					if (conceptRule != null && conceptRule.Body[i].LambdaRule != null)
					{
						immutableStack = immutableStack.Push(ScopeElement.Define(conceptRule.Body[i].LambdaRule.Variable));
					}
					else
					{
						ConversionRule conversionRule = operatorRule as ConversionRule;
						if (conversionRule != null)
						{
							foreach (KeyValuePair<Symbol, Symbol> keyValuePair in conversionRule.Substitutions)
							{
								immutableStack = immutableStack.Push(ScopeElement.Define(keyValuePair.Key));
								if (conversionRule == context.Settings.RuleForBackCompatParsing)
								{
									immutableStack = immutableStack.Push(ScopeElement.Substitute(keyValuePair.Value, keyValuePair.Key));
								}
							}
						}
					}
					ProgramNode programNode = ProgramNode.ParseXML(array[i], operatorRule.Body[i].ResolvedType, context.WithScope(immutableStack).WithGrammar(operatorRule.Body[i].Grammar));
					if (programNode == null)
					{
						return null;
					}
					programNode = programNode.AddConversionRules(operatorRule.Body[i]) ?? programNode;
					if (programNode.Symbol == operatorRule.Body[i] && programNode.GrammarRule is ConversionRule && programNode.GrammarRule.Body[0] != operatorRule.Body[i] && programNode.Children[0].Symbol == operatorRule.Body[i])
					{
						programNode = programNode.Children[0];
					}
					list.Add(programNode);
				}
				ProgramNode programNode2 = ((conceptRule != null) ? conceptRule.BuildConceptASTFromDslAST(list.ToArray()) : operatorRule.BuildASTNode(list.ToArray()));
				XAttribute xattribute2 = node.Attribute("symbol");
				if (xattribute2 != null)
				{
					Symbol symbol = context.Grammar.Symbol(xattribute2.Value);
					if (symbol != null && operatorRule.Head != symbol)
					{
						programNode2 = programNode2.AddConversionRules(symbol) ?? programNode2;
					}
				}
				return programNode2;
			}
			if (array.Length != 1 || !CS$<>8__locals1.ruleName.StartsWith("~convert", StringComparison.Ordinal))
			{
				return null;
			}
			Grammar grammar = context.Grammar;
			XAttribute xattribute3 = node.Attribute("symbol");
			Symbol symbol2 = grammar.Symbol((xattribute3 != null) ? xattribute3.Value : null);
			Grammar grammar2 = context.Grammar;
			XAttribute xattribute4 = array[0].Attribute("symbol");
			Symbol symbol3 = grammar2.Symbol((xattribute4 != null) ? xattribute4.Value : null);
			if (symbol2 == null || symbol3 == null)
			{
				return null;
			}
			IImmutableList<ConversionRule> immutableList = symbol3.ConversionRulesTo(symbol2);
			if (immutableList == null)
			{
				return null;
			}
			return NonterminalNode.ParseXML(array[0], symbol3.ResolvedType, context).AddConversionRules(immutableList);
		}

		// Token: 0x04001866 RID: 6246
		private readonly ProgramNode[] _children;

		// Token: 0x04001868 RID: 6248
		private int? _hashCode;
	}
}

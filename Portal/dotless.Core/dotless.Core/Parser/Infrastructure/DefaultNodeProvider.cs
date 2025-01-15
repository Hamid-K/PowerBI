using System;
using System.Collections.Generic;
using dotless.Core.Importers;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000050 RID: 80
	public class DefaultNodeProvider : INodeProvider
	{
		// Token: 0x06000338 RID: 824 RVA: 0x0000EB6B File Offset: 0x0000CD6B
		public Element Element(Combinator combinator, Node value, NodeLocation location)
		{
			return new Element(combinator, value)
			{
				Location = location
			};
		}

		// Token: 0x06000339 RID: 825 RVA: 0x0000EB7B File Offset: 0x0000CD7B
		public Combinator Combinator(string value, NodeLocation location)
		{
			return new Combinator(value)
			{
				Location = location
			};
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0000EB8A File Offset: 0x0000CD8A
		public Selector Selector(NodeList<Element> elements, NodeLocation location)
		{
			return new Selector(elements)
			{
				Location = location
			};
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000EB99 File Offset: 0x0000CD99
		public Rule Rule(string name, Node value, NodeLocation location)
		{
			return new Rule(name, value)
			{
				Location = location
			};
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000EBA9 File Offset: 0x0000CDA9
		public Rule Rule(string name, Node value, bool variadic, NodeLocation location)
		{
			return new Rule(name, value, variadic)
			{
				Location = location
			};
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000EBBB File Offset: 0x0000CDBB
		public Ruleset Ruleset(NodeList<Selector> selectors, NodeList rules, NodeLocation location)
		{
			return new Ruleset(selectors, rules)
			{
				Location = location
			};
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000EBCB File Offset: 0x0000CDCB
		public CssFunction CssFunction(string name, Node value, NodeLocation location)
		{
			return new CssFunction
			{
				Name = name,
				Value = value,
				Location = location
			};
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000EBE7 File Offset: 0x0000CDE7
		public Alpha Alpha(Node value, NodeLocation location)
		{
			return new Alpha(value)
			{
				Location = location
			};
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000EBF6 File Offset: 0x0000CDF6
		public Call Call(string name, NodeList<Node> arguments, NodeLocation location)
		{
			return new Call(name, arguments)
			{
				Location = location
			};
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000EC06 File Offset: 0x0000CE06
		public Color Color(string rgb, NodeLocation location)
		{
			Color color = dotless.Core.Parser.Tree.Color.FromHex(rgb);
			color.Location = location;
			return color;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0000EC15 File Offset: 0x0000CE15
		public Keyword Keyword(string value, NodeLocation location)
		{
			return new Keyword(value)
			{
				Location = location
			};
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0000EC24 File Offset: 0x0000CE24
		public Number Number(string value, string unit, NodeLocation location)
		{
			return new Number(value, unit)
			{
				Location = location
			};
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public Shorthand Shorthand(Node first, Node second, NodeLocation location)
		{
			return new Shorthand(first, second)
			{
				Location = location
			};
		}

		// Token: 0x06000345 RID: 837 RVA: 0x0000EC44 File Offset: 0x0000CE44
		public Variable Variable(string name, NodeLocation location)
		{
			return new Variable(name)
			{
				Location = location
			};
		}

		// Token: 0x06000346 RID: 838 RVA: 0x0000EC53 File Offset: 0x0000CE53
		public Url Url(Node value, IImporter importer, NodeLocation location)
		{
			return new Url(value, importer)
			{
				Location = location
			};
		}

		// Token: 0x06000347 RID: 839 RVA: 0x0000EC63 File Offset: 0x0000CE63
		public Script Script(string script, NodeLocation location)
		{
			return new Script(script)
			{
				Location = location
			};
		}

		// Token: 0x06000348 RID: 840 RVA: 0x0000EC72 File Offset: 0x0000CE72
		public GuardedRuleset GuardedRuleset(NodeList<Selector> selectors, NodeList rules, Condition condition, NodeLocation location)
		{
			return new GuardedRuleset(selectors, rules, condition)
			{
				Location = location
			};
		}

		// Token: 0x06000349 RID: 841 RVA: 0x0000EC84 File Offset: 0x0000CE84
		public MixinCall MixinCall(NodeList<Element> elements, List<NamedArgument> arguments, bool important, NodeLocation location)
		{
			return new MixinCall(elements, arguments, important)
			{
				Location = location
			};
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000EC96 File Offset: 0x0000CE96
		public MixinDefinition MixinDefinition(string name, NodeList<Rule> parameters, NodeList rules, Condition condition, bool variadic, NodeLocation location)
		{
			return new MixinDefinition(name, parameters, rules, condition, variadic)
			{
				Location = location
			};
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000ECAC File Offset: 0x0000CEAC
		public Import Import(Url path, Value features, ImportOptions option, NodeLocation location)
		{
			return new Import(path, features, option)
			{
				Location = location
			};
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000ECBE File Offset: 0x0000CEBE
		public Import Import(Quoted path, Value features, ImportOptions option, NodeLocation location)
		{
			return new Import(path, features, option)
			{
				Location = location
			};
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public Directive Directive(string name, string identifier, NodeList rules, NodeLocation location)
		{
			return new Directive(name, identifier, rules)
			{
				Location = location
			};
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000ECE2 File Offset: 0x0000CEE2
		public Media Media(NodeList rules, Value features, NodeLocation location)
		{
			return new Media(features, rules)
			{
				Location = location
			};
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000ECF2 File Offset: 0x0000CEF2
		public KeyFrame KeyFrame(NodeList identifier, NodeList rules, NodeLocation location)
		{
			return new KeyFrame(identifier, rules)
			{
				Location = location
			};
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000ED02 File Offset: 0x0000CF02
		public Directive Directive(string name, Node value, NodeLocation location)
		{
			return new Directive(name, value)
			{
				Location = location
			};
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000ED12 File Offset: 0x0000CF12
		public Expression Expression(NodeList expression, NodeLocation location)
		{
			return new Expression(expression)
			{
				Location = location
			};
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000ED21 File Offset: 0x0000CF21
		public Value Value(IEnumerable<Node> values, string important, NodeLocation location)
		{
			return new Value(values, important)
			{
				Location = location
			};
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000ED31 File Offset: 0x0000CF31
		public Operation Operation(string operation, Node left, Node right, NodeLocation location)
		{
			return new Operation(operation, left, right)
			{
				Location = location
			};
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000ED43 File Offset: 0x0000CF43
		public Assignment Assignment(string key, Node value, NodeLocation location)
		{
			return new Assignment(key, value)
			{
				Location = location
			};
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000ED53 File Offset: 0x0000CF53
		public Comment Comment(string value, NodeLocation location)
		{
			return new Comment(value)
			{
				Location = location
			};
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000ED62 File Offset: 0x0000CF62
		public TextNode TextNode(string contents, NodeLocation location)
		{
			return new TextNode(contents)
			{
				Location = location
			};
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000ED71 File Offset: 0x0000CF71
		public Quoted Quoted(string value, string contents, bool escaped, NodeLocation location)
		{
			return new Quoted(value, contents, escaped)
			{
				Location = location
			};
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000ED83 File Offset: 0x0000CF83
		public Extend Extend(List<Selector> exact, List<Selector> partial, NodeLocation location)
		{
			return new Extend(exact, partial)
			{
				Location = location
			};
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000ED93 File Offset: 0x0000CF93
		public Node Attribute(Node key, Node op, Node val, NodeLocation location)
		{
			return new dotless.Core.Parser.Tree.Attribute(key, op, val)
			{
				Location = location
			};
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000EDA5 File Offset: 0x0000CFA5
		public Paren Paren(Node value, NodeLocation location)
		{
			return new Paren(value)
			{
				Location = location
			};
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000EDB4 File Offset: 0x0000CFB4
		public Condition Condition(Node left, string operation, Node right, bool negate, NodeLocation location)
		{
			return new Condition(left, operation, right, negate)
			{
				Location = location
			};
		}
	}
}

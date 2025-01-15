using System;
using System.Collections.Generic;
using dotless.Core.Importers;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Infrastructure
{
	// Token: 0x02000056 RID: 86
	public interface INodeProvider
	{
		// Token: 0x060003B6 RID: 950
		Element Element(Combinator combinator, Node Value, NodeLocation location);

		// Token: 0x060003B7 RID: 951
		Combinator Combinator(string value, NodeLocation location);

		// Token: 0x060003B8 RID: 952
		Selector Selector(NodeList<Element> elements, NodeLocation location);

		// Token: 0x060003B9 RID: 953
		Rule Rule(string name, Node value, NodeLocation location);

		// Token: 0x060003BA RID: 954
		Rule Rule(string name, Node value, bool variadic, NodeLocation location);

		// Token: 0x060003BB RID: 955
		Ruleset Ruleset(NodeList<Selector> selectors, NodeList rules, NodeLocation location);

		// Token: 0x060003BC RID: 956
		CssFunction CssFunction(string name, Node value, NodeLocation location);

		// Token: 0x060003BD RID: 957
		Alpha Alpha(Node value, NodeLocation location);

		// Token: 0x060003BE RID: 958
		Call Call(string name, NodeList<Node> arguments, NodeLocation location);

		// Token: 0x060003BF RID: 959
		Color Color(string rgb, NodeLocation location);

		// Token: 0x060003C0 RID: 960
		Keyword Keyword(string value, NodeLocation location);

		// Token: 0x060003C1 RID: 961
		Number Number(string value, string unit, NodeLocation location);

		// Token: 0x060003C2 RID: 962
		Shorthand Shorthand(Node first, Node second, NodeLocation location);

		// Token: 0x060003C3 RID: 963
		Variable Variable(string name, NodeLocation location);

		// Token: 0x060003C4 RID: 964
		Url Url(Node value, IImporter importer, NodeLocation location);

		// Token: 0x060003C5 RID: 965
		Script Script(string script, NodeLocation location);

		// Token: 0x060003C6 RID: 966
		Paren Paren(Node node, NodeLocation location);

		// Token: 0x060003C7 RID: 967
		GuardedRuleset GuardedRuleset(NodeList<Selector> selectors, NodeList rules, Condition condition, NodeLocation location);

		// Token: 0x060003C8 RID: 968
		MixinCall MixinCall(NodeList<Element> elements, List<NamedArgument> arguments, bool important, NodeLocation location);

		// Token: 0x060003C9 RID: 969
		MixinDefinition MixinDefinition(string name, NodeList<Rule> parameters, NodeList rules, Condition condition, bool variadic, NodeLocation location);

		// Token: 0x060003CA RID: 970
		Condition Condition(Node left, string operation, Node right, bool negate, NodeLocation location);

		// Token: 0x060003CB RID: 971
		Import Import(Url path, Value features, ImportOptions option, NodeLocation location);

		// Token: 0x060003CC RID: 972
		Import Import(Quoted path, Value features, ImportOptions option, NodeLocation location);

		// Token: 0x060003CD RID: 973
		Directive Directive(string name, string identifier, NodeList rules, NodeLocation location);

		// Token: 0x060003CE RID: 974
		Directive Directive(string name, Node value, NodeLocation location);

		// Token: 0x060003CF RID: 975
		Media Media(NodeList rules, Value features, NodeLocation location);

		// Token: 0x060003D0 RID: 976
		KeyFrame KeyFrame(NodeList identifier, NodeList rules, NodeLocation location);

		// Token: 0x060003D1 RID: 977
		Expression Expression(NodeList expression, NodeLocation location);

		// Token: 0x060003D2 RID: 978
		Value Value(IEnumerable<Node> values, string important, NodeLocation location);

		// Token: 0x060003D3 RID: 979
		Operation Operation(string operation, Node left, Node right, NodeLocation location);

		// Token: 0x060003D4 RID: 980
		Assignment Assignment(string key, Node value, NodeLocation location);

		// Token: 0x060003D5 RID: 981
		Comment Comment(string value, NodeLocation location);

		// Token: 0x060003D6 RID: 982
		TextNode TextNode(string contents, NodeLocation location);

		// Token: 0x060003D7 RID: 983
		Quoted Quoted(string value, string contents, bool escaped, NodeLocation location);

		// Token: 0x060003D8 RID: 984
		Extend Extend(List<Selector> exact, List<Selector> partial, NodeLocation location);

		// Token: 0x060003D9 RID: 985
		Node Attribute(Node key, Node op, Node val, NodeLocation location);
	}
}

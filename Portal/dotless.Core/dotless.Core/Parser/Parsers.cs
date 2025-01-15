using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser
{
	// Token: 0x02000025 RID: 37
	public class Parsers
	{
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x000047E5 File Offset: 0x000029E5
		// (set) Token: 0x060000F7 RID: 247 RVA: 0x000047ED File Offset: 0x000029ED
		public INodeProvider NodeProvider { get; set; }

		// Token: 0x060000F8 RID: 248 RVA: 0x000047F6 File Offset: 0x000029F6
		public Parsers(INodeProvider nodeProvider)
		{
			this.NodeProvider = nodeProvider;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00004810 File Offset: 0x00002A10
		public NodeList Primary(Parser parser)
		{
			NodeList nodeList = new NodeList();
			this.GatherComments(parser);
			for (;;)
			{
				Node node = this.MixinDefinition(parser);
				Node node2 = (node ? node : (node | this.ExtendRule(parser)));
				Node node3 = (node2 ? node2 : (node2 | this.Rule(parser)));
				Node node4 = (node3 ? node3 : (node3 | this.PullComments()));
				Node node5 = (node4 ? node4 : (node4 | this.GuardedRuleset(parser)));
				Node node6 = (node5 ? node5 : (node5 | this.Ruleset(parser)));
				Node node7 = (node6 ? node6 : (node6 | this.MixinCall(parser)));
				Node node8;
				if (!(node8 = (node7 ? node7 : (node7 | this.Directive(parser)))))
				{
					break;
				}
				NodeList nodeList2;
				if (nodeList2 = this.PullComments())
				{
					nodeList.AddRange(nodeList2);
				}
				nodeList2 = node8 as NodeList;
				if (nodeList2)
				{
					foreach (Node node9 in nodeList2)
					{
						((Comment)node9).IsPreSelectorComment = true;
					}
					nodeList.AddRange(nodeList2);
				}
				else
				{
					nodeList.Add(node8);
				}
				this.GatherComments(parser);
			}
			return nodeList;
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00004984 File Offset: 0x00002B84
		// (set) Token: 0x060000FB RID: 251 RVA: 0x0000498C File Offset: 0x00002B8C
		private NodeList CurrentComments { get; set; }

		// Token: 0x060000FC RID: 252 RVA: 0x00004998 File Offset: 0x00002B98
		private void GatherComments(Parser parser)
		{
			Comment comment;
			while (comment = this.Comment(parser))
			{
				if (this.CurrentComments == null)
				{
					this.CurrentComments = new NodeList();
				}
				this.CurrentComments.Add(comment);
			}
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000049D6 File Offset: 0x00002BD6
		private NodeList PullComments()
		{
			NodeList currentComments = this.CurrentComments;
			this.CurrentComments = null;
			return currentComments;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000049E5 File Offset: 0x00002BE5
		private NodeList GatherAndPullComments(Parser parser)
		{
			this.GatherComments(parser);
			return this.PullComments();
		}

		// Token: 0x060000FF RID: 255 RVA: 0x000049F4 File Offset: 0x00002BF4
		private void PushComments()
		{
			this.CommentsStack.Push(this.PullComments());
		}

		// Token: 0x06000100 RID: 256 RVA: 0x00004A07 File Offset: 0x00002C07
		private void PopComments()
		{
			this.CurrentComments = this.CommentsStack.Pop();
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00004A1C File Offset: 0x00002C1C
		public Comment Comment(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			string comment = parser.Tokenizer.GetComment();
			if (comment != null)
			{
				return this.NodeProvider.Comment(comment, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00004A64 File Offset: 0x00002C64
		public Quoted Quoted(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			bool flag = false;
			char c = parser.Tokenizer.CurrentChar;
			if (parser.Tokenizer.CurrentChar == '~')
			{
				flag = true;
				c = parser.Tokenizer.NextChar;
			}
			if (c != '"' && c != '\'')
			{
				return null;
			}
			if (flag)
			{
				parser.Tokenizer.Match('~');
			}
			string quotedString = parser.Tokenizer.GetQuotedString();
			if (quotedString == null)
			{
				return null;
			}
			return this.NodeProvider.Quoted(quotedString, quotedString.Substring(1, quotedString.Length - 2), flag, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004B04 File Offset: 0x00002D04
		public Keyword Keyword(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("[A-Za-z0-9_-]+");
			if (regexMatchResult)
			{
				return this.NodeProvider.Keyword(regexMatchResult.Value, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004B5C File Offset: 0x00002D5C
		public Call Call(Parser parser)
		{
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("(%|[a-zA-Z0-9_-]+|progid:[\\w\\.]+)\\(");
			if (!regexMatchResult)
			{
				return null;
			}
			if (regexMatchResult[1].ToLowerInvariant() == "alpha")
			{
				Alpha alpha = this.Alpha(parser);
				if (alpha != null)
				{
					return alpha;
				}
			}
			NodeList<Node> nodeList = this.Arguments(parser);
			if (!parser.Tokenizer.Match(')'))
			{
				this.Recall(parser, parserLocation);
				return null;
			}
			return this.NodeProvider.Call(regexMatchResult[1], nodeList, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004C0C File Offset: 0x00002E0C
		public NodeList<Node> Arguments(Parser parser)
		{
			NodeList<Node> nodeList = new NodeList<Node>();
			do
			{
				Node node2;
				Node node = (node2 = this.Assignment(parser));
				if (!(node2 ? node2 : (node2 | (node = this.Expression(parser)))))
				{
					break;
				}
				nodeList.Add(node);
			}
			while (!(!parser.Tokenizer.Match(',')));
			return nodeList;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004C68 File Offset: 0x00002E68
		public Assignment Assignment(Parser parser)
		{
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("\\w+(?=\\s?=)");
			if (!regexMatchResult || !parser.Tokenizer.Match('='))
			{
				return null;
			}
			Node node = this.Entity(parser);
			if (node)
			{
				return this.NodeProvider.Assignment(regexMatchResult.Value, node, regexMatchResult.Location);
			}
			return null;
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00004CD0 File Offset: 0x00002ED0
		public Node Literal(Parser parser)
		{
			Node node = this.Dimension(parser);
			Node node2 = (node ? node : (node | this.Color(parser)));
			if (!Node.op_True(node2))
			{
				return node2 | this.Quoted(parser);
			}
			return node2;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004D18 File Offset: 0x00002F18
		public Url Url(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			if (parser.Tokenizer.CurrentChar != 'u' || !parser.Tokenizer.Match("url\\("))
			{
				return null;
			}
			this.GatherComments(parser);
			Node node = this.Quoted(parser);
			if (!node)
			{
				Parsers.ParserLocation parserLocation = this.Remember(parser);
				node = this.Expression(parser);
				if (node && !parser.Tokenizer.Peek(')'))
				{
					node = null;
					this.Recall(parser, parserLocation);
				}
			}
			else
			{
				node.PreComments = this.PullComments();
				node.PostComments = this.GatherAndPullComments(parser);
			}
			if (!node)
			{
				TextNode textNode = parser.Tokenizer.MatchAny("[^\\)\"']*");
				node = (textNode ? textNode : (textNode | new TextNode("")));
			}
			this.Expect(parser, ')');
			return this.NodeProvider.Url(node, parser.Importer, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004E20 File Offset: 0x00003020
		public Variable Variable(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			if (parser.Tokenizer.CurrentChar == '@' && (regexMatchResult = parser.Tokenizer.Match("@(@?[a-zA-Z0-9_-]+)")))
			{
				return this.NodeProvider.Variable(regexMatchResult.Value, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00004E88 File Offset: 0x00003088
		public Variable InterpolatedVariable(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			if (parser.Tokenizer.CurrentChar == '@' && (regexMatchResult = parser.Tokenizer.Match("@\\{(?<name>@?[a-zA-Z0-9_-]+)\\}")))
			{
				return this.NodeProvider.Variable("@" + regexMatchResult.Match.Groups["name"].Value, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00004F0C File Offset: 0x0000310C
		public Variable VariableCurly(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			if (parser.Tokenizer.CurrentChar == '@' && (regexMatchResult = parser.Tokenizer.Match("@\\{([a-zA-Z0-9_-]+)\\}")))
			{
				return this.NodeProvider.Variable("@" + regexMatchResult.Match.Groups[1].Value, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00004F8C File Offset: 0x0000318C
		public GuardedRuleset GuardedRuleset(Parser parser)
		{
			NodeList<Selector> nodeList = new NodeList<Selector>();
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			int index = parserLocation.TokenizerLocation.Index;
			Selector selector;
			while (selector = this.Selector(parser))
			{
				nodeList.Add(selector);
				if (!parser.Tokenizer.Match(','))
				{
					break;
				}
				this.GatherComments(parser);
			}
			if (parser.Tokenizer.Match("when"))
			{
				this.GatherAndPullComments(parser);
				Condition condition = this.Expect<Condition>(this.Conditions(parser), "Expected conditions after when (guard)", parser);
				NodeList nodeList2 = this.Block(parser);
				return this.NodeProvider.GuardedRuleset(nodeList, nodeList2, condition, parser.Tokenizer.GetNodeLocation(index));
			}
			this.Recall(parser, parserLocation);
			return null;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000504C File Offset: 0x0000324C
		public Extend ExtendRule(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			if ((regexMatchResult = parser.Tokenizer.Match("\\&?:extend\\(")) == null)
			{
				return null;
			}
			List<Selector> list = new List<Selector>();
			List<Selector> list2 = new List<Selector>();
			Selector selector;
			while (selector = this.Selector(parser))
			{
				if (selector.Elements.Count != 1 || selector.Elements.First<Element>().Value != null)
				{
					if (selector.Elements.Count > 1 && selector.Elements.Last<Element>().Value == "all")
					{
						selector.Elements.Remove(selector.Elements.Last<Element>());
						list2.Add(selector);
					}
					else
					{
						list.Add(selector);
					}
					if (!parser.Tokenizer.Match(','))
					{
						break;
					}
				}
			}
			if (!parser.Tokenizer.Match(')'))
			{
				throw new ParsingException("Extend rule not correctly terminated", parser.Tokenizer.GetNodeLocation(index));
			}
			if (regexMatchResult.Match.Value[0] == '&')
			{
				parser.Tokenizer.Match(';');
			}
			if (list2.Count == 0 && list.Count == 0)
			{
				return null;
			}
			return this.NodeProvider.Extend(list, list2, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x0600010E RID: 270 RVA: 0x000051B0 File Offset: 0x000033B0
		public Color Color(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			if (parser.Tokenizer.CurrentChar == '#' && (regexMatchResult = parser.Tokenizer.Match("#([a-fA-F0-9]{8}|[a-fA-F0-9]{6}|[a-fA-F0-9]{3})")))
			{
				return this.NodeProvider.Color(regexMatchResult[1], parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005218 File Offset: 0x00003418
		public Number Dimension(Parser parser)
		{
			char currentChar = parser.Tokenizer.CurrentChar;
			if (!char.IsNumber(currentChar) && currentChar != '.' && currentChar != '-' && currentChar != '+')
			{
				return null;
			}
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("([+-]?[0-9]*\\.?[0-9]+)(px|%|em|pc|ex|in|deg|s|ms|pt|cm|mm|ch|rem|vw|vh|vmin|vm(ax)?|grad|rad|fr|gr|Hz|kHz|dpi|dpcm|dppx)?", true);
			if (regexMatchResult)
			{
				return this.NodeProvider.Number(regexMatchResult[1], regexMatchResult[2], parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x0000529C File Offset: 0x0000349C
		public Script Script(Parser parser)
		{
			if (parser.Tokenizer.CurrentChar != '`')
			{
				return null;
			}
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult = parser.Tokenizer.MatchAny("`[^`]*`");
			if (!regexMatchResult)
			{
				return null;
			}
			return this.NodeProvider.Script(regexMatchResult.Value, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005304 File Offset: 0x00003504
		public string VariableName(Parser parser)
		{
			Variable variable = this.Variable(parser);
			if (variable != null)
			{
				return variable.Name;
			}
			return null;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005324 File Offset: 0x00003524
		public Shorthand Shorthand(Parser parser)
		{
			if (!parser.Tokenizer.Peek("[@%\\w.-]+\\/[@%\\w.-]+"))
			{
				return null;
			}
			int index = parser.Tokenizer.Location.Index;
			Node node = null;
			Node node3;
			Node node2 = (node3 = this.Entity(parser));
			Node node4 = (Node.op_False(node3) ? node3 : (node3 & parser.Tokenizer.Match('/')));
			if (Node.op_False(node4) ? node4 : (node4 & (node = this.Entity(parser))))
			{
				return this.NodeProvider.Shorthand(node2, node, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000053C4 File Offset: 0x000035C4
		public MixinCall MixinCall(Parser parser)
		{
			NodeList<Element> nodeList = new NodeList<Element>();
			int index = parser.Tokenizer.Location.Index;
			bool flag = false;
			Combinator combinator = null;
			this.PushComments();
			int index2 = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult;
			while (regexMatchResult = parser.Tokenizer.Match("[#.][a-zA-Z0-9_-]+"))
			{
				nodeList.Add(this.NodeProvider.Element(combinator, regexMatchResult, parser.Tokenizer.GetNodeLocation(index)));
				int index3 = parser.Tokenizer.Location.Index;
				CharMatchResult charMatchResult = parser.Tokenizer.Match('>');
				combinator = ((charMatchResult != null) ? this.NodeProvider.Combinator(charMatchResult.Value, parser.Tokenizer.GetNodeLocation(index)) : null);
				int index4 = parser.Tokenizer.Location.Index;
			}
			if (nodeList.Count == 0)
			{
				this.PopComments();
				return null;
			}
			List<NamedArgument> list = new List<NamedArgument>();
			if (parser.Tokenizer.Peek('('))
			{
				Parsers.ParserLocation parserLocation = this.Remember(parser);
				RegexMatchResult regexMatchResult2 = parser.Tokenizer.Match("\\([^()]*(?>(?>(?'open'\\()[^()]*)*(?>(?'-open'\\))[^()]*)*)+(?(open)(?!))\\)");
				bool flag2 = regexMatchResult2 != null && regexMatchResult2.Value.Contains(';');
				char c = (flag2 ? ';' : ',');
				this.Recall(parser, parserLocation);
				parser.Tokenizer.Match('(');
				Expression expression;
				while (expression = this.Expression(parser, flag2))
				{
					Expression expression2 = expression;
					string text = null;
					if (expression.Value.Count == 1 && expression.Value[0] is Variable && parser.Tokenizer.Match(':'))
					{
						expression2 = this.Expect<Expression>(this.Expression(parser), "expected value", parser);
						text = (expression.Value[0] as Variable).Name;
					}
					list.Add(new NamedArgument
					{
						Name = text,
						Value = expression2
					});
					if (!parser.Tokenizer.Match(c))
					{
						break;
					}
				}
				this.Expect(parser, ')');
			}
			this.GatherComments(parser);
			if (!string.IsNullOrEmpty(this.Important(parser)))
			{
				flag = true;
			}
			NodeList nodeList2 = this.GatherAndPullComments(parser);
			if (this.End(parser))
			{
				MixinCall mixinCall = this.NodeProvider.MixinCall(nodeList, list, flag, parser.Tokenizer.GetNodeLocation(index));
				mixinCall.PostComments = nodeList2;
				this.PopComments();
				return mixinCall;
			}
			this.PopComments();
			return null;
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005632 File Offset: 0x00003832
		private Expression Expression(Parser parser, bool allowList)
		{
			if (!allowList)
			{
				return this.Expression(parser);
			}
			return this.ExpressionOrExpressionList(parser);
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005648 File Offset: 0x00003848
		public MixinDefinition MixinDefinition(Parser parser)
		{
			if ((parser.Tokenizer.CurrentChar != '.' && parser.Tokenizer.CurrentChar != '#') || parser.Tokenizer.Peek("[^{]*}"))
			{
				return null;
			}
			int index = parser.Tokenizer.Location.Index;
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("([#.](?:[\\w-]|\\\\(?:[a-fA-F0-9]{1,6} ?|[^a-fA-F0-9]))+)\\s*\\(");
			if (!regexMatchResult)
			{
				return null;
			}
			this.PushComments();
			this.GatherAndPullComments(parser);
			string text = regexMatchResult[1];
			bool flag = false;
			NodeList<Rule> nodeList = new NodeList<Rule>();
			Condition condition = null;
			int index2;
			RegexMatchResult regexMatchResult2;
			for (;;)
			{
				index2 = parser.Tokenizer.Location.Index;
				if (parser.Tokenizer.CurrentChar == '.' && parser.Tokenizer.Match("\\.{3}"))
				{
					break;
				}
				if (regexMatchResult2 = parser.Tokenizer.Match("@[a-zA-Z0-9_-]+"))
				{
					this.GatherAndPullComments(parser);
					if (parser.Tokenizer.Match(':'))
					{
						this.GatherComments(parser);
						Expression expression = this.Expect<Expression>(this.Expression(parser), "Expected value", parser);
						nodeList.Add(this.NodeProvider.Rule(regexMatchResult2.Value, expression, parser.Tokenizer.GetNodeLocation(index2)));
					}
					else
					{
						if (parser.Tokenizer.Match("\\.{3}"))
						{
							goto Block_7;
						}
						nodeList.Add(this.NodeProvider.Rule(regexMatchResult2.Value, null, parser.Tokenizer.GetNodeLocation(index2)));
					}
				}
				else
				{
					Node node = this.Literal(parser);
					Node node2;
					if (!(node2 = (node ? node : (node | this.Keyword(parser)))))
					{
						goto IL_025D;
					}
					nodeList.Add(this.NodeProvider.Rule(null, node2, parser.Tokenizer.GetNodeLocation(index2)));
				}
				this.GatherAndPullComments(parser);
				TextNode textNode = parser.Tokenizer.Match(',');
				if (!(textNode ? textNode : (textNode | parser.Tokenizer.Match(';'))))
				{
					goto IL_025D;
				}
				this.GatherAndPullComments(parser);
			}
			flag = true;
			goto IL_025D;
			Block_7:
			flag = true;
			nodeList.Add(this.NodeProvider.Rule(regexMatchResult2.Value, null, true, parser.Tokenizer.GetNodeLocation(index2)));
			IL_025D:
			if (!parser.Tokenizer.Match(')'))
			{
				this.Recall(parser, parserLocation);
			}
			this.GatherAndPullComments(parser);
			if (parser.Tokenizer.Match("when"))
			{
				this.GatherAndPullComments(parser);
				condition = this.Expect<Condition>(this.Conditions(parser), "Expected conditions after when (mixin guards)", parser);
			}
			NodeList nodeList2 = this.Block(parser);
			this.PopComments();
			if (nodeList2 != null)
			{
				return this.NodeProvider.MixinDefinition(text, nodeList, nodeList2, condition, flag, parser.Tokenizer.GetNodeLocation(index));
			}
			this.Recall(parser, parserLocation);
			return null;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x00005948 File Offset: 0x00003B48
		public Condition Conditions(Parser parser)
		{
			Condition condition;
			if (condition = this.Condition(parser))
			{
				while (parser.Tokenizer.Match(','))
				{
					Condition condition2 = this.Expect<Condition>(this.Condition(parser), ", without recognised condition", parser);
					condition = this.NodeProvider.Condition(condition, "or", condition2, false, parser.Tokenizer.GetNodeLocation());
				}
				return condition;
			}
			return null;
		}

		// Token: 0x06000117 RID: 279 RVA: 0x000059B4 File Offset: 0x00003BB4
		public Condition Condition(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			bool flag = false;
			if (parser.Tokenizer.Match("not"))
			{
				flag = true;
			}
			this.Expect(parser, '(');
			Node node = this.Operation(parser);
			Node node2 = (node ? node : (node | this.Keyword(parser)));
			Node node3 = this.Expect<Node>(node2 ? node2 : (node2 | this.Quoted(parser)), "unrecognised condition", parser);
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("(>=|=<|[<=>])");
			Condition condition;
			if (regexMatchResult)
			{
				node = this.Operation(parser);
				node2 = (node ? node : (node | this.Keyword(parser)));
				Node node4 = this.Expect<Node>(node2 ? node2 : (node2 | this.Quoted(parser)), "unrecognised right hand side condition expression", parser);
				condition = this.NodeProvider.Condition(node3, regexMatchResult.Value, node4, flag, parser.Tokenizer.GetNodeLocation(index));
			}
			else
			{
				condition = this.NodeProvider.Condition(node3, "=", this.NodeProvider.Keyword("true", parser.Tokenizer.GetNodeLocation(index)), flag, parser.Tokenizer.GetNodeLocation(index));
			}
			this.Expect(parser, ')');
			if (parser.Tokenizer.Match("and"))
			{
				return this.NodeProvider.Condition(condition, "and", this.Condition(parser), false, parser.Tokenizer.GetNodeLocation(index));
			}
			return condition;
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005B58 File Offset: 0x00003D58
		public Node Entity(Parser parser)
		{
			Node node = this.Literal(parser);
			Node node2 = (node ? node : (node | this.Variable(parser)));
			Node node3 = (node2 ? node2 : (node2 | this.Url(parser)));
			Node node4 = (node3 ? node3 : (node3 | this.Call(parser)));
			Node node5 = (node4 ? node4 : (node4 | this.Keyword(parser)));
			if (!Node.op_True(node5))
			{
				return node5 | this.Script(parser);
			}
			return node5;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x00005BEC File Offset: 0x00003DEC
		private Expression ExpressionOrExpressionList(Parser parser)
		{
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			List<Expression> list = new List<Expression>();
			Expression expression;
			while (expression = this.Expression(parser))
			{
				list.Add(expression);
				if (!parser.Tokenizer.Match(','))
				{
					break;
				}
			}
			if (list.Count == 0)
			{
				this.Recall(parser, parserLocation);
				return null;
			}
			if (list.Count == 1)
			{
				return list[0];
			}
			return new Expression(list.Cast<Node>(), true);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005C63 File Offset: 0x00003E63
		public bool End(Parser parser)
		{
			return parser.Tokenizer.Match(";[;\\s]*") || parser.Tokenizer.Peek('}');
		}

		// Token: 0x0600011B RID: 283 RVA: 0x00005C8C File Offset: 0x00003E8C
		public Alpha Alpha(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			if (!parser.Tokenizer.Match("opacity\\s*=\\s*", true))
			{
				return null;
			}
			Node node = parser.Tokenizer.Match("[0-9]+");
			Node node2;
			if (node2 = (node ? node : (node | this.Variable(parser))))
			{
				this.Expect(parser, ')');
				return this.NodeProvider.Alpha(node2, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600011C RID: 284 RVA: 0x00005D1C File Offset: 0x00003F1C
		public Element Element(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			this.GatherComments(parser);
			Combinator combinator = this.Combinator(parser);
			this.PushComments();
			this.GatherComments(parser);
			if (parser.Tokenizer.Peek("when"))
			{
				return null;
			}
			Node node = this.ExtendRule(parser);
			Node node2 = (node ? node : (node | this.NonPseudoClassSelector(parser)));
			Node node3 = (node2 ? node2 : (node2 | Parsers.PseudoClassSelector(parser)));
			Node node4 = (node3 ? node3 : (node3 | Parsers.PseudoElementSelector(parser)));
			Node node5 = (node4 ? node4 : (node4 | parser.Tokenizer.Match('*')));
			Node node6 = (node5 ? node5 : (node5 | parser.Tokenizer.Match('&')));
			Node node7 = (node6 ? node6 : (node6 | this.Attribute(parser)));
			Node node8 = (node7 ? node7 : (node7 | parser.Tokenizer.MatchAny("\\(((?<N>\\()|(?<-N>\\))|[^()@]*)+\\)")));
			Node node9 = (node8 ? node8 : (node8 | parser.Tokenizer.Match("[\\.#](?=@\\{)")));
			Node node10 = (node9 ? node9 : (node9 | this.VariableCurly(parser)));
			if (!node10 && parser.Tokenizer.Match('('))
			{
				Variable variable = this.Variable(parser) ?? this.VariableCurly(parser);
				if (variable)
				{
					parser.Tokenizer.Match(')');
					node10 = this.NodeProvider.Paren(variable, parser.Tokenizer.GetNodeLocation(index));
				}
			}
			if (node10)
			{
				combinator.PostComments = this.PullComments();
				this.PopComments();
				combinator.PreComments = this.PullComments();
				return this.NodeProvider.Element(combinator, node10, parser.Tokenizer.GetNodeLocation(index));
			}
			this.PopComments();
			return null;
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00005F37 File Offset: 0x00004137
		private static RegexMatchResult PseudoClassSelector(Parser parser)
		{
			return parser.Tokenizer.Match(":(\\\\.|[a-zA-Z0-9_-])+");
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00005F49 File Offset: 0x00004149
		private static RegexMatchResult PseudoElementSelector(Parser parser)
		{
			return parser.Tokenizer.Match("::(\\\\.|[a-zA-Z0-9_-])+");
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00005F5C File Offset: 0x0000415C
		private Node NonPseudoClassSelector(Parser parser)
		{
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("[.#]?(\\\\.|[a-zA-Z0-9_-])+");
			if (!regexMatchResult)
			{
				return null;
			}
			if (parser.Tokenizer.Match('('))
			{
				this.Recall(parser, parserLocation);
				return null;
			}
			return regexMatchResult;
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00005FAC File Offset: 0x000041AC
		public Combinator Combinator(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			Node node;
			if (node = parser.Tokenizer.Match("[+>~]"))
			{
				return this.NodeProvider.Combinator(node.ToString(), parser.Tokenizer.GetNodeLocation(index));
			}
			return this.NodeProvider.Combinator(char.IsWhiteSpace(parser.Tokenizer.GetPreviousCharIgnoringComments()) ? " " : null, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006034 File Offset: 0x00004234
		public Selector Selector(Parser parser)
		{
			int num = 0;
			NodeList<Element> nodeList = new NodeList<Element>();
			int index = parser.Tokenizer.Location.Index;
			this.GatherComments(parser);
			this.PushComments();
			if (parser.Tokenizer.Match('('))
			{
				Node node = this.Entity(parser);
				this.Expect(parser, ')');
				return this.NodeProvider.Selector(new NodeList<Element> { this.NodeProvider.Element(null, node, parser.Tokenizer.GetNodeLocation(index)) }, parser.Tokenizer.GetNodeLocation(index));
			}
			for (;;)
			{
				Element element = this.Element(parser);
				if (!element)
				{
					break;
				}
				num++;
				nodeList.Add(element);
			}
			if (num > 0)
			{
				Selector selector = this.NodeProvider.Selector(nodeList, parser.Tokenizer.GetNodeLocation(index));
				selector.PostComments = this.GatherAndPullComments(parser);
				this.PopComments();
				selector.PreComments = this.PullComments();
				return selector;
			}
			this.PopComments();
			return null;
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000612C File Offset: 0x0000432C
		public Node Tag(Parser parser)
		{
			TextNode textNode = parser.Tokenizer.Match("[a-zA-Z][a-zA-Z-]*[0-9]?");
			if (!Node.op_True(textNode))
			{
				return textNode | parser.Tokenizer.Match('*');
			}
			return textNode;
		}

		// Token: 0x06000123 RID: 291 RVA: 0x00006168 File Offset: 0x00004368
		public Node Attribute(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			if (!parser.Tokenizer.Match('['))
			{
				return null;
			}
			Node node = this.InterpolatedVariable(parser);
			Node node2 = (node ? node : (node | parser.Tokenizer.Match("(\\\\.|[a-z0-9_-])+", true)));
			Node node3 = (node2 ? node2 : (node2 | this.Quoted(parser)));
			if (!node3)
			{
				return null;
			}
			Node node4 = parser.Tokenizer.Match("[|~*$^]?=");
			TextNode textNode = this.Quoted(parser);
			Node node5 = (textNode ? textNode : (textNode | parser.Tokenizer.Match("[\\w-]+")));
			this.Expect(parser, ']');
			return this.NodeProvider.Attribute(node3, node4, node5, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006254 File Offset: 0x00004454
		public NodeList Block(Parser parser)
		{
			if (!parser.Tokenizer.Match('{'))
			{
				return null;
			}
			NodeList nodeList = this.Expect<NodeList>(this.Primary(parser), "Expected content inside block", parser);
			this.Expect(parser, '}');
			return nodeList;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x00006288 File Offset: 0x00004488
		public Ruleset Ruleset(Parser parser)
		{
			NodeList<Selector> nodeList = new NodeList<Selector>();
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			int index = parserLocation.TokenizerLocation.Index;
			Selector selector;
			while (selector = this.Selector(parser))
			{
				nodeList.Add(selector);
				if (!parser.Tokenizer.Match(','))
				{
					break;
				}
				this.GatherComments(parser);
			}
			NodeList nodeList2;
			if (nodeList.Count > 0 && (nodeList2 = this.Block(parser)) != null)
			{
				return this.NodeProvider.Ruleset(nodeList, nodeList2, parser.Tokenizer.GetNodeLocation(index));
			}
			this.Recall(parser, parserLocation);
			return null;
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000631C File Offset: 0x0000451C
		public Rule Rule(Parser parser)
		{
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			this.PushComments();
			Variable variable = null;
			string text = this.Property(parser);
			bool flag = false;
			if (string.IsNullOrEmpty(text))
			{
				variable = this.Variable(parser);
				if (variable != null)
				{
					text = variable.Name;
				}
				else
				{
					Variable variable2 = this.InterpolatedVariable(parser);
					if (variable2 != null)
					{
						flag = true;
						text = variable2.Name;
					}
				}
			}
			NodeList nodeList = this.GatherAndPullComments(parser);
			if (text != null && parser.Tokenizer.Match(':'))
			{
				NodeList nodeList2 = this.GatherAndPullComments(parser);
				Node node;
				if (text == "font")
				{
					node = this.Font(parser);
				}
				else if (this.MatchesProperty("filter", text))
				{
					Node node2 = this.FilterExpressionList(parser);
					node = (node2 ? node2 : (node2 | this.Value(parser)));
				}
				else
				{
					node = this.Value(parser);
				}
				if (variable != null && node == null)
				{
					node = parser.Tokenizer.Match("[^;]*");
				}
				NodeList nodeList3 = this.GatherAndPullComments(parser);
				if (this.End(parser))
				{
					if (node == null)
					{
						throw new ParsingException(text + " is incomplete", parser.Tokenizer.GetNodeLocation());
					}
					node.PreComments = nodeList2;
					node.PostComments = nodeList3;
					Rule rule = this.NodeProvider.Rule(text, node, parser.Tokenizer.GetNodeLocation(parserLocation.TokenizerLocation.Index));
					if (flag)
					{
						rule.InterpolatedName = true;
						rule.Variable = false;
					}
					rule.PostNameComments = nodeList;
					this.PopComments();
					return rule;
				}
			}
			this.PopComments();
			this.Recall(parser, parserLocation);
			return null;
		}

		// Token: 0x06000127 RID: 295 RVA: 0x000064B3 File Offset: 0x000046B3
		private bool MatchesProperty(string expectedPropertyName, string actualPropertyName)
		{
			return string.Equals(expectedPropertyName, actualPropertyName) || Regex.IsMatch(actualPropertyName, string.Format("-(\\w+)-{0}", expectedPropertyName));
		}

		// Token: 0x06000128 RID: 296 RVA: 0x000064D4 File Offset: 0x000046D4
		private CssFunctionList FilterExpressionList(Parser parser)
		{
			CssFunctionList cssFunctionList = new CssFunctionList();
			Node node;
			while (node = this.FilterExpression(parser))
			{
				cssFunctionList.Add(node);
			}
			if (!cssFunctionList.Any<Node>())
			{
				return null;
			}
			return cssFunctionList;
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000650C File Offset: 0x0000470C
		private Node FilterExpression(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			this.GatherComments(parser);
			Url url = this.Url(parser);
			if (url)
			{
				return url;
			}
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("\\s*(blur|brightness|contrast|drop-shadow|grayscale|hue-rotate|invert|opacity|saturate|sepia|url)\\s*\\(");
			if (regexMatchResult == null)
			{
				return null;
			}
			Value value = this.Value(parser);
			if (value == null)
			{
				return null;
			}
			this.Expect(parser, ')');
			CssFunction cssFunction = this.NodeProvider.CssFunction(regexMatchResult.Match.Groups[1].Value.Trim(), value, parser.Tokenizer.GetNodeLocation(index));
			cssFunction.PreComments = this.PullComments();
			cssFunction.PostComments = this.GatherAndPullComments(parser);
			return cssFunction;
		}

		// Token: 0x0600012A RID: 298 RVA: 0x000065BC File Offset: 0x000047BC
		public Import Import(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			if (!parser.Tokenizer.Match("@import(-(once))?\\s+"))
			{
				return null;
			}
			ImportOptions importOptions = Parsers.ParseOptions(parser);
			Node node = this.Quoted(parser);
			Node node2 = (node ? node : (node | this.Url(parser)));
			if (!node2)
			{
				return null;
			}
			Value value = this.MediaFeatures(parser);
			this.Expect(parser, ';', "Expected ';' (possibly unrecognised media sequence)");
			if (node2 is Quoted)
			{
				return this.NodeProvider.Import(node2 as Quoted, value, importOptions, parser.Tokenizer.GetNodeLocation(index));
			}
			if (node2 is Url)
			{
				return this.NodeProvider.Import(node2 as Url, value, importOptions, parser.Tokenizer.GetNodeLocation(index));
			}
			throw new ParsingException("unrecognised @import format", parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x0600012B RID: 299 RVA: 0x000066A8 File Offset: 0x000048A8
		private static ImportOptions ParseOptions(Parser parser)
		{
			int index = parser.Tokenizer.Location.Index;
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("\\((?<keywords>.*)\\)");
			if (!regexMatchResult)
			{
				return ImportOptions.Once;
			}
			string value = regexMatchResult.Match.Groups["keywords"].Value;
			IEnumerable<string> enumerable = from kw in value.Split(new char[] { ',' })
				select kw.Trim();
			ImportOptions importOptions = (ImportOptions)0;
			foreach (string text in enumerable)
			{
				try
				{
					ImportOptions importOptions2 = (ImportOptions)Enum.Parse(typeof(ImportOptions), text, true);
					importOptions |= importOptions2;
				}
				catch (ArgumentException)
				{
					throw new ParsingException(string.Format("unrecognized @import option '{0}'", text), parser.Tokenizer.GetNodeLocation(index));
				}
			}
			Parsers.CheckForConflictingOptions(parser, importOptions, value, index);
			return importOptions;
		}

		// Token: 0x0600012C RID: 300 RVA: 0x000067C4 File Offset: 0x000049C4
		private static void CheckForConflictingOptions(Parser parser, ImportOptions options, string allKeywords, int index)
		{
			foreach (ImportOptions[] array2 in Parsers.illegalOptionCombinations)
			{
				if (Parsers.IsOptionSet(options, array2[0]) && Parsers.IsOptionSet(options, array2[1]))
				{
					throw new ParsingException(string.Format("invalid combination of @import options ({0}) -- specify either {1} or {2}, but not both", allKeywords, array2[0].ToString().ToLowerInvariant(), array2[1].ToString().ToLowerInvariant()), parser.Tokenizer.GetNodeLocation(index));
				}
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000684A File Offset: 0x00004A4A
		private static bool IsOptionSet(ImportOptions options, ImportOptions test)
		{
			return (options & test) == test;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x00006854 File Offset: 0x00004A54
		public Node Directive(Parser parser)
		{
			if (parser.Tokenizer.CurrentChar != '@')
			{
				return null;
			}
			Import import = this.Import(parser);
			if (import)
			{
				return import;
			}
			Media media = this.Media(parser);
			if (media)
			{
				return media;
			}
			this.GatherComments(parser);
			int index = parser.Tokenizer.Location.Index;
			string text = parser.Tokenizer.MatchString("@[-a-z]+");
			if (string.IsNullOrEmpty(text))
			{
				return null;
			}
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			string text2 = "[^{]+";
			string text3 = text;
			if (text.StartsWith("@-") && text.IndexOf('-', 2) > 0)
			{
				text3 = "@" + text.Substring(text.IndexOf('-', 2) + 1);
			}
			uint num = <PrivateImplementationDetails>.ComputeStringHash(text3);
			if (num <= 1790797137U)
			{
				if (num <= 1115695852U)
				{
					if (num <= 141895044U)
					{
						if (num != 53045354U)
						{
							if (num != 141895044U)
							{
								goto IL_03FB;
							}
							if (!(text3 == "@page"))
							{
								goto IL_03FB;
							}
						}
						else
						{
							if (!(text3 == "@top-left-corner"))
							{
								goto IL_03FB;
							}
							goto IL_03F0;
						}
					}
					else if (num != 217100364U)
					{
						if (num != 966173595U)
						{
							if (num != 1115695852U)
							{
								goto IL_03FB;
							}
							if (!(text3 == "@keyframes"))
							{
								goto IL_03FB;
							}
							flag3 = true;
							flag = true;
							goto IL_03FB;
						}
						else
						{
							if (!(text3 == "@bottom-right-corner"))
							{
								goto IL_03FB;
							}
							goto IL_03F0;
						}
					}
					else
					{
						if (!(text3 == "@left-middle"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else if (num <= 1575373189U)
				{
					if (num != 1424443600U)
					{
						if (num != 1464067500U)
						{
							if (num != 1575373189U)
							{
								goto IL_03FB;
							}
							if (!(text3 == "@viewport"))
							{
								goto IL_03FB;
							}
							goto IL_03F0;
						}
						else
						{
							if (!(text3 == "@bottom-left-corner"))
							{
								goto IL_03FB;
							}
							goto IL_03F0;
						}
					}
					else
					{
						if (!(text3 == "@left-top"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else if (num != 1639185998U)
				{
					if (num != 1717563521U)
					{
						if (num != 1790797137U)
						{
							goto IL_03FB;
						}
						if (!(text3 == "@top-right"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
					else
					{
						if (!(text3 == "@right-bottom"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else
				{
					if (!(text3 == "@left-bottom"))
					{
						goto IL_03FB;
					}
					goto IL_03F0;
				}
			}
			else if (num <= 2626319082U)
			{
				if (num <= 2185240437U)
				{
					if (num != 2021056855U)
					{
						if (num != 2185240437U)
						{
							goto IL_03FB;
						}
						if (!(text3 == "@right-top"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
					else
					{
						if (!(text3 == "@right-middle"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else if (num != 2329638312U)
				{
					if (num != 2337388350U)
					{
						if (num != 2626319082U)
						{
							goto IL_03FB;
						}
						if (!(text3 == "@document"))
						{
							goto IL_03FB;
						}
					}
					else
					{
						if (!(text3 == "@bottom-left"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else
				{
					if (!(text3 == "@top-center"))
					{
						goto IL_03FB;
					}
					goto IL_03F0;
				}
			}
			else if (num <= 3525801594U)
			{
				if (num != 2810103827U)
				{
					if (num != 3209067705U)
					{
						if (num != 3525801594U)
						{
							goto IL_03FB;
						}
						if (!(text3 == "@bottom-center"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
					else
					{
						if (!(text3 == "@top-right-corner"))
						{
							goto IL_03FB;
						}
						goto IL_03F0;
					}
				}
				else
				{
					if (!(text3 == "@bottom-right"))
					{
						goto IL_03FB;
					}
					goto IL_03F0;
				}
			}
			else if (num != 3594314923U)
			{
				if (num != 3872618680U)
				{
					if (num != 4114243044U)
					{
						goto IL_03FB;
					}
					if (!(text3 == "@top-left"))
					{
						goto IL_03FB;
					}
					goto IL_03F0;
				}
				else
				{
					if (!(text3 == "@font-face"))
					{
						goto IL_03FB;
					}
					flag2 = true;
					goto IL_03FB;
				}
			}
			else if (!(text3 == "@supports"))
			{
				goto IL_03FB;
			}
			flag2 = true;
			flag = true;
			goto IL_03FB;
			IL_03F0:
			flag2 = true;
			IL_03FB:
			string text4 = "";
			NodeList nodeList = this.PullComments();
			if (flag)
			{
				this.GatherComments(parser);
				RegexMatchResult regexMatchResult = parser.Tokenizer.MatchAny(text2);
				if (regexMatchResult != null)
				{
					text4 = regexMatchResult.Value.Trim();
				}
			}
			NodeList nodeList2 = this.GatherAndPullComments(parser);
			if (flag2)
			{
				NodeList nodeList3 = this.Block(parser);
				if (nodeList3 != null)
				{
					nodeList3.PreComments = nodeList2;
					return this.NodeProvider.Directive(text, text4, nodeList3, parser.Tokenizer.GetNodeLocation(index));
				}
			}
			else
			{
				if (flag3)
				{
					Directive directive = this.KeyFrameBlock(parser, text, text4, index);
					directive.PreComments = nodeList2;
					return directive;
				}
				Node node;
				if (node = this.Expression(parser))
				{
					node.PreComments = nodeList2;
					node.PostComments = this.GatherAndPullComments(parser);
					this.Expect(parser, ';', "missing semicolon in expression");
					Directive directive2 = this.NodeProvider.Directive(text, node, parser.Tokenizer.GetNodeLocation(index));
					directive2.PreComments = nodeList;
					return directive2;
				}
			}
			throw new ParsingException("directive block with unrecognised format", parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x0600012F RID: 303 RVA: 0x00006D64 File Offset: 0x00004F64
		public Expression MediaFeature(Parser parser)
		{
			NodeList nodeList = new NodeList();
			int index = parser.Tokenizer.Location.Index;
			for (;;)
			{
				this.GatherComments(parser);
				Keyword keyword = this.Keyword(parser);
				if (keyword)
				{
					keyword.PreComments = this.PullComments();
					keyword.PostComments = this.GatherAndPullComments(parser);
					nodeList.Add(keyword);
				}
				else
				{
					if (!parser.Tokenizer.Match('('))
					{
						goto IL_01E0;
					}
					this.GatherComments(parser);
					Parsers.ParserLocation parserLocation = this.Remember(parser);
					int index2 = parser.Tokenizer.Location.Index;
					string text = this.Property(parser);
					this.GatherAndPullComments(parser);
					if (!string.IsNullOrEmpty(text) && !parser.Tokenizer.Match(':'))
					{
						this.Recall(parser, parserLocation);
						text = null;
					}
					this.GatherComments(parser);
					parserLocation = this.Remember(parser);
					Node node = this.Entity(parser);
					if (!node || !parser.Tokenizer.Match(')'))
					{
						this.Recall(parser, parserLocation);
						RegexMatchResult regexMatchResult = parser.Tokenizer.Match("[^\\){]+");
						if (regexMatchResult)
						{
							node = this.NodeProvider.TextNode(regexMatchResult.Value, parser.Tokenizer.GetNodeLocation());
							this.Expect(parser, ')');
						}
					}
					if (!node)
					{
						break;
					}
					node.PreComments = this.PullComments();
					node.PostComments = this.GatherAndPullComments(parser);
					if (!string.IsNullOrEmpty(text))
					{
						Rule rule = this.NodeProvider.Rule(text, node, parser.Tokenizer.GetNodeLocation(index2));
						rule.IsSemiColonRequired = false;
						nodeList.Add(this.NodeProvider.Paren(rule, parser.Tokenizer.GetNodeLocation(index2)));
					}
					else
					{
						nodeList.Add(this.NodeProvider.Paren(node, parser.Tokenizer.GetNodeLocation(index2)));
					}
				}
			}
			return null;
			IL_01E0:
			if (nodeList.Count == 0)
			{
				return null;
			}
			return this.NodeProvider.Expression(nodeList, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000130 RID: 304 RVA: 0x00006F74 File Offset: 0x00005174
		public Value MediaFeatures(Parser parser)
		{
			List<Node> list = new List<Node>();
			int index = parser.Tokenizer.Location.Index;
			for (;;)
			{
				Node node = this.MediaFeature(parser);
				Node node2 = (node ? node : (node | this.Variable(parser)));
				if (!node2)
				{
					break;
				}
				list.Add(node2);
				if (!parser.Tokenizer.Match(","))
				{
					goto Block_3;
				}
			}
			return null;
			Block_3:
			return this.NodeProvider.Value(list, null, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000131 RID: 305 RVA: 0x00006FFC File Offset: 0x000051FC
		public Media Media(Parser parser)
		{
			if (!parser.Tokenizer.Match("@media"))
			{
				return null;
			}
			int index = parser.Tokenizer.Location.Index;
			Value value = this.MediaFeatures(parser);
			NodeList nodeList = this.GatherAndPullComments(parser);
			NodeList nodeList2 = this.Expect<NodeList>(this.Block(parser), "@media block with unrecognised format", parser);
			nodeList2.PreComments = nodeList;
			return this.NodeProvider.Media(nodeList2, value, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000132 RID: 306 RVA: 0x00007078 File Offset: 0x00005278
		public Directive KeyFrameBlock(Parser parser, string name, string identifier, int index)
		{
			if (!parser.Tokenizer.Match('{'))
			{
				return null;
			}
			NodeList nodeList = new NodeList();
			for (;;)
			{
				this.GatherComments(parser);
				NodeList nodeList2 = new NodeList();
				for (;;)
				{
					RegexMatchResult regexMatchResult;
					if (nodeList2.Count > 0)
					{
						regexMatchResult = this.Expect<RegexMatchResult>(parser.Tokenizer.Match("from|to|([0-9\\.]+%)"), "@keyframe block unknown identifier", parser);
					}
					else
					{
						regexMatchResult = parser.Tokenizer.Match("from|to|([0-9\\.]+%)");
						if (!regexMatchResult)
						{
							break;
						}
					}
					nodeList2.Add(new Element(null, regexMatchResult));
					this.GatherComments(parser);
					if (!parser.Tokenizer.Match(","))
					{
						break;
					}
					this.GatherComments(parser);
				}
				if (nodeList2.Count == 0)
				{
					break;
				}
				NodeList nodeList3 = this.GatherAndPullComments(parser);
				NodeList nodeList4 = this.Expect<NodeList>(this.Block(parser), "Expected css block after key frame identifier", parser);
				nodeList4.PreComments = nodeList3;
				nodeList4.PostComments = this.GatherAndPullComments(parser);
				nodeList.Add(this.NodeProvider.KeyFrame(nodeList2, nodeList4, parser.Tokenizer.GetNodeLocation()));
			}
			this.Expect(parser, '}', "Expected start, finish, % or '}}' but got {1}");
			return this.NodeProvider.Directive(name, identifier, nodeList, parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000133 RID: 307 RVA: 0x000071AC File Offset: 0x000053AC
		public Value Font(Parser parser)
		{
			NodeList nodeList = new NodeList();
			NodeList nodeList2 = new NodeList();
			int index = parser.Tokenizer.Location.Index;
			for (;;)
			{
				Node node = this.Shorthand(parser);
				Node node2;
				if (!(node2 = (node ? node : (node | this.Entity(parser)))))
				{
					break;
				}
				nodeList2.Add(node2);
			}
			nodeList.Add(this.NodeProvider.Expression(nodeList2, parser.Tokenizer.GetNodeLocation(index)));
			if (parser.Tokenizer.Match(','))
			{
				Node node2;
				while (node2 = this.Expression(parser))
				{
					nodeList.Add(node2);
					if (!parser.Tokenizer.Match(','))
					{
						break;
					}
				}
			}
			return this.NodeProvider.Value(nodeList, this.Important(parser), parser.Tokenizer.GetNodeLocation(index));
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000728C File Offset: 0x0000548C
		public Value Value(Parser parser)
		{
			NodeList nodeList = new NodeList();
			int index = parser.Tokenizer.Location.Index;
			Node node;
			while (node = this.Expression(parser))
			{
				nodeList.Add(node);
				if (!parser.Tokenizer.Match(','))
				{
					break;
				}
			}
			this.GatherComments(parser);
			string text = string.Join(" ", new string[]
			{
				this.IESlash9Hack(parser),
				this.Important(parser)
			}.Where((string x) => x != "").ToArray<string>());
			if (nodeList.Count > 0 || parser.Tokenizer.Match(';'))
			{
				Value value = this.NodeProvider.Value(nodeList, text, parser.Tokenizer.GetNodeLocation(index));
				if (!string.IsNullOrEmpty(text))
				{
					value.PreImportantComments = this.PullComments();
				}
				return value;
			}
			return null;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x00007384 File Offset: 0x00005584
		public string Important(Parser parser)
		{
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("!\\s*important");
			if (regexMatchResult != null)
			{
				return regexMatchResult.Value;
			}
			return "";
		}

		// Token: 0x06000136 RID: 310 RVA: 0x000073B4 File Offset: 0x000055B4
		public string IESlash9Hack(Parser parser)
		{
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("\\\\9");
			if (regexMatchResult != null)
			{
				return regexMatchResult.Value;
			}
			return "";
		}

		// Token: 0x06000137 RID: 311 RVA: 0x000073E4 File Offset: 0x000055E4
		public Expression Sub(Parser parser)
		{
			if (!parser.Tokenizer.Match('('))
			{
				return null;
			}
			Parsers.ParserLocation parserLocation = this.Remember(parser);
			Expression expression = this.Expression(parser);
			if (expression != null && parser.Tokenizer.Match(')'))
			{
				return expression;
			}
			this.Recall(parser, parserLocation);
			return null;
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000743C File Offset: 0x0000563C
		public Node Multiplication(Parser parser)
		{
			this.GatherComments(parser);
			Node node = this.Operand(parser);
			if (!node)
			{
				return null;
			}
			Node node2 = node;
			for (;;)
			{
				this.GatherComments(parser);
				int index = parser.Tokenizer.Location.Index;
				RegexMatchResult regexMatchResult = parser.Tokenizer.Match("[\\/*]");
				this.GatherComments(parser);
				Node node3 = null;
				Node node4 = regexMatchResult;
				if (!(Node.op_False(node4) ? node4 : (node4 & (node3 = this.Operand(parser)))))
				{
					break;
				}
				node2 = this.NodeProvider.Operation(regexMatchResult.Value, node2, node3, parser.Tokenizer.GetNodeLocation(index));
			}
			return node2;
		}

		// Token: 0x06000139 RID: 313 RVA: 0x000074E3 File Offset: 0x000056E3
		public Node UnicodeRange(Parser parser)
		{
			return parser.Tokenizer.Match("(U\\+[0-9a-f]+(-[0-9a-f]+))", true) ?? parser.Tokenizer.Match("(U\\+[0-9a-f?]+)", true);
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000750C File Offset: 0x0000570C
		public Node Operation(Parser parser)
		{
			bool strictMath = parser.StrictMath;
			Node node;
			try
			{
				parser.StrictMath = false;
				if (strictMath && parser.Tokenizer.Match('(') == null)
				{
					node = null;
				}
				else
				{
					Node node2 = this.Multiplication(parser);
					if (!node2)
					{
						node = null;
					}
					else
					{
						Operation operation = null;
						for (;;)
						{
							this.GatherComments(parser);
							int index = parser.Tokenizer.Location.Index;
							RegexMatchResult regexMatchResult = parser.Tokenizer.Match("[-+]\\s+");
							if (!regexMatchResult && !char.IsWhiteSpace(parser.Tokenizer.GetPreviousCharIgnoringComments()))
							{
								regexMatchResult = parser.Tokenizer.Match("[-+]");
							}
							Node node3 = null;
							Node node4 = regexMatchResult;
							if (!(Node.op_False(node4) ? node4 : (node4 & (node3 = this.Multiplication(parser)))))
							{
								break;
							}
							operation = this.NodeProvider.Operation(regexMatchResult.Value, operation ?? node2, node3, parser.Tokenizer.GetNodeLocation(index));
						}
						if (strictMath)
						{
							this.Expect(parser, ')', "Missing closing paren.");
						}
						node = operation ?? node2;
					}
				}
			}
			finally
			{
				parser.StrictMath = strictMath;
			}
			return node;
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000764C File Offset: 0x0000584C
		public Node Operand(Parser parser)
		{
			CharMatchResult charMatchResult = null;
			if (parser.Tokenizer.CurrentChar == '-' && parser.Tokenizer.Peek("-[@\\(]"))
			{
				charMatchResult = parser.Tokenizer.Match('-');
				this.GatherComments(parser);
			}
			Expression expression;
			if ((expression = this.Sub(parser)) == null && (expression = this.Dimension(parser)) == null)
			{
				expression = this.Color(parser) ?? this.Variable(parser);
			}
			Node node = expression;
			if (node != null)
			{
				if (!charMatchResult)
				{
					return node;
				}
				return this.NodeProvider.Operation("*", this.NodeProvider.Number("-1", "", charMatchResult.Location), node, charMatchResult.Location);
			}
			else
			{
				if (parser.Tokenizer.CurrentChar == 'u' && parser.Tokenizer.Peek("url\\("))
				{
					return null;
				}
				Node node2 = this.Call(parser);
				if (!Node.op_True(node2))
				{
					return node2 | this.Keyword(parser);
				}
				return node2;
			}
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000773C File Offset: 0x0000593C
		public Expression Expression(Parser parser)
		{
			NodeList nodeList = new NodeList();
			int index = parser.Tokenizer.Location.Index;
			for (;;)
			{
				Node node = this.UnicodeRange(parser);
				Node node2 = (node ? node : (node | this.Operation(parser)));
				Node node3 = (node2 ? node2 : (node2 | this.Entity(parser)));
				Node node4;
				if (!(node4 = (node3 ? node3 : (node3 | parser.Tokenizer.Match("[-+*/]")))))
				{
					break;
				}
				node4.PostComments = this.PullComments();
				nodeList.Add(node4);
			}
			if (nodeList.Count > 0)
			{
				return this.NodeProvider.Expression(nodeList, parser.Tokenizer.GetNodeLocation(index));
			}
			return null;
		}

		// Token: 0x0600013D RID: 317 RVA: 0x00007804 File Offset: 0x00005A04
		public string Property(Parser parser)
		{
			RegexMatchResult regexMatchResult = parser.Tokenizer.Match("\\*?-?[-_a-zA-Z][-_a-z0-9A-Z]*");
			if (regexMatchResult)
			{
				return regexMatchResult.Value;
			}
			return null;
		}

		// Token: 0x0600013E RID: 318 RVA: 0x00007832 File Offset: 0x00005A32
		public void Expect(Parser parser, char expectedString)
		{
			this.Expect(parser, expectedString, null);
		}

		// Token: 0x0600013F RID: 319 RVA: 0x00007840 File Offset: 0x00005A40
		public void Expect(Parser parser, char expectedString, string message)
		{
			if (parser.Tokenizer.Match(expectedString))
			{
				return;
			}
			message = message ?? "Expected '{0}' but found '{1}'";
			throw new ParsingException(string.Format(message, expectedString, parser.Tokenizer.NextChar), parser.Tokenizer.GetNodeLocation());
		}

		// Token: 0x06000140 RID: 320 RVA: 0x00007899 File Offset: 0x00005A99
		public T Expect<T>(T node, string message, Parser parser) where T : Node
		{
			if (node)
			{
				return node;
			}
			throw new ParsingException(message, parser.Tokenizer.GetNodeLocation());
		}

		// Token: 0x06000141 RID: 321 RVA: 0x000078BB File Offset: 0x00005ABB
		public Parsers.ParserLocation Remember(Parser parser)
		{
			return new Parsers.ParserLocation
			{
				Comments = this.CurrentComments,
				TokenizerLocation = parser.Tokenizer.Location
			};
		}

		// Token: 0x06000142 RID: 322 RVA: 0x000078DF File Offset: 0x00005ADF
		public void Recall(Parser parser, Parsers.ParserLocation location)
		{
			this.CurrentComments = location.Comments;
			parser.Tokenizer.Location = location.TokenizerLocation;
		}

		// Token: 0x04000035 RID: 53
		private Stack<NodeList> CommentsStack = new Stack<NodeList>();

		// Token: 0x04000036 RID: 54
		private static readonly ImportOptions[][] illegalOptionCombinations = new ImportOptions[][]
		{
			new ImportOptions[]
			{
				ImportOptions.Css,
				ImportOptions.Less
			},
			new ImportOptions[]
			{
				ImportOptions.Inline,
				ImportOptions.Css
			},
			new ImportOptions[]
			{
				ImportOptions.Inline,
				ImportOptions.Less
			},
			new ImportOptions[]
			{
				ImportOptions.Inline,
				ImportOptions.Reference
			},
			new ImportOptions[]
			{
				ImportOptions.Once,
				ImportOptions.Multiple
			},
			new ImportOptions[]
			{
				ImportOptions.Reference,
				ImportOptions.Css
			}
		};

		// Token: 0x020000DB RID: 219
		public class ParserLocation
		{
			// Token: 0x17000112 RID: 274
			// (get) Token: 0x06000606 RID: 1542 RVA: 0x00018C9B File Offset: 0x00016E9B
			// (set) Token: 0x06000607 RID: 1543 RVA: 0x00018CA3 File Offset: 0x00016EA3
			public NodeList Comments { get; set; }

			// Token: 0x17000113 RID: 275
			// (get) Token: 0x06000608 RID: 1544 RVA: 0x00018CAC File Offset: 0x00016EAC
			// (set) Token: 0x06000609 RID: 1545 RVA: 0x00018CB4 File Offset: 0x00016EB4
			public Location TokenizerLocation { get; set; }
		}
	}
}

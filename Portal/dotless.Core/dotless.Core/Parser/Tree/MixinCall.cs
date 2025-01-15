using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003F RID: 63
	public class MixinCall : Node
	{
		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000B6A7 File Offset: 0x000098A7
		// (set) Token: 0x06000260 RID: 608 RVA: 0x0000B6AF File Offset: 0x000098AF
		public List<NamedArgument> Arguments { get; set; }

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000B6B8 File Offset: 0x000098B8
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000B6C0 File Offset: 0x000098C0
		public Selector Selector { get; set; }

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x06000263 RID: 611 RVA: 0x0000B6C9 File Offset: 0x000098C9
		// (set) Token: 0x06000264 RID: 612 RVA: 0x0000B6D1 File Offset: 0x000098D1
		public bool Important { get; set; }

		// Token: 0x06000265 RID: 613 RVA: 0x0000B6DA File Offset: 0x000098DA
		public MixinCall(NodeList<Element> elements, List<NamedArgument> arguments, bool important)
		{
			this.Important = important;
			this.Selector = new Selector(elements);
			this.Arguments = arguments;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000B6FC File Offset: 0x000098FC
		protected override Node CloneCore()
		{
			return new MixinCall(new NodeList<Element>(this.Selector.Elements.Select((Element e) => e.Clone())), this.Arguments, this.Important);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000B750 File Offset: 0x00009950
		public override Node Evaluate(Env env)
		{
			IEnumerable<Closure> enumerable = env.FindRulesets(this.Selector);
			if (enumerable == null)
			{
				throw new ParsingException(this.Selector.ToCSS(env).Trim() + " is undefined", base.Location);
			}
			env.Rule = this;
			NodeList nodeList = new NodeList();
			if (base.PreComments)
			{
				nodeList.AddRange(base.PreComments);
			}
			List<Closure> list = enumerable.ToList<Closure>();
			List<Closure> list2 = list.Where((Closure c) => c.Ruleset is MixinDefinition).ToList<Closure>();
			List<Closure> list3 = new List<Closure>();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			foreach (Closure closure in list2)
			{
				MixinDefinition mixinDefinition = (MixinDefinition)closure.Ruleset;
				MixinMatch mixinMatch = mixinDefinition.MatchArguments(this.Arguments, env);
				if (mixinMatch != MixinMatch.ArgumentMismatch)
				{
					if (mixinMatch == MixinMatch.Default)
					{
						list3.Add(closure);
						flag3 = true;
					}
					else
					{
						flag = true;
						if (mixinMatch != MixinMatch.GuardFail)
						{
							flag2 = true;
							try
							{
								Env env2 = env.CreateChildEnvWithClosure(closure);
								nodeList.AddRange(mixinDefinition.Evaluate(this.Arguments, env2).Rules);
							}
							catch (ParsingException ex)
							{
								throw new ParsingException(ex.Message, ex.Location, base.Location);
							}
						}
					}
				}
			}
			if (!flag2 && flag3)
			{
				foreach (Closure closure2 in list3)
				{
					try
					{
						Env env3 = env.CreateChildEnvWithClosure(closure2);
						MixinDefinition mixinDefinition2 = (MixinDefinition)closure2.Ruleset;
						nodeList.AddRange(mixinDefinition2.Evaluate(this.Arguments, env3).Rules);
					}
					catch (ParsingException ex2)
					{
						throw new ParsingException(ex2.Message, ex2.Location, base.Location);
					}
				}
				flag = true;
			}
			if (!flag)
			{
				foreach (Closure closure3 in list.Except(list2))
				{
					if (closure3.Ruleset.Rules != null)
					{
						NodeList nodeList2 = (NodeList)closure3.Ruleset.Rules.Clone();
						NodeHelper.ExpandNodes<MixinCall>(env, nodeList2);
						nodeList.AddRange(nodeList2);
					}
					flag = true;
				}
			}
			if (base.PostComments)
			{
				nodeList.AddRange(base.PostComments);
			}
			env.Rule = null;
			if (!flag)
			{
				throw new ParsingException(string.Format("No matching definition was found for `{0}({1})`", this.Selector.ToCSS(env).Trim(), this.Arguments.Select((NamedArgument a) => a.Value.ToCSS(env)).JoinStrings(env.Compress ? "," : ", ")), base.Location);
			}
			nodeList.Accept(new ReferenceVisitor(base.IsReference));
			if (this.Important)
			{
				return this.MakeRulesImportant(nodeList);
			}
			return nodeList;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000BAC8 File Offset: 0x00009CC8
		public override void Accept(IVisitor visitor)
		{
			this.Selector = base.VisitAndReplace<Selector>(this.Selector, visitor);
			foreach (NamedArgument namedArgument in this.Arguments)
			{
				namedArgument.Value = base.VisitAndReplace<Expression>(namedArgument.Value, visitor);
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000BB3C File Offset: 0x00009D3C
		private Ruleset MakeRulesetImportant(Ruleset ruleset)
		{
			return new Ruleset(ruleset.Selectors, this.MakeRulesImportant(ruleset.Rules)).ReducedFrom<Ruleset>(new Node[] { ruleset });
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000BB64 File Offset: 0x00009D64
		private NodeList MakeRulesImportant(NodeList rules)
		{
			NodeList nodeList = new NodeList();
			foreach (Node node in rules)
			{
				if (node is MixinCall)
				{
					MixinCall mixinCall = (MixinCall)node;
					nodeList.Add(new MixinCall(mixinCall.Selector.Elements, new List<NamedArgument>(mixinCall.Arguments), true).ReducedFrom<MixinCall>(new Node[] { node }));
				}
				else if (node is Rule)
				{
					nodeList.Add(this.MakeRuleImportant((Rule)node));
				}
				else if (node is Ruleset)
				{
					nodeList.Add(this.MakeRulesetImportant((Ruleset)node));
				}
				else
				{
					nodeList.Add(node);
				}
			}
			return nodeList;
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000BC34 File Offset: 0x00009E34
		private Rule MakeRuleImportant(Rule rule)
		{
			Node value = rule.Value;
			Value value2 = value as Value;
			value2 = ((value2 != null) ? new Value(value2.Values, "!important").ReducedFrom<Value>(new Node[] { value2 }) : new Value(new NodeList { value }, "!important"));
			return new Rule(rule.Name, value2).ReducedFrom<Rule>(new Node[] { rule });
		}
	}
}

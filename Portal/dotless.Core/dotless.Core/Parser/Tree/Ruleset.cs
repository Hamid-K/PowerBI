using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000047 RID: 71
	public class Ruleset : Node
	{
		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CA RID: 714 RVA: 0x0000CE17 File Offset: 0x0000B017
		// (set) Token: 0x060002CB RID: 715 RVA: 0x0000CE1F File Offset: 0x0000B01F
		public NodeList<Selector> Selectors { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002CC RID: 716 RVA: 0x0000CE28 File Offset: 0x0000B028
		// (set) Token: 0x060002CD RID: 717 RVA: 0x0000CE30 File Offset: 0x0000B030
		public NodeList Rules { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002CE RID: 718 RVA: 0x0000CE39 File Offset: 0x0000B039
		// (set) Token: 0x060002CF RID: 719 RVA: 0x0000CE41 File Offset: 0x0000B041
		public bool Evaluated { get; protected set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x0000CE4A File Offset: 0x0000B04A
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x0000CE52 File Offset: 0x0000B052
		public bool IsRoot { get; set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002D2 RID: 722 RVA: 0x0000CE5B File Offset: 0x0000B05B
		// (set) Token: 0x060002D3 RID: 723 RVA: 0x0000CE63 File Offset: 0x0000B063
		public bool MultiMedia { get; set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x0000CE6C File Offset: 0x0000B06C
		// (set) Token: 0x060002D5 RID: 725 RVA: 0x0000CE74 File Offset: 0x0000B074
		public Ruleset OriginalRuleset { get; set; }

		// Token: 0x060002D6 RID: 726 RVA: 0x0000CE7D File Offset: 0x0000B07D
		public Ruleset(NodeList<Selector> selectors, NodeList rules)
			: this(selectors, rules, null)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000CE88 File Offset: 0x0000B088
		protected Ruleset(NodeList<Selector> selectors, NodeList rules, Ruleset originalRuleset)
			: this()
		{
			this.Selectors = selectors;
			this.Rules = rules;
			this.OriginalRuleset = originalRuleset ?? this;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000CEAA File Offset: 0x0000B0AA
		protected Ruleset()
		{
			this._lookups = new Dictionary<string, List<Closure>>();
			this.OriginalRuleset = this;
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000CEC4 File Offset: 0x0000B0C4
		public bool IsEqualOrClonedFrom(Node node)
		{
			Ruleset ruleset = node as Ruleset;
			return ruleset && this.IsEqualOrClonedFrom(ruleset);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000CEE9 File Offset: 0x0000B0E9
		public bool IsEqualOrClonedFrom(Ruleset ruleset)
		{
			return ruleset.OriginalRuleset == this.OriginalRuleset;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000CEFC File Offset: 0x0000B0FC
		public Rule Variable(string name, Node startNode)
		{
			Ruleset startNodeRuleset = startNode as Ruleset;
			return (from r in this.Rules.TakeWhile((Node r) => r != startNode && (startNodeRuleset == null || !startNodeRuleset.IsEqualOrClonedFrom(r))).OfType<Rule>()
				where r.Variable
				select r).Reverse<Rule>().FirstOrDefault((Rule r) => r.Name == name);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0000CF84 File Offset: 0x0000B184
		public List<Ruleset> Rulesets()
		{
			IEnumerable<Node> rules = this.Rules;
			return (rules ?? Enumerable.Empty<Node>()).OfType<Ruleset>().ToList<Ruleset>();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x0000CFAC File Offset: 0x0000B1AC
		public List<Closure> Find<TRuleset>(Env env, Selector selector, Ruleset self) where TRuleset : Ruleset
		{
			Context context = new Context();
			context.AppendSelectors(new Context(), this.Selectors ?? new NodeList<Selector>());
			Context context2 = new Context();
			context2.AppendSelectors(context, new NodeList<Selector>(new Selector[] { selector }));
			Selector selector2 = context2.Select((IEnumerable<Selector> selectors) => new Selector(selectors.SelectMany((Selector s) => s.Elements))).First<Selector>();
			return this.FindInternal(env, selector2, self, context).ToList<Closure>();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x0000D030 File Offset: 0x0000B230
		private IEnumerable<Closure> FindInternal(Env env, Selector selector, Ruleset self, Context context)
		{
			if (!selector.Elements.Any<Element>())
			{
				return Enumerable.Empty<Closure>();
			}
			string text = selector.ToCSS(env);
			if (this._lookups.ContainsKey(text))
			{
				return this._lookups[text];
			}
			self = self ?? this;
			List<Closure> list = new List<Closure>();
			Selector selector2 = (from selectors in context
				select new Selector(selectors.SelectMany((Selector s) => s.Elements)) into m
				where m.Elements.IsSubsequenceOf(selector.Elements, new Func<Element, Element, bool>(Ruleset.ElementValuesEqual))
				orderby m.Elements.Count descending
				select m).FirstOrDefault<Selector>();
			if (selector2 != null && selector2.Elements.Count == selector.Elements.Count)
			{
				list.Add(new Closure
				{
					Context = new List<Ruleset> { this },
					Ruleset = this
				});
			}
			foreach (Ruleset ruleset in this.Rulesets().Where(delegate(Ruleset rule)
			{
				if (rule != self)
				{
					return true;
				}
				MixinDefinition mixinDefinition = rule as MixinDefinition;
				return mixinDefinition != null && mixinDefinition.Condition != null;
			}))
			{
				if (ruleset.Selectors != null)
				{
					Context context2 = new Context();
					context2.AppendSelectors(context, ruleset.Selectors);
					foreach (Closure closure in ruleset.FindInternal(env, selector, self, context2))
					{
						closure.Context.Insert(0, this);
						list.Add(closure);
					}
				}
			}
			return this._lookups[text] = list;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x0000D234 File Offset: 0x0000B434
		private static bool ElementValuesEqual(Element e1, Element e2)
		{
			return (e1.Value == null && e2.Value == null) || (e1.Value != null && e2.Value != null && string.Equals(e1.Value.Trim(), e2.Value.Trim()));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x0000D280 File Offset: 0x0000B480
		public virtual MixinMatch MatchArguments(List<NamedArgument> arguments, Env env)
		{
			if (arguments != null && arguments.Count != 0)
			{
				return MixinMatch.ArgumentMismatch;
			}
			return MixinMatch.Pass;
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x0000D290 File Offset: 0x0000B490
		public override Node Evaluate(Env env)
		{
			if (this.Evaluated)
			{
				return this;
			}
			Ruleset ruleset = this.Clone().ReducedFrom<Ruleset>(new Node[] { this });
			ruleset.EvaluateRules(env);
			ruleset.Evaluated = true;
			return ruleset;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x0000D2BF File Offset: 0x0000B4BF
		private new Ruleset Clone()
		{
			return new Ruleset(new NodeList<Selector>(this.Selectors), new NodeList(this.Rules), this.OriginalRuleset)
			{
				IsReference = base.IsReference
			};
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x0000D2F0 File Offset: 0x0000B4F0
		protected override Node CloneCore()
		{
			return new Ruleset(new NodeList<Selector>(this.Selectors), new NodeList(this.Rules), this.OriginalRuleset)
			{
				Evaluated = this.Evaluated,
				IsRoot = this.IsRoot,
				MultiMedia = this.MultiMedia
			};
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x0000D342 File Offset: 0x0000B542
		public override void Accept(IVisitor visitor)
		{
			this.Selectors = base.VisitAndReplace<NodeList<Selector>>(this.Selectors, visitor);
			this.Rules = base.VisitAndReplace<NodeList>(this.Rules, visitor);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x0000D36C File Offset: 0x0000B56C
		protected void EvaluateRules(Env env)
		{
			env.Frames.Push(this);
			for (int i = 0; i < this.Selectors.Count; i++)
			{
				Node node = this.Selectors[i].Evaluate(env);
				IEnumerable<Selector> enumerable = node as IEnumerable<Selector>;
				if (enumerable != null)
				{
					this.Selectors.RemoveAt(i);
					this.Selectors.InsertRange(i, enumerable);
				}
				else
				{
					this.Selectors[i] = node as Selector;
				}
			}
			int count = env.MediaBlocks.Count;
			NodeHelper.ExpandNodes<Import>(env, this.Rules);
			NodeHelper.ExpandNodes<MixinCall>(env, this.Rules);
			foreach (Extend extend in this.Rules.OfType<Extend>().ToArray<Extend>())
			{
				foreach (Selector selector in this.Selectors)
				{
					if (env.MediaPath.Any<Media>())
					{
						env.MediaPath.Peek().AddExtension(selector, (Extend)extend.Evaluate(env), env);
					}
					else
					{
						env.AddExtension(selector, (Extend)extend.Evaluate(env), env);
					}
				}
			}
			for (int k = 0; k < this.Rules.Count; k++)
			{
				this.Rules[k] = this.Rules[k].Evaluate(env);
			}
			for (int l = count; l < env.MediaBlocks.Count; l++)
			{
				env.MediaBlocks[l].BubbleSelectors(this.Selectors);
			}
			env.Frames.Pop();
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x0000D538 File Offset: 0x0000B738
		protected Ruleset EvaluateRulesForFrame(Ruleset frame, Env context)
		{
			NodeList nodeList = new NodeList();
			foreach (Node node in this.Rules)
			{
				if (node is MixinDefinition)
				{
					MixinDefinition mixinDefinition = node as MixinDefinition;
					IEnumerable<Rule> enumerable = mixinDefinition.Params.Concat(frame.Rules.Cast<Rule>());
					nodeList.Add(new MixinDefinition(mixinDefinition.Name, new NodeList<Rule>(enumerable), mixinDefinition.Rules, mixinDefinition.Condition, mixinDefinition.Variadic));
				}
				else if (node is Import)
				{
					Node node2 = node.Evaluate(context);
					NodeList nodeList2 = node2 as NodeList;
					if (nodeList2 != null)
					{
						nodeList.AddRange(nodeList2);
					}
					else
					{
						nodeList.Add(node2);
					}
				}
				else if (node is Directive || node is Media)
				{
					nodeList.Add(node.Evaluate(context));
				}
				else if (node is Ruleset)
				{
					Ruleset ruleset = node as Ruleset;
					nodeList.Add(ruleset.Evaluate(context));
				}
				else if (node is MixinCall)
				{
					nodeList.AddRange((NodeList)node.Evaluate(context));
				}
				else
				{
					nodeList.Add(node.Evaluate(context));
				}
			}
			return new Ruleset(this.Selectors, nodeList);
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x0000D694 File Offset: 0x0000B894
		public override void AppendCSS(Env env)
		{
			if (this.Rules == null || !this.Rules.Any<Node>())
			{
				return;
			}
			((Ruleset)this.Evaluate(env)).AppendCSS(env, new Context());
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x0000D6C4 File Offset: 0x0000B8C4
		protected void AppendRules(Env env)
		{
			if (env.Compress && this.Rules.Count == 0)
			{
				return;
			}
			env.Output.Append(env.Compress ? "{" : " {\n").Push().AppendMany<Node>(this.Rules, "\n")
				.Trim()
				.Indent(env.Compress ? 0 : 2)
				.PopAndAppend();
			if (env.Compress)
			{
				env.Output.TrimRight(new char?(';'));
			}
			env.Output.Append(env.Compress ? "}" : "\n}");
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000D774 File Offset: 0x0000B974
		public virtual void AppendCSS(Env env, Context context)
		{
			List<StringBuilder> list = new List<StringBuilder>();
			int num = 0;
			Context context2 = new Context();
			if (!this.IsRoot)
			{
				if (!env.Compress && env.Debug && base.Location != null)
				{
					env.Output.Append(string.Format("/* {0}:L{1} */\n", base.Location.FileName, Zone.GetLineNumber(base.Location)));
				}
				context2.AppendSelectors(context, this.Selectors);
			}
			env.Output.Push();
			bool flag = false;
			foreach (Node node in this.Rules)
			{
				if (!node.IgnoreOutput())
				{
					Comment comment = node as Comment;
					if (comment == null || comment.IsValidCss)
					{
						Ruleset ruleset = node as Ruleset;
						if (ruleset != null)
						{
							ruleset.AppendCSS(env, context2);
							if (!ruleset.IsReference)
							{
								flag = true;
							}
						}
						else
						{
							Rule rule = node as Rule;
							if (!rule || !rule.Variable)
							{
								if (!this.IsRoot)
								{
									if (!comment)
									{
										num++;
									}
									env.Output.Push().Append(node);
									list.Add(env.Output.Pop());
								}
								else
								{
									env.Output.Append(node);
									if (!env.Compress)
									{
										env.Output.Append("\n");
									}
								}
							}
						}
					}
				}
			}
			StringBuilder stringBuilder2 = env.Output.Pop();
			if (this.AddExtenders(env, context, context2))
			{
				base.IsReference = false;
			}
			if (!base.IsReference)
			{
				if (this.IsRoot)
				{
					env.Output.AppendMany(list, env.Compress ? "" : "\n");
				}
				else if (num > 0)
				{
					context2.AppendCSS(env);
					env.Output.Append(env.Compress ? "{" : " {\n  ");
					env.Output.AppendMany(list.ConvertAll<string>((StringBuilder stringBuilder) => stringBuilder.ToString()).Distinct<string>(), env.Compress ? "" : "\n  ");
					if (env.Compress)
					{
						env.Output.TrimRight(new char?(';'));
					}
					env.Output.Append(env.Compress ? "}" : "\n}\n");
				}
			}
			if (!base.IsReference || flag)
			{
				env.Output.Append(stringBuilder2);
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x0000DA30 File Offset: 0x0000BC30
		private bool AddExtenders(Env env, Context context, Context paths)
		{
			bool flag = false;
			foreach (Selector selector in this.Selectors.Where((Selector s) => s.Elements.First<Element>().Value != null))
			{
				Context context2 = context.Clone();
				context2.AppendSelectors(context, new Selector[] { selector });
				string finalString = context2.ToCss(env);
				ExactExtender exactExtender = env.FindExactExtension(finalString);
				if (exactExtender != null)
				{
					exactExtender.IsMatched = true;
					paths.AppendSelectors(context.Clone(), exactExtender.ExtendedBy);
				}
				PartialExtender[] array = env.FindPartialExtensions(context2);
				if (array != null)
				{
					PartialExtender[] array2 = array;
					for (int i = 0; i < array2.Length; i++)
					{
						array2[i].IsMatched = true;
					}
					paths.AppendSelectors(context.Clone(), array.SelectMany((PartialExtender p) => p.Replacements(finalString)));
				}
				bool flag2;
				if (exactExtender != null)
				{
					flag2 = exactExtender.ExtendedBy.Any((Selector e) => !e.IsReference);
				}
				else
				{
					flag2 = false;
				}
				bool flag3 = flag2;
				bool flag4;
				if (array != null)
				{
					flag4 = array.Any((PartialExtender p) => p.ExtendedBy.Any((Selector e) => !e.IsReference));
				}
				else
				{
					flag4 = false;
				}
				bool flag5 = flag4;
				flag = flag || flag3 || flag5;
			}
			return flag;
		}

		// Token: 0x060002EB RID: 747 RVA: 0x0000DBC4 File Offset: 0x0000BDC4
		public override string ToString()
		{
			string text = "{0}{{{1}}}";
			if (this.Selectors == null || this.Selectors.Count <= 0)
			{
				return string.Format(text, "*", this.Rules.Count);
			}
			return string.Format(text, this.Selectors.Select((Selector s) => s.ToCSS(new Env(null))).JoinStrings(""), this.Rules.Count);
		}

		// Token: 0x040000AC RID: 172
		private Dictionary<string, List<Closure>> _lookups;
	}
}

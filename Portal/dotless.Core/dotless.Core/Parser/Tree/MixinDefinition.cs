using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000040 RID: 64
	public class MixinDefinition : Ruleset
	{
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600026C RID: 620 RVA: 0x0000BCA5 File Offset: 0x00009EA5
		// (set) Token: 0x0600026D RID: 621 RVA: 0x0000BCAD File Offset: 0x00009EAD
		public string Name { get; set; }

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000BCB6 File Offset: 0x00009EB6
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000BCBE File Offset: 0x00009EBE
		public NodeList<Rule> Params { get; set; }

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000270 RID: 624 RVA: 0x0000BCC7 File Offset: 0x00009EC7
		// (set) Token: 0x06000271 RID: 625 RVA: 0x0000BCCF File Offset: 0x00009ECF
		public Condition Condition { get; set; }

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000272 RID: 626 RVA: 0x0000BCD8 File Offset: 0x00009ED8
		// (set) Token: 0x06000273 RID: 627 RVA: 0x0000BCE0 File Offset: 0x00009EE0
		public bool Variadic { get; set; }

		// Token: 0x06000274 RID: 628 RVA: 0x0000BCEC File Offset: 0x00009EEC
		public MixinDefinition(string name, NodeList<Rule> parameters, NodeList rules, Condition condition, bool variadic)
		{
			this.Name = name;
			this.Params = parameters;
			base.Rules = rules;
			this.Condition = condition;
			this.Variadic = variadic;
			base.Selectors = new NodeList<Selector>
			{
				new Selector(new NodeList<Element>(new Element[]
				{
					new Element(null, name)
				}))
			};
			this._arity = this.Params.Count;
			this._required = this.Params.Count((Rule r) => string.IsNullOrEmpty(r.Name) || r.Value == null);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000BD92 File Offset: 0x00009F92
		public override Node Evaluate(Env env)
		{
			return this;
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000BD98 File Offset: 0x00009F98
		public Ruleset EvaluateParams(Env env, List<NamedArgument> args)
		{
			Dictionary<string, Node> dictionary = new Dictionary<string, Node>();
			args = args ?? new List<NamedArgument>();
			bool flag = false;
			foreach (NamedArgument namedArgument in args)
			{
				if (!string.IsNullOrEmpty(namedArgument.Name))
				{
					flag = true;
					dictionary[namedArgument.Name] = new Rule(namedArgument.Name, namedArgument.Value.Evaluate(env))
					{
						Location = namedArgument.Value.Location
					};
				}
				else if (flag)
				{
					throw new ParsingException("Positional arguments must appear before all named arguments.", namedArgument.Value.Location);
				}
			}
			for (int i = 0; i < this.Params.Count; i++)
			{
				if (!string.IsNullOrEmpty(this.Params[i].Name) && !dictionary.ContainsKey(this.Params[i].Name))
				{
					Node node;
					if (i < args.Count && string.IsNullOrEmpty(args[i].Name))
					{
						node = args[i].Value;
					}
					else
					{
						node = this.Params[i].Value;
					}
					if (!node)
					{
						throw new ParsingException(string.Format("wrong number of arguments for {0} ({1} for {2})", this.Name, (args != null) ? args.Count : 0, this._arity), base.Location);
					}
					Node node2;
					if (this.Params[i].Variadic)
					{
						NodeList nodeList = new NodeList();
						for (int j = i; j < args.Count; j++)
						{
							nodeList.Add(args[j].Value.Evaluate(env));
						}
						node2 = new Expression(nodeList).Evaluate(env);
					}
					else
					{
						node2 = node.Evaluate(env);
					}
					dictionary[this.Params[i].Name] = new Rule(this.Params[i].Name, node2)
					{
						Location = node.Location
					};
				}
			}
			List<Node> list = new List<Node>();
			for (int k = 0; k < Math.Max(this.Params.Count, args.Count); k++)
			{
				list.Add((k < args.Count) ? args[k].Value : this.Params[k].Value);
			}
			Ruleset ruleset = new Ruleset(new NodeList<Selector>(), new NodeList());
			ruleset.Rules.Insert(0, new Rule("@arguments", new Expression(list.Where((Node a) => a != null)).Evaluate(env)));
			foreach (KeyValuePair<string, Node> keyValuePair in dictionary)
			{
				ruleset.Rules.Add(keyValuePair.Value);
			}
			return ruleset;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000C0DC File Offset: 0x0000A2DC
		[Obsolete("This method will be removed in a future release. Use Evaluate(List<NamedArgument>, Env) instead.", false)]
		public Ruleset Evaluate(List<NamedArgument> args, Env env, List<Ruleset> closureFrames)
		{
			Env env2 = env.CreateChildEnvWithClosure(new Closure
			{
				Context = closureFrames,
				Ruleset = this
			});
			return this.Evaluate(args, env2);
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000C10C File Offset: 0x0000A30C
		public Ruleset Evaluate(List<NamedArgument> args, Env env)
		{
			Ruleset ruleset = this.EvaluateParams(env, args);
			Env env2 = env.CreateChildEnv();
			env2.Frames.Push(this);
			env2.Frames.Push(ruleset);
			Ruleset ruleset2 = base.EvaluateRulesForFrame(ruleset, env2);
			env2.Frames.Pop();
			env2.Frames.Pop();
			return ruleset2;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000C164 File Offset: 0x0000A364
		public override MixinMatch MatchArguments(List<NamedArgument> arguments, Env env)
		{
			int num = ((arguments != null) ? arguments.Count : 0);
			if (!this.Variadic)
			{
				if (num < this._required)
				{
					return MixinMatch.ArgumentMismatch;
				}
				if (num > this._arity)
				{
					return MixinMatch.ArgumentMismatch;
				}
			}
			if (this.Condition)
			{
				env.Frames.Push(this.EvaluateParams(env, arguments));
				bool flag = this.Condition.Passes(env);
				env.Frames.Pop();
				if (this.Condition.IsDefault)
				{
					return MixinMatch.Default;
				}
				if (!flag)
				{
					return MixinMatch.GuardFail;
				}
			}
			for (int i = 0; i < Math.Min(num, this._arity); i++)
			{
				if (string.IsNullOrEmpty(this.Params[i].Name) && arguments[i].Value.Evaluate(env).ToCSS(env) != this.Params[i].Value.Evaluate(env).ToCSS(env))
				{
					return MixinMatch.ArgumentMismatch;
				}
			}
			return MixinMatch.Pass;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000C257 File Offset: 0x0000A457
		public override void Accept(IVisitor visitor)
		{
			base.Accept(visitor);
			this.Params = base.VisitAndReplace<NodeList<Rule>>(this.Params, visitor);
			this.Condition = base.VisitAndReplace<Condition>(this.Condition, visitor, true);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000C287 File Offset: 0x0000A487
		public override void AppendCSS(Env env, Context context)
		{
		}

		// Token: 0x0400008E RID: 142
		private int _required;

		// Token: 0x0400008F RID: 143
		private int _arity;
	}
}

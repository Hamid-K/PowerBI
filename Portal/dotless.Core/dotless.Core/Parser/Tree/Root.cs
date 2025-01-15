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
	// Token: 0x02000045 RID: 69
	public class Root : Ruleset
	{
		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000C931 File Offset: 0x0000AB31
		// (set) Token: 0x060002AE RID: 686 RVA: 0x0000C939 File Offset: 0x0000AB39
		public Func<ParsingException, ParserException> Error { get; set; }

		// Token: 0x060002AF RID: 687 RVA: 0x0000C942 File Offset: 0x0000AB42
		public Root(NodeList rules, Func<ParsingException, ParserException> error)
			: this(rules, error, null)
		{
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000C94D File Offset: 0x0000AB4D
		protected Root(NodeList rules, Func<ParsingException, ParserException> error, Ruleset master)
			: base(new NodeList<Selector>(), rules, master)
		{
			this.Error = error;
			base.IsRoot = true;
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x0000C96C File Offset: 0x0000AB6C
		public override void AppendCSS(Env env)
		{
			try
			{
				if (base.Rules != null && base.Rules.Any<Node>())
				{
					Root root = (Root)this.Evaluate(env);
					root.Rules.InsertRange(0, root.CollectImports().Cast<Node>());
					root.AppendCSS(env, new Context());
				}
			}
			catch (ParsingException ex)
			{
				throw this.Error(ex);
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x0000C9E0 File Offset: 0x0000ABE0
		private IList<Import> CollectImports()
		{
			List<Import> list = base.Rules.OfType<Import>().ToList<Import>();
			foreach (Import import in list)
			{
				base.Rules.Remove(import);
			}
			return list;
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0000CA48 File Offset: 0x0000AC48
		private Root DoVisiting(Root node, Env env, VisitorPluginType pluginType)
		{
			return env.VisitorPlugins.Where((IVisitorPlugin p) => p.AppliesTo == pluginType).Aggregate(node, delegate(Root current, IVisitorPlugin plugin)
			{
				Root root2;
				try
				{
					plugin.OnPreVisiting(env);
					Root root = plugin.Apply(current);
					plugin.OnPostVisiting(env);
					root2 = root;
				}
				catch (Exception ex)
				{
					throw new ParserException(string.Format("Plugin '{0}' failed during visiting with error '{1}'", plugin.GetName(), ex.Message), ex);
				}
				return root2;
			});
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0000CA98 File Offset: 0x0000AC98
		public override Node Evaluate(Env env)
		{
			if (base.Evaluated)
			{
				return this;
			}
			Node node;
			try
			{
				env = env ?? new Env();
				env.Frames.Push(this);
				NodeHelper.ExpandNodes<Import>(env, base.Rules);
				env.Frames.Pop();
				Root root = new Root(new NodeList(base.Rules), this.Error, base.OriginalRuleset);
				root = this.DoVisiting(root, env, VisitorPluginType.BeforeEvaluation);
				root.ReducedFrom<Root>(new Node[] { this });
				root.EvaluateRules(env);
				root.Evaluated = true;
				root = this.DoVisiting(root, env, VisitorPluginType.AfterEvaluation);
				node = root;
			}
			catch (ParsingException ex)
			{
				throw this.Error(ex);
			}
			return node;
		}
	}
}

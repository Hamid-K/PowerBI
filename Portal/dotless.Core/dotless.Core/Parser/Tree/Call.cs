using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Functions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002D RID: 45
	public class Call : Node
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000195 RID: 405 RVA: 0x0000877C File Offset: 0x0000697C
		// (set) Token: 0x06000196 RID: 406 RVA: 0x00008784 File Offset: 0x00006984
		public string Name { get; set; }

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000878D File Offset: 0x0000698D
		// (set) Token: 0x06000198 RID: 408 RVA: 0x00008795 File Offset: 0x00006995
		public NodeList<Node> Arguments { get; set; }

		// Token: 0x06000199 RID: 409 RVA: 0x0000879E File Offset: 0x0000699E
		public Call(string name, NodeList<Node> arguments)
		{
			this.Name = name;
			this.Arguments = arguments;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000087B4 File Offset: 0x000069B4
		protected Call()
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000087BC File Offset: 0x000069BC
		protected override Node CloneCore()
		{
			return new Call(this.Name, (NodeList<Node>)this.Arguments.Clone());
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000087DC File Offset: 0x000069DC
		public override Node Evaluate(Env env)
		{
			if (env == null)
			{
				throw new ArgumentNullException("env");
			}
			IEnumerable<Node> enumerable = this.Arguments.Select((Node a) => a.Evaluate(env));
			Function function = env.GetFunction(this.Name);
			if (function != null)
			{
				function.Name = this.Name;
				function.Location = base.Location;
				return function.Call(env, enumerable).ReducedFrom<Node>(new Node[] { this });
			}
			env.Output.Push();
			env.Output.Append(this.Name).Append("(").AppendMany<Node>(enumerable, env.Compress ? "," : ", ")
				.Append(")");
			return new TextNode(env.Output.Pop().ToString()).ReducedFrom<Node>(new Node[] { this });
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000088EF File Offset: 0x00006AEF
		public override void Accept(IVisitor visitor)
		{
			this.Arguments = base.VisitAndReplace<NodeList<Node>>(this.Arguments, visitor);
		}
	}
}

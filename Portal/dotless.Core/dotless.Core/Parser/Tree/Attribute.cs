using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002C RID: 44
	public class Attribute : Node
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00008694 File Offset: 0x00006894
		// (set) Token: 0x0600018D RID: 397 RVA: 0x0000869C File Offset: 0x0000689C
		public Node Name { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000086A5 File Offset: 0x000068A5
		// (set) Token: 0x0600018F RID: 399 RVA: 0x000086AD File Offset: 0x000068AD
		public Node Op { get; set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000190 RID: 400 RVA: 0x000086B6 File Offset: 0x000068B6
		// (set) Token: 0x06000191 RID: 401 RVA: 0x000086BE File Offset: 0x000068BE
		public Node Value { get; set; }

		// Token: 0x06000192 RID: 402 RVA: 0x000086C7 File Offset: 0x000068C7
		public Attribute(Node name, Node op, Node value)
		{
			this.Name = name;
			this.Op = op;
			this.Value = value;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000086E4 File Offset: 0x000068E4
		protected override Node CloneCore()
		{
			return new Attribute(this.Name.Clone(), this.Op.Clone(), this.Value.Clone());
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000870C File Offset: 0x0000690C
		public override Node Evaluate(Env env)
		{
			return new TextNode(string.Format("[{0}{1}{2}]", this.Name.Evaluate(env).ToCSS(env), (this.Op == null) ? "" : this.Op.Evaluate(env).ToCSS(env), (this.Value == null) ? "" : this.Value.Evaluate(env).ToCSS(env)));
		}
	}
}

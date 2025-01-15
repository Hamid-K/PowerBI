using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002B RID: 43
	public class Assignment : Node
	{
		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000183 RID: 387 RVA: 0x000085E1 File Offset: 0x000067E1
		// (set) Token: 0x06000184 RID: 388 RVA: 0x000085E9 File Offset: 0x000067E9
		public string Key { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000185 RID: 389 RVA: 0x000085F2 File Offset: 0x000067F2
		// (set) Token: 0x06000186 RID: 390 RVA: 0x000085FA File Offset: 0x000067FA
		public Node Value { get; set; }

		// Token: 0x06000187 RID: 391 RVA: 0x00008603 File Offset: 0x00006803
		public Assignment(string key, Node value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008619 File Offset: 0x00006819
		public override Node Evaluate(Env env)
		{
			return new Assignment(this.Key, this.Value.Evaluate(env))
			{
				Location = base.Location
			};
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000863E File Offset: 0x0000683E
		protected override Node CloneCore()
		{
			return new Assignment(this.Key, this.Value.Clone());
		}

		// Token: 0x0600018A RID: 394 RVA: 0x00008656 File Offset: 0x00006856
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.Key).Append("=").Append(this.Value);
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000867F File Offset: 0x0000687F
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}
	}
}

using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000032 RID: 50
	public class CssFunction : Node
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001E4 RID: 484 RVA: 0x00009FB3 File Offset: 0x000081B3
		// (set) Token: 0x060001E5 RID: 485 RVA: 0x00009FBB File Offset: 0x000081BB
		public string Name { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x00009FC4 File Offset: 0x000081C4
		// (set) Token: 0x060001E7 RID: 487 RVA: 0x00009FCC File Offset: 0x000081CC
		public Node Value { get; set; }

		// Token: 0x060001E8 RID: 488 RVA: 0x00009FD5 File Offset: 0x000081D5
		protected override Node CloneCore()
		{
			return new CssFunction
			{
				Name = this.Name,
				Value = this.Value.Clone()
			};
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00009FF9 File Offset: 0x000081F9
		public override void AppendCSS(Env env)
		{
			env.Output.Append(string.Format("{0}({1})", this.Name, this.Value.ToCSS(env)));
		}

		// Token: 0x060001EA RID: 490 RVA: 0x0000A023 File Offset: 0x00008223
		public override Node Evaluate(Env env)
		{
			CssFunction cssFunction = (CssFunction)this.Clone();
			cssFunction.Value = this.Value.Evaluate(env);
			return cssFunction;
		}
	}
}

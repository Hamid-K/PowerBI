using System;
using System.Collections.Generic;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000037 RID: 55
	public class Extend : Node
	{
		// Token: 0x06000215 RID: 533 RVA: 0x0000A677 File Offset: 0x00008877
		public Extend(List<Selector> exact, List<Selector> partial)
		{
			this.Exact = exact;
			this.Partial = partial;
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000216 RID: 534 RVA: 0x0000A68D File Offset: 0x0000888D
		// (set) Token: 0x06000217 RID: 535 RVA: 0x0000A695 File Offset: 0x00008895
		public List<Selector> Exact { get; set; }

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000218 RID: 536 RVA: 0x0000A69E File Offset: 0x0000889E
		// (set) Token: 0x06000219 RID: 537 RVA: 0x0000A6A6 File Offset: 0x000088A6
		public List<Selector> Partial { get; set; }

		// Token: 0x0600021A RID: 538 RVA: 0x0000A6B0 File Offset: 0x000088B0
		public override Node Evaluate(Env env)
		{
			List<Selector> list = new List<Selector>();
			foreach (Selector selector in this.Exact)
			{
				Env env2 = env.CreateChildEnv();
				selector.AppendCSS(env2);
				list.Add(new Selector(new Element[]
				{
					new Element(selector.Elements.First<Element>().Combinator, env2.Output.ToString().Trim())
				})
				{
					IsReference = base.IsReference
				});
			}
			List<Selector> list2 = new List<Selector>();
			foreach (Selector selector2 in this.Partial)
			{
				Env env3 = env.CreateChildEnv();
				selector2.AppendCSS(env3);
				list2.Add(new Selector(new Element[]
				{
					new Element(selector2.Elements.First<Element>().Combinator, env3.Output.ToString().Trim())
				})
				{
					IsReference = base.IsReference
				});
			}
			return new Extend(list, list2)
			{
				IsReference = base.IsReference,
				Location = base.Location
			};
		}

		// Token: 0x0600021B RID: 539 RVA: 0x0000A81C File Offset: 0x00008A1C
		protected override Node CloneCore()
		{
			return new Extend(this.Exact.Select((Selector e) => (Selector)e.Clone()).ToList<Selector>(), this.Partial.Select((Selector e) => (Selector)e.Clone()).ToList<Selector>());
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000A88C File Offset: 0x00008A8C
		public override void AppendCSS(Env env)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000A88E File Offset: 0x00008A8E
		public override bool IgnoreOutput()
		{
			return true;
		}
	}
}

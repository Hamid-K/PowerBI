using System;
using System.Linq;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000033 RID: 51
	public class CssFunctionList : NodeList
	{
		// Token: 0x060001EC RID: 492 RVA: 0x0000A04A File Offset: 0x0000824A
		public override void AppendCSS(Env env)
		{
			env.Output.AppendMany<Node>(this.Inner, " ");
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000A063 File Offset: 0x00008263
		protected override Node CloneCore()
		{
			return new CssFunctionList();
		}

		// Token: 0x060001EE RID: 494 RVA: 0x0000A06C File Offset: 0x0000826C
		public override Node Evaluate(Env env)
		{
			CssFunctionList cssFunctionList = (CssFunctionList)this.Clone();
			cssFunctionList.Inner = this.Inner.Select((Node i) => i.Evaluate(env)).ToList<Node>();
			return cssFunctionList;
		}
	}
}

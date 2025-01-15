using System;

namespace Microsoft.ProgramSynthesis.Rules
{
	// Token: 0x02000387 RID: 903
	public class FunctionalSymbol1 : IFunctionalSymbol1
	{
		// Token: 0x0600141D RID: 5149 RVA: 0x0003AD8C File Offset: 0x00038F8C
		public FunctionalSymbol1(Func<object, object> function)
		{
			this.Function = function;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600141E RID: 5150 RVA: 0x0003AD9B File Offset: 0x00038F9B
		// (set) Token: 0x0600141F RID: 5151 RVA: 0x0003ADA3 File Offset: 0x00038FA3
		public Func<object, object> Function { get; set; }

		// Token: 0x06001420 RID: 5152 RVA: 0x0003ADAC File Offset: 0x00038FAC
		public object Evaluate(object arg)
		{
			return this.Function(arg);
		}
	}
}

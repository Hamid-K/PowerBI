using System;

namespace Microsoft.ProgramSynthesis.AST
{
	// Token: 0x020008C9 RID: 2249
	internal class CustomBehaviorHole : Hole
	{
		// Token: 0x06003057 RID: 12375 RVA: 0x0008E6E4 File Offset: 0x0008C8E4
		public CustomBehaviorHole(Symbol symbol, Func<State, object> behavior)
			: base(symbol, null)
		{
			this._behavior = behavior;
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x0008E6F5 File Offset: 0x0008C8F5
		protected override object Evaluate(State state)
		{
			return this._behavior(state);
		}

		// Token: 0x06003059 RID: 12377 RVA: 0x0008E703 File Offset: 0x0008C903
		public override ProgramNode Clone()
		{
			return new CustomBehaviorHole(this.Symbol, this._behavior);
		}

		// Token: 0x04001855 RID: 6229
		private readonly Func<State, object> _behavior;
	}
}

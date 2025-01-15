using System;

namespace Microsoft.ProgramSynthesis.Learning.Strategies.Deductive
{
	// Token: 0x0200071B RID: 1819
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
	public sealed class TacticAttribute : Attribute
	{
		// Token: 0x06002767 RID: 10087 RVA: 0x0006FA99 File Offset: 0x0006DC99
		public TacticAttribute(string symbol)
		{
			this.Symbol = symbol;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x0006FAA8 File Offset: 0x0006DCA8
		public string Symbol { get; }
	}
}

using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012D5 RID: 4821
	public class DelayedValue : IValueReference
	{
		// Token: 0x06007F3E RID: 32574 RVA: 0x001B3DFA File Offset: 0x001B1FFA
		public DelayedValue(Func<Value> valueProvider)
		{
			this.valueProvider = valueProvider;
		}

		// Token: 0x17002272 RID: 8818
		// (get) Token: 0x06007F3F RID: 32575 RVA: 0x001B3E09 File Offset: 0x001B2009
		public bool Evaluated
		{
			get
			{
				return this.evaluated;
			}
		}

		// Token: 0x17002273 RID: 8819
		// (get) Token: 0x06007F40 RID: 32576 RVA: 0x001B3E11 File Offset: 0x001B2011
		public Value Value
		{
			get
			{
				if (!this.evaluated)
				{
					this.value = this.valueProvider();
					this.valueProvider = null;
					this.evaluated = true;
				}
				return this.value;
			}
		}

		// Token: 0x040045A5 RID: 17829
		private Value value;

		// Token: 0x040045A6 RID: 17830
		private Func<Value> valueProvider;

		// Token: 0x040045A7 RID: 17831
		private bool evaluated;
	}
}

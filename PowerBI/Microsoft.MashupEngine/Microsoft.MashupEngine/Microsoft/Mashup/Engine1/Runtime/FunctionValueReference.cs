using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001329 RID: 4905
	internal class FunctionValueReference : IValueReference
	{
		// Token: 0x060081A8 RID: 33192 RVA: 0x001B88E9 File Offset: 0x001B6AE9
		public FunctionValueReference(FunctionValue function)
		{
			this.function = function;
		}

		// Token: 0x17002312 RID: 8978
		// (get) Token: 0x060081A9 RID: 33193 RVA: 0x001B88F8 File Offset: 0x001B6AF8
		public bool Evaluated
		{
			get
			{
				return this.value != null;
			}
		}

		// Token: 0x17002313 RID: 8979
		// (get) Token: 0x060081AA RID: 33194 RVA: 0x001B8903 File Offset: 0x001B6B03
		public Value Value
		{
			get
			{
				if (this.value == null)
				{
					this.value = this.function.Invoke();
					this.function = null;
				}
				return this.value;
			}
		}

		// Token: 0x04004694 RID: 18068
		private FunctionValue function;

		// Token: 0x04004695 RID: 18069
		private Value value;
	}
}

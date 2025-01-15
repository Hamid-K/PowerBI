using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200167A RID: 5754
	internal class TransformValueReference : IValueReference
	{
		// Token: 0x06009198 RID: 37272 RVA: 0x001E3B66 File Offset: 0x001E1D66
		public TransformValueReference(IValueReference reference, FunctionValue transform)
		{
			this.reference = reference;
			this.transform = transform;
		}

		// Token: 0x17002614 RID: 9748
		// (get) Token: 0x06009199 RID: 37273 RVA: 0x001E3B7C File Offset: 0x001E1D7C
		public bool Evaluated
		{
			get
			{
				return this.transform == null;
			}
		}

		// Token: 0x17002615 RID: 9749
		// (get) Token: 0x0600919A RID: 37274 RVA: 0x001E3B87 File Offset: 0x001E1D87
		public Value Value
		{
			get
			{
				if (this.transform != null)
				{
					this.reference = this.transform.Invoke(this.reference.Value);
					this.transform = null;
				}
				return this.reference.Value;
			}
		}

		// Token: 0x04004E2F RID: 20015
		private IValueReference reference;

		// Token: 0x04004E30 RID: 20016
		private FunctionValue transform;
	}
}

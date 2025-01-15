using System;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012E3 RID: 4835
	internal abstract class ElementAccessValueReference : IValueReference
	{
		// Token: 0x0600801B RID: 32795 RVA: 0x001B5224 File Offset: 0x001B3424
		public ElementAccessValueReference(IValueReference reference, int index)
		{
			this.reference = reference;
			this.index = index;
		}

		// Token: 0x170022BD RID: 8893
		// (get) Token: 0x0600801C RID: 32796 RVA: 0x001B523A File Offset: 0x001B343A
		public bool Evaluated
		{
			get
			{
				return this.index == -1;
			}
		}

		// Token: 0x170022BE RID: 8894
		// (get) Token: 0x0600801D RID: 32797 RVA: 0x001B5245 File Offset: 0x001B3445
		public Value Value
		{
			get
			{
				if (this.index != -1)
				{
					this.reference = this.GetValue();
					this.index = -1;
				}
				return this.reference.Value;
			}
		}

		// Token: 0x0600801E RID: 32798
		protected abstract Value GetValue();

		// Token: 0x040045C8 RID: 17864
		protected IValueReference reference;

		// Token: 0x040045C9 RID: 17865
		protected int index;
	}
}

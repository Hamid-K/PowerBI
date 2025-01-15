using System;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200026F RID: 623
	public abstract class BaseBuilder<TObject, TParent>
	{
		// Token: 0x06001300 RID: 4864 RVA: 0x00022284 File Offset: 0x00020484
		protected BaseBuilder(TParent parent)
		{
			this.Parent = parent;
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001301 RID: 4865 RVA: 0x00022293 File Offset: 0x00020493
		public virtual TParent Parent { get; }

		// Token: 0x06001302 RID: 4866
		public abstract TObject Build();
	}
}

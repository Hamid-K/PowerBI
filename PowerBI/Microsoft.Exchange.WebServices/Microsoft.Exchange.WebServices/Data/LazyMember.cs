using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000DD RID: 221
	internal class LazyMember<T>
	{
		// Token: 0x06000B20 RID: 2848 RVA: 0x00024ADE File Offset: 0x00023ADE
		public LazyMember(InitializeLazyMember<T> initializationDelegate)
		{
			this.initializationDelegate = initializationDelegate;
		}

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000B21 RID: 2849 RVA: 0x00024AF8 File Offset: 0x00023AF8
		public T Member
		{
			get
			{
				if (!this.initialized)
				{
					lock (this.lockObject)
					{
						if (!this.initialized)
						{
							this.lazyMember = this.initializationDelegate();
						}
						this.initialized = true;
					}
				}
				return this.lazyMember;
			}
		}

		// Token: 0x04000351 RID: 849
		private T lazyMember;

		// Token: 0x04000352 RID: 850
		private InitializeLazyMember<T> initializationDelegate;

		// Token: 0x04000353 RID: 851
		private object lockObject = new object();

		// Token: 0x04000354 RID: 852
		private bool initialized;
	}
}

using System;

namespace Microsoft.Data.OData
{
	// Token: 0x0200014F RID: 335
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x060008E5 RID: 2277 RVA: 0x0001C3DF File Offset: 0x0001A5DF
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x060008E6 RID: 2278 RVA: 0x0001C3E9 File Offset: 0x0001A5E9
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060008E7 RID: 2279 RVA: 0x0001C410 File Offset: 0x0001A610
		internal T Value
		{
			get
			{
				if (!this.valueCreated)
				{
					if (this.mutex != null)
					{
						lock (this.mutex)
						{
							if (!this.valueCreated)
							{
								this.CreateValue();
							}
							goto IL_003A;
						}
					}
					this.CreateValue();
				}
				IL_003A:
				return this.value;
			}
		}

		// Token: 0x060008E8 RID: 2280 RVA: 0x0001C470 File Offset: 0x0001A670
		private void CreateValue()
		{
			this.value = this.factory.Invoke();
			this.valueCreated = true;
		}

		// Token: 0x04000360 RID: 864
		private readonly object mutex;

		// Token: 0x04000361 RID: 865
		private readonly Func<T> factory;

		// Token: 0x04000362 RID: 866
		private T value;

		// Token: 0x04000363 RID: 867
		private bool valueCreated;
	}
}

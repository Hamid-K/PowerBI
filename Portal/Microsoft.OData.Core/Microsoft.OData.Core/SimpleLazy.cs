using System;

namespace Microsoft.OData
{
	// Token: 0x020000CF RID: 207
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x06000993 RID: 2451 RVA: 0x00018354 File Offset: 0x00016554
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x06000994 RID: 2452 RVA: 0x0001835E File Offset: 0x0001655E
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000995 RID: 2453 RVA: 0x00018384 File Offset: 0x00016584
		internal T Value
		{
			get
			{
				if (!this.valueCreated)
				{
					if (this.mutex != null)
					{
						object obj = this.mutex;
						lock (obj)
						{
							if (!this.valueCreated)
							{
								this.CreateValue();
							}
							goto IL_0041;
						}
					}
					this.CreateValue();
				}
				IL_0041:
				return this.value;
			}
		}

		// Token: 0x06000996 RID: 2454 RVA: 0x000183E8 File Offset: 0x000165E8
		private void CreateValue()
		{
			this.value = this.factory();
			this.valueCreated = true;
		}

		// Token: 0x04000359 RID: 857
		private readonly object mutex;

		// Token: 0x0400035A RID: 858
		private readonly Func<T> factory;

		// Token: 0x0400035B RID: 859
		private T value;

		// Token: 0x0400035C RID: 860
		private bool valueCreated;
	}
}

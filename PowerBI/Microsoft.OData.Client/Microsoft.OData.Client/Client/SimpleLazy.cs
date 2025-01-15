using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200000C RID: 12
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x000034DF File Offset: 0x000016DF
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000034E9 File Offset: 0x000016E9
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003510 File Offset: 0x00001710
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

		// Token: 0x06000053 RID: 83 RVA: 0x00003574 File Offset: 0x00001774
		private void CreateValue()
		{
			this.value = this.factory();
			this.valueCreated = true;
		}

		// Token: 0x04000021 RID: 33
		private readonly object mutex;

		// Token: 0x04000022 RID: 34
		private readonly Func<T> factory;

		// Token: 0x04000023 RID: 35
		private T value;

		// Token: 0x04000024 RID: 36
		private bool valueCreated;
	}
}

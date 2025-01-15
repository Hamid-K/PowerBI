using System;

namespace Microsoft.OData.Core
{
	// Token: 0x020001B9 RID: 441
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x0600105F RID: 4191 RVA: 0x00039402 File Offset: 0x00037602
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x0003940C File Offset: 0x0003760C
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x17000388 RID: 904
		// (get) Token: 0x06001061 RID: 4193 RVA: 0x00039430 File Offset: 0x00037630
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

		// Token: 0x06001062 RID: 4194 RVA: 0x00039490 File Offset: 0x00037690
		private void CreateValue()
		{
			this.value = this.factory.Invoke();
			this.valueCreated = true;
		}

		// Token: 0x04000767 RID: 1895
		private readonly object mutex;

		// Token: 0x04000768 RID: 1896
		private readonly Func<T> factory;

		// Token: 0x04000769 RID: 1897
		private T value;

		// Token: 0x0400076A RID: 1898
		private bool valueCreated;
	}
}

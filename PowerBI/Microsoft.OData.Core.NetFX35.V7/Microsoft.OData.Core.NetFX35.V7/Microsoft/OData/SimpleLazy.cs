using System;

namespace Microsoft.OData
{
	// Token: 0x020000AF RID: 175
	internal sealed class SimpleLazy<T>
	{
		// Token: 0x060006CB RID: 1739 RVA: 0x000134C7 File Offset: 0x000116C7
		internal SimpleLazy(Func<T> factory)
			: this(factory, false)
		{
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000134D1 File Offset: 0x000116D1
		internal SimpleLazy(Func<T> factory, bool isThreadSafe)
		{
			this.factory = factory;
			this.valueCreated = false;
			if (isThreadSafe)
			{
				this.mutex = new object();
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000134F8 File Offset: 0x000116F8
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
							goto IL_003A;
						}
					}
					this.CreateValue();
				}
				IL_003A:
				return this.value;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00013558 File Offset: 0x00011758
		private void CreateValue()
		{
			this.value = this.factory.Invoke();
			this.valueCreated = true;
		}

		// Token: 0x040002F5 RID: 757
		private readonly object mutex;

		// Token: 0x040002F6 RID: 758
		private readonly Func<T> factory;

		// Token: 0x040002F7 RID: 759
		private T value;

		// Token: 0x040002F8 RID: 760
		private bool valueCreated;
	}
}

using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000247 RID: 583
	internal sealed class Resource<TValue>
	{
		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000EEC RID: 3820 RVA: 0x000336B3 File Offset: 0x000318B3
		// (set) Token: 0x06000EED RID: 3821 RVA: 0x000336BB File Offset: 0x000318BB
		public TValue ResourceValue { get; private set; }

		// Token: 0x06000EEE RID: 3822 RVA: 0x000336C4 File Offset: 0x000318C4
		public Resource(TValue val)
		{
			this.ResourceValue = val;
			this.m_refernceCount = 1;
			this.m_isValid = true;
			this.m_isDestroyed = false;
			this.m_locker = new object();
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000336F4 File Offset: 0x000318F4
		public int IncRefernce()
		{
			object locker = this.m_locker;
			int num;
			lock (locker)
			{
				num = this.m_refernceCount + 1;
				this.m_refernceCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x00033744 File Offset: 0x00031944
		public int DecRefernce()
		{
			object locker = this.m_locker;
			int num;
			lock (locker)
			{
				num = this.m_refernceCount - 1;
				this.m_refernceCount = num;
				num = num;
			}
			return num;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x00033794 File Offset: 0x00031994
		public void Invalidate()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				this.m_isValid = false;
			}
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000337D8 File Offset: 0x000319D8
		public bool TryDestroy(ResourceOptions options)
		{
			object locker = this.m_locker;
			bool flag2;
			lock (locker)
			{
				if ((options.HasFlag(ResourceOptions.ForceRemove) || (!this.m_isValid && this.m_refernceCount == 0)) && !this.m_isDestroyed)
				{
					this.m_isDestroyed = true;
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x040005B5 RID: 1461
		private int m_refernceCount;

		// Token: 0x040005B6 RID: 1462
		public bool m_isValid;

		// Token: 0x040005B7 RID: 1463
		public bool m_isDestroyed;

		// Token: 0x040005B8 RID: 1464
		private object m_locker;
	}
}

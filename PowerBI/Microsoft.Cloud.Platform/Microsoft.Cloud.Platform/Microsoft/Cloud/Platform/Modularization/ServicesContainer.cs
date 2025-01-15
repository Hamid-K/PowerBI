using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D6 RID: 214
	internal class ServicesContainer
	{
		// Token: 0x0600060C RID: 1548 RVA: 0x0001559A File Offset: 0x0001379A
		internal ServicesContainer()
		{
			this.m_services = new Dictionary<Type, Pair<BlockServiceTicket, object>>();
			this.m_active = true;
			this.m_locker = new object();
		}

		// Token: 0x0600060D RID: 1549 RVA: 0x000155C0 File Offset: 0x000137C0
		internal bool ContainsService(Type serviceType)
		{
			object locker = this.m_locker;
			bool flag2;
			lock (locker)
			{
				flag2 = this.m_services.ContainsKey(serviceType);
			}
			return flag2;
		}

		// Token: 0x0600060E RID: 1550 RVA: 0x00015608 File Offset: 0x00013808
		internal bool AddService(Type serviceType, BlockServiceTicket ticket, object implementation)
		{
			object locker = this.m_locker;
			bool flag2;
			lock (locker)
			{
				if (!this.m_active)
				{
					flag2 = false;
				}
				else if (!this.ContainsService(serviceType))
				{
					this.m_services.Add(serviceType, new Pair<BlockServiceTicket, object>(ticket, implementation));
					flag2 = true;
				}
				else
				{
					flag2 = false;
				}
			}
			return flag2;
		}

		// Token: 0x0600060F RID: 1551 RVA: 0x00015674 File Offset: 0x00013874
		internal object GetService(Type serviceType)
		{
			object locker = this.m_locker;
			object obj;
			lock (locker)
			{
				obj = (this.ContainsService(serviceType) ? this.m_services[serviceType].Second : null);
			}
			return obj;
		}

		// Token: 0x170000F8 RID: 248
		internal object this[Type serviceType]
		{
			get
			{
				return this.GetService(serviceType);
			}
		}

		// Token: 0x06000611 RID: 1553 RVA: 0x000156DC File Offset: 0x000138DC
		internal void Deactivate()
		{
			object locker = this.m_locker;
			lock (locker)
			{
				foreach (Pair<BlockServiceTicket, object> pair in this.m_services.Values)
				{
					pair.First.Dispose();
				}
				this.m_services.Clear();
				this.m_active = false;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x00015774 File Offset: 0x00013974
		internal int Count
		{
			get
			{
				object locker = this.m_locker;
				int count;
				lock (locker)
				{
					count = this.m_services.Count;
				}
				return count;
			}
		}

		// Token: 0x0400021C RID: 540
		private Dictionary<Type, Pair<BlockServiceTicket, object>> m_services;

		// Token: 0x0400021D RID: 541
		private bool m_active;

		// Token: 0x0400021E RID: 542
		private object m_locker;
	}
}

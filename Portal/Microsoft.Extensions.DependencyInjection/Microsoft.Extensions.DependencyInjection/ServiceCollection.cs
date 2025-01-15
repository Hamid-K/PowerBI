using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection
{
	// Token: 0x02000004 RID: 4
	public class ServiceCollection : IServiceCollection, IList<ServiceDescriptor>, ICollection<ServiceDescriptor>, IEnumerable<ServiceDescriptor>, IEnumerable
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002091 File Offset: 0x00000291
		public int Count
		{
			get
			{
				return this._descriptors.Count;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000006 RID: 6 RVA: 0x0000209E File Offset: 0x0000029E
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000003 RID: 3
		public ServiceDescriptor this[int index]
		{
			get
			{
				return this._descriptors[index];
			}
			set
			{
				this._descriptors[index] = value;
			}
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020BE File Offset: 0x000002BE
		public void Clear()
		{
			this._descriptors.Clear();
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000020CB File Offset: 0x000002CB
		public bool Contains(ServiceDescriptor item)
		{
			return this._descriptors.Contains(item);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020D9 File Offset: 0x000002D9
		public void CopyTo(ServiceDescriptor[] array, int arrayIndex)
		{
			this._descriptors.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000020E8 File Offset: 0x000002E8
		public bool Remove(ServiceDescriptor item)
		{
			return this._descriptors.Remove(item);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020F6 File Offset: 0x000002F6
		public IEnumerator<ServiceDescriptor> GetEnumerator()
		{
			return this._descriptors.GetEnumerator();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002108 File Offset: 0x00000308
		void ICollection<ServiceDescriptor>.Add(ServiceDescriptor item)
		{
			this._descriptors.Add(item);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002116 File Offset: 0x00000316
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x06000010 RID: 16 RVA: 0x0000211E File Offset: 0x0000031E
		public int IndexOf(ServiceDescriptor item)
		{
			return this._descriptors.IndexOf(item);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x0000212C File Offset: 0x0000032C
		public void Insert(int index, ServiceDescriptor item)
		{
			this._descriptors.Insert(index, item);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x0000213B File Offset: 0x0000033B
		public void RemoveAt(int index)
		{
			this._descriptors.RemoveAt(index);
		}

		// Token: 0x04000001 RID: 1
		private readonly List<ServiceDescriptor> _descriptors = new List<ServiceDescriptor>();
	}
}

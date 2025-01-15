using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000010 RID: 16
	public sealed class MashupPermissionSet : ICollection<MashupPermission>, IEnumerable<MashupPermission>, IEnumerable
	{
		// Token: 0x06000099 RID: 153 RVA: 0x00004B1B File Offset: 0x00002D1B
		public MashupPermissionSet()
		{
			this.wrappedSet = new HashSet<MashupPermission>();
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004B2E File Offset: 0x00002D2E
		public MashupPermissionSet(IEnumerable<MashupPermission> collection)
		{
			this.wrappedSet = new HashSet<MashupPermission>(collection);
		}

		// Token: 0x0600009B RID: 155 RVA: 0x00004B42 File Offset: 0x00002D42
		public void Add(MashupPermission item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			this.wrappedSet.Add(item);
		}

		// Token: 0x0600009C RID: 156 RVA: 0x00004B5F File Offset: 0x00002D5F
		public void Clear()
		{
			this.wrappedSet.Clear();
		}

		// Token: 0x0600009D RID: 157 RVA: 0x00004B6C File Offset: 0x00002D6C
		public bool Contains(MashupPermission item)
		{
			return this.wrappedSet.Contains(item);
		}

		// Token: 0x0600009E RID: 158 RVA: 0x00004B7A File Offset: 0x00002D7A
		public void CopyTo(MashupPermission[] array, int arrayIndex)
		{
			this.wrappedSet.CopyTo(array, arrayIndex);
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00004B89 File Offset: 0x00002D89
		public int Count
		{
			get
			{
				return this.wrappedSet.Count;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x00004B96 File Offset: 0x00002D96
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00004B99 File Offset: 0x00002D99
		public bool Remove(MashupPermission item)
		{
			return this.wrappedSet.Remove(item);
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004BA7 File Offset: 0x00002DA7
		public IEnumerator<MashupPermission> GetEnumerator()
		{
			return this.wrappedSet.GetEnumerator();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x00004BB9 File Offset: 0x00002DB9
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.wrappedSet.GetEnumerator();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x00004BCB File Offset: 0x00002DCB
		internal bool SetEquals(IEnumerable<MashupPermission> other)
		{
			return this.wrappedSet.SetEquals(other);
		}

		// Token: 0x04000052 RID: 82
		private HashSet<MashupPermission> wrappedSet;
	}
}

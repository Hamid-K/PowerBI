using System;
using System.Collections;
using System.Collections.Generic;

namespace AngleSharp.Dom.Io
{
	// Token: 0x020001C3 RID: 451
	internal sealed class FileList : IFileList, IEnumerable<IFile>, IEnumerable
	{
		// Token: 0x06000F35 RID: 3893 RVA: 0x00046F3A File Offset: 0x0004513A
		internal FileList()
		{
			this._entries = new List<IFile>();
		}

		// Token: 0x1700032B RID: 811
		public IFile this[int index]
		{
			get
			{
				return this._entries[index];
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x06000F37 RID: 3895 RVA: 0x00046F5B File Offset: 0x0004515B
		public int Length
		{
			get
			{
				return this._entries.Count;
			}
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00046F68 File Offset: 0x00045168
		public void Add(IFile item)
		{
			this._entries.Add(item);
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x00046F76 File Offset: 0x00045176
		public void Clear()
		{
			this._entries.Clear();
		}

		// Token: 0x06000F3A RID: 3898 RVA: 0x00046F83 File Offset: 0x00045183
		public bool Remove(IFile item)
		{
			return this._entries.Remove(item);
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x00046F91 File Offset: 0x00045191
		public IEnumerator<IFile> GetEnumerator()
		{
			return this._entries.GetEnumerator();
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x00046FA3 File Offset: 0x000451A3
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000A06 RID: 2566
		private readonly List<IFile> _entries;
	}
}

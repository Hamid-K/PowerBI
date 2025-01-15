using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002F5 RID: 757
	public sealed class FindFoldersResults : IEnumerable<Folder>, IEnumerable
	{
		// Token: 0x06001ABE RID: 6846 RVA: 0x00048318 File Offset: 0x00047318
		internal FindFoldersResults()
		{
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001ABF RID: 6847 RVA: 0x0004832B File Offset: 0x0004732B
		// (set) Token: 0x06001AC0 RID: 6848 RVA: 0x00048333 File Offset: 0x00047333
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
			internal set
			{
				this.totalCount = value;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001AC1 RID: 6849 RVA: 0x0004833C File Offset: 0x0004733C
		// (set) Token: 0x06001AC2 RID: 6850 RVA: 0x00048344 File Offset: 0x00047344
		public int? NextPageOffset
		{
			get
			{
				return this.nextPageOffset;
			}
			internal set
			{
				this.nextPageOffset = value;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001AC3 RID: 6851 RVA: 0x0004834D File Offset: 0x0004734D
		// (set) Token: 0x06001AC4 RID: 6852 RVA: 0x00048355 File Offset: 0x00047355
		public bool MoreAvailable
		{
			get
			{
				return this.moreAvailable;
			}
			internal set
			{
				this.moreAvailable = value;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001AC5 RID: 6853 RVA: 0x0004835E File Offset: 0x0004735E
		public Collection<Folder> Folders
		{
			get
			{
				return this.folders;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00048366 File Offset: 0x00047366
		public IEnumerator<Folder> GetEnumerator()
		{
			return this.folders.GetEnumerator();
		}

		// Token: 0x06001AC7 RID: 6855 RVA: 0x00048373 File Offset: 0x00047373
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.folders.GetEnumerator();
		}

		// Token: 0x04001426 RID: 5158
		private int totalCount;

		// Token: 0x04001427 RID: 5159
		private int? nextPageOffset;

		// Token: 0x04001428 RID: 5160
		private bool moreAvailable;

		// Token: 0x04001429 RID: 5161
		private Collection<Folder> folders = new Collection<Folder>();
	}
}

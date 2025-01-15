using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Mashup.Engine1.Library.Exchange
{
	// Token: 0x02000BEC RID: 3052
	internal class FolderFilter
	{
		// Token: 0x0600531E RID: 21278 RVA: 0x001197DC File Offset: 0x001179DC
		public FolderFilter()
		{
			this.values = new string[0];
			this.sortDirection = null;
		}

		// Token: 0x0600531F RID: 21279 RVA: 0x001197FC File Offset: 0x001179FC
		public FolderFilter(string folderPath)
		{
			this.values = new string[1];
			this.values[0] = folderPath;
			this.sortDirection = null;
		}

		// Token: 0x06005320 RID: 21280 RVA: 0x00119825 File Offset: 0x00117A25
		public FolderFilter(FolderFilter filter, SortDirection sortDirection)
		{
			this.values = filter.Values;
			this.sortDirection = new SortDirection?(sortDirection);
		}

		// Token: 0x06005321 RID: 21281 RVA: 0x00119848 File Offset: 0x00117A48
		public FolderFilter(FolderFilter filter1, FolderFilter filter2)
		{
			this.values = new string[filter1.Values.Length + filter2.values.Length];
			for (int i = 0; i < filter1.values.Length; i++)
			{
				this.values[i] = filter1.values[i];
			}
			for (int j = filter1.values.Length; j < this.values.Length; j++)
			{
				this.values[j] = filter2.values[j - filter1.values.Length];
			}
			this.sortDirection = null;
		}

		// Token: 0x17001995 RID: 6549
		// (get) Token: 0x06005322 RID: 21282 RVA: 0x001198D9 File Offset: 0x00117AD9
		public string[] Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17001996 RID: 6550
		// (get) Token: 0x06005323 RID: 21283 RVA: 0x001198E1 File Offset: 0x00117AE1
		public SortDirection SortDirection
		{
			get
			{
				return this.sortDirection.Value;
			}
		}

		// Token: 0x17001997 RID: 6551
		// (get) Token: 0x06005324 RID: 21284 RVA: 0x001198EE File Offset: 0x00117AEE
		public bool HasSortDirection
		{
			get
			{
				return this.sortDirection != null;
			}
		}

		// Token: 0x17001998 RID: 6552
		// (get) Token: 0x06005325 RID: 21285 RVA: 0x001198FB File Offset: 0x00117AFB
		public bool IsEmpty
		{
			get
			{
				return this.values.Length == 0;
			}
		}

		// Token: 0x04002DE8 RID: 11752
		private string[] values;

		// Token: 0x04002DE9 RID: 11753
		private SortDirection? sortDirection;
	}
}

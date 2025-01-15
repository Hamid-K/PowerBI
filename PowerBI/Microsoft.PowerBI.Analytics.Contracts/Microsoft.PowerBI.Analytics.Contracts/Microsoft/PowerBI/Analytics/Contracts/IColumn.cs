using System;

namespace Microsoft.PowerBI.Analytics.Contracts
{
	// Token: 0x0200000D RID: 13
	public interface IColumn
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600001F RID: 31
		string Name { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000020 RID: 32
		DataType DataType { get; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000021 RID: 33
		string Role { get; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000022 RID: 34
		ISortInformation SortInformation { get; }

		// Token: 0x06000023 RID: 35
		ISortInformation CreateSortInformation(int sortIndex, SortDirection sortDirection);
	}
}

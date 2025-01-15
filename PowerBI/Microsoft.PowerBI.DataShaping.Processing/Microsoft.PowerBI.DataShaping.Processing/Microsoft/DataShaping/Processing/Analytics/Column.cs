using System;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.DataShaping.Processing.Analytics
{
	// Token: 0x020000AF RID: 175
	[ImmutableObject(true)]
	internal sealed class Column : IColumn
	{
		// Token: 0x06000481 RID: 1153 RVA: 0x0000DD90 File Offset: 0x0000BF90
		internal Column(string name, DataType dataType, string role, ISortInformation sortInformation)
		{
			this._name = name;
			this._dataType = dataType;
			this._role = role;
			this._sortInformation = sortInformation;
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000DDB5 File Offset: 0x0000BFB5
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000DDBD File Offset: 0x0000BFBD
		public DataType DataType
		{
			get
			{
				return this._dataType;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000DDC5 File Offset: 0x0000BFC5
		public string Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000DDCD File Offset: 0x0000BFCD
		public ISortInformation SortInformation
		{
			get
			{
				return this._sortInformation;
			}
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0000DDD5 File Offset: 0x0000BFD5
		public ISortInformation CreateSortInformation(int sortIndex, SortDirection sortDirection)
		{
			return new SortInformation(sortIndex, sortDirection);
		}

		// Token: 0x0400024E RID: 590
		private readonly string _name;

		// Token: 0x0400024F RID: 591
		private readonly DataType _dataType;

		// Token: 0x04000250 RID: 592
		private readonly string _role;

		// Token: 0x04000251 RID: 593
		private readonly ISortInformation _sortInformation;
	}
}

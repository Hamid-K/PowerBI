using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Language.Query
{
	// Token: 0x02001F18 RID: 7960
	internal class NestingExpandListColumnQuery : ExpandListColumnQuery, INestedOperationQuery
	{
		// Token: 0x06010C23 RID: 68643 RVA: 0x0039BA32 File Offset: 0x00399C32
		private NestingExpandListColumnQuery(int columnIndex, bool singleOrDefault, TypeValue columnType, INestedOperationQuery innerQuery)
			: base(columnIndex, singleOrDefault, columnType, innerQuery.AsQuery)
		{
			this.innerNestedOperationQuery = innerQuery;
		}

		// Token: 0x06010C24 RID: 68644 RVA: 0x0039BA4C File Offset: 0x00399C4C
		public static ExpandListColumnQuery New(int columnIndex, bool singleOrDefault, INestedOperationQuery innerQuery)
		{
			return new NestingExpandListColumnQuery(columnIndex, singleOrDefault, null, innerQuery);
		}

		// Token: 0x17002C6E RID: 11374
		// (get) Token: 0x06010C25 RID: 68645 RVA: 0x00004FAE File Offset: 0x000031AE
		public Query AsQuery
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06010C26 RID: 68646 RVA: 0x0039BA58 File Offset: 0x00399C58
		public bool TrySelectColumns(NestedColumnSelection columnSelection, out INestedOperationQuery query)
		{
			int num = columnSelection.ColumnSelection.CreateSelectMap(this.Columns).MapColumn(base.ColumnIndex);
			if (num == -1)
			{
				query = null;
				return false;
			}
			INestedOperationQuery nestedOperationQuery;
			if (this.innerNestedOperationQuery.TrySelectColumns(columnSelection, out nestedOperationQuery))
			{
				query = new NestingExpandListColumnQuery(num, base.SingleOrDefault, null, nestedOperationQuery);
			}
			query = null;
			return false;
		}

		// Token: 0x06010C27 RID: 68647 RVA: 0x0039BAB0 File Offset: 0x00399CB0
		public override bool TryExpandRecordColumn(int columnToExpand, Keys fieldsToProject, Keys newColumns, out Query query)
		{
			query = NestingExpandRecordColumnQuery.New(columnToExpand, fieldsToProject, newColumns, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x06010C28 RID: 68648 RVA: 0x0039BAC9 File Offset: 0x00399CC9
		public override bool TryExpandListColumn(int columnIndex, bool singleOrDefault, out Query query)
		{
			query = NestingExpandListColumnQuery.New(columnIndex, singleOrDefault, this);
			return query is INestedOperationQuery;
		}

		// Token: 0x04006463 RID: 25699
		private readonly INestedOperationQuery innerNestedOperationQuery;
	}
}

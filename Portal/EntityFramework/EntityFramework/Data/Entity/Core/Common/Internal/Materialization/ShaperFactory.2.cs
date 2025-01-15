using System;
using System.Data.Common;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x02000640 RID: 1600
	internal class ShaperFactory<T> : ShaperFactory
	{
		// Token: 0x06004D04 RID: 19716 RVA: 0x00110178 File Offset: 0x0010E378
		internal ShaperFactory(int stateCount, CoordinatorFactory<T> rootCoordinatorFactory, Type[] columnTypes, bool[] nullableColumns, MergeOption mergeOption)
		{
			this._stateCount = stateCount;
			this._rootCoordinatorFactory = rootCoordinatorFactory;
			this.ColumnTypes = columnTypes;
			this.NullableColumns = nullableColumns;
			this._mergeOption = mergeOption;
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06004D05 RID: 19717 RVA: 0x001101A5 File Offset: 0x0010E3A5
		// (set) Token: 0x06004D06 RID: 19718 RVA: 0x001101AD File Offset: 0x0010E3AD
		public Type[] ColumnTypes { get; private set; }

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06004D07 RID: 19719 RVA: 0x001101B6 File Offset: 0x0010E3B6
		// (set) Token: 0x06004D08 RID: 19720 RVA: 0x001101BE File Offset: 0x0010E3BE
		public bool[] NullableColumns { get; private set; }

		// Token: 0x06004D09 RID: 19721 RVA: 0x001101C7 File Offset: 0x0010E3C7
		internal Shaper<T> Create(DbDataReader reader, ObjectContext context, MetadataWorkspace workspace, MergeOption mergeOption, bool readerOwned, bool streaming)
		{
			return new Shaper<T>(reader, context, workspace, mergeOption, this._stateCount, this._rootCoordinatorFactory, readerOwned, streaming);
		}

		// Token: 0x04001B59 RID: 7001
		private readonly int _stateCount;

		// Token: 0x04001B5A RID: 7002
		private readonly CoordinatorFactory<T> _rootCoordinatorFactory;

		// Token: 0x04001B5B RID: 7003
		private readonly MergeOption _mergeOption;
	}
}

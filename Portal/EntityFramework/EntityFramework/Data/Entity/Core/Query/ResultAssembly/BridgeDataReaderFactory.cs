using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Linq;

namespace System.Data.Entity.Core.Query.ResultAssembly
{
	// Token: 0x02000329 RID: 809
	internal class BridgeDataReaderFactory
	{
		// Token: 0x060026A9 RID: 9897 RVA: 0x0006F7FB File Offset: 0x0006D9FB
		public BridgeDataReaderFactory(Translator translator = null)
		{
			this._translator = translator ?? new Translator();
		}

		// Token: 0x060026AA RID: 9898 RVA: 0x0006F814 File Offset: 0x0006DA14
		public virtual DbDataReader Create(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> keyValuePair = this.CreateShaperInfo(storeDataReader, columnMap, workspace);
			return new BridgeDataReader(keyValuePair.Key, keyValuePair.Value, 0, this.GetNextResultShaperInfo(storeDataReader, workspace, nextResultColumnMaps).GetEnumerator());
		}

		// Token: 0x060026AB RID: 9899 RVA: 0x0006F84E File Offset: 0x0006DA4E
		private KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>> CreateShaperInfo(DbDataReader storeDataReader, ColumnMap columnMap, MetadataWorkspace workspace)
		{
			Shaper<RecordState> shaper = this._translator.TranslateColumnMap<RecordState>(columnMap, workspace, null, MergeOption.NoTracking, true, true).Create(storeDataReader, null, workspace, MergeOption.NoTracking, true, true);
			return new KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>(shaper, shaper.RootCoordinator.TypedCoordinatorFactory);
		}

		// Token: 0x060026AC RID: 9900 RVA: 0x0006F87C File Offset: 0x0006DA7C
		private IEnumerable<KeyValuePair<Shaper<RecordState>, CoordinatorFactory<RecordState>>> GetNextResultShaperInfo(DbDataReader storeDataReader, MetadataWorkspace workspace, IEnumerable<ColumnMap> nextResultColumnMaps)
		{
			return nextResultColumnMaps.Select((ColumnMap nextResultColumnMap) => this.CreateShaperInfo(storeDataReader, nextResultColumnMap, workspace));
		}

		// Token: 0x04000D7A RID: 3450
		private readonly Translator _translator;
	}
}

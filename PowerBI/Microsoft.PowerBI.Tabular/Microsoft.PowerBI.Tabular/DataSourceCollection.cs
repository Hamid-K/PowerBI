using System;
using System.Collections.Generic;
using Microsoft.AnalysisServices.Tabular.Metadata;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x02000055 RID: 85
	public sealed class DataSourceCollection : NamedMetadataObjectCollection<DataSource, Model>
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0002023D File Offset: 0x0001E43D
		internal DataSourceCollection(Model parent, IEqualityComparer<string> comparer)
			: base(ObjectType.DataSource, parent, comparer, true)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00020249 File Offset: 0x0001E449
		private protected override void CompareWith(MetadataObjectCollection<DataSource, Model> other, CopyContext context, IList<DataSource> removedItems, IList<DataSource> addedItems, IList<KeyValuePair<DataSource, DataSource>> matchedItems)
		{
			base.CompareWith(other, context, removedItems, addedItems, matchedItems);
			Utils.AdjustTypedNamedObjectCollectionsComparison<DataSource>(removedItems, addedItems, matchedItems, DataSource.CompareDataSourceType);
		}
	}
}

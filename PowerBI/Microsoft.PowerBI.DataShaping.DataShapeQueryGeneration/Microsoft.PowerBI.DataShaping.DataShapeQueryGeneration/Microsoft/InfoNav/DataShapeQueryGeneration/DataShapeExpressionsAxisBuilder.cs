using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000D RID: 13
	internal sealed class DataShapeExpressionsAxisBuilder
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000046BB File Offset: 0x000028BB
		internal DataShapeExpressionsAxisBuilder()
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x000046C4 File Offset: 0x000028C4
		internal DataShapeExpressionsAxisGroupingBuilder WithGrouping(int index)
		{
			if (this._groupings == null)
			{
				this._groupings = new List<DataShapeExpressionsAxisGroupingBuilder>();
			}
			DataShapeExpressionsAxisGroupingBuilder dataShapeExpressionsAxisGroupingBuilder;
			if (!this._groupings.TryGetNonNullAtExtendedIndex(index, out dataShapeExpressionsAxisGroupingBuilder))
			{
				dataShapeExpressionsAxisGroupingBuilder = new DataShapeExpressionsAxisGroupingBuilder();
				this._groupings.SetAtExtendedIndex(index, dataShapeExpressionsAxisGroupingBuilder);
			}
			return dataShapeExpressionsAxisGroupingBuilder;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000470C File Offset: 0x0000290C
		internal DataShapeExpressionsAxisSynchronizationBuilder WithSynchronization(int index)
		{
			if (this._synchronization == null)
			{
				this._synchronization = new List<DataShapeExpressionsAxisSynchronizationBuilder>();
			}
			DataShapeExpressionsAxisSynchronizationBuilder dataShapeExpressionsAxisSynchronizationBuilder;
			if (!this._synchronization.TryGetNonNullAtExtendedIndex(index, out dataShapeExpressionsAxisSynchronizationBuilder))
			{
				dataShapeExpressionsAxisSynchronizationBuilder = new DataShapeExpressionsAxisSynchronizationBuilder();
				this._synchronization.SetAtExtendedIndex(index, dataShapeExpressionsAxisSynchronizationBuilder);
			}
			return dataShapeExpressionsAxisSynchronizationBuilder;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600007C RID: 124 RVA: 0x00004751 File Offset: 0x00002951
		internal IReadOnlyList<DataShapeExpressionsAxisGroupingBuilder> Groupings
		{
			get
			{
				return this._groupings;
			}
		}

		// Token: 0x0600007D RID: 125 RVA: 0x0000475C File Offset: 0x0000295C
		internal DataShapeExpressionsAxis Build()
		{
			if (this._groupings == null)
			{
				return null;
			}
			DataShapeExpressionsAxis dataShapeExpressionsAxis = new DataShapeExpressionsAxis();
			dataShapeExpressionsAxis.Groupings = this._groupings.Select((DataShapeExpressionsAxisGroupingBuilder b) => b.Build()).ToList<DataShapeExpressionsAxisGrouping>();
			List<DataShapeExpressionsAxisSynchronizationBuilder> synchronization = this._synchronization;
			IList<SynchronizedGroupingBlock> list;
			if (synchronization == null)
			{
				list = null;
			}
			else
			{
				list = synchronization.Select((DataShapeExpressionsAxisSynchronizationBuilder b) => b.Result).ToList<SynchronizedGroupingBlock>();
			}
			dataShapeExpressionsAxis.Synchronization = list;
			return dataShapeExpressionsAxis;
		}

		// Token: 0x0400004B RID: 75
		private List<DataShapeExpressionsAxisGroupingBuilder> _groupings;

		// Token: 0x0400004C RID: 76
		private List<DataShapeExpressionsAxisSynchronizationBuilder> _synchronization;
	}
}

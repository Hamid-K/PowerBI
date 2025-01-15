using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x0200000E RID: 14
	internal sealed class DataShapeExpressionsAxisGroupingBuilder
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000047E9 File Offset: 0x000029E9
		internal DataShapeExpressionsAxisGroupingBuilder()
		{
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000047F4 File Offset: 0x000029F4
		internal DataShapeExpressionsAxisGroupingBuilder WithKey(ConceptualPropertyReference source, IConceptualColumn sourceField, int? select, string calc, bool isIdentityKey, IReadOnlyList<int> selectIndicesWithThisIdentity = null)
		{
			DataShapeExpressionsAxisGroupingKeyBuilder dataShapeExpressionsAxisGroupingKeyBuilder = new DataShapeExpressionsAxisGroupingKeyBuilder();
			Util.AddToLazyList<DataShapeExpressionsAxisGroupingKeyBuilder>(ref this._keys, dataShapeExpressionsAxisGroupingKeyBuilder);
			dataShapeExpressionsAxisGroupingKeyBuilder.WithSource(source, sourceField);
			dataShapeExpressionsAxisGroupingKeyBuilder.WithId(select, calc, selectIndicesWithThisIdentity);
			dataShapeExpressionsAxisGroupingKeyBuilder.WithIsIdentity(isIdentityKey);
			return this;
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00004830 File Offset: 0x00002A30
		internal DataShapeExpressionsAxisGroupingBuilder WithInternalSortKey(IConceptualProperty sourceField, int? select, string calc, SortDirection sortDirection)
		{
			DataShapeExpressionsAxisGroupingSortKey dataShapeExpressionsAxisGroupingSortKey = new DataShapeExpressionsAxisGroupingSortKey(calc, sortDirection, sourceField, select);
			Util.AddToLazyList<DataShapeExpressionsAxisGroupingSortKey>(ref this._sortKeys, dataShapeExpressionsAxisGroupingSortKey);
			return this;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00004855 File Offset: 0x00002A55
		internal DataShapeExpressionsAxisGroupingBuilder WithMember(string memberId)
		{
			this.Member = memberId;
			return this;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000485F File Offset: 0x00002A5F
		internal DataShapeExpressionsAxisGroupingBuilder WithSubtotalMember(string subtotalMemberId, SubtotalType subtotalType)
		{
			this.SubtotalMember = subtotalMemberId;
			this.SubtotalType = subtotalType;
			return this;
		}

		// Token: 0x06000083 RID: 131 RVA: 0x00004870 File Offset: 0x00002A70
		internal DataShapeExpressionsAxisGroupingBuilder WithAggregates(string aggregatesMemberId, IReadOnlyList<string> ids)
		{
			if (this._aggregates == null)
			{
				this._aggregates = new DataShapeExpressionsAxisGroupingAggregates
				{
					AggregatesMember = aggregatesMemberId,
					Ids = new List<string>(ids.Count)
				};
			}
			foreach (string text in ids)
			{
				this._aggregates.Ids.Add(text);
			}
			return this;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x000048F0 File Offset: 0x00002AF0
		internal DataShapeExpressionsAxisGroupingBuilder WithRestartIdentity(string restartId)
		{
			Util.AddToLazyList<string>(ref this._restartIdentities, restartId);
			return this;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000048FF File Offset: 0x00002AFF
		internal DataShapeExpressionsAxisGroupingBuilder WithSynchronizationIndex(string synchronizationIndexId)
		{
			this._synchronizationIndex = synchronizationIndexId;
			return this;
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004909 File Offset: 0x00002B09
		internal DataShapeExpressionsAxisGroupingBuilder WithSynchronizedGroup(DataShapeExpressionsAxisGrouping synchronizedGroup)
		{
			this._synchronizationGroup = synchronizedGroup;
			return this;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00004913 File Offset: 0x00002B13
		internal IReadOnlyList<DataShapeExpressionsAxisGroupingKeyBuilder> Keys
		{
			get
			{
				return this._keys;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000491B File Offset: 0x00002B1B
		internal IReadOnlyList<DataShapeExpressionsAxisGroupingSortKey> SortKeys
		{
			get
			{
				return this._sortKeys;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00004923 File Offset: 0x00002B23
		// (set) Token: 0x0600008A RID: 138 RVA: 0x0000492B File Offset: 0x00002B2B
		internal string Member { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004934 File Offset: 0x00002B34
		// (set) Token: 0x0600008C RID: 140 RVA: 0x0000493C File Offset: 0x00002B3C
		internal string SubtotalMember { get; private set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004945 File Offset: 0x00002B45
		// (set) Token: 0x0600008E RID: 142 RVA: 0x0000494D File Offset: 0x00002B4D
		internal SubtotalType SubtotalType { get; private set; }

		// Token: 0x0600008F RID: 143 RVA: 0x00004958 File Offset: 0x00002B58
		internal DataShapeExpressionsAxisGrouping Build()
		{
			DataShapeExpressionsAxisGrouping dataShapeExpressionsAxisGrouping = new DataShapeExpressionsAxisGrouping();
			IList<DataShapeExpressionsAxisGroupingKey> list;
			if (this._keys == null)
			{
				list = null;
			}
			else
			{
				list = (from k in this._keys
					where k.IsIdentityKey
					select k.Build()).ToList<DataShapeExpressionsAxisGroupingKey>();
			}
			dataShapeExpressionsAxisGrouping.Keys = list;
			dataShapeExpressionsAxisGrouping.Member = this.Member;
			dataShapeExpressionsAxisGrouping.SubtotalMember = this.SubtotalMember;
			dataShapeExpressionsAxisGrouping.Aggregates = this._aggregates;
			dataShapeExpressionsAxisGrouping.RestartIdentities = this._restartIdentities;
			dataShapeExpressionsAxisGrouping.SynchronizedGroup = this._synchronizationGroup;
			dataShapeExpressionsAxisGrouping.SynchronizationIndex = this._synchronizationIndex;
			return dataShapeExpressionsAxisGrouping;
		}

		// Token: 0x0400004D RID: 77
		private List<DataShapeExpressionsAxisGroupingKeyBuilder> _keys;

		// Token: 0x0400004E RID: 78
		private DataShapeExpressionsAxisGroupingAggregates _aggregates;

		// Token: 0x0400004F RID: 79
		private List<string> _restartIdentities;

		// Token: 0x04000050 RID: 80
		private string _synchronizationIndex;

		// Token: 0x04000051 RID: 81
		private DataShapeExpressionsAxisGrouping _synchronizationGroup;

		// Token: 0x04000052 RID: 82
		private List<DataShapeExpressionsAxisGroupingSortKey> _sortKeys;
	}
}

using System;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000079 RID: 121
	internal abstract class DsqExpressionAggregateBase
	{
		// Token: 0x060004F6 RID: 1270 RVA: 0x00012990 File Offset: 0x00010B90
		protected DsqExpressionAggregateBase(DataShapeBindingAggregateContainer bindingAggregate, int? selectIndex, int? groupingIndex)
		{
			this.Aggregate = bindingAggregate;
			this.PrimaryGroupIndex = groupingIndex;
			this.SelectIndex = selectIndex;
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060004F7 RID: 1271
		public abstract DsqExpressionAggregateKind Kind { get; }

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x000129AD File Offset: 0x00010BAD
		public DataShapeBindingAggregateContainer Aggregate { get; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x000129B5 File Offset: 0x00010BB5
		public int? PrimaryGroupIndex { get; }

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060004FA RID: 1274 RVA: 0x000129BD File Offset: 0x00010BBD
		public int? SelectIndex { get; }

		// Token: 0x060004FB RID: 1275
		public abstract void Accept(DsqExpressionAggregatesVisitorBase visitor);

		// Token: 0x060004FC RID: 1276 RVA: 0x000129C5 File Offset: 0x00010BC5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as DsqExpressionAggregateBase);
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x000129D4 File Offset: 0x00010BD4
		public bool Equals(DsqExpressionAggregateBase other)
		{
			bool? flag = Util.AreEqual<DsqExpressionAggregateBase>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			if (this.Aggregate == null)
			{
				return other.Aggregate == null;
			}
			if (this.Aggregate.Equals(other.Aggregate))
			{
				int? primaryGroupIndex = this.PrimaryGroupIndex;
				int? primaryGroupIndex2 = other.PrimaryGroupIndex;
				return (primaryGroupIndex.GetValueOrDefault() == primaryGroupIndex2.GetValueOrDefault()) & (primaryGroupIndex != null == (primaryGroupIndex2 != null));
			}
			return false;
		}

		// Token: 0x060004FE RID: 1278 RVA: 0x00012A5E File Offset: 0x00010C5E
		public override int GetHashCode()
		{
			return Hashing.CombineHash(Hashing.GetHashCode<DataShapeBindingAggregateContainer>(this.Aggregate, null), Hashing.GetHashCode<int?>(this.PrimaryGroupIndex, null));
		}
	}
}

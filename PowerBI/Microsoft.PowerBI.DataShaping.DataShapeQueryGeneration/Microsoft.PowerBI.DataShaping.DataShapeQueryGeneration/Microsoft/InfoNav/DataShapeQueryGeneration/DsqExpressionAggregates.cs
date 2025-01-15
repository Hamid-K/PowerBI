using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000078 RID: 120
	internal sealed class DsqExpressionAggregates : IEnumerable<DsqExpressionAggregateBase>, IEnumerable
	{
		// Token: 0x060004E9 RID: 1257 RVA: 0x000126A2 File Offset: 0x000108A2
		internal DsqExpressionAggregates()
		{
			this._aggregateSet = new HashSet<DsqExpressionAggregateBase>();
			this._aggregates = new List<DsqExpressionAggregateBase>();
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060004EA RID: 1258 RVA: 0x000126C0 File Offset: 0x000108C0
		public int Count
		{
			get
			{
				return this._aggregates.Count;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x060004EB RID: 1259 RVA: 0x000126CD File Offset: 0x000108CD
		public IReadOnlyList<DsqExpressionAggregateBase> Aggregates
		{
			get
			{
				return this._aggregates;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x060004EC RID: 1260 RVA: 0x000126D5 File Offset: 0x000108D5
		public bool HasScopedAggregate
		{
			get
			{
				return this._aggregates.Any((DsqExpressionAggregateBase agg) => agg.Aggregate != null && agg.Aggregate.Scope != null);
			}
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00012701 File Offset: 0x00010901
		public void Add(DsqExpressionAggregateBase value)
		{
			if (!this._aggregateSet.Contains(value))
			{
				this._aggregateSet.Add(value);
				this._aggregates.Add(value);
			}
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x0001272C File Offset: 0x0001092C
		public void AddRange(IReadOnlyList<DsqExpressionAggregateBase> aggregates)
		{
			this.EnsureCapacity(aggregates.Count);
			foreach (DsqExpressionAggregateBase dsqExpressionAggregateBase in aggregates)
			{
				this.Add(dsqExpressionAggregateBase);
			}
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x00012780 File Offset: 0x00010980
		public bool TrySuppressScopedAggregates(int primaryGroupIndex)
		{
			if (this.Aggregates.IsEmpty<DsqExpressionAggregateBase>())
			{
				return false;
			}
			int count = this.Aggregates.Count;
			bool flag = false;
			for (int i = 0; i < count; i++)
			{
				DsqExpressionAggregateBase dsqExpressionAggregateBase = this.Aggregates[i];
				int? num = dsqExpressionAggregateBase.PrimaryGroupIndex;
				if ((num.GetValueOrDefault() == primaryGroupIndex) & (num != null))
				{
					flag = true;
					this._aggregateSet.Remove(dsqExpressionAggregateBase);
				}
				DataShapeBindingAggregateContainer aggregate = dsqExpressionAggregateBase.Aggregate;
				int? num2;
				if (aggregate == null)
				{
					num2 = null;
				}
				else
				{
					AggregateScope scope = aggregate.Scope;
					num2 = ((scope != null) ? new int?(scope.PrimaryDepth) : null);
				}
				num = num2;
				int num3 = primaryGroupIndex + 1;
				if ((num.GetValueOrDefault() == num3) & (num != null))
				{
					flag = true;
					this._aggregateSet.Remove(dsqExpressionAggregateBase);
				}
				if (dsqExpressionAggregateBase.PrimaryGroupIndex == null)
				{
					DataShapeBindingAggregateContainer aggregate2 = dsqExpressionAggregateBase.Aggregate;
					bool flag2;
					if (aggregate2 == null)
					{
						flag2 = false;
					}
					else
					{
						AggregateScope scope2 = aggregate2.Scope;
						num = ((scope2 != null) ? new int?(scope2.PrimaryDepth) : null);
						num3 = 1;
						flag2 = (num.GetValueOrDefault() == num3) & (num != null);
					}
					if (flag2 && primaryGroupIndex == 1)
					{
						flag = true;
						this._aggregateSet.Remove(dsqExpressionAggregateBase);
					}
				}
			}
			if (!flag)
			{
				return false;
			}
			this._aggregates = this._aggregateSet.ToList<DsqExpressionAggregateBase>();
			return true;
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x000128DC File Offset: 0x00010ADC
		private void EnsureCapacity(int itemsToAdd)
		{
			int num = this._aggregates.Count + itemsToAdd;
			if (num > this._aggregates.Capacity)
			{
				this._aggregates.Capacity = num;
			}
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x00012911 File Offset: 0x00010B11
		public bool Contains(DsqExpressionAggregateBase value)
		{
			return this._aggregateSet.Contains(value);
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0001291F File Offset: 0x00010B1F
		public IEnumerator<DsqExpressionAggregateBase> GetEnumerator()
		{
			return this._aggregates.GetEnumerator();
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x00012931 File Offset: 0x00010B31
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x00012939 File Offset: 0x00010B39
		public bool IsEmpty()
		{
			return this._aggregates.IsEmpty<DsqExpressionAggregateBase>();
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x00012948 File Offset: 0x00010B48
		internal void Accept(DsqExpressionAggregatesVisitorBase visitor)
		{
			foreach (DsqExpressionAggregateBase dsqExpressionAggregateBase in this)
			{
				dsqExpressionAggregateBase.Accept(visitor);
			}
		}

		// Token: 0x040002D2 RID: 722
		private HashSet<DsqExpressionAggregateBase> _aggregateSet;

		// Token: 0x040002D3 RID: 723
		private List<DsqExpressionAggregateBase> _aggregates;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000028 RID: 40
	internal sealed class DiscreteFilterState
	{
		// Token: 0x06000192 RID: 402 RVA: 0x0000939E File Offset: 0x0000759E
		internal DiscreteFilterState()
			: this(false)
		{
			this._columnStates = new Dictionary<IConceptualProperty, DiscreteColumnValueState>();
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000093B2 File Offset: 0x000075B2
		private DiscreteFilterState(bool isUndefined)
		{
			this._isUndefined = isUndefined;
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000093C1 File Offset: 0x000075C1
		internal bool IsUndefined
		{
			get
			{
				return this._isUndefined;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000093CC File Offset: 0x000075CC
		internal void AddValue(IConceptualProperty property, ResolvedQueryExpression value)
		{
			if (this.IsUndefined)
			{
				throw new InvalidOperationException("Cannot AddValue on Undefined state");
			}
			DiscreteColumnValueState discreteColumnValueState;
			if (!this._columnStates.TryGetValue(property, out discreteColumnValueState))
			{
				discreteColumnValueState = new DiscreteColumnValueState(property);
				this._columnStates.Add(discreteColumnValueState.Property, discreteColumnValueState);
			}
			discreteColumnValueState.Values.Add(value);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00009424 File Offset: 0x00007624
		internal bool TryGetDiscreteCount(IConceptualProperty property, out int count)
		{
			DiscreteColumnValueState discreteColumnValueState;
			if (!this.IsUndefined && this._columnStates.TryGetValue(property, out discreteColumnValueState))
			{
				count = discreteColumnValueState.Values.Count;
				return true;
			}
			count = 0;
			return false;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000945C File Offset: 0x0000765C
		internal DiscreteFilterState Or(DiscreteFilterState other)
		{
			if (this.IsUndefined || other.IsUndefined)
			{
				return DiscreteFilterState.Undefined;
			}
			if (this._columnStates.Count != other._columnStates.Count)
			{
				return DiscreteFilterState.Undefined;
			}
			foreach (DiscreteColumnValueState discreteColumnValueState in this._columnStates.Values)
			{
				DiscreteColumnValueState discreteColumnValueState2;
				if (!other._columnStates.TryGetValue(discreteColumnValueState.Property, out discreteColumnValueState2))
				{
					return DiscreteFilterState.Undefined;
				}
				discreteColumnValueState.Values.UnionWith(discreteColumnValueState2.Values);
			}
			return this;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00009514 File Offset: 0x00007714
		internal DiscreteFilterState And(DiscreteFilterState other)
		{
			if (this.IsUndefined)
			{
				return other;
			}
			if (other.IsUndefined)
			{
				return this;
			}
			foreach (DiscreteColumnValueState discreteColumnValueState in this._columnStates.Values)
			{
				DiscreteColumnValueState discreteColumnValueState2;
				if (other._columnStates.TryGetValue(discreteColumnValueState.Property, out discreteColumnValueState2))
				{
					discreteColumnValueState.Values.IntersectWith(discreteColumnValueState2.Values);
				}
			}
			foreach (KeyValuePair<IConceptualProperty, DiscreteColumnValueState> keyValuePair in other._columnStates)
			{
				if (!this._columnStates.ContainsKey(keyValuePair.Key))
				{
					this._columnStates.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return this;
		}

		// Token: 0x040000C6 RID: 198
		internal static readonly DiscreteFilterState Undefined = new DiscreteFilterState(true);

		// Token: 0x040000C7 RID: 199
		private readonly Dictionary<IConceptualProperty, DiscreteColumnValueState> _columnStates;

		// Token: 0x040000C8 RID: 200
		private readonly bool _isUndefined;
	}
}

using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000013 RID: 19
	internal sealed class SelectBindingsBuilder
	{
		// Token: 0x060000B7 RID: 183 RVA: 0x00004F90 File Offset: 0x00003190
		internal SelectBindingsBuilder(int selectCount, IReadOnlyList<ResolvedQuerySelect> resolvedQuerySelects)
		{
			this._selectBindings = new SelectBinding[selectCount];
			this._selectBindingGroupKeys = new List<SelectIdentityKeyBuilder>[selectCount];
			this._selectBindingSortKeys = new List<SelectSortIdentityKeyBuilder>[selectCount];
			this._calcIdsForSelectIndex = new SelectBindingValue[selectCount];
			this._resolvedQuerySelects = resolvedQuerySelects;
			this._lineageProperties = new IConceptualProperty[selectCount];
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00004FE6 File Offset: 0x000031E6
		internal int SelectCount
		{
			get
			{
				return this._selectBindings.Length;
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00004FF0 File Offset: 0x000031F0
		internal string GetFormatStringForSelect(int? selectIndex)
		{
			if (selectIndex == null)
			{
				return null;
			}
			return this._selectBindings[selectIndex.Value].Format;
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00005010 File Offset: 0x00003210
		internal string GetCalcIdForSelect(int selectIndex)
		{
			SelectBindingValue selectBindingValue = this._calcIdsForSelectIndex[selectIndex];
			if (selectBindingValue == null)
			{
				return null;
			}
			return selectBindingValue.Value;
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00005025 File Offset: 0x00003225
		internal void SetCalcIdForSelect(int selectIndex, string value)
		{
			if (this._calcIdsForSelectIndex[selectIndex] == null)
			{
				this._calcIdsForSelectIndex[selectIndex] = new SelectBindingValue();
			}
			this._calcIdsForSelectIndex[selectIndex].Value = value;
		}

		// Token: 0x060000BC RID: 188 RVA: 0x0000504C File Offset: 0x0000324C
		internal IntervalValue SetIntervalForSelect(int selectIndex, string minValue, string maxValue)
		{
			if (this._calcIdsForSelectIndex[selectIndex] == null)
			{
				this._calcIdsForSelectIndex[selectIndex] = new SelectBindingValue();
			}
			IntervalValue intervalValue = new IntervalValue
			{
				MinValue = minValue,
				MaxValue = maxValue
			};
			this._calcIdsForSelectIndex[selectIndex].Interval = intervalValue;
			return intervalValue;
		}

		// Token: 0x060000BD RID: 189 RVA: 0x00005094 File Offset: 0x00003294
		internal string GetCalcIdForSelectSource(int selectIndex, string entityName, string columnName)
		{
			List<SelectIdentityKeyBuilder> list = this._selectBindingGroupKeys[selectIndex];
			if (!list.IsNullOrEmpty<SelectIdentityKeyBuilder>())
			{
				foreach (SelectIdentityKeyBuilder selectIdentityKeyBuilder in list)
				{
					if (this.AreSameSource(selectIdentityKeyBuilder.Source, entityName, columnName))
					{
						if (selectIdentityKeyBuilder.CalcId == null && selectIdentityKeyBuilder.SelectIndex != null)
						{
							return this.GetCalcIdForSelect(selectIdentityKeyBuilder.SelectIndex.Value);
						}
						return selectIdentityKeyBuilder.CalcId;
					}
				}
			}
			List<SelectSortIdentityKeyBuilder> list2 = this._selectBindingSortKeys[selectIndex];
			if (!list2.IsNullOrEmpty<SelectSortIdentityKeyBuilder>())
			{
				foreach (SelectSortIdentityKeyBuilder selectSortIdentityKeyBuilder in list2)
				{
					if (this.AreSameSource(selectSortIdentityKeyBuilder.Source, entityName, columnName))
					{
						return selectSortIdentityKeyBuilder.Calc;
					}
				}
			}
			return null;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x000051A4 File Offset: 0x000033A4
		private bool AreSameSource(ConceptualPropertyReference source, string entityName, string columnName)
		{
			return source != null && ConceptualNameComparer.Instance.Equals(source.Entity, entityName) && ConceptualNameComparer.Instance.Equals(source.Property, columnName);
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000051D4 File Offset: 0x000033D4
		internal void AddToBindingIdentityKeys(IReadOnlyList<int> selectIndicesWithThisIdentity, int? selectIndex, IReadOnlyList<int> additionalSelectIndices, ConceptualPropertyReference source, IConceptualColumn internalLineageColumn, string calcId)
		{
			if (selectIndicesWithThisIdentity.IsNullOrEmpty<int>())
			{
				return;
			}
			SelectIdentityKeyBuilder selectIdentityKeyBuilder = new SelectIdentityKeyBuilder(source, internalLineageColumn, calcId, selectIndex, additionalSelectIndices);
			foreach (int num in selectIndicesWithThisIdentity)
			{
				Util.EnsureList<SelectIdentityKeyBuilder>(ref this._selectBindingGroupKeys[num]).Add(selectIdentityKeyBuilder);
			}
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00005244 File Offset: 0x00003444
		internal void AddInternalSortKeysToBinding(IReadOnlyList<int> selectIds, ConceptualPropertyReference sortKeyCalcSouce, IConceptualColumn internalLineageColumn, string calcId)
		{
			foreach (int num in selectIds)
			{
				this.AddInternalSortKeyToBinding(sortKeyCalcSouce, internalLineageColumn, num, calcId);
			}
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x00005290 File Offset: 0x00003490
		private void AddInternalSortKeyToBinding(ConceptualPropertyReference sortKeyCalcSouce, IConceptualColumn internalLineageColumn, int selectIdentityIndex, string calcId)
		{
			Util.EnsureList<SelectSortIdentityKeyBuilder>(ref this._selectBindingSortKeys[selectIdentityIndex]).Add(new SelectSortIdentityKeyBuilder(sortKeyCalcSouce, internalLineageColumn, calcId));
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x000052B1 File Offset: 0x000034B1
		internal void AddSelectBinding(int selectIndex, string value, string formatString, SelectKind kind, int? primaryDepth, int? secondaryDepth, IConceptualProperty lineageProperty)
		{
			this.SetSelectBinding(selectIndex, formatString, kind, primaryDepth, secondaryDepth, lineageProperty).Value = value;
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000052C9 File Offset: 0x000034C9
		internal void AddIntervalSelectBinding(int selectIndex, IntervalValue interval, string formatString, SelectKind kind, int? primaryDepth, int? secondaryDepth, IConceptualProperty lineageProperty)
		{
			this.SetSelectBinding(selectIndex, formatString, kind, primaryDepth, secondaryDepth, lineageProperty).Interval = interval;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000052E1 File Offset: 0x000034E1
		private SelectBinding SetSelectBinding(int selectIndex, string formatString, SelectKind kind, int? primaryDepth, int? secondaryDepth, IConceptualProperty lineageProperty)
		{
			SelectBinding selectBinding = this.EnsureSelectBinding(selectIndex);
			selectBinding.Kind = kind;
			selectBinding.Format = formatString;
			selectBinding.Depth = primaryDepth;
			selectBinding.SecondaryDepth = secondaryDepth;
			this.SetLineageProperty(selectIndex, lineageProperty);
			return selectBinding;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005311 File Offset: 0x00003511
		private void SetLineageProperty(int selectIndex, IConceptualProperty lineageProperty)
		{
			if (lineageProperty == null)
			{
				return;
			}
			this._lineageProperties[selectIndex] = lineageProperty;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005320 File Offset: 0x00003520
		internal SelectBinding EnsureSelectBinding(int selectIndex)
		{
			SelectBinding selectBinding = this._selectBindings[selectIndex];
			if (selectBinding == null)
			{
				selectBinding = (this._selectBindings[selectIndex] = new SelectBinding());
				if (this._resolvedQuerySelects != null && selectIndex >= 0 && selectIndex < this._resolvedQuerySelects.Count)
				{
					ResolvedQuerySelect resolvedQuerySelect = this._resolvedQuerySelects[selectIndex];
					if (!string.IsNullOrEmpty(resolvedQuerySelect.Name))
					{
						selectBinding.Name = resolvedQuerySelect.Name;
					}
				}
			}
			return selectBinding;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005389 File Offset: 0x00003589
		internal SelectBinding GetSelectBinding(int selectIndex)
		{
			return this._selectBindings[selectIndex];
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005393 File Offset: 0x00003593
		internal IReadOnlyList<SelectIdentityKeyBuilder> GetSelectGroupKeys(int selectIndex)
		{
			return this._selectBindingGroupKeys[selectIndex];
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x0000539D File Offset: 0x0000359D
		internal IReadOnlyList<SelectSortIdentityKeyBuilder> GetSelectSortKeys(int selectIndex)
		{
			return this._selectBindingSortKeys[selectIndex];
		}

		// Token: 0x060000CA RID: 202 RVA: 0x000053A7 File Offset: 0x000035A7
		internal IConceptualProperty GetLineageProperty(int selectIndex)
		{
			IConceptualProperty[] lineageProperties = this._lineageProperties;
			if (lineageProperties == null)
			{
				return null;
			}
			return lineageProperties[selectIndex];
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000053B8 File Offset: 0x000035B8
		internal SelectBinding[] ToBindings()
		{
			for (int i = 0; i < this._selectBindings.Length; i++)
			{
				List<SelectIdentityKeyBuilder> list = this._selectBindingGroupKeys[i];
				if (!list.IsNullOrEmpty<SelectIdentityKeyBuilder>())
				{
					List<SelectIdentityKey> list2 = new List<SelectIdentityKey>(list.Count);
					foreach (SelectIdentityKeyBuilder selectIdentityKeyBuilder in list)
					{
						list2.Add(selectIdentityKeyBuilder.ToIdentityKey(new Func<int, string>(this.GetCalcIdForSelect), i));
					}
					this._selectBindings[i].GroupKeys = list2;
				}
			}
			return this._selectBindings;
		}

		// Token: 0x04000061 RID: 97
		private readonly SelectBinding[] _selectBindings;

		// Token: 0x04000062 RID: 98
		private readonly List<SelectIdentityKeyBuilder>[] _selectBindingGroupKeys;

		// Token: 0x04000063 RID: 99
		private readonly List<SelectSortIdentityKeyBuilder>[] _selectBindingSortKeys;

		// Token: 0x04000064 RID: 100
		private readonly IReadOnlyList<ResolvedQuerySelect> _resolvedQuerySelects;

		// Token: 0x04000065 RID: 101
		private readonly SelectBindingValue[] _calcIdsForSelectIndex;

		// Token: 0x04000066 RID: 102
		private readonly IConceptualProperty[] _lineageProperties;
	}
}

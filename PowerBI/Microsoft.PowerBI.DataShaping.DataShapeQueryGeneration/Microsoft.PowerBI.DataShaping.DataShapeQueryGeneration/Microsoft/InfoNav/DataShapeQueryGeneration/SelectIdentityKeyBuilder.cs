using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000015 RID: 21
	internal sealed class SelectIdentityKeyBuilder
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x0000548A File Offset: 0x0000368A
		internal SelectIdentityKeyBuilder(ConceptualPropertyReference source, IConceptualColumn internalLineageColumn, string calcId, int? selectIndex, IReadOnlyList<int> additionalSelectIndices)
		{
			this.Source = source;
			this.InternalLineageColumn = internalLineageColumn;
			this.CalcId = calcId;
			this.SelectIndex = selectIndex;
			this._additionalSelectIndices = additionalSelectIndices;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000054B7 File Offset: 0x000036B7
		public int? SelectIndex { get; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x000054BF File Offset: 0x000036BF
		internal string CalcId { get; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x000054C7 File Offset: 0x000036C7
		internal ConceptualPropertyReference Source { get; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x000054CF File Offset: 0x000036CF
		internal IConceptualColumn InternalLineageColumn { get; }

		// Token: 0x060000D6 RID: 214 RVA: 0x000054D8 File Offset: 0x000036D8
		internal SelectIdentityKey ToIdentityKey(Func<int, string> getCalcIdForSelect, int currentSelectIndex)
		{
			string text = this.ResolveCalcId(getCalcIdForSelect);
			bool flag = (this.SelectIndex != null && this.SelectIndex.Value == currentSelectIndex) || (this._additionalSelectIndices != null && this._additionalSelectIndices.Contains(currentSelectIndex));
			return new SelectIdentityKey
			{
				Source = this.Source,
				Calc = text,
				IsSameAsSelect = flag
			};
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005548 File Offset: 0x00003748
		internal string ResolveCalcId(Func<int, string> getCalcIdForSelect)
		{
			string text = this.CalcId;
			if (text == null && this.SelectIndex != null)
			{
				text = getCalcIdForSelect(this.SelectIndex.Value);
			}
			return text;
		}

		// Token: 0x04000069 RID: 105
		private readonly IReadOnlyList<int> _additionalSelectIndices;
	}
}

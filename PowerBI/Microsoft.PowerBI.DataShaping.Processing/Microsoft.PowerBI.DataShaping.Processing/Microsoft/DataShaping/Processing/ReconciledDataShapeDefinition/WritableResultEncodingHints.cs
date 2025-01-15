using System;
using System.Collections.Generic;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x0200004C RID: 76
	internal sealed class WritableResultEncodingHints : ResultEncodingHints
	{
		// Token: 0x060001FB RID: 507 RVA: 0x0000615A File Offset: 0x0000435A
		internal void SetDisableDictionaryEncoding(bool value)
		{
			this._disableDictionaryEncoding = value;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006163 File Offset: 0x00004363
		internal void AddDictionaryEncodingExcludeList(string id)
		{
			Util.AddToLazySet<string>(ref this._dictEncodingExcludeList, id, null);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006174 File Offset: 0x00004374
		internal void ExcludeCalculationsForDictionaryEncoding(IList<Calculation> calcs)
		{
			if (calcs.IsNullOrEmpty<Calculation>())
			{
				return;
			}
			foreach (Calculation calculation in calcs)
			{
				if (base.CalculationsWithSharedValues.IsNullOrEmpty<KeyValuePair<string, ISet<string>>>() || !base.CalculationsWithSharedValues.ContainsKey(calculation.Id))
				{
					this.AddDictionaryEncodingExcludeList(calculation.Id);
				}
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000061EC File Offset: 0x000043EC
		internal void ExcludeMemberCalculationsFromDictionaryEncoding(DataMember dataMember)
		{
			this.ExcludeCalculationsForDictionaryEncoding(dataMember.Calculations);
			if (dataMember.Intersections.IsNullOrEmpty<DataIntersection>())
			{
				return;
			}
			foreach (DataIntersection dataIntersection in dataMember.Intersections)
			{
				this.ExcludeCalculationsForDictionaryEncoding(dataIntersection.Calculations);
			}
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006258 File Offset: 0x00004458
		internal void SetCalculationWithSharedValues(string calculationId, ISet<string> calculationSet)
		{
			IReadOnlyDictionary<string, ISet<string>> calculationsWithSharedValues = base.CalculationsWithSharedValues;
			Contract.RetailAssert(calculationsWithSharedValues == null || !calculationsWithSharedValues.ContainsKey(calculationId), "Calculation {0} listed twice in ResultEncodingHints calculation sets.", calculationId);
			Util.AddToLazyDictionary<string, ISet<string>>(ref this._calculationsWithSharedValues, calculationId, calculationSet, null);
		}
	}
}

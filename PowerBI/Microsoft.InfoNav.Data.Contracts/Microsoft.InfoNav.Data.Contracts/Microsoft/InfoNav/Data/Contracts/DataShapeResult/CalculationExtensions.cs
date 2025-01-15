using System;

namespace Microsoft.InfoNav.Data.Contracts.DataShapeResult
{
	// Token: 0x02000104 RID: 260
	public static class CalculationExtensions
	{
		// Token: 0x060006EC RID: 1772 RVA: 0x0000ECE3 File Offset: 0x0000CEE3
		public static bool TryGetValue(this Calculation calculation, out double value)
		{
			return calculation.TryGetValue(ConceptualPrimitiveType.Double, out value);
		}

		// Token: 0x060006ED RID: 1773 RVA: 0x0000ECED File Offset: 0x0000CEED
		public static bool TryGetValue(this Calculation calculation, out long value)
		{
			return calculation.TryGetValue(ConceptualPrimitiveType.Integer, out value);
		}

		// Token: 0x060006EE RID: 1774 RVA: 0x0000ECF7 File Offset: 0x0000CEF7
		public static bool TryGetValue(this Calculation calculation, out decimal value)
		{
			return calculation.TryGetValue(ConceptualPrimitiveType.Decimal, out value);
		}

		// Token: 0x060006EF RID: 1775 RVA: 0x0000ED01 File Offset: 0x0000CF01
		public static bool TryGetValue(this Calculation calculation, out DateTime value)
		{
			return calculation.TryGetValue(ConceptualPrimitiveType.DateTime, out value);
		}

		// Token: 0x060006F0 RID: 1776 RVA: 0x0000ED0C File Offset: 0x0000CF0C
		private static bool TryGetValue<T>(this Calculation calculation, ConceptualPrimitiveType expectedConceptualPrimitiveType, out T value) where T : IEquatable<T>
		{
			value = default(T);
			object obj = ((calculation == null) ? null : calculation.RawValue);
			if (obj != null && obj is T)
			{
				value = (T)((object)obj);
				return true;
			}
			return false;
		}
	}
}

using System;

namespace System
{
	// Token: 0x020000C7 RID: 199
	internal static class DecimalDecCalc
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x00018CF4 File Offset: 0x00016EF4
		private static uint D32DivMod1E9(uint hi32, ref uint lo32)
		{
			ulong num = ((ulong)hi32 << 32) | (ulong)lo32;
			lo32 = (uint)(num / 1000000000UL);
			return (uint)(num % 1000000000UL);
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00018D24 File Offset: 0x00016F24
		internal static uint DecDivMod1E9(ref MutableDecimal value)
		{
			return DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(DecimalDecCalc.D32DivMod1E9(0U, ref value.High), ref value.Mid), ref value.Low);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00018D48 File Offset: 0x00016F48
		internal static void DecAddInt32(ref MutableDecimal value, uint i)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, i) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00018D7C File Offset: 0x00016F7C
		private static bool D32AddCarry(ref uint value, uint i)
		{
			uint num = value;
			uint num2 = num + i;
			value = num2;
			return num2 < num || num2 < i;
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00018DA4 File Offset: 0x00016FA4
		internal static void DecMul10(ref MutableDecimal value)
		{
			MutableDecimal mutableDecimal = value;
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecShiftLeft(ref value);
			DecimalDecCalc.DecAdd(ref value, mutableDecimal);
			DecimalDecCalc.DecShiftLeft(ref value);
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x00018DD8 File Offset: 0x00016FD8
		private static void DecShiftLeft(ref MutableDecimal value)
		{
			uint num = (((value.Low & 2147483648U) != 0U) ? 1U : 0U);
			uint num2 = (((value.Mid & 2147483648U) != 0U) ? 1U : 0U);
			value.Low <<= 1;
			value.Mid = (value.Mid << 1) | num;
			value.High = (value.High << 1) | num2;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x00018E4C File Offset: 0x0001704C
		private static void DecAdd(ref MutableDecimal value, MutableDecimal d)
		{
			if (DecimalDecCalc.D32AddCarry(ref value.Low, d.Low) && DecimalDecCalc.D32AddCarry(ref value.Mid, 1U))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			if (DecimalDecCalc.D32AddCarry(ref value.Mid, d.Mid))
			{
				DecimalDecCalc.D32AddCarry(ref value.High, 1U);
			}
			DecimalDecCalc.D32AddCarry(ref value.High, d.High);
		}
	}
}

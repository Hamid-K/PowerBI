using System;
using System.Collections;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils
{
	// Token: 0x020001B1 RID: 433
	public static class VariableLengthBitArrayUtils
	{
		// Token: 0x06000996 RID: 2454 RVA: 0x0001C247 File Offset: 0x0001A447
		public static void Canonicalize(BitArray b0, BitArray b1, out BitArray canonicalB0, out BitArray canonicalB1, bool value)
		{
			canonicalB0 = new BitArray(b0);
			canonicalB1 = new BitArray(b1);
			VariableLengthBitArrayUtils.Canonicalize(canonicalB0, canonicalB1, value);
		}

		// Token: 0x06000997 RID: 2455 RVA: 0x0001C264 File Offset: 0x0001A464
		public static void Canonicalize(BitArray b0, BitArray b1, bool value)
		{
			if (b0.Length == b1.Length)
			{
				return;
			}
			int num = Math.Max(b0.Length, b1.Length);
			BitArray bitArray = ((b0.Length < num) ? b0 : b1);
			int length = bitArray.Length;
			bitArray.Length = num;
			for (int i = length; i < num; i++)
			{
				bitArray.Set(i, value);
			}
		}

		// Token: 0x06000998 RID: 2456 RVA: 0x0001C2C4 File Offset: 0x0001A4C4
		public static BitArray FunctionalOr(BitArray b0, BitArray b1, bool value)
		{
			BitArray bitArray;
			BitArray bitArray2;
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, out bitArray, out bitArray2, value);
			bitArray.Or(bitArray2);
			return bitArray;
		}

		// Token: 0x06000999 RID: 2457 RVA: 0x0001C2E8 File Offset: 0x0001A4E8
		public static BitArray FunctionalAnd(BitArray b0, BitArray b1, bool value)
		{
			BitArray bitArray;
			BitArray bitArray2;
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, out bitArray, out bitArray2, value);
			bitArray.And(bitArray2);
			return bitArray;
		}

		// Token: 0x0600099A RID: 2458 RVA: 0x0001C30A File Offset: 0x0001A50A
		public static BitArray FunctionalNot(BitArray b0)
		{
			return new BitArray(b0).Not();
		}

		// Token: 0x0600099B RID: 2459 RVA: 0x0001C318 File Offset: 0x0001A518
		public static BitArray FunctionalXor(BitArray b0, BitArray b1, bool value)
		{
			BitArray bitArray;
			BitArray bitArray2;
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, out bitArray, out bitArray2, value);
			bitArray.Xor(bitArray2);
			return bitArray;
		}

		// Token: 0x0600099C RID: 2460 RVA: 0x0001C33A File Offset: 0x0001A53A
		public static BitArray InPlaceOr(this BitArray b0, BitArray b1, bool value)
		{
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, value);
			b0.Or(b1);
			return b0;
		}

		// Token: 0x0600099D RID: 2461 RVA: 0x0001C34D File Offset: 0x0001A54D
		public static BitArray InPlaceAnd(this BitArray b0, BitArray b1, bool value)
		{
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, value);
			b0.And(b1);
			return b0;
		}

		// Token: 0x0600099E RID: 2462 RVA: 0x0001C360 File Offset: 0x0001A560
		public static BitArray InPlaceNot(this BitArray b0)
		{
			return b0.Not();
		}

		// Token: 0x0600099F RID: 2463 RVA: 0x0001C368 File Offset: 0x0001A568
		public static BitArray InPlaceXor(this BitArray b0, BitArray b1, bool value)
		{
			VariableLengthBitArrayUtils.Canonicalize(b0, b1, value);
			b0.Xor(b1);
			return b0;
		}

		// Token: 0x060009A0 RID: 2464 RVA: 0x0001C37B File Offset: 0x0001A57B
		public static BitArray ExtendAndUpdate(this BitArray b0, int position, bool value)
		{
			if (b0.Length <= position)
			{
				b0.Length = position + 1;
			}
			b0.Set(position, value);
			return b0;
		}
	}
}

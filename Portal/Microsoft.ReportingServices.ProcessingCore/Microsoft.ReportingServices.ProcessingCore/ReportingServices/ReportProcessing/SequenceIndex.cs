using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000664 RID: 1636
	internal abstract class SequenceIndex
	{
		// Token: 0x06005AB5 RID: 23221 RVA: 0x00175564 File Offset: 0x00173764
		internal static void SetBit(ref byte[] sequence, int sequenceIndex)
		{
			byte b = (byte)(SequenceIndex.BitMask001 << sequenceIndex % 8);
			byte[] array = sequence;
			int num = sequenceIndex >> 3;
			array[num] |= b;
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x00175590 File Offset: 0x00173790
		internal static void ClearBit(ref byte[] sequence, int sequenceIndex)
		{
			byte b = (byte)(SequenceIndex.BitMask001 << sequenceIndex % 8);
			b ^= SequenceIndex.BitMask255;
			byte[] array = sequence;
			int num = sequenceIndex >> 3;
			array[num] &= b;
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x001755C4 File Offset: 0x001737C4
		internal static bool GetBit(byte[] sequence, int sequenceIndex, bool returnValueIfSequenceNull)
		{
			if (sequence == null)
			{
				return returnValueIfSequenceNull;
			}
			byte b = (byte)(SequenceIndex.BitMask001 << sequenceIndex % 8);
			return (sequence[sequenceIndex >> 3] & b) > 0;
		}

		// Token: 0x04002F2B RID: 12075
		internal static byte BitMask001 = 1;

		// Token: 0x04002F2C RID: 12076
		internal static byte BitMask255 = byte.MaxValue;
	}
}

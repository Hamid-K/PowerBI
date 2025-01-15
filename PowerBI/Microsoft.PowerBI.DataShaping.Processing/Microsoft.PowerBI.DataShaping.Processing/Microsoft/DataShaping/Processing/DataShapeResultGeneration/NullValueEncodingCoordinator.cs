using System;
using System.Collections;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007A RID: 122
	internal sealed class NullValueEncodingCoordinator
	{
		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600031E RID: 798 RVA: 0x0000A33F File Offset: 0x0000853F
		// (set) Token: 0x0600031F RID: 799 RVA: 0x0000A347 File Offset: 0x00008547
		internal BitArray Flags { get; private set; }

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000A350 File Offset: 0x00008550
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000A358 File Offset: 0x00008558
		internal bool HasNullValues { get; private set; }

		// Token: 0x06000322 RID: 802 RVA: 0x0000A364 File Offset: 0x00008564
		internal void InitializeFlagSequence(int arraySize)
		{
			int num = Math.Min(arraySize, FlagSequenceWriter.MaxFlagSequenceSize);
			if (this.Flags == null || this.Flags.Length != num)
			{
				this.Flags = new BitArray(num);
			}
			else
			{
				this.Flags.SetAll(false);
			}
			this.HasNullValues = false;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A3B4 File Offset: 0x000085B4
		internal bool TryHandleValue(int valueIndex, object value)
		{
			bool flag = valueIndex < this.Flags.Count && value == null;
			if (flag)
			{
				this.HasNullValues = true;
				this.Flags[valueIndex] = true;
			}
			return flag;
		}
	}
}

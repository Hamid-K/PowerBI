using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeResultWriter;

namespace Microsoft.DataShaping.Processing.DataShapeResultGeneration
{
	// Token: 0x0200007B RID: 123
	internal sealed class RepeatedValueEncodingCoordinator
	{
		// Token: 0x06000325 RID: 805 RVA: 0x0000A3EA File Offset: 0x000085EA
		internal RepeatedValueEncodingCoordinator()
		{
			this._ownerIdToPreviousValues = new Dictionary<string, object[]>(StringComparer.Ordinal);
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000A402 File Offset: 0x00008602
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0000A40A File Offset: 0x0000860A
		internal BitArray Flags { get; private set; }

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000328 RID: 808 RVA: 0x0000A413 File Offset: 0x00008613
		// (set) Token: 0x06000329 RID: 809 RVA: 0x0000A41B File Offset: 0x0000861B
		internal bool HasRepeatedValues { get; private set; }

		// Token: 0x0600032A RID: 810 RVA: 0x0000A424 File Offset: 0x00008624
		internal void InitializeFlagSequence(string ownerId, int arraySize)
		{
			if (this._ownerIdToPreviousValues.TryGetValue(ownerId, out this._previousValues))
			{
				this._hasPreviousValues = true;
			}
			else
			{
				this._hasPreviousValues = false;
				this._previousValues = new object[arraySize];
				this._ownerIdToPreviousValues.Add(ownerId, this._previousValues);
			}
			int num = Math.Min(arraySize, FlagSequenceWriter.MaxFlagSequenceSize);
			if (this.Flags == null || this.Flags.Length != num)
			{
				this.Flags = new BitArray(num);
			}
			else
			{
				this.Flags.SetAll(false);
			}
			this.HasRepeatedValues = false;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0000A4B8 File Offset: 0x000086B8
		internal bool TryHandleValue(int valueIndex, object value)
		{
			bool flag = this._hasPreviousValues && valueIndex < this.Flags.Count && object.Equals(value, this._previousValues[valueIndex]);
			if (flag)
			{
				this.HasRepeatedValues = true;
				this.Flags[valueIndex] = true;
				return flag;
			}
			this.UpdatePreviousValue(valueIndex, value);
			return flag;
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0000A50C File Offset: 0x0000870C
		internal void UpdatePreviousValue(int valueIndex, object value)
		{
			this._previousValues[valueIndex] = value;
		}

		// Token: 0x040001CD RID: 461
		private readonly IDictionary<string, object[]> _ownerIdToPreviousValues;

		// Token: 0x040001CE RID: 462
		private object[] _previousValues;

		// Token: 0x040001CF RID: 463
		private bool _hasPreviousValues;
	}
}

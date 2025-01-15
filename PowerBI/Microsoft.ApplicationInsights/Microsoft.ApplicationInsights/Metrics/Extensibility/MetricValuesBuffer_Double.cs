using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200004C RID: 76
	internal sealed class MetricValuesBuffer_Double : MetricValuesBufferBase<double>
	{
		// Token: 0x0600027A RID: 634 RVA: 0x0000CCB4 File Offset: 0x0000AEB4
		public MetricValuesBuffer_Double(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000CCBD File Offset: 0x0000AEBD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override bool IsInvalidValue(double value)
		{
			return double.IsNaN(value);
		}

		// Token: 0x0600027C RID: 636 RVA: 0x0000CCC8 File Offset: 0x0000AEC8
		protected override void ResetValues(double[] values)
		{
			int i = 0;
			while (i < values.Length)
			{
				Interlocked.Exchange(ref values[i++], double.NaN);
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x0000CCF9 File Offset: 0x0000AEF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override double GetAndResetValueOnce(double[] values, int index)
		{
			return Interlocked.Exchange(ref values[index], double.NaN);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x0000CD10 File Offset: 0x0000AF10
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void WriteValueOnce(double[] values, int index, double value)
		{
			Interlocked.Exchange(ref values[index], value);
		}
	}
}

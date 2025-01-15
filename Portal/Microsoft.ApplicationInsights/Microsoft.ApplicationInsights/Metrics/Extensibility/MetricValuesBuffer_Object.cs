using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x0200004D RID: 77
	internal sealed class MetricValuesBuffer_Object : MetricValuesBufferBase<object>
	{
		// Token: 0x0600027F RID: 639 RVA: 0x0000CD20 File Offset: 0x0000AF20
		public MetricValuesBuffer_Object(int capacity)
			: base(capacity)
		{
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000CD29 File Offset: 0x0000AF29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override bool IsInvalidValue(object value)
		{
			return value == null;
		}

		// Token: 0x06000281 RID: 641 RVA: 0x0000CD30 File Offset: 0x0000AF30
		protected override void ResetValues(object[] values)
		{
			int i = 0;
			while (i < values.Length)
			{
				Interlocked.Exchange(ref values[i++], null);
			}
		}

		// Token: 0x06000282 RID: 642 RVA: 0x0000CD59 File Offset: 0x0000AF59
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override object GetAndResetValueOnce(object[] values, int index)
		{
			return Interlocked.Exchange(ref values[index], null);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0000CD68 File Offset: 0x0000AF68
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected override void WriteValueOnce(object[] values, int index, object value)
		{
			Interlocked.Exchange(ref values[index], value);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004B RID: 75
	// (Invoke) Token: 0x0600024D RID: 589
	public delegate void MeasurementCallback<T>([Nullable(1)] Instrument instrument, T measurement, [Nullable(new byte[] { 0, 0, 1, 2 })] ReadOnlySpan<KeyValuePair<string, object>> tags, [Nullable(2)] object state) where T : struct;
}

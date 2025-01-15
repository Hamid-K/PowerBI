using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000055 RID: 85
	[SecuritySafeCritical]
	public sealed class ObservableGauge<T> : ObservableInstrument<T> where T : struct
	{
		// Token: 0x06000292 RID: 658 RVA: 0x0000ABE9 File Offset: 0x00008DE9
		internal ObservableGauge(Meter meter, string name, Func<T> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			this._callback = observeValue;
			base.Publish();
		}

		// Token: 0x06000293 RID: 659 RVA: 0x0000AC12 File Offset: 0x00008E12
		internal ObservableGauge(Meter meter, string name, Func<Measurement<T>> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			this._callback = observeValue;
			base.Publish();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000AC3B File Offset: 0x00008E3B
		internal ObservableGauge(Meter meter, string name, Func<IEnumerable<Measurement<T>>> observeValues, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValues == null)
			{
				throw new ArgumentNullException("observeValues");
			}
			this._callback = observeValues;
			base.Publish();
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000AC64 File Offset: 0x00008E64
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected override IEnumerable<Measurement<T>> Observe()
		{
			return base.Observe(this._callback);
		}

		// Token: 0x0400011E RID: 286
		private object _callback;
	}
}

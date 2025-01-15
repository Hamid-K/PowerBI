using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000054 RID: 84
	[SecuritySafeCritical]
	public sealed class ObservableCounter<T> : ObservableInstrument<T> where T : struct
	{
		// Token: 0x0600028E RID: 654 RVA: 0x0000AB60 File Offset: 0x00008D60
		internal ObservableCounter(Meter meter, string name, Func<T> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			this._callback = observeValue;
			base.Publish();
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000AB89 File Offset: 0x00008D89
		internal ObservableCounter(Meter meter, string name, Func<Measurement<T>> observeValue, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValue == null)
			{
				throw new ArgumentNullException("observeValue");
			}
			this._callback = observeValue;
			base.Publish();
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000ABB2 File Offset: 0x00008DB2
		internal ObservableCounter(Meter meter, string name, Func<IEnumerable<Measurement<T>>> observeValues, string unit, string description)
			: base(meter, name, unit, description)
		{
			if (observeValues == null)
			{
				throw new ArgumentNullException("observeValues");
			}
			this._callback = observeValues;
			base.Publish();
		}

		// Token: 0x06000291 RID: 657 RVA: 0x0000ABDB File Offset: 0x00008DDB
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected override IEnumerable<Measurement<T>> Observe()
		{
			return base.Observe(this._callback);
		}

		// Token: 0x0400011D RID: 285
		private object _callback;
	}
}

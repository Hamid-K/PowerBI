using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000056 RID: 86
	[SecuritySafeCritical]
	public abstract class ObservableInstrument<T> : Instrument where T : struct
	{
		// Token: 0x06000296 RID: 662 RVA: 0x0000AC72 File Offset: 0x00008E72
		[NullableContext(1)]
		protected ObservableInstrument(Meter meter, string name, [Nullable(2)] string unit, [Nullable(2)] string description)
			: base(meter, name, unit, description)
		{
			Instrument.ValidateTypeParameter<T>();
		}

		// Token: 0x06000297 RID: 663
		[return: Nullable(new byte[] { 1, 0, 0 })]
		protected abstract IEnumerable<Measurement<T>> Observe();

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000298 RID: 664 RVA: 0x0000AC84 File Offset: 0x00008E84
		public override bool IsObservable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000299 RID: 665 RVA: 0x0000AC88 File Offset: 0x00008E88
		[SecuritySafeCritical]
		internal override void Observe(MeterListener listener)
		{
			object subscriptionState = base.GetSubscriptionState(listener);
			IEnumerable<Measurement<T>> enumerable = this.Observe();
			if (enumerable == null)
			{
				return;
			}
			foreach (Measurement<T> measurement in enumerable)
			{
				listener.NotifyMeasurement<T>(this, measurement.Value, measurement.Tags, subscriptionState);
			}
		}

		// Token: 0x0600029A RID: 666 RVA: 0x0000ACF4 File Offset: 0x00008EF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal IEnumerable<Measurement<T>> Observe(object callback)
		{
			Func<T> func = callback as Func<T>;
			if (func != null)
			{
				return new Measurement<T>[]
				{
					new Measurement<T>(func())
				};
			}
			Func<Measurement<T>> func2 = callback as Func<Measurement<T>>;
			if (func2 != null)
			{
				return new Measurement<T>[] { func2() };
			}
			Func<IEnumerable<Measurement<T>>> func3 = callback as Func<IEnumerable<Measurement<T>>>;
			if (func3 != null)
			{
				return func3();
			}
			return null;
		}
	}
}

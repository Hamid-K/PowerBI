using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004C RID: 76
	[SecuritySafeCritical]
	public sealed class MeterListener : IDisposable
	{
		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000251 RID: 593 RVA: 0x00009DA5 File Offset: 0x00007FA5
		// (set) Token: 0x06000252 RID: 594 RVA: 0x00009DAD File Offset: 0x00007FAD
		[Nullable(new byte[] { 2, 1, 1 })]
		public Action<Instrument, MeterListener> InstrumentPublished
		{
			[return: Nullable(new byte[] { 2, 1, 1 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 1 })]
			set;
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000253 RID: 595 RVA: 0x00009DB6 File Offset: 0x00007FB6
		// (set) Token: 0x06000254 RID: 596 RVA: 0x00009DBE File Offset: 0x00007FBE
		[Nullable(new byte[] { 2, 1, 2 })]
		public Action<Instrument, object> MeasurementsCompleted
		{
			[return: Nullable(new byte[] { 2, 1, 2 })]
			get;
			[param: Nullable(new byte[] { 2, 1, 2 })]
			set;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00009DC8 File Offset: 0x00007FC8
		[NullableContext(1)]
		public void EnableMeasurementEvents(Instrument instrument, [Nullable(2)] object state = null)
		{
			bool flag = false;
			bool flag2 = false;
			object obj = null;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (instrument != null && !this._disposed && !instrument.Meter.Disposed)
				{
					this._enabledMeasurementInstruments.AddIfNotExist(instrument, (Instrument instrument1, Instrument instrument2) => instrument1 == instrument2);
					obj = instrument.EnableMeasurement(new ListenerSubscription(this, state), out flag);
					flag2 = true;
				}
			}
			if (flag2)
			{
				if (flag && this.MeasurementsCompleted != null)
				{
					Action<Instrument, object> measurementsCompleted = this.MeasurementsCompleted;
					if (measurementsCompleted == null)
					{
						return;
					}
					measurementsCompleted(instrument, obj);
					return;
				}
			}
			else
			{
				Action<Instrument, object> measurementsCompleted2 = this.MeasurementsCompleted;
				if (measurementsCompleted2 == null)
				{
					return;
				}
				measurementsCompleted2(instrument, state);
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00009E94 File Offset: 0x00008094
		[NullableContext(1)]
		[return: Nullable(2)]
		public object DisableMeasurementEvents(Instrument instrument)
		{
			object obj = null;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (instrument != null)
				{
					if (this._enabledMeasurementInstruments.Remove(instrument, (Instrument instrument1, Instrument instrument2) => instrument1 == instrument2) != null)
					{
						obj = instrument.DisableMeasurements(this);
						goto IL_005A;
					}
				}
				return null;
			}
			IL_005A:
			Action<Instrument, object> measurementsCompleted = this.MeasurementsCompleted;
			if (measurementsCompleted != null)
			{
				measurementsCompleted(instrument, obj);
			}
			return obj;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00009F24 File Offset: 0x00008124
		public void SetMeasurementEventCallback<T>([Nullable(new byte[] { 2, 0 })] MeasurementCallback<T> measurementCallback) where T : struct
		{
			MeasurementCallback<byte> measurementCallback2 = measurementCallback as MeasurementCallback<byte>;
			if (measurementCallback2 != null)
			{
				MeasurementCallback<byte> measurementCallback3;
				if (measurementCallback != null)
				{
					measurementCallback3 = measurementCallback2;
				}
				else
				{
					measurementCallback3 = delegate(Instrument instrument, byte measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._byteMeasurementCallback = measurementCallback3;
				return;
			}
			MeasurementCallback<int> measurementCallback4 = measurementCallback as MeasurementCallback<int>;
			if (measurementCallback4 != null)
			{
				MeasurementCallback<int> measurementCallback5;
				if (measurementCallback != null)
				{
					measurementCallback5 = measurementCallback4;
				}
				else
				{
					measurementCallback5 = delegate(Instrument instrument, int measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._intMeasurementCallback = measurementCallback5;
				return;
			}
			MeasurementCallback<float> measurementCallback6 = measurementCallback as MeasurementCallback<float>;
			if (measurementCallback6 != null)
			{
				MeasurementCallback<float> measurementCallback7;
				if (measurementCallback != null)
				{
					measurementCallback7 = measurementCallback6;
				}
				else
				{
					measurementCallback7 = delegate(Instrument instrument, float measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._floatMeasurementCallback = measurementCallback7;
				return;
			}
			MeasurementCallback<double> measurementCallback8 = measurementCallback as MeasurementCallback<double>;
			if (measurementCallback8 != null)
			{
				MeasurementCallback<double> measurementCallback9;
				if (measurementCallback != null)
				{
					measurementCallback9 = measurementCallback8;
				}
				else
				{
					measurementCallback9 = delegate(Instrument instrument, double measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._doubleMeasurementCallback = measurementCallback9;
				return;
			}
			MeasurementCallback<decimal> measurementCallback10 = measurementCallback as MeasurementCallback<decimal>;
			if (measurementCallback10 != null)
			{
				MeasurementCallback<decimal> measurementCallback11;
				if (measurementCallback != null)
				{
					measurementCallback11 = measurementCallback10;
				}
				else
				{
					measurementCallback11 = delegate(Instrument instrument, decimal measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._decimalMeasurementCallback = measurementCallback11;
				return;
			}
			MeasurementCallback<short> measurementCallback12 = measurementCallback as MeasurementCallback<short>;
			if (measurementCallback12 != null)
			{
				MeasurementCallback<short> measurementCallback13;
				if (measurementCallback != null)
				{
					measurementCallback13 = measurementCallback12;
				}
				else
				{
					measurementCallback13 = delegate(Instrument instrument, short measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._shortMeasurementCallback = measurementCallback13;
				return;
			}
			MeasurementCallback<long> measurementCallback14 = measurementCallback as MeasurementCallback<long>;
			if (measurementCallback14 != null)
			{
				MeasurementCallback<long> measurementCallback15;
				if (measurementCallback != null)
				{
					measurementCallback15 = measurementCallback14;
				}
				else
				{
					measurementCallback15 = delegate(Instrument instrument, long measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
					{
					};
				}
				this._longMeasurementCallback = measurementCallback15;
				return;
			}
			throw new InvalidOperationException(SR.Format(SR.UnsupportedType, typeof(T)));
		}

		// Token: 0x06000258 RID: 600 RVA: 0x0000A0D0 File Offset: 0x000082D0
		public void Start()
		{
			List<Instrument> list = null;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (this._disposed)
				{
					return;
				}
				if (!MeterListener.s_allStartedListeners.Contains(this))
				{
					MeterListener.s_allStartedListeners.Add(this);
					list = Meter.GetPublishedInstruments();
				}
			}
			if (list != null)
			{
				foreach (Instrument instrument in list)
				{
					Action<Instrument, MeterListener> instrumentPublished = this.InstrumentPublished;
					if (instrumentPublished != null)
					{
						instrumentPublished(instrument, this);
					}
				}
			}
		}

		// Token: 0x06000259 RID: 601 RVA: 0x0000A184 File Offset: 0x00008384
		public void RecordObservableInstruments()
		{
			List<Exception> list = null;
			for (DiagNode<Instrument> diagNode = this._enabledMeasurementInstruments.First; diagNode != null; diagNode = diagNode.Next)
			{
				if (diagNode.Value.IsObservable)
				{
					try
					{
						diagNode.Value.Observe(this);
					}
					catch (Exception ex)
					{
						if (list == null)
						{
							list = new List<Exception>();
						}
						list.Add(ex);
					}
				}
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x0000A1F4 File Offset: 0x000083F4
		public void Dispose()
		{
			Dictionary<Instrument, object> dictionary = null;
			Action<Instrument, object> measurementsCompleted = this.MeasurementsCompleted;
			object syncObject = Instrument.SyncObject;
			lock (syncObject)
			{
				if (this._disposed)
				{
					return;
				}
				this._disposed = true;
				MeterListener.s_allStartedListeners.Remove(this);
				DiagNode<Instrument> diagNode = this._enabledMeasurementInstruments.First;
				if (diagNode != null && measurementsCompleted != null)
				{
					dictionary = new Dictionary<Instrument, object>();
					do
					{
						object obj = diagNode.Value.DisableMeasurements(this);
						dictionary.Add(diagNode.Value, obj);
						diagNode = diagNode.Next;
					}
					while (diagNode != null);
					this._enabledMeasurementInstruments.Clear();
				}
			}
			if (dictionary != null)
			{
				foreach (KeyValuePair<Instrument, object> keyValuePair in dictionary)
				{
					if (measurementsCompleted != null)
					{
						measurementsCompleted(keyValuePair.Key, keyValuePair.Value);
					}
				}
			}
		}

		// Token: 0x0600025B RID: 603 RVA: 0x0000A2F8 File Offset: 0x000084F8
		internal static List<MeterListener> GetAllListeners()
		{
			if (MeterListener.s_allStartedListeners.Count != 0)
			{
				return new List<MeterListener>(MeterListener.s_allStartedListeners);
			}
			return null;
		}

		// Token: 0x0600025C RID: 604 RVA: 0x0000A314 File Offset: 0x00008514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void NotifyMeasurement<T>(Instrument instrument, T measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state) where T : struct
		{
			if (typeof(T) == typeof(byte))
			{
				this._byteMeasurementCallback(instrument, (byte)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(short))
			{
				this._shortMeasurementCallback(instrument, (short)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(int))
			{
				this._intMeasurementCallback(instrument, (int)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(long))
			{
				this._longMeasurementCallback(instrument, (long)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(float))
			{
				this._floatMeasurementCallback(instrument, (float)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(double))
			{
				this._doubleMeasurementCallback(instrument, (double)((object)measurement), tags, state);
			}
			if (typeof(T) == typeof(decimal))
			{
				this._decimalMeasurementCallback(instrument, (decimal)((object)measurement), tags, state);
			}
		}

		// Token: 0x04000106 RID: 262
		private static List<MeterListener> s_allStartedListeners = new List<MeterListener>();

		// Token: 0x04000107 RID: 263
		private DiagLinkedList<Instrument> _enabledMeasurementInstruments = new DiagLinkedList<Instrument>();

		// Token: 0x04000108 RID: 264
		private bool _disposed;

		// Token: 0x04000109 RID: 265
		private MeasurementCallback<byte> _byteMeasurementCallback = delegate(Instrument instrument, byte measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010A RID: 266
		private MeasurementCallback<short> _shortMeasurementCallback = delegate(Instrument instrument, short measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010B RID: 267
		private MeasurementCallback<int> _intMeasurementCallback = delegate(Instrument instrument, int measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010C RID: 268
		private MeasurementCallback<long> _longMeasurementCallback = delegate(Instrument instrument, long measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010D RID: 269
		private MeasurementCallback<float> _floatMeasurementCallback = delegate(Instrument instrument, float measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010E RID: 270
		private MeasurementCallback<double> _doubleMeasurementCallback = delegate(Instrument instrument, double measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};

		// Token: 0x0400010F RID: 271
		private MeasurementCallback<decimal> _decimalMeasurementCallback = delegate(Instrument instrument, decimal measurement, ReadOnlySpan<KeyValuePair<string, object>> tags, object state)
		{
		};
	}
}

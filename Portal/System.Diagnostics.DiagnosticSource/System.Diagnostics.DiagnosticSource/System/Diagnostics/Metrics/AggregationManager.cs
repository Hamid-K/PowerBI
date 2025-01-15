using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Security;
using System.Threading;

namespace System.Diagnostics.Metrics
{
	// Token: 0x02000032 RID: 50
	[UnsupportedOSPlatform("browser")]
	[SecuritySafeCritical]
	internal sealed class AggregationManager
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00007AA4 File Offset: 0x00005CA4
		public AggregationManager(int maxTimeSeries, int maxHistograms, Action<Instrument, LabeledAggregationStatistics> collectMeasurement, Action<DateTime, DateTime> beginCollection, Action<DateTime, DateTime> endCollection, Action<Instrument> beginInstrumentMeasurements, Action<Instrument> endInstrumentMeasurements, Action<Instrument> instrumentPublished, Action initialInstrumentEnumerationComplete, Action<Exception> collectionError, Action timeSeriesLimitReached, Action histogramLimitReached, Action<Exception> observableInstrumentCallbackError)
		{
			this._maxTimeSeries = maxTimeSeries;
			this._maxHistograms = maxHistograms;
			this._collectMeasurement = collectMeasurement;
			this._beginCollection = beginCollection;
			this._endCollection = endCollection;
			this._beginInstrumentMeasurements = beginInstrumentMeasurements;
			this._endInstrumentMeasurements = endInstrumentMeasurements;
			this._instrumentPublished = instrumentPublished;
			this._initialInstrumentEnumerationComplete = initialInstrumentEnumerationComplete;
			this._collectionError = collectionError;
			this._timeSeriesLimitReached = timeSeriesLimitReached;
			this._histogramLimitReached = histogramLimitReached;
			this._observableInstrumentCallbackError = observableInstrumentCallbackError;
			this._listener = new MeterListener
			{
				InstrumentPublished = delegate(Instrument instrument, MeterListener listener)
				{
					this._instrumentPublished(instrument);
					InstrumentState instrumentState = this.GetInstrumentState(instrument);
					if (instrumentState != null)
					{
						this._beginInstrumentMeasurements(instrument);
						listener.EnableMeasurementEvents(instrument, instrumentState);
					}
				},
				MeasurementsCompleted = delegate(Instrument instrument, object cookie)
				{
					this._endInstrumentMeasurements(instrument);
					this.RemoveInstrumentState(instrument, (InstrumentState)cookie);
				}
			};
			this._listener.SetMeasurementEventCallback<double>(delegate(Instrument i, double m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update(m, l);
			});
			this._listener.SetMeasurementEventCallback<float>(delegate(Instrument i, float m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
			this._listener.SetMeasurementEventCallback<long>(delegate(Instrument i, long m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
			this._listener.SetMeasurementEventCallback<int>(delegate(Instrument i, int m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
			this._listener.SetMeasurementEventCallback<short>(delegate(Instrument i, short m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
			this._listener.SetMeasurementEventCallback<byte>(delegate(Instrument i, byte m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
			this._listener.SetMeasurementEventCallback<decimal>(delegate(Instrument i, decimal m, ReadOnlySpan<KeyValuePair<string, object>> l, object c)
			{
				((InstrumentState)c).Update((double)m, l);
			});
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00007C94 File Offset: 0x00005E94
		public void Include(string meterName)
		{
			this.Include((Instrument i) => i.Meter.Name == meterName);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007CC0 File Offset: 0x00005EC0
		public void Include(string meterName, string instrumentName)
		{
			this.Include((Instrument i) => i.Meter.Name == meterName && i.Name == instrumentName);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007CF4 File Offset: 0x00005EF4
		private void Include(Predicate<Instrument> instrumentFilter)
		{
			lock (this)
			{
				this._instrumentConfigFuncs.Add(instrumentFilter);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007D38 File Offset: 0x00005F38
		public AggregationManager SetCollectionPeriod(TimeSpan collectionPeriod)
		{
			lock (this)
			{
				this._collectionPeriod = collectionPeriod;
			}
			return this;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007D78 File Offset: 0x00005F78
		public void Start()
		{
			this._collectThread = new Thread(delegate
			{
				this.CollectWorker(this._cts.Token);
			});
			this._collectThread.IsBackground = true;
			this._collectThread.Name = "MetricsEventSource CollectWorker";
			this._collectThread.Start();
			this._listener.Start();
			this._initialInstrumentEnumerationComplete();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007DDC File Offset: 0x00005FDC
		private void CollectWorker(CancellationToken cancelToken)
		{
			try
			{
				double num = -1.0;
				lock (this)
				{
					num = this._collectionPeriod.TotalSeconds;
				}
				DateTime utcNow = DateTime.UtcNow;
				DateTime dateTime = utcNow;
				while (!cancelToken.IsCancellationRequested)
				{
					DateTime utcNow2 = DateTime.UtcNow;
					double totalSeconds = (utcNow2 - utcNow).TotalSeconds;
					double num2 = Math.Ceiling(totalSeconds / num) * num;
					DateTime dateTime2 = utcNow.AddSeconds(num2);
					DateTime dateTime3 = dateTime.AddSeconds(num);
					if (dateTime2 <= dateTime3)
					{
						dateTime2 = dateTime3;
					}
					TimeSpan timeSpan = dateTime2 - utcNow2;
					if (cancelToken.WaitHandle.WaitOne(timeSpan))
					{
						break;
					}
					this._beginCollection(dateTime, dateTime2);
					this.Collect();
					this._endCollection(dateTime, dateTime2);
					dateTime = dateTime2;
				}
			}
			catch (Exception ex)
			{
				this._collectionError(ex);
			}
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00007EEC File Offset: 0x000060EC
		public void Dispose()
		{
			this._cts.Cancel();
			if (this._collectThread != null)
			{
				this._collectThread.Join();
				this._collectThread = null;
			}
			this._listener.Dispose();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00007F20 File Offset: 0x00006120
		private void RemoveInstrumentState(Instrument instrument, InstrumentState state)
		{
			InstrumentState instrumentState;
			this._instrumentStates.TryRemove(instrument, out instrumentState);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00007F3C File Offset: 0x0000613C
		private InstrumentState GetInstrumentState(Instrument instrument)
		{
			InstrumentState instrumentState;
			if (!this._instrumentStates.TryGetValue(instrument, out instrumentState))
			{
				lock (this)
				{
					foreach (Predicate<Instrument> predicate in this._instrumentConfigFuncs)
					{
						if (predicate(instrument))
						{
							instrumentState = this.BuildInstrumentState(instrument);
							if (instrumentState != null)
							{
								this._instrumentStates.TryAdd(instrument, instrumentState);
								this._instrumentStates.TryGetValue(instrument, out instrumentState);
								break;
							}
							break;
						}
					}
				}
			}
			return instrumentState;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00007FF0 File Offset: 0x000061F0
		internal InstrumentState BuildInstrumentState(Instrument instrument)
		{
			Func<Aggregator> aggregatorFactory = this.GetAggregatorFactory(instrument);
			if (aggregatorFactory == null)
			{
				return null;
			}
			Type type = aggregatorFactory.GetType().GenericTypeArguments[0];
			Type type2 = typeof(InstrumentState<>).MakeGenericType(new Type[] { type });
			return (InstrumentState)Activator.CreateInstance(type2, new object[] { aggregatorFactory });
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00008048 File Offset: 0x00006248
		private Func<Aggregator> GetAggregatorFactory(Instrument instrument)
		{
			Type type = instrument.GetType();
			Type type2 = (type.IsGenericType ? type.GetGenericTypeDefinition() : null);
			if (type2 == typeof(Counter<>))
			{
				return delegate
				{
					Aggregator aggregator;
					lock (this)
					{
						aggregator = (this.CheckTimeSeriesAllowed() ? new RateSumAggregator() : null);
					}
					return aggregator;
				};
			}
			if (type2 == typeof(ObservableCounter<>))
			{
				return delegate
				{
					Aggregator aggregator2;
					lock (this)
					{
						aggregator2 = (this.CheckTimeSeriesAllowed() ? new RateAggregator() : null);
					}
					return aggregator2;
				};
			}
			if (type2 == typeof(ObservableGauge<>))
			{
				return delegate
				{
					Aggregator aggregator3;
					lock (this)
					{
						aggregator3 = (this.CheckTimeSeriesAllowed() ? new LastValue() : null);
					}
					return aggregator3;
				};
			}
			if (type2 == typeof(Histogram<>))
			{
				return delegate
				{
					Aggregator aggregator4;
					lock (this)
					{
						aggregator4 = ((!this.CheckTimeSeriesAllowed() || !this.CheckHistogramAllowed()) ? null : new ExponentialHistogramAggregator(AggregationManager.s_defaultHistogramConfig));
					}
					return aggregator4;
				};
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000080F0 File Offset: 0x000062F0
		private bool CheckTimeSeriesAllowed()
		{
			if (this._currentTimeSeries < this._maxTimeSeries)
			{
				this._currentTimeSeries++;
				return true;
			}
			if (this._currentTimeSeries == this._maxTimeSeries)
			{
				this._currentTimeSeries++;
				this._timeSeriesLimitReached();
				return false;
			}
			return false;
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00008148 File Offset: 0x00006348
		private bool CheckHistogramAllowed()
		{
			if (this._currentHistograms < this._maxHistograms)
			{
				this._currentHistograms++;
				return true;
			}
			if (this._currentHistograms == this._maxHistograms)
			{
				this._currentHistograms++;
				this._histogramLimitReached();
				return false;
			}
			return false;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000081A0 File Offset: 0x000063A0
		internal void Collect()
		{
			try
			{
				this._listener.RecordObservableInstruments();
			}
			catch (Exception ex)
			{
				this._observableInstrumentCallbackError(ex);
			}
			using (IEnumerator<KeyValuePair<Instrument, InstrumentState>> enumerator = this._instrumentStates.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					KeyValuePair<Instrument, InstrumentState> kv = enumerator.Current;
					kv.Value.Collect(kv.Key, delegate(LabeledAggregationStatistics labeledAggStats)
					{
						this._collectMeasurement(kv.Key, labeledAggStats);
					});
				}
			}
		}

		// Token: 0x040000BF RID: 191
		public const double MinCollectionTimeSecs = 0.1;

		// Token: 0x040000C0 RID: 192
		private static readonly QuantileAggregation s_defaultHistogramConfig = new QuantileAggregation(new double[] { 0.5, 0.95, 0.99 });

		// Token: 0x040000C1 RID: 193
		private readonly List<Predicate<Instrument>> _instrumentConfigFuncs = new List<Predicate<Instrument>>();

		// Token: 0x040000C2 RID: 194
		private TimeSpan _collectionPeriod;

		// Token: 0x040000C3 RID: 195
		private readonly ConcurrentDictionary<Instrument, InstrumentState> _instrumentStates = new ConcurrentDictionary<Instrument, InstrumentState>();

		// Token: 0x040000C4 RID: 196
		private readonly CancellationTokenSource _cts = new CancellationTokenSource();

		// Token: 0x040000C5 RID: 197
		private Thread _collectThread;

		// Token: 0x040000C6 RID: 198
		private readonly MeterListener _listener;

		// Token: 0x040000C7 RID: 199
		private int _currentTimeSeries;

		// Token: 0x040000C8 RID: 200
		private int _currentHistograms;

		// Token: 0x040000C9 RID: 201
		private readonly int _maxTimeSeries;

		// Token: 0x040000CA RID: 202
		private readonly int _maxHistograms;

		// Token: 0x040000CB RID: 203
		private readonly Action<Instrument, LabeledAggregationStatistics> _collectMeasurement;

		// Token: 0x040000CC RID: 204
		private readonly Action<DateTime, DateTime> _beginCollection;

		// Token: 0x040000CD RID: 205
		private readonly Action<DateTime, DateTime> _endCollection;

		// Token: 0x040000CE RID: 206
		private readonly Action<Instrument> _beginInstrumentMeasurements;

		// Token: 0x040000CF RID: 207
		private readonly Action<Instrument> _endInstrumentMeasurements;

		// Token: 0x040000D0 RID: 208
		private readonly Action<Instrument> _instrumentPublished;

		// Token: 0x040000D1 RID: 209
		private readonly Action _initialInstrumentEnumerationComplete;

		// Token: 0x040000D2 RID: 210
		private readonly Action<Exception> _collectionError;

		// Token: 0x040000D3 RID: 211
		private readonly Action _timeSeriesLimitReached;

		// Token: 0x040000D4 RID: 212
		private readonly Action _histogramLimitReached;

		// Token: 0x040000D5 RID: 213
		private readonly Action<Exception> _observableInstrumentCallbackError;
	}
}

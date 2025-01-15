using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;

namespace System.Diagnostics.Metrics
{
	// Token: 0x0200004E RID: 78
	[EventSource(Name = "System.Diagnostics.Metrics")]
	internal sealed class MetricsEventSource : EventSource
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000A4C0 File Offset: 0x000086C0
		private MetricsEventSource.CommandHandler Handler
		{
			get
			{
				if (this._handler == null)
				{
					Interlocked.CompareExchange<MetricsEventSource.CommandHandler>(ref this._handler, new MetricsEventSource.CommandHandler(this), null);
				}
				return this._handler;
			}
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000A4E3 File Offset: 0x000086E3
		private MetricsEventSource()
		{
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000A4EB File Offset: 0x000086EB
		[Event(1, Keywords = (EventKeywords)1L)]
		public void Message(string Message)
		{
			base.WriteEvent(1, Message);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000A4F5 File Offset: 0x000086F5
		[Event(2, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CollectionStart(string sessionId, DateTime intervalStartTime, DateTime intervalEndTime)
		{
			base.WriteEvent(2, new object[] { sessionId, intervalStartTime, intervalEndTime });
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000A51A File Offset: 0x0000871A
		[Event(3, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CollectionStop(string sessionId, DateTime intervalStartTime, DateTime intervalEndTime)
		{
			base.WriteEvent(3, new object[] { sessionId, intervalStartTime, intervalEndTime });
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000A540 File Offset: 0x00008740
		[Event(4, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void CounterRateValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string rate)
		{
			base.WriteEvent(4, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				unit ?? "",
				tags,
				rate
			});
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000A58C File Offset: 0x0000878C
		[Event(5, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void GaugeValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string lastValue)
		{
			base.WriteEvent(5, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				unit ?? "",
				tags,
				lastValue
			});
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000A5D8 File Offset: 0x000087D8
		[Event(6, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void HistogramValuePublished(string sessionId, string meterName, string meterVersion, string instrumentName, string unit, string tags, string quantiles)
		{
			base.WriteEvent(6, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				unit ?? "",
				tags,
				quantiles
			});
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000A624 File Offset: 0x00008824
		[Event(7, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void BeginInstrumentReporting(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			base.WriteEvent(7, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				instrumentType,
				unit ?? "",
				description ?? ""
			});
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000A67C File Offset: 0x0000887C
		[Event(8, Keywords = (EventKeywords)2L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void EndInstrumentReporting(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			base.WriteEvent(8, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				instrumentType,
				unit ?? "",
				description ?? ""
			});
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000A6D1 File Offset: 0x000088D1
		[Event(9, Keywords = (EventKeywords)7L)]
		public void Error(string sessionId, string errorMessage)
		{
			base.WriteEvent(9, sessionId, errorMessage);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000A6DD File Offset: 0x000088DD
		[Event(10, Keywords = (EventKeywords)6L)]
		public void InitialInstrumentEnumerationComplete(string sessionId)
		{
			base.WriteEvent(10, sessionId);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000A6E8 File Offset: 0x000088E8
		[Event(11, Keywords = (EventKeywords)4L)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "This calls WriteEvent with all primitive arguments which is safe. Primitives are always serialized properly.")]
		public void InstrumentPublished(string sessionId, string meterName, string meterVersion, string instrumentName, string instrumentType, string unit, string description)
		{
			base.WriteEvent(11, new object[]
			{
				sessionId,
				meterName,
				meterVersion ?? "",
				instrumentName,
				instrumentType,
				unit ?? "",
				description ?? ""
			});
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000A73E File Offset: 0x0000893E
		[Event(12, Keywords = (EventKeywords)2L)]
		public void TimeSeriesLimitReached(string sessionId)
		{
			base.WriteEvent(12, sessionId);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A749 File Offset: 0x00008949
		[Event(13, Keywords = (EventKeywords)2L)]
		public void HistogramLimitReached(string sessionId)
		{
			base.WriteEvent(13, sessionId);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A754 File Offset: 0x00008954
		[Event(14, Keywords = (EventKeywords)2L)]
		public void ObservableInstrumentCallbackError(string sessionId, string errorMessage)
		{
			base.WriteEvent(14, sessionId, errorMessage);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000A760 File Offset: 0x00008960
		[Event(15, Keywords = (EventKeywords)7L)]
		public void MultipleSessionsNotSupportedError(string runningSessionId)
		{
			base.WriteEvent(15, runningSessionId);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000A76C File Offset: 0x0000896C
		[NonEvent]
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			lock (this)
			{
				this.Handler.OnEventCommand(command);
			}
		}

		// Token: 0x04000114 RID: 276
		public static readonly MetricsEventSource Log = new MetricsEventSource();

		// Token: 0x04000115 RID: 277
		private MetricsEventSource.CommandHandler _handler;

		// Token: 0x02000099 RID: 153
		public static class Keywords
		{
			// Token: 0x040001D6 RID: 470
			public const EventKeywords Messages = (EventKeywords)1L;

			// Token: 0x040001D7 RID: 471
			public const EventKeywords TimeSeriesValues = (EventKeywords)2L;

			// Token: 0x040001D8 RID: 472
			public const EventKeywords InstrumentPublishing = (EventKeywords)4L;
		}

		// Token: 0x0200009A RID: 154
		private sealed class CommandHandler
		{
			// Token: 0x060003E3 RID: 995 RVA: 0x0000D807 File Offset: 0x0000BA07
			public CommandHandler(MetricsEventSource parent)
			{
				this.Parent = parent;
			}

			// Token: 0x170000CF RID: 207
			// (get) Token: 0x060003E4 RID: 996 RVA: 0x0000D821 File Offset: 0x0000BA21
			// (set) Token: 0x060003E5 RID: 997 RVA: 0x0000D829 File Offset: 0x0000BA29
			public MetricsEventSource Parent { get; private set; }

			// Token: 0x060003E6 RID: 998 RVA: 0x0000D834 File Offset: 0x0000BA34
			public void OnEventCommand(EventCommandEventArgs command)
			{
				try
				{
					if (command.Command == EventCommand.Update || command.Command == EventCommand.Disable || command.Command == EventCommand.Enable)
					{
						if (this._aggregationManager != null)
						{
							if (command.Command == EventCommand.Enable || command.Command == EventCommand.Update)
							{
								this.Parent.MultipleSessionsNotSupportedError(this._sessionId);
								return;
							}
							this._aggregationManager.Dispose();
							this._aggregationManager = null;
							this.Parent.Message("Previous session with id " + this._sessionId + " is stopped");
						}
						this._sessionId = "";
					}
					if ((command.Command == EventCommand.Update || command.Command == EventCommand.Enable) && command.Arguments != null)
					{
						string text;
						if (command.Arguments.TryGetValue("SessionId", out text))
						{
							this._sessionId = text;
							this.Parent.Message("SessionId argument received: " + this._sessionId);
						}
						else
						{
							this._sessionId = Guid.NewGuid().ToString();
							this.Parent.Message("New session started. SessionId auto-generated: " + this._sessionId);
						}
						double num = 1.0;
						double num2 = num;
						string text2;
						if (command.Arguments.TryGetValue("RefreshInterval", out text2))
						{
							this.Parent.Message("RefreshInterval argument received: " + text2);
							if (!double.TryParse(text2, out num2))
							{
								this.Parent.Message(string.Format("Failed to parse RefreshInterval. Using default {0}s.", num));
								num2 = num;
							}
							else if (num2 < 0.1)
							{
								this.Parent.Message(string.Format("RefreshInterval too small. Using minimum interval {0} seconds.", 0.1));
								num2 = 0.1;
							}
						}
						else
						{
							this.Parent.Message(string.Format("No RefreshInterval argument received. Using default {0}s.", num));
							num2 = num;
						}
						int num3 = 1000;
						string text3;
						int num4;
						if (command.Arguments.TryGetValue("MaxTimeSeries", out text3))
						{
							this.Parent.Message("MaxTimeSeries argument received: " + text3);
							if (!int.TryParse(text3, out num4))
							{
								this.Parent.Message(string.Format("Failed to parse MaxTimeSeries. Using default {0}", num3));
								num4 = num3;
							}
						}
						else
						{
							this.Parent.Message(string.Format("No MaxTimeSeries argument received. Using default {0}", num3));
							num4 = num3;
						}
						int num5 = 20;
						string text4;
						int num6;
						if (command.Arguments.TryGetValue("MaxHistograms", out text4))
						{
							this.Parent.Message("MaxHistograms argument received: " + text4);
							if (!int.TryParse(text4, out num6))
							{
								this.Parent.Message(string.Format("Failed to parse MaxHistograms. Using default {0}", num5));
								num6 = num5;
							}
						}
						else
						{
							this.Parent.Message(string.Format("No MaxHistogram argument received. Using default {0}", num5));
							num6 = num5;
						}
						string sessionId = this._sessionId;
						this._aggregationManager = new AggregationManager(num4, num6, delegate(Instrument i, LabeledAggregationStatistics s)
						{
							this.TransmitMetricValue(i, s, sessionId);
						}, delegate(DateTime startIntervalTime, DateTime endIntervalTime)
						{
							this.Parent.CollectionStart(sessionId, startIntervalTime, endIntervalTime);
						}, delegate(DateTime startIntervalTime, DateTime endIntervalTime)
						{
							this.Parent.CollectionStop(sessionId, startIntervalTime, endIntervalTime);
						}, delegate(Instrument i)
						{
							this.Parent.BeginInstrumentReporting(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
						}, delegate(Instrument i)
						{
							this.Parent.EndInstrumentReporting(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
						}, delegate(Instrument i)
						{
							this.Parent.InstrumentPublished(sessionId, i.Meter.Name, i.Meter.Version, i.Name, i.GetType().Name, i.Unit, i.Description);
						}, delegate
						{
							this.Parent.InitialInstrumentEnumerationComplete(sessionId);
						}, delegate(Exception e)
						{
							this.Parent.Error(sessionId, e.ToString());
						}, delegate
						{
							this.Parent.TimeSeriesLimitReached(sessionId);
						}, delegate
						{
							this.Parent.HistogramLimitReached(sessionId);
						}, delegate(Exception e)
						{
							this.Parent.ObservableInstrumentCallbackError(sessionId, e.ToString());
						});
						this._aggregationManager.SetCollectionPeriod(TimeSpan.FromSeconds(num2));
						string text5;
						if (command.Arguments.TryGetValue("Metrics", out text5))
						{
							this.Parent.Message("Metrics argument received: " + text5);
							this.ParseSpecs(text5);
						}
						else
						{
							this.Parent.Message("No Metrics argument received");
						}
						this._aggregationManager.Start();
					}
				}
				catch (Exception ex) when (this.LogError(ex))
				{
				}
			}

			// Token: 0x060003E7 RID: 999 RVA: 0x0000DC58 File Offset: 0x0000BE58
			private bool LogError(Exception e)
			{
				this.Parent.Error(this._sessionId, e.ToString());
				return false;
			}

			// Token: 0x060003E8 RID: 1000 RVA: 0x0000DC74 File Offset: 0x0000BE74
			[UnsupportedOSPlatform("browser")]
			private void ParseSpecs(string metricsSpecs)
			{
				if (metricsSpecs == null)
				{
					return;
				}
				string[] array = metricsSpecs.Split(MetricsEventSource.CommandHandler.s_instrumentSeperators, StringSplitOptions.RemoveEmptyEntries);
				foreach (string text in array)
				{
					MetricsEventSource.MetricSpec metricSpec;
					if (!MetricsEventSource.MetricSpec.TryParse(text, out metricSpec))
					{
						this.Parent.Message("Failed to parse metric spec: " + text);
					}
					else
					{
						this.Parent.Message(string.Format("Parsed metric: {0}", metricSpec));
						if (metricSpec.InstrumentName != null)
						{
							this._aggregationManager.Include(metricSpec.MeterName, metricSpec.InstrumentName);
						}
						else
						{
							this._aggregationManager.Include(metricSpec.MeterName);
						}
					}
				}
			}

			// Token: 0x060003E9 RID: 1001 RVA: 0x0000DD18 File Offset: 0x0000BF18
			private void TransmitMetricValue(Instrument instrument, LabeledAggregationStatistics stats, string sessionId)
			{
				RateStatistics rateStatistics = stats.AggregationStatistics as RateStatistics;
				if (rateStatistics != null)
				{
					MetricsEventSource.Log.CounterRateValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, this.FormatTags(stats.Labels), (rateStatistics.Delta != null) ? rateStatistics.Delta.Value.ToString(CultureInfo.InvariantCulture) : "");
					return;
				}
				LastValueStatistics lastValueStatistics = stats.AggregationStatistics as LastValueStatistics;
				if (lastValueStatistics != null)
				{
					MetricsEventSource.Log.GaugeValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, this.FormatTags(stats.Labels), (lastValueStatistics.LastValue != null) ? lastValueStatistics.LastValue.Value.ToString(CultureInfo.InvariantCulture) : "");
					return;
				}
				HistogramStatistics histogramStatistics = stats.AggregationStatistics as HistogramStatistics;
				if (histogramStatistics != null)
				{
					MetricsEventSource.Log.HistogramValuePublished(sessionId, instrument.Meter.Name, instrument.Meter.Version, instrument.Name, instrument.Unit, this.FormatTags(stats.Labels), this.FormatQuantiles(histogramStatistics.Quantiles));
				}
			}

			// Token: 0x060003EA RID: 1002 RVA: 0x0000DE74 File Offset: 0x0000C074
			private string FormatTags(KeyValuePair<string, string>[] labels)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < labels.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", labels[i].Key, labels[i].Value);
					if (i != labels.Length - 1)
					{
						stringBuilder.Append(',');
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060003EB RID: 1003 RVA: 0x0000DED8 File Offset: 0x0000C0D8
			private string FormatQuantiles(QuantileValue[] quantiles)
			{
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < quantiles.Length; i++)
				{
					stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "{0}={1}", quantiles[i].Quantile, quantiles[i].Value);
					if (i != quantiles.Length - 1)
					{
						stringBuilder.Append(';');
					}
				}
				return stringBuilder.ToString();
			}

			// Token: 0x040001D9 RID: 473
			private AggregationManager _aggregationManager;

			// Token: 0x040001DA RID: 474
			private string _sessionId = "";

			// Token: 0x040001DC RID: 476
			private static readonly char[] s_instrumentSeperators = new char[] { '\r', '\n', ',', ';' };
		}

		// Token: 0x0200009B RID: 155
		private class MetricSpec
		{
			// Token: 0x170000D0 RID: 208
			// (get) Token: 0x060003ED RID: 1005 RVA: 0x0000DF5B File Offset: 0x0000C15B
			// (set) Token: 0x060003EE RID: 1006 RVA: 0x0000DF63 File Offset: 0x0000C163
			public string MeterName { get; private set; }

			// Token: 0x170000D1 RID: 209
			// (get) Token: 0x060003EF RID: 1007 RVA: 0x0000DF6C File Offset: 0x0000C16C
			// (set) Token: 0x060003F0 RID: 1008 RVA: 0x0000DF74 File Offset: 0x0000C174
			public string InstrumentName { get; private set; }

			// Token: 0x060003F1 RID: 1009 RVA: 0x0000DF7D File Offset: 0x0000C17D
			public MetricSpec(string meterName, string instrumentName)
			{
				this.MeterName = meterName;
				this.InstrumentName = instrumentName;
			}

			// Token: 0x060003F2 RID: 1010 RVA: 0x0000DF94 File Offset: 0x0000C194
			public static bool TryParse(string text, out MetricsEventSource.MetricSpec spec)
			{
				int num = text.IndexOf('\\');
				if (num == -1)
				{
					spec = new MetricsEventSource.MetricSpec(text.Trim(), null);
					return true;
				}
				string text2 = text.Substring(0, num).Trim();
				string text3 = text.Substring(num + 1).Trim();
				spec = new MetricsEventSource.MetricSpec(text2, text3);
				return true;
			}

			// Token: 0x060003F3 RID: 1011 RVA: 0x0000DFE5 File Offset: 0x0000C1E5
			public override string ToString()
			{
				if (this.InstrumentName == null)
				{
					return this.MeterName;
				}
				return this.MeterName + "\\" + this.InstrumentName;
			}

			// Token: 0x040001DD RID: 477
			private const char MeterInstrumentSeparator = '\\';
		}
	}
}

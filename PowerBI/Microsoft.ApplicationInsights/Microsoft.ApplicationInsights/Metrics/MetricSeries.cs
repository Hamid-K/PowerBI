using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Threading;
using Microsoft.ApplicationInsights.Metrics.Extensibility;

namespace Microsoft.ApplicationInsights.Metrics
{
	// Token: 0x02000032 RID: 50
	public sealed class MetricSeries
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x0000A178 File Offset: 0x00008378
		internal MetricSeries(MetricAggregationManager aggregationManager, MetricIdentifier metricIdentifier, IEnumerable<KeyValuePair<string, string>> dimensionNamesAndValues, IMetricSeriesConfiguration configuration)
		{
			Util.ValidateNotNull(aggregationManager, "aggregationManager");
			this.aggregationManager = aggregationManager;
			Util.ValidateNotNull(metricIdentifier, "metricIdentifier");
			this.MetricIdentifier = metricIdentifier;
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			if (dimensionNamesAndValues != null)
			{
				int num = 0;
				foreach (KeyValuePair<string, string> keyValuePair in dimensionNamesAndValues)
				{
					if (keyValuePair.Value == null)
					{
						throw new ArgumentNullException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The value for dimension '{0}' number is null.", new object[] { keyValuePair.Key })));
					}
					if (string.IsNullOrWhiteSpace(keyValuePair.Value))
					{
						throw new ArgumentNullException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The value for dimension '{0}' is empty or white-space.", new object[] { keyValuePair.Key })));
					}
					dictionary[keyValuePair.Key] = keyValuePair.Value;
					num++;
				}
			}
			if (metricIdentifier.DimensionsCount != dictionary.Count)
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0} contains {1} dimensions,", new object[] { "metricIdentifier", metricIdentifier.DimensionsCount })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" however the specified {0} contains {1} name-value pairs with unique names.", new object[] { "dimensionNamesAndValues", dictionary.Count })));
			}
			foreach (string text in metricIdentifier.GetDimensionNames())
			{
				if (!dictionary.ContainsKey(text))
				{
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("The specified {0} contains a dimension named \"{1}\",", new object[] { "metricIdentifier", text })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" however the specified {0} does not contain an entry for that name.", new object[] { "dimensionNamesAndValues" })));
				}
			}
			this.dimensionNamesAndValues = dictionary;
			Util.ValidateNotNull(configuration, "configuration");
			this.configuration = configuration;
			this.requiresPersistentAggregator = configuration.RequiresPersistentAggregation;
			this.aggregatorPersistent = null;
			this.aggregatorDefault = null;
			this.aggregatorQuickPulse = null;
			this.aggregatorCustom = null;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public IReadOnlyDictionary<string, string> DimensionNamesAndValues
		{
			get
			{
				return this.dimensionNamesAndValues;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x0000A3BC File Offset: 0x000085BC
		public MetricIdentifier MetricIdentifier { get; }

		// Token: 0x060001D3 RID: 467 RVA: 0x0000A3C4 File Offset: 0x000085C4
		public void TrackValue(double metricValue)
		{
			List<Exception> list = null;
			if (this.requiresPersistentAggregator)
			{
				MetricSeries.TrackValue(this.GetOrCreatePersistentAggregator(), metricValue, ref list);
			}
			else
			{
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.Default, ref this.aggregatorDefault), metricValue, ref list);
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.QuickPulse, ref this.aggregatorQuickPulse), metricValue, ref list);
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.Custom, ref this.aggregatorCustom), metricValue, ref list);
			}
			if (list == null)
			{
				return;
			}
			if (list.Count == 1)
			{
				ExceptionDispatchInfo.Capture(list[0]).Throw();
				return;
			}
			throw new AggregateException(list);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000A450 File Offset: 0x00008650
		public void TrackValue(object metricValue)
		{
			List<Exception> list = null;
			if (this.requiresPersistentAggregator)
			{
				MetricSeries.TrackValue(this.GetOrCreatePersistentAggregator(), metricValue, ref list);
			}
			else
			{
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.Default, ref this.aggregatorDefault), metricValue, ref list);
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.QuickPulse, ref this.aggregatorQuickPulse), metricValue, ref list);
				MetricSeries.TrackValue(this.GetOrCreateAggregator(MetricAggregationCycleKind.Custom, ref this.aggregatorCustom), metricValue, ref list);
			}
			if (list == null)
			{
				return;
			}
			if (list.Count == 1)
			{
				ExceptionDispatchInfo.Capture(list[0]).Throw();
				return;
			}
			throw new AggregateException(list);
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000A4DB File Offset: 0x000086DB
		internal void ResetAggregation()
		{
			this.ResetAggregation(DateTimeOffset.Now);
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000A4E8 File Offset: 0x000086E8
		internal void ResetAggregation(DateTimeOffset periodStart)
		{
			periodStart = Util.RoundDownToSecond(periodStart);
			if (this.requiresPersistentAggregator)
			{
				IMetricSeriesAggregator metricSeriesAggregator = this.aggregatorPersistent;
				if (metricSeriesAggregator == null)
				{
					return;
				}
				metricSeriesAggregator.Reset(periodStart);
				return;
			}
			else
			{
				IMetricSeriesAggregator metricSeriesAggregator2 = MetricSeries.UnwrapAggregator(this.aggregatorDefault);
				if (metricSeriesAggregator2 != null)
				{
					metricSeriesAggregator2.Reset(periodStart);
				}
				IMetricSeriesAggregator metricSeriesAggregator3 = MetricSeries.UnwrapAggregator(this.aggregatorQuickPulse);
				if (metricSeriesAggregator3 != null)
				{
					metricSeriesAggregator3.Reset(periodStart);
				}
				IMetricSeriesAggregator metricSeriesAggregator4 = MetricSeries.UnwrapAggregator(this.aggregatorCustom);
				if (metricSeriesAggregator4 == null)
				{
					return;
				}
				metricSeriesAggregator4.Reset(periodStart);
				return;
			}
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000A55B File Offset: 0x0000875B
		internal MetricAggregate GetCurrentAggregateUnsafe()
		{
			return this.GetCurrentAggregateUnsafe(MetricAggregationCycleKind.Default, DateTimeOffset.Now);
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000A56C File Offset: 0x0000876C
		internal MetricAggregate GetCurrentAggregateUnsafe(MetricAggregationCycleKind aggregationCycleKind, DateTimeOffset dateTime)
		{
			IMetricSeriesAggregator metricSeriesAggregator;
			if (this.requiresPersistentAggregator)
			{
				metricSeriesAggregator = this.aggregatorPersistent;
			}
			else
			{
				switch (aggregationCycleKind)
				{
				case MetricAggregationCycleKind.Default:
					metricSeriesAggregator = MetricSeries.UnwrapAggregator(this.aggregatorDefault);
					break;
				case MetricAggregationCycleKind.QuickPulse:
					metricSeriesAggregator = MetricSeries.UnwrapAggregator(this.aggregatorQuickPulse);
					break;
				case MetricAggregationCycleKind.Custom:
					metricSeriesAggregator = MetricSeries.UnwrapAggregator(this.aggregatorCustom);
					break;
				default:
					throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
				}
			}
			dateTime = Util.RoundDownToSecond(dateTime);
			if (metricSeriesAggregator == null)
			{
				return null;
			}
			return metricSeriesAggregator.CreateAggregateUnsafe(dateTime);
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000A60C File Offset: 0x0000880C
		internal void ClearAggregator(MetricAggregationCycleKind aggregationCycleKind)
		{
			if (this.requiresPersistentAggregator)
			{
				return;
			}
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
			{
				WeakReference<IMetricSeriesAggregator> weakReference = Interlocked.Exchange<WeakReference<IMetricSeriesAggregator>>(ref this.aggregatorDefault, null);
				this.aggregatorRecycleCacheDefault = MetricSeries.UnwrapAggregator(weakReference);
				return;
			}
			case MetricAggregationCycleKind.QuickPulse:
			{
				WeakReference<IMetricSeriesAggregator> weakReference = Interlocked.Exchange<WeakReference<IMetricSeriesAggregator>>(ref this.aggregatorQuickPulse, null);
				this.aggregatorRecycleCacheQuickPulse = MetricSeries.UnwrapAggregator(weakReference);
				return;
			}
			case MetricAggregationCycleKind.Custom:
			{
				WeakReference<IMetricSeriesAggregator> weakReference = Interlocked.Exchange<WeakReference<IMetricSeriesAggregator>>(ref this.aggregatorCustom, null);
				this.aggregatorRecycleCacheCustom = MetricSeries.UnwrapAggregator(weakReference);
				return;
			}
			default:
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Unexpected value of {0}: {1}.", new object[] { "aggregationCycleKind", aggregationCycleKind })));
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000A6B0 File Offset: 0x000088B0
		private static void TrackValue(IMetricSeriesAggregator aggregator, double metricValue, ref List<Exception> errors)
		{
			if (aggregator != null)
			{
				try
				{
					aggregator.TrackValue(metricValue);
				}
				catch (Exception ex)
				{
					List<Exception> list;
					errors = (list = errors ?? new List<Exception>());
					list.Add(ex);
				}
			}
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000A6F4 File Offset: 0x000088F4
		private static void TrackValue(IMetricSeriesAggregator aggregator, object metricValue, ref List<Exception> errors)
		{
			if (aggregator != null)
			{
				try
				{
					aggregator.TrackValue(metricValue);
				}
				catch (Exception ex)
				{
					List<Exception> list;
					errors = (list = errors ?? new List<Exception>());
					list.Add(ex);
				}
			}
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000A738 File Offset: 0x00008938
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static IMetricSeriesAggregator UnwrapAggregator(WeakReference<IMetricSeriesAggregator> aggregatorWeakRef)
		{
			if (aggregatorWeakRef != null)
			{
				IMetricSeriesAggregator metricSeriesAggregator = null;
				if (aggregatorWeakRef.TryGetTarget(out metricSeriesAggregator))
				{
					return metricSeriesAggregator;
				}
			}
			return null;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000A758 File Offset: 0x00008958
		private IMetricSeriesAggregator GetOrCreatePersistentAggregator()
		{
			IMetricSeriesAggregator metricSeriesAggregator = this.aggregatorPersistent;
			if (metricSeriesAggregator == null)
			{
				metricSeriesAggregator = this.configuration.CreateNewAggregator(this, MetricAggregationCycleKind.Default);
				IMetricSeriesAggregator metricSeriesAggregator2 = Interlocked.CompareExchange<IMetricSeriesAggregator>(ref this.aggregatorPersistent, metricSeriesAggregator, null);
				if (metricSeriesAggregator2 == null)
				{
					if (!this.aggregationManager.AddAggregator(metricSeriesAggregator, MetricAggregationCycleKind.Default))
					{
						throw new InvalidOperationException("Internal SDK gub. Please report! Info: _aggregationManager.AddAggregator reports false for a PERSISTENT aggregator. This should never happen.");
					}
				}
				else
				{
					metricSeriesAggregator = metricSeriesAggregator2;
				}
			}
			return metricSeriesAggregator;
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000A7AC File Offset: 0x000089AC
		private IMetricSeriesAggregator GetOrCreateAggregator(MetricAggregationCycleKind aggregationCycleKind, ref WeakReference<IMetricSeriesAggregator> aggregatorWeakRef)
		{
			IMetricSeriesAggregator metricSeriesAggregator;
			IMetricSeriesAggregator newOrRecycledAggregatorInstance;
			WeakReference<IMetricSeriesAggregator> weakReference2;
			for (;;)
			{
				WeakReference<IMetricSeriesAggregator> weakReference = aggregatorWeakRef;
				metricSeriesAggregator = MetricSeries.UnwrapAggregator(weakReference);
				if (metricSeriesAggregator != null)
				{
					break;
				}
				if (aggregationCycleKind != MetricAggregationCycleKind.Default)
				{
					IMetricSeriesFilter metricSeriesFilter;
					if (!this.aggregationManager.IsCycleActive(aggregationCycleKind, out metricSeriesFilter))
					{
						goto Block_2;
					}
					IMetricValueFilter metricValueFilter;
					if (!Util.FilterWillConsume(metricSeriesFilter, this, out metricValueFilter))
					{
						goto Block_3;
					}
				}
				newOrRecycledAggregatorInstance = this.GetNewOrRecycledAggregatorInstance(aggregationCycleKind);
				weakReference2 = new WeakReference<IMetricSeriesAggregator>(newOrRecycledAggregatorInstance, false);
				if (Interlocked.CompareExchange<WeakReference<IMetricSeriesAggregator>>(ref aggregatorWeakRef, weakReference2, weakReference) == weakReference)
				{
					goto Block_4;
				}
			}
			return metricSeriesAggregator;
			Block_2:
			return null;
			Block_3:
			return null;
			Block_4:
			if (this.aggregationManager.AddAggregator(newOrRecycledAggregatorInstance, aggregationCycleKind))
			{
				return newOrRecycledAggregatorInstance;
			}
			Interlocked.CompareExchange<WeakReference<IMetricSeriesAggregator>>(ref aggregatorWeakRef, null, weakReference2);
			return null;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000A824 File Offset: 0x00008A24
		private IMetricSeriesAggregator GetNewOrRecycledAggregatorInstance(MetricAggregationCycleKind aggregationCycleKind)
		{
			IMetricSeriesAggregator recycledAggregatorInstance = this.GetRecycledAggregatorInstance(aggregationCycleKind);
			return recycledAggregatorInstance ?? this.configuration.CreateNewAggregator(this, aggregationCycleKind);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000A84C File Offset: 0x00008A4C
		private IMetricSeriesAggregator GetRecycledAggregatorInstance(MetricAggregationCycleKind aggregationCycleKind)
		{
			if (this.requiresPersistentAggregator)
			{
				return null;
			}
			IMetricSeriesAggregator metricSeriesAggregator = null;
			switch (aggregationCycleKind)
			{
			case MetricAggregationCycleKind.Default:
				metricSeriesAggregator = Interlocked.Exchange<IMetricSeriesAggregator>(ref this.aggregatorRecycleCacheDefault, null);
				break;
			case MetricAggregationCycleKind.QuickPulse:
				metricSeriesAggregator = Interlocked.Exchange<IMetricSeriesAggregator>(ref this.aggregatorRecycleCacheQuickPulse, null);
				break;
			case MetricAggregationCycleKind.Custom:
				metricSeriesAggregator = Interlocked.Exchange<IMetricSeriesAggregator>(ref this.aggregatorRecycleCacheCustom, null);
				break;
			}
			if (metricSeriesAggregator == null)
			{
				return null;
			}
			if (metricSeriesAggregator.TryRecycle())
			{
				return metricSeriesAggregator;
			}
			return null;
		}

		// Token: 0x040000D4 RID: 212
		internal readonly IMetricSeriesConfiguration configuration;

		// Token: 0x040000D5 RID: 213
		private readonly MetricAggregationManager aggregationManager;

		// Token: 0x040000D6 RID: 214
		private readonly bool requiresPersistentAggregator;

		// Token: 0x040000D7 RID: 215
		private readonly IReadOnlyDictionary<string, string> dimensionNamesAndValues;

		// Token: 0x040000D8 RID: 216
		private IMetricSeriesAggregator aggregatorPersistent;

		// Token: 0x040000D9 RID: 217
		private WeakReference<IMetricSeriesAggregator> aggregatorDefault;

		// Token: 0x040000DA RID: 218
		private WeakReference<IMetricSeriesAggregator> aggregatorQuickPulse;

		// Token: 0x040000DB RID: 219
		private WeakReference<IMetricSeriesAggregator> aggregatorCustom;

		// Token: 0x040000DC RID: 220
		private IMetricSeriesAggregator aggregatorRecycleCacheDefault;

		// Token: 0x040000DD RID: 221
		private IMetricSeriesAggregator aggregatorRecycleCacheQuickPulse;

		// Token: 0x040000DE RID: 222
		private IMetricSeriesAggregator aggregatorRecycleCacheCustom;
	}
}

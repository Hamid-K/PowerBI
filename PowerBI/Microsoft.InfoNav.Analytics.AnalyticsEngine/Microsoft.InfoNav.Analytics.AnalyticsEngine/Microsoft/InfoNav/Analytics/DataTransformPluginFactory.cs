using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.PowerBI.Analytics.Contracts;

namespace Microsoft.InfoNav.Analytics
{
	// Token: 0x0200000C RID: 12
	[ImmutableObject(true)]
	public sealed class DataTransformPluginFactory : IDataTransformPluginFactory
	{
		// Token: 0x06000015 RID: 21 RVA: 0x00002376 File Offset: 0x00000576
		public DataTransformPluginFactory(AnalyticsEngineFactory analyticsEngineFactory, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			this._registeredPlugins = DataTransformPluginFactory.RegisterPlugIns(analyticsEngineFactory, analyticsFeatureSwitchProvider);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000238C File Offset: 0x0000058C
		public IDataTransformPlugin Create(string name)
		{
			Func<IDataTransformPlugin> func;
			if (this._registeredPlugins.TryGetValue(name, out func))
			{
				return func();
			}
			return null;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023B1 File Offset: 0x000005B1
		public bool HasTransform(string name)
		{
			return !string.IsNullOrWhiteSpace(name) && this._registeredPlugins.ContainsKey(name);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000023CC File Offset: 0x000005CC
		private static IReadOnlyDictionary<string, Func<IDataTransformPlugin>> RegisterPlugIns(AnalyticsEngineFactory analyticsEngineFactory, IAnalyticsFeatureSwitchProvider analyticsFeatureSwitchProvider)
		{
			Dictionary<string, Func<IDataTransformPlugin>> dictionary = new Dictionary<string, Func<IDataTransformPlugin>>(3, TransformNameComparer.Instance);
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.Forecast))
			{
				dictionary.Add("Forecast", new Func<IDataTransformPlugin>(analyticsEngineFactory.CreateForecastPlugin));
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.OutlierDetection))
			{
				dictionary.Add("OutlierDetection", new Func<IDataTransformPlugin>(analyticsEngineFactory.CreateOutlierDetectorPlugin));
			}
			if (analyticsFeatureSwitchProvider.IsEnabled(AnalyticsFeatureSwitchKind.SpatialClusteringInProcessing))
			{
				dictionary.Add("SpatialClustering", new Func<IDataTransformPlugin>(analyticsEngineFactory.CreateSpatialClustererPlugin));
			}
			return dictionary;
		}

		// Token: 0x0400004D RID: 77
		private readonly IReadOnlyDictionary<string, Func<IDataTransformPlugin>> _registeredPlugins;
	}
}

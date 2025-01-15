using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.ApplicationInsights.DataContracts;

namespace Microsoft.ApplicationInsights.Metrics.Extensibility
{
	// Token: 0x02000043 RID: 67
	internal abstract class MetricAggregateToApplicationInsightsPipelineConverterBase : IMetricAggregateToTelemetryPipelineConverter
	{
		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000246 RID: 582
		public abstract string AggregationKindMoniker { get; }

		// Token: 0x06000247 RID: 583 RVA: 0x0000BE14 File Offset: 0x0000A014
		public object Convert(MetricAggregate aggregate)
		{
			this.ValidateAggregate(aggregate);
			return this.ConvertAggregateToTelemetry(aggregate);
		}

		// Token: 0x06000248 RID: 584
		protected abstract void PopulateDataValues(MetricTelemetry telemetryItem, MetricAggregate aggregate);

		// Token: 0x06000249 RID: 585 RVA: 0x0000BE24 File Offset: 0x0000A024
		private static void PopulateTelemetryContext(IDictionary<string, string> dimensions, TelemetryContext telemetryContext, out IEnumerable<KeyValuePair<string, string>> nonContextDimensions)
		{
			if (dimensions == null)
			{
				nonContextDimensions = null;
				return;
			}
			List<KeyValuePair<string, string>> list = null;
			foreach (KeyValuePair<string, string> keyValuePair in dimensions)
			{
				if (!string.IsNullOrWhiteSpace(keyValuePair.Key) && keyValuePair.Value != null)
				{
					string key = keyValuePair.Key;
					uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
					if (num <= 2537523971U)
					{
						if (num <= 1975643529U)
						{
							if (num <= 554383511U)
							{
								if (num != 360516497U)
								{
									if (num != 405215829U)
									{
										if (num != 554383511U)
										{
											goto IL_0637;
										}
										if (!(key == "TelemetryContext.Device.ScreenResolution"))
										{
											goto IL_0637;
										}
										telemetryContext.Device.ScreenResolution = keyValuePair.Value;
										continue;
									}
									else
									{
										if (!(key == "TelemetryContext.Cloud.RoleName"))
										{
											goto IL_0637;
										}
										telemetryContext.Cloud.RoleName = keyValuePair.Value;
										continue;
									}
								}
								else
								{
									if (!(key == "TelemetryContext.Device.NetworkType"))
									{
										goto IL_0637;
									}
									telemetryContext.Device.NetworkType = keyValuePair.Value;
									continue;
								}
							}
							else if (num != 709112607U)
							{
								if (num != 925638093U)
								{
									if (num != 1975643529U)
									{
										goto IL_0637;
									}
									if (!(key == "TelemetryContext.Session.IsFirst"))
									{
										goto IL_0637;
									}
									try
									{
										telemetryContext.Session.IsFirst = new bool?(global::System.Convert.ToBoolean(keyValuePair.Value, CultureInfo.InvariantCulture));
										continue;
									}
									catch
									{
										try
										{
											int num2 = global::System.Convert.ToInt32(keyValuePair.Value, CultureInfo.InvariantCulture);
											if (num2 == 1)
											{
												telemetryContext.Session.IsFirst = new bool?(true);
											}
											else if (num2 == 0)
											{
												telemetryContext.Session.IsFirst = new bool?(false);
											}
										}
										catch
										{
										}
										continue;
									}
								}
								else
								{
									if (!(key == "TelemetryContext.Operation.Name"))
									{
										goto IL_0637;
									}
									telemetryContext.Operation.Name = keyValuePair.Value;
									continue;
								}
							}
							else
							{
								if (!(key == "TelemetryContext.User.AuthenticatedUserId"))
								{
									goto IL_0637;
								}
								telemetryContext.User.AuthenticatedUserId = keyValuePair.Value;
								continue;
							}
						}
						else if (num <= 2333651228U)
						{
							if (num != 2172093557U)
							{
								if (num != 2221110626U)
								{
									if (num != 2333651228U)
									{
										goto IL_0637;
									}
									if (!(key == "TelemetryContext.Operation.SyntheticSource"))
									{
										goto IL_0637;
									}
									telemetryContext.Operation.SyntheticSource = keyValuePair.Value;
									continue;
								}
								else
								{
									if (!(key == "TelemetryContext.InstrumentationKey"))
									{
										goto IL_0637;
									}
									telemetryContext.InstrumentationKey = keyValuePair.Value;
									continue;
								}
							}
							else
							{
								if (!(key == "TelemetryContext.Device.OperatingSystem"))
								{
									goto IL_0637;
								}
								telemetryContext.Device.OperatingSystem = keyValuePair.Value;
								continue;
							}
						}
						else if (num != 2357625971U)
						{
							if (num != 2508072696U)
							{
								if (num != 2537523971U)
								{
									goto IL_0637;
								}
								if (!(key == "TelemetryContext.Operation.ParentId"))
								{
									goto IL_0637;
								}
								telemetryContext.Operation.ParentId = keyValuePair.Value;
								continue;
							}
							else
							{
								if (!(key == "TelemetryContext.Session.Id"))
								{
									goto IL_0637;
								}
								telemetryContext.Session.Id = keyValuePair.Value;
								continue;
							}
						}
						else
						{
							if (!(key == "TelemetryContext.User.Id"))
							{
								goto IL_0637;
							}
							telemetryContext.User.Id = keyValuePair.Value;
							continue;
						}
					}
					else if (num <= 3451445133U)
					{
						if (num <= 2900511473U)
						{
							if (num != 2541123995U)
							{
								if (num != 2673272755U)
								{
									if (num != 2900511473U)
									{
										goto IL_0637;
									}
									if (!(key == "TelemetryContext.Device.Type"))
									{
										goto IL_0637;
									}
									telemetryContext.Device.Type = keyValuePair.Value;
									continue;
								}
								else
								{
									if (!(key == "TelemetryContext.Operation.Id"))
									{
										goto IL_0637;
									}
									telemetryContext.Operation.Id = keyValuePair.Value;
									continue;
								}
							}
							else
							{
								if (!(key == "TelemetryContext.Location.Ip"))
								{
									goto IL_0637;
								}
								telemetryContext.Location.Ip = keyValuePair.Value;
								continue;
							}
						}
						else if (num != 3334163388U)
						{
							if (num != 3437662383U)
							{
								if (num != 3451445133U)
								{
									goto IL_0637;
								}
								if (!(key == "TelemetryContext.Operation.CorrelationVector"))
								{
									goto IL_0637;
								}
								telemetryContext.Operation.CorrelationVector = keyValuePair.Value;
								continue;
							}
							else
							{
								if (!(key == "TelemetryContext.Device.Language"))
								{
									goto IL_0637;
								}
								telemetryContext.Device.Language = keyValuePair.Value;
								continue;
							}
						}
						else
						{
							if (!(key == "TelemetryContext.Device.Id"))
							{
								goto IL_0637;
							}
							telemetryContext.Device.Id = keyValuePair.Value;
							continue;
						}
					}
					else if (num <= 3873446200U)
					{
						if (num != 3763903931U)
						{
							if (num != 3790098449U)
							{
								if (num != 3873446200U)
								{
									goto IL_0637;
								}
								if (!(key == "TelemetryContext.Component.Version"))
								{
									goto IL_0637;
								}
								telemetryContext.Component.Version = keyValuePair.Value;
								continue;
							}
							else
							{
								if (!(key == "TelemetryContext.Cloud.RoleInstance"))
								{
									goto IL_0637;
								}
								telemetryContext.Cloud.RoleInstance = keyValuePair.Value;
								continue;
							}
						}
						else
						{
							if (!(key == "TelemetryContext.Device.OemName"))
							{
								goto IL_0637;
							}
							telemetryContext.Device.OemName = keyValuePair.Value;
							continue;
						}
					}
					else if (num != 3930719674U)
					{
						if (num != 4181273764U)
						{
							if (num != 4262928462U)
							{
								goto IL_0637;
							}
							if (!(key == "TelemetryContext.Device.Model"))
							{
								goto IL_0637;
							}
							telemetryContext.Device.Model = keyValuePair.Value;
							continue;
						}
						else if (!(key == "TelemetryContext.User.AccountId"))
						{
							goto IL_0637;
						}
					}
					else
					{
						if (!(key == "TelemetryContext.User.UserAgent"))
						{
							goto IL_0637;
						}
						telemetryContext.User.UserAgent = keyValuePair.Value;
						continue;
					}
					telemetryContext.User.AccountId = keyValuePair.Value;
					continue;
					IL_0637:
					string text;
					if (MetricDimensionNames.TelemetryContext.IsProperty(keyValuePair.Key, out text))
					{
						telemetryContext.GlobalProperties[text] = keyValuePair.Value;
					}
					else
					{
						if (list == null)
						{
							list = new List<KeyValuePair<string, string>>(dimensions.Count);
						}
						list.Add(keyValuePair);
					}
				}
			}
			nonContextDimensions = list;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x0000C50C File Offset: 0x0000A70C
		private void ValidateAggregate(MetricAggregate metricAggregate)
		{
			Util.ValidateNotNull(metricAggregate, "metricAggregate");
			Util.ValidateNotNull(metricAggregate.AggregationKindMoniker, "AggregationKindMoniker");
			string aggregationKindMoniker = this.AggregationKindMoniker;
			if (!metricAggregate.AggregationKindMoniker.Equals(aggregationKindMoniker, StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create("Cannot convert the specified {0}, because is has", new object[] { metricAggregate })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" {0}=\"{1}\".", new object[] { "AggregationKindMoniker", metricAggregate.AggregationKindMoniker })) + global::System.FormattableString.Invariant(global::System.Runtime.CompilerServices.FormattableStringFactory.Create(" This converter handles \"{0}\".", new object[] { aggregationKindMoniker })));
			}
		}

		// Token: 0x0600024B RID: 587 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		private MetricTelemetry ConvertAggregateToTelemetry(MetricAggregate aggregate)
		{
			MetricTelemetry metricTelemetry = new MetricTelemetry();
			if (aggregate.MetricNamespace.Length > 0)
			{
				metricTelemetry.MetricNamespace = aggregate.MetricNamespace;
			}
			metricTelemetry.Name = aggregate.MetricId;
			this.PopulateDataValues(metricTelemetry, aggregate);
			IDictionary<string, string> properties = metricTelemetry.Properties;
			if (properties != null)
			{
				properties.Add("_MS.AggregationIntervalMs", ((long)aggregate.AggregationPeriodDuration.TotalMilliseconds).ToString(CultureInfo.InvariantCulture));
			}
			metricTelemetry.Timestamp = aggregate.AggregationPeriodStart;
			IEnumerable<KeyValuePair<string, string>> enumerable;
			MetricAggregateToApplicationInsightsPipelineConverterBase.PopulateTelemetryContext(aggregate.Dimensions, metricTelemetry.Context, out enumerable);
			if (enumerable != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in enumerable)
				{
					metricTelemetry.Properties[keyValuePair.Key] = keyValuePair.Value;
				}
			}
			Util.StampSdkVersionToContext(metricTelemetry);
			return metricTelemetry;
		}

		// Token: 0x0400010C RID: 268
		public const string AggregationIntervalMonikerPropertyKey = "_MS.AggregationIntervalMs";
	}
}

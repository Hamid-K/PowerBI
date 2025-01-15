using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E25 RID: 3621
	internal sealed class CdpaSetContextProvider : SetContextProvider
	{
		// Token: 0x0600618B RID: 24971 RVA: 0x0014D528 File Offset: 0x0014B728
		public CdpaSetContextProvider(CdpaService service)
		{
			this.service = service;
		}

		// Token: 0x17001CAD RID: 7341
		// (get) Token: 0x0600618C RID: 24972 RVA: 0x0014D537 File Offset: 0x0014B737
		public override IResource Resource
		{
			get
			{
				return this.service.Resource;
			}
		}

		// Token: 0x17001CAE RID: 7342
		// (get) Token: 0x0600618D RID: 24973 RVA: 0x0014D544 File Offset: 0x0014B744
		public override IEngineHost EngineHost
		{
			get
			{
				return this.service.EngineHost;
			}
		}

		// Token: 0x0600618E RID: 24974 RVA: 0x0014D551 File Offset: 0x0014B751
		public override bool TryCompileScalarExpression(ICube cube, Dimensionality dimensionality, CubeExpression expression, out Set set)
		{
			return new CdpaSetContextProvider.CdpaSetCompiler(cube).TryCompileScalarExpression(dimensionality, expression, out set);
		}

		// Token: 0x0600618F RID: 24975 RVA: 0x0014D564 File Offset: 0x0014B764
		public override bool TryCreateContext(ICube cube, Set set, IList<ParameterArguments> arguments, out SetContext context)
		{
			CdpaCube cdpaCube = CdpaSetContextProvider.GetCdpaCube(cube);
			try
			{
				CdpaQueryGenerator cdpaQueryGenerator = new CdpaQueryGenerator(this.service, cube);
				CdpaDataflowGraph cdpaDataflowGraph = cdpaQueryGenerator.Visit(set);
				CdpaRequestWithTimeConfigurationAndResponseProtocol cdpaRequestWithTimeConfigurationAndResponseProtocol;
				if (cdpaDataflowGraph.Keys.Count<ScopePath>() == 1)
				{
					CdpaMetricConfiguration metric = cdpaDataflowGraph.GetNode(cdpaDataflowGraph.Keys.Single<ScopePath>()).Metric;
					if (metric.IsEmpty)
					{
						cdpaRequestWithTimeConfigurationAndResponseProtocol = null;
					}
					else
					{
						cdpaRequestWithTimeConfigurationAndResponseProtocol = new CdpaSeriesRequest
						{
							Metric = metric,
							Granularity = cdpaDataflowGraph.Granularity,
							TimeRange = cdpaDataflowGraph.TimeRange
						};
					}
				}
				else
				{
					cdpaRequestWithTimeConfigurationAndResponseProtocol = this.CreateFunnelRequest(cdpaQueryGenerator, cdpaDataflowGraph);
				}
				if (cdpaRequestWithTimeConfigurationAndResponseProtocol == null)
				{
					context = new CdpaSetContextProvider.CdpaSetContext(this, cdpaCube, cube, set, arguments);
				}
				else
				{
					DateTime dateTime = this.service.EngineHost.QueryService<ICurrentTimeService>().FixedUtcNow;
					dateTime = dateTime.RoundTo(CdpaSetContextProvider.ResolutionAsGranularity, false);
					bool flag;
					cdpaRequestWithTimeConfigurationAndResponseProtocol = CdpaSetContextProvider.SetDefaultTimeRange(cdpaRequestWithTimeConfigurationAndResponseProtocol, dateTime, out flag);
					cdpaRequestWithTimeConfigurationAndResponseProtocol = CdpaSetContextProvider.AlignTimeRangeToGranularity(cdpaRequestWithTimeConfigurationAndResponseProtocol, dateTime);
					if (cdpaRequestWithTimeConfigurationAndResponseProtocol.Granularity == null)
					{
						cdpaRequestWithTimeConfigurationAndResponseProtocol = cdpaRequestWithTimeConfigurationAndResponseProtocol.ShallowCopy();
						cdpaRequestWithTimeConfigurationAndResponseProtocol.Granularity = TimePartsTimeGranularity.Coarsest;
					}
					cdpaRequestWithTimeConfigurationAndResponseProtocol.ResponseFormat = "jsonSeries";
					cdpaRequestWithTimeConfigurationAndResponseProtocol.ResponseProtocol = "oneShot";
					if (arguments.Count > 0)
					{
						Dictionary<string, string> dictionary = new Dictionary<string, string>();
						foreach (ParameterArguments parameterArguments in arguments)
						{
							CdpaParameter cdpaParameter;
							if (!cdpaCube.Parameters.TryGetValue(QualifiedName.From(parameterArguments.Parameter), out cdpaParameter) || parameterArguments.Values.Length != 1)
							{
								throw new NotSupportedException();
							}
							string asString = cdpaCube.TransformTypeInvariant(TypeValue.Text, parameterArguments.Values[0]).AsString;
							dictionary.Add(cdpaParameter.ParameterName, asString);
						}
						foreach (CdpaParameter cdpaParameter2 in cdpaCube.Parameters.Values)
						{
							if (cdpaParameter2.IsRequired && cdpaParameter2.DefaultValue != null && !dictionary.ContainsKey(cdpaParameter2.ParameterName))
							{
								string asString2 = cdpaCube.TransformTypeInvariant(TypeValue.Text, cdpaParameter2.DefaultValue).AsString;
								dictionary.Add(cdpaParameter2.ParameterName, asString2);
							}
						}
						cdpaRequestWithTimeConfigurationAndResponseProtocol.ExtraParameters = new CdpaExtraParameters
						{
							PropertyValues = dictionary
						};
					}
					context = new CdpaSetContextProvider.CdpaSetContext(this, cdpaCube, cube, set, arguments, cdpaRequestWithTimeConfigurationAndResponseProtocol, flag);
				}
			}
			catch (NotSupportedException)
			{
				context = null;
			}
			return context != null;
		}

		// Token: 0x06006190 RID: 24976 RVA: 0x0014D804 File Offset: 0x0014BA04
		private static bool IsVirtualTable(CdpaEvent e)
		{
			return e.EventName.Contains("_v_");
		}

		// Token: 0x06006191 RID: 24977 RVA: 0x0014D818 File Offset: 0x0014BA18
		private static CdpaCube GetCdpaCube(ICube cube)
		{
			CdpaCube cdpaCube = cube.GetUnscoped() as CdpaCube;
			if (cdpaCube != null)
			{
				return cdpaCube;
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006192 RID: 24978 RVA: 0x0014D83B File Offset: 0x0014BA3B
		private static IEnumerable<CdpaDimensionAttribute> Order(IEnumerable<CdpaDimensionAttribute> attributes)
		{
			return attributes.OrderBy((CdpaDimensionAttribute a) => a.QualifiedName.AsString, StringComparer.Ordinal);
		}

		// Token: 0x06006193 RID: 24979 RVA: 0x0014D868 File Offset: 0x0014BA68
		private static CdpaRequestWithTimeConfigurationAndResponseProtocol SetDefaultTimeRange(CdpaRequestWithTimeConfigurationAndResponseProtocol request, DateTime utcNow, out bool truncatedResult)
		{
			truncatedResult = false;
			if (request.TimeRange == null)
			{
				request = request.ShallowCopy();
				truncatedResult = true;
				TimePartsTimeGranularity timePartsTimeGranularity;
				if ((!request.Granularity.TryGetTimePartsGranularity(out timePartsTimeGranularity) || TimePartsTimeGranularity.Finest.Equals(timePartsTimeGranularity)) && !MetricGranularityTimeGranularity.P1D.TryGetTimePartsGranularity(out timePartsTimeGranularity))
				{
					throw new NotSupportedException();
				}
				DateTime dateTime = utcNow;
				for (int i = 0; i < 30; i++)
				{
					dateTime = timePartsTimeGranularity.SubtractFinestPartFrom(dateTime);
				}
				request.TimeRange = new TimeIntervalCdpaTimeRange
				{
					UtcStart = new DateTime?(dateTime),
					UtcEnd = new DateTime?(utcNow)
				};
			}
			return request;
		}

		// Token: 0x06006194 RID: 24980 RVA: 0x0014D8F8 File Offset: 0x0014BAF8
		private static CdpaRequestWithTimeConfigurationAndResponseProtocol AlignTimeRangeToGranularity(CdpaRequestWithTimeConfigurationAndResponseProtocol request, DateTime utcNow)
		{
			request = request.ShallowCopy();
			if (request.TimeRange != null)
			{
				request.TimeRange = request.TimeRange.ToTimePartsFilteredTimeInterval().ToTimeInterval();
				request.TimeRange = request.TimeRange.NullableIntersect(new TimeIntervalCdpaTimeRange
				{
					UtcStart = new DateTime?(CdpaTimeRange.UtcEarliest),
					UtcEnd = new DateTime?(utcNow)
				}).ToTimePartsFilteredTimeInterval().ToTimeInterval();
			}
			TimePartsTimeGranularity timePartsTimeGranularity = null;
			TimeIntervalCdpaTimeRange timeIntervalCdpaTimeRange = request.TimeRange as TimeIntervalCdpaTimeRange;
			if (timeIntervalCdpaTimeRange != null)
			{
				if (timeIntervalCdpaTimeRange.UtcStart.Value >= timeIntervalCdpaTimeRange.UtcEnd.Value)
				{
					throw new NotSupportedException();
				}
				timeIntervalCdpaTimeRange = new TimeIntervalCdpaTimeRange
				{
					UtcStart = new DateTime?(timeIntervalCdpaTimeRange.UtcStart.Value.RoundTo(CdpaSetContextProvider.ResolutionAsGranularity, false)),
					UtcEnd = new DateTime?(timeIntervalCdpaTimeRange.UtcEnd.Value.RoundTo(CdpaSetContextProvider.ResolutionAsGranularity, true))
				};
				request.TimeRange = timeIntervalCdpaTimeRange;
				if (utcNow.RoundTo(CdpaSetContextProvider.ResolutionAsGranularity, true) == timeIntervalCdpaTimeRange.UtcEnd)
				{
					TimeSpan timeSpan = timeIntervalCdpaTimeRange.UtcEnd.Value - timeIntervalCdpaTimeRange.UtcStart.Value;
					timePartsTimeGranularity = new TimePartsTimeGranularity
					{
						Years = 0,
						Months = 0,
						Days = (short)timeSpan.Days,
						Hours = (short)timeSpan.Hours,
						Minutes = (short)timeSpan.Minutes,
						Seconds = (short)timeSpan.Seconds
					};
				}
			}
			AnchoredTimeGranularity anchoredTimeGranularity = request.Granularity as AnchoredTimeGranularity;
			if (anchoredTimeGranularity != null)
			{
				bool flag = false;
				TimePartsTimeGranularity timePartsTimeGranularity2;
				if (anchoredTimeGranularity.Granularity.TryGetTimePartsGranularity(out timePartsTimeGranularity2))
				{
					DateTime nearestAnchor = timePartsTimeGranularity2.GetNearestAnchor(anchoredTimeGranularity.Anchor, timeIntervalCdpaTimeRange.UtcStart.Value);
					DateTime nearestAnchor2 = timePartsTimeGranularity2.ToUnits().GetNearestAnchor(nearestAnchor, timeIntervalCdpaTimeRange.UtcStart.Value);
					flag = nearestAnchor == nearestAnchor2;
				}
				if (!flag)
				{
					throw new NotSupportedException();
				}
				request.Granularity = anchoredTimeGranularity.Granularity;
			}
			string text;
			if (timePartsTimeGranularity != null && CdpaSetContextProvider.timeGranularityToTimeSpanMap.TryGetValue(timePartsTimeGranularity, out text))
			{
				request.TimeRange = new TimeSpanCdpaTimeRange
				{
					TimeSpan = text
				};
			}
			return request;
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x0014DB44 File Offset: 0x0014BD44
		private static IEnumerable<ScopePath> TopologicalSort(CdpaDataflowGraph graph, ScopePath toNode)
		{
			HashSet<ScopePath> hashSet = new HashSet<ScopePath>();
			return CdpaSetContextProvider.TopologicalSort(graph, hashSet, toNode);
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x0014DB5F File Offset: 0x0014BD5F
		private static IEnumerable<ScopePath> TopologicalSort(CdpaDataflowGraph graph, HashSet<ScopePath> visited, ScopePath toNode)
		{
			if (visited.Add(toNode))
			{
				foreach (ScopePath scopePath in graph.KeysTo(toNode))
				{
					foreach (ScopePath scopePath2 in CdpaSetContextProvider.TopologicalSort(graph, visited, scopePath))
					{
						yield return scopePath2;
					}
					IEnumerator<ScopePath> enumerator2 = null;
				}
				IEnumerator<ScopePath> enumerator = null;
				yield return toNode;
			}
			yield break;
			yield break;
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x0014DB7D File Offset: 0x0014BD7D
		private static bool HasMeasure(CdpaDataflowNode node)
		{
			if (node.Metric != null)
			{
				return node.Metric.Operands.Any((CdpaOperand o) => o.Measure != null);
			}
			return false;
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x0014DBB8 File Offset: 0x0014BDB8
		private static CdpaTableConfiguration ToTableConfiguration(CdpaTableOrMetricConfiguration tableOrMetric, string funnelKey)
		{
			CdpaTableConfiguration cdpaTableConfiguration = tableOrMetric as CdpaTableConfiguration;
			if (cdpaTableConfiguration == null)
			{
				CdpaMetricConfiguration cdpaMetricConfiguration = (CdpaMetricConfiguration)tableOrMetric;
				if (cdpaMetricConfiguration.Operands.Count == 1)
				{
					CdpaOperand cdpaOperand = cdpaMetricConfiguration.Operands[0];
					if (cdpaOperand.Measure != null && cdpaOperand.Measure.Operations.Count == 1)
					{
						MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = cdpaOperand.Measure as MetricPropertyCdpaMetricMeasure;
						DistinctCountCdpaMetricPropertyOperation distinctCountCdpaMetricPropertyOperation = cdpaOperand.Measure.Operations[0] as DistinctCountCdpaMetricPropertyOperation;
						if (metricPropertyCdpaMetricMeasure != null && distinctCountCdpaMetricPropertyOperation != null && metricPropertyCdpaMetricMeasure.PropertyName == funnelKey)
						{
							cdpaTableConfiguration = cdpaOperand.Table;
						}
					}
				}
			}
			if (cdpaTableConfiguration == null)
			{
				throw new NotSupportedException();
			}
			return cdpaTableConfiguration;
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x0014DC58 File Offset: 0x0014BE58
		private static string[] GetArrayPrefix(string[] array, int length)
		{
			string[] array2 = new string[length];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = array[i];
			}
			return array2;
		}

		// Token: 0x0600619A RID: 24986 RVA: 0x0014DC84 File Offset: 0x0014BE84
		private CdpaFunnelRequest CreateFunnelRequest(CdpaQueryGenerator queryGenerator, CdpaDataflowGraph graph)
		{
			CdpaDataflowGraph cdpaDataflowGraph = new CdpaDataflowGraph();
			foreach (ScopePath scopePath in graph.Keys)
			{
				cdpaDataflowGraph.Add(scopePath, graph.GetNode(scopePath));
			}
			foreach (ScopePath scopePath2 in graph.Keys)
			{
				for (int i = scopePath2.Path.Length - 1; i >= 0; i--)
				{
					ScopePath scopePath3 = new ScopePath(CdpaSetContextProvider.GetArrayPrefix(scopePath2.Path, i));
					if (graph.ContainsNode(scopePath3))
					{
						cdpaDataflowGraph.Add(scopePath2, scopePath3, EmptyArray<CdpaDataflowEdgeConstraint>.Instance);
						break;
					}
				}
				foreach (ScopePath scopePath4 in graph.KeysFrom(scopePath2))
				{
					CdpaDataflowEdgeConstraint[] edge = graph.GetEdge(scopePath2, scopePath4);
					if (edge.Any((CdpaDataflowEdgeConstraint e) => e is TimePeriodCdpaDataflowEdgeConstraint))
					{
						cdpaDataflowGraph.Add(scopePath2, scopePath4, edge);
					}
				}
			}
			List<ScopePath[]> list = new List<ScopePath[]>();
			foreach (ScopePath scopePath5 in cdpaDataflowGraph.Keys)
			{
				if (CdpaSetContextProvider.HasMeasure(cdpaDataflowGraph.GetNode(scopePath5)))
				{
					list.Add(CdpaSetContextProvider.TopologicalSort(cdpaDataflowGraph, scopePath5).ToArray<ScopePath>());
				}
			}
			list = list.OrderByDescending((ScopePath[] f) => f.Length).ToList<ScopePath[]>();
			CdpaFunnelDefinition cdpaFunnelDefinition = this.CreateFunnelDefinition(queryGenerator, cdpaDataflowGraph, list, 0);
			if (cdpaFunnelDefinition.FunnelKey == null)
			{
				throw new NotSupportedException();
			}
			for (int j = 0; j < cdpaFunnelDefinition.Steps.Count; j++)
			{
				cdpaFunnelDefinition.Steps[j].Configuration = CdpaSetContextProvider.ToTableConfiguration(cdpaFunnelDefinition.Steps[j].Configuration, cdpaFunnelDefinition.FunnelKey);
			}
			for (int k = 1; k < list.Count; k++)
			{
				CdpaFunnelDefinition cdpaFunnelDefinition2 = this.CreateFunnelDefinition(queryGenerator, cdpaDataflowGraph, list, k);
				if ((cdpaFunnelDefinition2.Steps.Count == 1 && cdpaFunnelDefinition2.FunnelKey != null) || (cdpaFunnelDefinition2.Steps.Count > 1 && cdpaFunnelDefinition2.FunnelKey != cdpaFunnelDefinition.FunnelKey))
				{
					throw new NotSupportedException();
				}
				if ((cdpaFunnelDefinition2.Steps.Count == 1 && cdpaFunnelDefinition2.MaxCompletionTime != null) || (cdpaFunnelDefinition2.Steps.Count == 2 && cdpaFunnelDefinition2.MaxCompletionTime != cdpaFunnelDefinition.MaxCompletionTime && cdpaFunnelDefinition2.MaxCompletionTime != cdpaFunnelDefinition2.Steps[1].MaxStepCompletionTime) || (cdpaFunnelDefinition2.Steps.Count > 2 && cdpaFunnelDefinition2.MaxCompletionTime != cdpaFunnelDefinition.MaxCompletionTime))
				{
					throw new NotSupportedException();
				}
				for (int l = 0; l < cdpaFunnelDefinition2.Steps.Count; l++)
				{
					CdpaFunnelStepDefinition cdpaFunnelStepDefinition = cdpaFunnelDefinition.Steps[l];
					CdpaFunnelStepDefinition cdpaFunnelStepDefinition2 = cdpaFunnelDefinition2.Steps[l];
					if (l != cdpaFunnelDefinition2.Steps.Count - 1 && cdpaFunnelStepDefinition2.MaxStepCompletionTime != cdpaFunnelStepDefinition.MaxStepCompletionTime)
					{
						throw new NotSupportedException();
					}
					cdpaFunnelStepDefinition2.Configuration = CdpaSetContextProvider.ToTableConfiguration(cdpaFunnelStepDefinition2.Configuration, cdpaFunnelDefinition.FunnelKey);
					if (!cdpaFunnelStepDefinition2.Configuration.Equals(cdpaFunnelStepDefinition.Configuration))
					{
						throw new NotSupportedException();
					}
				}
			}
			return new CdpaFunnelRequest
			{
				DataModel = cdpaFunnelDefinition,
				Granularity = graph.Granularity,
				TimeRange = graph.TimeRange
			};
		}

		// Token: 0x0600619B RID: 24987 RVA: 0x0014E084 File Offset: 0x0014C284
		private CdpaFunnelDefinition CreateFunnelDefinition(CdpaQueryGenerator queryGenerator, CdpaDataflowGraph graph, List<ScopePath[]> sortedFunnels, int funnelIndex)
		{
			if (funnelIndex >= sortedFunnels.Count)
			{
				throw new NotSupportedException();
			}
			ScopePath[] array = sortedFunnels[funnelIndex];
			int num = Math.Max(array.Length - 1, 1);
			if (num > sortedFunnels.Count)
			{
				throw new NotSupportedException();
			}
			CdpaFunnelDefinition cdpaFunnelDefinition = new CdpaFunnelDefinition
			{
				FunnelName = "funnel.0",
				Steps = new List<CdpaFunnelStepDefinition>()
			};
			for (int i = 0; i < num; i++)
			{
				ScopePath scopePath = sortedFunnels[sortedFunnels.Count - 1 - i].Last<ScopePath>();
				CdpaMetricConfiguration cdpaMetricConfiguration = graph.GetNode(array[i]).Metric;
				CdpaMetricConfiguration metric = graph.GetNode(scopePath).Metric;
				cdpaFunnelDefinition.Steps.Add(new CdpaFunnelStepDefinition
				{
					ScopePath = scopePath,
					StepName = "step." + (i + 1).ToString()
				});
				if (i > 0)
				{
					CdpaDataflowEdgeConstraint[] array2;
					if (!graph.TryGetEdge(array[i - 1], array[i], out array2))
					{
						throw new NotSupportedException();
					}
					for (int j = 0; j < array2.Length; j++)
					{
						TimePeriodCdpaDataflowEdgeConstraint timePeriodCdpaDataflowEdgeConstraint = array2[j] as TimePeriodCdpaDataflowEdgeConstraint;
						if (timePeriodCdpaDataflowEdgeConstraint != null && timePeriodCdpaDataflowEdgeConstraint.DurationIsValid)
						{
							string text = timePeriodCdpaDataflowEdgeConstraint.Duration.ToIso8601();
							cdpaFunnelDefinition.Steps[i].MaxStepCompletionTime = cdpaFunnelDefinition.Steps[i].MaxStepCompletionTime.NullableMerge(text);
						}
						else
						{
							PropertyEqualityCdpaDataflowEdgeConstraint propertyEqualityCdpaDataflowEdgeConstraint = array2[j] as PropertyEqualityCdpaDataflowEdgeConstraint;
							if (propertyEqualityCdpaDataflowEdgeConstraint == null)
							{
								throw new NotSupportedException();
							}
							cdpaFunnelDefinition.FunnelKey = cdpaFunnelDefinition.FunnelKey.NullableMerge(propertyEqualityCdpaDataflowEdgeConstraint.PropertyName);
						}
					}
				}
				cdpaMetricConfiguration = queryGenerator.Merge(new CdpaMetricConfiguration[] { cdpaMetricConfiguration, metric });
				cdpaFunnelDefinition.Steps[i].Configuration = cdpaMetricConfiguration;
			}
			if (array.Length >= 2)
			{
				int num2 = 0;
				int num3 = array.Length - 2;
				CdpaDataflowEdgeConstraint[] array3;
				if (graph.TryGetEdge(array[num2], array[num3], out array3))
				{
					CdpaDataflowEdgeConstraint[] array4 = array3;
					int k = 0;
					while (k < array4.Length)
					{
						CdpaDataflowEdgeConstraint cdpaDataflowEdgeConstraint = array4[k];
						if (num3 <= num2)
						{
							goto IL_022B;
						}
						TimePeriodCdpaDataflowEdgeConstraint timePeriodCdpaDataflowEdgeConstraint2 = cdpaDataflowEdgeConstraint as TimePeriodCdpaDataflowEdgeConstraint;
						if (timePeriodCdpaDataflowEdgeConstraint2 == null || !timePeriodCdpaDataflowEdgeConstraint2.DurationIsValid)
						{
							goto IL_022B;
						}
						string text2 = timePeriodCdpaDataflowEdgeConstraint2.Duration.ToIso8601();
						cdpaFunnelDefinition.MaxCompletionTime = cdpaFunnelDefinition.MaxCompletionTime.NullableMerge(text2);
						IL_0260:
						k++;
						continue;
						IL_022B:
						if (num2 + 1 == num3)
						{
							PropertyEqualityCdpaDataflowEdgeConstraint propertyEqualityCdpaDataflowEdgeConstraint2 = cdpaDataflowEdgeConstraint as PropertyEqualityCdpaDataflowEdgeConstraint;
							if (propertyEqualityCdpaDataflowEdgeConstraint2 != null)
							{
								cdpaFunnelDefinition.FunnelKey = cdpaFunnelDefinition.FunnelKey.NullableMerge(propertyEqualityCdpaDataflowEdgeConstraint2.PropertyName);
								goto IL_0260;
							}
						}
						throw new NotSupportedException();
					}
				}
			}
			return cdpaFunnelDefinition;
		}

		// Token: 0x04003520 RID: 13600
		public static readonly ITimeGranularity ResolutionAsGranularity = MetricGranularityTimeGranularity.PT1S;

		// Token: 0x04003521 RID: 13601
		public static readonly TimeSpan ResolutionAsTimeSpan = TimeSpan.FromSeconds(1.0);

		// Token: 0x04003522 RID: 13602
		private static readonly Dictionary<TimePartsTimeGranularity, string> timeGranularityToTimeSpanMap = new Dictionary<TimePartsTimeGranularity, string>
		{
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 0,
					Minutes = 1,
					Seconds = 0
				},
				"PT1M"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 0,
					Minutes = 5,
					Seconds = 0
				},
				"PT5M"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 0,
					Minutes = 10,
					Seconds = 0
				},
				"PT10M"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 0,
					Minutes = 15,
					Seconds = 0
				},
				"PT15M"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 0,
					Minutes = 30,
					Seconds = 0
				},
				"PT30M"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 1,
					Minutes = 0,
					Seconds = 0
				},
				"PT1H"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 2,
					Minutes = 0,
					Seconds = 0
				},
				"PT2H"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 4,
					Minutes = 0,
					Seconds = 0
				},
				"PT4H"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 6,
					Minutes = 0,
					Seconds = 0
				},
				"PT6H"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 0,
					Hours = 12,
					Minutes = 0,
					Seconds = 0
				},
				"PT12H"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 1,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P1D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 2,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P2D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 4,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P4D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 7,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P7D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 14,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P14D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 30,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P30D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 60,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P60D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 90,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P90D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 180,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P180D"
			},
			{
				new TimePartsTimeGranularity
				{
					Years = 0,
					Months = 0,
					Days = 365,
					Hours = 0,
					Minutes = 0,
					Seconds = 0
				},
				"P365D"
			}
		};

		// Token: 0x04003523 RID: 13603
		private readonly CdpaService service;

		// Token: 0x02000E26 RID: 3622
		private sealed class CdpaSetCompiler : SetCompiler
		{
			// Token: 0x0600619D RID: 24989 RVA: 0x0006804F File Offset: 0x0006624F
			public CdpaSetCompiler(ICube cube)
				: base(cube)
			{
			}

			// Token: 0x0600619E RID: 24990 RVA: 0x0014E7D0 File Offset: 0x0014C9D0
			protected override Set NewSelect(Dimensionality visibleDimensionality)
			{
				return new VisibleSlicerSet(visibleDimensionality);
			}
		}

		// Token: 0x02000E27 RID: 3623
		private sealed class CdpaSetContext : SetContext
		{
			// Token: 0x0600619F RID: 24991 RVA: 0x0014E7D8 File Offset: 0x0014C9D8
			public CdpaSetContext(CdpaSetContextProvider provider, CdpaCube cdpaCube, ICube cube, Set set, IList<ParameterArguments> arguments)
				: this(provider, cdpaCube, cube, set, arguments, null, false)
			{
			}

			// Token: 0x060061A0 RID: 24992 RVA: 0x0014E7EC File Offset: 0x0014C9EC
			public CdpaSetContext(CdpaSetContextProvider provider, CdpaCube cdpaCube, ICube cube, Set set, IList<ParameterArguments> arguments, CdpaRequestWithTimeConfigurationAndResponseProtocol request, bool truncatedResult)
				: base(set, arguments)
			{
				this.timestampColumnName = "timestamp." + Guid.NewGuid().ToString();
				this.provider = provider;
				this.cdpaCube = cdpaCube;
				this.cube = cube;
				this.request = request;
				this.truncatedResult = truncatedResult;
			}

			// Token: 0x17001CAF RID: 7343
			// (get) Token: 0x060061A1 RID: 24993 RVA: 0x0014E84C File Offset: 0x0014CA4C
			public override TableValue DirectQueryCapabilities
			{
				get
				{
					if (this.capabilities == null)
					{
						List<Value> list = new List<Value>();
						list.Add(CapabilityModule.NewCapability("Core", Value.Null));
						list.Add(CapabilityModule.NewCapability("LiteralCount", NumberValue.New(1000)));
						list.Add(CapabilityModule.NewCapability("Table.FirstN", Value.Null));
						list.Add(CapabilityModule.NewCapability("List.Average", Value.Null));
						list.Add(CapabilityModule.NewCapability("List.Max", Value.Null));
						list.Add(CapabilityModule.NewCapability("List.Min", Value.Null));
						list.Add(CapabilityModule.NewCapability("List.Sum", Value.Null));
						list.Add(CapabilityModule.NewCapability("Table.RowCount", Value.Null));
						TableTypeValue asTableType = CapabilityModule.DirectQueryCapabilities.From.Type.AsFunctionType.ReturnType.AsTableType;
						this.capabilities = ListValue.New(list.ToArray()).ToTable(asTableType);
					}
					return this.capabilities;
				}
			}

			// Token: 0x17001CB0 RID: 7344
			// (get) Token: 0x060061A2 RID: 24994 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue DisplayFolders
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17001CB1 RID: 7345
			// (get) Token: 0x060061A3 RID: 24995 RVA: 0x00066554 File Offset: 0x00064754
			public override TableValue MeasureGroups
			{
				get
				{
					return TableValue.Empty;
				}
			}

			// Token: 0x17001CB2 RID: 7346
			// (get) Token: 0x060061A4 RID: 24996 RVA: 0x0014E950 File Offset: 0x0014CB50
			public override TableValue Dimensions
			{
				get
				{
					if (this.dimensionsTable == null)
					{
						Dictionary<QualifiedName, TableValue> dictionary = new Dictionary<QualifiedName, TableValue>();
						foreach (CdpaDimension cdpaDimension in this.cdpaCube.Dimensions.Values)
						{
							string text = PersistentCacheKey.CdpaDatabase.Qualify(this.provider.service.Tenant, cdpaDimension.QualifiedName.AsString);
							TableValue tableValue = this.CreateDimensionTable(cdpaDimension);
							tableValue = tableValue.ReplaceRelationshipIdentity(text);
							dictionary.Add(cdpaDimension.QualifiedName, tableValue);
						}
						foreach (CdpaDimension cdpaDimension2 in this.cdpaCube.Dimensions.Values)
						{
							TableValue tableValue2 = dictionary[cdpaDimension2.QualifiedName];
							foreach (CdpaDimensionAttribute cdpaDimensionAttribute in cdpaDimension2.Attributes.Values)
							{
								CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = cdpaDimensionAttribute as CdpaRelatedDimensionAttribute;
								if (cdpaRelatedDimensionAttribute != null)
								{
									foreach (CdpaDimensionAttribute cdpaDimensionAttribute2 in cdpaRelatedDimensionAttribute.RelatedAttributes)
									{
										CdpaDimension dimension = cdpaDimensionAttribute2.Dimension;
										TableValue tableValue3 = dictionary[dimension.QualifiedName];
										tableValue2 = tableValue2.NestedJoin(TextValue.New(cdpaDimensionAttribute.PropertyName), tableValue3, TextValue.New(cdpaDimensionAttribute2.PropertyName), Library.JoinKind.LeftOuter, TextValue.New(dimension.QualifiedName.AsString), Value.Null);
										tableValue2 = tableValue2.RemoveColumns(TextValue.New(dimension.QualifiedName.AsString), Value.Null);
									}
								}
							}
							dictionary[cdpaDimension2.QualifiedName] = tableValue2;
						}
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
						foreach (CdpaDimension cdpaDimension3 in this.cdpaCube.Dimensions.Values)
						{
							cubeObjectTableBuilder.AddDimension(cdpaDimension3.QualifiedName.AsString, cdpaDimension3.Caption, dictionary[cdpaDimension3.QualifiedName]);
						}
						this.dimensionsTable = cubeObjectTableBuilder.ToTable();
					}
					return this.dimensionsTable;
				}
			}

			// Token: 0x17001CB3 RID: 7347
			// (get) Token: 0x060061A5 RID: 24997 RVA: 0x0014EC2C File Offset: 0x0014CE2C
			public override TableValue Measures
			{
				get
				{
					if (this.measuresTable == null)
					{
						CubeObjectTableBuilder cubeObjectTableBuilder = CubeObjectTableBuilder.New();
						foreach (CdpaMeasure cdpaMeasure in this.cdpaCube.Measures.Values)
						{
							cubeObjectTableBuilder.AddMeasure(cdpaMeasure.QualifiedName.AsString, cdpaMeasure.Caption, new MeasureValue(cdpaMeasure.QualifiedName.ToExpression(), cdpaMeasure.Type));
						}
						this.measuresTable = cubeObjectTableBuilder.ToTable();
					}
					return this.measuresTable;
				}
			}

			// Token: 0x17001CB4 RID: 7348
			// (get) Token: 0x060061A6 RID: 24998 RVA: 0x0014ECCC File Offset: 0x0014CECC
			public override SetContextProvider ContextProvider
			{
				get
				{
					return this.provider;
				}
			}

			// Token: 0x060061A7 RID: 24999 RVA: 0x0014ECD4 File Offset: 0x0014CED4
			public override TableValue GetParameters(CubeValue cube)
			{
				if (this.parametersTable == null)
				{
					HashSet<string> hashSet = new HashSet<string>();
					foreach (ParameterArguments parameterArguments in base.ParameterArguments)
					{
						hashSet.Add(parameterArguments.Parameter.Identifier);
					}
					List<IValueReference> list = new List<IValueReference>();
					foreach (CdpaParameter cdpaParameter in this.cdpaCube.Parameters.Values)
					{
						if (!hashSet.Contains(cdpaParameter.QualifiedName.AsString))
						{
							RecordValue recordValue = RecordValue.Empty;
							if (cdpaParameter.AllowedValues != null)
							{
								ListValue listValue = ListValue.New(cdpaParameter.AllowedValues.Cast<IValueReference>());
								recordValue = recordValue.Concatenate(RecordValue.New(CdpaSetContextProvider.CdpaSetContext.allowedValuesMetaType, new Value[] { listValue })).AsRecord;
								recordValue = recordValue.Concatenate(NavigationTableServices.NewAllowedValuesIsOpenSetMetadata(true)).AsRecord;
							}
							if (cdpaParameter.DefaultValue != null)
							{
								recordValue = recordValue.Concatenate(NavigationTableServices.NewDefaultValueMetadata(cdpaParameter.DefaultValue)).AsRecord;
							}
							TypeValue asType = cdpaParameter.Type.NewMeta(cdpaParameter.Type.MetaValue.Concatenate(recordValue).AsRecord).AsType;
							ParameterValue parameterValue = new ParameterValue(cube, cdpaParameter.QualifiedName.ToExpression(), 1, new string[] { "value" }, new TypeValue[] { asType });
							list.Add(RecordValue.New(CubeParametersTableValue.Columns, new Value[]
							{
								TextValue.New(cdpaParameter.QualifiedName.AsString),
								TextValue.New(cdpaParameter.Caption),
								LogicalValue.New(!cdpaParameter.IsRequired),
								parameterValue
							}));
						}
					}
					this.parametersTable = ListValue.New(list).ToTable(CubeParametersTableValue.Type);
				}
				return this.parametersTable;
			}

			// Token: 0x060061A8 RID: 25000 RVA: 0x0010322F File Offset: 0x0010142F
			public override TableValue GetAvailableProperties()
			{
				return CubePropertiesTableValue.Empty;
			}

			// Token: 0x060061A9 RID: 25001 RVA: 0x00066802 File Offset: 0x00064A02
			public override TableValue GetAvailableMeasureProperties()
			{
				return CubeMeasurePropertiesTableValue.Empty;
			}

			// Token: 0x060061AA RID: 25002 RVA: 0x0014EF00 File Offset: 0x0014D100
			public override IEnumerator<IValueReference> Evaluate()
			{
				IEnumerator<IValueReference> enumerator;
				try
				{
					enumerator = this.NewEvaluator().Evaluate().GetEnumerator();
				}
				catch (NotSupportedException)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.Cube_QueryNotSupported, null, null);
				}
				return enumerator;
			}

			// Token: 0x060061AB RID: 25003 RVA: 0x0014EF40 File Offset: 0x0014D140
			private CdpaSetContextProvider.CdpaSetContext.RequestEvaluator NewEvaluator()
			{
				if (this.request == null)
				{
					return new CdpaSetContextProvider.CdpaSetContext.EmptyRequestEvaluator(this);
				}
				CdpaFunnelRequest cdpaFunnelRequest = this.request as CdpaFunnelRequest;
				if (cdpaFunnelRequest != null)
				{
					return new CdpaSetContextProvider.CdpaSetContext.FunnelRequestEvaluator(this, cdpaFunnelRequest);
				}
				CdpaSeriesRequest cdpaSeriesRequest = this.request as CdpaSeriesRequest;
				if (cdpaSeriesRequest == null)
				{
					throw new NotSupportedException();
				}
				CdpaSignalsRequest cdpaSignalsRequest;
				string text;
				if (this.TryGetSignalsRequest(cdpaSeriesRequest, out cdpaSignalsRequest, out text))
				{
					return new CdpaSetContextProvider.CdpaSetContext.SignalsRequestEvaluator(this, cdpaSignalsRequest, text);
				}
				CdpaPropertiesRequest cdpaPropertiesRequest;
				string text2;
				string text3;
				if (this.TryGetPropertiesRequest(cdpaSeriesRequest, out cdpaPropertiesRequest, out text2, out text3))
				{
					return new CdpaSetContextProvider.CdpaSetContext.PropertiesRequestEvaluator(this, cdpaPropertiesRequest, text2, text3);
				}
				return new CdpaSetContextProvider.CdpaSetContext.SeriesRequestEvaluator(this, cdpaSeriesRequest);
			}

			// Token: 0x060061AC RID: 25004 RVA: 0x0014EFC4 File Offset: 0x0014D1C4
			private bool TryGetPropertiesRequest(CdpaSeriesRequest seriesRequest, out CdpaPropertiesRequest propertiesRequest, out string property, out string countOperandId)
			{
				ITimeGranularity timeGranularity = seriesRequest.Granularity ?? TimePartsTimeGranularity.Coarsest;
				if (TimePartsTimeGranularity.Coarsest.Equals(timeGranularity) && seriesRequest.Metric.Operands.Count == 1)
				{
					CdpaOperand cdpaOperand = seriesRequest.Metric.Operands.Single<CdpaOperand>();
					EventCountCdpaMetricMeasure eventCountCdpaMetricMeasure = cdpaOperand.Measure as EventCountCdpaMetricMeasure;
					if (cdpaOperand.Table.Dimensions.Count == 1 && (cdpaOperand.Measure == null || eventCountCdpaMetricMeasure != null) && cdpaOperand.Splits.Count == 1)
					{
						countOperandId = ((eventCountCdpaMetricMeasure != null) ? cdpaOperand.Id : null);
						bool flag = true;
						CdpaMetricSplit split = cdpaOperand.Splits.First<CdpaMetricSplit>();
						if (!split.IsRestricted)
						{
							List<CdpaEvent> list = new List<CdpaEvent>();
							Func<CdpaDimensionAttribute, bool> <>9__0;
							foreach (CdpaEvent cdpaEvent in cdpaOperand.Table.Events)
							{
								QualifiedName qualifiedName = QualifiedName.New(cdpaEvent.EventName);
								IEnumerable<CdpaDimensionAttribute> values = ((CdpaSignalDimension)this.cdpaCube.Dimensions[qualifiedName]).Attributes.Values;
								Func<CdpaDimensionAttribute, bool> func;
								if ((func = <>9__0) == null)
								{
									func = (<>9__0 = (CdpaDimensionAttribute a) => a.PropertyName == split.PropertyName);
								}
								if (!values.Any(func))
								{
									flag = false;
									break;
								}
								if (!CdpaSetContextProvider.IsVirtualTable(cdpaEvent))
								{
									list.Add(cdpaEvent);
								}
							}
							bool flag2 = true;
							CdpaValue cdpaValue = null;
							if (cdpaOperand.Table.Filters != null)
							{
								flag2 = false;
								IList<CdpaPropertyFilterAndGroup> propertyFilterAndGroup = cdpaOperand.Table.Filters.PropertyFilterAndGroup;
								if (propertyFilterAndGroup.Count == 1)
								{
									IList<CdpaPropertyFilter> propertyFilters = propertyFilterAndGroup.First<CdpaPropertyFilterAndGroup>().PropertyFilters;
									if (propertyFilters.Count == 1)
									{
										CdpaPropertyFilter cdpaPropertyFilter = propertyFilters.First<CdpaPropertyFilter>();
										if (cdpaPropertyFilter.PropertyName == split.PropertyName && cdpaPropertyFilter.Operator == "contains")
										{
											cdpaValue = ((ContainsStringCdpaPropertyFilter)cdpaPropertyFilter).Value;
											if (cdpaValue.Type == "string")
											{
												flag2 = true;
											}
										}
									}
								}
							}
							if (flag && list.Count > 0 && flag2)
							{
								propertiesRequest = new CdpaPropertiesRequest
								{
									Configuration = new CdpaSignalsConfiguration
									{
										Table = new CdpaTableConfiguration
										{
											Dimensions = cdpaOperand.Table.Dimensions,
											Events = list
										}
									}
								};
								if (cdpaValue != null)
								{
									propertiesRequest.SearchString = (string)cdpaValue.Value;
								}
								property = split.PropertyName;
								return true;
							}
						}
					}
				}
				propertiesRequest = null;
				property = null;
				countOperandId = null;
				return false;
			}

			// Token: 0x060061AD RID: 25005 RVA: 0x0014F270 File Offset: 0x0014D470
			private bool TryGetSignalsRequest(CdpaSeriesRequest seriesRequest, out CdpaSignalsRequest signalsRequest, out string timestampProperty)
			{
				ITimeGranularity timeGranularity = seriesRequest.Granularity ?? TimePartsTimeGranularity.Coarsest;
				if (TimePartsTimeGranularity.Finest.Equals(timeGranularity) && seriesRequest.Metric.Operands.Count == 1)
				{
					CdpaOperand cdpaOperand = seriesRequest.Metric.Operands.Single<CdpaOperand>();
					if (cdpaOperand.Measure == null)
					{
						HashSet<string> hashSet = new HashSet<string>();
						foreach (CdpaMetricSplit cdpaMetricSplit in cdpaOperand.Splits)
						{
							hashSet.Add(cdpaMetricSplit.PropertyName);
						}
						timestampProperty = null;
						bool flag = true;
						foreach (CdpaEvent cdpaEvent in cdpaOperand.Table.Events)
						{
							QualifiedName qualifiedName = QualifiedName.New(cdpaEvent.EventName);
							foreach (CdpaDimensionAttribute cdpaDimensionAttribute in ((CdpaSignalDimension)this.cdpaCube.Dimensions[qualifiedName]).Attributes.Values)
							{
								if (this.cdpaCube.IsTimestampAttribute(cdpaDimensionAttribute.QualifiedName.ToExpression()))
								{
									timestampProperty = ((CdpaSignalDimensionAttribute)cdpaDimensionAttribute).PropertyName;
								}
								else if (!hashSet.Contains(cdpaDimensionAttribute.PropertyName))
								{
									flag = false;
									break;
								}
							}
						}
						if (timestampProperty != null && flag)
						{
							int num = int.MaxValue;
							if (!base.Set.TakeCount.IsInfinite && base.Set.TakeCount.Value <= 2147483647L)
							{
								num = (int)base.Set.TakeCount.Value;
							}
							signalsRequest = new CdpaSignalsRequest
							{
								Configuration = new CdpaSignalsConfiguration
								{
									Table = cdpaOperand.Table
								},
								TimeRange = seriesRequest.TimeRange,
								Limit = num,
								ResponseFormat = "jsonDataTable",
								ExtraParameters = seriesRequest.ExtraParameters
							};
							return true;
						}
					}
				}
				signalsRequest = null;
				timestampProperty = null;
				return false;
			}

			// Token: 0x060061AE RID: 25006 RVA: 0x0014F4B8 File Offset: 0x0014D6B8
			public override void ReportFoldingFailure()
			{
				IFoldingFailureService foldingFailureService = this.provider.service.EngineHost.QueryService<IFoldingFailureService>();
				if (foldingFailureService != null && foldingFailureService.ThrowOnFoldingFailure)
				{
					throw ValueException.NewExpressionError<Message0>(Strings.FoldingFailure, null, null);
				}
			}

			// Token: 0x060061AF RID: 25007 RVA: 0x0014F4F4 File Offset: 0x0014D6F4
			private void AddDimensionContent(CubeObjectTableBuilder dimensionBuilder, CdpaDimension dimension)
			{
				foreach (CdpaDimensionAttribute cdpaDimensionAttribute in CdpaSetContextProvider.Order(dimension.Attributes.Values))
				{
					dimensionBuilder.AddDimensionAttribute(cdpaDimensionAttribute.QualifiedName.AsString, cdpaDimensionAttribute.Caption, string.Empty, dimension.QualifiedName.AsString);
				}
			}

			// Token: 0x060061B0 RID: 25008 RVA: 0x0014F56C File Offset: 0x0014D76C
			private TableValue CreateDimensionTable(CdpaDimension dimension)
			{
				Dimensionality dimensionality = new Dimensionality(from a in CdpaSetContextProvider.Order(dimension.Attributes.Values)
					select new CubeLevelRange(a, a));
				Set set = EverythingSet.Instance.ExpandTo(dimensionality);
				int num = -1;
				KeysBuilder keysBuilder = default(KeysBuilder);
				ArrayBuilder<IdentifierCubeExpression> arrayBuilder = default(ArrayBuilder<IdentifierCubeExpression>);
				foreach (CdpaDimensionAttribute cdpaDimensionAttribute in CdpaSetContextProvider.Order(dimension.Attributes.Values))
				{
					keysBuilder.Add(cdpaDimensionAttribute.PropertyName);
					arrayBuilder.Add(cdpaDimensionAttribute.Identifier);
					if (cdpaDimensionAttribute is CdpaSignalDimensionAttribute)
					{
						if (num == -1)
						{
							num = keysBuilder.Count - 1;
						}
						else
						{
							num = -2;
						}
					}
				}
				SetContextProvider setContextProvider = this.provider;
				IList<ParameterArguments> parameterArguments = base.ParameterArguments;
				ICube cube = this.cube;
				Set set2 = set;
				Projection projection = new Projection(keysBuilder.ToKeys(), arrayBuilder.ToArray());
				TableKey[] array;
				if (num < 0)
				{
					array = EmptyArray<TableKey>.Instance;
				}
				else
				{
					(array = new TableKey[1])[0] = new TableKey(new int[] { num }, false);
				}
				return SetContextTableValue.New(new SetContextQuery(setContextProvider, parameterArguments, cube, set2, projection, array));
			}

			// Token: 0x04003524 RID: 13604
			private const int defaultRecordSize = 4;

			// Token: 0x04003525 RID: 13605
			private static readonly TypeValue delayedNullableListType = PreviewServices.ConvertToDelayedValue(TypeValue.List.Nullable, "Value");

			// Token: 0x04003526 RID: 13606
			private static readonly RecordTypeValue allowedValuesMetaType = RecordTypeValue.New(RecordValue.New(new NamedValue[]
			{
				new NamedValue("Documentation.AllowedValues", RecordTypeAlgebra.NewField(CdpaSetContextProvider.CdpaSetContext.delayedNullableListType, false))
			}), false);

			// Token: 0x04003527 RID: 13607
			private readonly string timestampColumnName;

			// Token: 0x04003528 RID: 13608
			private readonly CdpaSetContextProvider provider;

			// Token: 0x04003529 RID: 13609
			private readonly CdpaCube cdpaCube;

			// Token: 0x0400352A RID: 13610
			private readonly ICube cube;

			// Token: 0x0400352B RID: 13611
			private readonly CdpaRequestWithTimeConfigurationAndResponseProtocol request;

			// Token: 0x0400352C RID: 13612
			private readonly bool truncatedResult;

			// Token: 0x0400352D RID: 13613
			private TableValue capabilities;

			// Token: 0x0400352E RID: 13614
			private TableValue dimensionsTable;

			// Token: 0x0400352F RID: 13615
			private TableValue measuresTable;

			// Token: 0x04003530 RID: 13616
			private TableValue parametersTable;

			// Token: 0x02000E28 RID: 3624
			private abstract class RequestEvaluator
			{
				// Token: 0x060061B2 RID: 25010 RVA: 0x0014F700 File Offset: 0x0014D900
				protected RequestEvaluator(CdpaSetContextProvider.CdpaSetContext context)
				{
					this.context = context;
				}

				// Token: 0x060061B3 RID: 25011 RVA: 0x0014F70F File Offset: 0x0014D90F
				public IEnumerable<IValueReference> Evaluate()
				{
					Value value = this.ExecuteQuery();
					string text = Strings.Cdpa_RequestId_None;
					Value value2;
					if (value.MetaValue.TryGetValue("X-Request-ID", out value2) && value2.IsText)
					{
						text = value2.AsString;
					}
					ListValue listValue = ListValue.New(value["executionInfo"].AsRecord["messages"].AsList.Where((IValueReference m) => m.Value.AsRecord["type"].AsString == "error"));
					if (!listValue.IsEmpty)
					{
						string asString = listValue.First<IValueReference>().Value.AsRecord["message"].AsString;
						throw this.context.provider.service.NewServiceError(Strings.Cdpa_RequestError(text, asString), null);
					}
					IEnumerable<IValueReference> enumerable = this.ToTable(value);
					enumerable = new CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.ResponseExceptionTranslatingEnumerable(this, text, enumerable);
					long rowCount = long.MaxValue;
					if (!this.context.Set.TakeCount.IsInfinite)
					{
						rowCount = this.context.Set.TakeCount.Value;
					}
					foreach (IValueReference valueReference in enumerable)
					{
						yield return valueReference;
						long num = rowCount;
						rowCount = num - 1L;
						if (rowCount == 0L)
						{
							yield break;
						}
					}
					IEnumerator<IValueReference> enumerator = null;
					if (rowCount > 0L && this.TruncatedResult)
					{
						throw this.context.provider.service.NewServiceError(Strings.Cdpa_TruncatedResult, null);
					}
					yield break;
					yield break;
				}

				// Token: 0x060061B4 RID: 25012 RVA: 0x0014F720 File Offset: 0x0014D920
				protected ITimeGranularity GetSupportedGranularity(ITimeGranularity granularity)
				{
					TimePartsTimeGranularity coarsest;
					if (!granularity.TryGetTimePartsGranularity(out coarsest))
					{
						coarsest = TimePartsTimeGranularity.Coarsest;
					}
					MetricGranularityTimeGranularity metricGranularityTimeGranularity;
					if (!coarsest.TryGetMetricGranularity(out metricGranularityTimeGranularity))
					{
						throw this.context.provider.service.NewServiceError(Strings.Cdpa_GranularityNotSupported(coarsest.ToJson()), null);
					}
					return metricGranularityTimeGranularity;
				}

				// Token: 0x060061B5 RID: 25013 RVA: 0x0014F76F File Offset: 0x0014D96F
				protected static string GetPropertyMetricName(string operationName, string propertyName)
				{
					return QualifiedName.New(operationName).Qualify("(" + propertyName + ")").AsString;
				}

				// Token: 0x060061B6 RID: 25014 RVA: 0x0014F791 File Offset: 0x0014D991
				protected static string GetEventCountMetricName()
				{
					return CdpaQueryGenerator.eventCountOperationCtor().Name + "()";
				}

				// Token: 0x060061B7 RID: 25015 RVA: 0x0014F7AC File Offset: 0x0014D9AC
				protected static string GetOperandQualifiedName(string operandId, string name)
				{
					return QualifiedName.New(operandId).Qualify(name).AsString;
				}

				// Token: 0x060061B8 RID: 25016 RVA: 0x0014F7C0 File Offset: 0x0014D9C0
				protected static IEnumerable<IValueReference> TransformRows(IEnumerable<IValueReference> rows, RecordTypeValue targetRowType, List<Func<RecordValue, Value>> projections)
				{
					return rows.Select((IValueReference r) => RecordValue.New(targetRowType, (int i) => projections[i](r.Value.AsRecord)));
				}

				// Token: 0x17001CB5 RID: 7349
				// (get) Token: 0x060061B9 RID: 25017
				protected abstract bool TruncatedResult { get; }

				// Token: 0x060061BA RID: 25018
				protected abstract Value ExecuteQuery();

				// Token: 0x060061BB RID: 25019
				protected abstract IEnumerable<IValueReference> ToTable(Value response);

				// Token: 0x060061BC RID: 25020 RVA: 0x0014F7F4 File Offset: 0x0014D9F4
				protected IEnumerable<IValueReference> ConvertColumnTypes(IEnumerable<IValueReference> rows, RecordTypeValue targetRowType)
				{
					TransformTypesHelper transformTypesHelper = new TransformTypesHelper(this.context.provider.service.EngineHost, null);
					List<Func<RecordValue, Value>> list = new List<Func<RecordValue, Value>>();
					for (int i = 0; i < targetRowType.Fields.Keys.Length; i++)
					{
						int fieldIndex = i;
						TypeValue asType = targetRowType.Fields[fieldIndex]["Type"].AsType;
						FunctionValue function = transformTypesHelper.GetFunctionValueFromType(asType, asType, false);
						list.Add((RecordValue row) => function.Invoke(row[fieldIndex]));
					}
					return CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.TransformRows(rows, targetRowType, list);
				}

				// Token: 0x04003531 RID: 13617
				protected readonly CdpaSetContextProvider.CdpaSetContext context;

				// Token: 0x02000E29 RID: 3625
				private class ResponseExceptionTranslatingEnumerable : IEnumerable<IValueReference>, IEnumerable
				{
					// Token: 0x060061BD RID: 25021 RVA: 0x0014F89A File Offset: 0x0014DA9A
					public ResponseExceptionTranslatingEnumerable(CdpaSetContextProvider.CdpaSetContext.RequestEvaluator evaluator, string requestId, IEnumerable<IValueReference> enumerable)
					{
						this.evaluator = evaluator;
						this.requestId = requestId;
						this.enumerable = enumerable;
					}

					// Token: 0x060061BE RID: 25022 RVA: 0x0014F8B7 File Offset: 0x0014DAB7
					IEnumerator IEnumerable.GetEnumerator()
					{
						return this.GetEnumerator();
					}

					// Token: 0x060061BF RID: 25023 RVA: 0x0014F8BF File Offset: 0x0014DABF
					public IEnumerator<IValueReference> GetEnumerator()
					{
						return new CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.ResponseExceptionTranslatingEnumerable.Enumerator(this, this.enumerable.GetEnumerator());
					}

					// Token: 0x04003532 RID: 13618
					private readonly CdpaSetContextProvider.CdpaSetContext.RequestEvaluator evaluator;

					// Token: 0x04003533 RID: 13619
					private readonly string requestId;

					// Token: 0x04003534 RID: 13620
					private readonly IEnumerable<IValueReference> enumerable;

					// Token: 0x02000E2A RID: 3626
					private class Enumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
					{
						// Token: 0x060061C0 RID: 25024 RVA: 0x0014F8D2 File Offset: 0x0014DAD2
						public Enumerator(CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.ResponseExceptionTranslatingEnumerable enumerable, IEnumerator<IValueReference> enumerator)
						{
							this.enumerable = enumerable;
							this.enumerator = enumerator;
						}

						// Token: 0x17001CB6 RID: 7350
						// (get) Token: 0x060061C1 RID: 25025 RVA: 0x0014F8E8 File Offset: 0x0014DAE8
						object IEnumerator.Current
						{
							get
							{
								return this.Current;
							}
						}

						// Token: 0x17001CB7 RID: 7351
						// (get) Token: 0x060061C2 RID: 25026 RVA: 0x0014F8F0 File Offset: 0x0014DAF0
						public IValueReference Current
						{
							get
							{
								IValueReference valueReference;
								try
								{
									valueReference = this.enumerator.Current;
								}
								catch (NotSupportedException ex)
								{
									throw this.enumerable.evaluator.context.provider.service.NewServiceError(Strings.Cdpa_ResponseError(this.enumerable.requestId), ex);
								}
								return valueReference;
							}
						}

						// Token: 0x060061C3 RID: 25027 RVA: 0x0014F954 File Offset: 0x0014DB54
						public bool MoveNext()
						{
							bool flag;
							try
							{
								flag = this.enumerator.MoveNext();
							}
							catch (NotSupportedException ex)
							{
								throw this.enumerable.evaluator.context.provider.service.NewServiceError(Strings.Cdpa_ResponseError(this.enumerable.requestId), ex);
							}
							return flag;
						}

						// Token: 0x060061C4 RID: 25028 RVA: 0x000033E7 File Offset: 0x000015E7
						public void Reset()
						{
							throw new NotSupportedException();
						}

						// Token: 0x060061C5 RID: 25029 RVA: 0x0014F9B8 File Offset: 0x0014DBB8
						public void Dispose()
						{
							if (this.enumerator != null)
							{
								this.enumerator.Dispose();
								this.enumerator = null;
							}
						}

						// Token: 0x04003535 RID: 13621
						private readonly CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.ResponseExceptionTranslatingEnumerable enumerable;

						// Token: 0x04003536 RID: 13622
						private IEnumerator<IValueReference> enumerator;
					}
				}
			}

			// Token: 0x02000E30 RID: 3632
			private class EmptyRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.RequestEvaluator
			{
				// Token: 0x060061D8 RID: 25048 RVA: 0x0014FD87 File Offset: 0x0014DF87
				public EmptyRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context)
					: base(context)
				{
				}

				// Token: 0x17001CBA RID: 7354
				// (get) Token: 0x060061D9 RID: 25049 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool TruncatedResult
				{
					get
					{
						return false;
					}
				}

				// Token: 0x060061DA RID: 25050 RVA: 0x0014FD90 File Offset: 0x0014DF90
				protected override Value ExecuteQuery()
				{
					return RecordValue.New(Keys.New("executionInfo"), new Value[] { RecordValue.New(Keys.New("messages"), new Value[] { ListValue.Empty }) });
				}

				// Token: 0x060061DB RID: 25051 RVA: 0x0014FDD2 File Offset: 0x0014DFD2
				protected override IEnumerable<IValueReference> ToTable(Value response)
				{
					yield break;
				}
			}

			// Token: 0x02000E32 RID: 3634
			private abstract class SingleMetricRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.RequestEvaluator
			{
				// Token: 0x060061E4 RID: 25060 RVA: 0x0014FD87 File Offset: 0x0014DF87
				protected SingleMetricRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context)
					: base(context)
				{
				}

				// Token: 0x17001CBD RID: 7357
				// (get) Token: 0x060061E5 RID: 25061 RVA: 0x0014FE63 File Offset: 0x0014E063
				protected RecordTypeValue ResponseRowType
				{
					get
					{
						if (this.responseRowType == null)
						{
							this.responseRowType = this.NewResponseRowType();
						}
						return this.responseRowType;
					}
				}

				// Token: 0x17001CBE RID: 7358
				// (get) Token: 0x060061E6 RID: 25062
				protected abstract CdpaMetricConfiguration Metric { get; }

				// Token: 0x060061E7 RID: 25063
				protected abstract IEnumerable<IValueReference> ToResponseTable(Value response);

				// Token: 0x060061E8 RID: 25064 RVA: 0x0014FE80 File Offset: 0x0014E080
				protected override IEnumerable<IValueReference> ToTable(Value response)
				{
					IEnumerable<IValueReference> enumerable = this.ToResponseTable(response);
					enumerable = base.ConvertColumnTypes(enumerable, this.ResponseRowType);
					return this.ProjectToResultTable(enumerable);
				}

				// Token: 0x060061E9 RID: 25065 RVA: 0x0014FEAC File Offset: 0x0014E0AC
				protected virtual RecordTypeValue NewResponseRowType()
				{
					Dictionary<string, TypeValue> dictionary = new Dictionary<string, TypeValue>();
					foreach (CdpaDimensionAttribute cdpaDimensionAttribute in this.context.cdpaCube.Attributes.Values)
					{
						CdpaSignalDimensionAttribute cdpaSignalDimensionAttribute = cdpaDimensionAttribute as CdpaSignalDimensionAttribute;
						if (cdpaSignalDimensionAttribute != null)
						{
							dictionary[cdpaSignalDimensionAttribute.PropertyName] = cdpaSignalDimensionAttribute.Type;
						}
					}
					HashSet<string> hashSet = new HashSet<string>();
					RecordBuilder recordBuilder = new RecordBuilder(4);
					recordBuilder.Add(this.context.timestampColumnName, RecordTypeValue.NewField(TypeValue.DateTime.Nullable, null), TypeValue.Record);
					foreach (CdpaOperand cdpaOperand in this.Metric.Operands)
					{
						foreach (CdpaMetricSplit cdpaMetricSplit in cdpaOperand.Splits)
						{
							if (hashSet.Add(cdpaMetricSplit.PropertyName))
							{
								TypeValue any;
								if (!dictionary.TryGetValue(cdpaMetricSplit.PropertyName, out any))
								{
									any = TypeValue.Any;
								}
								recordBuilder.Add(cdpaMetricSplit.PropertyName, RecordTypeValue.NewField(any, null), TypeValue.Record);
							}
						}
					}
					foreach (CdpaOperand cdpaOperand2 in this.Metric.Operands)
					{
						if (cdpaOperand2.Measure != null)
						{
							foreach (CdpaOperation cdpaOperation in cdpaOperand2.Measure.Operations)
							{
								string text = null;
								MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = cdpaOperand2.Measure as MetricPropertyCdpaMetricMeasure;
								if (metricPropertyCdpaMetricMeasure != null)
								{
									text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetPropertyMetricName(cdpaOperation.Name, metricPropertyCdpaMetricMeasure.PropertyName);
								}
								if (cdpaOperand2.Measure is EventCountCdpaMetricMeasure)
								{
									text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetEventCountMetricName();
								}
								if (text == null)
								{
									throw new NotSupportedException();
								}
								text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand2.Id, text);
								recordBuilder.Add(text, RecordTypeValue.NewField(TypeValue.Any, null), TypeValue.Record);
							}
						}
					}
					return RecordTypeValue.New(recordBuilder.ToRecord(), false);
				}

				// Token: 0x060061EA RID: 25066 RVA: 0x0015012C File Offset: 0x0014E32C
				protected IEnumerable<IValueReference> ProjectToResultTable(IEnumerable<IValueReference> rows)
				{
					List<Func<RecordValue, Value>> list = new List<Func<RecordValue, Value>>();
					RecordBuilder recordBuilder = new RecordBuilder(4);
					CdpaSetContextProvider.CdpaSetContext.SingleMetricRequestEvaluator.MapToResultColumnsVisitor mapToResultColumnsVisitor = new CdpaSetContextProvider.CdpaSetContext.SingleMetricRequestEvaluator.MapToResultColumnsVisitor(this.context, this.ResponseRowType);
					foreach (ICubeObject cubeObject in this.context.Set.GetResultObjects())
					{
						ICubeObject2 cubeObject2 = (ICubeObject2)cubeObject;
						RecordValue recordValue = RecordTypeValue.NewField(cubeObject2.Type, LogicalValue.False);
						recordBuilder.Add(cubeObject2.Identifier.Identifier, recordValue, TypeValue.Record);
						ScopePath scopePath;
						ICubeObject2 cubeObject3 = (ICubeObject2)cubeObject.GetUnscoped(out scopePath);
						CubeExpression cubeExpression = mapToResultColumnsVisitor.MapToResultColumns(cubeObject3.Identifier);
						QueryExpression queryExpression = new CubeExpressionToQueryExpressionVisitor(this.ResponseRowType.Fields.Keys, this.context.timestampColumnName, new Func<IdentifierCubeExpression, bool>(this.context.cdpaCube.IsTimestampAttribute)).Translate(cubeExpression);
						FunctionValue function = QueryExpressionAssembler.Assemble(this.ResponseRowType.Fields.Keys, queryExpression);
						list.Add((RecordValue row) => function.Invoke(row));
					}
					RecordTypeValue recordTypeValue = RecordTypeValue.New(recordBuilder.ToRecord(), false);
					return CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.TransformRows(rows, recordTypeValue, list);
				}

				// Token: 0x04003548 RID: 13640
				private RecordTypeValue responseRowType;

				// Token: 0x02000E33 RID: 3635
				private class MapToResultColumnsVisitor : CubeExpressionVisitor
				{
					// Token: 0x060061EB RID: 25067 RVA: 0x00150288 File Offset: 0x0014E488
					public MapToResultColumnsVisitor(CdpaSetContextProvider.CdpaSetContext context, RecordTypeValue rowType)
					{
						this.context = context;
						this.rowType = rowType;
						this.metricToOperandMap = new Dictionary<string, CdpaOperand>();
						this.PopulateMetricToOperandMap();
					}

					// Token: 0x060061EC RID: 25068 RVA: 0x00072ED2 File Offset: 0x000710D2
					public CubeExpression MapToResultColumns(CubeExpression expression)
					{
						return this.Visit(expression);
					}

					// Token: 0x060061ED RID: 25069 RVA: 0x001502B0 File Offset: 0x0014E4B0
					protected override CubeExpression VisitInvocation(InvocationCubeExpression invocation)
					{
						CubeExpression cubeExpression = base.VisitInvocation(invocation);
						FunctionValue functionValue;
						IList<CubeExpression> list;
						IdentifierCubeExpression identifierCubeExpression;
						Func<CdpaMetricPropertyOperation> func;
						if (cubeExpression.TryGetInvocation(out functionValue, out list) && list.Count == 1 && list[0].TryGetIdentifier(out identifierCubeExpression) && CdpaQueryGenerator.operationCtors.TryGetValue(functionValue, out func))
						{
							string propertyMetricName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetPropertyMetricName(func().Name, identifierCubeExpression.Identifier);
							CdpaOperand cdpaOperand;
							if (this.metricToOperandMap.TryGetValue(propertyMetricName, out cdpaOperand))
							{
								return new IdentifierCubeExpression(CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand.Id, propertyMetricName));
							}
						}
						return cubeExpression;
					}

					// Token: 0x060061EE RID: 25070 RVA: 0x00150338 File Offset: 0x0014E538
					protected override CubeExpression VisitIdentifier(IdentifierCubeExpression identifier)
					{
						ICubeObject measure;
						if (this.context.cube.TryGetObject(identifier, out measure))
						{
							CdpaHierarchyLevel cdpaHierarchyLevel = measure as CdpaHierarchyLevel;
							if (cdpaHierarchyLevel != null)
							{
								return this.Visit(cdpaHierarchyLevel.Attribute.QualifiedName.ToExpression());
							}
							CdpaSignalDimensionAttribute cdpaSignalDimensionAttribute = measure as CdpaSignalDimensionAttribute;
							if (cdpaSignalDimensionAttribute != null)
							{
								if (this.context.cdpaCube.IsTimestampAttribute(identifier))
								{
									return new IdentifierCubeExpression(this.context.timestampColumnName);
								}
								return new IdentifierCubeExpression(cdpaSignalDimensionAttribute.PropertyName);
							}
							else
							{
								CdpaRelatedDimensionAttribute cdpaRelatedDimensionAttribute = measure as CdpaRelatedDimensionAttribute;
								if (cdpaRelatedDimensionAttribute != null)
								{
									foreach (CdpaDimensionAttribute cdpaDimensionAttribute in cdpaRelatedDimensionAttribute.RelatedAttributes)
									{
										cdpaSignalDimensionAttribute = cdpaDimensionAttribute as CdpaSignalDimensionAttribute;
										if (cdpaSignalDimensionAttribute != null)
										{
											IdentifierCubeExpression identifierCubeExpression = cdpaSignalDimensionAttribute.QualifiedName.ToExpression();
											if (this.rowType.Fields.Keys.IndexOfKey(cdpaSignalDimensionAttribute.PropertyName) != -1 || this.context.cdpaCube.IsTimestampAttribute(identifierCubeExpression))
											{
												return this.Visit(identifierCubeExpression);
											}
										}
									}
									throw new NotSupportedException();
								}
								CdpaProjectedDimensionAttribute cdpaProjectedDimensionAttribute = measure as CdpaProjectedDimensionAttribute;
								if (cdpaProjectedDimensionAttribute != null)
								{
									return this.Visit(cdpaProjectedDimensionAttribute.Projection);
								}
								OrderByTopCdpaMeasure orderByTopCdpaMeasure = measure as OrderByTopCdpaMeasure;
								if (orderByTopCdpaMeasure != null)
								{
									measure = orderByTopCdpaMeasure.Measure;
								}
								CdpaProjectedMeasure cdpaProjectedMeasure = measure as CdpaProjectedMeasure;
								if (cdpaProjectedMeasure != null)
								{
									return this.Visit(cdpaProjectedMeasure.Projection);
								}
								if (measure is CdpaRowCountMeasure)
								{
									string eventCountMetricName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetEventCountMetricName();
									CdpaOperand cdpaOperand;
									if (this.metricToOperandMap.TryGetValue(eventCountMetricName, out cdpaOperand))
									{
										return new IdentifierCubeExpression(CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand.Id, eventCountMetricName));
									}
									throw new NotSupportedException();
								}
							}
						}
						return base.VisitIdentifier(identifier);
					}

					// Token: 0x060061EF RID: 25071 RVA: 0x001504F0 File Offset: 0x0014E6F0
					private IEnumerable<CdpaSignalDimension> GetSignalDimensions(IEnumerable<CdpaDimension> dimensions)
					{
						HashSet<CdpaSignalDimension> hashSet = new HashSet<CdpaSignalDimension>();
						foreach (CdpaDimension cdpaDimension in dimensions)
						{
							CdpaSignalDimension cdpaSignalDimension = cdpaDimension as CdpaSignalDimension;
							if (cdpaSignalDimension == null)
							{
								throw new NotSupportedException();
							}
							hashSet.Add(cdpaSignalDimension);
						}
						return hashSet;
					}

					// Token: 0x060061F0 RID: 25072 RVA: 0x00150550 File Offset: 0x0014E750
					private void PopulateMetricToOperandMap()
					{
						foreach (CdpaOperand cdpaOperand in ((CdpaSeriesRequest)this.context.request).Metric.Operands)
						{
							if (cdpaOperand.Measure != null)
							{
								MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = cdpaOperand.Measure as MetricPropertyCdpaMetricMeasure;
								foreach (CdpaOperation cdpaOperation in cdpaOperand.Measure.Operations)
								{
									string text = null;
									if (cdpaOperation is CdpaMetricPropertyOperation)
									{
										text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetPropertyMetricName(cdpaOperation.Name, metricPropertyCdpaMetricMeasure.PropertyName);
									}
									if (cdpaOperation is CdpaEventCountOperation)
									{
										cdpaOperand.Table.Events.Select((CdpaEvent e) => e.EventName);
										text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetEventCountMetricName();
									}
									if (text == null || this.metricToOperandMap.ContainsKey(text))
									{
										throw new NotSupportedException();
									}
									this.metricToOperandMap.Add(text, cdpaOperand);
								}
							}
						}
					}

					// Token: 0x04003549 RID: 13641
					private readonly CdpaSetContextProvider.CdpaSetContext context;

					// Token: 0x0400354A RID: 13642
					private readonly RecordTypeValue rowType;

					// Token: 0x0400354B RID: 13643
					private readonly Dictionary<string, CdpaOperand> metricToOperandMap;
				}
			}

			// Token: 0x02000E36 RID: 3638
			private class PropertiesRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.SingleMetricRequestEvaluator
			{
				// Token: 0x060061F6 RID: 25078 RVA: 0x001506B2 File Offset: 0x0014E8B2
				public PropertiesRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context, CdpaPropertiesRequest request, string property, string countOperandId)
					: base(context)
				{
					this.request = request;
					this.property = property;
					this.countOperandId = countOperandId;
				}

				// Token: 0x17001CBF RID: 7359
				// (get) Token: 0x060061F7 RID: 25079 RVA: 0x001506D4 File Offset: 0x0014E8D4
				protected override CdpaMetricConfiguration Metric
				{
					get
					{
						return new CdpaMetricConfiguration
						{
							Operands = new CdpaOperand[]
							{
								new CdpaOperand
								{
									Table = this.request.Configuration.Table
								}
							}
						};
					}
				}

				// Token: 0x17001CC0 RID: 7360
				// (get) Token: 0x060061F8 RID: 25080 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool TruncatedResult
				{
					get
					{
						return false;
					}
				}

				// Token: 0x060061F9 RID: 25081 RVA: 0x00150712 File Offset: 0x0014E912
				protected override Value ExecuteQuery()
				{
					return this.context.provider.service.ExecutePropertiesQuery(this.property, this.request.ToJson());
				}

				// Token: 0x060061FA RID: 25082 RVA: 0x0015073C File Offset: 0x0014E93C
				protected override RecordTypeValue NewResponseRowType()
				{
					if (this.countOperandId != null)
					{
						string eventCountMetricName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetEventCountMetricName();
						string operandQualifiedName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(this.countOperandId, eventCountMetricName);
						return RecordTypeValue.New(RecordValue.New(Keys.New(this.property, operandQualifiedName), new Value[]
						{
							RecordTypeValue.NewField(TypeValue.Text, null),
							RecordTypeValue.NewField(TypeValue.Int64, null)
						}));
					}
					return RecordTypeValue.New(RecordValue.New(Keys.New(this.property), new Value[] { RecordTypeValue.NewField(TypeValue.Text, null) }));
				}

				// Token: 0x060061FB RID: 25083 RVA: 0x001507C6 File Offset: 0x0014E9C6
				protected override IEnumerable<IValueReference> ToResponseTable(Value response)
				{
					foreach (IValueReference valueReference in response["values"].AsList)
					{
						Value value = valueReference.Value;
						yield return RecordValue.New(base.ResponseRowType, delegate(int i)
						{
							if (i == 0)
							{
								return value;
							}
							if (i != 1)
							{
								throw new InvalidOperationException();
							}
							return NumberValue.One;
						});
					}
					IEnumerator<IValueReference> enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x0400354F RID: 13647
				private readonly CdpaPropertiesRequest request;

				// Token: 0x04003550 RID: 13648
				private readonly string property;

				// Token: 0x04003551 RID: 13649
				private readonly string countOperandId;
			}

			// Token: 0x02000E39 RID: 3641
			private class SignalsRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.SingleMetricRequestEvaluator
			{
				// Token: 0x06006207 RID: 25095 RVA: 0x001509AC File Offset: 0x0014EBAC
				public SignalsRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context, CdpaSignalsRequest request, string timestampProperty)
					: base(context)
				{
					this.request = request.ShallowCopy();
					this.timestampProperty = timestampProperty;
					this.truncatedResult = context.truncatedResult;
					if (this.request.Limit > 10000)
					{
						this.request.Limit = 10000;
						this.truncatedResult = true;
					}
				}

				// Token: 0x17001CC3 RID: 7363
				// (get) Token: 0x06006208 RID: 25096 RVA: 0x00150A08 File Offset: 0x0014EC08
				protected override CdpaMetricConfiguration Metric
				{
					get
					{
						return new CdpaMetricConfiguration
						{
							Operands = new CdpaOperand[]
							{
								new CdpaOperand
								{
									Table = this.request.Configuration.Table
								}
							}
						};
					}
				}

				// Token: 0x17001CC4 RID: 7364
				// (get) Token: 0x06006209 RID: 25097 RVA: 0x00150A46 File Offset: 0x0014EC46
				protected override bool TruncatedResult
				{
					get
					{
						return this.truncatedResult;
					}
				}

				// Token: 0x0600620A RID: 25098 RVA: 0x00150A4E File Offset: 0x0014EC4E
				protected override Value ExecuteQuery()
				{
					return this.context.provider.service.ExecuteSignalsQuery(this.request.ToJson());
				}

				// Token: 0x0600620B RID: 25099 RVA: 0x00150A70 File Offset: 0x0014EC70
				protected override IEnumerable<IValueReference> ToResponseTable(Value response)
				{
					foreach (IValueReference valueReference in response["rows"].AsList)
					{
						RecordValue rowAsRecord = valueReference.Value.AsRecord;
						yield return RecordValue.New(base.ResponseRowType, delegate(int i)
						{
							string text = ((i == 0) ? this.timestampProperty : this.ResponseRowType.Fields.Keys[i]);
							Value @null;
							if (!rowAsRecord.TryGetValue(text, out @null))
							{
								@null = Value.Null;
							}
							return @null;
						});
					}
					IEnumerator<IValueReference> enumerator = null;
					yield break;
					yield break;
				}

				// Token: 0x0400355A RID: 13658
				private readonly CdpaSignalsRequest request;

				// Token: 0x0400355B RID: 13659
				private readonly string timestampProperty;

				// Token: 0x0400355C RID: 13660
				private readonly bool truncatedResult;
			}

			// Token: 0x02000E3C RID: 3644
			private class SeriesRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.SingleMetricRequestEvaluator
			{
				// Token: 0x06006217 RID: 25111 RVA: 0x00150C9B File Offset: 0x0014EE9B
				public SeriesRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context, CdpaSeriesRequest request)
					: base(context)
				{
					this.request = (CdpaSeriesRequest)request.ShallowCopy();
					this.request.Granularity = base.GetSupportedGranularity(this.request.Granularity);
				}

				// Token: 0x17001CC7 RID: 7367
				// (get) Token: 0x06006218 RID: 25112 RVA: 0x00150CD1 File Offset: 0x0014EED1
				protected override CdpaMetricConfiguration Metric
				{
					get
					{
						return this.request.Metric;
					}
				}

				// Token: 0x17001CC8 RID: 7368
				// (get) Token: 0x06006219 RID: 25113 RVA: 0x00150CDE File Offset: 0x0014EEDE
				protected override bool TruncatedResult
				{
					get
					{
						return this.context.truncatedResult;
					}
				}

				// Token: 0x0600621A RID: 25114 RVA: 0x00150CEB File Offset: 0x0014EEEB
				protected override Value ExecuteQuery()
				{
					return this.context.provider.service.ExecuteTimeSeriesQuery(this.request.ToJson());
				}

				// Token: 0x0600621B RID: 25115 RVA: 0x00150D0D File Offset: 0x0014EF0D
				protected override IEnumerable<IValueReference> ToResponseTable(Value response)
				{
					CdpaSetContextProvider.CdpaSetContext.SeriesRequestEvaluator.<>c__DisplayClass7_0 CS$<>8__locals1 = new CdpaSetContextProvider.CdpaSetContext.SeriesRequestEvaluator.<>c__DisplayClass7_0();
					CS$<>8__locals1.<>4__this = this;
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					foreach (CdpaOperand cdpaOperand in this.request.Metric.Operands)
					{
						if (cdpaOperand.Measure != null)
						{
							MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = cdpaOperand.Measure as MetricPropertyCdpaMetricMeasure;
							foreach (CdpaOperation cdpaOperation in cdpaOperand.Measure.Operations)
							{
								string text = null;
								if (cdpaOperation is CdpaMetricPropertyOperation)
								{
									string propertyMetricName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetPropertyMetricName(cdpaOperation.Name, metricPropertyCdpaMetricMeasure.PropertyName);
									text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand.Id, propertyMetricName);
								}
								if (cdpaOperation is CdpaEventCountOperation)
								{
									string eventCountMetricName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetEventCountMetricName();
									text = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand.Id, eventCountMetricName);
								}
								string operandQualifiedName = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(cdpaOperand.Id, cdpaOperation.Name);
								if (text == null || dictionary.ContainsKey(operandQualifiedName))
								{
									throw new NotSupportedException();
								}
								dictionary.Add(operandQualifiedName, text);
							}
						}
					}
					bool flag = false;
					HashSet<string> hashSet = new HashSet<string>();
					Dictionary<RecordValue, Dictionary<string, ListValue>> coordinateToOperationColumnToValuesMap = new Dictionary<RecordValue, Dictionary<string, ListValue>>();
					foreach (IValueReference valueReference in response["series"].AsList)
					{
						flag = true;
						RecordValue asRecord = valueReference.Value.AsRecord;
						RecordValue asRecord2 = asRecord["seriesInfo"].AsRecord;
						RecordValue asRecord3 = asRecord2["combination"].AsRecord;
						RecordValue asRecord4 = asRecord2["operation"].AsRecord;
						string operandQualifiedName2 = CdpaSetContextProvider.CdpaSetContext.RequestEvaluator.GetOperandQualifiedName(asRecord2["operandId"].AsString, asRecord4["name"].AsString);
						string text2;
						if (dictionary.TryGetValue(operandQualifiedName2, out text2))
						{
							Dictionary<string, ListValue> dictionary2;
							if (!coordinateToOperationColumnToValuesMap.TryGetValue(asRecord3, out dictionary2))
							{
								dictionary2 = new Dictionary<string, ListValue>();
								coordinateToOperationColumnToValuesMap.Add(asRecord3, dictionary2);
							}
							dictionary2.Add(text2, asRecord["values"].AsList);
							hashSet.Add(text2);
						}
					}
					CS$<>8__locals1.operationColumns = new string[hashSet.Count];
					int num = 0;
					for (int k = 0; k < base.ResponseRowType.Fields.Keys.Length; k++)
					{
						string text3 = base.ResponseRowType.Fields.Keys[k];
						if (hashSet.Contains(text3))
						{
							CS$<>8__locals1.operationColumns[num] = text3;
							num++;
						}
					}
					if (!flag)
					{
						yield break;
					}
					TimePartsTimeGranularity resultGranularity;
					if (!new MetricGranularityTimeGranularity
					{
						Granularity = response["granularity"].AsString
					}.TryGetTimePartsGranularity(out resultGranularity))
					{
						throw new NotSupportedException();
					}
					string[] array = response["interval"].AsString.Split(new char[] { '/' });
					DateTime utcDateTime = DateTimeOffset.Parse(array[0]).UtcDateTime;
					DateTime end = DateTimeOffset.Parse(array[1]).UtcDateTime;
					int valueIndex = 0;
					DateTime currentTimestamp = utcDateTime;
					while (currentTimestamp < end)
					{
						CdpaSetContextProvider.CdpaSetContext.SeriesRequestEvaluator.<>c__DisplayClass7_1 CS$<>8__locals2 = new CdpaSetContextProvider.CdpaSetContext.SeriesRequestEvaluator.<>c__DisplayClass7_1();
						CS$<>8__locals2.CS$<>8__locals1 = CS$<>8__locals1;
						CS$<>8__locals2.iterationTimestamp = currentTimestamp;
						foreach (KeyValuePair<RecordValue, Dictionary<string, ListValue>> keyValuePair in coordinateToOperationColumnToValuesMap)
						{
							RecordValue coordinate = keyValuePair.Key;
							Dictionary<string, ListValue> value = keyValuePair.Value;
							IValueReference[] rowValues = new IValueReference[CS$<>8__locals2.CS$<>8__locals1.operationColumns.Length];
							bool flag2 = false;
							for (int j = 0; j < rowValues.Length; j++)
							{
								ListValue listValue;
								if (value.TryGetValue(CS$<>8__locals2.CS$<>8__locals1.operationColumns[j], out listValue) && valueIndex < listValue.Count)
								{
									rowValues[j] = listValue[valueIndex];
									flag2 = true;
								}
								else
								{
									rowValues[j] = Value.Null;
								}
							}
							if (flag2)
							{
								yield return RecordValue.New(base.ResponseRowType, delegate(int i)
								{
									if (i == 0)
									{
										return DateTimeValue.New(CS$<>8__locals2.iterationTimestamp);
									}
									if (i < CS$<>8__locals2.CS$<>8__locals1.<>4__this.ResponseRowType.Fields.Keys.Length - CS$<>8__locals2.CS$<>8__locals1.operationColumns.Length)
									{
										return coordinate[CS$<>8__locals2.CS$<>8__locals1.<>4__this.ResponseRowType.Fields.Keys[i]];
									}
									return rowValues[i - (CS$<>8__locals2.CS$<>8__locals1.<>4__this.ResponseRowType.Fields.Keys.Length - CS$<>8__locals2.CS$<>8__locals1.operationColumns.Length)].Value;
								});
							}
						}
						Dictionary<RecordValue, Dictionary<string, ListValue>>.Enumerator enumerator4 = default(Dictionary<RecordValue, Dictionary<string, ListValue>>.Enumerator);
						int num2 = valueIndex;
						valueIndex = num2 + 1;
						CS$<>8__locals2 = null;
						currentTimestamp = resultGranularity.AddFinestPartTo(currentTimestamp);
					}
					yield break;
					yield break;
				}

				// Token: 0x04003566 RID: 13670
				private readonly CdpaSeriesRequest request;
			}

			// Token: 0x02000E41 RID: 3649
			private class FunnelRequestEvaluator : CdpaSetContextProvider.CdpaSetContext.RequestEvaluator
			{
				// Token: 0x06006229 RID: 25129 RVA: 0x0015148C File Offset: 0x0014F68C
				public FunnelRequestEvaluator(CdpaSetContextProvider.CdpaSetContext context, CdpaFunnelRequest funnelRequest)
					: base(context)
				{
					this.funnelRequest = (CdpaFunnelRequest)funnelRequest.ShallowCopy();
					this.funnelRequest.Granularity = base.GetSupportedGranularity(this.funnelRequest.Granularity);
					this.responseRowType = this.NewResponseRowType();
				}

				// Token: 0x17001CCB RID: 7371
				// (get) Token: 0x0600622A RID: 25130 RVA: 0x00002105 File Offset: 0x00000305
				protected override bool TruncatedResult
				{
					get
					{
						return false;
					}
				}

				// Token: 0x0600622B RID: 25131 RVA: 0x001514D9 File Offset: 0x0014F6D9
				protected override Value ExecuteQuery()
				{
					return this.context.provider.service.ExecuteFunnelQuery(this.funnelRequest.ToJson());
				}

				// Token: 0x0600622C RID: 25132 RVA: 0x001514FB File Offset: 0x0014F6FB
				protected override IEnumerable<IValueReference> ToTable(Value response)
				{
					Dictionary<string, int> dictionary = new Dictionary<string, int>();
					for (int i = 0; i < this.responseRowType.Fields.Keys.Length; i++)
					{
						ScopePath path;
						new IdentifierCubeExpression(this.responseRowType.Fields.Keys[i]).GetUnscoped(out path);
						CdpaFunnelStepDefinition cdpaFunnelStepDefinition = this.funnelRequest.DataModel.Steps.Where((CdpaFunnelStepDefinition s) => s.ScopePath.Equals(path)).SingleOrDefault<CdpaFunnelStepDefinition>();
						if (cdpaFunnelStepDefinition == null)
						{
							throw new NotSupportedException();
						}
						dictionary.Add(cdpaFunnelStepDefinition.StepName, i);
					}
					int num = 0;
					Value[] array = new Value[this.responseRowType.Fields.Keys.Length];
					foreach (IValueReference valueReference in response.AsRecord["rows"].AsList)
					{
						RecordValue asRecord = valueReference.Value.AsRecord;
						int num2;
						if (dictionary.TryGetValue(asRecord["StepId"].AsString, out num2))
						{
							array[num2] = asRecord["RawCount"];
							num++;
						}
					}
					if (num == array.Length)
					{
						yield return RecordValue.New(this.responseRowType, array);
						yield break;
					}
					if (num == 0)
					{
						yield break;
					}
					throw new NotSupportedException();
					yield break;
				}

				// Token: 0x0600622D RID: 25133 RVA: 0x00151514 File Offset: 0x0014F714
				private RecordTypeValue NewResponseRowType()
				{
					KeysBuilder keysBuilder = default(KeysBuilder);
					ArrayBuilder<RecordValue> arrayBuilder = default(ArrayBuilder<RecordValue>);
					foreach (ICubeObject cubeObject in this.context.Set.GetResultObjects())
					{
						ICubeObject2 cubeObject2 = (ICubeObject2)cubeObject;
						keysBuilder.Add(cubeObject2.Identifier.Identifier);
						arrayBuilder.Add(RecordTypeValue.NewField(cubeObject2.Type, null));
					}
					Keys keys = keysBuilder.ToKeys();
					Value[] array = arrayBuilder.ToArray();
					return RecordTypeValue.New(RecordValue.New(keys, array), false);
				}

				// Token: 0x0400357C RID: 13692
				private readonly CdpaFunnelRequest funnelRequest;

				// Token: 0x0400357D RID: 13693
				private readonly RecordTypeValue responseRowType;
			}
		}
	}
}

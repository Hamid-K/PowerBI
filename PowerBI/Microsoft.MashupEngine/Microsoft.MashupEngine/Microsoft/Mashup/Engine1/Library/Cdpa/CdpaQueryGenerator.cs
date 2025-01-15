using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000E16 RID: 3606
	internal class CdpaQueryGenerator : SetVisitor<CdpaDataflowGraph, CubeExpression, CubeSortOrder>
	{
		// Token: 0x06006103 RID: 24835 RVA: 0x0014A930 File Offset: 0x00148B30
		public CdpaQueryGenerator(CdpaService service, ICube cube)
		{
			this.service = service;
			this.cube = cube;
			this.unscopedCube = (CdpaCube)this.cube.GetUnscoped();
			this.eventsAndSplitsHelper = new EventsAndSplitsHelper(this.service, this.cube);
			this.attributeInliner = new InlineAttributesVisitor(this.cube);
			this.filterTranslator = new CdpaQueryGenerator.FilterTranslator(this);
			this.metricMerger = new CdpaQueryGenerator.MetricMerger(this);
			this.metricUnioner = new CdpaQueryGenerator.MetricUnioner(this);
		}

		// Token: 0x06006104 RID: 24836 RVA: 0x0014A9B3 File Offset: 0x00148BB3
		public CdpaMetricConfiguration Merge(IEnumerable<CdpaMetricConfiguration> metrics)
		{
			return this.metricMerger.Combine(metrics);
		}

		// Token: 0x06006105 RID: 24837 RVA: 0x0014A9C1 File Offset: 0x00148BC1
		protected override CdpaDataflowGraph NewCrossJoin(CdpaDataflowGraph[] sets)
		{
			return this.Merge(sets);
		}

		// Token: 0x06006106 RID: 24838 RVA: 0x0014A9CA File Offset: 0x00148BCA
		private CdpaDataflowGraph Merge(CdpaDataflowGraph[] sets)
		{
			return CdpaQueryGenerator.Combine(sets, this.metricMerger);
		}

		// Token: 0x06006107 RID: 24839 RVA: 0x0014A9D8 File Offset: 0x00148BD8
		protected override CdpaDataflowGraph NewDescendTo(CdpaDataflowGraph graph, Dimensionality from, Dimensionality to)
		{
			CdpaDataflowGraph cdpaDataflowGraph = graph.ShallowCopy();
			foreach (ICubeHierarchy cubeHierarchy in from.Hierarchies)
			{
				ICubeLevel fine = from.GetLevelRange(cubeHierarchy).Fine;
				ICubeLevel fine2 = to.GetLevelRange(cubeHierarchy).Fine;
				ScopePath scopePath;
				CdpaHierarchyLevel cdpaHierarchyLevel = (CdpaHierarchyLevel)fine.GetUnscoped(out scopePath);
				CdpaHierarchyLevel cdpaHierarchyLevel2 = (CdpaHierarchyLevel)fine2.GetUnscoped(out scopePath);
				CdpaHierarchy cdpaHierarchy = (CdpaHierarchy)cubeHierarchy.GetUnscoped(out scopePath);
				IdentifierCubeExpression identifierCubeExpression;
				ITimeGranularity timeGranularity;
				if (this.unscopedCube.TryGetTimeGranularity(cdpaHierarchyLevel2, out identifierCubeExpression, out timeGranularity))
				{
					cdpaDataflowGraph.Granularity = cdpaDataflowGraph.Granularity.CrossJoin(timeGranularity);
					for (int i = cdpaHierarchyLevel.Number + 1; i < cdpaHierarchyLevel2.Number; i++)
					{
						CdpaHierarchyLevel cdpaHierarchyLevel3 = cdpaHierarchy.Levels[i];
						IdentifierCubeExpression identifierCubeExpression2;
						ITimeGranularity timeGranularity2;
						if (!this.unscopedCube.TryGetTimeGranularity(cdpaHierarchyLevel3, out identifierCubeExpression2, out timeGranularity2) || !identifierCubeExpression2.Equals(identifierCubeExpression))
						{
							throw new NotSupportedException();
						}
						cdpaDataflowGraph.Granularity = cdpaDataflowGraph.Granularity.CrossJoin(timeGranularity2);
					}
				}
				else
				{
					CdpaDataflowNode cdpaDataflowNode = cdpaDataflowGraph.GetNode(scopePath).ShallowCopy();
					cdpaDataflowGraph.Add(scopePath, cdpaDataflowNode);
					cdpaDataflowNode.Metric = cdpaDataflowNode.Metric.ShallowCopy();
					for (int j = cdpaHierarchyLevel.Number; j <= cdpaHierarchyLevel2.Number; j++)
					{
						CdpaHierarchyLevel cdpaHierarchyLevel4 = cdpaHierarchy.Levels[j];
						HashSet<QualifiedName> hashSet = new HashSet<QualifiedName>();
						HashSet<CdpaEvent> currentEvents = new HashSet<CdpaEvent>();
						HashSet<CdpaMetricSplit> currentSplits = new HashSet<CdpaMetricSplit>();
						this.eventsAndSplitsHelper.AddEventsAndSplits(cdpaHierarchyLevel4, hashSet, currentEvents, currentSplits);
						cdpaDataflowNode.Metric.Operands = cdpaDataflowNode.Metric.Operands.Select(delegate(CdpaOperand o)
						{
							if (currentEvents.Overlaps(o.Table.Events))
							{
								o = o.ShallowCopy();
								o.Splits = o.Splits.CrossJoin(currentSplits).ToArray<CdpaMetricSplit>();
							}
							return o;
						}).ToArray<CdpaOperand>();
					}
				}
			}
			return cdpaDataflowGraph;
		}

		// Token: 0x06006108 RID: 24840 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CdpaDataflowGraph NewDistinct(CdpaDataflowGraph graph)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006109 RID: 24841 RVA: 0x0014ABE0 File Offset: 0x00148DE0
		protected override CdpaDataflowGraph NewEverything()
		{
			CdpaDataflowGraph cdpaDataflowGraph = new CdpaDataflowGraph();
			cdpaDataflowGraph.Add(ScopePath.Default, new CdpaDataflowNode
			{
				Metric = new CdpaMetricConfiguration
				{
					Operands = EmptyArray<CdpaOperand>.Instance
				}
			});
			return cdpaDataflowGraph;
		}

		// Token: 0x0600610A RID: 24842 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CdpaDataflowGraph NewExcept(CdpaDataflowGraph graph, CdpaDataflowGraph except)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600610B RID: 24843 RVA: 0x0014AC10 File Offset: 0x00148E10
		protected override CdpaDataflowGraph NewFilter(CdpaDataflowGraph graph, CubeExpression expression)
		{
			IList<CubeExpression> conjunctiveNF = expression.GetConjunctiveNF();
			CdpaDataflowGraph[] array = new CdpaDataflowGraph[conjunctiveNF.Count];
			for (int i = 0; i < array.Length; i++)
			{
				IList<CubeExpression> disjunctiveNF = conjunctiveNF[i].GetDisjunctiveNF();
				CdpaDataflowGraph[] array2 = new CdpaDataflowGraph[disjunctiveNF.Count];
				for (int j = 0; j < array2.Length; j++)
				{
					array2[j] = this.NewFilterPredicate(graph, disjunctiveNF[j]);
				}
				array[i] = this.NewUnion(array2);
			}
			return this.Merge(array);
		}

		// Token: 0x0600610C RID: 24844 RVA: 0x0014AC94 File Offset: 0x00148E94
		private CdpaDataflowGraph NewFilterPredicate(CdpaDataflowGraph graph, CubeExpression predicate)
		{
			DateTime dateTime = this.service.EngineHost.QueryService<ICurrentTimeService>().FixedUtcNow;
			dateTime = dateTime.RoundTo(CdpaSetContextProvider.ResolutionAsGranularity, false);
			CdpaDataflowGraph cdpaDataflowGraph = graph.ShallowCopy();
			CubeExpression cubeExpression = this.attributeInliner.InlineAttributes(predicate);
			IList<ScopePath> list;
			CubeExpression unscoped = cubeExpression.GetUnscoped(out list);
			if (list.Count == 1)
			{
				BinaryOperator2 binaryOperator;
				CubeExpression cubeExpression2;
				ConstantCubeExpression constantCubeExpression;
				if (unscoped.TryGetConstantFilter(out binaryOperator, out cubeExpression2, out constantCubeExpression))
				{
					IdentifierCubeExpression identifierCubeExpression;
					if (cubeExpression2.TryGetIdentifier(out identifierCubeExpression) && this.unscopedCube.IsTimestampAttribute(identifierCubeExpression))
					{
						Value value = constantCubeExpression.Value;
						if (value.Kind == ValueKind.DateTime)
						{
							DateTime dateTime2 = DateTime.SpecifyKind(value.AsDateTime.AsClrDateTime, DateTimeKind.Utc);
							CdpaTimeRange cdpaTimeRange;
							switch (binaryOperator)
							{
							case BinaryOperator2.GreaterThan:
								dateTime2 = dateTime2.AddTicks(1L);
								break;
							case BinaryOperator2.LessThan:
								goto IL_0116;
							case BinaryOperator2.GreaterThanOrEquals:
								break;
							case BinaryOperator2.LessThanOrEquals:
								dateTime2 = dateTime2.AddTicks(1L);
								goto IL_0116;
							case BinaryOperator2.Equals:
								cdpaTimeRange = new TimeIntervalCdpaTimeRange
								{
									UtcStart = new DateTime?(dateTime2),
									UtcEnd = new DateTime?(dateTime2)
								};
								cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange);
								return cdpaDataflowGraph;
							default:
								goto IL_0185;
							}
							cdpaTimeRange = new TimeIntervalCdpaTimeRange
							{
								UtcStart = new DateTime?(dateTime2),
								UtcEnd = new DateTime?(dateTime)
							};
							cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange);
							return cdpaDataflowGraph;
							IL_0116:
							cdpaTimeRange = new TimeIntervalCdpaTimeRange
							{
								UtcStart = new DateTime?(CdpaTimeRange.UtcEarliest),
								UtcEnd = new DateTime?(dateTime2)
							};
							cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange);
							return cdpaDataflowGraph;
						}
					}
					IL_0185:
					FunctionValue functionValue;
					if (this.unscopedCube.TryGetFinestTimePart(cubeExpression2, out functionValue, out identifierCubeExpression) && this.unscopedCube.IsTimestampAttribute(identifierCubeExpression) && constantCubeExpression.Value.Kind == ValueKind.DateTime)
					{
						TimeParts<Value> timeParts = TimePartsFilter.ToTimeParts(new DateTime?(constantCubeExpression.Value.AsDateTime.AsClrDateTime));
						switch (Array.IndexOf<FunctionValue>(CdpaQueryGenerator.partFunctions, functionValue))
						{
						case 0:
							if (!timeParts.Months.Equals(NumberValue.One))
							{
								throw new NotSupportedException();
							}
							timeParts.Months = null;
							break;
						case 1:
							break;
						case 2:
							goto IL_0250;
						case 3:
							goto IL_0271;
						case 4:
							goto IL_0292;
						case 5:
							goto IL_02BB;
						default:
							throw new NotSupportedException();
						}
						if (!timeParts.Days.Equals(NumberValue.One))
						{
							throw new NotSupportedException();
						}
						timeParts.Days = null;
						IL_0250:
						if (!timeParts.Hours.Equals(NumberValue.Zero))
						{
							throw new NotSupportedException();
						}
						timeParts.Hours = null;
						IL_0271:
						if (!timeParts.Minutes.Equals(NumberValue.Zero))
						{
							throw new NotSupportedException();
						}
						timeParts.Minutes = null;
						IL_0292:
						if (!timeParts.Seconds.Equals(NumberValue.Zero))
						{
							throw new NotSupportedException();
						}
						timeParts.Seconds = null;
						IL_02BB:
						CdpaTimeRange cdpaTimeRange2;
						switch (binaryOperator)
						{
						case BinaryOperator2.GreaterThan:
							cdpaTimeRange2 = new TimeIntervalCdpaTimeRange
							{
								UtcStart = TimePartsFilter.ToDateTime(timeParts, false) + TimeSpan.FromTicks(1L)
							};
							break;
						case BinaryOperator2.LessThan:
							cdpaTimeRange2 = new TimeIntervalCdpaTimeRange
							{
								UtcEnd = TimePartsFilter.ToDateTime(timeParts, false)
							};
							break;
						case BinaryOperator2.GreaterThanOrEquals:
							cdpaTimeRange2 = new TimeIntervalCdpaTimeRange
							{
								UtcStart = TimePartsFilter.ToDateTime(timeParts, false)
							};
							break;
						case BinaryOperator2.LessThanOrEquals:
							cdpaTimeRange2 = new TimeIntervalCdpaTimeRange
							{
								UtcEnd = TimePartsFilter.ToDateTime(timeParts, true)
							};
							break;
						case BinaryOperator2.Equals:
							cdpaTimeRange2 = new TimeIntervalCdpaTimeRange
							{
								UtcStart = TimePartsFilter.ToDateTime(timeParts, false),
								UtcEnd = TimePartsFilter.ToDateTime(timeParts, true)
							};
							break;
						default:
							throw new NotSupportedException();
						}
						cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange2);
						return cdpaDataflowGraph;
					}
					Func<Value, TimeParts<Value>> func;
					if (this.unscopedCube.TryGetTimePart(cubeExpression2, out functionValue, out identifierCubeExpression) && this.unscopedCube.IsTimestampAttribute(identifierCubeExpression) && constantCubeExpression.Value.Kind == ValueKind.Number && CdpaQueryGenerator.partToTimePartsCtorMap.TryGetValue(functionValue, out func))
					{
						Value value2 = constantCubeExpression.Value;
						CdpaTimeRange cdpaTimeRange3;
						switch (binaryOperator)
						{
						case BinaryOperator2.GreaterThan:
							value2 = value2.Add(NumberValue.One);
							break;
						case BinaryOperator2.LessThan:
							goto IL_047C;
						case BinaryOperator2.GreaterThanOrEquals:
							break;
						case BinaryOperator2.LessThanOrEquals:
							value2 = value2.Add(NumberValue.One);
							goto IL_047C;
						case BinaryOperator2.Equals:
							cdpaTimeRange3 = new TimePartsFilteredTimeIntervalCdpaTimeRange
							{
								PartsFilter = new TimePartsFilter
								{
									UtcStart = func(value2),
									UtcEnd = func(value2)
								}
							};
							cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange3);
							return cdpaDataflowGraph;
						default:
							goto IL_04F7;
						}
						cdpaTimeRange3 = new TimePartsFilteredTimeIntervalCdpaTimeRange
						{
							PartsFilter = new TimePartsFilter
							{
								UtcStart = func(value2)
							}
						};
						cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange3);
						return cdpaDataflowGraph;
						IL_047C:
						cdpaTimeRange3 = new TimePartsFilteredTimeIntervalCdpaTimeRange
						{
							PartsFilter = new TimePartsFilter
							{
								UtcEnd = func(value2)
							}
						};
						cdpaDataflowGraph.TimeRange = cdpaDataflowGraph.TimeRange.NullableIntersect(cdpaTimeRange3);
						return cdpaDataflowGraph;
					}
				}
				IL_04F7:
				HashSet<QualifiedName> hashSet;
				HashSet<CdpaEvent> hashSet2;
				CdpaPropertyFilterOrGroup cdpaPropertyFilterOrGroup = this.filterTranslator.Translate(unscoped, out hashSet, out hashSet2) as CdpaPropertyFilterOrGroup;
				if (cdpaPropertyFilterOrGroup != null)
				{
					CdpaDataflowGraph cdpaDataflowGraph2 = new CdpaDataflowGraph();
					cdpaDataflowGraph2.Add(list.Single<ScopePath>(), new CdpaDataflowNode
					{
						Metric = new CdpaMetricConfiguration
						{
							Operands = new CdpaOperand[]
							{
								new CdpaOperand
								{
									Id = this.NextId(),
									Table = new CdpaTableConfiguration
									{
										Dimensions = hashSet.ToArray<QualifiedName>(),
										Events = hashSet2.ToArray<CdpaEvent>(),
										Filters = cdpaPropertyFilterOrGroup
									}
								}
							}
						}
					});
					return this.NewCrossJoin(new CdpaDataflowGraph[] { cdpaDataflowGraph, cdpaDataflowGraph2 });
				}
			}
			BinaryCubeExpression binaryCubeExpression = cubeExpression as BinaryCubeExpression;
			if (binaryCubeExpression != null)
			{
				IdentifierCubeExpression identifierCubeExpression2;
				IdentifierCubeExpression identifierCubeExpression3;
				if (binaryCubeExpression.Left.TryGetIdentifier(out identifierCubeExpression2) && binaryCubeExpression.Right.TryGetIdentifier(out identifierCubeExpression3))
				{
					ScopePath scopePath;
					IdentifierCubeExpression unscoped2 = identifierCubeExpression2.GetUnscoped(out scopePath);
					ScopePath scopePath2;
					IdentifierCubeExpression unscoped3 = identifierCubeExpression3.GetUnscoped(out scopePath2);
					ICubeObject cubeObject;
					ICubeObject cubeObject2;
					if (this.unscopedCube.TryGetObject(unscoped2, out cubeObject) && cubeObject is CdpaDimensionAttribute && this.unscopedCube.TryGetObject(unscoped3, out cubeObject2) && cubeObject2 is CdpaDimensionAttribute)
					{
						HashSet<QualifiedName> hashSet3 = new HashSet<QualifiedName>();
						HashSet<CdpaEvent> hashSet4 = new HashSet<CdpaEvent>();
						HashSet<CdpaMetricSplit> hashSet5 = new HashSet<CdpaMetricSplit>();
						this.eventsAndSplitsHelper.AddEventsAndSplits((CdpaDimensionAttribute)cubeObject, hashSet3, hashSet4, hashSet5);
						this.eventsAndSplitsHelper.AddEventsAndSplits((CdpaDimensionAttribute)cubeObject2, hashSet3, hashSet4, hashSet5);
						if (hashSet5.Count == 1)
						{
							if (binaryCubeExpression.Operator == BinaryOperator2.Equals)
							{
								PropertyEqualityCdpaDataflowEdgeConstraint propertyEqualityCdpaDataflowEdgeConstraint = new PropertyEqualityCdpaDataflowEdgeConstraint(hashSet5.Single<CdpaMetricSplit>().PropertyName);
								cdpaDataflowGraph.Add(scopePath, scopePath2, new CdpaDataflowEdgeConstraint[] { propertyEqualityCdpaDataflowEdgeConstraint });
								cdpaDataflowGraph.Add(scopePath2, scopePath, new CdpaDataflowEdgeConstraint[] { propertyEqualityCdpaDataflowEdgeConstraint });
								return cdpaDataflowGraph;
							}
							if (this.unscopedCube.IsTimestampAttribute(unscoped2) && (binaryCubeExpression.Operator == BinaryOperator2.GreaterThan || binaryCubeExpression.Operator == BinaryOperator2.LessThan))
							{
								TimePeriodCdpaDataflowEdgeConstraint unknown = TimePeriodCdpaDataflowEdgeConstraint.Unknown;
								if (binaryCubeExpression.Operator == BinaryOperator2.GreaterThan)
								{
									cdpaDataflowGraph.Add(scopePath2, scopePath, new CdpaDataflowEdgeConstraint[] { unknown });
								}
								else
								{
									cdpaDataflowGraph.Add(scopePath, scopePath2, new CdpaDataflowEdgeConstraint[] { unknown });
								}
								return cdpaDataflowGraph;
							}
						}
					}
				}
				IdentifierCubeExpression identifierCubeExpression4;
				CubeExpression cubeExpression3;
				Value value3;
				IdentifierCubeExpression identifierCubeExpression5;
				if (binaryCubeExpression.Operator == BinaryOperator2.LessThanOrEquals && binaryCubeExpression.Left.TryGetIdentifier(out identifierCubeExpression4) && CdpaQueryGenerator.TryGetDelta(binaryCubeExpression.Right, out cubeExpression3, out value3) && cubeExpression3.TryGetIdentifier(out identifierCubeExpression5))
				{
					ScopePath scopePath3;
					IdentifierCubeExpression unscoped4 = identifierCubeExpression4.GetUnscoped(out scopePath3);
					ScopePath scopePath4;
					IdentifierCubeExpression unscoped5 = identifierCubeExpression5.GetUnscoped(out scopePath4);
					if (this.unscopedCube.IsTimestampAttribute(unscoped4) && this.unscopedCube.IsTimestampAttribute(unscoped5) && value3.IsDuration)
					{
						TimePeriodCdpaDataflowEdgeConstraint timePeriodCdpaDataflowEdgeConstraint = new TimePeriodCdpaDataflowEdgeConstraint(value3.AsDuration.AsClrTimeSpan);
						cdpaDataflowGraph.Add(scopePath4, scopePath3, new CdpaDataflowEdgeConstraint[] { timePeriodCdpaDataflowEdgeConstraint });
						return cdpaDataflowGraph;
					}
				}
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600610D RID: 24845 RVA: 0x0014B47C File Offset: 0x0014967C
		private static bool TryGetDelta(CubeExpression expr, out CubeExpression from, out Value delta)
		{
			BinaryCubeExpression binaryCubeExpression = expr as BinaryCubeExpression;
			if (binaryCubeExpression != null && binaryCubeExpression.Operator == BinaryOperator2.Add)
			{
				if (binaryCubeExpression.Right.TryGetConstant(out delta))
				{
					from = binaryCubeExpression.Left;
					return true;
				}
				if (binaryCubeExpression.Left.TryGetConstant(out delta))
				{
					from = binaryCubeExpression.Right;
					return true;
				}
			}
			from = null;
			delta = null;
			return false;
		}

		// Token: 0x0600610E RID: 24846 RVA: 0x0014B4D2 File Offset: 0x001496D2
		protected override CdpaDataflowGraph NewIntersect(CdpaDataflowGraph graph1, CdpaDataflowGraph graph2)
		{
			return this.NewCrossJoin(new CdpaDataflowGraph[] { graph1, graph2 });
		}

		// Token: 0x0600610F RID: 24847 RVA: 0x0014B4E8 File Offset: 0x001496E8
		protected override CdpaDataflowGraph NewLevel(ICubeLevel level)
		{
			ScopePath scopePath;
			CdpaDimensionAttribute cdpaDimensionAttribute = (CdpaDimensionAttribute)level.GetUnscoped(out scopePath);
			HashSet<QualifiedName> hashSet = new HashSet<QualifiedName>();
			HashSet<CdpaEvent> hashSet2 = new HashSet<CdpaEvent>();
			HashSet<CdpaMetricSplit> hashSet3 = new HashSet<CdpaMetricSplit>();
			this.eventsAndSplitsHelper.AddEventsAndSplits(cdpaDimensionAttribute, hashSet, hashSet2, hashSet3);
			CdpaDataflowGraph cdpaDataflowGraph = new CdpaDataflowGraph();
			CdpaOperand cdpaOperand = new CdpaOperand
			{
				Id = this.NextId(),
				Table = new CdpaTableConfiguration
				{
					Dimensions = hashSet.ToArray<QualifiedName>(),
					Events = hashSet2.ToArray<CdpaEvent>()
				}
			};
			cdpaDataflowGraph.Add(scopePath, new CdpaDataflowNode
			{
				Metric = new CdpaMetricConfiguration
				{
					Operands = new CdpaOperand[] { cdpaOperand }
				}
			});
			IdentifierCubeExpression identifierCubeExpression;
			ITimeGranularity timeGranularity;
			if (this.unscopedCube.TryGetTimeGranularity(cdpaDimensionAttribute, out identifierCubeExpression, out timeGranularity))
			{
				cdpaDataflowGraph.Granularity = timeGranularity;
			}
			else
			{
				cdpaOperand.Splits = hashSet3.ToArray<CdpaMetricSplit>();
			}
			return cdpaDataflowGraph;
		}

		// Token: 0x06006110 RID: 24848 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CdpaDataflowGraph NewMember(ICubeLevel level, Value member)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006111 RID: 24849 RVA: 0x000033E7 File Offset: 0x000015E7
		protected override CdpaDataflowGraph NewOrderBy(CdpaDataflowGraph graph, CubeSortOrder[] order)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06006112 RID: 24850 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected override CdpaDataflowGraph NewOrderHierarchies(CdpaDataflowGraph graph, Dimensionality from, Dimensionality to)
		{
			return graph;
		}

		// Token: 0x06006113 RID: 24851 RVA: 0x0014B5C4 File Offset: 0x001497C4
		protected override CdpaDataflowGraph NewProject(CdpaDataflowGraph graph, IEnumerable<ICubeObject> objects)
		{
			CdpaTenant cdpaTenant = new CdpaTenant
			{
				TargetDocumentId = this.service.Tenant
			};
			CdpaDataflowGraph cdpaDataflowGraph = graph;
			foreach (ICubeObject cubeObject in objects)
			{
				if (cubeObject.Kind == CubeObjectKind.Measure)
				{
					ScopePath scopePath;
					CdpaMeasure cdpaMeasure = (CdpaMeasure)cubeObject.GetUnscoped(out scopePath);
					CdpaMetricMeasure cdpaMetricMeasure = null;
					HashSet<QualifiedName> hashSet = new HashSet<QualifiedName>();
					HashSet<CdpaEvent> hashSet2 = new HashSet<CdpaEvent>();
					OrderByTopCdpaMeasure orderByTopCdpaMeasure = cdpaMeasure as OrderByTopCdpaMeasure;
					if (orderByTopCdpaMeasure != null)
					{
						cdpaMeasure = orderByTopCdpaMeasure.Measure;
					}
					CdpaRowCountMeasure cdpaRowCountMeasure = cdpaMeasure as CdpaRowCountMeasure;
					if (cdpaRowCountMeasure != null)
					{
						foreach (CdpaDimension cdpaDimension in cdpaRowCountMeasure.Dimensions)
						{
							CdpaSignalDimension cdpaSignalDimension = cdpaDimension as CdpaSignalDimension;
							if (cdpaSignalDimension != null)
							{
								hashSet2.Add(new CdpaEvent
								{
									EventName = cdpaSignalDimension.SignalName,
									Tenant = cdpaTenant
								});
							}
							hashSet.Add(cdpaDimension.QualifiedName);
						}
						cdpaMetricMeasure = new EventCountCdpaMetricMeasure
						{
							Operations = new CdpaOperation[]
							{
								new CdpaEventCountOperation()
							}
						};
					}
					CdpaProjectedMeasure cdpaProjectedMeasure = cdpaMeasure as CdpaProjectedMeasure;
					if (cdpaProjectedMeasure != null)
					{
						cdpaMetricMeasure = this.CreateMeasure(cdpaProjectedMeasure, hashSet2, cdpaTenant);
					}
					if (cdpaMetricMeasure != null)
					{
						CdpaDataflowNode cdpaDataflowNode = new CdpaDataflowNode
						{
							Metric = new CdpaMetricConfiguration
							{
								Operands = new CdpaOperand[]
								{
									new CdpaOperand
									{
										Id = this.NextId(),
										Table = new CdpaTableConfiguration
										{
											Dimensions = hashSet.ToArray<QualifiedName>(),
											Events = hashSet2.ToArray<CdpaEvent>()
										},
										Measure = cdpaMetricMeasure
									}
								}
							}
						};
						if (orderByTopCdpaMeasure != null)
						{
							if (cdpaMetricMeasure.Operations.Count != 1)
							{
								throw new NotSupportedException();
							}
							cdpaDataflowNode.Metric.Operands[0].Top = new CdpaTopConfiguration
							{
								Order = (orderByTopCdpaMeasure.Ascending ? "ascending" : "descending"),
								Count = orderByTopCdpaMeasure.TakeCount,
								Operation = cdpaMetricMeasure.Operations.Single<CdpaOperation>()
							};
						}
						CdpaDataflowGraph cdpaDataflowGraph2 = new CdpaDataflowGraph();
						cdpaDataflowGraph2.Add(scopePath, cdpaDataflowNode);
						cdpaDataflowGraph = this.NewCrossJoin(new CdpaDataflowGraph[] { cdpaDataflowGraph, cdpaDataflowGraph2 });
						continue;
					}
				}
				throw new NotSupportedException();
			}
			return cdpaDataflowGraph;
		}

		// Token: 0x06006114 RID: 24852 RVA: 0x0014B854 File Offset: 0x00149A54
		private CdpaMetricMeasure CreateMeasure(CdpaProjectedMeasure projectedMeasure, HashSet<CdpaEvent> events, CdpaTenant tenant)
		{
			InvocationCubeExpression invocationCubeExpression = projectedMeasure.Projection as InvocationCubeExpression;
			Value value;
			IdentifierCubeExpression identifierCubeExpression;
			Func<CdpaMetricPropertyOperation> func;
			ICubeObject cubeObject;
			if (invocationCubeExpression != null && invocationCubeExpression.Function.TryGetConstant(out value) && value.IsFunction && invocationCubeExpression.Arguments.Count == 1 && invocationCubeExpression.Arguments[0].TryGetIdentifier(out identifierCubeExpression) && CdpaQueryGenerator.operationCtors.TryGetValue(value.AsFunction, out func) && this.unscopedCube.TryGetObject(identifierCubeExpression, out cubeObject))
			{
				CdpaDimensionAttribute cdpaDimensionAttribute = (CdpaDimensionAttribute)cubeObject;
				return new MetricPropertyCdpaMetricMeasure
				{
					PropertyName = this.GetPropertyName(cdpaDimensionAttribute, events, tenant),
					Operations = new CdpaMetricPropertyOperation[] { func() }
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006115 RID: 24853 RVA: 0x0014B918 File Offset: 0x00149B18
		private string GetPropertyName(CdpaDimensionAttribute attribute, HashSet<CdpaEvent> events, CdpaTenant tenant)
		{
			HashSet<QualifiedName> hashSet = new HashSet<QualifiedName>();
			HashSet<CdpaMetricSplit> hashSet2 = new HashSet<CdpaMetricSplit>();
			this.eventsAndSplitsHelper.AddEventsAndSplits(attribute, hashSet, events, hashSet2);
			HashSet<string> hashSet3 = new HashSet<string>();
			foreach (CdpaMetricSplit cdpaMetricSplit in hashSet2)
			{
				hashSet3.Add(cdpaMetricSplit.PropertyName);
			}
			if (hashSet3.Count == 1)
			{
				return hashSet3.Single<string>();
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006116 RID: 24854 RVA: 0x0000A6A5 File Offset: 0x000088A5
		protected override CdpaDataflowGraph NewSkipTake(CdpaDataflowGraph graph, RowRange rowRange)
		{
			return graph;
		}

		// Token: 0x06006117 RID: 24855 RVA: 0x0014B9A4 File Offset: 0x00149BA4
		protected override CdpaDataflowGraph NewUnion(CdpaDataflowGraph[] graphs)
		{
			return CdpaQueryGenerator.Combine(graphs, this.metricUnioner);
		}

		// Token: 0x06006118 RID: 24856 RVA: 0x0014B9B4 File Offset: 0x00149BB4
		protected override CdpaDataflowGraph VisitOther(Set set)
		{
			VisibleSlicerSet visibleSlicerSet = set as VisibleSlicerSet;
			if (visibleSlicerSet != null)
			{
				return this.NewSelect(this.Visit(visibleSlicerSet.VisibleAxis), this.Visit(visibleSlicerSet.SlicerAxis));
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006119 RID: 24857 RVA: 0x000020F7 File Offset: 0x000002F7
		protected override CubeExpression VisitFilter(CdpaDataflowGraph graph, CubeExpression filter)
		{
			return filter;
		}

		// Token: 0x0600611A RID: 24858 RVA: 0x000020F7 File Offset: 0x000002F7
		protected override CubeSortOrder VisitOrder(CdpaDataflowGraph graph, CubeSortOrder order)
		{
			return order;
		}

		// Token: 0x0600611B RID: 24859 RVA: 0x0014B9EF File Offset: 0x00149BEF
		private string NextId()
		{
			this.nextId++;
			return this.nextId.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x0600611C RID: 24860 RVA: 0x0014BA10 File Offset: 0x00149C10
		private CdpaDataflowGraph NewSelect(CdpaDataflowGraph visibleAxis, CdpaDataflowGraph slicerAxis)
		{
			Dictionary<ScopePath, HashSet<string>> dictionary = new Dictionary<ScopePath, HashSet<string>>();
			foreach (ScopePath scopePath in visibleAxis.Keys)
			{
				CdpaDataflowNode node = visibleAxis.GetNode(scopePath);
				HashSet<string> hashSet = new HashSet<string>();
				foreach (CdpaOperand cdpaOperand in node.Metric.Operands)
				{
					foreach (CdpaMetricSplit cdpaMetricSplit in cdpaOperand.Splits)
					{
						hashSet.Add(cdpaMetricSplit.PropertyName);
					}
				}
				dictionary.Add(scopePath, hashSet);
			}
			CdpaDataflowGraph cdpaDataflowGraph = slicerAxis.ShallowCopy();
			cdpaDataflowGraph.Granularity = null;
			foreach (ScopePath scopePath2 in slicerAxis.Keys)
			{
				CdpaDataflowNode node2 = cdpaDataflowGraph.GetNode(scopePath2);
				HashSet<string> splitProperties;
				if (!dictionary.TryGetValue(scopePath2, out splitProperties))
				{
					splitProperties = null;
				}
				node2.Metric = node2.Metric.ShallowCopy();
				node2.Metric.Operands = new List<CdpaOperand>(node2.Metric.Operands);
				Func<CdpaMetricSplit, bool> <>9__0;
				Func<CdpaMetricSplit, bool> <>9__1;
				for (int i = 0; i < node2.Metric.Operands.Count; i++)
				{
					CdpaOperand cdpaOperand2 = node2.Metric.Operands[i].ShallowCopy();
					node2.Metric.Operands[i] = cdpaOperand2;
					IEnumerable<CdpaMetricSplit> splits = cdpaOperand2.Splits;
					Func<CdpaMetricSplit, bool> func;
					if ((func = <>9__0) == null)
					{
						func = (<>9__0 = (CdpaMetricSplit s) => (splitProperties == null || !splitProperties.Contains(s.PropertyName)) && s.IsRestricted);
					}
					if (splits.Any(func))
					{
						throw new NotSupportedException();
					}
					CdpaOperand cdpaOperand3 = cdpaOperand2;
					IEnumerable<CdpaMetricSplit> splits2 = cdpaOperand2.Splits;
					Func<CdpaMetricSplit, bool> func2;
					if ((func2 = <>9__1) == null)
					{
						func2 = (<>9__1 = (CdpaMetricSplit s) => splitProperties != null && splitProperties.Contains(s.PropertyName));
					}
					cdpaOperand3.Splits = splits2.Where(func2).ToArray<CdpaMetricSplit>();
					cdpaOperand2.Measure = null;
				}
			}
			return this.Merge(new CdpaDataflowGraph[] { visibleAxis, cdpaDataflowGraph });
		}

		// Token: 0x0600611D RID: 24861 RVA: 0x0014BCB8 File Offset: 0x00149EB8
		private static CdpaDataflowGraph Combine(CdpaDataflowGraph[] sets, CdpaQueryGenerator.MetricCombiner metricCombiner)
		{
			if (sets.Length == 1)
			{
				return sets[0];
			}
			Dictionary<ScopePath, List<CdpaMetricConfiguration>> dictionary = new Dictionary<ScopePath, List<CdpaMetricConfiguration>>();
			foreach (CdpaDataflowGraph cdpaDataflowGraph in sets)
			{
				foreach (ScopePath scopePath in cdpaDataflowGraph.Keys)
				{
					List<CdpaMetricConfiguration> list;
					if (!dictionary.TryGetValue(scopePath, out list))
					{
						list = new List<CdpaMetricConfiguration>();
						dictionary.Add(scopePath, list);
					}
					list.Add(cdpaDataflowGraph.GetNode(scopePath).Metric);
				}
			}
			CdpaDataflowGraph cdpaDataflowGraph2 = new CdpaDataflowGraph();
			foreach (KeyValuePair<ScopePath, List<CdpaMetricConfiguration>> keyValuePair in dictionary)
			{
				cdpaDataflowGraph2.Add(keyValuePair.Key, new CdpaDataflowNode
				{
					Metric = metricCombiner.Combine(keyValuePair.Value)
				});
			}
			foreach (CdpaDataflowGraph cdpaDataflowGraph3 in sets)
			{
				foreach (ScopePath scopePath2 in cdpaDataflowGraph3.Keys)
				{
					foreach (ScopePath scopePath3 in cdpaDataflowGraph3.KeysFrom(scopePath2))
					{
						cdpaDataflowGraph2.Add(scopePath2, scopePath3, cdpaDataflowGraph3.GetEdge(scopePath2, scopePath3));
					}
				}
				cdpaDataflowGraph2.Granularity = cdpaDataflowGraph2.Granularity.CrossJoin(cdpaDataflowGraph3.Granularity);
				cdpaDataflowGraph2.TimeRange = cdpaDataflowGraph2.TimeRange.NullableIntersect(cdpaDataflowGraph3.TimeRange);
			}
			return cdpaDataflowGraph2;
		}

		// Token: 0x0600611E RID: 24862 RVA: 0x0014BE9C File Offset: 0x0014A09C
		private static bool MeasuresAreCompatible(CdpaMetricMeasure measure1, CdpaMetricMeasure measure2)
		{
			if (measure1 == null || measure2 == null)
			{
				return true;
			}
			if (measure1 is EventCountCdpaMetricMeasure && measure2 is EventCountCdpaMetricMeasure)
			{
				return true;
			}
			MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure = measure1 as MetricPropertyCdpaMetricMeasure;
			MetricPropertyCdpaMetricMeasure metricPropertyCdpaMetricMeasure2 = measure2 as MetricPropertyCdpaMetricMeasure;
			if (metricPropertyCdpaMetricMeasure != null && metricPropertyCdpaMetricMeasure2 != null && metricPropertyCdpaMetricMeasure.PropertyName == metricPropertyCdpaMetricMeasure2.PropertyName)
			{
				bool flag = metricPropertyCdpaMetricMeasure.Operations.Any((CdpaOperation o) => o.Name == "distinctCount");
				bool flag2 = metricPropertyCdpaMetricMeasure2.Operations.Any((CdpaOperation o) => o.Name == "distinctCount");
				return flag == flag2;
			}
			return false;
		}

		// Token: 0x0600611F RID: 24863 RVA: 0x0014BF44 File Offset: 0x0014A144
		private static string GetCdpaType(TypeValue mType)
		{
			switch (mType.TypeKind)
			{
			case ValueKind.DateTime:
				return "date-time";
			case ValueKind.Number:
				return "number";
			case ValueKind.Logical:
				return "boolean";
			case ValueKind.Text:
				return "string";
			}
			throw new NotSupportedException();
		}

		// Token: 0x06006120 RID: 24864 RVA: 0x0014BF98 File Offset: 0x0014A198
		private static IList<TItem> Concat<TItem>(IList<TItem> list1, IList<TItem> list2)
		{
			TItem[] array = new TItem[list1.Count + list2.Count];
			for (int i = 0; i < list1.Count; i++)
			{
				array[i] = list1[i];
			}
			for (int j = 0; j < list2.Count; j++)
			{
				array[list1.Count + j] = list2[j];
			}
			return array;
		}

		// Token: 0x06006121 RID: 24865 RVA: 0x0014BFFE File Offset: 0x0014A1FE
		private static CdpaPropertyFilterOrGroup Intersect(CdpaPropertyFilterOrGroup group1, CdpaPropertyFilterOrGroup group2)
		{
			if (group1 == null)
			{
				return group2;
			}
			if (group2 == null)
			{
				return group1;
			}
			return group1.And(group2);
		}

		// Token: 0x040034EA RID: 13546
		public static readonly Func<CdpaEventCountOperation> eventCountOperationCtor = () => new CdpaEventCountOperation();

		// Token: 0x040034EB RID: 13547
		public static readonly Dictionary<FunctionValue, Func<CdpaMetricPropertyOperation>> operationCtors = new Dictionary<FunctionValue, Func<CdpaMetricPropertyOperation>>
		{
			{
				LanguageLibrary.List.Count,
				() => new CountCdpaMetricPropertyOperation()
			},
			{
				Library.List.CountOfDistinct,
				() => new DistinctCountCdpaMetricPropertyOperation()
			},
			{
				Library.List.Min,
				() => new MinCdpaMetricPropertyOperation()
			},
			{
				Library.List.Max,
				() => new MaxCdpaMetricPropertyOperation()
			},
			{
				Library.List.Sum,
				() => new SumCdpaMetricPropertyOperation()
			},
			{
				Library.List.Average,
				() => new AverageCdpaMetricPropertyOperation()
			},
			{
				Library.List.StandardDeviation,
				() => new StandardDeviationCdpaMetricPropertyOperation()
			}
		};

		// Token: 0x040034EC RID: 13548
		public static readonly FunctionValue[] partFunctions = new FunctionValue[]
		{
			Library.Date.Year,
			Library.Date.Month,
			Library.Date.Day,
			Library.Time.Hour,
			Library.Time.Minute,
			Library.Time.Second
		};

		// Token: 0x040034ED RID: 13549
		private static readonly Dictionary<FunctionValue, Func<Value, TimeParts<Value>>> partToTimePartsCtorMap = new Dictionary<FunctionValue, Func<Value, TimeParts<Value>>>
		{
			{
				Library.Date.Year,
				(Value value) => new TimeParts<Value>
				{
					Years = value
				}
			},
			{
				Library.Date.Month,
				(Value value) => new TimeParts<Value>
				{
					Months = value
				}
			},
			{
				Library.Date.Day,
				(Value value) => new TimeParts<Value>
				{
					Days = value
				}
			},
			{
				Library.Time.Hour,
				(Value value) => new TimeParts<Value>
				{
					Hours = value
				}
			},
			{
				Library.Time.Minute,
				(Value value) => new TimeParts<Value>
				{
					Minutes = value
				}
			},
			{
				Library.Time.Second,
				(Value value) => new TimeParts<Value>
				{
					Seconds = value
				}
			}
		};

		// Token: 0x040034EE RID: 13550
		private readonly CdpaService service;

		// Token: 0x040034EF RID: 13551
		private readonly ICube cube;

		// Token: 0x040034F0 RID: 13552
		private readonly CdpaCube unscopedCube;

		// Token: 0x040034F1 RID: 13553
		private readonly EventsAndSplitsHelper eventsAndSplitsHelper;

		// Token: 0x040034F2 RID: 13554
		private readonly InlineAttributesVisitor attributeInliner;

		// Token: 0x040034F3 RID: 13555
		private readonly CdpaQueryGenerator.FilterTranslator filterTranslator;

		// Token: 0x040034F4 RID: 13556
		private readonly CdpaQueryGenerator.MetricMerger metricMerger;

		// Token: 0x040034F5 RID: 13557
		private readonly CdpaQueryGenerator.MetricUnioner metricUnioner;

		// Token: 0x040034F6 RID: 13558
		private int nextId;

		// Token: 0x02000E17 RID: 3607
		public static class PartIndex
		{
			// Token: 0x040034F7 RID: 13559
			public const int Year = 0;

			// Token: 0x040034F8 RID: 13560
			public const int Month = 1;

			// Token: 0x040034F9 RID: 13561
			public const int Day = 2;

			// Token: 0x040034FA RID: 13562
			public const int Hour = 3;

			// Token: 0x040034FB RID: 13563
			public const int Minute = 4;

			// Token: 0x040034FC RID: 13564
			public const int Second = 5;
		}

		// Token: 0x02000E18 RID: 3608
		private class EventSet : IEquatable<CdpaQueryGenerator.EventSet>, IIntersectable<CdpaQueryGenerator.EventSet>, IUnionable<CdpaQueryGenerator.EventSet>
		{
			// Token: 0x06006123 RID: 24867 RVA: 0x0014C1E4 File Offset: 0x0014A3E4
			public EventSet(IEnumerable<CdpaEvent> events)
			{
				this.events = new HashSet<CdpaEvent>(events);
			}

			// Token: 0x06006124 RID: 24868 RVA: 0x0014C1F8 File Offset: 0x0014A3F8
			public bool IsSubsetOf(CdpaQueryGenerator.EventSet other)
			{
				return this.events.IsSubsetOf(other.events);
			}

			// Token: 0x06006125 RID: 24869 RVA: 0x0014C20B File Offset: 0x0014A40B
			public CdpaQueryGenerator.EventSet Intersect(CdpaQueryGenerator.EventSet other)
			{
				HashSet<CdpaEvent> hashSet = new HashSet<CdpaEvent>(this.events);
				hashSet.IntersectWith(other.events);
				return new CdpaQueryGenerator.EventSet(hashSet);
			}

			// Token: 0x06006126 RID: 24870 RVA: 0x0014C229 File Offset: 0x0014A429
			public CdpaQueryGenerator.EventSet Union(CdpaQueryGenerator.EventSet other)
			{
				HashSet<CdpaEvent> hashSet = new HashSet<CdpaEvent>(this.events);
				hashSet.UnionWith(other.events);
				return new CdpaQueryGenerator.EventSet(hashSet);
			}

			// Token: 0x06006127 RID: 24871 RVA: 0x0014C247 File Offset: 0x0014A447
			public override int GetHashCode()
			{
				return this.events.NullableSetGetHashCode<CdpaEvent>();
			}

			// Token: 0x06006128 RID: 24872 RVA: 0x0014C254 File Offset: 0x0014A454
			public override bool Equals(object other)
			{
				return this.Equals(other as CdpaQueryGenerator.EventSet);
			}

			// Token: 0x06006129 RID: 24873 RVA: 0x0014C262 File Offset: 0x0014A462
			public bool Equals(CdpaQueryGenerator.EventSet other)
			{
				return other != null && this.events.NullableSetEquals(other.events);
			}

			// Token: 0x040034FD RID: 13565
			private readonly HashSet<CdpaEvent> events;
		}

		// Token: 0x02000E19 RID: 3609
		private class FilterTranslator : CubeExpressionVisitor<object, object>
		{
			// Token: 0x0600612A RID: 24874 RVA: 0x0014C27A File Offset: 0x0014A47A
			public FilterTranslator(CdpaQueryGenerator queryGenerator)
			{
				this.queryGenerator = queryGenerator;
				this.functionHandlers = new Dictionary<FunctionValue, Func<object, object[], object>> { 
				{
					Library.Text.Contains,
					new Func<object, object[], object>(this.NewTextContains)
				} };
			}

			// Token: 0x0600612B RID: 24875 RVA: 0x0014C2AC File Offset: 0x0014A4AC
			public object Translate(CubeExpression expression, out HashSet<QualifiedName> referencedDimensions, out HashSet<CdpaEvent> referencedEvents)
			{
				HashSet<QualifiedName> hashSet = this.referencedDimensions;
				HashSet<CdpaEvent> hashSet2 = this.referencedEvents;
				this.referencedDimensions = new HashSet<QualifiedName>();
				this.referencedEvents = new HashSet<CdpaEvent>();
				object obj;
				try
				{
					referencedDimensions = this.referencedDimensions;
					referencedEvents = this.referencedEvents;
					obj = CdpaQueryGenerator.FilterTranslator.ToOrGroup(this.Visit(expression));
				}
				finally
				{
					this.referencedDimensions = hashSet;
					this.referencedEvents = hashSet2;
				}
				return obj;
			}

			// Token: 0x0600612C RID: 24876 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override object NewSortOrder(object expression, bool ascending)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600612D RID: 24877 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override object NewIf(object condition, object trueCase, object falseCase)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600612E RID: 24878 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override object NewQuery(object from, IList<IdentifierCubeExpression> dimensionAttributes, IList<IdentifierCubeExpression> properties, IList<IdentifierCubeExpression> measures, IList<IdentifierCubeExpression> measureProperties, object filter, object[] sortOrders, RowRange rowRange)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600612F RID: 24879 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override object NewConstant(Value constant)
			{
				return constant;
			}

			// Token: 0x06006130 RID: 24880 RVA: 0x0014C31C File Offset: 0x0014A51C
			protected override object NewIdentifier(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				if (this.queryGenerator.unscopedCube.TryGetObject(identifier, out cubeObject) && cubeObject is CdpaDimensionAttribute)
				{
					return cubeObject;
				}
				throw new NotSupportedException();
			}

			// Token: 0x06006131 RID: 24881 RVA: 0x0014C350 File Offset: 0x0014A550
			protected override object NewInvocation(object function, object[] arguments)
			{
				Value value = function as Value;
				Func<object, object[], object> func;
				if (value != null && value.IsFunction && this.functionHandlers.TryGetValue(value.AsFunction, out func))
				{
					return func(function, arguments);
				}
				throw new NotSupportedException();
			}

			// Token: 0x06006132 RID: 24882 RVA: 0x0014C394 File Offset: 0x0014A594
			protected object NewTextContains(object function, object[] arguments)
			{
				if (arguments.Length == 2)
				{
					CdpaDimensionAttribute cdpaDimensionAttribute = arguments[0] as CdpaDimensionAttribute;
					Value value = arguments[1] as Value;
					if (cdpaDimensionAttribute != null && value != null && value.IsText && cdpaDimensionAttribute.Type.TypeKind == value.Kind)
					{
						HashSet<CdpaMetricSplit> hashSet = new HashSet<CdpaMetricSplit>();
						this.queryGenerator.eventsAndSplitsHelper.AddEventsAndSplits(cdpaDimensionAttribute, this.referencedDimensions, this.referencedEvents, hashSet);
						object obj = null;
						foreach (CdpaMetricSplit cdpaMetricSplit in hashSet)
						{
							ContainsStringCdpaPropertyFilter containsStringCdpaPropertyFilter = new ContainsStringCdpaPropertyFilter
							{
								PropertyName = cdpaMetricSplit.PropertyName,
								Value = new CdpaValue
								{
									Type = "string",
									Value = value.AsString
								}
							};
							obj = ((obj != null) ? CdpaQueryGenerator.FilterTranslator.ToOrGroup(obj).Or(CdpaQueryGenerator.FilterTranslator.ToOrGroup(containsStringCdpaPropertyFilter)) : CdpaQueryGenerator.FilterTranslator.ToOrGroup(containsStringCdpaPropertyFilter));
						}
						if (obj != null)
						{
							return obj;
						}
					}
				}
				throw new NotSupportedException();
			}

			// Token: 0x06006133 RID: 24883 RVA: 0x0014C4AC File Offset: 0x0014A6AC
			protected override object NewBinary(BinaryOperator2 op, object left, object right)
			{
				if (CdpaQueryGenerator.FilterTranslator.IsComparisonOperator(op))
				{
					CdpaDimensionAttribute cdpaDimensionAttribute = left as CdpaDimensionAttribute;
					Value value = right as Value;
					if (cdpaDimensionAttribute != null && value != null)
					{
						return this.NewComparison(op, cdpaDimensionAttribute, value);
					}
					CdpaDimensionAttribute cdpaDimensionAttribute2 = right as CdpaDimensionAttribute;
					Value value2 = left as Value;
					BinaryOperator2 binaryOperator;
					if (cdpaDimensionAttribute2 != null && value2 != null && op.TrySwapOperands(out binaryOperator))
					{
						return this.NewComparison(binaryOperator, cdpaDimensionAttribute2, value2);
					}
				}
				switch (op)
				{
				case BinaryOperator2.Equals:
					if (right.Equals(LogicalValue.False))
					{
						return CdpaQueryGenerator.FilterTranslator.ToOrGroup(left).Not();
					}
					if (left.Equals(LogicalValue.False))
					{
						return CdpaQueryGenerator.FilterTranslator.ToOrGroup(right).Not();
					}
					break;
				case BinaryOperator2.And:
					return CdpaQueryGenerator.FilterTranslator.ToOrGroup(left).And(CdpaQueryGenerator.FilterTranslator.ToOrGroup(right));
				case BinaryOperator2.Or:
					return CdpaQueryGenerator.FilterTranslator.ToOrGroup(left).Or(CdpaQueryGenerator.FilterTranslator.ToOrGroup(right));
				}
				throw new NotSupportedException();
			}

			// Token: 0x06006134 RID: 24884 RVA: 0x0014C580 File Offset: 0x0014A780
			private object NewComparison(BinaryOperator2 op, CdpaDimensionAttribute attribute, Value value)
			{
				HashSet<CdpaMetricSplit> hashSet = new HashSet<CdpaMetricSplit>();
				this.queryGenerator.eventsAndSplitsHelper.AddEventsAndSplits(attribute, this.referencedDimensions, this.referencedEvents, hashSet);
				object obj = null;
				foreach (CdpaMetricSplit cdpaMetricSplit in hashSet)
				{
					if (attribute.Type.TypeKind == value.Kind)
					{
						switch (attribute.Type.TypeKind)
						{
						case ValueKind.Date:
						case ValueKind.DateTime:
						case ValueKind.Number:
						{
							ValueComparisonCdpaPropertyFilter valueComparisonCdpaPropertyFilter;
							switch (op)
							{
							case BinaryOperator2.GreaterThan:
								valueComparisonCdpaPropertyFilter = new IsGreaterThanValueComparisonCdpaPropertyFilter();
								break;
							case BinaryOperator2.LessThan:
								valueComparisonCdpaPropertyFilter = new IsLessThanValueComparisonCdpaPropertyFilter();
								break;
							case BinaryOperator2.GreaterThanOrEquals:
								valueComparisonCdpaPropertyFilter = new IsGreaterOrEqualValueComparisonCdpaPropertyFilter();
								break;
							case BinaryOperator2.LessThanOrEquals:
								valueComparisonCdpaPropertyFilter = new IsLessOrEqualValueComparisonCdpaPropertyFilter();
								break;
							case BinaryOperator2.Equals:
								valueComparisonCdpaPropertyFilter = new EqualsValueComparisonCdpaPropertyFilter();
								break;
							case BinaryOperator2.NotEquals:
								valueComparisonCdpaPropertyFilter = new NotEqualsValueComparisonCdpaPropertyFilter();
								break;
							default:
								throw new NotSupportedException();
							}
							TypeValue typeValue = value.Type;
							if (typeValue.TypeKind == ValueKind.Number)
							{
								typeValue = attribute.Type;
							}
							valueComparisonCdpaPropertyFilter.PropertyName = cdpaMetricSplit.PropertyName;
							valueComparisonCdpaPropertyFilter.Value = new CdpaValue
							{
								Type = CdpaQueryGenerator.GetCdpaType(typeValue),
								Value = ValueMarshaller.MarshalToClr(value)
							};
							obj = ((obj != null) ? CdpaQueryGenerator.FilterTranslator.ToOrGroup(obj).Or(CdpaQueryGenerator.FilterTranslator.ToOrGroup(valueComparisonCdpaPropertyFilter)) : CdpaQueryGenerator.FilterTranslator.ToOrGroup(valueComparisonCdpaPropertyFilter));
							continue;
						}
						case ValueKind.Text:
						{
							StringComparisonCdpaPropertyFilter stringComparisonCdpaPropertyFilter;
							if (op != BinaryOperator2.Equals)
							{
								if (op != BinaryOperator2.NotEquals)
								{
									throw new NotSupportedException();
								}
								stringComparisonCdpaPropertyFilter = new NotEqualsStringComparisonCdpaPropertyFilter();
							}
							else
							{
								stringComparisonCdpaPropertyFilter = new EqualsStringComparisonCdpaPropertyFilter();
							}
							stringComparisonCdpaPropertyFilter.PropertyName = cdpaMetricSplit.PropertyName;
							stringComparisonCdpaPropertyFilter.Value = new CdpaValue
							{
								Type = CdpaQueryGenerator.GetCdpaType(value.Type),
								Value = value.AsString
							};
							obj = ((obj != null) ? CdpaQueryGenerator.FilterTranslator.ToOrGroup(obj).Or(CdpaQueryGenerator.FilterTranslator.ToOrGroup(stringComparisonCdpaPropertyFilter)) : CdpaQueryGenerator.FilterTranslator.ToOrGroup(stringComparisonCdpaPropertyFilter));
							continue;
						}
						}
					}
					if (value.Kind != ValueKind.Null)
					{
						obj = null;
						break;
					}
					UnaryCdpaPropertyFilter unaryCdpaPropertyFilter;
					if (op != BinaryOperator2.Equals)
					{
						if (op != BinaryOperator2.NotEquals)
						{
							throw new NotSupportedException();
						}
						unaryCdpaPropertyFilter = new ExistsUnaryCdpaPropertyFilter();
					}
					else
					{
						unaryCdpaPropertyFilter = new NotExistsUnaryCdpaPropertyFilter();
					}
					unaryCdpaPropertyFilter.PropertyName = cdpaMetricSplit.PropertyName;
					obj = ((obj != null) ? CdpaQueryGenerator.FilterTranslator.ToOrGroup(obj).Or(CdpaQueryGenerator.FilterTranslator.ToOrGroup(unaryCdpaPropertyFilter)) : CdpaQueryGenerator.FilterTranslator.ToOrGroup(unaryCdpaPropertyFilter));
				}
				if (obj == null)
				{
					throw new NotSupportedException();
				}
				return obj;
			}

			// Token: 0x06006135 RID: 24885 RVA: 0x0014C800 File Offset: 0x0014AA00
			private static CdpaPropertyFilterOrGroup ToOrGroup(object obj)
			{
				CdpaPropertyFilterOrGroup cdpaPropertyFilterOrGroup = obj as CdpaPropertyFilterOrGroup;
				if (cdpaPropertyFilterOrGroup == null)
				{
					cdpaPropertyFilterOrGroup = CdpaQueryGenerator.FilterTranslator.ToAndGroup(obj).ToOrGroup();
				}
				return cdpaPropertyFilterOrGroup;
			}

			// Token: 0x06006136 RID: 24886 RVA: 0x0014C824 File Offset: 0x0014AA24
			private static CdpaPropertyFilterAndGroup ToAndGroup(object obj)
			{
				CdpaPropertyFilterAndGroup cdpaPropertyFilterAndGroup = obj as CdpaPropertyFilterAndGroup;
				if (cdpaPropertyFilterAndGroup == null)
				{
					cdpaPropertyFilterAndGroup = CdpaQueryGenerator.FilterTranslator.ToFilter(obj).ToAndGroup();
				}
				return cdpaPropertyFilterAndGroup;
			}

			// Token: 0x06006137 RID: 24887 RVA: 0x0014C848 File Offset: 0x0014AA48
			private static CdpaPropertyFilter ToFilter(object obj)
			{
				CdpaPropertyFilter cdpaPropertyFilter = obj as CdpaPropertyFilter;
				if (cdpaPropertyFilter != null)
				{
					return cdpaPropertyFilter;
				}
				throw new NotSupportedException();
			}

			// Token: 0x06006138 RID: 24888 RVA: 0x0014C866 File Offset: 0x0014AA66
			private static bool IsComparisonOperator(BinaryOperator2 op)
			{
				return op - BinaryOperator2.GreaterThan <= 5;
			}

			// Token: 0x040034FE RID: 13566
			private readonly CdpaQueryGenerator queryGenerator;

			// Token: 0x040034FF RID: 13567
			private readonly Dictionary<FunctionValue, Func<object, object[], object>> functionHandlers;

			// Token: 0x04003500 RID: 13568
			private HashSet<QualifiedName> referencedDimensions;

			// Token: 0x04003501 RID: 13569
			private HashSet<CdpaEvent> referencedEvents;
		}

		// Token: 0x02000E1A RID: 3610
		private abstract class MetricCombiner
		{
			// Token: 0x06006139 RID: 24889 RVA: 0x0014C871 File Offset: 0x0014AA71
			protected MetricCombiner(CdpaQueryGenerator generator)
			{
				this.generator = generator;
			}

			// Token: 0x0600613A RID: 24890
			protected abstract IList<QualifiedName> Combine(IList<QualifiedName> dimensions1, IList<QualifiedName> dimensions2);

			// Token: 0x0600613B RID: 24891
			protected abstract CdpaQueryGenerator.EventSet Combine(CdpaQueryGenerator.EventSet es1, CdpaQueryGenerator.EventSet es2);

			// Token: 0x0600613C RID: 24892
			protected abstract IList<CdpaEvent> Combine(IList<CdpaEvent> events1, IList<CdpaEvent> events2);

			// Token: 0x0600613D RID: 24893
			protected abstract CdpaPropertyFilterOrGroup Combine(CdpaPropertyFilterOrGroup filters1, CdpaPropertyFilterOrGroup filters2);

			// Token: 0x0600613E RID: 24894
			protected abstract IList<CdpaMetricSplit> Combine(IList<CdpaMetricSplit> splits1, IList<CdpaMetricSplit> splits2);

			// Token: 0x0600613F RID: 24895
			protected abstract CdpaTopConfiguration Combine(CdpaTopConfiguration top1, CdpaTopConfiguration top2);

			// Token: 0x06006140 RID: 24896 RVA: 0x0014C880 File Offset: 0x0014AA80
			public CdpaMetricConfiguration Combine(IEnumerable<CdpaMetricConfiguration> metrics)
			{
				List<KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand>> list = new List<KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand>>();
				foreach (CdpaMetricConfiguration cdpaMetricConfiguration in metrics)
				{
					foreach (CdpaOperand cdpaOperand in cdpaMetricConfiguration.Operands)
					{
						list.Add(new KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand>(new CdpaQueryGenerator.EventSet(cdpaOperand.Table.Events), cdpaOperand));
						bool flag;
						do
						{
							flag = false;
							for (int i = 0; i < list.Count; i++)
							{
								CdpaQueryGenerator.EventSet eventSet = list[i].Key;
								CdpaOperand cdpaOperand2 = list[i].Value;
								for (int j = list.Count - 1; j > i; j--)
								{
									CdpaQueryGenerator.EventSet eventSet2 = list[j].Key;
									CdpaOperand value = list[j].Value;
									cdpaOperand2 = new CdpaOperand
									{
										Id = this.generator.NextId(),
										Table = new CdpaTableConfiguration
										{
											Dimensions = this.Combine(cdpaOperand2.Table.Dimensions, value.Table.Dimensions),
											Events = cdpaOperand2.Table.Events,
											Filters = this.Combine(cdpaOperand2.Table.Filters, value.Table.Filters)
										},
										Measure = cdpaOperand2.Measure,
										Splits = this.Combine(cdpaOperand2.Splits, value.Splits),
										Top = cdpaOperand2.Top
									};
									CdpaOperand cdpaOperand3 = new CdpaOperand
									{
										Id = this.generator.NextId(),
										Table = new CdpaTableConfiguration
										{
											Dimensions = value.Table.Dimensions,
											Events = value.Table.Events,
											Filters = cdpaOperand2.Table.Filters
										},
										Measure = value.Measure,
										Splits = cdpaOperand2.Splits,
										Top = value.Top
									};
									if (eventSet.IsSubsetOf(eventSet2) || eventSet2.IsSubsetOf(eventSet))
									{
										eventSet = this.Combine(eventSet, eventSet2);
										eventSet2 = eventSet;
										cdpaOperand2.Table.Events = this.Combine(cdpaOperand2.Table.Events, value.Table.Events);
										cdpaOperand3.Table.Events = cdpaOperand2.Table.Events;
										if (CdpaQueryGenerator.MeasuresAreCompatible(cdpaOperand2.Measure, value.Measure))
										{
											cdpaOperand2.Measure = cdpaOperand2.Measure.NullableUnion(cdpaOperand3.Measure);
											cdpaOperand2.Top = this.Combine(cdpaOperand2.Top, cdpaOperand3.Top);
											eventSet2 = null;
											cdpaOperand3 = null;
											list.RemoveAt(j);
											flag = true;
										}
									}
									if (eventSet2 != null)
									{
										list[j] = new KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand>(eventSet2, cdpaOperand3);
									}
								}
								list[i] = new KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand>(eventSet, cdpaOperand2);
							}
						}
						while (flag);
					}
				}
				CdpaMetricConfiguration cdpaMetricConfiguration2 = new CdpaMetricConfiguration();
				cdpaMetricConfiguration2.Operands = list.Select((KeyValuePair<CdpaQueryGenerator.EventSet, CdpaOperand> kvp) => kvp.Value).ToArray<CdpaOperand>();
				return cdpaMetricConfiguration2;
			}

			// Token: 0x04003502 RID: 13570
			private readonly CdpaQueryGenerator generator;
		}

		// Token: 0x02000E1C RID: 3612
		private class MetricMerger : CdpaQueryGenerator.MetricCombiner
		{
			// Token: 0x06006144 RID: 24900 RVA: 0x0014CC31 File Offset: 0x0014AE31
			public MetricMerger(CdpaQueryGenerator generator)
				: base(generator)
			{
			}

			// Token: 0x06006145 RID: 24901 RVA: 0x0014CC3A File Offset: 0x0014AE3A
			protected override IList<QualifiedName> Combine(IList<QualifiedName> dimensions1, IList<QualifiedName> dimensions2)
			{
				return dimensions1.Intersect(dimensions2).ToArray<QualifiedName>();
			}

			// Token: 0x06006146 RID: 24902 RVA: 0x0014CC48 File Offset: 0x0014AE48
			protected override CdpaQueryGenerator.EventSet Combine(CdpaQueryGenerator.EventSet es1, CdpaQueryGenerator.EventSet es2)
			{
				return es1.Intersect(es2);
			}

			// Token: 0x06006147 RID: 24903 RVA: 0x0014CC51 File Offset: 0x0014AE51
			protected override IList<CdpaEvent> Combine(IList<CdpaEvent> events1, IList<CdpaEvent> events2)
			{
				return events1.Intersect(events2).ToArray<CdpaEvent>();
			}

			// Token: 0x06006148 RID: 24904 RVA: 0x0014CC5F File Offset: 0x0014AE5F
			protected override CdpaPropertyFilterOrGroup Combine(CdpaPropertyFilterOrGroup filters1, CdpaPropertyFilterOrGroup filters2)
			{
				return filters1.NullableIntersect(filters2);
			}

			// Token: 0x06006149 RID: 24905 RVA: 0x0014CC68 File Offset: 0x0014AE68
			protected override IList<CdpaMetricSplit> Combine(IList<CdpaMetricSplit> splits1, IList<CdpaMetricSplit> splits2)
			{
				return splits1.CrossJoin(splits2).ToArray<CdpaMetricSplit>();
			}

			// Token: 0x0600614A RID: 24906 RVA: 0x0014CC76 File Offset: 0x0014AE76
			protected override CdpaTopConfiguration Combine(CdpaTopConfiguration top1, CdpaTopConfiguration top2)
			{
				return top1.NullableIntersect(top2);
			}
		}

		// Token: 0x02000E1D RID: 3613
		private class MetricUnioner : CdpaQueryGenerator.MetricCombiner
		{
			// Token: 0x0600614B RID: 24907 RVA: 0x0014CC31 File Offset: 0x0014AE31
			public MetricUnioner(CdpaQueryGenerator generator)
				: base(generator)
			{
			}

			// Token: 0x0600614C RID: 24908 RVA: 0x0014CC7F File Offset: 0x0014AE7F
			protected override IList<QualifiedName> Combine(IList<QualifiedName> dimensions1, IList<QualifiedName> dimensions2)
			{
				return dimensions1.Union(dimensions2).ToArray<QualifiedName>();
			}

			// Token: 0x0600614D RID: 24909 RVA: 0x0014CC8D File Offset: 0x0014AE8D
			protected override CdpaQueryGenerator.EventSet Combine(CdpaQueryGenerator.EventSet es1, CdpaQueryGenerator.EventSet es2)
			{
				return es1.Union(es2);
			}

			// Token: 0x0600614E RID: 24910 RVA: 0x0014CC96 File Offset: 0x0014AE96
			protected override IList<CdpaEvent> Combine(IList<CdpaEvent> events1, IList<CdpaEvent> events2)
			{
				return events1.Union(events2).ToArray<CdpaEvent>();
			}

			// Token: 0x0600614F RID: 24911 RVA: 0x0014CCA4 File Offset: 0x0014AEA4
			protected override CdpaPropertyFilterOrGroup Combine(CdpaPropertyFilterOrGroup filters1, CdpaPropertyFilterOrGroup filters2)
			{
				return filters1.NullableUnion(filters2);
			}

			// Token: 0x06006150 RID: 24912 RVA: 0x0014CCAD File Offset: 0x0014AEAD
			protected override IList<CdpaMetricSplit> Combine(IList<CdpaMetricSplit> splits1, IList<CdpaMetricSplit> splits2)
			{
				return splits1.Union(splits2).ToArray<CdpaMetricSplit>();
			}

			// Token: 0x06006151 RID: 24913 RVA: 0x0014CCBB File Offset: 0x0014AEBB
			protected override CdpaTopConfiguration Combine(CdpaTopConfiguration top1, CdpaTopConfiguration top2)
			{
				return top1.NullableUnion(top2);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.QueryBindingDescriptor;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000012 RID: 18
	internal static class DataShapeLimitsBuilder
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00004B9C File Offset: 0x00002D9C
		internal static DataShapeLimits CreateLimitsDescriptor(IntermediateDataReduction dsqReduction)
		{
			if (dsqReduction == null)
			{
				return null;
			}
			DataShapeLimitDescriptor dataShapeLimitDescriptor = DataShapeLimitsBuilder.CreateLimitDescriptor(dsqReduction.Primary, false);
			DataShapeLimitDescriptor dataShapeLimitDescriptor2 = DataShapeLimitsBuilder.CreateLimitDescriptor(dsqReduction.Secondary, false);
			DataShapeLimitDescriptor dataShapeLimitDescriptor3 = DataShapeLimitsBuilder.CreateLimitDescriptor(dsqReduction.Intersection, false);
			List<DataShapeLimitDescriptor> list = DataShapeLimitsBuilder.CreateScopedLimitDescriptors(dsqReduction.Scoped);
			if (dataShapeLimitDescriptor == null && dataShapeLimitDescriptor2 == null && dataShapeLimitDescriptor3 == null && list == null)
			{
				return null;
			}
			return new DataShapeLimits
			{
				Primary = dataShapeLimitDescriptor,
				Secondary = dataShapeLimitDescriptor2,
				Intersection = dataShapeLimitDescriptor3,
				Scoped = list
			};
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004C10 File Offset: 0x00002E10
		private static List<DataShapeLimitDescriptor> CreateScopedLimitDescriptors(List<IntermediateScopedReductionAlgorithm> scopedReductions)
		{
			if (scopedReductions.IsNullOrEmpty<IntermediateScopedReductionAlgorithm>())
			{
				return null;
			}
			List<DataShapeLimitDescriptor> list = new List<DataShapeLimitDescriptor>(scopedReductions.Count);
			foreach (IntermediateScopedReductionAlgorithm intermediateScopedReductionAlgorithm in scopedReductions)
			{
				list.Add(DataShapeLimitsBuilder.CreateLimitDescriptor(intermediateScopedReductionAlgorithm));
			}
			return list;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004C7C File Offset: 0x00002E7C
		private static DataShapeLimitDescriptor CreateLimitDescriptor(IntermediateScopedReductionAlgorithm scopedReduction)
		{
			DataShapeLimitDescriptor dataShapeLimitDescriptor = DataShapeLimitsBuilder.CreateLimitDescriptor(scopedReduction.Algorithm, true);
			dataShapeLimitDescriptor.Scope = DataShapeLimitsBuilder.CreateDataReductionScope(scopedReduction.Scope);
			return dataShapeLimitDescriptor;
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004C9B File Offset: 0x00002E9B
		private static DataReductionScope CreateDataReductionScope(IntermediateReductionScope scope)
		{
			return new DataReductionScope
			{
				Primary = DataShapeLimitsBuilder.CreateScopeList(scope.Primary),
				Secondary = DataShapeLimitsBuilder.CreateScopeList(scope.Secondary)
			};
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004CC4 File Offset: 0x00002EC4
		private static List<int> CreateScopeList(List<int> intermediateScopeList)
		{
			if (intermediateScopeList.IsNullOrEmpty<int>())
			{
				return null;
			}
			return new List<int>(intermediateScopeList);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004CD8 File Offset: 0x00002ED8
		private static DataShapeLimitDescriptor CreateLimitDescriptor(IntermediateReductionAlgorithm algorithm, bool includeWindow)
		{
			if (algorithm == null || (!includeWindow && algorithm is IntermediateDataWindow))
			{
				return null;
			}
			DataShapeLimitDescriptor dataShapeLimitDescriptor = new DataShapeLimitDescriptor();
			dataShapeLimitDescriptor.Id = algorithm.Id;
			IntermediateSimpleLimit intermediateSimpleLimit = algorithm as IntermediateSimpleLimit;
			if (intermediateSimpleLimit == null)
			{
				IntermediateBinnedLineSampleLimit intermediateBinnedLineSampleLimit = algorithm as IntermediateBinnedLineSampleLimit;
				if (intermediateBinnedLineSampleLimit == null)
				{
					IntermediateOverlappingPointsSampleLimit intermediateOverlappingPointsSampleLimit = algorithm as IntermediateOverlappingPointsSampleLimit;
					if (intermediateOverlappingPointsSampleLimit == null)
					{
						IntermediateTopNPerLevelSampleLimit intermediateTopNPerLevelSampleLimit = algorithm as IntermediateTopNPerLevelSampleLimit;
						if (intermediateTopNPerLevelSampleLimit == null)
						{
							IntermediateDataWindow intermediateDataWindow = algorithm as IntermediateDataWindow;
							if (intermediateDataWindow == null)
							{
								throw new InvalidOperationException("Unknown limit algorithm type.");
							}
							dataShapeLimitDescriptor.Window = DataShapeLimitsBuilder.CreateWindowLimitDescriptor(intermediateDataWindow);
						}
						else
						{
							dataShapeLimitDescriptor.TopNPerLevel = DataShapeLimitsBuilder.CreateTopNPerLevelSampleLimit(intermediateTopNPerLevelSampleLimit);
						}
					}
					else
					{
						dataShapeLimitDescriptor.OverlappingPointsSample = DataShapeLimitsBuilder.CreateOverlappingPointsSampleLimit(intermediateOverlappingPointsSampleLimit);
					}
				}
				else
				{
					dataShapeLimitDescriptor.BinnedLineSample = DataShapeLimitsBuilder.CreateBinnedLineSampleLimit(intermediateBinnedLineSampleLimit);
				}
			}
			else
			{
				DataShapeLimitsBuilder.CreateSimpleLimit(intermediateSimpleLimit, dataShapeLimitDescriptor);
			}
			return dataShapeLimitDescriptor;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004D90 File Offset: 0x00002F90
		private static WindowLimitDescriptor CreateWindowLimitDescriptor(IntermediateDataWindow window)
		{
			return new WindowLimitDescriptor
			{
				Count = window.Count.Value,
				Calc = window.Calc
			};
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00004DC4 File Offset: 0x00002FC4
		private static void CreateSimpleLimit(IntermediateSimpleLimit simpleLimit, DataShapeLimitDescriptor descriptor)
		{
			switch (simpleLimit.Kind)
			{
			case IntermediateSimpleLimitKind.Top:
			case IntermediateSimpleLimitKind.First:
				descriptor.Top = new TopLimitDescriptor
				{
					Count = simpleLimit.Count.Value,
					Calc = simpleLimit.Calc
				};
				return;
			case IntermediateSimpleLimitKind.Sample:
				descriptor.Sample = new SampleLimitDescriptor
				{
					Count = simpleLimit.Count.Value,
					Calc = simpleLimit.Calc
				};
				return;
			case IntermediateSimpleLimitKind.Bottom:
			case IntermediateSimpleLimitKind.Last:
				descriptor.Bottom = new BottomLimitDescriptor
				{
					Count = simpleLimit.Count.Value,
					Calc = simpleLimit.Calc
				};
				return;
			default:
				throw new InvalidOperationException("Unsupported simple limit kind");
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00004E88 File Offset: 0x00003088
		private static BinnedLineSampleLimitDescriptor CreateBinnedLineSampleLimit(IntermediateBinnedLineSampleLimit binnedLineSampleLimit)
		{
			return new BinnedLineSampleLimitDescriptor
			{
				MaxTargetPointCount = binnedLineSampleLimit.Count.Value,
				MinPointsPerSeriesCount = binnedLineSampleLimit.MinPointsPerSeries.Value,
				IntersectionDbCountCalc = binnedLineSampleLimit.DbCountCalc,
				PrimaryDbCountCalc = binnedLineSampleLimit.DbPrimaryCalc,
				SecondaryDbCountCalc = binnedLineSampleLimit.DbSecondaryCalc,
				WarningCount = binnedLineSampleLimit.WarningCount
			};
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00004EF4 File Offset: 0x000030F4
		private static OverlappingPointsSampleLimitDescriptor CreateOverlappingPointsSampleLimit(IntermediateOverlappingPointsSampleLimit overlappingPointsSampleLimit)
		{
			return new OverlappingPointsSampleLimitDescriptor
			{
				Count = overlappingPointsSampleLimit.Count.Value,
				X = DataShapeLimitsBuilder.CreatePlotAxisDescriptor(overlappingPointsSampleLimit.X),
				Y = DataShapeLimitsBuilder.CreatePlotAxisDescriptor(overlappingPointsSampleLimit.Y)
			};
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x00004F3C File Offset: 0x0000313C
		private static TopNPerLevelDescriptor CreateTopNPerLevelSampleLimit(IntermediateTopNPerLevelSampleLimit topNPerLevelSampleLimit)
		{
			return new TopNPerLevelDescriptor
			{
				Count = topNPerLevelSampleLimit.Count.Value
			};
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00004F62 File Offset: 0x00003162
		private static PlotAxisBindingDescriptor CreatePlotAxisDescriptor(IntermediatePlotAxisBinding plotAxis)
		{
			if (plotAxis == null || plotAxis.Applied == null)
			{
				return null;
			}
			return new PlotAxisBindingDescriptor
			{
				Transform = plotAxis.Transform,
				Applied = plotAxis.Applied
			};
		}
	}
}

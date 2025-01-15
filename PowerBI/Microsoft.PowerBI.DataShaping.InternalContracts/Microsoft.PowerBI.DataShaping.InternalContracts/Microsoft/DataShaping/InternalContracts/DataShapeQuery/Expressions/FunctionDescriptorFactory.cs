using System;
using System.Collections.Generic;
using Microsoft.DataShaping.Common;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery.Expressions
{
	// Token: 0x020000D7 RID: 215
	internal static class FunctionDescriptorFactory
	{
		// Token: 0x06000604 RID: 1540 RVA: 0x0000C34F File Offset: 0x0000A54F
		public static FunctionDescriptor GetDescriptor(string name)
		{
			return FunctionDescriptorFactory.Descriptors[name];
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x0000C35C File Offset: 0x0000A55C
		public static bool TryGetDescriptor(string name, out FunctionDescriptor descriptor)
		{
			return FunctionDescriptorFactory.Descriptors.TryGetValue(name, out descriptor);
		}

		// Token: 0x06000606 RID: 1542 RVA: 0x0000C36C File Offset: 0x0000A56C
		private static Dictionary<string, FunctionDescriptor> CreateDescriptors()
		{
			return new Dictionary<string, FunctionDescriptor>(FunctionDescriptorFactory.FunctionNameComparer)
			{
				{
					"Any",
					new FunctionDescriptor("Any", "Any", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"Average",
					new FunctionDescriptor("Average", "Core.Average", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"Count",
					new FunctionDescriptor("Count", "Core.Count", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"DistinctCount",
					new FunctionDescriptor("DistinctCount", "Core.DistinctCount", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"Max",
					new FunctionDescriptor("Max", "Core.Max", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"Min",
					new FunctionDescriptor("Min", "Core.Min", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"Sum",
					new FunctionDescriptor("Sum", "Core.Sum", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"CountRows",
					new FunctionDescriptor("CountRows", "CountRows", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, new ScalarValue?(false))
					}), false, true, false, true, false, true)
				},
				{
					"PercentileExc",
					new FunctionDescriptor("PercentileExc", "Core.PercentileExc", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null)
					}), false, true, false, true, false, true)
				},
				{
					"PercentileInc",
					new FunctionDescriptor("PercentileInc", "Core.PercentileInc", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null)
					}), false, true, false, true, false, true)
				},
				{
					"SingleValue",
					new FunctionDescriptor("SingleValue", "Core.SingleValue", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"PositiveValues",
					new FunctionDescriptor("PositiveValues", "PositiveValues", FunctionCategory.Filter, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedCalculationReference, false, null) }), false, true, false, true, false, true)
				},
				{
					"NegativeValues",
					new FunctionDescriptor("NegativeValues", "NegativeValues", FunctionCategory.Filter, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedCalculationReference, false, null) }), false, true, false, true, false, true)
				},
				{
					"Between",
					new FunctionDescriptor("Between", "Core.Between", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"Ceiling",
					new FunctionDescriptor("Ceiling", "Core.Ceiling", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"Floor",
					new FunctionDescriptor("Floor", "Core.Floor", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"Sqrt",
					new FunctionDescriptor("Sqrt", "Core.Sqrt", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"If",
					new FunctionDescriptor("If", "Core.If", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"IfError",
					new FunctionDescriptor("IfError", "Core.IfError", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"IsEmptyTable",
					new FunctionDescriptor("IsEmptyTable", "IsEmptyTable", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.BatchSubQuery, false, null) }), false, true, false, false, false, true)
				},
				{
					"IsNull",
					new FunctionDescriptor("IsNull", "IsNull", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"IsZero",
					new FunctionDescriptor("IsZero", "IsZero", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null)
					}), false, true, false, true, false, true)
				},
				{
					"Log10",
					new FunctionDescriptor("Log10", "Core.Log10", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, false, true, false, true)
				},
				{
					"MaxValue",
					new FunctionDescriptor("MaxValue", "MaxValue", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(true)
					}), true, true, false, true, false, true)
				},
				{
					"MinValue",
					new FunctionDescriptor("MinValue", "MinValue", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(true)
					}), true, true, false, true, false, true)
				},
				{
					"Power",
					new FunctionDescriptor("Power", "Core.Power", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, true, false, true, false, true)
				},
				{
					"ScopeKeys",
					new FunctionDescriptor("ScopeKeys", "Comparable", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(true) }), true, false, false, true, false, true)
				},
				{
					"Comparable",
					new FunctionDescriptor("Comparable", "Comparable", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(true) }), true, false, false, true, false, true)
				},
				{
					"Array",
					new FunctionDescriptor("Array", "Array", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(true) }), true, false, false, true, false, true)
				},
				{
					"Format",
					new FunctionDescriptor("Format", "Format", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null)
					}), false, true, false, true, false, true)
				},
				{
					"BinColumnMin",
					new FunctionDescriptor("BinColumnMin", "BinColumnMin", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedProperty, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null)
					}), false, false, false, true, false, true)
				},
				{
					"BinColumnMax",
					new FunctionDescriptor("BinColumnMax", "BinColumnMax", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedProperty, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedProperty, true, null)
					}), false, false, false, true, false, true)
				},
				{
					"Evaluate",
					new FunctionDescriptor("Evaluate", "Evaluate", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfAnyKind(false),
						ArgumentDescriptor.OfAnyKind(false)
					}), false, false, false, true, false, true)
				},
				{
					"Subtotal",
					new FunctionDescriptor("Subtotal", "Subtotal", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedCalculationReference, false, null) }), false, false, false, true, false, true)
				},
				{
					"Scope",
					new FunctionDescriptor("Scope", "Scope", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedProperty, true, null) }), false, false, false, false, false, true)
				},
				{
					"Intersect",
					new FunctionDescriptor("Intersect", "Intersect", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null)
					}), false, false, false, true, false, true)
				},
				{
					"Rollup",
					new FunctionDescriptor("Rollup", "Rollup", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null)
					}), false, false, false, true, false, true)
				},
				{
					"ScopeOf",
					new FunctionDescriptor("ScopeOf", "ScopeOf", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedCalculationReference, false, null) }), false, false, false, false, false, true)
				},
				{
					"LimitProperty",
					new FunctionDescriptor("LimitProperty", "LimitProperty", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[]
					{
						ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedLimitReference, false, null),
						ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, true, null)
					}), true, false, false, true, false, true)
				},
				{
					"Median",
					new FunctionDescriptor("Median", "Core.Median", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"StandardDeviation",
					new FunctionDescriptor("StandardDeviation", "Core.StandardDeviation", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"TransformOutputRoleRef",
					new FunctionDescriptor("TransformOutputRoleRef", "TransformOutputRoleRef", FunctionCategory.Context, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.Literal, false, null) }), true, false, false, true, false, true)
				},
				{
					"Variance",
					new FunctionDescriptor("Variance", "Core.Variance", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfAnyKind(false) }), false, true, true, true, false, true)
				},
				{
					"SynchronizationIndex",
					new FunctionDescriptor("SynchronizationIndex", "SynchronizationIndex", FunctionCategory.Scalar, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null) }), false, false, false, true, false, true)
				},
				{
					"RangeMax",
					new FunctionDescriptor("RangeMax", "RangeMax", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null) }), false, false, false, true, false, true)
				},
				{
					"RangeMin",
					new FunctionDescriptor("RangeMin", "RangeMin", FunctionCategory.Aggregate, FunctionDescriptorFactory.MakeArgs(new ArgumentDescriptor[] { ArgumentDescriptor.OfKind(ExpressionNodeKind.ResolvedScopeReference, false, null) }), false, false, false, true, false, true)
				}
			};
		}

		// Token: 0x06000607 RID: 1543 RVA: 0x0000CF02 File Offset: 0x0000B102
		private static IEnumerable<ArgumentDescriptor> MakeArgs(params ArgumentDescriptor[] args)
		{
			return args;
		}

		// Token: 0x0400027D RID: 637
		private const string NsPrefix = "Core.";

		// Token: 0x0400027E RID: 638
		internal static StringComparer FunctionNameComparer = StringComparer.Ordinal;

		// Token: 0x0400027F RID: 639
		private static readonly Dictionary<string, FunctionDescriptor> Descriptors = FunctionDescriptorFactory.CreateDescriptors();
	}
}

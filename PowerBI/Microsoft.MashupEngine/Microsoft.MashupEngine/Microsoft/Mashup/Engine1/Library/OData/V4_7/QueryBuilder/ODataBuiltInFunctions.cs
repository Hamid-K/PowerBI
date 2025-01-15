using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using Microsoft.OData.UriParser.Aggregation;

namespace Microsoft.Mashup.Engine1.Library.OData.V4_7.QueryBuilder
{
	// Token: 0x020007C6 RID: 1990
	internal static class ODataBuiltInFunctions
	{
		// Token: 0x060039DA RID: 14810 RVA: 0x000BABB0 File Offset: 0x000B8DB0
		public static bool TryFindAggregateFunction(Value function, out AggregationMethod aggregateMethod)
		{
			return ODataBuiltInFunctions.AggregateFunctionLookup.TryGetValue(function, out aggregateMethod);
		}

		// Token: 0x060039DB RID: 14811 RVA: 0x000BABBE File Offset: 0x000B8DBE
		public static bool TryFindODataFunctionValue(Value mFunctionValue, out ODataBuiltInFunctions.PrimitiveFunctionValue[] functionValues)
		{
			return ODataBuiltInFunctions.MFunctionValueToODataFunctionValue.TryGetValue(mFunctionValue, out functionValues);
		}

		// Token: 0x060039DC RID: 14812 RVA: 0x000BABCC File Offset: 0x000B8DCC
		public static bool TryFindBuiltInFunctionCallNode(Value mFunctionValue, out Func<IEnumerable<QueryNode>, QueryNode> functionCallNodeFunc)
		{
			return ODataBuiltInFunctions.FunctionValueToSingleValueFunctionCallLookup.TryGetValue(mFunctionValue.AsFunction, out functionCallNodeFunc);
		}

		// Token: 0x060039DD RID: 14813 RVA: 0x000BABE0 File Offset: 0x000B8DE0
		public static QueryNode ConvertCombineToConcat(IEnumerable<QueryNode> arguments)
		{
			QueryNode queryNode;
			using (IEnumerator<QueryNode> enumerator = arguments.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					queryNode = ODataExpression.EmptyStringConstant;
				}
				else
				{
					QueryNode queryNode2 = enumerator.Current;
					if (!enumerator.MoveNext())
					{
						queryNode = queryNode2;
					}
					else
					{
						do
						{
							queryNode2 = new SingleValueFunctionCallNode("concat", new QueryNode[] { queryNode2, enumerator.Current }, EdmCoreModel.Instance.GetString(true));
						}
						while (enumerator.MoveNext());
						queryNode = queryNode2;
					}
				}
			}
			return queryNode;
		}

		// Token: 0x060039DE RID: 14814 RVA: 0x000BAC68 File Offset: 0x000B8E68
		internal static IEnumerable<FunctionValue> GetFunctionCallNodeKeys()
		{
			return ODataBuiltInFunctions.FunctionValueToSingleValueFunctionCallLookup.Keys;
		}

		// Token: 0x060039DF RID: 14815 RVA: 0x000BAC74 File Offset: 0x000B8E74
		internal static IEnumerable<FunctionValue> GetODataFunctionValueKeys()
		{
			return ODataBuiltInFunctions.MFunctionValueToODataFunctionValue.Keys.Select((Value v) => v.AsFunction);
		}

		// Token: 0x04001DFB RID: 7675
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Contains = new ODataBuiltInFunctions.PrimitiveFunctionValue("contains", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04001DFC RID: 7676
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue EndsWith = new ODataBuiltInFunctions.PrimitiveFunctionValue("endswith", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04001DFD RID: 7677
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue StartsWith = new ODataBuiltInFunctions.PrimitiveFunctionValue("startswith", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04001DFE RID: 7678
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Length = new ODataBuiltInFunctions.PrimitiveFunctionValue("length", TypeValue.Number, new TypeValue[] { TypeValue.Text });

		// Token: 0x04001DFF RID: 7679
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Trim = new ODataBuiltInFunctions.PrimitiveFunctionValue("trim", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x04001E00 RID: 7680
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Concat = new ODataBuiltInFunctions.PrimitiveFunctionValue("concat", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04001E01 RID: 7681
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue IndexOf = new ODataBuiltInFunctions.PrimitiveFunctionValue("indexof", TypeValue.Number, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04001E02 RID: 7682
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue SubString = new ODataBuiltInFunctions.PrimitiveFunctionValue("substring", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Number
		});

		// Token: 0x04001E03 RID: 7683
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue SubString2 = new ODataBuiltInFunctions.PrimitiveFunctionValue("substring", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Number,
			TypeValue.Number
		});

		// Token: 0x04001E04 RID: 7684
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue ToLower = new ODataBuiltInFunctions.PrimitiveFunctionValue("tolower", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x04001E05 RID: 7685
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue ToUpper = new ODataBuiltInFunctions.PrimitiveFunctionValue("toupper", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x04001E06 RID: 7686
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Year = new ODataBuiltInFunctions.PrimitiveFunctionValue("year", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E07 RID: 7687
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Month = new ODataBuiltInFunctions.PrimitiveFunctionValue("month", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E08 RID: 7688
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Day = new ODataBuiltInFunctions.PrimitiveFunctionValue("day", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E09 RID: 7689
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Hour = new ODataBuiltInFunctions.PrimitiveFunctionValue("hour", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E0A RID: 7690
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Minute = new ODataBuiltInFunctions.PrimitiveFunctionValue("minute", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E0B RID: 7691
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Second = new ODataBuiltInFunctions.PrimitiveFunctionValue("second", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04001E0C RID: 7692
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Now = new ODataBuiltInFunctions.PrimitiveFunctionValue("now", TypeValue.DateTimeZone, Array.Empty<TypeValue>());

		// Token: 0x04001E0D RID: 7693
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Round = new ODataBuiltInFunctions.PrimitiveFunctionValue("round", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x04001E0E RID: 7694
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Floor = new ODataBuiltInFunctions.PrimitiveFunctionValue("floor", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x04001E0F RID: 7695
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Ceiling = new ODataBuiltInFunctions.PrimitiveFunctionValue("ceiling", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x04001E10 RID: 7696
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue CombineMadeFromConcat = new ODataBuiltInFunctions.PrimitiveFunctionValue("concat", TypeValue.Text, new TypeValue[]
		{
			TypeValue.List,
			TypeValue.Text
		});

		// Token: 0x04001E11 RID: 7697
		private static readonly Dictionary<FunctionValue, Func<IEnumerable<QueryNode>, QueryNode>> FunctionValueToSingleValueFunctionCallLookup = new Dictionary<FunctionValue, Func<IEnumerable<QueryNode>, QueryNode>>
		{
			{
				TimeSpecificFunction.DateTimeZoneUtcNow,
				(IEnumerable<QueryNode> nodes) => new SingleValueFunctionCallNode("now", nodes, EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset, false))
			},
			{
				Library.Date.Year,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("year")
			},
			{
				Library.Date.Month,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("month")
			},
			{
				Library.Date.Day,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("day")
			},
			{
				Library.Time.Hour,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("hour")
			},
			{
				Library.Time.Minute,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("minute")
			},
			{
				Library.Time.Second,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("second")
			},
			{
				Library.Text.Length,
				(IEnumerable<QueryNode> nodes) => nodes.CreateInt32ReturnTypeFunctionCallNode("length")
			},
			{
				Library.Text.Trim,
				(IEnumerable<QueryNode> nodes) => nodes.CreateStringReturnTypeFunctionCallNode("trim")
			},
			{
				Library.Text.Contains,
				(IEnumerable<QueryNode> nodes) => nodes.CreateStringReturnTypeFunctionCallNode("contains")
			},
			{
				Library.Text.StartsWith,
				(IEnumerable<QueryNode> nodes) => nodes.CreateBooleanReturnTypeFunctionCallNode("startswith")
			},
			{
				Library.Text.EndsWith,
				(IEnumerable<QueryNode> nodes) => nodes.CreateBooleanReturnTypeFunctionCallNode("endswith")
			},
			{
				Library.Text.PositionOf,
				(IEnumerable<QueryNode> nodes) => nodes.CreateBooleanReturnTypeFunctionCallNode("indexof")
			},
			{
				Library.Text.Range,
				(IEnumerable<QueryNode> nodes) => nodes.CreateBooleanReturnTypeFunctionCallNode("substring")
			},
			{
				Library.Text.Combine,
				(IEnumerable<QueryNode> nodes) => ODataBuiltInFunctions.ConvertCombineToConcat(nodes)
			},
			{
				CultureSpecificFunction.TextLower,
				(IEnumerable<QueryNode> nodes) => nodes.CreateStringReturnTypeFunctionCallNode("tolower")
			},
			{
				CultureSpecificFunction.TextUpper,
				(IEnumerable<QueryNode> nodes) => nodes.CreateStringReturnTypeFunctionCallNode("toupper")
			},
			{
				Library.Number.Round,
				(IEnumerable<QueryNode> nodes) => nodes.Single<QueryNode>().CreateFunctionWithSingleParameterInWithSameReturnTypeOut("round")
			},
			{
				Library.Number.RoundUp,
				(IEnumerable<QueryNode> nodes) => nodes.Single<QueryNode>().CreateFunctionWithSingleParameterInWithSameReturnTypeOut("ceiling")
			},
			{
				Library.Number.RoundDown,
				(IEnumerable<QueryNode> nodes) => nodes.Single<QueryNode>().CreateFunctionWithSingleParameterInWithSameReturnTypeOut("floor")
			}
		};

		// Token: 0x04001E12 RID: 7698
		private static readonly IDictionary<Value, ODataBuiltInFunctions.PrimitiveFunctionValue[]> MFunctionValueToODataFunctionValue = new Dictionary<Value, ODataBuiltInFunctions.PrimitiveFunctionValue[]>
		{
			{
				Library.Date.Year,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Year }
			},
			{
				Library.Date.Month,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Month }
			},
			{
				Library.Date.Day,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Day }
			},
			{
				Library.Time.Hour,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Hour }
			},
			{
				Library.Time.Minute,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Minute }
			},
			{
				Library.Time.Second,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Second }
			},
			{
				Library.Text.Length,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Length }
			},
			{
				Library.Text.Trim,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Trim }
			},
			{
				Library.Text.Contains,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Contains }
			},
			{
				Library.Text.StartsWith,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.StartsWith }
			},
			{
				Library.Text.EndsWith,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.EndsWith }
			},
			{
				Library.Text.PositionOf,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.IndexOf }
			},
			{
				Library.Text.Range,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[]
				{
					ODataBuiltInFunctions.SubString,
					ODataBuiltInFunctions.SubString2
				}
			},
			{
				Library.Text.Combine,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.CombineMadeFromConcat }
			},
			{
				Library.Number.Round,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Round }
			},
			{
				Library.Number.RoundUp,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Ceiling }
			},
			{
				Library.Number.RoundDown,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Floor }
			},
			{
				TimeSpecificFunction.DateTimeZoneUtcNow,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.Now }
			},
			{
				CultureSpecificFunction.TextLower,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.ToLower }
			},
			{
				CultureSpecificFunction.TextUpper,
				new ODataBuiltInFunctions.PrimitiveFunctionValue[] { ODataBuiltInFunctions.ToUpper }
			}
		};

		// Token: 0x04001E13 RID: 7699
		private static readonly IDictionary<Value, AggregationMethod> AggregateFunctionLookup = new Dictionary<Value, AggregationMethod>
		{
			{
				Library.List.Average,
				AggregationMethod.Average
			},
			{
				Library.List.Max,
				AggregationMethod.Max
			},
			{
				Library.List.Min,
				AggregationMethod.Min
			},
			{
				Library.List.Sum,
				AggregationMethod.Sum
			}
		};

		// Token: 0x020007C7 RID: 1991
		internal class PrimitiveFunctionValue : NativeFunctionValueN
		{
			// Token: 0x060039E1 RID: 14817 RVA: 0x000BB444 File Offset: 0x000B9644
			public PrimitiveFunctionValue(string odataFunctionName, TypeValue returnType, params TypeValue[] parameters)
				: base(parameters.Length, ODataBuiltInFunctions.PrimitiveFunctionValue.args[parameters.Length])
			{
				this.returnType = returnType;
				this.parameters = parameters;
				this.odataFunctionName = odataFunctionName;
			}

			// Token: 0x060039E2 RID: 14818 RVA: 0x000BB470 File Offset: 0x000B9670
			protected override Value InvokeN(Value[] args)
			{
				for (int i = 0; i < args.Length; i++)
				{
					TypeValue typeValue = this.parameters[i];
					TypeValue asType = args[i].AsType;
					if (asType.TypeKind != typeValue.TypeKind || asType.IsNullable != typeValue.IsNullable)
					{
						return TypeValue.None;
					}
				}
				return this.ReturnType;
			}

			// Token: 0x17001388 RID: 5000
			// (get) Token: 0x060039E3 RID: 14819 RVA: 0x000BB4C5 File Offset: 0x000B96C5
			protected override TypeValue ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x17001389 RID: 5001
			// (get) Token: 0x060039E4 RID: 14820 RVA: 0x000BB4CD File Offset: 0x000B96CD
			public string ODataFunctionName
			{
				get
				{
					return this.odataFunctionName;
				}
			}

			// Token: 0x04001E14 RID: 7700
			private static readonly string[][] args = new string[][]
			{
				new string[0],
				new string[] { "arg0" },
				new string[] { "arg0", "arg1" },
				new string[] { "arg0", "arg1", "arg2" }
			};

			// Token: 0x04001E15 RID: 7701
			private readonly TypeValue[] parameters;

			// Token: 0x04001E16 RID: 7702
			private readonly TypeValue returnType;

			// Token: 0x04001E17 RID: 7703
			private readonly string odataFunctionName;
		}
	}
}

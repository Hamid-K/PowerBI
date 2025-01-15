using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.OData.Core.UriParser.Aggregation;
using Microsoft.OData.Core.UriParser.Semantic;
using Microsoft.OData.Edm;
using Microsoft.OData.Edm.Library;

namespace Microsoft.Mashup.Engine1.Library.OData.V4
{
	// Token: 0x02000857 RID: 2135
	internal static class ODataBuiltInFunctions
	{
		// Token: 0x06003D72 RID: 15730 RVA: 0x000C7B02 File Offset: 0x000C5D02
		public static bool TryFindAggregateFunction(Value function, out AggregationMethod aggregateMethod)
		{
			return ODataBuiltInFunctions.AggregateFunctionLookup.TryGetValue(function, out aggregateMethod);
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000C7B10 File Offset: 0x000C5D10
		public static bool TryFindODataFunctionValue(Value mFunctionValue, out ODataBuiltInFunctions.PrimitiveFunctionValue[] functionValues)
		{
			if (ODataBuiltInFunctions.MFunctionValueToODataFunctionValue.TryGetValue(mFunctionValue, out functionValues))
			{
				return true;
			}
			Type type = mFunctionValue.GetType();
			ODataBuiltInFunctions.PrimitiveFunctionValue primitiveFunctionValue;
			if (ODataBuiltInFunctions.MFunctionValueTypeToODataFunctionValue.TryGetValue(type, out primitiveFunctionValue))
			{
				functionValues = new ODataBuiltInFunctions.PrimitiveFunctionValue[] { primitiveFunctionValue };
				return true;
			}
			functionValues = null;
			return false;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000C7B55 File Offset: 0x000C5D55
		public static bool TryFindBuiltInFunctionCallNode(Value mFunctionValue, out Func<IEnumerable<QueryNode>, QueryNode> functionCallNodeFunc)
		{
			if (ODataBuiltInFunctions.FunctionValueToSingleValueFunctionCallLookup.TryGetValue(mFunctionValue.AsFunction, out functionCallNodeFunc))
			{
				return true;
			}
			functionCallNodeFunc = null;
			return false;
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000C7B70 File Offset: 0x000C5D70
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
							queryNode2 = new SingleValueFunctionCallNode("concat", new QueryNode[] { queryNode2, enumerator.Current }, Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetString(true));
						}
						while (enumerator.MoveNext());
						queryNode = queryNode2;
					}
				}
			}
			return queryNode;
		}

		// Token: 0x0400202A RID: 8234
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Contains = new ODataBuiltInFunctions.PrimitiveFunctionValue("contains", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x0400202B RID: 8235
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue EndsWith = new ODataBuiltInFunctions.PrimitiveFunctionValue("endswith", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x0400202C RID: 8236
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue StartsWith = new ODataBuiltInFunctions.PrimitiveFunctionValue("startswith", TypeValue.Logical, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x0400202D RID: 8237
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Length = new ODataBuiltInFunctions.PrimitiveFunctionValue("length", TypeValue.Number, new TypeValue[] { TypeValue.Text });

		// Token: 0x0400202E RID: 8238
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Trim = new ODataBuiltInFunctions.PrimitiveFunctionValue("trim", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x0400202F RID: 8239
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Concat = new ODataBuiltInFunctions.PrimitiveFunctionValue("concat", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04002030 RID: 8240
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue IndexOf = new ODataBuiltInFunctions.PrimitiveFunctionValue("indexof", TypeValue.Number, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Text
		});

		// Token: 0x04002031 RID: 8241
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue SubString = new ODataBuiltInFunctions.PrimitiveFunctionValue("substring", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Number
		});

		// Token: 0x04002032 RID: 8242
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue SubString2 = new ODataBuiltInFunctions.PrimitiveFunctionValue("substring", TypeValue.Text, new TypeValue[]
		{
			TypeValue.Text,
			TypeValue.Number,
			TypeValue.Number
		});

		// Token: 0x04002033 RID: 8243
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue ToLower = new ODataBuiltInFunctions.PrimitiveFunctionValue("tolower", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x04002034 RID: 8244
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue ToUpper = new ODataBuiltInFunctions.PrimitiveFunctionValue("toupper", TypeValue.Text, new TypeValue[] { TypeValue.Text });

		// Token: 0x04002035 RID: 8245
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Year = new ODataBuiltInFunctions.PrimitiveFunctionValue("year", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04002036 RID: 8246
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Month = new ODataBuiltInFunctions.PrimitiveFunctionValue("month", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04002037 RID: 8247
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Day = new ODataBuiltInFunctions.PrimitiveFunctionValue("day", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04002038 RID: 8248
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Hour = new ODataBuiltInFunctions.PrimitiveFunctionValue("hour", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x04002039 RID: 8249
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Minute = new ODataBuiltInFunctions.PrimitiveFunctionValue("minute", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x0400203A RID: 8250
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Second = new ODataBuiltInFunctions.PrimitiveFunctionValue("second", TypeValue.Number, new TypeValue[] { TypeValue.DateTimeZone });

		// Token: 0x0400203B RID: 8251
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Now = new ODataBuiltInFunctions.PrimitiveFunctionValue("now", TypeValue.DateTimeZone, Array.Empty<TypeValue>());

		// Token: 0x0400203C RID: 8252
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Round = new ODataBuiltInFunctions.PrimitiveFunctionValue("round", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x0400203D RID: 8253
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Floor = new ODataBuiltInFunctions.PrimitiveFunctionValue("floor", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x0400203E RID: 8254
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue Ceiling = new ODataBuiltInFunctions.PrimitiveFunctionValue("ceiling", TypeValue.Number, new TypeValue[] { TypeValue.Number });

		// Token: 0x0400203F RID: 8255
		private static readonly ODataBuiltInFunctions.PrimitiveFunctionValue CombineMadeFromConcat = new ODataBuiltInFunctions.PrimitiveFunctionValue("concat", TypeValue.Text, new TypeValue[]
		{
			TypeValue.List,
			TypeValue.Text
		});

		// Token: 0x04002040 RID: 8256
		private static readonly Dictionary<FunctionValue, Func<IEnumerable<QueryNode>, QueryNode>> FunctionValueToSingleValueFunctionCallLookup = new Dictionary<FunctionValue, Func<IEnumerable<QueryNode>, QueryNode>>
		{
			{
				TimeSpecificFunction.DateTimeZoneUtcNow,
				(IEnumerable<QueryNode> nodes) => new SingleValueFunctionCallNode("now", nodes, Microsoft.OData.Edm.Library.EdmCoreModel.Instance.GetPrimitive(Microsoft.OData.Edm.EdmPrimitiveTypeKind.DateTimeOffset, false))
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

		// Token: 0x04002041 RID: 8257
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
			}
		};

		// Token: 0x04002042 RID: 8258
		private static readonly IDictionary<Type, ODataBuiltInFunctions.PrimitiveFunctionValue> MFunctionValueTypeToODataFunctionValue = new Dictionary<Type, ODataBuiltInFunctions.PrimitiveFunctionValue>
		{
			{
				typeof(Library.DateTimeZone.UtcNowFunctionValue),
				ODataBuiltInFunctions.Now
			},
			{
				typeof(Library.Text.LowerFunctionValue),
				ODataBuiltInFunctions.ToLower
			},
			{
				typeof(Library.Text.UpperFunctionValue),
				ODataBuiltInFunctions.ToUpper
			}
		};

		// Token: 0x04002043 RID: 8259
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

		// Token: 0x02000858 RID: 2136
		internal class PrimitiveFunctionValue : NativeFunctionValueN
		{
			// Token: 0x06003D77 RID: 15735 RVA: 0x000C8360 File Offset: 0x000C6560
			public PrimitiveFunctionValue(string odataFunctionName, TypeValue returnType, params TypeValue[] parameters)
				: base(parameters.Length, ODataBuiltInFunctions.PrimitiveFunctionValue.args[parameters.Length])
			{
				this.returnType = returnType;
				this.parameters = parameters;
				this.odataFunctionName = odataFunctionName;
			}

			// Token: 0x06003D78 RID: 15736 RVA: 0x000C838C File Offset: 0x000C658C
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

			// Token: 0x17001444 RID: 5188
			// (get) Token: 0x06003D79 RID: 15737 RVA: 0x000C83E1 File Offset: 0x000C65E1
			protected override TypeValue ReturnType
			{
				get
				{
					return this.returnType;
				}
			}

			// Token: 0x17001445 RID: 5189
			// (get) Token: 0x06003D7A RID: 15738 RVA: 0x000C83E9 File Offset: 0x000C65E9
			public string ODataFunctionName
			{
				get
				{
					return this.odataFunctionName;
				}
			}

			// Token: 0x04002044 RID: 8260
			private static readonly string[][] args = new string[][]
			{
				new string[0],
				new string[] { "arg0" },
				new string[] { "arg0", "arg1" },
				new string[] { "arg0", "arg1", "arg2" }
			};

			// Token: 0x04002045 RID: 8261
			private readonly TypeValue[] parameters;

			// Token: 0x04002046 RID: 8262
			private readonly TypeValue returnType;

			// Token: 0x04002047 RID: 8263
			private readonly string odataFunctionName;
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AB RID: 683
	[DataContract(Name = "Expression", Namespace = "http://schemas.microsoft.com/sqlbi/2013/01/NLRuntimeService")]
	public sealed class QueryExpressionContainer : IEquatable<QueryExpressionContainer>
	{
		// Token: 0x060014ED RID: 5357 RVA: 0x00026628 File Offset: 0x00024828
		public QueryExpressionContainer()
		{
		}

		// Token: 0x060014EE RID: 5358 RVA: 0x00026630 File Offset: 0x00024830
		public QueryExpressionContainer(QueryExpression expression, string name = null, string nativeReferenceName = null)
		{
			Contract.CheckParam(expression != null, "expression");
			this.SetExpression(expression);
			this.Name = name;
			this.NativeReferenceName = nativeReferenceName;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060014EF RID: 5359 RVA: 0x0002665E File Offset: 0x0002485E
		// (set) Token: 0x060014F0 RID: 5360 RVA: 0x00026666 File Offset: 0x00024866
		[DataMember(IsRequired = false, Order = 1, EmitDefaultValue = false)]
		public QuerySourceRefExpression SourceRef
		{
			get
			{
				return this.GetExpression<QuerySourceRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060014F1 RID: 5361 RVA: 0x0002666F File Offset: 0x0002486F
		// (set) Token: 0x060014F2 RID: 5362 RVA: 0x00026677 File Offset: 0x00024877
		public QueryPropertyExpression Property
		{
			get
			{
				return this.GetExpression<QueryPropertyExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060014F3 RID: 5363 RVA: 0x00026680 File Offset: 0x00024880
		// (set) Token: 0x060014F4 RID: 5364 RVA: 0x000266B7 File Offset: 0x000248B7
		[DataMember(IsRequired = false, Name = "Property", Order = 2, EmitDefaultValue = false)]
		private QueryPropertyExpression PropertyContract
		{
			get
			{
				QueryPropertyExpression expression = this.GetExpression<QueryPropertyExpression>();
				if (!(expression != null) || !(expression.GetType() == typeof(QueryPropertyExpression)))
				{
					return null;
				}
				return expression;
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x060014F5 RID: 5365 RVA: 0x000266C0 File Offset: 0x000248C0
		// (set) Token: 0x060014F6 RID: 5366 RVA: 0x000266C8 File Offset: 0x000248C8
		[DataMember(IsRequired = false, Order = 3, EmitDefaultValue = false)]
		public QueryAndExpression And
		{
			get
			{
				return this.GetExpression<QueryAndExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x060014F7 RID: 5367 RVA: 0x000266D1 File Offset: 0x000248D1
		// (set) Token: 0x060014F8 RID: 5368 RVA: 0x000266D9 File Offset: 0x000248D9
		[DataMember(IsRequired = false, Order = 4, EmitDefaultValue = false)]
		public QueryOrExpression Or
		{
			get
			{
				return this.GetExpression<QueryOrExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x060014F9 RID: 5369 RVA: 0x000266E2 File Offset: 0x000248E2
		// (set) Token: 0x060014FA RID: 5370 RVA: 0x000266EA File Offset: 0x000248EA
		[DataMember(IsRequired = false, Order = 5, EmitDefaultValue = false)]
		public QueryContainsExpression Contains
		{
			get
			{
				return this.GetExpression<QueryContainsExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x060014FB RID: 5371 RVA: 0x000266F3 File Offset: 0x000248F3
		// (set) Token: 0x060014FC RID: 5372 RVA: 0x000266FB File Offset: 0x000248FB
		[DataMember(IsRequired = false, Order = 6, EmitDefaultValue = false)]
		public QueryComparisonExpression Comparison
		{
			get
			{
				return this.GetExpression<QueryComparisonExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x060014FD RID: 5373 RVA: 0x00026704 File Offset: 0x00024904
		// (set) Token: 0x060014FE RID: 5374 RVA: 0x0002670C File Offset: 0x0002490C
		[DataMember(IsRequired = false, Order = 7, EmitDefaultValue = false)]
		public QueryBetweenExpression Between
		{
			get
			{
				return this.GetExpression<QueryBetweenExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x060014FF RID: 5375 RVA: 0x00026715 File Offset: 0x00024915
		// (set) Token: 0x06001500 RID: 5376 RVA: 0x0002671D File Offset: 0x0002491D
		[DataMember(IsRequired = false, Order = 8, EmitDefaultValue = false)]
		public QueryInExpression In
		{
			get
			{
				return this.GetExpression<QueryInExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06001501 RID: 5377 RVA: 0x00026726 File Offset: 0x00024926
		// (set) Token: 0x06001502 RID: 5378 RVA: 0x0002672E File Offset: 0x0002492E
		[DataMember(IsRequired = false, Order = 8, EmitDefaultValue = false)]
		public QueryAggregationExpression Aggregation
		{
			get
			{
				return this.GetExpression<QueryAggregationExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06001503 RID: 5379 RVA: 0x00026737 File Offset: 0x00024937
		// (set) Token: 0x06001504 RID: 5380 RVA: 0x0002673F File Offset: 0x0002493F
		[DataMember(IsRequired = false, Order = 9, EmitDefaultValue = false)]
		public QueryExistsExpression Exists
		{
			get
			{
				return this.GetExpression<QueryExistsExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06001505 RID: 5381 RVA: 0x00026748 File Offset: 0x00024948
		// (set) Token: 0x06001506 RID: 5382 RVA: 0x00026750 File Offset: 0x00024950
		[DataMember(IsRequired = false, Order = 10, EmitDefaultValue = false)]
		public QueryDatePartExpression DatePart
		{
			get
			{
				return this.GetExpression<QueryDatePartExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06001507 RID: 5383 RVA: 0x00026759 File Offset: 0x00024959
		// (set) Token: 0x06001508 RID: 5384 RVA: 0x00026761 File Offset: 0x00024961
		[DataMember(IsRequired = false, Order = 11, EmitDefaultValue = false)]
		public QueryBooleanConstantExpression Boolean
		{
			get
			{
				return this.GetExpression<QueryBooleanConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06001509 RID: 5385 RVA: 0x0002676A File Offset: 0x0002496A
		// (set) Token: 0x0600150A RID: 5386 RVA: 0x00026772 File Offset: 0x00024972
		[DataMember(IsRequired = false, Order = 12, EmitDefaultValue = false)]
		public QueryIntegerConstantExpression Integer
		{
			get
			{
				return this.GetExpression<QueryIntegerConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x0600150B RID: 5387 RVA: 0x0002677B File Offset: 0x0002497B
		// (set) Token: 0x0600150C RID: 5388 RVA: 0x00026783 File Offset: 0x00024983
		[DataMember(IsRequired = false, Order = 13, EmitDefaultValue = false)]
		public QueryDecimalConstantExpression Decimal
		{
			get
			{
				return this.GetExpression<QueryDecimalConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x0600150D RID: 5389 RVA: 0x0002678C File Offset: 0x0002498C
		// (set) Token: 0x0600150E RID: 5390 RVA: 0x00026794 File Offset: 0x00024994
		[DataMember(IsRequired = false, Order = 14, EmitDefaultValue = false)]
		public QueryStringConstantExpression String
		{
			get
			{
				return this.GetExpression<QueryStringConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x0600150F RID: 5391 RVA: 0x0002679D File Offset: 0x0002499D
		// (set) Token: 0x06001510 RID: 5392 RVA: 0x000267A5 File Offset: 0x000249A5
		[DataMember(IsRequired = false, Order = 15, EmitDefaultValue = false)]
		public QueryDateTimeConstantExpression DateTime
		{
			get
			{
				return this.GetExpression<QueryDateTimeConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06001511 RID: 5393 RVA: 0x000267AE File Offset: 0x000249AE
		// (set) Token: 0x06001512 RID: 5394 RVA: 0x000267B6 File Offset: 0x000249B6
		[DataMember(IsRequired = false, Order = 16, EmitDefaultValue = false)]
		public QueryDateConstantExpression Date
		{
			get
			{
				return this.GetExpression<QueryDateConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x06001513 RID: 5395 RVA: 0x000267BF File Offset: 0x000249BF
		// (set) Token: 0x06001514 RID: 5396 RVA: 0x000267C7 File Offset: 0x000249C7
		[DataMember(IsRequired = false, Order = 19, EmitDefaultValue = false)]
		public QueryYearConstantExpression Year
		{
			get
			{
				return this.GetExpression<QueryYearConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x06001515 RID: 5397 RVA: 0x000267D0 File Offset: 0x000249D0
		// (set) Token: 0x06001516 RID: 5398 RVA: 0x000267D8 File Offset: 0x000249D8
		[DataMember(IsRequired = false, Order = 20, EmitDefaultValue = false)]
		public QueryDecadeConstantExpression Decade
		{
			get
			{
				return this.GetExpression<QueryDecadeConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x06001517 RID: 5399 RVA: 0x000267E1 File Offset: 0x000249E1
		// (set) Token: 0x06001518 RID: 5400 RVA: 0x000267E9 File Offset: 0x000249E9
		[DataMember(IsRequired = false, Order = 21, EmitDefaultValue = false)]
		public QueryYearAndMonthConstantExpression YearAndMonth
		{
			get
			{
				return this.GetExpression<QueryYearAndMonthConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x06001519 RID: 5401 RVA: 0x000267F2 File Offset: 0x000249F2
		// (set) Token: 0x0600151A RID: 5402 RVA: 0x000267FA File Offset: 0x000249FA
		[DataMember(IsRequired = false, Order = 22, EmitDefaultValue = false)]
		public QueryNotExpression Not
		{
			get
			{
				return this.GetExpression<QueryNotExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600151B RID: 5403 RVA: 0x00026803 File Offset: 0x00024A03
		// (set) Token: 0x0600151C RID: 5404 RVA: 0x0002680B File Offset: 0x00024A0B
		[DataMember(IsRequired = false, Order = 23, EmitDefaultValue = false)]
		public QueryNullConstantExpression Null
		{
			get
			{
				return this.GetExpression<QueryNullConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x0600151D RID: 5405 RVA: 0x00026814 File Offset: 0x00024A14
		// (set) Token: 0x0600151E RID: 5406 RVA: 0x0002681C File Offset: 0x00024A1C
		[DataMember(IsRequired = false, Order = 24, EmitDefaultValue = false)]
		public QueryStartsWithExpression StartsWith
		{
			get
			{
				return this.GetExpression<QueryStartsWithExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600151F RID: 5407 RVA: 0x00026825 File Offset: 0x00024A25
		// (set) Token: 0x06001520 RID: 5408 RVA: 0x0002682D File Offset: 0x00024A2D
		[DataMember(IsRequired = false, Order = 25, EmitDefaultValue = false)]
		public QueryDateTimeSecondConstantExpression DateTimeSecond
		{
			get
			{
				return this.GetExpression<QueryDateTimeSecondConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x00026836 File Offset: 0x00024A36
		// (set) Token: 0x06001522 RID: 5410 RVA: 0x0002683E File Offset: 0x00024A3E
		[DataMember(IsRequired = false, Order = 26, EmitDefaultValue = false)]
		public QueryNumberConstantExpression Number
		{
			get
			{
				return this.GetExpression<QueryNumberConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06001523 RID: 5411 RVA: 0x00026847 File Offset: 0x00024A47
		// (set) Token: 0x06001524 RID: 5412 RVA: 0x0002684F File Offset: 0x00024A4F
		[DataMember(IsRequired = false, Order = 27, EmitDefaultValue = false)]
		public QueryDateAddExpression DateAdd
		{
			get
			{
				return this.GetExpression<QueryDateAddExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06001525 RID: 5413 RVA: 0x00026858 File Offset: 0x00024A58
		// (set) Token: 0x06001526 RID: 5414 RVA: 0x00026860 File Offset: 0x00024A60
		[DataMember(IsRequired = false, Order = 28, EmitDefaultValue = false)]
		public QueryNowExpression Now
		{
			get
			{
				return this.GetExpression<QueryNowExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06001527 RID: 5415 RVA: 0x00026869 File Offset: 0x00024A69
		// (set) Token: 0x06001528 RID: 5416 RVA: 0x00026871 File Offset: 0x00024A71
		[DataMember(IsRequired = false, Order = 29, EmitDefaultValue = false)]
		public QueryYearAndWeekConstantExpression YearAndWeek
		{
			get
			{
				return this.GetExpression<QueryYearAndWeekConstantExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06001529 RID: 5417 RVA: 0x0002687A File Offset: 0x00024A7A
		// (set) Token: 0x0600152A RID: 5418 RVA: 0x00026882 File Offset: 0x00024A82
		[DataMember(IsRequired = false, Order = 30, EmitDefaultValue = false)]
		public QueryDateSpanExpression DateSpan
		{
			get
			{
				return this.GetExpression<QueryDateSpanExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x0600152B RID: 5419 RVA: 0x0002688B File Offset: 0x00024A8B
		// (set) Token: 0x0600152C RID: 5420 RVA: 0x00026893 File Offset: 0x00024A93
		[DataMember(IsRequired = false, Order = 31, EmitDefaultValue = false)]
		public QueryLiteralExpression Literal
		{
			get
			{
				return this.GetExpression<QueryLiteralExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x0600152D RID: 5421 RVA: 0x0002689C File Offset: 0x00024A9C
		// (set) Token: 0x0600152E RID: 5422 RVA: 0x000268A4 File Offset: 0x00024AA4
		[DataMember(IsRequired = false, Order = 32, EmitDefaultValue = false)]
		public QueryColumnExpression Column
		{
			get
			{
				return this.GetExpression<QueryColumnExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x0600152F RID: 5423 RVA: 0x000268AD File Offset: 0x00024AAD
		// (set) Token: 0x06001530 RID: 5424 RVA: 0x000268B5 File Offset: 0x00024AB5
		[DataMember(IsRequired = false, Order = 33, EmitDefaultValue = false)]
		public QueryMeasureExpression Measure
		{
			get
			{
				return this.GetExpression<QueryMeasureExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001531 RID: 5425 RVA: 0x000268BE File Offset: 0x00024ABE
		// (set) Token: 0x06001532 RID: 5426 RVA: 0x000268C6 File Offset: 0x00024AC6
		[DataMember(IsRequired = false, Order = 34, EmitDefaultValue = false)]
		public QueryPercentileExpression Percentile
		{
			get
			{
				return this.GetExpression<QueryPercentileExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001533 RID: 5427 RVA: 0x000268CF File Offset: 0x00024ACF
		// (set) Token: 0x06001534 RID: 5428 RVA: 0x000268D7 File Offset: 0x00024AD7
		[DataMember(IsRequired = false, Order = 35, EmitDefaultValue = false)]
		public QueryHierarchyExpression Hierarchy
		{
			get
			{
				return this.GetExpression<QueryHierarchyExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001535 RID: 5429 RVA: 0x000268E0 File Offset: 0x00024AE0
		// (set) Token: 0x06001536 RID: 5430 RVA: 0x000268E8 File Offset: 0x00024AE8
		[DataMember(IsRequired = false, Order = 36, EmitDefaultValue = false)]
		public QueryHierarchyLevelExpression HierarchyLevel
		{
			get
			{
				return this.GetExpression<QueryHierarchyLevelExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001537 RID: 5431 RVA: 0x000268F1 File Offset: 0x00024AF1
		// (set) Token: 0x06001538 RID: 5432 RVA: 0x000268F9 File Offset: 0x00024AF9
		[DataMember(IsRequired = false, Order = 37, EmitDefaultValue = false)]
		public QueryDefaultValueExpression DefaultValue
		{
			get
			{
				return this.GetExpression<QueryDefaultValueExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x00026902 File Offset: 0x00024B02
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x0002690A File Offset: 0x00024B0A
		[DataMember(IsRequired = false, Order = 38, EmitDefaultValue = false)]
		public QueryAnyValueExpression AnyValue
		{
			get
			{
				return this.GetExpression<QueryAnyValueExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600153B RID: 5435 RVA: 0x00026913 File Offset: 0x00024B13
		// (set) Token: 0x0600153C RID: 5436 RVA: 0x0002691B File Offset: 0x00024B1B
		[DataMember(IsRequired = false, Order = 39, EmitDefaultValue = false)]
		public QueryPropertyVariationSourceExpression PropertyVariationSource
		{
			get
			{
				return this.GetExpression<QueryPropertyVariationSourceExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x0600153D RID: 5437 RVA: 0x00026924 File Offset: 0x00024B24
		// (set) Token: 0x0600153E RID: 5438 RVA: 0x0002692C File Offset: 0x00024B2C
		[DataMember(IsRequired = false, Order = 40, EmitDefaultValue = false)]
		public QueryArithmeticExpression Arithmetic
		{
			get
			{
				return this.GetExpression<QueryArithmeticExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600153F RID: 5439 RVA: 0x00026935 File Offset: 0x00024B35
		// (set) Token: 0x06001540 RID: 5440 RVA: 0x0002693D File Offset: 0x00024B3D
		[DataMember(IsRequired = false, Order = 41, EmitDefaultValue = false)]
		public QueryScopedEvalExpression ScopedEval
		{
			get
			{
				return this.GetExpression<QueryScopedEvalExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06001541 RID: 5441 RVA: 0x00026946 File Offset: 0x00024B46
		// (set) Token: 0x06001542 RID: 5442 RVA: 0x0002694E File Offset: 0x00024B4E
		[DataMember(IsRequired = false, Order = 42, EmitDefaultValue = false)]
		public QueryTransformTableRefExpression TransformTableRef
		{
			get
			{
				return this.GetExpression<QueryTransformTableRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06001543 RID: 5443 RVA: 0x00026957 File Offset: 0x00024B57
		// (set) Token: 0x06001544 RID: 5444 RVA: 0x0002695F File Offset: 0x00024B5F
		[DataMember(IsRequired = false, Order = 43, EmitDefaultValue = false)]
		public QueryTransformOutputRoleRefExpression TransformOutputRoleRef
		{
			get
			{
				return this.GetExpression<QueryTransformOutputRoleRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06001545 RID: 5445 RVA: 0x00026968 File Offset: 0x00024B68
		// (set) Token: 0x06001546 RID: 5446 RVA: 0x00026970 File Offset: 0x00024B70
		[DataMember(IsRequired = false, Order = 44, EmitDefaultValue = false)]
		public QuerySubqueryExpression Subquery
		{
			get
			{
				return this.GetExpression<QuerySubqueryExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06001547 RID: 5447 RVA: 0x00026979 File Offset: 0x00024B79
		// (set) Token: 0x06001548 RID: 5448 RVA: 0x00026981 File Offset: 0x00024B81
		[DataMember(IsRequired = false, Order = 45, EmitDefaultValue = false)]
		public QueryFloorExpression Floor
		{
			get
			{
				return this.GetExpression<QueryFloorExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06001549 RID: 5449 RVA: 0x0002698A File Offset: 0x00024B8A
		// (set) Token: 0x0600154A RID: 5450 RVA: 0x00026992 File Offset: 0x00024B92
		[DataMember(IsRequired = false, Order = 46, EmitDefaultValue = false)]
		public QueryDiscretizeExpression Discretize
		{
			get
			{
				return this.GetExpression<QueryDiscretizeExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x0600154B RID: 5451 RVA: 0x0002699B File Offset: 0x00024B9B
		// (set) Token: 0x0600154C RID: 5452 RVA: 0x000269A3 File Offset: 0x00024BA3
		[DataMember(IsRequired = false, Order = 48, EmitDefaultValue = false)]
		public QueryMemberExpression Member
		{
			get
			{
				return this.GetExpression<QueryMemberExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x0600154D RID: 5453 RVA: 0x000269AC File Offset: 0x00024BAC
		// (set) Token: 0x0600154E RID: 5454 RVA: 0x000269B4 File Offset: 0x00024BB4
		[DataMember(IsRequired = false, Order = 49, EmitDefaultValue = false)]
		public QueryFilteredEvalExpression FilteredEval
		{
			get
			{
				return this.GetExpression<QueryFilteredEvalExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x0600154F RID: 5455 RVA: 0x000269BD File Offset: 0x00024BBD
		// (set) Token: 0x06001550 RID: 5456 RVA: 0x000269C5 File Offset: 0x00024BC5
		[DataMember(IsRequired = false, Order = 50, EmitDefaultValue = false)]
		public QueryEndsWithExpression EndsWith
		{
			get
			{
				return this.GetExpression<QueryEndsWithExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06001551 RID: 5457 RVA: 0x000269CE File Offset: 0x00024BCE
		// (set) Token: 0x06001552 RID: 5458 RVA: 0x000269D6 File Offset: 0x00024BD6
		[DataMember(IsRequired = false, Order = 51, EmitDefaultValue = false)]
		public QueryNativeFormatExpression NativeFormat
		{
			get
			{
				return this.GetExpression<QueryNativeFormatExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06001553 RID: 5459 RVA: 0x000269DF File Offset: 0x00024BDF
		// (set) Token: 0x06001554 RID: 5460 RVA: 0x000269E7 File Offset: 0x00024BE7
		[DataMember(IsRequired = false, Order = 52, EmitDefaultValue = false)]
		public QueryNativeMeasureExpression NativeMeasure
		{
			get
			{
				return this.GetExpression<QueryNativeMeasureExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06001555 RID: 5461 RVA: 0x000269F0 File Offset: 0x00024BF0
		// (set) Token: 0x06001556 RID: 5462 RVA: 0x000269F8 File Offset: 0x00024BF8
		[DataMember(IsRequired = false, Order = 53, EmitDefaultValue = false)]
		public QueryLetRefExpression LetRef
		{
			get
			{
				return this.GetExpression<QueryLetRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06001557 RID: 5463 RVA: 0x00026A01 File Offset: 0x00024C01
		// (set) Token: 0x06001558 RID: 5464 RVA: 0x00026A09 File Offset: 0x00024C09
		[DataMember(IsRequired = false, Order = 54, EmitDefaultValue = false)]
		public QueryRoleRefExpression RoleRef
		{
			get
			{
				return this.GetExpression<QueryRoleRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06001559 RID: 5465 RVA: 0x00026A12 File Offset: 0x00024C12
		// (set) Token: 0x0600155A RID: 5466 RVA: 0x00026A1A File Offset: 0x00024C1A
		[DataMember(IsRequired = false, Order = 55, EmitDefaultValue = false)]
		public QuerySummaryValueRefExpression SummaryValueRef
		{
			get
			{
				return this.GetExpression<QuerySummaryValueRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x0600155B RID: 5467 RVA: 0x00026A23 File Offset: 0x00024C23
		// (set) Token: 0x0600155C RID: 5468 RVA: 0x00026A2B File Offset: 0x00024C2B
		[DataMember(IsRequired = false, Order = 56, EmitDefaultValue = false)]
		public QueryParameterRefExpression ParameterRef
		{
			get
			{
				return this.GetExpression<QueryParameterRefExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x0600155D RID: 5469 RVA: 0x00026A34 File Offset: 0x00024C34
		// (set) Token: 0x0600155E RID: 5470 RVA: 0x00026A3C File Offset: 0x00024C3C
		[DataMember(IsRequired = false, Order = 57, EmitDefaultValue = false)]
		public QueryTableTypeExpression TableType
		{
			get
			{
				return this.GetExpression<QueryTableTypeExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x0600155F RID: 5471 RVA: 0x00026A45 File Offset: 0x00024C45
		// (set) Token: 0x06001560 RID: 5472 RVA: 0x00026A4D File Offset: 0x00024C4D
		[DataMember(IsRequired = false, Order = 58, EmitDefaultValue = false)]
		public QueryPrimitiveTypeExpression PrimitiveType
		{
			get
			{
				return this.GetExpression<QueryPrimitiveTypeExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06001561 RID: 5473 RVA: 0x00026A56 File Offset: 0x00024C56
		// (set) Token: 0x06001562 RID: 5474 RVA: 0x00026A5E File Offset: 0x00024C5E
		[DataMember(IsRequired = false, Order = 59, EmitDefaultValue = false)]
		public QueryTypeOfExpression TypeOf
		{
			get
			{
				return this.GetExpression<QueryTypeOfExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E4 RID: 1252
		// (get) Token: 0x06001563 RID: 5475 RVA: 0x00026A67 File Offset: 0x00024C67
		// (set) Token: 0x06001564 RID: 5476 RVA: 0x00026A6F File Offset: 0x00024C6F
		[DataMember(IsRequired = false, Order = 60, EmitDefaultValue = false)]
		public QuerySparklineDataExpression SparklineData
		{
			get
			{
				return this.GetExpression<QuerySparklineDataExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06001565 RID: 5477 RVA: 0x00026A78 File Offset: 0x00024C78
		// (set) Token: 0x06001566 RID: 5478 RVA: 0x00026A80 File Offset: 0x00024C80
		[DataMember(IsRequired = false, Order = 61, EmitDefaultValue = false)]
		public QueryNativeVisualCalculationExpression NativeVisualCalculation
		{
			get
			{
				return this.GetExpression<QueryNativeVisualCalculationExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06001567 RID: 5479 RVA: 0x00026A89 File Offset: 0x00024C89
		// (set) Token: 0x06001568 RID: 5480 RVA: 0x00026A91 File Offset: 0x00024C91
		[DataMember(IsRequired = false, Order = 62, EmitDefaultValue = false)]
		public QueryMinExpression Min
		{
			get
			{
				return this.GetExpression<QueryMinExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06001569 RID: 5481 RVA: 0x00026A9A File Offset: 0x00024C9A
		// (set) Token: 0x0600156A RID: 5482 RVA: 0x00026AA2 File Offset: 0x00024CA2
		[DataMember(IsRequired = false, Order = 63, EmitDefaultValue = false)]
		public QueryMaxExpression Max
		{
			get
			{
				return this.GetExpression<QueryMaxExpression>();
			}
			set
			{
				this.SetExpression(value);
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00026AAB File Offset: 0x00024CAB
		// (set) Token: 0x0600156C RID: 5484 RVA: 0x00026AB3 File Offset: 0x00024CB3
		[DataMember(IsRequired = false, Order = 998, EmitDefaultValue = false)]
		public string NativeReferenceName { get; set; }

		// Token: 0x170004E9 RID: 1257
		// (get) Token: 0x0600156D RID: 5485 RVA: 0x00026ABC File Offset: 0x00024CBC
		// (set) Token: 0x0600156E RID: 5486 RVA: 0x00026AC4 File Offset: 0x00024CC4
		[DataMember(IsRequired = false, Order = 999, EmitDefaultValue = false)]
		public string Name { get; set; }

		// Token: 0x170004EA RID: 1258
		// (get) Token: 0x0600156F RID: 5487 RVA: 0x00026ACD File Offset: 0x00024CCD
		public QueryConstantExpression Constant
		{
			get
			{
				return this.GetExpression<QueryConstantExpression>();
			}
		}

		// Token: 0x170004EB RID: 1259
		// (get) Token: 0x06001570 RID: 5488 RVA: 0x00026AD5 File Offset: 0x00024CD5
		public QueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00026ADD File Offset: 0x00024CDD
		public override string ToString()
		{
			return this.ToString(false);
		}

		// Token: 0x06001572 RID: 5490 RVA: 0x00026AE6 File Offset: 0x00024CE6
		public string ToTraceString()
		{
			return this.ToString(true);
		}

		// Token: 0x06001573 RID: 5491 RVA: 0x00026AEF File Offset: 0x00024CEF
		internal void ReplaceExpression(QueryExpression expression)
		{
			this._expression = null;
			this.SetExpression(expression);
		}

		// Token: 0x06001574 RID: 5492 RVA: 0x00026B00 File Offset: 0x00024D00
		internal void WriteQueryString(QueryStringWriter w)
		{
			try
			{
				this._expression.WriteQueryString(w);
				if (w.EmitExpressionNames && !string.IsNullOrEmpty(this.Name))
				{
					w.Write(" as ");
					w.WriteIdentifierCustomerContent(this.Name);
				}
				if (w.EmitExpressionNames && !string.IsNullOrEmpty(this.NativeReferenceName))
				{
					w.Write(" with nativereferencename ");
					w.WriteIdentifierCustomerContent(this.NativeReferenceName);
				}
			}
			catch (Exception ex) when (!ex.IsStoppingException())
			{
				if (w.TraceString)
				{
					w.Write(ex.ToString());
				}
				throw;
			}
		}

		// Token: 0x06001575 RID: 5493 RVA: 0x00026BB8 File Offset: 0x00024DB8
		private string ToString(bool traceString)
		{
			QueryStringWriter queryStringWriter = new QueryStringWriter(false, traceString);
			this.WriteQueryString(queryStringWriter);
			return queryStringWriter.ToString();
		}

		// Token: 0x06001576 RID: 5494 RVA: 0x00026BDA File Offset: 0x00024DDA
		private TExpression GetExpression<TExpression>() where TExpression : QueryExpression
		{
			return this._expression as TExpression;
		}

		// Token: 0x06001577 RID: 5495 RVA: 0x00026BEC File Offset: 0x00024DEC
		private void SetExpression(QueryExpression expression)
		{
			Contract.CheckParam(this._expression == null || this._expression == expression, "expression", "An expression container can only have a single value");
			this._expression = expression;
		}

		// Token: 0x06001578 RID: 5496 RVA: 0x00026C1E File Offset: 0x00024E1E
		public static implicit operator QueryExpressionContainer(QueryExpression value)
		{
			if (!(value == null))
			{
				return new QueryExpressionContainer(value, null, null);
			}
			return null;
		}

		// Token: 0x06001579 RID: 5497 RVA: 0x00026C33 File Offset: 0x00024E33
		public override bool Equals(object obj)
		{
			return this.Equals(obj as QueryExpressionContainer);
		}

		// Token: 0x0600157A RID: 5498 RVA: 0x00026C44 File Offset: 0x00024E44
		public override int GetHashCode()
		{
			if (this.Name != null || this.NativeReferenceName != null)
			{
				return Hashing.CombineHash(Hashing.GetHashCode<string>(this.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(this.NativeReferenceName, ConceptualNameComparer.Instance), Hashing.GetHashCode<QueryExpression>(this.Expression, null));
			}
			return Hashing.GetHashCode<QueryExpression>(this.Expression, null);
		}

		// Token: 0x0600157B RID: 5499 RVA: 0x00026CA0 File Offset: 0x00024EA0
		public bool Equals(QueryExpressionContainer other)
		{
			bool? flag = Util.AreEqual<QueryExpressionContainer>(this, other);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(this.Name, other.Name) && ConceptualNameComparer.Instance.Equals(this.NativeReferenceName, other.NativeReferenceName) && this.Expression == null == (other.Expression == null) && (this.Expression == null || this.Expression.Equals(other.Expression));
		}

		// Token: 0x0600157C RID: 5500 RVA: 0x00026D34 File Offset: 0x00024F34
		public static bool operator ==(QueryExpressionContainer left, QueryExpressionContainer right)
		{
			bool? flag = Util.AreEqual<QueryExpressionContainer>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Equals(right);
		}

		// Token: 0x0600157D RID: 5501 RVA: 0x00026D61 File Offset: 0x00024F61
		public static bool operator !=(QueryExpressionContainer left, QueryExpressionContainer right)
		{
			return !(left == right);
		}

		// Token: 0x04000849 RID: 2121
		private QueryExpression _expression;
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000430 RID: 1072
	internal sealed class SapHanaCubeExpressionVisitor2 : SapHanaCubeExpressionVisitor
	{
		// Token: 0x06002484 RID: 9348 RVA: 0x00067C20 File Offset: 0x00065E20
		public SapHanaCubeExpressionVisitor2(SapHanaCubeBase cube, OdbcQueryDomain queryDomain)
			: base(cube, queryDomain)
		{
			this.cubeQuery = queryDomain.NewQuery(queryDomain.DataSource.GetOrCreateTableInfo(new OdbcIdentifier(null, cube.SchemaName, cube.Identifier.Identifier), null), null);
			Dictionary<string, OdbcQueryColumnInfo> columnInfos = new Dictionary<string, OdbcQueryColumnInfo>();
			for (int i = 0; i < this.cubeQuery.ColumnInfos.Length; i++)
			{
				columnInfos.Add(this.cubeQuery.ColumnInfos[i].LocalName, this.cubeQuery.ColumnInfos[i]);
			}
			this.sqlExpressionGenerator = ((OdbcQueryDomain)this.cubeQuery.QueryDomain).NewQueryExpressionVisitor(this.cubeQuery.Columns.Select((string c) => new SelectItem(new ColumnReference(Alias.NewNativeAlias(c)))).ToArray<SelectItem>(), this.cubeQuery.Columns.Select((string c) => columnInfos[c]).ToArray<OdbcQueryColumnInfo>(), true, null);
			this.queryExpressionGenerator = new SapHanaCubeExpressionVisitor2.CubeExpressionTranslator2(this, this.cubeQuery.Columns, false);
			this.sqlQueryGenerator = new SapHanaCubeExpressionVisitor2.SqlQueryGenerator(this, columnInfos);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00067D54 File Offset: 0x00065F54
		protected override Query CompileQuery(QueryCubeExpression queryExpression, IList<ParameterArguments> variableArguments)
		{
			Set set;
			if (!new SapHanaCubeExpressionVisitor2.HanaSetCompiler(this.cube).TryCompile(queryExpression, out set))
			{
				throw new NotSupportedException();
			}
			SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery = this.sqlQueryGenerator.Visit(set);
			if (sqlQuery == null)
			{
				throw new NotSupportedException();
			}
			sqlQuery = this.ApplyVariableArguments(sqlQuery, variableArguments);
			OdbcQueryColumnInfo[] array = sqlQuery.query.SelectItems.Select((SelectItem s, int i) => sqlQuery.columns[i]).ToArray<OdbcQueryColumnInfo>();
			return new OptimizableQuery(new OdbcQuery(this.queryDomain, array, sqlQuery.query, sqlQuery.tableKeys ?? EmptyArray<TableKey>.Instance, false, SapHanaCubeExpressionVisitor2.GetSortOrder(sqlQuery, array), RowRange.All, null));
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x00067E20 File Offset: 0x00066020
		private SapHanaCubeExpressionVisitor2.SqlQuery ApplyVariableArguments(SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery, IList<ParameterArguments> variableArguments)
		{
			Condition variableFilters = this.GetVariableFilters(variableArguments);
			if (variableFilters != null)
			{
				Condition condition = variableFilters;
				if (sqlQuery.query.WhereClause != null)
				{
					condition = new ConditionOperation(variableFilters, ConditionOperator.And, sqlQuery.query.WhereClause);
				}
				OdbcQuerySpecification odbcQuerySpecification = sqlQuery.query.ShallowCopy();
				odbcQuerySpecification.WhereClause = condition;
				sqlQuery = new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = sqlQuery.columns,
					tableKeys = sqlQuery.tableKeys,
					query = odbcQuerySpecification
				};
			}
			return sqlQuery;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x00067E94 File Offset: 0x00066094
		private Condition GetVariableFilters(IList<ParameterArguments> arguments)
		{
			CubeExpression cubeExpression = null;
			foreach (ParameterArguments parameterArguments in arguments)
			{
				SapHanaParameter sapHanaParameter = this.cube.Parameters[parameterArguments.Parameter.Identifier];
				if (string.IsNullOrEmpty(sapHanaParameter.PlaceholderName) && string.IsNullOrEmpty(sapHanaParameter.ModelElementName))
				{
					throw ValueException.NewDataSourceError<Message1>(Strings.SapHanaSharedColumnVariableErrorTitle(sapHanaParameter.Name), TextValue.New(Strings.SapHanaSharedColumnVariableErrorDetail), null);
				}
				CubeExpression cubeExpression2 = SapHanaCubeExpressionVisitor.VariableFilterCompiler.Compile(sapHanaParameter, parameterArguments.Values);
				cubeExpression = cubeExpression.And(cubeExpression2);
			}
			if (cubeExpression != null)
			{
				QueryExpression queryExpression = this.queryExpressionGenerator.GetQueryExpression(cubeExpression);
				return this.sqlExpressionGenerator.GetCondition(queryExpression).Expression;
			}
			return null;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x00067F6C File Offset: 0x0006616C
		private static TableSortOrder GetSortOrder(SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery, OdbcQueryColumnInfo[] queryColumns)
		{
			if (sqlQuery.query.OrderByClause != null)
			{
				IList<OrderByItem> orderByItems = sqlQuery.query.OrderByClause.OrderByItems;
				SortOrder[] array = new SortOrder[orderByItems.Count];
				for (int i = 0; i < array.Length; i++)
				{
					string name = orderByItems[i].SortColumn.Name.Name;
					int num = SapHanaCubeExpressionVisitor2.IndexOf(queryColumns, name);
					if (num == -1)
					{
						array = null;
						break;
					}
					array[i] = new SortOrder(new TableValue.ColumnSelectorFunctionValue(name, num), null, orderByItems[i].Order == OrderByOption.Ascending);
				}
				if (array != null)
				{
					return new TableSortOrder(array);
				}
			}
			return null;
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x00068010 File Offset: 0x00066210
		private static int IndexOf(OdbcQueryColumnInfo[] columnInfos, string name)
		{
			for (int i = 0; i < columnInfos.Length; i++)
			{
				if (columnInfos[i].LocalName == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04000EC3 RID: 3779
		private static readonly Keys MemberMetadataKeys = Keys.New("Cube.AttributeMemberId");

		// Token: 0x04000EC4 RID: 3780
		private readonly OdbcQuery cubeQuery;

		// Token: 0x04000EC5 RID: 3781
		private SapHanaCubeExpressionVisitor2.CubeExpressionTranslator2 queryExpressionGenerator;

		// Token: 0x04000EC6 RID: 3782
		private OdbcQueryExpressionVisitor sqlExpressionGenerator;

		// Token: 0x04000EC7 RID: 3783
		private SapHanaCubeExpressionVisitor2.SqlQueryGenerator sqlQueryGenerator;

		// Token: 0x02000431 RID: 1073
		private class HanaSetCompiler : SetCompiler
		{
			// Token: 0x0600248B RID: 9355 RVA: 0x0006804F File Offset: 0x0006624F
			public HanaSetCompiler(SapHanaCubeBase cube)
				: base(cube)
			{
			}

			// Token: 0x0600248C RID: 9356 RVA: 0x00068058 File Offset: 0x00066258
			protected override Set NewSelect(Dimensionality visibleDimensionality)
			{
				return new SapHanaCubeExpressionVisitor2.SapHanaSelectSet(visibleDimensionality);
			}
		}

		// Token: 0x02000432 RID: 1074
		private class SapHanaSelectSet : Set
		{
			// Token: 0x0600248D RID: 9357 RVA: 0x00068060 File Offset: 0x00066260
			public SapHanaSelectSet(Dimensionality visibleAxisDimensionality)
				: this(EverythingSet.Instance.ExpandTo(visibleAxisDimensionality), EverythingSet.Instance)
			{
			}

			// Token: 0x0600248E RID: 9358 RVA: 0x00068078 File Offset: 0x00066278
			private SapHanaSelectSet(Set visibleAxis, Set slicerAxis)
			{
				this.visibleAxis = visibleAxis;
				this.slicerAxis = slicerAxis;
			}

			// Token: 0x17000EED RID: 3821
			// (get) Token: 0x0600248F RID: 9359 RVA: 0x0006808E File Offset: 0x0006628E
			public override SetKind Kind
			{
				get
				{
					return SetKind.Other;
				}
			}

			// Token: 0x17000EEE RID: 3822
			// (get) Token: 0x06002490 RID: 9360 RVA: 0x00068092 File Offset: 0x00066292
			public override double Cardinality
			{
				get
				{
					return Math.Min(this.visibleAxis.Cardinality, this.slicerAxis.Cardinality);
				}
			}

			// Token: 0x17000EEF RID: 3823
			// (get) Token: 0x06002491 RID: 9361 RVA: 0x000680AF File Offset: 0x000662AF
			public override Dimensionality Dimensionality
			{
				get
				{
					return this.visibleAxis.Dimensionality;
				}
			}

			// Token: 0x17000EF0 RID: 3824
			// (get) Token: 0x06002492 RID: 9362 RVA: 0x000680BC File Offset: 0x000662BC
			public override bool HasMeasureFilter
			{
				get
				{
					return this.visibleAxis.HasMeasureFilter || this.slicerAxis.HasMeasureFilter;
				}
			}

			// Token: 0x17000EF1 RID: 3825
			// (get) Token: 0x06002493 RID: 9363 RVA: 0x000680D8 File Offset: 0x000662D8
			public Set VisibleAxis
			{
				get
				{
					return this.visibleAxis;
				}
			}

			// Token: 0x17000EF2 RID: 3826
			// (get) Token: 0x06002494 RID: 9364 RVA: 0x000680E0 File Offset: 0x000662E0
			public Set SlicerAxis
			{
				get
				{
					return this.slicerAxis;
				}
			}

			// Token: 0x06002495 RID: 9365 RVA: 0x000680E8 File Offset: 0x000662E8
			public override IEnumerable<Set> GetSubsets()
			{
				foreach (Set set in this.visibleAxis.GetSubsets())
				{
					yield return set;
				}
				IEnumerator<Set> enumerator = null;
				foreach (Set set2 in this.slicerAxis.GetSubsets())
				{
					yield return set2;
				}
				enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06002496 RID: 9366 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set CrossJoinAsLeft(Set other)
			{
				return this;
			}

			// Token: 0x06002497 RID: 9367 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set CrossJoinAsRight(Set other)
			{
				return this;
			}

			// Token: 0x06002498 RID: 9368 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
			public override Set DescendTo(Dimensionality newDimensionality)
			{
				return this;
			}

			// Token: 0x06002499 RID: 9369 RVA: 0x000680F8 File Offset: 0x000662F8
			public override Set IntersectAsLeft(Set other)
			{
				return this.IntersectCommon(other);
			}

			// Token: 0x0600249A RID: 9370 RVA: 0x000680F8 File Offset: 0x000662F8
			public override Set IntersectAsRight(Set other)
			{
				return this.IntersectCommon(other);
			}

			// Token: 0x0600249B RID: 9371 RVA: 0x00068104 File Offset: 0x00066304
			private Set IntersectCommon(Set other)
			{
				Set set = this.visibleAxis;
				Set set2 = this.slicerAxis;
				if (other.HasMeasureFilter)
				{
					if (!other.Dimensionality.Equals(set.Dimensionality))
					{
						throw new NotSupportedException();
					}
					set = set.Intersect(other);
				}
				else
				{
					set2 = set2.Intersect(other);
				}
				return new SapHanaCubeExpressionVisitor2.SapHanaSelectSet(set, set2);
			}

			// Token: 0x0600249C RID: 9372 RVA: 0x00068159 File Offset: 0x00066359
			public override Set EnsureUniqueHierarchyMembers()
			{
				return new SapHanaCubeExpressionVisitor2.SapHanaSelectSet(this.visibleAxis.EnsureUniqueHierarchyMembers(), this.slicerAxis.EnsureUniqueHierarchyMembers());
			}

			// Token: 0x0600249D RID: 9373 RVA: 0x00068176 File Offset: 0x00066376
			public override Set Unordered()
			{
				return new SapHanaCubeExpressionVisitor2.SapHanaSelectSet(this.visibleAxis.Unordered(), this.slicerAxis.Unordered());
			}

			// Token: 0x0600249E RID: 9374 RVA: 0x000033E7 File Offset: 0x000015E7
			public override Set NewScope(string scope)
			{
				throw new NotSupportedException();
			}

			// Token: 0x0600249F RID: 9375 RVA: 0x00068193 File Offset: 0x00066393
			public bool Equals(SapHanaCubeExpressionVisitor2.SapHanaSelectSet other)
			{
				return other != null && this.visibleAxis.Equals(other.visibleAxis) && this.slicerAxis.Equals(other.slicerAxis);
			}

			// Token: 0x060024A0 RID: 9376 RVA: 0x000681BE File Offset: 0x000663BE
			public override bool Equals(object other)
			{
				return this.Equals(other as SapHanaCubeExpressionVisitor2.SapHanaSelectSet);
			}

			// Token: 0x060024A1 RID: 9377 RVA: 0x000681CC File Offset: 0x000663CC
			public override int GetHashCode()
			{
				return this.visibleAxis.GetHashCode() + 5011 * this.slicerAxis.GetHashCode();
			}

			// Token: 0x04000EC8 RID: 3784
			private readonly Set visibleAxis;

			// Token: 0x04000EC9 RID: 3785
			private readonly Set slicerAxis;
		}

		// Token: 0x02000434 RID: 1076
		private class SqlQuery
		{
			// Token: 0x04000ECF RID: 3791
			public IList<OdbcQueryColumnInfo> columns;

			// Token: 0x04000ED0 RID: 3792
			public TableKey[] tableKeys;

			// Token: 0x04000ED1 RID: 3793
			public OdbcQuerySpecification query;
		}

		// Token: 0x02000435 RID: 1077
		private class SqlQueryGenerator : SetVisitor<SapHanaCubeExpressionVisitor2.SqlQuery, Condition, OrderByItem>
		{
			// Token: 0x060024AD RID: 9389 RVA: 0x000683FF File Offset: 0x000665FF
			public SqlQueryGenerator(SapHanaCubeExpressionVisitor2 visitor, Dictionary<string, OdbcQueryColumnInfo> columnInfos)
			{
				this.visitor = visitor;
				this.columnInfos = columnInfos;
			}

			// Token: 0x060024AE RID: 9390 RVA: 0x00068418 File Offset: 0x00066618
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewCrossJoin(SapHanaCubeExpressionVisitor2.SqlQuery[] sets)
			{
				List<OdbcQueryColumnInfo> list = new List<OdbcQueryColumnInfo>(sets.Length);
				List<SelectItem> list2 = new List<SelectItem>(sets.Length);
				GroupByClause groupByClause = new GroupByClause();
				Condition condition = null;
				IList<FromItem> list3 = null;
				for (int i = 0; i < sets.Length; i++)
				{
					list.AddRange(sets[i].columns);
					list2.AddRange(sets[i].query.SelectItems);
					SapHanaCubeExpressionVisitor2.SqlQueryGenerator.AddRange<GroupByItem>(groupByClause.Items, sets[i].query.GroupByClause.Items);
					condition = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.And(condition, sets[i].query.WhereClause);
					list3 = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(list3, sets[i].query.FromItems);
				}
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = list,
					query = new OdbcQuerySpecification
					{
						SelectItems = list2,
						WhereClause = condition,
						FromItems = list3,
						GroupByClause = groupByClause
					}
				};
			}

			// Token: 0x060024AF RID: 9391 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewDescendTo(SapHanaCubeExpressionVisitor2.SqlQuery set, Dimensionality from, Dimensionality to)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060024B0 RID: 9392 RVA: 0x000033E7 File Offset: 0x000015E7
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewDistinct(SapHanaCubeExpressionVisitor2.SqlQuery set)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060024B1 RID: 9393 RVA: 0x000684F8 File Offset: 0x000666F8
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewEverything()
			{
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = EmptyArray<OdbcQueryColumnInfo>.Instance,
					query = new OdbcQuerySpecification
					{
						SelectItems = EmptyArray<SelectItem>.Instance,
						FromItems = new FromItem[] { this.NewFrom() },
						GroupByClause = new GroupByClause()
					}
				};
			}

			// Token: 0x060024B2 RID: 9394 RVA: 0x00068550 File Offset: 0x00066750
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewExcept(SapHanaCubeExpressionVisitor2.SqlQuery set, SapHanaCubeExpressionVisitor2.SqlQuery except)
			{
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set.columns, except.columns),
					query = new OdbcQuerySpecification
					{
						SelectItems = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set.query.SelectItems, except.query.SelectItems),
						WhereClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.And(set.query.WhereClause, SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Not(except.query.WhereClause)),
						FromItems = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set.query.FromItems, except.query.FromItems),
						GroupByClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set.query.GroupByClause, except.query.GroupByClause)
					}
				};
			}

			// Token: 0x060024B3 RID: 9395 RVA: 0x00068610 File Offset: 0x00066810
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewFilter(SapHanaCubeExpressionVisitor2.SqlQuery set, Condition filter)
			{
				OdbcQuerySpecification odbcQuerySpecification = set.query.ShallowCopy();
				odbcQuerySpecification.WhereClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.And(odbcQuerySpecification.WhereClause, filter);
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = set.columns,
					tableKeys = set.tableKeys,
					query = odbcQuerySpecification
				};
			}

			// Token: 0x060024B4 RID: 9396 RVA: 0x00068660 File Offset: 0x00066860
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewIntersect(SapHanaCubeExpressionVisitor2.SqlQuery set1, SapHanaCubeExpressionVisitor2.SqlQuery set2)
			{
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set1.columns, set2.columns),
					query = new OdbcQuerySpecification
					{
						SelectItems = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set1.query.SelectItems, set2.query.SelectItems),
						WhereClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.And(set1.query.WhereClause, set2.query.WhereClause),
						FromItems = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set1.query.FromItems, set2.query.FromItems),
						GroupByClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(set1.query.GroupByClause, set2.query.GroupByClause)
					}
				};
			}

			// Token: 0x060024B5 RID: 9397 RVA: 0x00068718 File Offset: 0x00066918
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewLevel(ICubeLevel level)
			{
				SapHanaDimensionAttribute sapHanaDimensionAttribute = (SapHanaDimensionAttribute)level;
				DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute = sapHanaDimensionAttribute as DynamicSapHanaDimensionAttribute;
				if (dynamicSapHanaDimensionAttribute != null)
				{
					Alias alias = Alias.NewNativeAlias(dynamicSapHanaDimensionAttribute.Name);
					QueryExpression queryExpression = this.visitor.queryExpressionGenerator.GetQueryExpression(dynamicSapHanaDimensionAttribute.Expression);
					OdbcSqlExpression odbcSqlExpression = this.visitor.sqlExpressionGenerator.Visit(queryExpression);
					OdbcScalarExpression odbcScalarExpression;
					if (odbcSqlExpression.Kind == OdbcSqlExpressionKind.Condition)
					{
						odbcScalarExpression = new OdbcScalarExpression(this.visitor.sqlExpressionGenerator.NewColumnType(Odbc32.SQL_TYPE.BIT, false), odbcSqlExpression.AsCondition.Expression);
					}
					else
					{
						odbcScalarExpression = odbcSqlExpression.AsScalar;
					}
					SqlExpression expression = odbcScalarExpression.Expression;
					GroupByClause groupByClause = new GroupByClause();
					groupByClause.Items.Add(new GroupByItem
					{
						Expression = expression
					});
					return new SapHanaCubeExpressionVisitor2.SqlQuery
					{
						columns = new OdbcQueryColumnInfo[]
						{
							new OdbcQueryColumnInfo(dynamicSapHanaDimensionAttribute.Name, dynamicSapHanaDimensionAttribute.TypeValue, odbcScalarExpression.TypeInfo)
						},
						query = new OdbcQuerySpecification
						{
							SelectItems = new SelectItem[]
							{
								new SelectItem(expression, alias)
							},
							FromItems = new FromItem[] { this.NewFrom() },
							GroupByClause = groupByClause
						}
					};
				}
				ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = (ColumnSapHanaDimensionAttribute)sapHanaDimensionAttribute;
				Alias alias2 = Alias.NewNativeAlias(columnSapHanaDimensionAttribute.Column.Name);
				Alias alias3 = Alias.NewNativeAlias(columnSapHanaDimensionAttribute.CaptionColumn.Name);
				Alias alias4 = Alias.NewNativeAlias(sapHanaDimensionAttribute.Name);
				GroupByClause groupByClause2 = new GroupByClause();
				groupByClause2.Items.Add(new GroupByItem
				{
					Expression = new ColumnReference(alias2)
				});
				if (!alias3.Equals(alias2))
				{
					groupByClause2.Items.Add(new GroupByItem
					{
						Expression = new ColumnReference(alias3)
					});
				}
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = new OdbcQueryColumnInfo[] { this.NewColumnInfo(columnSapHanaDimensionAttribute.CaptionColumn.Name, sapHanaDimensionAttribute.Name) },
					query = new OdbcQuerySpecification
					{
						SelectItems = new SelectItem[] { SapHanaCubeExpressionVisitor2.SqlQueryGenerator.NewSelectColumn(alias3, alias4) },
						FromItems = new FromItem[] { this.NewFrom() },
						GroupByClause = groupByClause2
					}
				};
			}

			// Token: 0x060024B6 RID: 9398 RVA: 0x00068950 File Offset: 0x00066B50
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewMember(ICubeLevel level, Value member)
			{
				SapHanaDimensionAttribute sapHanaDimensionAttribute = (SapHanaDimensionAttribute)level;
				DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute = sapHanaDimensionAttribute as DynamicSapHanaDimensionAttribute;
				SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery = this.NewLevel(level);
				sqlQuery.query.WhereClause = this.VisitFilter(sqlQuery, new BinaryCubeExpression(BinaryOperator2.Equals, (dynamicSapHanaDimensionAttribute != null) ? dynamicSapHanaDimensionAttribute.Expression : new IdentifierCubeExpression(((ColumnSapHanaDimensionAttribute)sapHanaDimensionAttribute).Column.Name), new ConstantCubeExpression(member)));
				return sqlQuery;
			}

			// Token: 0x060024B7 RID: 9399 RVA: 0x000689B4 File Offset: 0x00066BB4
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewOrderBy(SapHanaCubeExpressionVisitor2.SqlQuery set, OrderByItem[] order)
			{
				OdbcQuerySpecification odbcQuerySpecification = set.query.ShallowCopy();
				odbcQuerySpecification.OrderByClause = new OrderByClause();
				for (int i = 0; i < order.Length; i++)
				{
					odbcQuerySpecification.OrderByClause.OrderByItems.Add(order[i]);
				}
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = set.columns,
					tableKeys = set.tableKeys,
					query = odbcQuerySpecification
				};
			}

			// Token: 0x060024B8 RID: 9400 RVA: 0x0000A6A5 File Offset: 0x000088A5
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewOrderHierarchies(SapHanaCubeExpressionVisitor2.SqlQuery set, Dimensionality from, Dimensionality to)
			{
				return set;
			}

			// Token: 0x060024B9 RID: 9401 RVA: 0x00068A20 File Offset: 0x00066C20
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewProject(SapHanaCubeExpressionVisitor2.SqlQuery set, IEnumerable<ICubeObject> objects)
			{
				List<OdbcQueryColumnInfo> list = new List<OdbcQueryColumnInfo>(set.columns);
				List<SelectItem> list2 = new List<SelectItem>(set.query.SelectItems);
				foreach (ICubeObject cubeObject in objects)
				{
					CubeObjectKind kind = cubeObject.Kind;
					if (kind != CubeObjectKind.Property)
					{
						if (kind != CubeObjectKind.Measure)
						{
							throw new NotSupportedException();
						}
						SapHanaMeasure sapHanaMeasure = (SapHanaMeasure)cubeObject;
						QueryExpression measureExpr = this.visitor.GetMeasureExpr(this.visitor.cubeQuery.Columns, sapHanaMeasure);
						SqlExpression expression = this.visitor.sqlExpressionGenerator.Visit(measureExpr).AsScalar.Expression;
						list.Add(this.NewColumnInfo(sapHanaMeasure.Column.Name, sapHanaMeasure.Name));
						list2.Add(new SelectItem(expression, Alias.NewNativeAlias(sapHanaMeasure.Name)));
					}
					else
					{
						SapHanaProperty sapHanaProperty = (SapHanaProperty)cubeObject;
						list.Add(this.NewColumnInfo(sapHanaProperty.Column.Name, sapHanaProperty.Name));
						list2.Add(SapHanaCubeExpressionVisitor2.SqlQueryGenerator.NewSelectColumn(Alias.NewNativeAlias(sapHanaProperty.Column.Name), Alias.NewNativeAlias(sapHanaProperty.Name)));
					}
				}
				OdbcQuerySpecification odbcQuerySpecification = set.query.ShallowCopy();
				odbcQuerySpecification.SelectItems = list2;
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = list,
					tableKeys = set.tableKeys,
					query = odbcQuerySpecification
				};
			}

			// Token: 0x060024BA RID: 9402 RVA: 0x00068BB4 File Offset: 0x00066DB4
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewSkipTake(SapHanaCubeExpressionVisitor2.SqlQuery set, RowRange rowRange)
			{
				OdbcLimitClause odbcLimitClause;
				RowRange rowRange2;
				if (rowRange.SkipCount.IsZero && !rowRange.TakeCount.IsZero && this.visitor.queryDomain.SqlExpressionGenerator.TryGetLimitClause(rowRange, out odbcLimitClause, out rowRange2) && rowRange2.IsAll)
				{
					OdbcQuerySpecification odbcQuerySpecification = set.query.ShallowCopy();
					odbcQuerySpecification.LimitClause = odbcLimitClause;
					return new SapHanaCubeExpressionVisitor2.SqlQuery
					{
						columns = set.columns,
						tableKeys = set.tableKeys,
						query = odbcQuerySpecification
					};
				}
				throw new NotSupportedException();
			}

			// Token: 0x060024BB RID: 9403 RVA: 0x00068C48 File Offset: 0x00066E48
			protected override SapHanaCubeExpressionVisitor2.SqlQuery VisitUnion(UnionSet union)
			{
				Dimensionality dimensionality = null;
				SapHanaDimensionAttribute sapHanaDimensionAttribute = null;
				SapHanaColumn sapHanaColumn = null;
				List<IValueReference> list = null;
				IValueReference valueReference = null;
				Set set = EverythingSet.Instance;
				foreach (Set set2 in union.Sets)
				{
					SapHanaDimensionAttribute sapHanaDimensionAttribute2 = null;
					SapHanaColumn sapHanaColumn2 = null;
					IValueReference valueReference2 = null;
					IValueReference valueReference3 = null;
					SetKind kind = set2.Kind;
					CubeExpression cubeExpression;
					ConstantCubeExpression constantCubeExpression;
					if (kind != SetKind.Filter)
					{
						if (kind == SetKind.Member)
						{
							MemberSet memberSet = (MemberSet)set2;
							sapHanaDimensionAttribute2 = (SapHanaDimensionAttribute)memberSet.Level;
							ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute = sapHanaDimensionAttribute2 as ColumnSapHanaDimensionAttribute;
							sapHanaColumn2 = ((columnSapHanaDimensionAttribute != null) ? columnSapHanaDimensionAttribute.Column : null);
							valueReference2 = memberSet.Member;
						}
					}
					else if (((FilterSet)set2).Predicate.TryGetConstantFilter(out cubeExpression, out constantCubeExpression))
					{
						if (this.TryGetAttributeReference(cubeExpression, out sapHanaDimensionAttribute2))
						{
							ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute2 = sapHanaDimensionAttribute2 as ColumnSapHanaDimensionAttribute;
							sapHanaColumn2 = ((columnSapHanaDimensionAttribute2 != null) ? columnSapHanaDimensionAttribute2.CaptionColumn : null);
						}
						else if (cubeExpression.Kind == CubeExpressionKind.Invocation)
						{
							InvocationCubeExpression invocationCubeExpression = (InvocationCubeExpression)cubeExpression;
							if (invocationCubeExpression.Function.Kind == CubeExpressionKind.Constant && invocationCubeExpression.Arguments.Count == 1 && this.TryGetAttributeReference(invocationCubeExpression.Arguments[0], out sapHanaDimensionAttribute2))
							{
								valueReference3 = ((ConstantCubeExpression)invocationCubeExpression.Function).Value;
								ColumnSapHanaDimensionAttribute columnSapHanaDimensionAttribute3 = sapHanaDimensionAttribute2 as ColumnSapHanaDimensionAttribute;
								sapHanaColumn2 = ((columnSapHanaDimensionAttribute3 != null) ? columnSapHanaDimensionAttribute3.Column : null);
							}
						}
						valueReference2 = constantCubeExpression.Value;
					}
					bool flag = false;
					if (sapHanaColumn2 != null && sapHanaDimensionAttribute2 != null && valueReference2 != null)
					{
						flag = true;
						if (valueReference3 != null)
						{
							if (valueReference == null)
							{
								if (list != null)
								{
									flag = false;
								}
								else
								{
									valueReference = valueReference3;
								}
							}
							else if (valueReference3 != valueReference)
							{
								flag = false;
							}
						}
						else if (valueReference != null)
						{
							flag = false;
						}
						if ((dimensionality != null && !dimensionality.Equals(set2.Dimensionality)) || (sapHanaColumn != null && !sapHanaColumn.Equals(sapHanaColumn2)))
						{
							throw new NotSupportedException();
						}
						if (list == null)
						{
							list = new List<IValueReference>(union.Sets.Length);
						}
						if (dimensionality == null)
						{
							dimensionality = set2.Dimensionality;
							sapHanaDimensionAttribute = sapHanaDimensionAttribute2;
							sapHanaColumn = sapHanaColumn2;
						}
					}
					if (flag)
					{
						list.Add(valueReference2);
					}
					else
					{
						set = set.Union(set2);
					}
				}
				if (list != null && list.Count > 0)
				{
					LevelSet levelSet = new LevelSet(sapHanaDimensionAttribute);
					CubeExpression cubeExpression2 = new IdentifierCubeExpression(sapHanaColumn.Name);
					if (valueReference != null)
					{
						cubeExpression2 = new InvocationCubeExpression(new ConstantCubeExpression(valueReference.Value), new CubeExpression[] { cubeExpression2 });
					}
					CubeExpression cubeExpression3 = new InvocationCubeExpression(new ConstantCubeExpression(Library.List.Contains), new CubeExpression[]
					{
						new ConstantCubeExpression(ListValue.New(list)),
						cubeExpression2
					});
					Set set3 = FilterSet.New(levelSet, levelSet.Dimensionality, cubeExpression3, false);
					Set set4 = EverythingSet.Instance.Union(set3);
					set4 = set4.Union(set);
					return this.Visit(set4);
				}
				return base.VisitUnion(union);
			}

			// Token: 0x060024BC RID: 9404 RVA: 0x00068EE4 File Offset: 0x000670E4
			protected override SapHanaCubeExpressionVisitor2.SqlQuery NewUnion(SapHanaCubeExpressionVisitor2.SqlQuery[] sets)
			{
				IList<OdbcQueryColumnInfo> list = null;
				IList<SelectItem> list2 = null;
				Condition condition = null;
				IList<FromItem> list3 = null;
				GroupByClause groupByClause = null;
				for (int i = 0; i < sets.Length; i++)
				{
					list = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(list, sets[i].columns);
					list2 = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(list2, sets[i].query.SelectItems);
					condition = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Or(condition, sets[i].query.WhereClause);
					list3 = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(list3, sets[i].query.FromItems);
					groupByClause = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(groupByClause, sets[i].query.GroupByClause);
				}
				return new SapHanaCubeExpressionVisitor2.SqlQuery
				{
					columns = list,
					query = new OdbcQuerySpecification
					{
						SelectItems = list2,
						WhereClause = condition,
						FromItems = list3,
						GroupByClause = groupByClause
					}
				};
			}

			// Token: 0x060024BD RID: 9405 RVA: 0x00068FA8 File Offset: 0x000671A8
			protected override Condition VisitFilter(SapHanaCubeExpressionVisitor2.SqlQuery set, CubeExpression filter)
			{
				QueryExpression queryExpression = this.visitor.queryExpressionGenerator.GetQueryExpression(filter);
				return this.visitor.sqlExpressionGenerator.GetCondition(queryExpression).Expression;
			}

			// Token: 0x060024BE RID: 9406 RVA: 0x00068FE0 File Offset: 0x000671E0
			protected override OrderByItem VisitOrder(SapHanaCubeExpressionVisitor2.SqlQuery set, CubeSortOrder order)
			{
				IdentifierCubeExpression identifierCubeExpression = order.Expression as IdentifierCubeExpression;
				if (identifierCubeExpression != null)
				{
					return new OrderByItem(new ColumnReference(Alias.NewNativeAlias(identifierCubeExpression.Identifier)), order.Ascending ? OrderByOption.Ascending : OrderByOption.Descending);
				}
				throw new NotSupportedException();
			}

			// Token: 0x060024BF RID: 9407 RVA: 0x00069024 File Offset: 0x00067224
			protected override SapHanaCubeExpressionVisitor2.SqlQuery VisitOther(Set set)
			{
				SapHanaCubeExpressionVisitor2.SapHanaSelectSet sapHanaSelectSet = set as SapHanaCubeExpressionVisitor2.SapHanaSelectSet;
				if (sapHanaSelectSet != null)
				{
					return this.NewSelect(this.Visit(sapHanaSelectSet.VisibleAxis), this.Visit(sapHanaSelectSet.SlicerAxis));
				}
				throw new NotSupportedException();
			}

			// Token: 0x060024C0 RID: 9408 RVA: 0x0006905F File Offset: 0x0006725F
			private bool TryGetAttributeReference(CubeExpression expression, out SapHanaDimensionAttribute attribute)
			{
				attribute = null;
				return expression.Kind == CubeExpressionKind.Identifier && this.visitor.cube.Attributes.TryGetValue(((IdentifierCubeExpression)expression).Identifier, out attribute);
			}

			// Token: 0x060024C1 RID: 9409 RVA: 0x00069090 File Offset: 0x00067290
			private SapHanaCubeExpressionVisitor2.SqlQuery NewSelect(SapHanaCubeExpressionVisitor2.SqlQuery visibleAxis, SapHanaCubeExpressionVisitor2.SqlQuery slicerAxis)
			{
				HashSet<Alias> hashSet = new HashSet<Alias>();
				HashSet<Alias> hashSet2 = new HashSet<Alias>();
				ArrayBuilder<int> arrayBuilder = default(ArrayBuilder<int>);
				foreach (GroupByItem groupByItem in visibleAxis.query.GroupByClause.Items)
				{
					ColumnReference columnReference = groupByItem.Expression as ColumnReference;
					if (columnReference == null)
					{
						hashSet = null;
						break;
					}
					hashSet.Add(columnReference.Name);
				}
				if (hashSet != null)
				{
					for (int i = 0; i < visibleAxis.query.SelectItems.Count; i++)
					{
						ColumnReference columnReference2 = visibleAxis.query.SelectItems[i].Expression as ColumnReference;
						if (columnReference2 != null && hashSet.Contains(columnReference2.Name))
						{
							hashSet2.Add(columnReference2.Name);
							arrayBuilder.Add(i);
						}
					}
				}
				SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery = new SapHanaCubeExpressionVisitor2.SqlQuery();
				sqlQuery.columns = visibleAxis.columns;
				SapHanaCubeExpressionVisitor2.SqlQuery sqlQuery2 = sqlQuery;
				object obj;
				if (hashSet == null || hashSet2.Count != hashSet.Count)
				{
					obj = null;
				}
				else
				{
					(obj = new TableKey[1])[0] = new TableKey(arrayBuilder.ToArray(), false);
				}
				sqlQuery2.tableKeys = obj;
				sqlQuery.query = new OdbcQuerySpecification
				{
					SelectItems = visibleAxis.query.SelectItems,
					FromItems = SapHanaCubeExpressionVisitor2.SqlQueryGenerator.Union(visibleAxis.query.FromItems, slicerAxis.query.FromItems),
					WhereClause = slicerAxis.query.WhereClause,
					GroupByClause = ((visibleAxis.query.GroupByClause.Items.Count > 0) ? visibleAxis.query.GroupByClause : null),
					HavingClause = visibleAxis.query.WhereClause
				};
				return sqlQuery;
			}

			// Token: 0x060024C2 RID: 9410 RVA: 0x00069254 File Offset: 0x00067454
			private FromItem NewFrom()
			{
				return this.visitor.NewFrom(this.visitor.cube.SchemaName, this.visitor.cube.ViewName);
			}

			// Token: 0x060024C3 RID: 9411 RVA: 0x00069284 File Offset: 0x00067484
			private OdbcQueryColumnInfo NewColumnInfo(string columnName, string alias)
			{
				OdbcQueryColumnInfo odbcQueryColumnInfo = this.columnInfos[columnName];
				if (alias != columnName)
				{
					odbcQueryColumnInfo = new OdbcQueryColumnInfo(alias, odbcQueryColumnInfo.AscribedTypeValue, odbcQueryColumnInfo.TypeInfo);
				}
				return odbcQueryColumnInfo;
			}

			// Token: 0x060024C4 RID: 9412 RVA: 0x000692BC File Offset: 0x000674BC
			private static void AddRange<T>(IList<T> list, IEnumerable<T> items)
			{
				foreach (T t in items)
				{
					list.Add(t);
				}
			}

			// Token: 0x060024C5 RID: 9413 RVA: 0x00069304 File Offset: 0x00067504
			private static SelectItem NewSelectColumn(Alias columnName, Alias alias)
			{
				ColumnReference columnReference = new ColumnReference(columnName);
				if (alias != null && !columnName.Equals(alias))
				{
					return new SelectItem(columnReference, alias);
				}
				return new SelectItem(columnReference);
			}

			// Token: 0x060024C6 RID: 9414 RVA: 0x00069334 File Offset: 0x00067534
			[Conditional("DEBUG")]
			private static void AssertSimpleQuery(SapHanaCubeExpressionVisitor2.SqlQuery query)
			{
				if (query.query.FromItems.Count == 1)
				{
					if (!query.query.SelectItems.Any((SelectItem s) => !(s.Expression is ColumnReference) && !(s.Expression is ScalarExpression)))
					{
						if (query.query.GroupByClause != null)
						{
							if (query.query.GroupByClause.Items.Any((GroupByItem s) => !(s.Expression is ColumnReference) && !(s.Expression is ScalarExpression)))
							{
								goto IL_00B1;
							}
						}
						if (query.query.HavingClause == null && query.query.OrderByClause == null && query.query.LimitClause == null)
						{
							return;
						}
					}
				}
				IL_00B1:
				throw new InvalidOperationException();
			}

			// Token: 0x060024C7 RID: 9415 RVA: 0x000693F8 File Offset: 0x000675F8
			private static IList<OdbcQueryColumnInfo> Union(IList<OdbcQueryColumnInfo> left, IList<OdbcQueryColumnInfo> right)
			{
				if (left == null)
				{
					return right;
				}
				return left;
			}

			// Token: 0x060024C8 RID: 9416 RVA: 0x000693F8 File Offset: 0x000675F8
			private static IList<SelectItem> Union(IList<SelectItem> left, IList<SelectItem> right)
			{
				if (left == null)
				{
					return right;
				}
				return left;
			}

			// Token: 0x060024C9 RID: 9417 RVA: 0x00069402 File Offset: 0x00067602
			private static IList<FromItem> Union(IList<FromItem> left, IList<FromItem> right)
			{
				return left ?? right;
			}

			// Token: 0x060024CA RID: 9418 RVA: 0x000693F8 File Offset: 0x000675F8
			private static GroupByClause Union(GroupByClause left, GroupByClause right)
			{
				if (left == null)
				{
					return right;
				}
				return left;
			}

			// Token: 0x060024CB RID: 9419 RVA: 0x0006940A File Offset: 0x0006760A
			private static Condition And(Condition left, Condition right)
			{
				if (left == null)
				{
					return right;
				}
				if (right == null)
				{
					return left;
				}
				return new ConditionOperation(left, ConditionOperator.And, right);
			}

			// Token: 0x060024CC RID: 9420 RVA: 0x0006941E File Offset: 0x0006761E
			private static Condition Or(Condition left, Condition right)
			{
				if (left == null)
				{
					return right;
				}
				if (right == null)
				{
					return left;
				}
				return new ConditionOperation(left, ConditionOperator.Or, right);
			}

			// Token: 0x060024CD RID: 9421 RVA: 0x00069432 File Offset: 0x00067632
			private static Condition Not(Condition operand)
			{
				if (operand == null)
				{
					return null;
				}
				return new ConditionOperation(ConditionOperator.Not, operand);
			}

			// Token: 0x060024CE RID: 9422 RVA: 0x00069440 File Offset: 0x00067640
			private static bool Equal(Alias left, Alias right)
			{
				return (left == null && right == null) || (left != null && left.Equals(right));
			}

			// Token: 0x04000ED2 RID: 3794
			private readonly SapHanaCubeExpressionVisitor2 visitor;

			// Token: 0x04000ED3 RID: 3795
			private readonly Dictionary<string, OdbcQueryColumnInfo> columnInfos;
		}

		// Token: 0x02000437 RID: 1079
		private class CubeExpressionTranslator2 : SapHanaCubeExpressionVisitor.CubeExpressionTranslator
		{
			// Token: 0x060024D3 RID: 9427 RVA: 0x000694A6 File Offset: 0x000676A6
			public CubeExpressionTranslator2(SapHanaCubeExpressionVisitor2 visitor, Keys queryColumns, bool treatMeasureAsColumnAccess = false)
				: base(visitor, queryColumns)
			{
			}

			// Token: 0x17000EF5 RID: 3829
			// (get) Token: 0x060024D4 RID: 9428 RVA: 0x000694B0 File Offset: 0x000676B0
			private SapHanaCubeExpressionVisitor2 Visitor
			{
				get
				{
					return (SapHanaCubeExpressionVisitor2)this.visitor;
				}
			}

			// Token: 0x060024D5 RID: 9429 RVA: 0x000694C0 File Offset: 0x000676C0
			protected override QueryExpression NewIdentifier(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				if (this.Visitor.cube.TryGetObject(identifier, out cubeObject))
				{
					switch (cubeObject.Kind)
					{
					case CubeObjectKind.DimensionAttribute:
					{
						SapHanaDimensionAttribute sapHanaDimensionAttribute = (SapHanaDimensionAttribute)cubeObject;
						DynamicSapHanaDimensionAttribute dynamicSapHanaDimensionAttribute = sapHanaDimensionAttribute as DynamicSapHanaDimensionAttribute;
						if (dynamicSapHanaDimensionAttribute == null)
						{
							return base.NewIdentifier(new IdentifierCubeExpression(((ColumnSapHanaDimensionAttribute)sapHanaDimensionAttribute).CaptionColumn.Name));
						}
						return this.Visitor.queryExpressionGenerator.GetQueryExpression(dynamicSapHanaDimensionAttribute.Expression);
					}
					case CubeObjectKind.Property:
					{
						SapHanaProperty sapHanaProperty = (SapHanaProperty)cubeObject;
						return base.NewIdentifier(new IdentifierCubeExpression(sapHanaProperty.Column.Name));
					}
					case CubeObjectKind.Measure:
					{
						SapHanaMeasure sapHanaMeasure = (SapHanaMeasure)cubeObject;
						return this.Visitor.GetMeasureExpr(this.queryColumns, sapHanaMeasure);
					}
					}
				}
				return base.NewIdentifier(identifier);
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Capability;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Cube
{
	// Token: 0x02000D6C RID: 3436
	internal class SetContextQuery : DataSourceQuery
	{
		// Token: 0x06005D51 RID: 23889 RVA: 0x00142DD7 File Offset: 0x00140FD7
		public SetContextQuery(SetContextProvider provider, IList<ParameterArguments> arguments, ICube cube, Set set, Projection projection, IList<TableKey> tableKeys)
			: this(provider, arguments, cube, set, projection, tableKeys, null)
		{
		}

		// Token: 0x06005D52 RID: 23890 RVA: 0x00142DE9 File Offset: 0x00140FE9
		public SetContextQuery(SetContextProvider provider, IList<ParameterArguments> arguments, ICube cube, Set set, Projection projection, Query query)
			: this(provider, arguments, cube, set, projection, null, query)
		{
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00142DFC File Offset: 0x00140FFC
		private SetContextQuery(SetContextProvider provider, IList<ParameterArguments> arguments, ICube cube, Set set, Projection projection, IList<TableKey> tableKeys, Query query)
		{
			this.provider = provider;
			this.arguments = arguments;
			this.cube = cube;
			this.set = set;
			this.projection = projection;
			this.tableKeys = tableKeys;
			this.query = query;
			if (this.query == null && this.Context == null)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06005D54 RID: 23892 RVA: 0x00142E5A File Offset: 0x0014105A
		private SetContextQuery New(ICube cube, Set set, Projection projection, Query query)
		{
			return new SetContextQuery(this.provider, this.arguments, cube, set, projection, query);
		}

		// Token: 0x17001B8E RID: 7054
		// (get) Token: 0x06005D55 RID: 23893 RVA: 0x00142E72 File Offset: 0x00141072
		public SetContextProvider Provider
		{
			get
			{
				return this.provider;
			}
		}

		// Token: 0x17001B8F RID: 7055
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x00142E7A File Offset: 0x0014107A
		public IList<ParameterArguments> Arguments
		{
			get
			{
				return this.arguments;
			}
		}

		// Token: 0x17001B90 RID: 7056
		// (get) Token: 0x06005D57 RID: 23895 RVA: 0x00142E82 File Offset: 0x00141082
		public ICube Cube
		{
			get
			{
				return this.cube;
			}
		}

		// Token: 0x17001B91 RID: 7057
		// (get) Token: 0x06005D58 RID: 23896 RVA: 0x00142E8A File Offset: 0x0014108A
		public Set Set
		{
			get
			{
				return this.set;
			}
		}

		// Token: 0x17001B92 RID: 7058
		// (get) Token: 0x06005D59 RID: 23897 RVA: 0x00142E92 File Offset: 0x00141092
		public Projection Projection
		{
			get
			{
				return this.projection;
			}
		}

		// Token: 0x17001B93 RID: 7059
		// (get) Token: 0x06005D5A RID: 23898 RVA: 0x00142E9A File Offset: 0x0014109A
		public Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17001B94 RID: 7060
		// (get) Token: 0x06005D5B RID: 23899 RVA: 0x00142EA2 File Offset: 0x001410A2
		public override IEngineHost EngineHost
		{
			get
			{
				return this.provider.EngineHost;
			}
		}

		// Token: 0x17001B95 RID: 7061
		// (get) Token: 0x06005D5C RID: 23900 RVA: 0x00142EAF File Offset: 0x001410AF
		public SetContext Context
		{
			get
			{
				if (this.context == null && !this.provider.TryCreateContext(this.cube, this.set, this.arguments, out this.context))
				{
					this.context = null;
				}
				return this.context;
			}
		}

		// Token: 0x17001B96 RID: 7062
		// (get) Token: 0x06005D5D RID: 23901 RVA: 0x00142EEB File Offset: 0x001410EB
		public override Keys Columns
		{
			get
			{
				return this.projection.Keys;
			}
		}

		// Token: 0x06005D5E RID: 23902 RVA: 0x00142EF8 File Offset: 0x001410F8
		public override TypeValue GetColumnType(int index)
		{
			TypeValue typeValue = TypeValue.Any;
			ICubeObject cubeObject;
			if (this.cube.TryGetObject(this.projection.Identifiers[index], out cubeObject))
			{
				ICubeObject2 cubeObject2 = cubeObject as ICubeObject2;
				if (cubeObject2 != null)
				{
					typeValue = cubeObject2.Type;
				}
				if (cubeObject.Kind == CubeObjectKind.DimensionAttribute)
				{
					typeValue = BinaryOperator.AddMeta.Invoke(typeValue, CubeContextCubeValue.CubeDimensionAttributeMetadata).AsType;
					typeValue = BinaryOperator.AddMeta.Invoke(typeValue, CubeContextCubeValue.NewGroupByKeyMetadata(this.Columns[index])).AsType;
				}
			}
			return typeValue;
		}

		// Token: 0x17001B97 RID: 7063
		// (get) Token: 0x06005D5F RID: 23903 RVA: 0x00142F79 File Offset: 0x00141179
		public override IList<TableKey> TableKeys
		{
			get
			{
				IList<TableKey> list;
				if ((list = this.tableKeys) == null)
				{
					Query query = this.Query;
					list = ((query != null) ? query.TableKeys : null) ?? EmptyArray<TableKey>.Instance;
				}
				return list;
			}
		}

		// Token: 0x17001B98 RID: 7064
		// (get) Token: 0x06005D60 RID: 23904 RVA: 0x00142FA0 File Offset: 0x001411A0
		public override TableSortOrder SortOrder
		{
			get
			{
				Query query = this.Query;
				return ((query != null) ? query.SortOrder : null) ?? TableSortOrder.Unknown;
			}
		}

		// Token: 0x06005D61 RID: 23905 RVA: 0x00142FC0 File Offset: 0x001411C0
		public override IEnumerable<IValueReference> GetRows()
		{
			IEnumerable<IValueReference> enumerable;
			if (this.TryGetRows(out enumerable))
			{
				this.Context.ReportFoldingFailure();
				return enumerable;
			}
			return this.Query.GetRows();
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x00142FF0 File Offset: 0x001411F0
		public override bool TryGetReader(out IPageReader reader)
		{
			IEnumerable<IValueReference> enumerable;
			if (this.TryGetRows(out enumerable))
			{
				reader = new QueryTableValue(new SetContextQuery.PageReaderQuery(this, enumerable)).GetReader();
				return true;
			}
			return this.Query.TryGetReader(out reader);
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x00143028 File Offset: 0x00141228
		public override Query SelectColumns(ColumnSelection columnSelection)
		{
			Query query = base.SelectColumns(columnSelection);
			Projection projection = this.projection.ProjectColumns(columnSelection);
			return this.New(this.cube, this.set, projection, query);
		}

		// Token: 0x06005D64 RID: 23908 RVA: 0x00143060 File Offset: 0x00141260
		public override Query RenameReorderColumns(ColumnSelection columnSelection)
		{
			Query query = base.RenameReorderColumns(columnSelection);
			Projection projection = this.projection.ProjectColumns(columnSelection);
			return this.New(this.cube, this.set, projection, query);
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x00143098 File Offset: 0x00141298
		public override Query SelectRows(FunctionValue condition)
		{
			Query query = base.SelectRows(condition);
			QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, condition);
			CubeExpression cubeExpression;
			Set set;
			if (this.TryToCubeExpression(queryExpression, out cubeExpression) && this.provider.TryCompileScalarExpression(this.cube, this.set.Dimensionality, cubeExpression, out set))
			{
				Set set2 = this.set.Intersect(set);
				Projection projection;
				set2 = this.Simplify(set2, this.projection, out projection);
				return this.New(this.cube, set2, projection, query);
			}
			return query;
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x00143114 File Offset: 0x00141314
		public override Query Skip(RowCount count)
		{
			Query query = base.Skip(count);
			Set set = this.set.Skip(count);
			return this.New(this.cube, set, this.projection, query);
		}

		// Token: 0x06005D67 RID: 23911 RVA: 0x0014314C File Offset: 0x0014134C
		public override Query Take(RowCount count)
		{
			Query query = base.Take(count);
			Set set = this.set.Take(count);
			return this.New(this.cube, set, this.projection, query);
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x00143184 File Offset: 0x00141384
		public override Query Group(Grouping grouping)
		{
			Query query = base.Group(grouping);
			ArrayBuilder<ICubeObject> arrayBuilder = default(ArrayBuilder<ICubeObject>);
			ArrayBuilder<ICubeLevel> arrayBuilder2 = default(ArrayBuilder<ICubeLevel>);
			IdentifierCubeExpression[] array = new IdentifierCubeExpression[grouping.KeyColumns.Length + grouping.Constructors.Length];
			for (int i = 0; i < grouping.KeyColumns.Length; i++)
			{
				array[i] = this.projection.Identifiers[grouping.KeyColumns[i]];
				ICubeObject cubeObject;
				if (!this.cube.TryGetObject(array[i], out cubeObject) || !(cubeObject is ICubeLevel))
				{
					return query;
				}
				arrayBuilder2.Add((ICubeLevel)cubeObject);
			}
			int j = 0;
			while (j < grouping.Constructors.Length)
			{
				ICubeMeasure cubeMeasure = null;
				IdentifierCubeExpression identifierCubeExpression = null;
				ColumnConstructor columnConstructor = grouping.Constructors[j];
				FunctionValue functionValue;
				if (SetContextQuery.TryGetMeasureApplication2(columnConstructor.Function, out functionValue) && functionValue is MeasureValue)
				{
					identifierCubeExpression = ((MeasureValue)functionValue).Measure;
					goto IL_0141;
				}
				QueryExpression queryExpression = QueryExpressionBuilder.ToQueryExpression(this, columnConstructor.Function);
				int num;
				if (GroupQuery.TryGetFirstInvocation(queryExpression, out num))
				{
					int num2 = Array.IndexOf<int>(grouping.KeyColumns, num);
					if (num2 != -1)
					{
						array[grouping.KeyColumns.Length + j] = array[num2];
						goto IL_0187;
					}
				}
				CubeExpression cubeExpression;
				if (!this.TryToCubeExpression(queryExpression, out cubeExpression) || !this.cube.TryCreateMeasure(cubeExpression, out cubeMeasure))
				{
					goto IL_0141;
				}
				ICubeObject2 cubeObject2 = cubeMeasure as ICubeObject2;
				if (cubeObject2 != null)
				{
					identifierCubeExpression = cubeObject2.Identifier;
					goto IL_0141;
				}
				goto IL_0141;
				IL_0187:
				j++;
				continue;
				IL_0141:
				ICubeObject cubeObject3;
				if (cubeMeasure == null && identifierCubeExpression != null && this.cube.TryGetObject(identifierCubeExpression, out cubeObject3))
				{
					cubeMeasure = cubeObject3 as ICubeMeasure;
				}
				if (identifierCubeExpression != null && cubeMeasure != null)
				{
					array[grouping.KeyColumns.Length + j] = identifierCubeExpression;
					arrayBuilder.Add(cubeMeasure);
					goto IL_0187;
				}
				return query;
			}
			Set set = new VisibleSlicerSet(new Dimensionality(from l in arrayBuilder2.ToArray()
				select new CubeLevelRange(l, l)));
			foreach (Set set2 in this.set.GetSubsets())
			{
				set = set.Intersect(set2);
			}
			if (arrayBuilder.Count > 0)
			{
				set = set.Project(arrayBuilder.ToArray());
			}
			Projection projection = new Projection(grouping.ResultKeys, array);
			return this.New(this.cube, set, projection, query);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001433EC File Offset: 0x001415EC
		public override bool TryJoinAsLeft(RowCount take, int[] leftKeyColumns, Query rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			SetContextQuery setContextQuery = rightQuery as SetContextQuery;
			SetContextQuery setContextQuery2;
			if (setContextQuery != null && SetContextQuery.TryJoin(take, this, leftKeyColumns, setContextQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out setContextQuery2))
			{
				query = setContextQuery2;
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x00143428 File Offset: 0x00141628
		public override bool TryJoinAsRight(RowCount take, Query leftQuery, int[] leftKeyColumns, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out Query query)
		{
			SetContextQuery setContextQuery = leftQuery as SetContextQuery;
			SetContextQuery setContextQuery2;
			if (setContextQuery != null && SetContextQuery.TryJoin(take, setContextQuery, leftKeyColumns, this, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers, out setContextQuery2))
			{
				query = setContextQuery2;
				return true;
			}
			query = null;
			return false;
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x00143463 File Offset: 0x00141663
		public override bool TryInvokeAsArgument(FunctionValue function, Value[] arguments, int index, out Value result)
		{
			if (function.FunctionIdentity.Equals(CapabilityModule.DirectQueryCapabilities.From.FunctionIdentity) && index == 0)
			{
				result = this.Context.DirectQueryCapabilities;
				return true;
			}
			result = null;
			return false;
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x00143494 File Offset: 0x00141694
		public SetContextQuery ApplyParameter(FunctionValue parameter, Value[] args)
		{
			ParameterArguments parameterArguments = new ParameterArguments(((ParameterValue)parameter).Parameter, args);
			return new SetContextQuery(this.provider, this.arguments.Concat(new ParameterArguments[] { parameterArguments }).ToArray<ParameterArguments>(), this.cube, this.set, this.projection, this.tableKeys, this.query);
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x001434F6 File Offset: 0x001416F6
		private bool TryToCubeExpression(QueryExpression queryExpr, out CubeExpression cubeExpr)
		{
			cubeExpr = new QueryExpressionTranslator(new SetContextQuery.SetContextQueryCubeMetadataProvider(this)).Translate(queryExpr);
			return cubeExpr != null;
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x00143510 File Offset: 0x00141710
		private bool TryGetRows(out IEnumerable<IValueReference> rows)
		{
			SetContext localContext = this.Context;
			if (localContext != null)
			{
				if (this.set.GetResultObjects().Any<ICubeObject>())
				{
					this.EnsureParametersAreProvided();
				}
				View view = this.projection.ToView(this.set);
				rows = DeferredEnumerable.From<IValueReference>(() => ProjectColumnsQuery.Project(localContext.Evaluate(), view.InnerSelection));
				return true;
			}
			rows = null;
			return false;
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x00143580 File Offset: 0x00141780
		private void EnsureParametersAreProvided()
		{
			CubeValue asCube = SetContextTableValue.New(this).AsCube;
			using (IEnumerator<IValueReference> enumerator = this.Context.GetParameters(asCube).GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Value[CubeParametersTableValue.IsOptional].AsBoolean)
					{
						throw ValueException.NewParameterError<Message0>(Strings.Cube_ParameterMissing, Value.Null);
					}
				}
			}
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x00143600 File Offset: 0x00141800
		private SetContextQuery NewScope(string scope)
		{
			return new SetContextQuery(this.provider, this.arguments, this.cube.NewScope(scope), this.set.NewScope(scope), this.projection.NewScope(scope), this.tableKeys, this.query);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x00143650 File Offset: 0x00141850
		private Set Simplify(Set set, Projection projection, out Projection newProjection)
		{
			Set set2;
			try
			{
				bool flag = true;
				while (flag)
				{
					set = this.Simplify(set, projection, out projection, out flag);
				}
				newProjection = projection;
				set2 = set;
			}
			catch (NotSupportedException)
			{
				newProjection = projection;
				set2 = set;
			}
			return set2;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x00143694 File Offset: 0x00141894
		private Set Simplify(Set set, Projection projection, out Projection newProjection, out bool modified)
		{
			DataflowGraph dataflowGraph = DataflowGraph.From(this.provider, this.cube, set);
			Set set2;
			dataflowGraph.AddConstraints(set, out set2);
			Set set3;
			Dictionary<ScopePath, ScopePath> replacements = dataflowGraph.GetReplacements(out set3);
			Dimensionality dimensionality = set.Dimensionality.ReplaceScopePaths(replacements);
			newProjection = projection.ReplaceScopePaths(replacements);
			Set set4 = new VisibleSlicerSet(dimensionality);
			foreach (Set set5 in set2.GetSubsets().Concat(set3.GetSubsets()))
			{
				set4 = set4.Intersect(set5.ReplaceScopePaths(replacements));
			}
			modified = replacements.Count > 0;
			set4 = set4.Project(from o in set.GetResultObjects()
				where o.Kind > CubeObjectKind.DimensionAttribute
				select o);
			return set4;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x00143778 File Offset: 0x00141978
		private static string NextId()
		{
			long num = SetContextQuery.nextId;
			SetContextQuery.nextId = num + 1L;
			long num2 = num;
			return num2.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001437A0 File Offset: 0x001419A0
		private static bool TryGetMeasureApplication2(FunctionValue function, out FunctionValue measure)
		{
			Value value = function;
			IFunctionExpression functionExpression = function.Expression as IFunctionExpression;
			if (functionExpression != null)
			{
				functionExpression = ConstantFoldingVisitor.Fold(functionExpression) as IFunctionExpression;
			}
			if (functionExpression != null)
			{
				Dictionary<string, IExpression> dictionary;
				Value value2;
				if (SetContextQuery.measureByIdPattern.TryMatch(functionExpression, out dictionary) && dictionary.TryGetConstant("measureIdRecord", out value2) && value2.IsRecord)
				{
					IdentifierCubeExpression identifierCubeExpression = new IdentifierCubeExpression(value2["Id"].AsString);
					measure = new MeasureValue(identifierCubeExpression, TypeValue.Any);
					return true;
				}
				if (SetContextQuery.measureByObjectPattern.TryMatch(functionExpression, out dictionary))
				{
					dictionary.TryGetConstant("measure", out value);
				}
			}
			if (value.IsFunction)
			{
				measure = value.AsFunction.SubtractMetaValue as MeasureValue;
				if (measure == null && value.AsFunction.FunctionIdentity.Equals(TableModule.Table.RowCount))
				{
					measure = MeasureValue.Count;
				}
				return measure != null;
			}
			measure = null;
			return false;
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x00143880 File Offset: 0x00141A80
		private static bool TryGetEqualityExpression(QueryExpression leftExpr, QueryExpression rightExpr, FunctionValue keyEqualityComparer, out QueryExpression expression)
		{
			if (keyEqualityComparer == null || keyEqualityComparer.IsNull || keyEqualityComparer.Equals(Library._Value.Equals))
			{
				expression = new BinaryQueryExpression(BinaryOperator2.Equals, leftExpr, rightExpr);
				return true;
			}
			if (keyEqualityComparer.Equals(Library._Value.NullableEquals))
			{
				expression = new InvocationQueryExpression(new ConstantQueryExpression(Library._Value.NullableEquals), new QueryExpression[] { leftExpr, rightExpr });
				return true;
			}
			expression = null;
			return false;
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x001438E4 File Offset: 0x00141AE4
		private static bool TryJoin(RowCount take, SetContextQuery leftQuery, int[] leftKeyColumns, SetContextQuery rightQuery, int[] rightKeyColumns, TableTypeAlgebra.JoinKind joinKind, Keys joinKeys, JoinColumn[] joinColumns, JoinAlgorithm joinAlgorithm, FunctionValue[] keyEqualityComparers, out SetContextQuery newSetQuery)
		{
			if ((joinKind == TableTypeAlgebra.JoinKind.Inner || leftKeyColumns.Length == 0) && joinKeys.Length == leftQuery.Columns.Length + rightQuery.Columns.Length && take.IsInfinite && leftQuery.Provider.Equals(rightQuery.Provider) && leftQuery.Arguments.SequenceEqual(rightQuery.Arguments))
			{
				Query query = new JoinQuery(take, leftQuery, leftKeyColumns, rightQuery, rightKeyColumns, joinKind, joinKeys, joinColumns, joinAlgorithm, keyEqualityComparers);
				SetContextQuery setContextQuery = leftQuery.NewScope(SetContextQuery.NextId());
				SetContextQuery setContextQuery2 = rightQuery.NewScope(SetContextQuery.NextId());
				ICube cube = setContextQuery.Cube.Union(setContextQuery2.Cube);
				Projection projection = setContextQuery.Projection.Append(setContextQuery2.Projection);
				Set set = new VisibleSlicerSet(setContextQuery.Set.Dimensionality.Union(setContextQuery2.Set.Dimensionality));
				foreach (Set set2 in setContextQuery.Set.GetSubsets())
				{
					set = set.Intersect(set2);
				}
				foreach (Set set3 in setContextQuery2.Set.GetSubsets())
				{
					set = set.Intersect(set3);
				}
				set = set.Project(from o in setContextQuery.Set.GetResultObjects().Concat(setContextQuery2.Set.GetResultObjects())
					where o is ICubeMeasure
					select o);
				newSetQuery = new SetContextQuery(leftQuery.Provider, leftQuery.Arguments, cube, set, projection, query);
				QueryExpression queryExpression = null;
				for (int i = 0; i < leftKeyColumns.Length; i++)
				{
					QueryExpression queryExpression2;
					if (!SetContextQuery.TryGetEqualityExpression(new ColumnAccessQueryExpression(leftKeyColumns[i]), new ColumnAccessQueryExpression(leftQuery.Columns.Length + rightKeyColumns[i]), (keyEqualityComparers != null) ? keyEqualityComparers[i] : null, out queryExpression2))
					{
						newSetQuery = null;
						break;
					}
					if (queryExpression == null)
					{
						queryExpression = queryExpression2;
					}
					else
					{
						queryExpression = new BinaryQueryExpression(BinaryOperator2.And, queryExpression, queryExpression2);
					}
				}
				if (newSetQuery != null && queryExpression != null)
				{
					FunctionValue functionValue = QueryExpressionAssembler.Assemble(query.Columns, queryExpression);
					newSetQuery = newSetQuery.SelectRows(functionValue) as SetContextQuery;
				}
				if (newSetQuery != null)
				{
					return true;
				}
			}
			newSetQuery = null;
			return false;
		}

		// Token: 0x04003365 RID: 13157
		private static long nextId;

		// Token: 0x04003366 RID: 13158
		private readonly SetContextProvider provider;

		// Token: 0x04003367 RID: 13159
		private readonly IList<ParameterArguments> arguments;

		// Token: 0x04003368 RID: 13160
		private readonly ICube cube;

		// Token: 0x04003369 RID: 13161
		private readonly Set set;

		// Token: 0x0400336A RID: 13162
		private readonly Projection projection;

		// Token: 0x0400336B RID: 13163
		private readonly IList<TableKey> tableKeys;

		// Token: 0x0400336C RID: 13164
		private readonly Query query;

		// Token: 0x0400336D RID: 13165
		private SetContext context;

		// Token: 0x0400336E RID: 13166
		private static readonly ExpressionPattern measureByIdPattern = new ExpressionPattern(new string[] { "(__row) => Cube.Measures(__row){__measureIdRecord}[Data](__row)" });

		// Token: 0x0400336F RID: 13167
		private static readonly ExpressionPattern measureByObjectPattern = new ExpressionPattern(new string[] { "(__row) => __measure(__row)" });

		// Token: 0x02000D6D RID: 3437
		private class PageReaderQuery : DataSourceQuery
		{
			// Token: 0x06005D78 RID: 23928 RVA: 0x00143B96 File Offset: 0x00141D96
			public PageReaderQuery(SetContextQuery query, IEnumerable<IValueReference> enumerable)
			{
				this.query = query;
				this.enumerable = enumerable;
			}

			// Token: 0x17001B99 RID: 7065
			// (get) Token: 0x06005D79 RID: 23929 RVA: 0x00143BAC File Offset: 0x00141DAC
			public override Keys Columns
			{
				get
				{
					return this.query.Columns;
				}
			}

			// Token: 0x17001B9A RID: 7066
			// (get) Token: 0x06005D7A RID: 23930 RVA: 0x00143BB9 File Offset: 0x00141DB9
			public override IEngineHost EngineHost
			{
				get
				{
					return this.query.EngineHost;
				}
			}

			// Token: 0x06005D7B RID: 23931 RVA: 0x00143BC6 File Offset: 0x00141DC6
			public override TypeValue GetColumnType(int column)
			{
				return this.query.GetColumnType(column);
			}

			// Token: 0x06005D7C RID: 23932 RVA: 0x00143BD4 File Offset: 0x00141DD4
			public override IEnumerable<IValueReference> GetRows()
			{
				return this.enumerable;
			}

			// Token: 0x04003370 RID: 13168
			private readonly SetContextQuery query;

			// Token: 0x04003371 RID: 13169
			private readonly IEnumerable<IValueReference> enumerable;
		}

		// Token: 0x02000D6E RID: 3438
		private class SetContextQueryCubeMetadataProvider : ICubeMetadataProvider
		{
			// Token: 0x06005D7D RID: 23933 RVA: 0x00143BDC File Offset: 0x00141DDC
			public SetContextQueryCubeMetadataProvider(SetContextQuery query)
			{
				this.query = query;
			}

			// Token: 0x06005D7E RID: 23934 RVA: 0x00143BEB File Offset: 0x00141DEB
			public IdentifierCubeExpression GetIdentifier(int columnIndex)
			{
				return this.query.Projection.Identifiers[columnIndex];
			}

			// Token: 0x06005D7F RID: 23935 RVA: 0x00143C00 File Offset: 0x00141E00
			public bool IsDimensionAttribute(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				return this.query.cube.TryGetObject(identifier, out cubeObject) && cubeObject.Kind == CubeObjectKind.DimensionAttribute;
			}

			// Token: 0x06005D80 RID: 23936 RVA: 0x00143C30 File Offset: 0x00141E30
			public bool IsProperty(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				return this.query.cube.TryGetObject(identifier, out cubeObject) && cubeObject.Kind == CubeObjectKind.Property;
			}

			// Token: 0x06005D81 RID: 23937 RVA: 0x00143C60 File Offset: 0x00141E60
			public bool IsMeasure(IdentifierCubeExpression identifier)
			{
				ICubeObject cubeObject;
				return this.query.cube.TryGetObject(identifier, out cubeObject) && cubeObject.Kind == CubeObjectKind.Measure;
			}

			// Token: 0x06005D82 RID: 23938 RVA: 0x000091AE File Offset: 0x000073AE
			public IdentifierCubeExpression GetProperty(IdentifierCubeExpression attribute, CubePropertyKind kind, string name = null)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005D83 RID: 23939 RVA: 0x000091AE File Offset: 0x000073AE
			public bool TryGetPropertyKey(IdentifierCubeExpression property, out IdentifierCubeExpression key)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06005D84 RID: 23940 RVA: 0x000091AE File Offset: 0x000073AE
			public IdentifierCubeExpression GetMeasureProperty(IdentifierCubeExpression measure, string propertyName)
			{
				throw new NotImplementedException();
			}

			// Token: 0x04003372 RID: 13170
			private readonly SetContextQuery query;
		}
	}
}

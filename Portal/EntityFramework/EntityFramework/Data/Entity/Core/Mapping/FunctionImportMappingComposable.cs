using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Query.InternalTrees;
using System.Data.Entity.Core.Query.PlanCompiler;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000537 RID: 1335
	public sealed class FunctionImportMappingComposable : FunctionImportMapping
	{
		// Token: 0x060041AA RID: 16810 RVA: 0x000DD940 File Offset: 0x000DBB40
		public FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, FunctionImportResultMapping resultMapping, EntityContainerMapping containerMapping)
			: base(Check.NotNull<EdmFunction>(functionImport, "functionImport"), Check.NotNull<EdmFunction>(targetFunction, "targetFunction"))
		{
			Check.NotNull<FunctionImportResultMapping>(resultMapping, "resultMapping");
			Check.NotNull<EntityContainerMapping>(containerMapping, "containerMapping");
			if (!functionImport.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("functionImport"));
			}
			if (!targetFunction.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("targetFunction"));
			}
			EdmType edmType;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, 0, out edmType))
			{
				throw new ArgumentException(Strings.InvalidReturnTypeForComposableFunction);
			}
			EdmFunction edmFunction = ((containerMapping.StorageMappingItemCollection != null) ? containerMapping.StorageMappingItemCollection.StoreItemCollection.ConvertToCTypeFunction(targetFunction) : StoreItemCollection.ConvertFunctionSignatureToCType(targetFunction));
			RowType tvfReturnType = TypeHelpers.GetTvfReturnType(edmFunction);
			RowType tvfReturnType2 = TypeHelpers.GetTvfReturnType(targetFunction);
			if (tvfReturnType == null)
			{
				throw new ArgumentException(Strings.Mapping_FunctionImport_ResultMapping_InvalidSType(functionImport.Identity), "functionImport");
			}
			List<EdmSchemaError> list = new List<EdmSchemaError>();
			FunctionImportMappingComposableHelper functionImportMappingComposableHelper = new FunctionImportMappingComposableHelper(containerMapping, string.Empty, list);
			FunctionImportMappingComposable functionImportMappingComposable;
			if (Helper.IsStructuralType(edmType))
			{
				functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithStructuralResult(functionImport, edmFunction, resultMapping.SourceList, tvfReturnType, tvfReturnType2, LineInfo.Empty, out functionImportMappingComposable);
			}
			else
			{
				functionImportMappingComposableHelper.TryCreateFunctionImportMappingComposableWithScalarResult(functionImport, edmFunction, targetFunction, edmType, tvfReturnType, LineInfo.Empty, out functionImportMappingComposable);
			}
			if (functionImportMappingComposable == null)
			{
				throw new InvalidOperationException((list.Count > 0) ? list[0].Message : string.Empty);
			}
			this._containerMapping = functionImportMappingComposable._containerMapping;
			this.m_commandParameters = functionImportMappingComposable.m_commandParameters;
			this.m_structuralTypeMappings = functionImportMappingComposable.m_structuralTypeMappings;
			this.m_targetFunctionKeys = functionImportMappingComposable.m_targetFunctionKeys;
			this._resultMapping = resultMapping;
		}

		// Token: 0x060041AB RID: 16811 RVA: 0x000DDAC4 File Offset: 0x000DBCC4
		internal FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> structuralTypeMappings)
			: base(functionImport, targetFunction)
		{
			if (!functionImport.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("functionImport"));
			}
			if (!targetFunction.IsComposableAttribute)
			{
				throw new ArgumentException(Strings.NonComposableFunctionCannotBeMappedAsComposable("targetFunction"));
			}
			EdmType edmType;
			if (!MetadataHelper.TryGetFunctionImportReturnType<EdmType>(functionImport, 0, out edmType))
			{
				throw new ArgumentException(Strings.InvalidReturnTypeForComposableFunction);
			}
			if (!TypeSemantics.IsScalarType(edmType) && (structuralTypeMappings == null || structuralTypeMappings.Count == 0))
			{
				throw new ArgumentException(Strings.StructuralTypeMappingsMustNotBeNullForFunctionImportsReturningNonScalarValues);
			}
			this.m_structuralTypeMappings = structuralTypeMappings;
		}

		// Token: 0x060041AC RID: 16812 RVA: 0x000DDB44 File Offset: 0x000DBD44
		internal FunctionImportMappingComposable(EdmFunction functionImport, EdmFunction targetFunction, List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> structuralTypeMappings, EdmProperty[] targetFunctionKeys, EntityContainerMapping containerMapping)
			: base(functionImport, targetFunction)
		{
			this._containerMapping = containerMapping;
			this.m_commandParameters = functionImport.Parameters.Select((FunctionParameter p) => TypeHelpers.GetPrimitiveTypeUsageForScalar(p.TypeUsage).Parameter(p.Name)).ToArray<DbParameterReferenceExpression>();
			this.m_structuralTypeMappings = structuralTypeMappings;
			this.m_targetFunctionKeys = targetFunctionKeys;
		}

		// Token: 0x17000D01 RID: 3329
		// (get) Token: 0x060041AD RID: 16813 RVA: 0x000DDBA5 File Offset: 0x000DBDA5
		public FunctionImportResultMapping ResultMapping
		{
			get
			{
				return this._resultMapping;
			}
		}

		// Token: 0x060041AE RID: 16814 RVA: 0x000DDBAD File Offset: 0x000DBDAD
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._resultMapping);
			base.SetReadOnly();
		}

		// Token: 0x17000D02 RID: 3330
		// (get) Token: 0x060041AF RID: 16815 RVA: 0x000DDBC0 File Offset: 0x000DBDC0
		internal ReadOnlyCollection<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> StructuralTypeMappings
		{
			get
			{
				if (this.m_structuralTypeMappings != null)
				{
					return new ReadOnlyCollection<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>>(this.m_structuralTypeMappings);
				}
				return null;
			}
		}

		// Token: 0x17000D03 RID: 3331
		// (get) Token: 0x060041B0 RID: 16816 RVA: 0x000DDBD7 File Offset: 0x000DBDD7
		internal EdmProperty[] TvfKeys
		{
			get
			{
				return this.m_targetFunctionKeys;
			}
		}

		// Token: 0x060041B1 RID: 16817 RVA: 0x000DDBE0 File Offset: 0x000DBDE0
		internal Node GetInternalTree(Command targetIqtCommand, IList<Node> targetIqtArguments)
		{
			if (this.m_internalTreeNode == null)
			{
				DiscriminatorMap discriminatorMap;
				Command command = ITreeGenerator.Generate(this.GenerateFunctionView(out discriminatorMap), discriminatorMap);
				Node root = command.Root;
				PlanCompiler.Assert(root.Op.OpType == OpType.PhysicalProject, "Expected a physical projectOp at the root of the tree - found " + root.Op.OpType.ToString());
				PhysicalProjectOp physicalProjectOp = (PhysicalProjectOp)root.Op;
				Node child = root.Child0;
				command.DisableVarVecEnumCaching();
				Node node = child;
				Var var = physicalProjectOp.Outputs[0];
				if (!Command.EqualTypes(physicalProjectOp.ColumnMap.Type, base.FunctionImport.ReturnParameter.TypeUsage))
				{
					TypeUsage typeUsage = ((CollectionType)base.FunctionImport.ReturnParameter.TypeUsage.EdmType).TypeUsage;
					Node node2 = command.CreateNode(command.CreateVarRefOp(var));
					Node node3 = command.CreateNode(command.CreateSoftCastOp(typeUsage), node2);
					Node node4 = command.CreateVarDefListNode(node3, out var);
					ProjectOp projectOp = command.CreateProjectOp(var);
					node = command.CreateNode(projectOp, node, node4);
				}
				this.m_internalTreeNode = command.BuildCollect(node, var);
			}
			Dictionary<string, Node> dictionary = new Dictionary<string, Node>(this.m_commandParameters.Length);
			for (int i = 0; i < this.m_commandParameters.Length; i++)
			{
				DbParameterReferenceExpression dbParameterReferenceExpression = this.m_commandParameters[i];
				Node node5 = targetIqtArguments[i];
				if (TypeSemantics.IsEnumerationType(node5.Op.Type))
				{
					node5 = targetIqtCommand.CreateNode(targetIqtCommand.CreateSoftCastOp(TypeHelpers.CreateEnumUnderlyingTypeUsage(node5.Op.Type)), node5);
				}
				dictionary.Add(dbParameterReferenceExpression.ParameterName, node5);
			}
			return FunctionImportMappingComposable.FunctionViewOpCopier.Copy(targetIqtCommand, this.m_internalTreeNode, dictionary);
		}

		// Token: 0x060041B2 RID: 16818 RVA: 0x000DDD90 File Offset: 0x000DBF90
		internal DbQueryCommandTree GenerateFunctionView(out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression dbExpression = base.TargetFunction.Invoke(this.GetParametersForTargetFunctionCall());
			DbExpression dbExpression2;
			if (this.m_structuralTypeMappings != null)
			{
				dbExpression2 = this.GenerateStructuralTypeResultMappingView(dbExpression, out discriminatorMap);
			}
			else
			{
				dbExpression2 = this.GenerateScalarResultMappingView(dbExpression);
			}
			return DbQueryCommandTree.FromValidExpression(this._containerMapping.StorageMappingItemCollection.Workspace, DataSpace.SSpace, dbExpression2, true, false);
		}

		// Token: 0x060041B3 RID: 16819 RVA: 0x000DDDE6 File Offset: 0x000DBFE6
		private IEnumerable<DbExpression> GetParametersForTargetFunctionCall()
		{
			using (ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = base.TargetFunction.Parameters.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					FunctionParameter targetParameter = enumerator.Current;
					FunctionParameter functionParameter = base.FunctionImport.Parameters.Single((FunctionParameter p) => p.Name == targetParameter.Name);
					yield return this.m_commandParameters[base.FunctionImport.Parameters.IndexOf(functionParameter)];
				}
			}
			ReadOnlyMetadataCollection<FunctionParameter>.Enumerator enumerator = default(ReadOnlyMetadataCollection<FunctionParameter>.Enumerator);
			yield break;
			yield break;
		}

		// Token: 0x060041B4 RID: 16820 RVA: 0x000DDDF8 File Offset: 0x000DBFF8
		private DbExpression GenerateStructuralTypeResultMappingView(DbExpression storeFunctionInvoke, out DiscriminatorMap discriminatorMap)
		{
			discriminatorMap = null;
			DbExpression dbExpression = storeFunctionInvoke;
			if (this.m_structuralTypeMappings.Count == 1)
			{
				Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple = this.m_structuralTypeMappings[0];
				StructuralType item = tuple.Item1;
				List<ConditionPropertyMapping> conditions = tuple.Item2;
				List<PropertyMapping> item2 = tuple.Item3;
				if (conditions.Count > 0)
				{
					dbExpression = dbExpression.Where((DbExpression row) => FunctionImportMappingComposable.GenerateStructuralTypeConditionsPredicate(conditions, row));
				}
				DbExpressionBinding dbExpressionBinding = dbExpression.BindAs("row");
				DbExpression dbExpression2 = FunctionImportMappingComposable.GenerateStructuralTypeMappingView(item, item2, dbExpressionBinding.Variable);
				dbExpression = dbExpressionBinding.Project(dbExpression2);
			}
			else
			{
				DbExpressionBinding binding = dbExpression.BindAs("row");
				List<DbExpression> list = this.m_structuralTypeMappings.Select((Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> m) => FunctionImportMappingComposable.GenerateStructuralTypeConditionsPredicate(m.Item2, binding.Variable)).ToList<DbExpression>();
				dbExpression = binding.Filter(Helpers.BuildBalancedTreeInPlace<DbExpression>(list.ToArray(), (DbExpression prev, DbExpression next) => prev.Or(next)));
				binding = dbExpression.BindAs("row");
				List<DbExpression> list2 = new List<DbExpression>(this.m_structuralTypeMappings.Count);
				foreach (Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>> tuple2 in this.m_structuralTypeMappings)
				{
					StructuralType item3 = tuple2.Item1;
					List<PropertyMapping> item4 = tuple2.Item3;
					list2.Add(FunctionImportMappingComposable.GenerateStructuralTypeMappingView(item3, item4, binding.Variable));
				}
				DbExpression dbExpression3 = DbExpressionBuilder.Case(list.Take(this.m_structuralTypeMappings.Count - 1), list2.Take(this.m_structuralTypeMappings.Count - 1), list2[this.m_structuralTypeMappings.Count - 1]);
				dbExpression = binding.Project(dbExpression3);
				DiscriminatorMap.TryCreateDiscriminatorMap(base.FunctionImport.EntitySet, dbExpression, out discriminatorMap);
			}
			return dbExpression;
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000DDFF4 File Offset: 0x000DC1F4
		private static DbExpression GenerateStructuralTypeMappingView(StructuralType structuralType, List<PropertyMapping> propertyMappings, DbExpression row)
		{
			List<DbExpression> list = new List<DbExpression>(TypeHelpers.GetAllStructuralMembers(structuralType).Count);
			for (int i = 0; i < propertyMappings.Count; i++)
			{
				PropertyMapping propertyMapping = propertyMappings[i];
				list.Add(FunctionImportMappingComposable.GeneratePropertyMappingView(propertyMapping, row));
			}
			return TypeUsage.Create(structuralType).New(list);
		}

		// Token: 0x060041B6 RID: 16822 RVA: 0x000DE044 File Offset: 0x000DC244
		private static DbExpression GenerateStructuralTypeConditionsPredicate(List<ConditionPropertyMapping> conditions, DbExpression row)
		{
			return Helpers.BuildBalancedTreeInPlace<DbExpression>(conditions.Select((ConditionPropertyMapping c) => FunctionImportMappingComposable.GeneratePredicate(c, row)).ToArray<DbExpression>(), (DbExpression prev, DbExpression next) => prev.And(next));
		}

		// Token: 0x060041B7 RID: 16823 RVA: 0x000DE09C File Offset: 0x000DC29C
		private static DbExpression GeneratePredicate(ConditionPropertyMapping condition, DbExpression row)
		{
			DbExpression dbExpression = FunctionImportMappingComposable.GenerateColumnRef(row, condition.Column);
			if (condition.IsNull == null)
			{
				return dbExpression.Equal(dbExpression.ResultType.Constant(condition.Value));
			}
			if (!condition.IsNull.Value)
			{
				return dbExpression.IsNull().Not();
			}
			return dbExpression.IsNull();
		}

		// Token: 0x060041B8 RID: 16824 RVA: 0x000DE100 File Offset: 0x000DC300
		private static DbExpression GeneratePropertyMappingView(PropertyMapping mapping, DbExpression row)
		{
			ScalarPropertyMapping scalarPropertyMapping = (ScalarPropertyMapping)mapping;
			return FunctionImportMappingComposable.GenerateScalarPropertyMappingView(scalarPropertyMapping.Property, scalarPropertyMapping.Column, row);
		}

		// Token: 0x060041B9 RID: 16825 RVA: 0x000DE128 File Offset: 0x000DC328
		private static DbExpression GenerateScalarPropertyMappingView(EdmProperty edmProperty, EdmProperty columnProperty, DbExpression row)
		{
			DbExpression dbExpression = FunctionImportMappingComposable.GenerateColumnRef(row, columnProperty);
			if (!TypeSemantics.IsEqual(dbExpression.ResultType, edmProperty.TypeUsage))
			{
				dbExpression = dbExpression.CastTo(edmProperty.TypeUsage);
			}
			return dbExpression;
		}

		// Token: 0x060041BA RID: 16826 RVA: 0x000DE15E File Offset: 0x000DC35E
		private static DbExpression GenerateColumnRef(DbExpression row, EdmProperty column)
		{
			RowType rowType = (RowType)row.ResultType.EdmType;
			return row.Property(column.Name);
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000DE180 File Offset: 0x000DC380
		private DbExpression GenerateScalarResultMappingView(DbExpression storeFunctionInvoke)
		{
			CollectionType functionImportReturnType;
			MetadataHelper.TryGetFunctionImportReturnCollectionType(base.FunctionImport, 0, out functionImportReturnType);
			RowType rowType = (RowType)((CollectionType)storeFunctionInvoke.ResultType.EdmType).TypeUsage.EdmType;
			EdmProperty column = rowType.Properties[0];
			Func<DbExpression, DbExpression> scalarView = delegate(DbExpression row)
			{
				DbPropertyExpression dbPropertyExpression = row.Property(column);
				if (TypeSemantics.IsEqual(functionImportReturnType.TypeUsage, column.TypeUsage))
				{
					return dbPropertyExpression;
				}
				return dbPropertyExpression.CastTo(functionImportReturnType.TypeUsage);
			};
			return storeFunctionInvoke.Select((DbExpression row) => scalarView(row));
		}

		// Token: 0x040016C7 RID: 5831
		private readonly FunctionImportResultMapping _resultMapping;

		// Token: 0x040016C8 RID: 5832
		private readonly EntityContainerMapping _containerMapping;

		// Token: 0x040016C9 RID: 5833
		private readonly DbParameterReferenceExpression[] m_commandParameters;

		// Token: 0x040016CA RID: 5834
		private readonly List<Tuple<StructuralType, List<ConditionPropertyMapping>, List<PropertyMapping>>> m_structuralTypeMappings;

		// Token: 0x040016CB RID: 5835
		private readonly EdmProperty[] m_targetFunctionKeys;

		// Token: 0x040016CC RID: 5836
		private Node m_internalTreeNode;

		// Token: 0x02000B38 RID: 2872
		private sealed class FunctionViewOpCopier : OpCopier
		{
			// Token: 0x06006538 RID: 25912 RVA: 0x0015C44C File Offset: 0x0015A64C
			private FunctionViewOpCopier(Command cmd, Dictionary<string, Node> viewArguments)
				: base(cmd)
			{
				this.m_viewArguments = viewArguments;
			}

			// Token: 0x06006539 RID: 25913 RVA: 0x0015C45C File Offset: 0x0015A65C
			internal static Node Copy(Command cmd, Node viewNode, Dictionary<string, Node> viewArguments)
			{
				return new FunctionImportMappingComposable.FunctionViewOpCopier(cmd, viewArguments).CopyNode(viewNode);
			}

			// Token: 0x0600653A RID: 25914 RVA: 0x0015C46C File Offset: 0x0015A66C
			public override Node Visit(VarRefOp op, Node n)
			{
				Node node;
				if (op.Var.VarType == VarType.Parameter && this.m_viewArguments.TryGetValue(((ParameterVar)op.Var).ParameterName, out node))
				{
					return OpCopier.Copy(this.m_destCmd, node);
				}
				return base.Visit(op, n);
			}

			// Token: 0x04002D3D RID: 11581
			private readonly Dictionary<string, Node> m_viewArguments;
		}
	}
}

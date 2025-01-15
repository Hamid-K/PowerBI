using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DataShaping;
using Microsoft.DataShaping.Common;
using Microsoft.DataShaping.InternalContracts.Model;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.InfoNav.Utils;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal;
using Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal
{
	// Token: 0x02000154 RID: 340
	internal static class QueryExpressionBuilder
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x0003642B File Offset: 0x0003462B
		public static QueryAllExpression All(this IEdmFieldInstance fieldInstance, IConceptualColumn column)
		{
			return fieldInstance.Entity.All(fieldInstance.Field, (column != null) ? column.Entity : null, column);
		}

		// Token: 0x060012CD RID: 4813 RVA: 0x0003644B File Offset: 0x0003464B
		public static QueryAllExpression All(this IConceptualColumn column)
		{
			return column.Entity.All(column);
		}

		// Token: 0x060012CE RID: 4814 RVA: 0x00036459 File Offset: 0x00034659
		public static QueryAllExpression All(this EntitySet targetSet, IConceptualEntity targetEntity)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetSet, targetEntity);
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x00036463 File Offset: 0x00034663
		public static QueryAllExpression All(this IConceptualEntity targetEntity)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetEntity);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x0003646C File Offset: 0x0003466C
		public static QueryAllExpression All(this EntitySet targetSet, EdmField fieldMetadata, IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetSet, fieldMetadata, targetEntity, columnMetadata);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x00036478 File Offset: 0x00034678
		public static QueryAllExpression All(this IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetEntity, columnMetadata);
		}

		// Token: 0x060012D2 RID: 4818 RVA: 0x00036482 File Offset: 0x00034682
		public static QueryAllExpression All(this EntitySet targetSet, IReadOnlyList<EdmField> fieldsMetadata, IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetSet, fieldsMetadata, targetEntity, columnsMetadata);
		}

		// Token: 0x060012D3 RID: 4819 RVA: 0x0003648E File Offset: 0x0003468E
		public static QueryAllExpression All(this IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.All, targetEntity, columnsMetadata);
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00036498 File Offset: 0x00034698
		public static QueryAllExpression All()
		{
			return new QueryAllExpression(QueryAllKind.All, QueryAllExpression.NoArgAllResultType);
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x000364A5 File Offset: 0x000346A5
		public static QueryAllExpression AllSelected()
		{
			return new QueryAllExpression(QueryAllKind.AllSelected, QueryAllExpression.NoArgAllResultType);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000364B2 File Offset: 0x000346B2
		public static QueryAllExpression AllSelected(this EntitySet targetSet, IConceptualEntity targetEntity)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetSet, targetEntity);
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x000364BC File Offset: 0x000346BC
		public static QueryAllExpression AllSelected(this IConceptualEntity targetEntity)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetEntity);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000364C5 File Offset: 0x000346C5
		public static QueryAllExpression AllSelected(this IEdmFieldInstance fieldInstance, IConceptualColumn columnMetadata)
		{
			return fieldInstance.Entity.AllSelected(fieldInstance.Field, (columnMetadata != null) ? columnMetadata.Entity : null, columnMetadata);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000364E5 File Offset: 0x000346E5
		public static QueryAllExpression AllSelected(this IConceptualColumn columnMetadata)
		{
			return columnMetadata.Entity.AllSelected(columnMetadata);
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000364F3 File Offset: 0x000346F3
		public static QueryAllExpression AllSelected(this EntitySet targetSet, EdmField fieldMetadata, IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetSet, fieldMetadata, targetEntity, columnMetadata);
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000364FF File Offset: 0x000346FF
		public static QueryAllExpression AllSelected(this IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetEntity, columnMetadata);
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00036509 File Offset: 0x00034709
		public static QueryAllExpression AllSelected(this EntitySet targetSet, IReadOnlyList<EdmField> fieldsMetadata, IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetSet, fieldsMetadata, targetEntity, columnsMetadata);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x00036515 File Offset: 0x00034715
		public static QueryAllExpression AllSelected(this IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			return QueryExpressionBuilder.CreateAllExpr(QueryAllKind.AllSelected, targetEntity, columnsMetadata);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x0003651F File Offset: 0x0003471F
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, EntitySet targetSet, IConceptualEntity targetEntity)
		{
			if (targetEntity == null)
			{
				return new QueryAllExpression(allKind, targetSet.ElementType.ConceptualType.Table(), targetSet, targetEntity);
			}
			return new QueryAllExpression(allKind, targetEntity.GetExtensionAwareResultType(), targetSet, targetEntity);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x0003654B File Offset: 0x0003474B
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, IConceptualEntity targetEntity)
		{
			return new QueryAllExpression(allKind, targetEntity.GetExtensionAwareResultType(), null, targetEntity);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x0003655C File Offset: 0x0003475C
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, EntitySet targetSet, EdmField fieldMetadata, IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			ArgumentValidation.CheckNotNull<EntitySet>(targetSet, "targetSet");
			ArgumentValidation.CheckNotNull<EdmField>(fieldMetadata, "fieldMetadata");
			ArgumentValidation.CheckCondition(fieldMetadata.DeclaringType == targetSet.ElementType, "fieldMetadata");
			ArgumentValidation.CheckCondition(targetEntity == null == (columnMetadata == null), "targetEntity and columnMetadata must both be passed or must both be omitted");
			ConceptualTableType conceptualTableType = ((columnMetadata != null) ? columnMetadata.ConceptualTypeColumn.Row().Table() : fieldMetadata.Column.Row().Table());
			return new QueryAllExpression(allKind, conceptualTableType, targetSet, fieldMetadata, targetEntity, columnMetadata);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000365E2 File Offset: 0x000347E2
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, IConceptualEntity targetEntity, IConceptualColumn columnMetadata)
		{
			return new QueryAllExpression(allKind, columnMetadata.ConceptualTypeColumn.Row().Table(), null, null, targetEntity, columnMetadata);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x00036600 File Offset: 0x00034800
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, EntitySet targetSet, IReadOnlyList<EdmField> fieldsMetadata, IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			ConceptualTableType conceptualTableType;
			if (columnsMetadata == null)
			{
				conceptualTableType = fieldsMetadata.Select((EdmField f) => f.Column).EvaluateReadOnly<ConceptualTypeColumn>().Row()
					.Table();
			}
			else
			{
				conceptualTableType = columnsMetadata.Select((IConceptualColumn c) => c.ConceptualTypeColumn).EvaluateReadOnly<ConceptualTypeColumn>().Row()
					.Table();
			}
			ConceptualTableType conceptualTableType2 = conceptualTableType;
			return new QueryAllExpression(allKind, conceptualTableType2, targetSet, fieldsMetadata, targetEntity, columnsMetadata);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x0003668C File Offset: 0x0003488C
		private static QueryAllExpression CreateAllExpr(QueryAllKind allKind, IConceptualEntity targetEntity, IReadOnlyList<IConceptualColumn> columnsMetadata)
		{
			ConceptualTableType conceptualTableType = columnsMetadata.Select((IConceptualColumn p) => p.ConceptualTypeColumn).EvaluateReadOnly<ConceptualTypeColumn>().Row()
				.Table();
			return new QueryAllExpression(allKind, conceptualTableType, null, null, targetEntity, columnsMetadata);
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000366D9 File Offset: 0x000348D9
		public static KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> As(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression value, string alias)
		{
			return new KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(alias, value);
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000366E2 File Offset: 0x000348E2
		public static QueryBatchRootExpression BatchRoot(IReadOnlyList<QueryParameterDeclarationExpression> queryParameters, IReadOnlyList<QueryBaseDeclarationExpression> declarations, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression outputTable)
		{
			return QueryExpressionBuilder.BatchRoot(queryParameters, declarations, new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] { outputTable }, outputTable.ConceptualResultType);
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000366FB File Offset: 0x000348FB
		public static QueryBatchRootExpression BatchRoot(IReadOnlyList<QueryParameterDeclarationExpression> queryParameters, IReadOnlyList<QueryBaseDeclarationExpression> declarations, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> outputTables)
		{
			return QueryExpressionBuilder.BatchRoot(queryParameters, declarations, outputTables, ConceptualPrimitiveResultType.Integer);
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x0003670A File Offset: 0x0003490A
		private static QueryBatchRootExpression BatchRoot(IReadOnlyList<QueryParameterDeclarationExpression> queryParameters, IReadOnlyList<QueryBaseDeclarationExpression> declarations, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> outputTables, ConceptualResultType conceptualResultType)
		{
			return new QueryBatchRootExpression(conceptualResultType, queryParameters, declarations, outputTables);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x00036715 File Offset: 0x00034915
		public static QueryEarlierExpression Earlier(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression column)
		{
			return column.Earlier(1);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x0003671E File Offset: 0x0003491E
		public static QueryEarlierExpression Earlier(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression column, int number)
		{
			return new QueryEarlierExpression(column, number);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x00036728 File Offset: 0x00034928
		public static QueryMeasureDeclarationExpression DeclareMeasureAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, EntitySet targetEntitySet, EdmMeasure edmMeasure, IConceptualEntity targetEntity, IConceptualMeasure measure)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression queryMeasureExpression = targetEntitySet.InvokeMeasure(edmMeasure, targetEntity, measure);
			return expression.DeclareMeasureAs(queryMeasureExpression);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x00036747 File Offset: 0x00034947
		public static QueryMeasureDeclarationExpression DeclareMeasureAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression measureRef)
		{
			return new QueryMeasureDeclarationExpression(expression, measureRef);
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x00036750 File Offset: 0x00034950
		public static QueryDataSourceVariablesDeclarationExpression DeclareDataSourceVariables(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression)
		{
			return new QueryDataSourceVariablesDeclarationExpression(expression);
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x00036758 File Offset: 0x00034958
		public static QueryMParameterDeclarationExpression DeclareMParameter(this string parameterName, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression)
		{
			return new QueryMParameterDeclarationExpression(parameterName, expression);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x00036764 File Offset: 0x00034964
		public static QueryFieldDeclarationExpression DeclareFieldAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, EntitySet targetEntitySet, EdmField field, IConceptualEntity targetEntity, IConceptualColumn column)
		{
			QueryFieldExpression queryFieldExpression = ((targetEntity != null) ? targetEntity.QdmReference().Field(column) : targetEntitySet.QdmReference(null).Field(field, null));
			return expression.DeclareFieldAs(queryFieldExpression);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x0003679C File Offset: 0x0003499C
		public static QueryFieldDeclarationExpression DeclareFieldAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, IConceptualEntity entity, string columnEdmName)
		{
			IConceptualColumn conceptualColumn = (IConceptualColumn)entity.GetPropertyByEdmName(columnEdmName);
			return expression.DeclareFieldAs(null, null, entity, conceptualColumn);
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000367C0 File Offset: 0x000349C0
		public static QueryFieldDeclarationExpression DeclareFieldAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, QueryFieldExpression fieldRef)
		{
			return new QueryFieldDeclarationExpression(expression, fieldRef);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000367C9 File Offset: 0x000349C9
		public static QueryTableDeclarationExpression DeclareTableAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, QueryVisualShape visualShape, IConceptualEntity entity, IReadOnlyList<QueryFieldDeclarationExpression> additionalColumns)
		{
			return new QueryTableDeclarationExpression(entity, expression, visualShape, additionalColumns);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000367D4 File Offset: 0x000349D4
		public static QueryVariableDeclarationExpression DeclareVariableAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, string varName)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(input, "input");
			return new QueryVariableDeclarationExpression(input, input.ConceptualResultType.Variable(varName));
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000367F4 File Offset: 0x000349F4
		public static QueryParameterDeclarationExpression DeclareParameterAs(this ConceptualResultType resultType, string name)
		{
			return new QueryParameterDeclarationExpression(resultType.Parameter(name));
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x00036802 File Offset: 0x00034A02
		public static QueryParameterReferenceExpression Parameter(this ConceptualResultType resultType, string name)
		{
			return new QueryParameterReferenceExpression(name, resultType);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x0003680C File Offset: 0x00034A0C
		public static QueryExpressionBinding BindAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, string varName)
		{
			ConceptualRowType rowType = ((ConceptualTableType)input.ConceptualResultType).RowType;
			QueryVariableReferenceExpression queryVariableReferenceExpression = new QueryVariableReferenceExpression(varName, rowType);
			return new QueryExpressionBinding(input, queryVariableReferenceExpression);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x00036839 File Offset: 0x00034A39
		public static QueryCalculateExpression Calculate(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> filters)
		{
			return QueryExpressionBuilder.CalculateExpression(argument, filters);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x00036842 File Offset: 0x00034A42
		public static QueryCalculateExpression Calculate(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument, params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] filters)
		{
			return QueryExpressionBuilder.CalculateExpression(argument, filters);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x0003684C File Offset: 0x00034A4C
		private static QueryCalculateExpression CalculateExpression(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> filters)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(argument, "argument");
			ArgumentValidation.CheckCondition(filters.EmptyIfNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>().All((Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression f) => f.ConceptualResultType is ConceptualTableType), "filters");
			QueryCalculateExpression queryCalculateExpression = argument as QueryCalculateExpression;
			if (queryCalculateExpression != null && queryCalculateExpression.Filters.Count == 0)
			{
				return queryCalculateExpression.Argument.Calculate(filters);
			}
			return new QueryCalculateExpression(argument.ConceptualResultType, argument, filters);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000368CA File Offset: 0x00034ACA
		public static QueryDistinctExpression Distinct(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			return new QueryDistinctExpression(argument.ConceptualResultType, argument);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000368D8 File Offset: 0x00034AD8
		public static QueryTypeCastExpression Cast(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, ConceptualResultType conceptualResultType)
		{
			return new QueryTypeCastExpression(input, conceptualResultType);
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000368E4 File Offset: 0x00034AE4
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ResolveCrossTypeComparisons(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression leftQueryExpression, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression rightQueryExpression)
		{
			ConceptualResultType conceptualResultType = leftQueryExpression.ConceptualResultType;
			ConceptualResultType conceptualResultType2 = rightQueryExpression.ConceptualResultType;
			ConceptualPrimitiveType? primitiveTypeKind = conceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType? primitiveTypeKind2 = conceptualResultType2.GetPrimitiveTypeKind();
			if (!((primitiveTypeKind.GetValueOrDefault() == primitiveTypeKind2.GetValueOrDefault()) & (primitiveTypeKind != null == (primitiveTypeKind2 != null))) && conceptualResultType.IsNumeric() && conceptualResultType2.IsNumeric())
			{
				rightQueryExpression = rightQueryExpression.Cast(leftQueryExpression.ConceptualResultType);
			}
			return rightQueryExpression;
		}

		// Token: 0x060012FC RID: 4860 RVA: 0x0003694F File Offset: 0x00034B4F
		public static QueryCountRowsExpression CountRows(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			return new QueryCountRowsExpression(ConceptualPrimitiveResultType.Integer, argument);
		}

		// Token: 0x060012FD RID: 4861 RVA: 0x0003695C File Offset: 0x00034B5C
		public static QueryGenerateExpression Generate(IReadOnlyList<QueryExpressionBinding> inputs)
		{
			return QueryExpressionBuilder.CreateGenerateExpr(inputs, QueryGenerateKind.Generate);
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x00036965 File Offset: 0x00034B65
		public static QueryCurrentGroupExpression CurrentGroup(this QueryGroupExpressionBinding input)
		{
			ArgumentValidation.CheckNotNull<QueryGroupExpressionBinding>(input, "input");
			return new QueryCurrentGroupExpression(input.Expression.ConceptualResultType, input.Variable);
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x00036989 File Offset: 0x00034B89
		public static QueryGenerateExpression GenerateAll(IReadOnlyList<QueryExpressionBinding> inputs)
		{
			return QueryExpressionBuilder.CreateGenerateExpr(inputs, QueryGenerateKind.GenerateAll);
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x00036994 File Offset: 0x00034B94
		private static QueryGenerateExpression CreateGenerateExpr(IReadOnlyList<QueryExpressionBinding> inputs, QueryGenerateKind generateKind)
		{
			ConceptualTableType conceptualTableType;
			try
			{
				conceptualTableType = inputs.Select((QueryExpressionBinding q) => q.Variable.ConceptualResultType).MergeRows(ColumnMergingBehavior.Disallow).Table();
			}
			catch (ArgumentException ex)
			{
				throw new QueryJoinException("Error merging columns for GenerateExpression: " + ex.Message, ex);
			}
			return new QueryGenerateExpression(generateKind, conceptualTableType, inputs);
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x00036A04 File Offset: 0x00034C04
		public static QueryCrossJoinExpression CrossJoin(params QueryExpressionBinding[] inputs)
		{
			return QueryExpressionBuilder.CrossJoin(inputs);
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x00036A0C File Offset: 0x00034C0C
		public static QueryCrossJoinExpression CrossJoin(IReadOnlyList<QueryExpressionBinding> inputs)
		{
			return new QueryCrossJoinExpression(inputs.Select((QueryExpressionBinding q) => q.Variable.ConceptualResultType).MergeRows(ColumnMergingBehavior.Disallow).Table(), inputs);
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x00036A44 File Offset: 0x00034C44
		internal static QueryExpressionWithLocalVariables CreateExpressionWithLocalVariables(IReadOnlyList<QueryVariableDeclarationExpression> declarations, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression result)
		{
			return new QueryExpressionWithLocalVariables(result.ConceptualResultType, declarations, result);
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x00036A54 File Offset: 0x00034C54
		public static QueryImplicitJoinWithProjectionExpression ImplicitJoinWithProjection(QueryExpressionBinding primaryTable, IReadOnlyList<ImplicitJoinSecondaryTable> secondaryTables)
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>();
			foreach (ImplicitJoinSecondaryTable implicitJoinSecondaryTable in secondaryTables)
			{
				foreach (KeyValuePair<string, QueryFieldExpression> keyValuePair in implicitJoinSecondaryTable.KeyColumns)
				{
					list.Add(keyValuePair.Value.ConceptualResultType.Column(keyValuePair.Key, keyValuePair.Key));
				}
			}
			return new QueryImplicitJoinWithProjectionExpression(list.Row().Table(), primaryTable, secondaryTables);
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x00036B08 File Offset: 0x00034D08
		public static QueryNaturalJoinExpression NaturalJoin(NaturalJoinKind joinKind, QueryExpressionBinding left, QueryExpressionBinding right)
		{
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(left, "left");
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(right, "right");
			ConceptualTableType conceptualTableType;
			try
			{
				conceptualTableType = left.Variable.ConceptualResultType.MergeRows(right.Variable.ConceptualResultType, ColumnMergingBehavior.Merge).Table();
			}
			catch (ArgumentException ex)
			{
				throw new QueryJoinException("Error merging columns for NaturalJoin: " + ex.Message, ex);
			}
			return new QueryNaturalJoinExpression(joinKind, conceptualTableType, left, right);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x00036B84 File Offset: 0x00034D84
		public static QueryUnionAllExpression UnionAll(IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> tables, TypeUnionBehavior typeUnionBehavior = TypeUnionBehavior.Upcast)
		{
			Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression = tables[0];
			for (int i = 1; i < tables.Count; i++)
			{
				Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression queryExpression2 = tables[i];
			}
			return new QueryUnionAllExpression(queryExpression.ConceptualResultType, tables, typeUnionBehavior);
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x00036BC0 File Offset: 0x00034DC0
		public static QueryAddMissingItemsExpression AddMissingItems(IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> showAllColumns, QueryExpressionBinding table, IEnumerable<IAddMissingItemsGroupItem> groups, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> contextTables)
		{
			ArgumentValidation.CheckNotNullOrEmpty<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(showAllColumns, "showAllColumns");
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(table, "table");
			ArgumentValidation.CheckNotNullOrEmpty<IAddMissingItemsGroupItem>(groups, "groups");
			ArgumentValidation.CheckNotNull<IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>(contextTables, "contextTables");
			return new QueryAddMissingItemsExpression(table.Expression.ConceptualResultType, showAllColumns, table, groups, contextTables);
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x00036C14 File Offset: 0x00034E14
		public static QuerySubstituteWithIndexExpression SubstituteWithIndex(this QueryExpressionBinding table, string indexColumnName, QueryExpressionBinding indexTable, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> indexTableSortOrder)
		{
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(table, "table");
			ArgumentValidation.CheckNotNullOrEmpty(indexColumnName, "indexColumnName");
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(indexTable, "indexTable");
			ArgumentValidation.CheckNotNull<IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause>>(indexTableSortOrder, "indexTableSortOrder");
			ReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> readOnlyCollection = indexTableSortOrder.ToReadOnlyCollection<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause>();
			ConceptualTypeColumn conceptualTypeColumn = ConceptualPrimitiveResultType.Integer.Column(indexColumnName, null);
			return new QuerySubstituteWithIndexExpression(DaxFunctions.DetermineSubstituteWithIndexResultColumns<ConceptualTypeColumn>(QueryExpressionBuilder.GetTableConceptualColumns(table.Expression), conceptualTypeColumn, QueryExpressionBuilder.GetTableConceptualColumns(indexTable.Expression), (ConceptualTypeColumn c) => c.Name).ToList<ConceptualTypeColumn>().Row()
				.Table(), table, indexColumnName, indexTable, readOnlyCollection);
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x00036CB8 File Offset: 0x00034EB8
		private static IEnumerable<ConceptualTypeColumn> GetTableConceptualColumns(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression tableExpr)
		{
			return (tableExpr.ConceptualResultType as ConceptualTableType).RowType.Columns;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x00036CCF File Offset: 0x00034ECF
		public static QueryDaxTextExpression DaxText(ConceptualResultType conceptualResultType, string text)
		{
			return new QueryDaxTextExpression(conceptualResultType, text);
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x00036CD8 File Offset: 0x00034ED8
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression Equal(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.Equals);
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x00036CFA File Offset: 0x00034EFA
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression EqualIdentity(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.EqualsIdentity);
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x00036D1C File Offset: 0x00034F1C
		private static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression Compare(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind comparisonKind)
		{
			QueryExpressionValidation.ValidateComparison(comparisonKind, left.ConceptualResultType, right.ConceptualResultType);
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression(comparisonKind, ConceptualPrimitiveResultType.Boolean, left, right);
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x00036D3D File Offset: 0x00034F3D
		public static QueryFieldExpression Field(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression instance, ConceptualTypeColumn column)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(instance, "instance");
			ArgumentValidation.CheckNotNull<ConceptualTypeColumn>(column, "column");
			return QueryExpressionBuilder.CreateFieldExpr(instance, column);
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x00036D5E File Offset: 0x00034F5E
		public static QueryFieldExpression Field(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression instance, IConceptualColumn column)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(instance, "instance");
			ArgumentValidation.CheckNotNull<IConceptualColumn>(column, "column");
			return QueryExpressionBuilder.CreateFieldExpr(instance, column.ConceptualTypeColumn);
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x00036D84 File Offset: 0x00034F84
		public static QueryFieldExpression Field(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression instance, EdmField fieldMetadata, IConceptualColumn column)
		{
			return QueryExpressionBuilder.CreateFieldExpr(instance, ((column != null) ? column.ConceptualTypeColumn : null) ?? fieldMetadata.Column);
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x00036DA4 File Offset: 0x00034FA4
		public static QueryFieldExpression Field(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression instance, string fieldEdmName)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(instance, "instance");
			ConceptualTypeColumn columnByEdmName = instance.ConceptualResultType.GetRowType().GetColumnByEdmName(fieldEdmName);
			if (columnByEdmName == null)
			{
				Microsoft.DataShaping.Contract.RetailFail("Expected column {0} to be in the input instance", fieldEdmName.MarkAsCustomerContent());
			}
			return QueryExpressionBuilder.CreateFieldExpr(instance, columnByEdmName);
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x00036DE9 File Offset: 0x00034FE9
		public static QueryFieldReferenceNameExpression FieldReferenceName(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression table, string fieldEdmName)
		{
			Microsoft.DataShaping.Contract.RetailAssert(table.ConceptualResultType.GetRowType().GetColumn(fieldEdmName) != null, "Could not find field in input expression.");
			return new QueryFieldReferenceNameExpression(table, fieldEdmName, ConceptualPrimitiveResultType.Text);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x00036E15 File Offset: 0x00035015
		private static QueryFieldExpression CreateFieldExpr(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression instance, ConceptualTypeColumn column)
		{
			return new QueryFieldExpression(instance, column);
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x00036E1E File Offset: 0x0003501E
		public static QueryFilterExpression Filter(this QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression predicate)
		{
			return new QueryFilterExpression(input.Expression.ConceptualResultType, input, predicate);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x00036E32 File Offset: 0x00035032
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression GreaterThan(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.GreaterThan);
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x00036E54 File Offset: 0x00035054
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression GreaterThanOrEqual(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.GreaterThanOrEquals);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x00036E76 File Offset: 0x00035076
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryInExpression In(this IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> expressions, IReadOnlyList<IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> values, bool isStrict)
		{
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryInExpression(ConceptualPrimitiveResultType.Boolean, expressions, values, isStrict);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x00036E85 File Offset: 0x00035085
		public static QueryInTableExpression InTable(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			return new QueryInTableExpression(ConceptualPrimitiveResultType.Boolean, left, right);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x00036E94 File Offset: 0x00035094
		public static QueryGroupExpressionBinding GroupBindAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, string varName)
		{
			ConceptualRowType rowType = ((ConceptualTableType)input.ConceptualResultType).RowType;
			QueryVariableReferenceExpression queryVariableReferenceExpression = new QueryVariableReferenceExpression(varName, rowType);
			return new QueryGroupExpressionBinding(input, queryVariableReferenceExpression);
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x00036EC1 File Offset: 0x000350C1
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> keys)
		{
			return input.GroupBy(QueryExpressionBuilder.GroupItemsFromKeyPairs(keys), null);
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x00036ED0 File Offset: 0x000350D0
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, params KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>[] keys)
		{
			return input.GroupBy(QueryExpressionBuilder.GroupItemsFromKeyPairs(keys), null);
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x00036EDF File Offset: 0x000350DF
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> keys, IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> aggregates)
		{
			return input.GroupBy(QueryExpressionBuilder.GroupItemsFromKeyPairs(keys), aggregates);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x00036EEE File Offset: 0x000350EE
		private static IEnumerable<IGroupItem> GroupItemsFromKeyPairs(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> keys)
		{
			keys = QueryExpressionBuilder.NormalizeKeyPairSequence(keys);
			return keys.Select((KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> k) => new CompositeKeyGroupItem(new KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>[] { k })).ToArray<CompositeKeyGroupItem>();
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00036F22 File Offset: 0x00035122
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, IEnumerable<IGroupItem> groupItems)
		{
			return input.GroupBy(groupItems, null);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x00036F2C File Offset: 0x0003512C
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, params IGroupItem[] groupItems)
		{
			return input.GroupBy(groupItems, null);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x00036F38 File Offset: 0x00035138
		public static QueryGroupByExpression GroupBy(this QueryGroupExpressionBinding input, IEnumerable<IGroupItem> groupItems, IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> aggregates)
		{
			groupItems = groupItems ?? Microsoft.Reporting.Util.EmptyArray<IGroupItem>();
			aggregates = aggregates ?? Microsoft.Reporting.Util.EmptyArray<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>();
			List<ConceptualTypeColumn> list = groupItems.GetGroupKeyColumns().ToList<ConceptualTypeColumn>();
			List<ConceptualTypeColumn> list2 = aggregates.Select((KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> kvp) => kvp.Value.ConceptualResultType.Column(kvp.Key, null)).ToList<ConceptualTypeColumn>();
			QueryExpressionValidation.ValidateGroupBy(list, list2);
			return new QueryGroupByExpression(list.Concat(list2).ToList<ConceptualTypeColumn>().Row()
				.Table(), input, groupItems, aggregates);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00036FB8 File Offset: 0x000351B8
		public static QueryGroupAndJoinExpression GroupAndJoin(IEnumerable<IGroupItem> groupItems, IEnumerable<QueryGroupAndJoinAdditionalColumn> additionalColumns, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> contextTables)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<IGroupItem>>(groupItems, "groupItems");
			ArgumentValidation.CheckNotNull<IEnumerable<QueryGroupAndJoinAdditionalColumn>>(additionalColumns, "additionalColumns");
			ArgumentValidation.CheckCondition(contextTables.All((Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ct) => ct.ConceptualResultType is ConceptualTableType), "contextTables");
			if (!groupItems.Any<IGroupItem>() && !additionalColumns.Any<QueryGroupAndJoinAdditionalColumn>())
			{
				throw new QueryFunctionInvocationException("At least one column must be specified when constructing a GroupAndJoin.");
			}
			IEnumerable<ConceptualTypeColumn> groupKeyColumns = groupItems.GetGroupKeyColumns();
			IEnumerable<ConceptualTypeColumn> subtotalIndicatorColumns = groupItems.GetSubtotalIndicatorColumns();
			IEnumerable<ConceptualTypeColumn> enumerable = additionalColumns.Select((QueryGroupAndJoinAdditionalColumn c) => c.Expression.ConceptualResultType.Column(c.Name, null));
			return new QueryGroupAndJoinExpression(groupKeyColumns.Concat(subtotalIndicatorColumns).Concat(enumerable).ToList<ConceptualTypeColumn>()
				.Row()
				.Table(), groupItems, additionalColumns, contextTables);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003707D File Offset: 0x0003527D
		private static IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> NormalizeKeyPairSequence(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> pairs)
		{
			return pairs ?? Enumerable.Empty<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>();
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x00037089 File Offset: 0x00035289
		public static QueryIsAggregateExpression IsAggregate(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(argument, "argument");
			return new QueryIsAggregateExpression(ConceptualPrimitiveResultType.Boolean, argument);
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000370A2 File Offset: 0x000352A2
		public static QueryIsNullExpression IsNull(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			QueryExpressionValidation.ValidateIsNullExpressionArgument(argument.ConceptualResultType);
			return new QueryIsNullExpression(ConceptualPrimitiveResultType.Boolean, argument);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000370BA File Offset: 0x000352BA
		public static QueryIsEmptyExpression IsEmpty(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(argument, "argument");
			return new QueryIsEmptyExpression(ConceptualPrimitiveResultType.Boolean, argument);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000370D3 File Offset: 0x000352D3
		public static QueryIsOnOrAfterExpression IsOnOrAfter(QueryIsOnOrAfterArgument first, params QueryIsOnOrAfterArgument[] rest)
		{
			ArgumentValidation.CheckNotNull<QueryIsOnOrAfterArgument>(first, "first");
			ArgumentValidation.CheckNotNull<QueryIsOnOrAfterArgument[]>(rest, "rest");
			return QueryExpressionBuilder.IsOnOrAfter(rest.Prepend(first));
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000370F9 File Offset: 0x000352F9
		public static QueryIsAfterExpression IsAfter(QueryIsOnOrAfterArgument first, params QueryIsOnOrAfterArgument[] rest)
		{
			ArgumentValidation.CheckNotNull<QueryIsOnOrAfterArgument>(first, "first");
			ArgumentValidation.CheckNotNull<QueryIsOnOrAfterArgument[]>(rest, "rest");
			return QueryExpressionBuilder.IsAfter(rest.Prepend(first));
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x0003711F File Offset: 0x0003531F
		public static QueryDateDiffExpression DateDiff(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression startDate, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression endDate, TimeUnit timeUnit)
		{
			return new QueryDateDiffExpression(ConceptualPrimitiveResultType.Integer, startDate, endDate, timeUnit);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x00037130 File Offset: 0x00035330
		public static QuerySampleAxisWithLocalMinMaxExpression SampleAxisWithLocalMinMax(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxTargetPointCount, QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression axis, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> measures, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression minPointsResolution, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> series, DynamicSeriesSelectionCriteria dynamicSeriesSelectionCriteria, SortDirection dynamicSeriesSelectionCriteriaOrder, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxPointsResolution, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxDynamicSeriesCount)
		{
			return new QuerySampleAxisWithLocalMinMaxExpression(QueryExpressionBuilder.CreateLimitResultType(input), maxTargetPointCount, input, axis, measures, minPointsResolution, series, dynamicSeriesSelectionCriteria, dynamicSeriesSelectionCriteriaOrder, maxPointsResolution, maxDynamicSeriesCount);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x00037158 File Offset: 0x00035358
		public static QuerySampleCartesianPointsByCoverExpression SampleCartesianPointsByCover(Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxTargetPointCount, QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression x, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression y, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression radius, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxMinRatio, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression maxBlankRatio)
		{
			return new QuerySampleCartesianPointsByCoverExpression(QueryExpressionBuilder.CreateLimitResultType(input), maxTargetPointCount, input, x, y, radius, maxMinRatio, maxBlankRatio);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x00037170 File Offset: 0x00035370
		public static QueryTopNPerLevelSampleExpression TopNPerLevel(this QueryExpressionBinding input, IReadOnlyList<TopNPerLevelLevelRow> levels, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression count, string restartIndicatorColumnName, QueryTopNPerLevelWindowExpansion windowExpansionInfo)
		{
			ConceptualTypeColumn conceptualTypeColumn = ConceptualPrimitiveResultType.Integer.Column(restartIndicatorColumnName, null);
			return new QueryTopNPerLevelSampleExpression(QueryExpressionBuilder.GetTableConceptualColumns(input.Expression).Concat(new ConceptualTypeColumn[] { conceptualTypeColumn }).ToList<ConceptualTypeColumn>()
				.Row()
				.Table(), input, levels, count, restartIndicatorColumnName, windowExpansionInfo);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000371BE File Offset: 0x000353BE
		public static QueryIsOnOrAfterExpression IsOnOrAfter(IEnumerable<QueryIsOnOrAfterArgument> arguments)
		{
			return new QueryIsOnOrAfterExpression(ConceptualPrimitiveResultType.Boolean, arguments);
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000371CB File Offset: 0x000353CB
		public static QueryIsAfterExpression IsAfter(IEnumerable<QueryIsOnOrAfterArgument> arguments)
		{
			return new QueryIsAfterExpression(ConceptualPrimitiveResultType.Boolean, arguments);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000371D8 File Offset: 0x000353D8
		public static QueryLookupValueExpression LookupValue(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression resultColumn, IReadOnlyList<QueryLookupTuple> lookupTuples)
		{
			return new QueryLookupValueExpression(resultColumn.ConceptualResultType, resultColumn, lookupTuples);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000371E7 File Offset: 0x000353E7
		public static QuerySwitchExpression Switch(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, IReadOnlyList<QuerySwitchCase> cases)
		{
			return input.Switch(cases, null);
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000371F4 File Offset: 0x000353F4
		public static QuerySwitchExpression Switch(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, IReadOnlyList<QuerySwitchCase> cases, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression defaultResult)
		{
			ConceptualResultType conceptualResultType;
			if (defaultResult != null)
			{
				conceptualResultType = defaultResult.ConceptualResultType;
			}
			else
			{
				conceptualResultType = cases[0].Result.ConceptualResultType;
			}
			return new QuerySwitchExpression(conceptualResultType, input, cases, defaultResult);
		}

		// Token: 0x06001331 RID: 4913 RVA: 0x00037228 File Offset: 0x00035428
		public static QueryExtensionFunctionExpression InvokeExtensionFunction(ConceptualResultType conceptualResultType, string functionName, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Name", "Index" })] IReadOnlyList<global::System.ValueTuple<string, int>> resultColumnSourceArgumentIndices)
		{
			return new QueryExtensionFunctionExpression(conceptualResultType, functionName, arguments, resultColumnSourceArgumentIndices);
		}

		// Token: 0x06001332 RID: 4914 RVA: 0x00037233 File Offset: 0x00035433
		public static QueryFunctionExpression Invoke(this EdmFunction function, params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] arguments)
		{
			return function.InvokeFunction(arguments);
		}

		// Token: 0x06001333 RID: 4915 RVA: 0x0003723C File Offset: 0x0003543C
		public static QueryFunctionExpression Invoke(this EdmFunction function, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments)
		{
			return function.InvokeFunction(arguments);
		}

		// Token: 0x06001334 RID: 4916 RVA: 0x00037248 File Offset: 0x00035448
		private static QueryFunctionExpression InvokeFunction(this EdmFunction function, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments)
		{
			ArgumentValidation.CheckNotNull<EdmFunction>(function, "function");
			return QueryExpressionBuilder.InvokeFunction<EdmFunction>((EdmFunction func, IReadOnlyList<ConceptualResultType> expressions) => function, function, arguments);
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0003728C File Offset: 0x0003548C
		internal static QueryFunctionExpression InvokeFunction<TFunctionMetadata>(Func<TFunctionMetadata, IReadOnlyList<ConceptualResultType>, EdmFunction> invokeMethod, TFunctionMetadata functionMetadata, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments)
		{
			arguments = arguments ?? Microsoft.Reporting.Util.EmptyArray<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>();
			List<ConceptualResultType> list = arguments.Select((Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression a) => a.ConceptualResultType).ToList<ConceptualResultType>();
			EdmFunction edmFunction;
			try
			{
				edmFunction = invokeMethod(functionMetadata, list);
				QueryExpressionValidation.ValidateFunction(edmFunction, list);
			}
			catch (ArgumentException ex)
			{
				throw new QueryFunctionInvocationException(ex);
			}
			return new QueryFunctionExpression(edmFunction, arguments);
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x00037300 File Offset: 0x00035500
		internal static QueryOperatorExpression InvokeOperator(this EdmOperator @operator, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments, bool useBinaryEquivalent)
		{
			ArgumentValidation.CheckNotNull<EdmOperator>(@operator, "operator");
			return QueryExpressionBuilder.InvokeOperator<EdmOperator>((EdmOperator op, IReadOnlyList<ConceptualResultType> expressions) => @operator, @operator, arguments, useBinaryEquivalent);
		}

		// Token: 0x06001337 RID: 4919 RVA: 0x00037344 File Offset: 0x00035544
		internal static QueryOperatorExpression InvokeOperator<TOperatorMetadata>(Func<TOperatorMetadata, IReadOnlyList<ConceptualResultType>, EdmOperator> invokeMethod, TOperatorMetadata functionMetadata, IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments, bool useBinaryEquivalent)
		{
			arguments = arguments ?? Microsoft.Reporting.Util.EmptyArray<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>();
			List<ConceptualResultType> list = arguments.Select((Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression a) => a.ConceptualResultType).ToList<ConceptualResultType>();
			EdmOperator edmOperator;
			try
			{
				edmOperator = invokeMethod(functionMetadata, list);
				QueryExpressionValidation.ValidateOperator(edmOperator, list);
			}
			catch (ArgumentException ex)
			{
				throw new QueryFunctionInvocationException(ex);
			}
			return new QueryOperatorExpression(edmOperator, arguments, useBinaryEquivalent);
		}

		// Token: 0x06001338 RID: 4920 RVA: 0x000373B8 File Offset: 0x000355B8
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this EdmMeasureInstance measureInstance, IConceptualMeasure targetMeasure)
		{
			return measureInstance.Entity.InvokeMeasure(measureInstance.Measure, (targetMeasure != null) ? targetMeasure.Entity : null, targetMeasure);
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x000373DA File Offset: 0x000355DA
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this IConceptualMeasure measure)
		{
			return measure.Entity.InvokeMeasure(measure);
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x000373E8 File Offset: 0x000355E8
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this EntitySet targetSet, EdmMeasure measureMetadata, IConceptualEntity targetEntity, IConceptualMeasure targetMeasure)
		{
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression((targetEntity != null) ? ConceptualPrimitiveResultType.FromPrimitive(targetMeasure.ConceptualDataType) : measureMetadata.ConceptualType, targetSet, measureMetadata, targetEntity, targetMeasure);
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0003740C File Offset: 0x0003560C
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this EntitySet targetSet, string measureName, IConceptualEntity targetEntity)
		{
			EdmMeasure edmMeasure = targetSet.ElementType.Members[measureName] as EdmMeasure;
			IConceptualProperty conceptualProperty = null;
			if (targetEntity != null && !targetEntity.TryGetPropertyByEdmName(measureName, out conceptualProperty))
			{
				Microsoft.DataShaping.Contract.RetailFail("Unable to find specified field");
			}
			return targetSet.InvokeMeasure(edmMeasure, targetEntity, conceptualProperty as IConceptualMeasure);
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x00037458 File Offset: 0x00035658
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this IConceptualEntity targetEntity, IConceptualMeasure targetMeasure)
		{
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression(ConceptualPrimitiveResultType.FromPrimitive(targetMeasure.ConceptualDataType), null, null, targetEntity, targetMeasure);
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x00037470 File Offset: 0x00035670
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this EntitySet targetSet, string measureName)
		{
			EdmMeasure edmMeasure = targetSet.ElementType.Members[measureName] as EdmMeasure;
			return targetSet.InvokeMeasure(edmMeasure, null, null);
		}

		// Token: 0x0600133E RID: 4926 RVA: 0x000374A0 File Offset: 0x000356A0
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryMeasureExpression InvokeMeasure(this EntitySet targetSet, IConceptualMeasure targetMeasure)
		{
			EdmMeasure edmMeasure = targetSet.ElementType.Members[targetMeasure.EdmName] as EdmMeasure;
			return targetSet.InvokeMeasure(edmMeasure, targetMeasure.Entity, targetMeasure);
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x000374D7 File Offset: 0x000356D7
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression LessThan(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.LessThan);
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x000374F9 File Offset: 0x000356F9
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression LessThanOrEqual(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.LessThanOrEquals);
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0003751B File Offset: 0x0003571B
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryLiteralExpression Literal(ScalarValue value)
		{
			return value.Type.GetPrimitive().Literal(value);
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x00037530 File Offset: 0x00035730
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryLiteralExpression Literal(this ConceptualResultType conceptualResultType, ScalarValue value)
		{
			ConceptualPrimitiveResultType primitive = value.Type.GetPrimitive();
			Microsoft.DataShaping.Contract.RetailAssert(primitive.Equals(conceptualResultType), "Specified conceptualResultType {0} must match the type of the value {1}", conceptualResultType, primitive);
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryLiteralExpression(value, conceptualResultType);
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x00037564 File Offset: 0x00035764
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ToExpression(this ConceptualResultType constantType, ScalarValue value)
		{
			ArgumentValidation.CheckNotNull<ConceptualResultType>(constantType, "constantType");
			if (value == ScalarValue.Null)
			{
				return constantType.Null();
			}
			return constantType.Literal(value);
		}

		// Token: 0x06001344 RID: 4932 RVA: 0x0003758D File Offset: 0x0003578D
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression ToExpressionFromDeclaredType(ScalarValue value, ConceptualResultType nullType)
		{
			if (value == ScalarValue.Null)
			{
				return nullType.Null();
			}
			return QueryExpressionBuilder.Literal(value);
		}

		// Token: 0x06001345 RID: 4933 RVA: 0x000375A9 File Offset: 0x000357A9
		public static QueryNewInstanceExpression New(this ConceptualResultType conceptualType, params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] arguments)
		{
			return QueryExpressionBuilder.NewInstance(conceptualType, arguments);
		}

		// Token: 0x06001346 RID: 4934 RVA: 0x000375B4 File Offset: 0x000357B4
		private static QueryNewInstanceExpression NewInstance(ConceptualResultType conceptualType, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> arguments)
		{
			arguments = arguments ?? Microsoft.Reporting.Util.EmptyArray<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>();
			IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> enumerable = ((ConceptualRowType)conceptualType).Columns.Select((ConceptualTypeColumn c) => c.Name).Zip(arguments);
			return new QueryNewInstanceExpression(conceptualType, enumerable);
		}

		// Token: 0x06001347 RID: 4935 RVA: 0x0003760A File Offset: 0x0003580A
		public static QueryNewInstanceExpression NewRow(params KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>[] columnValues)
		{
			return QueryExpressionBuilder.CreateNewRowExpr(columnValues);
		}

		// Token: 0x06001348 RID: 4936 RVA: 0x00037612 File Offset: 0x00035812
		public static QueryNewInstanceExpression NewRow(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> columnValues)
		{
			return QueryExpressionBuilder.CreateNewRowExpr(columnValues);
		}

		// Token: 0x06001349 RID: 4937 RVA: 0x0003761A File Offset: 0x0003581A
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression NotEqual(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.NotEquals);
		}

		// Token: 0x0600134A RID: 4938 RVA: 0x0003763C File Offset: 0x0003583C
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonExpression NotEqualIdentity(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression left, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression right)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(left, "left");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(right, "right");
			return QueryExpressionBuilder.Compare(left, right, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryComparisonKind.NotEqualsIdentity);
		}

		// Token: 0x0600134B RID: 4939 RVA: 0x0003765E File Offset: 0x0003585E
		public static QueryNullExpression Null(this ConceptualResultType conceptualResultType)
		{
			return new QueryNullExpression(conceptualResultType);
		}

		// Token: 0x0600134C RID: 4940 RVA: 0x00037668 File Offset: 0x00035868
		private static QueryNewInstanceExpression CreateNewRowExpr(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> columnValues)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>>(columnValues, "columnValues");
			return new QueryNewInstanceExpression(columnValues.Select((KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> kvp) => kvp.Value.ConceptualResultType.Column(kvp.Key, null)).ToList<ConceptualTypeColumn>().Row(), columnValues);
		}

		// Token: 0x0600134D RID: 4941 RVA: 0x000376B6 File Offset: 0x000358B6
		public static QueryNewTableExpression NewTable(params KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>[] columns)
		{
			return QueryExpressionBuilder.CreateNewTableExpr(columns);
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000376BE File Offset: 0x000358BE
		public static QueryNewTableExpression NewTable(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> columns)
		{
			return QueryExpressionBuilder.CreateNewTableExpr(columns);
		}

		// Token: 0x0600134F RID: 4943 RVA: 0x000376C8 File Offset: 0x000358C8
		private static QueryNewTableExpression CreateNewTableExpr(IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> columns)
		{
			ArgumentValidation.CheckNotNull<IEnumerable<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>>(columns, "columns");
			return new QueryNewTableExpression(columns.Select((KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> kvp) => kvp.Value.ConceptualResultType.Column(kvp.Key, null)).ToList<ConceptualTypeColumn>().Row()
				.Table(), columns);
		}

		// Token: 0x06001350 RID: 4944 RVA: 0x0003771B File Offset: 0x0003591B
		public static QueryNonVisualExpression NonVisual(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			return new QueryNonVisualExpression(argument.ConceptualResultType, argument);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x0003772C File Offset: 0x0003592C
		public static QueryProjectExpression Project(this QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression projection, ProjectSubsetStrategy projectSubsetStrategy = ProjectSubsetStrategy.Default)
		{
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(input, "input");
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(projection, "projection");
			ConceptualResultType conceptualResultType = projection.ConceptualResultType;
			ConceptualPrimitiveResultType conceptualPrimitiveResultType = projection.ConceptualResultType as ConceptualPrimitiveResultType;
			if (conceptualPrimitiveResultType != null)
			{
				conceptualResultType = ConceptualCollectionType.FromPrimitive(conceptualPrimitiveResultType);
			}
			ConceptualRowType conceptualRowType = projection.ConceptualResultType as ConceptualRowType;
			if (conceptualRowType != null)
			{
				conceptualResultType = conceptualRowType.Table();
			}
			return new QueryProjectExpression(conceptualResultType, input, projection, projectSubsetStrategy);
		}

		// Token: 0x06001352 RID: 4946 RVA: 0x0003778D File Offset: 0x0003598D
		public static QueryEnsureUniqueUnqualifiedNamesExpression EnsureUniqueUnqualifiedNames(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression table, bool forceRename)
		{
			return new QueryEnsureUniqueUnqualifiedNamesExpression(table, forceRename);
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x00037796 File Offset: 0x00035996
		public static QueryRelatedColumnExpression RelatedColumn(EdmFieldInstance field, IConceptualColumn column)
		{
			return new QueryRelatedColumnExpression(field.QdmReference(column).ConceptualResultType, field, column);
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000377AB File Offset: 0x000359AB
		public static QueryRelatedColumnExpression RelatedColumn(IConceptualColumn column)
		{
			return new QueryRelatedColumnExpression(column.QdmReference().ConceptualResultType, EdmFieldInstance.Empty, column);
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000377C3 File Offset: 0x000359C3
		public static QueryScanExpression Scan(this EntitySet targetSet, bool excludeBlankRow = false)
		{
			return new QueryScanExpression(targetSet.ElementType.ConceptualType.Table(), targetSet, excludeBlankRow, null);
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000377DD File Offset: 0x000359DD
		public static QueryScanExpression Scan(this IConceptualEntity targetEntity, bool excludeBlankRow = false)
		{
			return new QueryScanExpression(targetEntity.GetExtensionAwareResultType(), null, excludeBlankRow, targetEntity);
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000377ED File Offset: 0x000359ED
		public static QueryScalarEntityReferenceExpression ScalarEntity(this EntitySet entitySet, IConceptualEntity targetEntity = null)
		{
			return new QueryScalarEntityReferenceExpression(entitySet.ElementType.ConceptualType, entitySet, targetEntity);
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x00037801 File Offset: 0x00035A01
		public static QueryScalarEntityReferenceExpression ScalarEntity(this IConceptualEntity targetEntity)
		{
			return new QueryScalarEntityReferenceExpression(targetEntity.GetExtensionAwareRowResultType(), null, targetEntity);
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x00037810 File Offset: 0x00035A10
		public static QuerySingleValueExpression SingleValue(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression argument)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(argument, "argument");
			return new QuerySingleValueExpression(((ConceptualTableType)argument.ConceptualResultType).RowType.Columns.Single("Expected only 1 column", Array.Empty<string>()).PrimitiveType, argument);
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x0003784D File Offset: 0x00035A4D
		public static QuerySortExpression Sort(this QueryExpressionBinding input, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return QueryExpressionBuilder.SortInput(input, sortOrder);
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x00037856 File Offset: 0x00035A56
		public static QuerySortExpression Sort(this QueryExpressionBinding input, params Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause[] sortOrder)
		{
			return QueryExpressionBuilder.SortInput(input, sortOrder);
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x0003785F File Offset: 0x00035A5F
		private static QuerySortExpression SortInput(QueryExpressionBinding input, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			ArgumentValidation.CheckNotNull<QueryExpressionBinding>(input, "input");
			ArgumentValidation.CheckNotNullOrEmpty<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause>(sortOrder, "sortOrder");
			return new QuerySortExpression(input.Expression.ConceptualResultType, input, sortOrder);
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0003788B File Offset: 0x00035A8B
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause ToSortClauseDescending(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression key)
		{
			return key.ToSortClause(SortDirection.Descending);
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x00037894 File Offset: 0x00035A94
		public static Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause ToSortClause(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression key, SortDirection direction = SortDirection.Ascending)
		{
			QueryExpressionValidation.ValidateSortClause(key.ConceptualResultType);
			return new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause(direction, key);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000378A8 File Offset: 0x00035AA8
		public static QueryLimitExpression Sample(this QueryExpressionBinding input, int count, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return input.Sample(QueryExpressionBuilder.Literal(count), sortOrder);
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000378BC File Offset: 0x00035ABC
		public static QueryLimitExpression Sample(this QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression count, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			ConceptualResultType conceptualResultType = QueryExpressionBuilder.CreateLimitResultType(input);
			return new QueryLimitExpression(QueryLimitOperator.Sample, conceptualResultType, input, count, null, sortOrder);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000378DB File Offset: 0x00035ADB
		public static QueryLimitExpression TopNSkip(this QueryExpressionBinding input, int count, long skipCount, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return input.TopNSkip(QueryExpressionBuilder.Literal(count), QueryExpressionBuilder.Literal(skipCount), sortOrder);
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000378FC File Offset: 0x00035AFC
		public static QueryLimitExpression TopNSkip(this QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression count, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression skipCount, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			ConceptualResultType conceptualResultType = QueryExpressionBuilder.CreateLimitResultType(input);
			return new QueryLimitExpression(QueryLimitOperator.TopNSkip, conceptualResultType, input, count, skipCount, sortOrder);
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x0003791B File Offset: 0x00035B1B
		public static QueryLimitExpression TopN(this QueryExpressionBinding input, int count, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			return input.TopN(QueryExpressionBuilder.Literal(count), sortOrder);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x00037930 File Offset: 0x00035B30
		public static QueryLimitExpression TopN(this QueryExpressionBinding input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression count, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause> sortOrder)
		{
			ConceptualResultType conceptualResultType = QueryExpressionBuilder.CreateLimitResultType(input);
			return new QueryLimitExpression(QueryLimitOperator.TopN, conceptualResultType, input, count, null, sortOrder);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x0003794F File Offset: 0x00035B4F
		private static ConceptualResultType CreateLimitResultType(QueryExpressionBinding input)
		{
			return input.Expression.ConceptualResultType;
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x0003795C File Offset: 0x00035B5C
		public static QueryVariableReferenceExpression Variable(this ConceptualResultType conceptualResultType, string name)
		{
			ArgumentValidation.CheckNotNull<ConceptualResultType>(conceptualResultType, "conceptualResultType");
			return new QueryVariableReferenceExpression(name, conceptualResultType);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x00037971 File Offset: 0x00035B71
		public static QueryStartAtExpression StartAt(this QuerySortExpression input, IEnumerable<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> values)
		{
			ArgumentValidation.CheckNotNull<QuerySortExpression>(input, "input");
			ArgumentValidation.CheckNotNullOrEmpty<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(values, "values");
			return new QueryStartAtExpression(input, values);
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x00037994 File Offset: 0x00035B94
		public static QueryDataTableExpression DataTable(IReadOnlyList<QueryTupleExpression> rows, IReadOnlyList<string> columnNames, IReadOnlyList<ConceptualPrimitiveResultType> columnPrimitiveTypes)
		{
			List<ConceptualTypeColumn> list = new List<ConceptualTypeColumn>(columnNames.Count);
			for (int i = 0; i < columnNames.Count; i++)
			{
				list.Add(columnPrimitiveTypes[i].Column(columnNames[i], null));
			}
			return new QueryDataTableExpression(list.Row().Table(), rows, columnNames);
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000379EC File Offset: 0x00035BEC
		public static QueryTupleExpression Tuple(IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> columns, IReadOnlyList<string> columnNames)
		{
			List<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> list = new List<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>>(columns.Count);
			List<ConceptualTypeColumn> list2 = new List<ConceptualTypeColumn>(columns.Count);
			for (int i = 0; i < columns.Count; i++)
			{
				list.Add(Microsoft.DataShaping.Util.ToKeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(columnNames[i], columns[i]));
				list2.Add(columns[i].ConceptualResultType.Column(columnNames[i], null));
			}
			return new QueryTupleExpression(list2.Row(), list);
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x00037A66 File Offset: 0x00035C66
		public static QueryConvertToStringExpression ConvertToString(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input)
		{
			ArgumentValidation.CheckNotNull<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>(input, "input");
			return new QueryConvertToStringExpression(input);
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x00037A7A File Offset: 0x00035C7A
		public static QueryConcatenateExpression Concatenate(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression other)
		{
			return new QueryConcatenateExpression(new Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression[] { input, other });
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x00037A8F File Offset: 0x00035C8F
		public static QueryConcatenateExpression Concatenate(this IReadOnlyList<Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> inputs)
		{
			return new QueryConcatenateExpression(inputs);
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x00037A97 File Offset: 0x00035C97
		public static QueryConcatenateXExpression ConcatenateX(this QueryExpressionBinding table, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression expression, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression delimiter, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QuerySortClause orderBy)
		{
			return new QueryConcatenateXExpression(table, expression, delimiter, orderBy);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x00037AA2 File Offset: 0x00035CA2
		public static QueryFormatExpression Format(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression input, string formatString, string locale = null)
		{
			return new QueryFormatExpression(input, formatString, locale);
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x00037AAC File Offset: 0x00035CAC
		public static QueryTreatAsExpression TreatAs(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression inputTable, IReadOnlyList<KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression>> columns)
		{
			return new QueryTreatAsExpression(columns.Select((KeyValuePair<string, Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression> kvp) => kvp.Value.ConceptualResultType.Column(kvp.Key, null)).ToList<ConceptualTypeColumn>().Row()
				.Table(), inputTable, columns);
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x00037AE9 File Offset: 0x00035CE9
		public static QueryTypeSafeFloorExpression TypeSafeFloor(this Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal.QueryExpression field, double size, TimeUnit? timeUnit)
		{
			return new QueryTypeSafeFloorExpression(field.ConceptualResultType, field, size, timeUnit);
		}
	}
}

using System;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.ExpressionBuilder.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000005 RID: 5
	internal static class DiscretizeExpressionTranslator
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002068 File Offset: 0x00000268
		internal static QueryExpression Translate(QueryExpression fieldExpression, int count)
		{
			QueryExpressionWithLocalVariablesBuilder queryExpressionWithLocalVariablesBuilder = new QueryExpressionWithLocalVariablesBuilder();
			QueryVariableReferenceExpression queryVariableReferenceExpression = queryExpressionWithLocalVariablesBuilder.DeclareVariable(QueryExpressionBuilder.Literal(count), "Count");
			QueryExpression projectTableExpression = DiscretizeExpressionTranslator.GetProjectTableExpression(fieldExpression);
			QueryVariableReferenceExpression queryVariableReferenceExpression2 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(projectTableExpression.Min(), "Min");
			QueryVariableReferenceExpression queryVariableReferenceExpression3 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(projectTableExpression.Max(), "Max");
			QueryVariableReferenceExpression queryVariableReferenceExpression4 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryVariableReferenceExpression3.Minus(queryVariableReferenceExpression2), "Difference");
			QueryFunctionExpression queryFunctionExpression = queryVariableReferenceExpression4.GreaterThan(Literals.ZeroInt64).If(queryVariableReferenceExpression4.DivideRaw(queryVariableReferenceExpression), Literals.OneInt64);
			QueryVariableReferenceExpression queryVariableReferenceExpression5 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(queryFunctionExpression, "Size");
			QueryExpression binNumberExpression = DiscretizeExpressionTranslator.GetBinNumberExpression(fieldExpression, queryVariableReferenceExpression2, queryVariableReferenceExpression5);
			QueryVariableReferenceExpression queryVariableReferenceExpression6 = queryExpressionWithLocalVariablesBuilder.DeclareVariable(binNumberExpression, "BinNumber");
			QueryExpression binnedValueExpression = DiscretizeExpressionTranslator.GetBinnedValueExpression(fieldExpression, queryVariableReferenceExpression2, queryVariableReferenceExpression6, queryVariableReferenceExpression, queryVariableReferenceExpression5);
			queryExpressionWithLocalVariablesBuilder.SetResult(binnedValueExpression);
			return queryExpressionWithLocalVariablesBuilder.ToQueryExpression();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x00002134 File Offset: 0x00000334
		private static QueryExpression GetProjectTableExpression(QueryExpression fieldExpression)
		{
			global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn> referencedModelField = DiscretizeExpressionTranslator.GetReferencedModelField(fieldExpression);
			IEdmFieldInstance item = referencedModelField.Item1;
			IConceptualColumn item2 = referencedModelField.Item2;
			if (item2 != null)
			{
				IConceptualEntity entity = item2.Entity;
				QueryExpressionBinding queryExpressionBinding = entity.Scan(true).BindAs(entity.EdmName);
				return queryExpressionBinding.Project(queryExpressionBinding.Variable.Field(item2), ProjectSubsetStrategy.Default);
			}
			EntitySet entity2 = item.Entity;
			EdmField field = item.Field;
			QueryExpressionBinding queryExpressionBinding2 = entity2.Scan(true).BindAs(entity2.Name);
			return queryExpressionBinding2.Project(queryExpressionBinding2.Variable.Field(field, null), ProjectSubsetStrategy.Default);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000021B8 File Offset: 0x000003B8
		private static global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn> GetReferencedModelField(QueryExpression expression)
		{
			QueryFieldExpression queryFieldExpression = expression as QueryFieldExpression;
			if (queryFieldExpression != null)
			{
				QueryScalarEntityReferenceExpression queryScalarEntityReferenceExpression = queryFieldExpression.Instance as QueryScalarEntityReferenceExpression;
				if (queryScalarEntityReferenceExpression != null)
				{
					IConceptualColumn conceptualColumn = null;
					if (queryScalarEntityReferenceExpression.TargetEntity != null)
					{
						conceptualColumn = queryScalarEntityReferenceExpression.TargetEntity.GetPropertyByEdmName(queryFieldExpression.Column.EdmName).AsColumn();
					}
					IEdmFieldInstance edmFieldInstance = null;
					if (queryScalarEntityReferenceExpression.Target != null)
					{
						edmFieldInstance = queryScalarEntityReferenceExpression.Target.FieldInstance(queryFieldExpression.Column.EdmName);
					}
					return new global::System.ValueTuple<IEdmFieldInstance, IConceptualColumn>(edmFieldInstance, conceptualColumn);
				}
			}
			Contract.RetailFail("The binned expression must refer to a model field.");
			throw new InvalidOperationException("The binned expression must refer to a model field.");
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002248 File Offset: 0x00000448
		private static QueryExpression GetBinNumberExpression(QueryExpression fieldExpression, QueryExpression minExpr, QueryExpression sizeExpr)
		{
			ConceptualPrimitiveType? primitiveTypeKind = fieldExpression.ConceptualResultType.GetPrimitiveTypeKind();
			ConceptualPrimitiveType? conceptualPrimitiveType = primitiveTypeKind;
			ConceptualPrimitiveType conceptualPrimitiveType2 = ConceptualPrimitiveType.Double;
			if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
			{
				conceptualPrimitiveType = primitiveTypeKind;
				conceptualPrimitiveType2 = ConceptualPrimitiveType.DateTime;
				if (!((conceptualPrimitiveType.GetValueOrDefault() == conceptualPrimitiveType2) & (conceptualPrimitiveType != null)))
				{
					return fieldExpression.Minus(minExpr).DivideRaw(sizeExpr).Int();
				}
			}
			QueryFunctionExpression queryFunctionExpression = fieldExpression.Minus(minExpr).DivideRaw(sizeExpr).RoundDown(Literals.ZeroInt64);
			return fieldExpression.IsNull().If(queryFunctionExpression.ConceptualResultType.Null(), queryFunctionExpression);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000022D4 File Offset: 0x000004D4
		private static QueryExpression GetBinnedValueExpression(QueryExpression fieldExpression, QueryExpression minExpr, QueryExpression binNumberExpr, QueryExpression countExpr, QueryExpression sizeExpr)
		{
			QueryFunctionExpression queryFunctionExpression = binNumberExpr.MinValue(countExpr.Minus(Literals.OneInt64));
			QueryFunctionExpression queryFunctionExpression2 = minExpr.Plus(queryFunctionExpression.Multiply(sizeExpr));
			return fieldExpression.IsNull().If(queryFunctionExpression2.ConceptualResultType.Null(), queryFunctionExpression2);
		}

		// Token: 0x04000029 RID: 41
		private const string CountVariableName = "Count";

		// Token: 0x0400002A RID: 42
		private const string MinVariableName = "Min";

		// Token: 0x0400002B RID: 43
		private const string MaxVariableName = "Max";

		// Token: 0x0400002C RID: 44
		private const string DifferenceVariableName = "Difference";

		// Token: 0x0400002D RID: 45
		private const string SizeVariableName = "Size";

		// Token: 0x0400002E RID: 46
		private const string BinNumberVariableName = "BinNumber";
	}
}

using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Data.Contracts.ConceptualResultTypes;
using Microsoft.Reporting.QueryDesign.Edm.Internal;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000133 RID: 307
	internal static class DaxFunctionTransforms
	{
		// Token: 0x0200037A RID: 890
		internal abstract class AggregateFunctionTransformBase : DaxFunctionTransform
		{
			// Token: 0x06001F8A RID: 8074 RVA: 0x00056BEB File Offset: 0x00054DEB
			protected AggregateFunctionTransformBase(Func<DaxColumnRef, DaxExpression> daxColumnAggregate)
			{
				this._daxColumnAggregate = ArgumentValidation.CheckNotNull<Func<DaxColumnRef, DaxExpression>>(daxColumnAggregate, "daxColumnAggregate");
			}

			// Token: 0x06001F8B RID: 8075 RVA: 0x00056C04 File Offset: 0x00054E04
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 1, "Unexpected arguments were specified for an aggregate function.");
				QueryProjectExpression queryProjectExpression = base.Arguments[0] as QueryProjectExpression;
				ConceptualTypeColumn conceptualTypeColumn;
				EntitySet entitySet;
				IConceptualEntity conceptualEntity;
				if (DaxFunctionTransforms.AggregateFunctionTransformBase.IsColumnScan(base.Arguments[0], out conceptualTypeColumn, out entitySet, out conceptualEntity))
				{
					return this.TranslateColumnAggregate(conceptualTypeColumn, entitySet, conceptualEntity);
				}
				if (queryProjectExpression != null)
				{
					return this.TranslateTableAggregate(queryProjectExpression);
				}
				throw new DaxTranslationException("Unexpected arguments were specified for an aggregate function.");
			}

			// Token: 0x06001F8C RID: 8076 RVA: 0x00056C74 File Offset: 0x00054E74
			protected virtual DaxExpression TranslateColumnAggregate(ConceptualTypeColumn columnType, EntitySet entitySet, IConceptualEntity entity = null)
			{
				DaxColumnRef daxColumnRef = DaxRef.Column(columnType, entitySet, entity);
				return this._daxColumnAggregate(daxColumnRef);
			}

			// Token: 0x06001F8D RID: 8077
			protected abstract DaxExpression TranslateTableAggregate(QueryProjectExpression projectExpr);

			// Token: 0x06001F8E RID: 8078 RVA: 0x00056C98 File Offset: 0x00054E98
			internal static bool IsColumnScan(QueryExpression expression, out ConceptualTypeColumn columnType, out EntitySet entitySet, out IConceptualEntity entity)
			{
				columnType = null;
				entitySet = null;
				entity = null;
				QueryProjectExpression queryProjectExpression = expression as QueryProjectExpression;
				if (queryProjectExpression == null)
				{
					return false;
				}
				QueryScanExpression queryScanExpression = queryProjectExpression.Input.Expression as QueryScanExpression;
				if (queryScanExpression == null)
				{
					return false;
				}
				QueryFieldExpression queryFieldExpression = queryProjectExpression.Projection as QueryFieldExpression;
				if (queryFieldExpression == null || !queryFieldExpression.Instance.Equals(queryProjectExpression.Input.Variable))
				{
					return false;
				}
				columnType = queryFieldExpression.Column;
				entitySet = queryScanExpression.Target;
				entity = queryScanExpression.TargetEntity;
				return true;
			}

			// Token: 0x040012BC RID: 4796
			private readonly Func<DaxColumnRef, DaxExpression> _daxColumnAggregate;
		}

		// Token: 0x0200037B RID: 891
		internal class AggregateFunctionTransform : DaxFunctionTransforms.AggregateFunctionTransformBase
		{
			// Token: 0x06001F8F RID: 8079 RVA: 0x00056D12 File Offset: 0x00054F12
			internal AggregateFunctionTransform(Func<DaxColumnRef, DaxExpression> daxColumnAggregate, Func<DaxExpression, DaxExpression, DaxExpression> daxTableAggregate)
				: base(daxColumnAggregate)
			{
				this._daxTableAggregate = ArgumentValidation.CheckNotNull<Func<DaxExpression, DaxExpression, DaxExpression>>(daxTableAggregate, "daxTableAggregate");
			}

			// Token: 0x06001F90 RID: 8080 RVA: 0x00056D2C File Offset: 0x00054F2C
			protected override DaxExpression TranslateTableAggregate(QueryProjectExpression projectExpr)
			{
				return base.DaxTransform.EvaluateInScope<DaxExpression>(projectExpr.Input, (DaxExpression inputTable) => this._daxTableAggregate(inputTable, projectExpr.Projection.Accept<DaxExpression>(this.DaxTransform)), true);
			}

			// Token: 0x040012BD RID: 4797
			private readonly Func<DaxExpression, DaxExpression, DaxExpression> _daxTableAggregate;
		}

		// Token: 0x0200037C RID: 892
		internal sealed class MinMaxFunctionTransform : DaxFunctionTransforms.AggregateFunctionTransform
		{
			// Token: 0x06001F91 RID: 8081 RVA: 0x00056D70 File Offset: 0x00054F70
			internal MinMaxFunctionTransform(Func<DaxColumnRef, DaxExpression> daxColumnAggregate, Func<DaxExpression, DaxExpression, DaxExpression> daxTableAggregate)
				: base(daxColumnAggregate, daxTableAggregate)
			{
			}

			// Token: 0x06001F92 RID: 8082 RVA: 0x00056D7A File Offset: 0x00054F7A
			protected override DaxExpression TranslateColumnAggregate(ConceptualTypeColumn columnType, EntitySet entitySet, IConceptualEntity entity = null)
			{
				if (!base.DaxTransform.DaxCapabilities.IsSupported(DaxFunctionKind.StringMinMax) && columnType.PrimitiveType.ConceptualDataType == ConceptualPrimitiveType.Text)
				{
					throw new DaxTranslationException("The query uses min or max over a string column. This is not supported by the underlying model.", CommandTreeTranslationErrorCode.UnsupportedStringMinMaxColumn);
				}
				return base.TranslateColumnAggregate(columnType, entitySet, entity);
			}

			// Token: 0x06001F93 RID: 8083 RVA: 0x00056DB3 File Offset: 0x00054FB3
			protected override DaxExpression TranslateTableAggregate(QueryProjectExpression projectExpr)
			{
				if (!base.DaxTransform.DaxCapabilities.IsSupported(DaxFunctionKind.StringMinMax) && projectExpr.Projection.ConceptualResultType.IsText())
				{
					throw new DaxTranslationException("The query uses min or max over a string expression. This is not supported by the underlying model.", CommandTreeTranslationErrorCode.UnsupportedStringMinMaxExpression);
				}
				return base.TranslateTableAggregate(projectExpr);
			}
		}

		// Token: 0x0200037D RID: 893
		internal sealed class DistinctCountFunctionTransform : DaxFunctionTransforms.AggregateFunctionTransformBase
		{
			// Token: 0x06001F94 RID: 8084 RVA: 0x00056DEE File Offset: 0x00054FEE
			internal DistinctCountFunctionTransform()
			{
				Func<DaxColumnRef, DaxExpression> func;
				if ((func = DaxFunctionTransforms.DistinctCountFunctionTransform.<>O.<0>__DistinctCount) == null)
				{
					func = (DaxFunctionTransforms.DistinctCountFunctionTransform.<>O.<0>__DistinctCount = new Func<DaxColumnRef, DaxExpression>(DaxFunctions.DistinctCount));
				}
				base..ctor(func);
			}

			// Token: 0x06001F95 RID: 8085 RVA: 0x00056E11 File Offset: 0x00055011
			protected override DaxExpression TranslateTableAggregate(QueryProjectExpression projectExpr)
			{
				throw new NotImplementedException();
			}

			// Token: 0x02000451 RID: 1105
			[CompilerGenerated]
			private static class <>O
			{
				// Token: 0x040014EF RID: 5359
				public static Func<DaxColumnRef, DaxExpression> <0>__DistinctCount;
			}
		}

		// Token: 0x0200037E RID: 894
		internal sealed class PercentileFunctionTransform : DaxFunctionTransform
		{
			// Token: 0x06001F96 RID: 8086 RVA: 0x00056E18 File Offset: 0x00055018
			internal PercentileFunctionTransform(Func<DaxColumnRef, DaxExpression, DaxExpression> daxColumnAggregate, Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> daxTableAggregate)
			{
				this._daxColumnAggregate = ArgumentValidation.CheckNotNull<Func<DaxColumnRef, DaxExpression, DaxExpression>>(daxColumnAggregate, "daxColumnAggregate");
				this._daxTableAggregate = ArgumentValidation.CheckNotNull<Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>>(daxTableAggregate, "daxTableAggregate");
			}

			// Token: 0x06001F97 RID: 8087 RVA: 0x00056E44 File Offset: 0x00055044
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 2, "Unexpected arguments were specified for an aggregate function.");
				QueryProjectExpression projectExpr = base.Arguments[0] as QueryProjectExpression;
				DaxExpression k = base.Arguments[1].Accept<DaxExpression>(base.DaxTransform);
				ConceptualTypeColumn conceptualTypeColumn;
				EntitySet entitySet;
				IConceptualEntity conceptualEntity;
				if (DaxFunctionTransforms.AggregateFunctionTransformBase.IsColumnScan(base.Arguments[0], out conceptualTypeColumn, out entitySet, out conceptualEntity))
				{
					DaxColumnRef daxColumnRef = DaxRef.Column(conceptualTypeColumn, entitySet, conceptualEntity);
					return this._daxColumnAggregate(daxColumnRef, k);
				}
				if (projectExpr != null)
				{
					return base.DaxTransform.EvaluateInScope<DaxExpression>(projectExpr.Input, (DaxExpression inputTable) => this._daxTableAggregate(inputTable, projectExpr.Projection.Accept<DaxExpression>(this.DaxTransform), k), true);
				}
				throw new DaxTranslationException("Unexpected arguments were specified for an aggregate function.");
			}

			// Token: 0x040012BE RID: 4798
			private readonly Func<DaxColumnRef, DaxExpression, DaxExpression> _daxColumnAggregate;

			// Token: 0x040012BF RID: 4799
			private readonly Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> _daxTableAggregate;
		}

		// Token: 0x0200037F RID: 895
		internal sealed class BinaryFunctionTransform : DaxFunctionTransform
		{
			// Token: 0x06001F98 RID: 8088 RVA: 0x00056F17 File Offset: 0x00055117
			internal BinaryFunctionTransform(Func<DaxExpression, DaxExpression, DaxExpression> daxFunction)
			{
				this._daxFunction = ArgumentValidation.CheckNotNull<Func<DaxExpression, DaxExpression, DaxExpression>>(daxFunction, "daxFunction");
			}

			// Token: 0x06001F99 RID: 8089 RVA: 0x00056F30 File Offset: 0x00055130
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 2, "Unexpected arguments were specified for a binary function.");
				return this._daxFunction(base.Arguments[0].Accept<DaxExpression>(base.DaxTransform), base.Arguments[1].Accept<DaxExpression>(base.DaxTransform));
			}

			// Token: 0x040012C0 RID: 4800
			private readonly Func<DaxExpression, DaxExpression, DaxExpression> _daxFunction;
		}

		// Token: 0x02000380 RID: 896
		internal sealed class TriaryFunctionTransform : DaxFunctionTransform
		{
			// Token: 0x06001F9A RID: 8090 RVA: 0x00056F8E File Offset: 0x0005518E
			internal TriaryFunctionTransform(Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> daxFunction)
			{
				this._daxFunction = ArgumentValidation.CheckNotNull<Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression>>(daxFunction, "daxFunction");
			}

			// Token: 0x06001F9B RID: 8091 RVA: 0x00056FA8 File Offset: 0x000551A8
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 3, "Unexpected arguments were specified for a triary function.");
				return this._daxFunction(base.Arguments[0].Accept<DaxExpression>(base.DaxTransform), base.Arguments[1].Accept<DaxExpression>(base.DaxTransform), base.Arguments[2].Accept<DaxExpression>(base.DaxTransform));
			}

			// Token: 0x040012C1 RID: 4801
			private readonly Func<DaxExpression, DaxExpression, DaxExpression, DaxExpression> _daxFunction;
		}

		// Token: 0x02000381 RID: 897
		internal sealed class UnaryFunctionTransform : DaxFunctionTransform
		{
			// Token: 0x06001F9C RID: 8092 RVA: 0x0005701D File Offset: 0x0005521D
			internal UnaryFunctionTransform(Func<DaxExpression, DaxExpression> daxFunction)
			{
				this._daxFunction = ArgumentValidation.CheckNotNull<Func<DaxExpression, DaxExpression>>(daxFunction, "daxFunction");
			}

			// Token: 0x06001F9D RID: 8093 RVA: 0x00057036 File Offset: 0x00055236
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 1, "Unexpected arguments were specified for a unary function.");
				return this._daxFunction(base.Arguments[0].Accept<DaxExpression>(base.DaxTransform));
			}

			// Token: 0x040012C2 RID: 4802
			private readonly Func<DaxExpression, DaxExpression> _daxFunction;
		}

		// Token: 0x02000382 RID: 898
		internal static class TextFunctionTransformUtil
		{
			// Token: 0x06001F9E RID: 8094 RVA: 0x00057072 File Offset: 0x00055272
			internal static DaxExpression InvokeContains(DaxExpression fullString, DaxExpression substring)
			{
				return DaxOperators.GreaterThanOrEquals(DaxFunctionTransforms.TextFunctionTransformUtil.InvokeDaxSearch(substring, fullString, DaxLiteral.StringStartIndex), DaxLiteral.StringStartIndex);
			}

			// Token: 0x06001F9F RID: 8095 RVA: 0x0005708C File Offset: 0x0005528C
			internal static DaxExpression InvokeEndsWith(DaxExpression fullString, DaxExpression substring)
			{
				return DaxOperators.NotEqual(DaxFunctions.If(DaxOperators.GreaterThanOrEquals(DaxFunctions.Len(fullString), DaxFunctions.Len(substring)), DaxFunctionTransforms.TextFunctionTransformUtil.InvokeDaxSearch(substring, fullString, DaxOperators.Plus(DaxOperators.Minus(DaxFunctions.Len(fullString), DaxFunctions.Len(substring)), DaxLiteral.StringStartIndex)), DaxLiteral.StringInvalidIndex), DaxLiteral.StringInvalidIndex);
			}

			// Token: 0x06001FA0 RID: 8096 RVA: 0x000570E0 File Offset: 0x000552E0
			internal static DaxExpression InvokeStartsWith(DaxExpression fullString, DaxExpression substring)
			{
				return DaxOperators.Equal(DaxFunctionTransforms.TextFunctionTransformUtil.InvokeDaxSearch(substring, fullString, DaxLiteral.StringStartIndex), DaxLiteral.StringStartIndex);
			}

			// Token: 0x06001FA1 RID: 8097 RVA: 0x000570F8 File Offset: 0x000552F8
			private static DaxExpression InvokeDaxSearch(DaxExpression substring, DaxExpression fullString, DaxExpression startIndex)
			{
				return DaxFunctions.Search(substring, fullString, startIndex, DaxLiteral.StringInvalidIndex);
			}
		}

		// Token: 0x02000383 RID: 899
		internal static class ScalarFunctionTransformUtil
		{
			// Token: 0x06001FA2 RID: 8098 RVA: 0x00057108 File Offset: 0x00055308
			internal static DaxExpression InvokeDaxBetween(DaxExpression candidate, DaxExpression value1, DaxExpression value2)
			{
				DaxExpression daxExpression = DaxFunctions.And(DaxOperators.LessThanOrEquals(value1, candidate), DaxOperators.GreaterThanOrEquals(value2, candidate));
				DaxExpression daxExpression2 = DaxFunctions.And(DaxOperators.LessThanOrEquals(value2, candidate), DaxOperators.GreaterThanOrEquals(value1, candidate));
				return DaxFunctions.Or(daxExpression, daxExpression2);
			}
		}

		// Token: 0x02000384 RID: 900
		internal sealed class DateTimeEqualToSecondFunctionTransform : DaxFunctionTransform
		{
			// Token: 0x06001FA3 RID: 8099 RVA: 0x00057144 File Offset: 0x00055344
			internal override DaxExpression Translate()
			{
				DaxValidation.CheckCondition(base.Arguments.Count == 2, "Unexpected arguments were specified for the DateTimeEqualToSecond function.");
				DaxExpression daxExpression = base.Arguments[0].Accept<DaxExpression>(base.DaxTransform);
				DateTime dateTime;
				DateTime dateTime2;
				DaxFunctionTransforms.DateTimeEqualToSecondFunctionTransform.GetDateTimeBoundaries(base.Arguments[1], out dateTime, out dateTime2);
				return DaxFunctions.And(DaxOperators.GreaterThanOrEquals(daxExpression, DaxLiteral.FromDateTime(dateTime)), DaxOperators.LessThan(daxExpression, DaxLiteral.FromDateTime(dateTime2)));
			}

			// Token: 0x06001FA4 RID: 8100 RVA: 0x000571B4 File Offset: 0x000553B4
			private static void GetDateTimeBoundaries(QueryExpression argument, out DateTime minimum, out DateTime maximum)
			{
				DateTime dateTime = Convert.ToDateTime(ArgumentValidation.CheckAs<QueryLiteralExpression>(argument, "argument").Value.Value, CultureInfo.InvariantCulture);
				minimum = dateTime.RemoveSubsecondComponent();
				maximum = minimum.AddSeconds(1.0);
			}
		}
	}
}

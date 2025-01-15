using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.InfoNav.Common.Internal;
using Microsoft.InfoNav.Data.PrimitiveValues;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x020002AE RID: 686
	[ImmutableObject(true)]
	public class QueryExpressionValidator : DefaultQueryExpressionVisitor
	{
		// Token: 0x060015C2 RID: 5570 RVA: 0x000276F0 File Offset: 0x000258F0
		public QueryExpressionValidator(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
		}

		// Token: 0x060015C3 RID: 5571 RVA: 0x000276FF File Offset: 0x000258FF
		public static bool IsValid(QueryExpressionContainer expression)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			new QueryExpressionValidator(errorTrackingOnlyErrorContext).ValidateExpression(expression);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x060015C4 RID: 5572 RVA: 0x0002771A File Offset: 0x0002591A
		public static bool IsValidStandalone(QueryExpressionContainer expression)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			new QueryExpressionValidator(errorTrackingOnlyErrorContext).ValidateStandaloneExpression(expression);
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x060015C5 RID: 5573 RVA: 0x00027738 File Offset: 0x00025938
		public static bool AreValid(IList<QueryExpressionContainer> expressions)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryExpressionValidator queryExpressionValidator = new QueryExpressionValidator(errorTrackingOnlyErrorContext);
			foreach (QueryExpressionContainer queryExpressionContainer in expressions)
			{
				if (queryExpressionContainer == null)
				{
					return false;
				}
				queryExpressionValidator.ValidateExpression(queryExpressionContainer);
			}
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x060015C6 RID: 5574 RVA: 0x000277A8 File Offset: 0x000259A8
		public static bool AreValidStandalone(IList<QueryExpressionContainer> expressions)
		{
			ErrorTrackingOnlyErrorContext errorTrackingOnlyErrorContext = new ErrorTrackingOnlyErrorContext();
			QueryExpressionValidator queryExpressionValidator = new QueryExpressionValidator(errorTrackingOnlyErrorContext);
			foreach (QueryExpressionContainer queryExpressionContainer in expressions)
			{
				if (queryExpressionContainer == null)
				{
					return false;
				}
				queryExpressionValidator.ValidateStandaloneExpression(queryExpressionContainer);
			}
			return !errorTrackingOnlyErrorContext.HasError;
		}

		// Token: 0x060015C7 RID: 5575 RVA: 0x00027818 File Offset: 0x00025A18
		public void SetQueryValidator(QueryDefinitionValidator queryValidator)
		{
			this._queryValidator = queryValidator;
		}

		// Token: 0x060015C8 RID: 5576 RVA: 0x00027824 File Offset: 0x00025A24
		public void ValidateExpression(QueryExpressionContainer expression)
		{
			bool standaloneExpression = this._standaloneExpression;
			this._standaloneExpression = false;
			this.VisitExpression(expression);
			this._standaloneExpression = standaloneExpression;
		}

		// Token: 0x060015C9 RID: 5577 RVA: 0x00027850 File Offset: 0x00025A50
		public void ValidateStandaloneExpression(QueryExpressionContainer expression)
		{
			bool standaloneExpression = this._standaloneExpression;
			this._standaloneExpression = true;
			this.VisitExpression(expression);
			this._standaloneExpression = standaloneExpression;
		}

		// Token: 0x060015CA RID: 5578 RVA: 0x00027879 File Offset: 0x00025A79
		public override void VisitExpression(QueryExpressionContainer expression)
		{
			if (expression.Expression == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.EmptyExpressionContainer, new object[0]);
				return;
			}
			base.VisitExpression(expression);
		}

		// Token: 0x060015CB RID: 5579 RVA: 0x000278A8 File Offset: 0x00025AA8
		protected internal override void Visit(QuerySourceRefExpression sourceRef)
		{
			bool flag = !string.IsNullOrEmpty(sourceRef.Source);
			bool flag2 = !string.IsNullOrEmpty(sourceRef.Entity);
			if (flag && flag2)
			{
				this._errorContext.RegisterError(QueryValidationMessages.IncompatibleEntityAndSource(sourceRef.Entity, sourceRef.Source), new object[0]);
				return;
			}
			if (this._standaloneExpression && !flag2)
			{
				this._errorContext.RegisterError(QueryValidationMessages.EntityMissingInStandaloneExpression, new object[0]);
				return;
			}
			if (!this._standaloneExpression && !flag)
			{
				this._errorContext.RegisterError(QueryValidationMessages.EntitySourceMissing, new object[0]);
			}
		}

		// Token: 0x060015CC RID: 5580 RVA: 0x00027940 File Offset: 0x00025B40
		protected internal override void Visit(QuerySubqueryExpression expression)
		{
			if (expression.Query == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.ExpressionMissingSubquery, new object[0]);
				return;
			}
			if (!expression.Query.GroupBy.IsNullOrEmptyCollection<QueryExpressionContainer>())
			{
				this._errorContext.RegisterError(QueryValidationMessages.GroupByInSubquery, new object[0]);
				return;
			}
			if (this._queryValidator == null)
			{
				this._queryValidator = new QueryDefinitionValidator(this);
			}
			bool standaloneExpression = this._standaloneExpression;
			this._standaloneExpression = false;
			this._queryValidator.Visit(this._errorContext, expression.Query);
			this._standaloneExpression = standaloneExpression;
		}

		// Token: 0x060015CD RID: 5581 RVA: 0x000279D5 File Offset: 0x00025BD5
		private bool IsNullOrEmptyCollection<T>(ICollection<T> collection, object parent, string propertyName)
		{
			if (collection.IsNullOrEmptyCollection<T>())
			{
				this._errorContext.RegisterError(QueryValidationMessages.NullOrEmptyProperty(parent, propertyName), new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x060015CE RID: 5582 RVA: 0x000279FC File Offset: 0x00025BFC
		private bool IsNullOrEmptyProperty(object property, object parent, string propertyName)
		{
			if (property == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.NullOrEmptyProperty(parent, propertyName), new object[0]);
				return true;
			}
			string text = property as string;
			if (text != null && string.IsNullOrEmpty(text))
			{
				this._errorContext.RegisterError(QueryValidationMessages.NullOrEmptyProperty(parent, propertyName), new object[0]);
				return true;
			}
			return false;
		}

		// Token: 0x060015CF RID: 5583 RVA: 0x00027A54 File Offset: 0x00025C54
		private bool IsPropertyValid(QueryPropertyExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return false;
			}
			if (expression.Expression.SourceRef == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression(expression.GetType().Name), new object[0]);
				return false;
			}
			return !this.IsNullOrEmptyProperty(expression.Property, expression, "Property");
		}

		// Token: 0x060015D0 RID: 5584 RVA: 0x00027AC4 File Offset: 0x00025CC4
		protected internal override void Visit(QueryPropertyExpression expression)
		{
			if (!this.IsPropertyValid(expression))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D1 RID: 5585 RVA: 0x00027AD8 File Offset: 0x00025CD8
		protected internal override void Visit(QueryColumnExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Property, expression, "Property"))
			{
				return;
			}
			if (expression.Expression.SourceRef == null && expression.Expression.PropertyVariationSource == null && expression.Expression.TransformTableRef == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression("Expression"), new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x00027B6B File Offset: 0x00025D6B
		protected internal override void Visit(QueryMeasureExpression expression)
		{
			if (!this.IsPropertyValid(expression))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D3 RID: 5587 RVA: 0x00027B80 File Offset: 0x00025D80
		protected internal override void Visit(QueryHierarchyExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Hierarchy, expression, "Hierarchy"))
			{
				return;
			}
			if (expression.Expression.SourceRef == null && expression.Expression.PropertyVariationSource == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression("Expression"), new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D4 RID: 5588 RVA: 0x00027C00 File Offset: 0x00025E00
		protected internal override void Visit(QueryHierarchyLevelExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Expression.Hierarchy == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression(expression.GetType().Name), new object[0]);
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Level, expression, "Level"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D5 RID: 5589 RVA: 0x00027C74 File Offset: 0x00025E74
		protected internal override void Visit(QueryPropertyVariationSourceExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Name, expression, "Name"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Property, expression, "Property"))
			{
				return;
			}
			if (expression.Expression.SourceRef == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression(expression.GetType().Name), new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D6 RID: 5590 RVA: 0x00027CFC File Offset: 0x00025EFC
		protected internal override void Visit(QueryAggregationExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (!expression.Function.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidAggregation, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D7 RID: 5591 RVA: 0x00027D50 File Offset: 0x00025F50
		protected internal override void Visit(QueryDatePartExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (!expression.Function.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidDatePart, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D8 RID: 5592 RVA: 0x00027DA4 File Offset: 0x00025FA4
		protected internal override void Visit(QueryPercentileExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Exclusive)
			{
				if (expression.K <= 0.0 || expression.K >= 1.0)
				{
					this._errorContext.RegisterError(QueryValidationMessages.InvalidPercentile, new object[0]);
					return;
				}
			}
			else if (expression.K < 0.0 || expression.K > 1.0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidPercentile, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015D9 RID: 5593 RVA: 0x00027E48 File Offset: 0x00026048
		protected internal override void Visit(QueryFloorExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Size <= 0.0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidFloor, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DA RID: 5594 RVA: 0x00027E99 File Offset: 0x00026099
		protected internal override void Visit(QueryDiscretizeExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Count <= 0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidDiscretize, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DB RID: 5595 RVA: 0x00027ED8 File Offset: 0x000260D8
		protected internal override void Visit(QuerySparklineDataExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Measure, expression, "Measure"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Groupings, expression, "Groupings"))
			{
				return;
			}
			if (expression.PointsPerSparkline <= 0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidSparklineDataPointsPerSparkline, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x00027F36 File Offset: 0x00026136
		protected internal override void Visit(QueryMemberExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Member, expression, "Expression"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DD RID: 5597 RVA: 0x00027F69 File Offset: 0x00026169
		protected internal override void Visit(QueryNativeFormatExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.FormatString, expression, "FormatString"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DE RID: 5598 RVA: 0x00027F9C File Offset: 0x0002619C
		protected internal override void Visit(QueryNativeMeasureExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Language, expression, "Language"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x00027FD0 File Offset: 0x000261D0
		protected internal override void Visit(QueryExistsExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Expression.SourceRef == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidChildExpression(expression.GetType().Name), new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E0 RID: 5600 RVA: 0x0002802E File Offset: 0x0002622E
		protected internal override void Visit(QueryNotExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E1 RID: 5601 RVA: 0x0002804C File Offset: 0x0002624C
		protected internal override void Visit(QueryAndExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E2 RID: 5602 RVA: 0x0002807F File Offset: 0x0002627F
		protected internal override void Visit(QueryOrExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E3 RID: 5603 RVA: 0x000280B4 File Offset: 0x000262B4
		protected internal override void Visit(QueryComparisonExpression expression)
		{
			if (!expression.ComparisonKind.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidComparisonKind(expression.ComparisonKind), new object[0]);
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00028124 File Offset: 0x00026324
		private bool ValidateOperandsAreStrings(QueryBinaryExpression expression)
		{
			if (!expression.Left.CanReturnStrings() || (expression.Right.Constant != null && expression.Right.String == null))
			{
				this._errorContext.RegisterError(QueryValidationMessages.NonStringOperand(expression.GetType().Name), new object[0]);
				return false;
			}
			return true;
		}

		// Token: 0x060015E5 RID: 5605 RVA: 0x00028188 File Offset: 0x00026388
		protected internal override void Visit(QueryContainsExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			if (!this.ValidateOperandsAreStrings(expression))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E6 RID: 5606 RVA: 0x000281C5 File Offset: 0x000263C5
		protected internal override void Visit(QueryStartsWithExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			if (!this.ValidateOperandsAreStrings(expression))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E7 RID: 5607 RVA: 0x00028204 File Offset: 0x00026404
		protected internal override void Visit(QueryArithmeticExpression expression)
		{
			if (!expression.Operator.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidArithmeticExpression, new object[0]);
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x0002826B File Offset: 0x0002646B
		protected internal override void Visit(QueryEndsWithExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Left, expression, "Left"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Right, expression, "Right"))
			{
				return;
			}
			if (!this.ValidateOperandsAreStrings(expression))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x000282A8 File Offset: 0x000264A8
		protected internal override void Visit(QueryBetweenExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.LowerBound, expression, "LowerBound"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.UpperBound, expression, "UpperBound"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015EA RID: 5610 RVA: 0x000282FC File Offset: 0x000264FC
		protected internal override void Visit(QueryInExpression expression)
		{
			if (expression.Expressions.IsNullOrEmpty<QueryExpressionContainer>())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionExpressions, new object[0]);
				return;
			}
			int count = expression.Expressions.Count;
			if (expression.HasValues)
			{
				if (expression.Table != null)
				{
					this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionValuesWithTable, new object[0]);
					return;
				}
				using (List<List<QueryExpressionContainer>>.Enumerator enumerator = expression.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						List<QueryExpressionContainer> list = enumerator.Current;
						if (list.IsNullOrEmpty<QueryExpressionContainer>() || count != list.Count)
						{
							this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionValues, new object[0]);
							return;
						}
					}
					goto IL_0134;
				}
			}
			if (!(expression.Table != null))
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionNoValuesNoTable, new object[0]);
				return;
			}
			if (expression.Table.SourceRef == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionTable, new object[0]);
				return;
			}
			if (expression.EqualityKind != null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryInExpressionEqualityKind, new object[0]);
				return;
			}
			IL_0134:
			base.Visit(expression);
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x00028454 File Offset: 0x00026654
		protected internal override void Visit(QueryScopedEvalExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Scope, expression, "Scope"))
			{
				return;
			}
			foreach (QueryExpressionContainer queryExpressionContainer in expression.Scope)
			{
				if (queryExpressionContainer == null || (queryExpressionContainer.Column == null && queryExpressionContainer.HierarchyLevel == null))
				{
					this._errorContext.RegisterError(QueryValidationMessages.InvalidScopedEvalExpression, new object[0]);
					return;
				}
			}
			base.Visit(expression);
		}

		// Token: 0x060015EC RID: 5612 RVA: 0x00028510 File Offset: 0x00026710
		protected internal override void Visit(QueryFilteredEvalExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (expression.Filters.IsNullOrEmpty<QueryFilter>())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidFilteredEvalExpression, new object[0]);
				return;
			}
			if (this._queryValidator == null)
			{
				this._queryValidator = new QueryDefinitionValidator(this);
			}
			foreach (QueryFilter queryFilter in expression.Filters)
			{
				this._queryValidator.Visit(this._errorContext, queryFilter);
			}
			base.Visit(expression);
		}

		// Token: 0x060015ED RID: 5613 RVA: 0x000285C4 File Offset: 0x000267C4
		protected internal override void Visit(QueryDecadeConstantExpression expression)
		{
			if (expression.Value % 10 != 0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidDecadeConstant, new object[0]);
				return;
			}
		}

		// Token: 0x060015EE RID: 5614 RVA: 0x000285E8 File Offset: 0x000267E8
		protected internal override void Visit(QueryNumberConstantExpression expression)
		{
			this.IsNullOrEmptyProperty(expression.Value, expression, "Value");
		}

		// Token: 0x060015EF RID: 5615 RVA: 0x000285FD File Offset: 0x000267FD
		protected internal override void Visit(QueryYearAndMonthConstantExpression expression)
		{
			if (expression.Year < 0 || expression.Month <= 0 || expression.Month >= 13)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidYearAndMonthConstant, new object[0]);
				return;
			}
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x00028632 File Offset: 0x00026832
		protected internal override void Visit(QueryYearConstantExpression expression)
		{
			if (expression.Value < 0)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidYearConstant, new object[0]);
				return;
			}
		}

		// Token: 0x060015F1 RID: 5617 RVA: 0x00028654 File Offset: 0x00026854
		protected internal override void Visit(QueryLiteralExpression expression)
		{
			PrimitiveValue primitiveValue;
			if (!PrimitiveValueEncoding.TryParseTypeEncodedString(expression.Value, out primitiveValue))
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidLiteralExpression, new object[0]);
				return;
			}
		}

		// Token: 0x060015F2 RID: 5618 RVA: 0x00028687 File Offset: 0x00026887
		protected internal override void Visit(QueryDateAddExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x000286A8 File Offset: 0x000268A8
		protected internal override void Visit(QueryDateSpanExpression expression)
		{
			if (!expression.TimeUnit.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidDateSpan, new object[0]);
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x000286FA File Offset: 0x000268FA
		protected internal override void Visit(QueryTransformOutputRoleRefExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Role, expression, "Role"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F5 RID: 5621 RVA: 0x00028718 File Offset: 0x00026918
		protected internal override void Visit(QueryTransformTableRefExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Source, expression, "Source"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F6 RID: 5622 RVA: 0x00028736 File Offset: 0x00026936
		protected internal override void Visit(QueryLetRefExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Name, expression, "Name"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F7 RID: 5623 RVA: 0x00028754 File Offset: 0x00026954
		protected internal override void Visit(QueryParameterRefExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Name, expression, "Name"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015F8 RID: 5624 RVA: 0x00028774 File Offset: 0x00026974
		protected internal override void Visit(QueryTableTypeExpression expression)
		{
			if (this.IsNullOrEmptyCollection<QueryExpressionContainer>(expression.Columns, expression, "Columns"))
			{
				return;
			}
			List<QueryExpressionContainer> columns = expression.Columns;
			HashSet<string> hashSet = new HashSet<string>(ConceptualNameComparer.Instance);
			for (int i = 0; i < columns.Count; i++)
			{
				this.ValidateTableTypeColumn(columns[i], hashSet);
			}
		}

		// Token: 0x060015F9 RID: 5625 RVA: 0x000287C8 File Offset: 0x000269C8
		private void ValidateTableTypeColumn(QueryExpressionContainer column, HashSet<string> usedNames)
		{
			if (column == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.NullExpressionContainer, new object[0]);
				return;
			}
			if (string.IsNullOrEmpty(column.Name))
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryTableTypeColumnMissingName, new object[0]);
				return;
			}
			if (!usedNames.Add(column.Name))
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryTableTypeColumnDuplicateName(column.Name), new object[0]);
				return;
			}
			if (column.PrimitiveType == null && column.TypeOf == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryTableTypeColumnInvalidExpressionType(column.Name, column), new object[0]);
			}
			this.VisitExpression(column);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x00028884 File Offset: 0x00026A84
		protected internal override void Visit(QueryPrimitiveTypeExpression expression)
		{
			ConceptualPrimitiveType type = expression.Type;
			if (!type.IsValid() || type == ConceptualPrimitiveType.None || type == ConceptualPrimitiveType.Null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidPrimitiveTypeInvalidType, new object[0]);
			}
		}

		// Token: 0x060015FB RID: 5627 RVA: 0x000288C4 File Offset: 0x00026AC4
		protected internal override void Visit(QueryTypeOfExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			QueryExpressionContainer expression2 = expression.Expression;
			if (expression2.Column == null && expression2.Measure == null && expression2.HierarchyLevel == null)
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidQueryTypeOfExpressionType(expression2), new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015FC RID: 5628 RVA: 0x00028936 File Offset: 0x00026B36
		protected internal override void Visit(QueryNativeVisualCalculationExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (this.IsNullOrEmptyProperty(expression.Language, expression, "Language"))
			{
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015FD RID: 5629 RVA: 0x0002896C File Offset: 0x00026B6C
		protected internal override void Visit(QueryMinExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (!expression.IncludeAllTypes.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidIncludeAllTypesBehavior, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x060015FE RID: 5630 RVA: 0x000289C0 File Offset: 0x00026BC0
		protected internal override void Visit(QueryMaxExpression expression)
		{
			if (this.IsNullOrEmptyProperty(expression.Expression, expression, "Expression"))
			{
				return;
			}
			if (!expression.IncludeAllTypes.IsValid())
			{
				this._errorContext.RegisterError(QueryValidationMessages.InvalidIncludeAllTypesBehavior, new object[0]);
				return;
			}
			base.Visit(expression);
		}

		// Token: 0x0400084C RID: 2124
		private readonly IErrorContext _errorContext;

		// Token: 0x0400084D RID: 2125
		private bool _standaloneExpression;

		// Token: 0x0400084E RID: 2126
		private QueryDefinitionValidator _queryValidator;
	}
}

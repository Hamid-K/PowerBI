using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000206 RID: 518
	public abstract class ResolvedQueryExpressionEqualsComparer : ResolvedQueryExpressionEqualityComparer
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000EB7 RID: 3767
		public abstract ResolvedQueryDefinitionEqualityComparer StructureComparer { get; }

		// Token: 0x06000EB8 RID: 3768 RVA: 0x0001C216 File Offset: 0x0001A416
		public override int VisitGetHashCode(ResolvedQuerySourceRefExpression obj)
		{
			return Hashing.CombineHash(obj.SourceEntity.GetHashCode(), Hashing.GetHashCode<string>(obj.SourceName, QueryNameComparer.Instance));
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x0001C238 File Offset: 0x0001A438
		public override bool VisitEquals(ResolvedQuerySourceRefExpression left, ResolvedQuerySourceRefExpression right)
		{
			if (!(right != null) || left.SourceEntity != right.SourceEntity)
			{
				return false;
			}
			bool? flag = Util.AreEqual<string>(left.SourceName, right.SourceName);
			if (flag == null)
			{
				return QueryNameComparer.Instance.Equals(left.SourceName, right.SourceName);
			}
			return flag.GetValueOrDefault();
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x0001C297 File Offset: 0x0001A497
		public override int VisitGetHashCode(ResolvedQueryExpressionSourceRefExpression obj)
		{
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.SourceName, null), this.GetHashCode(obj.Expression));
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x0001C2B6 File Offset: 0x0001A4B6
		public override bool VisitEquals(ResolvedQueryExpressionSourceRefExpression left, ResolvedQueryExpressionSourceRefExpression right)
		{
			return right != null && ConceptualNameComparer.Instance.Equals(left.SourceName, right.SourceName) && this.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x0001C2ED File Offset: 0x0001A4ED
		public override int VisitGetHashCode(ResolvedQuerySubqueryExpression obj)
		{
			return this.StructureComparer.GetHashCode(obj.Subquery);
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x0001C300 File Offset: 0x0001A500
		public override bool VisitEquals(ResolvedQuerySubqueryExpression left, ResolvedQuerySubqueryExpression right)
		{
			return right != null && this.StructureComparer.Equals(left.Subquery, right.Subquery);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x0001C324 File Offset: 0x0001A524
		private int GetHashCodePropertyExpression(ResolvedQueryPropertyExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Expression), obj.Property.GetHashCode());
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x0001C342 File Offset: 0x0001A542
		private bool EqualsPropertyExpression(ResolvedQueryPropertyExpression left, ResolvedQueryPropertyExpression right)
		{
			return right != null && left.Property == right.Property && this.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x0001C36F File Offset: 0x0001A56F
		public override int VisitGetHashCode(ResolvedQueryColumnExpression obj)
		{
			return this.GetHashCodePropertyExpression(obj);
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x0001C378 File Offset: 0x0001A578
		public override bool VisitEquals(ResolvedQueryColumnExpression left, ResolvedQueryColumnExpression right)
		{
			return this.EqualsPropertyExpression(left, right);
		}

		// Token: 0x06000EC2 RID: 3778 RVA: 0x0001C382 File Offset: 0x0001A582
		public override int VisitGetHashCode(ResolvedQueryMeasureExpression obj)
		{
			return this.GetHashCodePropertyExpression(obj);
		}

		// Token: 0x06000EC3 RID: 3779 RVA: 0x0001C38B File Offset: 0x0001A58B
		public override bool VisitEquals(ResolvedQueryMeasureExpression left, ResolvedQueryMeasureExpression right)
		{
			return this.EqualsPropertyExpression(left, right);
		}

		// Token: 0x06000EC4 RID: 3780 RVA: 0x0001C395 File Offset: 0x0001A595
		public override int VisitGetHashCode(ResolvedQueryHierarchyExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Expression), obj.Hierarchy.GetHashCode());
		}

		// Token: 0x06000EC5 RID: 3781 RVA: 0x0001C3B3 File Offset: 0x0001A5B3
		public override bool VisitEquals(ResolvedQueryHierarchyExpression left, ResolvedQueryHierarchyExpression right)
		{
			return right != null && left.Hierarchy == right.Hierarchy && this.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000EC6 RID: 3782 RVA: 0x0001C3E0 File Offset: 0x0001A5E0
		public override int VisitGetHashCode(ResolvedQueryHierarchyLevelExpression obj)
		{
			return Hashing.CombineHash(this.VisitGetHashCode(obj.HierarchyExpression), obj.Level.GetHashCode());
		}

		// Token: 0x06000EC7 RID: 3783 RVA: 0x0001C3FE File Offset: 0x0001A5FE
		public override bool VisitEquals(ResolvedQueryHierarchyLevelExpression left, ResolvedQueryHierarchyLevelExpression right)
		{
			return right != null && left.Level == right.Level && this.VisitEquals(left.HierarchyExpression, right.HierarchyExpression);
		}

		// Token: 0x06000EC8 RID: 3784 RVA: 0x0001C42B File Offset: 0x0001A62B
		public override int VisitGetHashCode(ResolvedQueryPropertyVariationSourceExpression obj)
		{
			return Hashing.CombineHash(this.VisitGetHashCode(obj.SourceRefExpression), obj.VariationSource.GetHashCode(), obj.Property.GetHashCode());
		}

		// Token: 0x06000EC9 RID: 3785 RVA: 0x0001C454 File Offset: 0x0001A654
		public override bool VisitEquals(ResolvedQueryPropertyVariationSourceExpression left, ResolvedQueryPropertyVariationSourceExpression right)
		{
			return right != null && left.VariationSource == right.VariationSource && left.Property == right.Property && this.VisitEquals(left.SourceRefExpression, right.SourceRefExpression);
		}

		// Token: 0x06000ECA RID: 3786 RVA: 0x0001C48F File Offset: 0x0001A68F
		public override int VisitGetHashCode(ResolvedQueryColumnReferenceExpression obj)
		{
			return Hashing.CombineHash(obj.SelectName.GetHashCode(), this.GetHashCode(obj.Source));
		}

		// Token: 0x06000ECB RID: 3787 RVA: 0x0001C4AD File Offset: 0x0001A6AD
		public override bool VisitEquals(ResolvedQueryColumnReferenceExpression left, ResolvedQueryColumnReferenceExpression right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.SelectName, right.SelectName) && this.Equals(left.Source, right.Source);
		}

		// Token: 0x06000ECC RID: 3788 RVA: 0x0001C4E4 File Offset: 0x0001A6E4
		private int GetHashCodeUnaryExpression(ResolvedQueryUnaryExpression obj)
		{
			return this.GetHashCode(obj.Expression);
		}

		// Token: 0x06000ECD RID: 3789 RVA: 0x0001C4F2 File Offset: 0x0001A6F2
		private bool EqualsUnaryExpression(ResolvedQueryUnaryExpression left, ResolvedQueryUnaryExpression right)
		{
			return right != null && this.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000ECE RID: 3790 RVA: 0x0001C511 File Offset: 0x0001A711
		public override int VisitGetHashCode(ResolvedQueryNotExpression obj)
		{
			return this.GetHashCodeUnaryExpression(obj);
		}

		// Token: 0x06000ECF RID: 3791 RVA: 0x0001C51A File Offset: 0x0001A71A
		public override bool VisitEquals(ResolvedQueryNotExpression left, ResolvedQueryNotExpression right)
		{
			return this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x0001C524 File Offset: 0x0001A724
		private int GetHashCodeBinaryExpression(ResolvedQueryBinaryExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Left), this.GetHashCode(obj.Right));
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x0001C543 File Offset: 0x0001A743
		private bool EqualsBinaryExpression(ResolvedQueryBinaryExpression left, ResolvedQueryBinaryExpression right)
		{
			return right != null && this.Equals(left.Left, right.Left) && this.Equals(left.Right, right.Right);
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x0001C576 File Offset: 0x0001A776
		public override int VisitGetHashCode(ResolvedQueryAndExpression obj)
		{
			return this.GetHashCodeBinaryExpression(obj);
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x0001C57F File Offset: 0x0001A77F
		public override bool VisitEquals(ResolvedQueryAndExpression left, ResolvedQueryAndExpression right)
		{
			return this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x0001C589 File Offset: 0x0001A789
		public override int VisitGetHashCode(ResolvedQueryOrExpression obj)
		{
			return this.GetHashCodeBinaryExpression(obj);
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x0001C592 File Offset: 0x0001A792
		public override bool VisitEquals(ResolvedQueryOrExpression left, ResolvedQueryOrExpression right)
		{
			return this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x0001C59C File Offset: 0x0001A79C
		public override int VisitGetHashCode(ResolvedQueryAggregationExpression obj)
		{
			return Hashing.CombineHash(obj.Function.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x0001C5C9 File Offset: 0x0001A7C9
		public override bool VisitEquals(ResolvedQueryAggregationExpression left, ResolvedQueryAggregationExpression right)
		{
			return right != null && left.Function == right.Function && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x0001C5EC File Offset: 0x0001A7EC
		public override int VisitGetHashCode(ResolvedQueryArithmeticExpression obj)
		{
			return Hashing.CombineHash(obj.Operator.GetHashCode(), this.GetHashCodeBinaryExpression(obj));
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x0001C619 File Offset: 0x0001A819
		public override bool VisitEquals(ResolvedQueryArithmeticExpression left, ResolvedQueryArithmeticExpression right)
		{
			return right != null && left.Operator == right.Operator && this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x0001C63C File Offset: 0x0001A83C
		public override int VisitGetHashCode(ResolvedQueryBetweenExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Expression), this.GetHashCode(obj.LowerBound), this.GetHashCode(obj.UpperBound));
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x0001C668 File Offset: 0x0001A868
		public override bool VisitEquals(ResolvedQueryBetweenExpression left, ResolvedQueryBetweenExpression right)
		{
			return right != null && this.Equals(left.Expression, right.Expression) && this.Equals(left.LowerBound, right.LowerBound) && this.Equals(left.UpperBound, right.UpperBound);
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x0001C6BC File Offset: 0x0001A8BC
		public override int VisitGetHashCode(ResolvedQueryComparisonExpression obj)
		{
			return Hashing.CombineHash(obj.ComparisonKind.GetHashCode(), this.GetHashCodeBinaryExpression(obj));
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0001C6E9 File Offset: 0x0001A8E9
		public override bool VisitEquals(ResolvedQueryComparisonExpression left, ResolvedQueryComparisonExpression right)
		{
			return right != null && left.ComparisonKind == right.ComparisonKind && this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x0001C70C File Offset: 0x0001A90C
		public override int VisitGetHashCode(ResolvedQueryContainsExpression obj)
		{
			return this.GetHashCodeBinaryExpression(obj);
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x0001C715 File Offset: 0x0001A915
		public override bool VisitEquals(ResolvedQueryContainsExpression left, ResolvedQueryContainsExpression right)
		{
			return this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x0001C720 File Offset: 0x0001A920
		public override int VisitGetHashCode(ResolvedQueryDateAddExpression obj)
		{
			return Hashing.CombineHash(obj.Amount.GetHashCode(), obj.TimeUnit.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x0001C75C File Offset: 0x0001A95C
		public override bool VisitEquals(ResolvedQueryDateAddExpression left, ResolvedQueryDateAddExpression right)
		{
			return right != null && left.Amount == right.Amount && left.TimeUnit.Equals(right.TimeUnit) && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x0001C7AC File Offset: 0x0001A9AC
		public override int VisitGetHashCode(ResolvedQueryDateSpanExpression obj)
		{
			return Hashing.CombineHash(obj.TimeUnit.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x0001C7D9 File Offset: 0x0001A9D9
		public override bool VisitEquals(ResolvedQueryDateSpanExpression left, ResolvedQueryDateSpanExpression right)
		{
			return right != null && left.TimeUnit == right.TimeUnit && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x0001C7FC File Offset: 0x0001A9FC
		public override int VisitGetHashCode(ResolvedQueryExistsExpression obj)
		{
			return this.GetHashCodeUnaryExpression(obj);
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x0001C805 File Offset: 0x0001AA05
		public override bool VisitEquals(ResolvedQueryExistsExpression left, ResolvedQueryExistsExpression right)
		{
			return this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x0001C810 File Offset: 0x0001AA10
		public override int VisitGetHashCode(ResolvedQueryFloorExpression obj)
		{
			return Hashing.CombineHash(obj.Size.GetHashCode(), Hashing.GetHashCode<TimeUnit?>(obj.TimeUnit, null), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x0001C844 File Offset: 0x0001AA44
		public override bool VisitEquals(ResolvedQueryFloorExpression left, ResolvedQueryFloorExpression right)
		{
			if (right != null && left.Size == right.Size)
			{
				TimeUnit? timeUnit = left.TimeUnit;
				TimeUnit? timeUnit2 = right.TimeUnit;
				if ((timeUnit.GetValueOrDefault() == timeUnit2.GetValueOrDefault()) & (timeUnit != null == (timeUnit2 != null)))
				{
					return this.EqualsUnaryExpression(left, right);
				}
			}
			return false;
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x0001C8A4 File Offset: 0x0001AAA4
		public override int VisitGetHashCode(ResolvedQueryInExpression obj)
		{
			int num = Hashing.CombineHash(Hashing.CombineHash<ResolvedQueryExpression>(obj.Expressions, obj.Expressions.Count, this), Hashing.GetHashCode<QueryEqualitySemanticsKind?>(obj.EqualityKind, null));
			if (obj.HasValues)
			{
				IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values = obj.Values;
				for (int i = 0; i < values.Count; i++)
				{
					num = Hashing.CombineHash(num, Hashing.CombineHash<ResolvedQueryExpression>(values[i], values[i].Count, this));
				}
			}
			else
			{
				num = Hashing.CombineHash(num, this.GetHashCode(obj.Table));
			}
			return num;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x0001C930 File Offset: 0x0001AB30
		public override bool VisitEquals(ResolvedQueryInExpression left, ResolvedQueryInExpression right)
		{
			if (right == null)
			{
				return false;
			}
			if (!left.Expressions.SequenceEqualReadOnly(right.Expressions, this))
			{
				return false;
			}
			if (left.HasValues)
			{
				if (!right.HasValues)
				{
					return false;
				}
				IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values = left.Values;
				IReadOnlyList<IReadOnlyList<ResolvedQueryExpression>> values2 = right.Values;
				if (values.Count != values2.Count)
				{
					return false;
				}
				for (int i = 0; i < values.Count; i++)
				{
					if (!values[i].SequenceEqualReadOnly(values2[i]))
					{
						return false;
					}
				}
			}
			else
			{
				if (!right.HasTable)
				{
					return false;
				}
				if (!this.Equals(left.Table, right.Table))
				{
					return false;
				}
			}
			QueryEqualitySemanticsKind? equalityKind = left.EqualityKind;
			QueryEqualitySemanticsKind? equalityKind2 = right.EqualityKind;
			return (equalityKind.GetValueOrDefault() == equalityKind2.GetValueOrDefault()) & (equalityKind != null == (equalityKind2 != null));
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x0001CA08 File Offset: 0x0001AC08
		public override int VisitGetHashCode(ResolvedQueryScopedEvalExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Expression), Hashing.CombineHash<ResolvedQueryExpression>(obj.Scope, obj.Scope.Count, this));
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x0001CA32 File Offset: 0x0001AC32
		public override bool VisitEquals(ResolvedQueryScopedEvalExpression left, ResolvedQueryScopedEvalExpression right)
		{
			return !(right == null) && this.Equals(left.Expression, right.Expression) && left.Scope.SequenceEqualReadOnly(right.Scope, this);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		public override int VisitGetHashCode(ResolvedQueryFilteredEvalExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Expression), Hashing.CombineHashReadOnly<ResolvedQueryFilter>(obj.Filters, new Func<ResolvedQueryFilter, int>(this.StructureComparer.GetHashCode)));
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public override bool VisitEquals(ResolvedQueryFilteredEvalExpression left, ResolvedQueryFilteredEvalExpression right)
		{
			return right != null && this.Equals(left.Expression, right.Expression) && left.Filters.SequenceEqualReadOnly(right.Filters, new Func<ResolvedQueryFilter, ResolvedQueryFilter, bool>(this.StructureComparer.Equals));
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x0001CAEB File Offset: 0x0001ACEB
		public override int VisitGetHashCode(ResolvedQueryLiteralExpression obj)
		{
			return obj.Value.GetHashCode();
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x0001CAF8 File Offset: 0x0001ACF8
		public override bool VisitEquals(ResolvedQueryLiteralExpression left, ResolvedQueryLiteralExpression right)
		{
			return right != null && left.Value.Equals(right.Value);
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x0001CB16 File Offset: 0x0001AD16
		public override int VisitGetHashCode(ResolvedQueryNowExpression obj)
		{
			return obj.GetType().GetHashCode();
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x0001CB23 File Offset: 0x0001AD23
		public override bool VisitEquals(ResolvedQueryNowExpression left, ResolvedQueryNowExpression right)
		{
			return right != null;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x0001CB2C File Offset: 0x0001AD2C
		public override int VisitGetHashCode(ResolvedQueryPercentileExpression obj)
		{
			return Hashing.CombineHash(obj.Exclusive.GetHashCode(), obj.K.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x0001CB61 File Offset: 0x0001AD61
		public override bool VisitEquals(ResolvedQueryPercentileExpression left, ResolvedQueryPercentileExpression right)
		{
			return right != null && left.Exclusive == right.Exclusive && left.K == right.K && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x0001CB94 File Offset: 0x0001AD94
		public override int VisitGetHashCode(ResolvedQueryMinExpression obj)
		{
			return Hashing.CombineHash(obj.IncludeAllTypes.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x0001CBC1 File Offset: 0x0001ADC1
		public override bool VisitEquals(ResolvedQueryMinExpression left, ResolvedQueryMinExpression right)
		{
			return right != null && left.IncludeAllTypes == right.IncludeAllTypes && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x0001CBE4 File Offset: 0x0001ADE4
		public override int VisitGetHashCode(ResolvedQueryMaxExpression obj)
		{
			return Hashing.CombineHash(obj.IncludeAllTypes.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x0001CC11 File Offset: 0x0001AE11
		public override bool VisitEquals(ResolvedQueryMaxExpression left, ResolvedQueryMaxExpression right)
		{
			return right != null && left.IncludeAllTypes == right.IncludeAllTypes && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x0001CC34 File Offset: 0x0001AE34
		public override int VisitGetHashCode(ResolvedQueryStartsWithExpression obj)
		{
			return this.GetHashCodeBinaryExpression(obj);
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x0001CC3D File Offset: 0x0001AE3D
		public override bool VisitEquals(ResolvedQueryStartsWithExpression left, ResolvedQueryStartsWithExpression right)
		{
			return this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x0001CC47 File Offset: 0x0001AE47
		public override int VisitGetHashCode(ResolvedQueryEndsWithExpression obj)
		{
			return this.GetHashCodeBinaryExpression(obj);
		}

		// Token: 0x06000EFB RID: 3835 RVA: 0x0001CC50 File Offset: 0x0001AE50
		public override bool VisitEquals(ResolvedQueryEndsWithExpression left, ResolvedQueryEndsWithExpression right)
		{
			return this.EqualsBinaryExpression(left, right);
		}

		// Token: 0x06000EFC RID: 3836 RVA: 0x0001CC5A File Offset: 0x0001AE5A
		public override int VisitGetHashCode(ResolvedQueryDefaultValueExpression obj)
		{
			return obj.GetType().GetHashCode();
		}

		// Token: 0x06000EFD RID: 3837 RVA: 0x0001CC67 File Offset: 0x0001AE67
		public override bool VisitEquals(ResolvedQueryDefaultValueExpression left, ResolvedQueryDefaultValueExpression right)
		{
			return right != null;
		}

		// Token: 0x06000EFE RID: 3838 RVA: 0x0001CC70 File Offset: 0x0001AE70
		public override int VisitGetHashCode(ResolvedQueryAnyValueExpression obj)
		{
			return obj.DefaultValueOverridesAncestors.GetHashCode();
		}

		// Token: 0x06000EFF RID: 3839 RVA: 0x0001CC8B File Offset: 0x0001AE8B
		public override bool VisitEquals(ResolvedQueryAnyValueExpression left, ResolvedQueryAnyValueExpression right)
		{
			return right != null && left.DefaultValueOverridesAncestors == right.DefaultValueOverridesAncestors;
		}

		// Token: 0x06000F00 RID: 3840 RVA: 0x0001CCA6 File Offset: 0x0001AEA6
		public override int VisitGetHashCode(ResolvedQueryTransformOutputRoleRefExpression obj)
		{
			return obj.Role.GetHashCode();
		}

		// Token: 0x06000F01 RID: 3841 RVA: 0x0001CCB3 File Offset: 0x0001AEB3
		public override bool VisitEquals(ResolvedQueryTransformOutputRoleRefExpression left, ResolvedQueryTransformOutputRoleRefExpression right)
		{
			return right != null && string.Equals(left.Role, right.Role, StringComparison.Ordinal);
		}

		// Token: 0x06000F02 RID: 3842 RVA: 0x0001CCD2 File Offset: 0x0001AED2
		public override int VisitGetHashCode(ResolvedQueryTransformTableColumnExpression obj)
		{
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Table.Name), QueryNameComparer.Instance.GetHashCode(obj.Column.Name));
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x0001CD04 File Offset: 0x0001AF04
		public override bool VisitEquals(ResolvedQueryTransformTableColumnExpression left, ResolvedQueryTransformTableColumnExpression right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Table.Name, right.Table.Name) && QueryNameComparer.Instance.Equals(left.Column.Name, right.Column.Name);
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x0001CD60 File Offset: 0x0001AF60
		public override int VisitGetHashCode(ResolvedQueryDiscretizeExpression obj)
		{
			return Hashing.CombineHash(obj.Count.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000F05 RID: 3845 RVA: 0x0001CD87 File Offset: 0x0001AF87
		public override bool VisitEquals(ResolvedQueryDiscretizeExpression left, ResolvedQueryDiscretizeExpression right)
		{
			return right != null && left.Count == right.Count && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000F06 RID: 3846 RVA: 0x0001CDAC File Offset: 0x0001AFAC
		public override int VisitGetHashCode(ResolvedQuerySparklineDataExpression obj)
		{
			return Hashing.CombineHash(this.GetHashCode(obj.Measure), Hashing.CombineHash<ResolvedQueryExpression>(obj.Groupings, obj.Groupings.Count, this), obj.PointsPerSparkline.GetHashCode(), this.GetHashCode(obj.ScalarKey), obj.IncludeMinGroupingInterval.GetHashCode());
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x0001CE0C File Offset: 0x0001B00C
		public override bool VisitEquals(ResolvedQuerySparklineDataExpression left, ResolvedQuerySparklineDataExpression right)
		{
			return right != null && this.Equals(left.Measure, right.Measure) && left.Groupings.SequenceEqualReadOnly(right.Groupings, this) && left.PointsPerSparkline == right.PointsPerSparkline && this.Equals(left.ScalarKey, right.ScalarKey) && left.IncludeMinGroupingInterval == right.IncludeMinGroupingInterval;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x0001CE7C File Offset: 0x0001B07C
		public override int VisitGetHashCode(ResolvedQueryMemberExpression obj)
		{
			return Hashing.CombineHash(obj.Member.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x0001CE95 File Offset: 0x0001B095
		public override bool VisitEquals(ResolvedQueryMemberExpression left, ResolvedQueryMemberExpression right)
		{
			return right != null && left.Member == right.Member && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x0001CEBD File Offset: 0x0001B0BD
		public override int VisitGetHashCode(ResolvedQueryNativeFormatExpression obj)
		{
			return Hashing.CombineHash(obj.FormatString.GetHashCode(), this.GetHashCodeUnaryExpression(obj));
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x0001CED6 File Offset: 0x0001B0D6
		public override bool VisitEquals(ResolvedQueryNativeFormatExpression left, ResolvedQueryNativeFormatExpression right)
		{
			return right != null && left.FormatString == right.FormatString && this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x0001CEFE File Offset: 0x0001B0FE
		public override int VisitGetHashCode(ResolvedQueryNativeMeasureExpression obj)
		{
			return Hashing.CombineHash(obj.Language.GetHashCode(), obj.Expression.GetHashCode());
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x0001CF1B File Offset: 0x0001B11B
		public override bool VisitEquals(ResolvedQueryNativeMeasureExpression left, ResolvedQueryNativeMeasureExpression right)
		{
			return right != null && left.Language == right.Language && left.Expression == right.Expression;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x0001CF4C File Offset: 0x0001B14C
		public override int VisitGetHashCode(ResolvedQueryLetRefExpression obj)
		{
			return QueryNameComparer.Instance.GetHashCode(obj.Binding.Name);
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x0001CF63 File Offset: 0x0001B163
		public override bool VisitEquals(ResolvedQueryLetRefExpression left, ResolvedQueryLetRefExpression right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Binding.Name, right.Binding.Name);
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x0001CF90 File Offset: 0x0001B190
		public override int VisitGetHashCode(ResolvedQueryRoleRefExpression obj)
		{
			return QueryValueComparers.RoleRefComparer.GetHashCode(obj.Role);
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x0001CFA2 File Offset: 0x0001B1A2
		public override bool VisitEquals(ResolvedQueryRoleRefExpression left, ResolvedQueryRoleRefExpression right)
		{
			return right != null && QueryValueComparers.RoleRefComparer.Equals(left.Role, right.Role);
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x0001CFC5 File Offset: 0x0001B1C5
		public override int VisitGetHashCode(ResolvedSummaryValueRefExpression obj)
		{
			return QueryValueComparers.SummaryValueRefComparer.GetHashCode(obj.Name);
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x0001CFD7 File Offset: 0x0001B1D7
		public override bool VisitEquals(ResolvedSummaryValueRefExpression left, ResolvedSummaryValueRefExpression right)
		{
			return right != null && QueryValueComparers.SummaryValueRefComparer.Equals(left.Name, right.Name);
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x0001CFFA File Offset: 0x0001B1FA
		public override int VisitGetHashCode(ResolvedQueryParameterRefExpression obj)
		{
			return QueryNameComparer.Instance.GetHashCode(obj.Declaration.Name);
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x0001D011 File Offset: 0x0001B211
		public override bool VisitEquals(ResolvedQueryParameterRefExpression left, ResolvedQueryParameterRefExpression right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Declaration.Name, right.Declaration.Name);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x0001D03E File Offset: 0x0001B23E
		public override int VisitGetHashCode(ResolvedQueryTypeOfExpression obj)
		{
			return this.GetHashCodeUnaryExpression(obj);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x0001D047 File Offset: 0x0001B247
		public override bool VisitEquals(ResolvedQueryTypeOfExpression left, ResolvedQueryTypeOfExpression right)
		{
			return this.EqualsUnaryExpression(left, right);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x0001D051 File Offset: 0x0001B251
		public override int VisitGetHashCode(ResolvedQueryTableTypeExpression obj)
		{
			return Hashing.CombineHashReadOnly<ResolvedQueryTableTypeColumn>(obj.Columns, this);
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x0001D05F File Offset: 0x0001B25F
		public override bool VisitEquals(ResolvedQueryTableTypeExpression left, ResolvedQueryTableTypeExpression right)
		{
			return right != null && left.Columns.SequenceEqualReadOnly(right.Columns, this);
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x0001D07E File Offset: 0x0001B27E
		public override int GetHashCode(ResolvedQueryTableTypeColumn obj)
		{
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Name), this.GetHashCode(obj.Expression));
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x0001D0A1 File Offset: 0x0001B2A1
		public override bool Equals(ResolvedQueryTableTypeColumn left, ResolvedQueryTableTypeColumn right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Name, right.Name) && this.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x0001D0D4 File Offset: 0x0001B2D4
		public override int VisitGetHashCode(ResolvedQueryPrimitiveTypeExpression obj)
		{
			return obj.Type.GetHashCode();
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x0001D0F5 File Offset: 0x0001B2F5
		public override bool VisitEquals(ResolvedQueryPrimitiveTypeExpression left, ResolvedQueryPrimitiveTypeExpression right)
		{
			return right != null && left.Type == right.Type;
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x0001D110 File Offset: 0x0001B310
		public override int VisitGetHashCode(ResolvedQueryNativeVisualCalculationExpression obj)
		{
			return Hashing.CombineHash(obj.Language.GetHashCode(), obj.Expression.GetHashCode());
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x0001D12D File Offset: 0x0001B32D
		public override bool VisitEquals(ResolvedQueryNativeVisualCalculationExpression left, ResolvedQueryNativeVisualCalculationExpression right)
		{
			return right != null && left.Language == right.Language && left.Expression == right.Expression;
		}
	}
}

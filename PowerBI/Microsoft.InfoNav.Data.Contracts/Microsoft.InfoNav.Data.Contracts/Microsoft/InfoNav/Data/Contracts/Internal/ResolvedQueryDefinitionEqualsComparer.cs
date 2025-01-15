using System;
using System.Linq;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000202 RID: 514
	public abstract class ResolvedQueryDefinitionEqualsComparer : ResolvedQueryDefinitionEqualityComparer
	{
		// Token: 0x170003E5 RID: 997
		// (get) Token: 0x06000E21 RID: 3617
		protected abstract ResolvedQueryExpressionEqualityComparer ExpressionComparer { get; }

		// Token: 0x06000E22 RID: 3618 RVA: 0x0001B524 File Offset: 0x00019724
		public override bool Equals(ResolvedQueryDefinition left, ResolvedQueryDefinition right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryDefinition>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			if (!this.HasSameQueryShape(left, right))
			{
				return false;
			}
			if (this.EqualsQueryDefinitionName(left.Name, right.Name))
			{
				int? top = left.Top;
				int? top2 = right.Top;
				if ((top.GetValueOrDefault() == top2.GetValueOrDefault()) & (top != null == (top2 != null)))
				{
					long? skip = left.Skip;
					long? skip2 = right.Skip;
					if (((skip.GetValueOrDefault() == skip2.GetValueOrDefault()) & (skip != null == (skip2 != null))) && left.Parameters.SequenceEqualReadOnly(right.Parameters, this) && left.Let.SequenceEqualReadOnly(right.Let, this) && left.From.SequenceEqualReadOnly(right.From, this) && left.Where.SequenceEqualReadOnly(right.Where, this) && left.Transform.SequenceEqualReadOnly(right.Transform, this) && left.OrderBy.SequenceEqualReadOnly(right.OrderBy, this) && left.GroupBy.SequenceEqualReadOnly(right.GroupBy, new Func<ResolvedQueryExpression, ResolvedQueryExpression, bool>(this.ExpressionComparer.Equals)) && left.Select.SequenceEqualReadOnly(right.Select, this))
					{
						return left.VisualShape.SequenceEqualReadOnly(right.VisualShape, this);
					}
				}
			}
			return false;
		}

		// Token: 0x06000E23 RID: 3619 RVA: 0x0001B6A4 File Offset: 0x000198A4
		protected virtual bool EqualsQueryDefinitionName(string leftName, string rightName)
		{
			return QueryNameComparer.Instance.Equals(leftName, rightName);
		}

		// Token: 0x06000E24 RID: 3620 RVA: 0x0001B6B4 File Offset: 0x000198B4
		protected bool HasSameQueryShape(ResolvedQueryDefinition left, ResolvedQueryDefinition right)
		{
			return left.Parameters.Count == right.Parameters.Count || left.Let.Count == right.Let.Count || left.From.Count == right.From.Count || left.Where.Count == right.Where.Count || left.Transform.Count == right.Transform.Count || left.OrderBy.Count == right.OrderBy.Count || left.GroupBy.Count == right.GroupBy.Count || left.Select.Count == right.Select.Count;
		}

		// Token: 0x06000E25 RID: 3621 RVA: 0x0001B78C File Offset: 0x0001998C
		public override int GetHashCode(ResolvedQueryDefinition obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.Name, QueryNameComparer.Instance), Hashing.GetHashCode<int?>(obj.Top, null), Hashing.GetHashCode<long?>(obj.Skip, null), Hashing.CombineHashReadOnly<ResolvedQueryParameterDeclaration>(obj.Parameters, this), Hashing.CombineHashReadOnly<ResolvedQueryLetBinding>(obj.Let, this), Hashing.CombineHashReadOnly<ResolvedQuerySource>(obj.From, this), Hashing.CombineHashReadOnly<ResolvedQueryFilter>(obj.Where, this), Hashing.CombineHashReadOnly<ResolvedQueryTransform>(obj.Transform, this), Hashing.CombineHashReadOnly<ResolvedQuerySortClause>(obj.OrderBy, this), Hashing.CombineHashReadOnly<ResolvedQueryExpression>(obj.GroupBy, new Func<ResolvedQueryExpression, int>(this.ExpressionComparer.GetHashCode)), Hashing.CombineHashReadOnly<ResolvedQuerySelect>(obj.Select, this));
		}

		// Token: 0x06000E26 RID: 3622 RVA: 0x0001B840 File Offset: 0x00019A40
		public override bool Equals(ResolvedQueryParameterDeclaration left, ResolvedQueryParameterDeclaration right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryParameterDeclaration>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && this.ExpressionComparer.Equals(left.TypeExpression, right.TypeExpression);
		}

		// Token: 0x06000E27 RID: 3623 RVA: 0x0001B897 File Offset: 0x00019A97
		public override int GetHashCode(ResolvedQueryParameterDeclaration obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.Name, QueryNameComparer.Instance), this.ExpressionComparer.GetHashCode(obj.TypeExpression));
		}

		// Token: 0x06000E28 RID: 3624 RVA: 0x0001B8C8 File Offset: 0x00019AC8
		public override bool Equals(ResolvedQueryLetBinding left, ResolvedQueryLetBinding right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryLetBinding>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E29 RID: 3625 RVA: 0x0001B91F File Offset: 0x00019B1F
		public override int GetHashCode(ResolvedQueryLetBinding obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.Name, QueryNameComparer.Instance), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x0001B950 File Offset: 0x00019B50
		public override bool Equals(ResolvedQueryFilter left, ResolvedQueryFilter right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryFilter>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Target.SequenceEqualReadOnly(right.Target, new Func<ResolvedQueryExpression, ResolvedQueryExpression, bool>(this.ExpressionComparer.Equals)) && this.ExpressionComparer.Equals(left.Condition, right.Condition);
		}

		// Token: 0x06000E2B RID: 3627 RVA: 0x0001B9B4 File Offset: 0x00019BB4
		public override int GetHashCode(ResolvedQueryFilter obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.CombineHashReadOnly<ResolvedQueryExpression>(obj.Target, new Func<ResolvedQueryExpression, int>(this.ExpressionComparer.GetHashCode)), this.ExpressionComparer.GetHashCode(obj.Condition));
		}

		// Token: 0x06000E2C RID: 3628 RVA: 0x0001B9F4 File Offset: 0x00019BF4
		public override bool Equals(ResolvedQueryTransform left, ResolvedQueryTransform right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransform>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && QueryValueComparers.TransformAlgorithm.Equals(left.Algorithm, right.Algorithm) && this.Equals(left.Input, right.Input) && this.Equals(left.Output, right.Output);
		}

		// Token: 0x06000E2D RID: 3629 RVA: 0x0001BA74 File Offset: 0x00019C74
		public override int GetHashCode(ResolvedQueryTransform obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Name), QueryValueComparers.TransformAlgorithm.GetHashCode(obj.Algorithm), this.GetHashCode(obj.Input), this.GetHashCode(obj.Output));
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x0001BAC8 File Offset: 0x00019CC8
		public override bool Equals(ResolvedQueryTransformInput left, ResolvedQueryTransformInput right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransformInput>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Parameters.SequenceEqualReadOnly(right.Parameters, new Func<ResolvedQueryTransformParameter, ResolvedQueryTransformParameter, bool>(this.Equals)) && this.Equals(left.Table, right.Table);
		}

		// Token: 0x06000E2F RID: 3631 RVA: 0x0001BB22 File Offset: 0x00019D22
		public override int GetHashCode(ResolvedQueryTransformInput obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.CombineHashReadOnly<ResolvedQueryTransformParameter>(obj.Parameters, this), this.GetHashCode(obj.Table));
		}

		// Token: 0x06000E30 RID: 3632 RVA: 0x0001BB4C File Offset: 0x00019D4C
		public override bool Equals(ResolvedQueryTransformOutput left, ResolvedQueryTransformOutput right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransformOutput>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return this.Equals(left.Table, right.Table);
		}

		// Token: 0x06000E31 RID: 3633 RVA: 0x0001BB84 File Offset: 0x00019D84
		public override int GetHashCode(ResolvedQueryTransformOutput obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return this.GetHashCode(obj.Table);
		}

		// Token: 0x06000E32 RID: 3634 RVA: 0x0001BB9C File Offset: 0x00019D9C
		public override bool Equals(ResolvedQueryTransformParameter left, ResolvedQueryTransformParameter right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransformParameter>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x0001BBF3 File Offset: 0x00019DF3
		public override int GetHashCode(ResolvedQueryTransformParameter obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Name), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0001BC24 File Offset: 0x00019E24
		public override bool Equals(ResolvedQueryTransformTable left, ResolvedQueryTransformTable right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransformTable>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && left.Columns.SequenceEqual(right.Columns, this);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x0001BC76 File Offset: 0x00019E76
		public override int GetHashCode(ResolvedQueryTransformTable obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Name), Hashing.CombineHashReadOnly<ResolvedQueryTransformTableColumn>(obj.Columns, this));
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x0001BCA4 File Offset: 0x00019EA4
		public override bool Equals(ResolvedQueryTransformTableColumn left, ResolvedQueryTransformTableColumn right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryTransformTableColumn>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return QueryNameComparer.Instance.Equals(left.Name, right.Name) && StringComparer.Ordinal.Equals(left.Role, right.Role) && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x0001BD14 File Offset: 0x00019F14
		public override int GetHashCode(ResolvedQueryTransformTableColumn obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(QueryNameComparer.Instance.GetHashCode(obj.Name), Hashing.GetHashCode<string>(obj.Role, StringComparer.Ordinal), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x0001BD60 File Offset: 0x00019F60
		public override bool Equals(ResolvedQuerySortClause left, ResolvedQuerySortClause right)
		{
			bool? flag = Util.AreEqual<ResolvedQuerySortClause>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Direction == right.Direction && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x0001BDB0 File Offset: 0x00019FB0
		public override int GetHashCode(ResolvedQuerySortClause obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(obj.Direction.GetHashCode(), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x0001BDF0 File Offset: 0x00019FF0
		protected virtual bool EqualsQuerySource(ResolvedQuerySource left, ResolvedQuerySource right)
		{
			return right != null && QueryNameComparer.Instance.Equals(left.Name, right.Name);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x0001BE0D File Offset: 0x0001A00D
		private int GetHashCodeQuerySoure(ResolvedQuerySource obj)
		{
			return QueryNameComparer.Instance.GetHashCode(obj.Name);
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x0001BE1F File Offset: 0x0001A01F
		public override bool Equals(ResolvedEntitySource left, ResolvedEntitySource right)
		{
			return this.EqualsQuerySource(left, right) && QueryNameComparer.Instance.Equals(left.Schema, right.Schema) && left.Entity == right.Entity;
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x0001BE53 File Offset: 0x0001A053
		public override int GetHashCode(ResolvedEntitySource obj)
		{
			return Hashing.CombineHash(this.GetHashCodeQuerySoure(obj), Hashing.GetHashCode<string>(obj.Schema, QueryNameComparer.Instance), obj.Entity.GetHashCode());
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x0001BE7C File Offset: 0x0001A07C
		public override bool Equals(ResolvedExpressionSource left, ResolvedExpressionSource right)
		{
			return this.EqualsQuerySource(left, right) && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x0001BEA1 File Offset: 0x0001A0A1
		public override int GetHashCode(ResolvedExpressionSource obj)
		{
			return Hashing.CombineHash(this.GetHashCodeQuerySoure(obj), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x0001BEC0 File Offset: 0x0001A0C0
		public override bool Equals(ResolvedQuerySelect left, ResolvedQuerySelect right)
		{
			bool? flag = Util.AreEqual<ResolvedQuerySelect>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(left.Name, right.Name) && ConceptualNameComparer.Instance.Equals(left.NativeReferenceName, right.NativeReferenceName) && this.ExpressionComparer.Equals(left.Expression, right.Expression);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x0001BF30 File Offset: 0x0001A130
		public override int GetHashCode(ResolvedQuerySelect obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.Name, ConceptualNameComparer.Instance), Hashing.GetHashCode<string>(obj.NativeReferenceName, ConceptualNameComparer.Instance), this.ExpressionComparer.GetHashCode(obj.Expression));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0001BF7C File Offset: 0x0001A17C
		public override bool Equals(ResolvedQueryAxis left, ResolvedQueryAxis right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryAxis>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return ConceptualNameComparer.Instance.Equals(left.Name, right.Name) && left.Groups.SequenceEqualReadOnly(right.Groups, new Func<ResolvedQueryAxisGroup, ResolvedQueryAxisGroup, bool>(this.Equals));
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x0001BFDA File Offset: 0x0001A1DA
		public override int GetHashCode(ResolvedQueryAxis obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(Hashing.GetHashCode<string>(obj.Name, ConceptualNameComparer.Instance), Hashing.CombineHashReadOnly<ResolvedQueryAxisGroup>(obj.Groups, new Func<ResolvedQueryAxisGroup, int>(this.GetHashCode)));
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x0001C014 File Offset: 0x0001A214
		public override bool Equals(ResolvedQueryAxisGroup left, ResolvedQueryAxisGroup right)
		{
			bool? flag = Util.AreEqual<ResolvedQueryAxisGroup>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.Subtotal == right.Subtotal && left.Keys.SequenceEqualReadOnly(right.Keys, new Func<ResolvedQueryExpression, ResolvedQueryExpression, bool>(this.ExpressionComparer.Equals));
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x0001C070 File Offset: 0x0001A270
		public override int GetHashCode(ResolvedQueryAxisGroup obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return Hashing.CombineHash(obj.Subtotal.GetHashCode(), Hashing.CombineHashReadOnly<ResolvedQueryExpression>(obj.Keys, new Func<ResolvedQueryExpression, int>(this.ExpressionComparer.GetHashCode)));
		}
	}
}

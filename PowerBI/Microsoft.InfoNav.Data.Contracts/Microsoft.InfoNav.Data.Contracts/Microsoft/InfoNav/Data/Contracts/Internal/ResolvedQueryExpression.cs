using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200022E RID: 558
	[ImmutableObject(true)]
	public abstract class ResolvedQueryExpression : IEquatable<ResolvedQueryExpression>
	{
		// Token: 0x06001017 RID: 4119
		public abstract void Accept(ResolvedQueryExpressionVisitor visitor);

		// Token: 0x06001018 RID: 4120
		public abstract T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor);

		// Token: 0x06001019 RID: 4121
		public abstract bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other);

		// Token: 0x0600101A RID: 4122
		public abstract int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer);

		// Token: 0x0600101B RID: 4123 RVA: 0x0001E5F8 File Offset: 0x0001C7F8
		public static bool operator ==(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return object.Equals(left, right);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0001E601 File Offset: 0x0001C801
		public static bool operator !=(ResolvedQueryExpression left, ResolvedQueryExpression right)
		{
			return !object.Equals(left, right);
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0001E60D File Offset: 0x0001C80D
		public virtual bool Equals(ResolvedQueryExpression other)
		{
			return DefaultResolvedQueryExpressionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0001E61B File Offset: 0x0001C81B
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryExpression);
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0001E629 File Offset: 0x0001C829
		public override int GetHashCode()
		{
			return DefaultResolvedQueryExpressionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}

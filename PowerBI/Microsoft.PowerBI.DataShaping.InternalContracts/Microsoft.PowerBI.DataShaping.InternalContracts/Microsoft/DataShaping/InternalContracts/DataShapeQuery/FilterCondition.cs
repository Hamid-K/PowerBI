using System;
using System.Collections.Generic;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery.Writer;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x02000094 RID: 148
	internal abstract class FilterCondition : IIdentifiable, IEquatable<FilterCondition>, IStructuredToString, IExpressionEquatable<FilterCondition>
	{
		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000383 RID: 899
		public abstract ObjectType ObjectType { get; }

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0000701D File Offset: 0x0000521D
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00007025 File Offset: 0x00005225
		public Identifier Id { get; set; }

		// Token: 0x06000386 RID: 902
		public abstract TResult Accept<TResult>(FilterVisitor<TResult> visitor);

		// Token: 0x06000387 RID: 903 RVA: 0x0000702E File Offset: 0x0000522E
		public override bool Equals(object obj)
		{
			return this.Equals(obj as FilterCondition);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0000703C File Offset: 0x0000523C
		public bool Equals(FilterCondition other)
		{
			return this.Equals(other, FilterConditionComparer.DefaultExpressionComparer);
		}

		// Token: 0x06000389 RID: 905
		public abstract bool Equals(FilterCondition other, IEqualityComparer<Expression> expressionComparer);

		// Token: 0x0600038A RID: 906 RVA: 0x0000704A File Offset: 0x0000524A
		public int GetHashCode(IEqualityComparer<Expression> expressionComparer)
		{
			return this.GetHashCodeImpl(expressionComparer);
		}

		// Token: 0x0600038B RID: 907 RVA: 0x00007053 File Offset: 0x00005253
		public override int GetHashCode()
		{
			return this.GetHashCodeImpl(FilterConditionComparer.DefaultExpressionComparer);
		}

		// Token: 0x0600038C RID: 908
		protected abstract int GetHashCodeImpl(IEqualityComparer<Expression> exprComparer);

		// Token: 0x0600038D RID: 909 RVA: 0x00007060 File Offset: 0x00005260
		protected static bool CheckReferenceAndTypeEquality<TCondition>(TCondition @this, FilterCondition other, out bool areEqual, out TCondition otherTyped) where TCondition : FilterCondition
		{
			if (@this == other)
			{
				areEqual = true;
				otherTyped = default(TCondition);
				return true;
			}
			if (@this == null || other == null)
			{
				areEqual = false;
				otherTyped = default(TCondition);
				return true;
			}
			otherTyped = other as TCondition;
			if (otherTyped == null)
			{
				areEqual = false;
				return true;
			}
			areEqual = false;
			return false;
		}

		// Token: 0x0600038E RID: 910
		public abstract void WriteTo(StructuredStringBuilder builder);
	}
}

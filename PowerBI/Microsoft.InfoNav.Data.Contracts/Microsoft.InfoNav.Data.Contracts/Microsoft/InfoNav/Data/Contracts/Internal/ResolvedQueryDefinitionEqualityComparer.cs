using System;
using System.Collections.Generic;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000201 RID: 513
	public abstract class ResolvedQueryDefinitionEqualityComparer : IEqualityComparer<ResolvedQueryDefinition>, IEqualityComparer<ResolvedQueryParameterDeclaration>, IEqualityComparer<ResolvedQueryLetBinding>, IEqualityComparer<ResolvedQuerySource>, IEqualityComparer<ResolvedQueryFilter>, IEqualityComparer<ResolvedQueryTransform>, IEqualityComparer<ResolvedQueryTransformParameter>, IEqualityComparer<ResolvedQueryTransformTableColumn>, IEqualityComparer<ResolvedQuerySortClause>, IEqualityComparer<ResolvedQuerySelect>, IEqualityComparer<ResolvedQueryAxis>, IEqualityComparer<ResolvedQueryAxisGroup>
	{
		// Token: 0x06000DFE RID: 3582
		public abstract bool Equals(ResolvedQueryDefinition left, ResolvedQueryDefinition right);

		// Token: 0x06000DFF RID: 3583
		public abstract int GetHashCode(ResolvedQueryDefinition obj);

		// Token: 0x06000E00 RID: 3584
		public abstract bool Equals(ResolvedQueryParameterDeclaration left, ResolvedQueryParameterDeclaration right);

		// Token: 0x06000E01 RID: 3585
		public abstract int GetHashCode(ResolvedQueryParameterDeclaration obj);

		// Token: 0x06000E02 RID: 3586
		public abstract bool Equals(ResolvedQueryLetBinding left, ResolvedQueryLetBinding right);

		// Token: 0x06000E03 RID: 3587
		public abstract int GetHashCode(ResolvedQueryLetBinding obj);

		// Token: 0x06000E04 RID: 3588 RVA: 0x0001B4DC File Offset: 0x000196DC
		public bool Equals(ResolvedQuerySource left, ResolvedQuerySource right)
		{
			bool? flag = Util.AreEqual<ResolvedQuerySource>(left, right);
			if (flag != null)
			{
				return flag.Value;
			}
			return left.AcceptEquals(this, right);
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x0001B50A File Offset: 0x0001970A
		public int GetHashCode(ResolvedQuerySource obj)
		{
			if (obj == null)
			{
				return -48879;
			}
			return obj.AcceptGetHashCode(this);
		}

		// Token: 0x06000E06 RID: 3590
		public abstract bool Equals(ResolvedQueryFilter left, ResolvedQueryFilter right);

		// Token: 0x06000E07 RID: 3591
		public abstract int GetHashCode(ResolvedQueryFilter obj);

		// Token: 0x06000E08 RID: 3592
		public abstract bool Equals(ResolvedQueryTransform left, ResolvedQueryTransform right);

		// Token: 0x06000E09 RID: 3593
		public abstract int GetHashCode(ResolvedQueryTransform obj);

		// Token: 0x06000E0A RID: 3594
		public abstract bool Equals(ResolvedQueryTransformInput left, ResolvedQueryTransformInput right);

		// Token: 0x06000E0B RID: 3595
		public abstract int GetHashCode(ResolvedQueryTransformInput obj);

		// Token: 0x06000E0C RID: 3596
		public abstract bool Equals(ResolvedQueryTransformOutput left, ResolvedQueryTransformOutput right);

		// Token: 0x06000E0D RID: 3597
		public abstract int GetHashCode(ResolvedQueryTransformOutput obj);

		// Token: 0x06000E0E RID: 3598
		public abstract bool Equals(ResolvedQueryTransformParameter left, ResolvedQueryTransformParameter right);

		// Token: 0x06000E0F RID: 3599
		public abstract int GetHashCode(ResolvedQueryTransformParameter obj);

		// Token: 0x06000E10 RID: 3600
		public abstract bool Equals(ResolvedQueryTransformTable left, ResolvedQueryTransformTable right);

		// Token: 0x06000E11 RID: 3601
		public abstract int GetHashCode(ResolvedQueryTransformTable obj);

		// Token: 0x06000E12 RID: 3602
		public abstract bool Equals(ResolvedQueryTransformTableColumn left, ResolvedQueryTransformTableColumn right);

		// Token: 0x06000E13 RID: 3603
		public abstract int GetHashCode(ResolvedQueryTransformTableColumn obj);

		// Token: 0x06000E14 RID: 3604
		public abstract bool Equals(ResolvedQuerySortClause left, ResolvedQuerySortClause right);

		// Token: 0x06000E15 RID: 3605
		public abstract int GetHashCode(ResolvedQuerySortClause obj);

		// Token: 0x06000E16 RID: 3606
		public abstract bool Equals(ResolvedQuerySelect left, ResolvedQuerySelect right);

		// Token: 0x06000E17 RID: 3607
		public abstract int GetHashCode(ResolvedQuerySelect obj);

		// Token: 0x06000E18 RID: 3608
		public abstract bool Equals(ResolvedQueryAxis left, ResolvedQueryAxis right);

		// Token: 0x06000E19 RID: 3609
		public abstract int GetHashCode(ResolvedQueryAxis obj);

		// Token: 0x06000E1A RID: 3610
		public abstract bool Equals(ResolvedQueryAxisGroup left, ResolvedQueryAxisGroup right);

		// Token: 0x06000E1B RID: 3611
		public abstract int GetHashCode(ResolvedQueryAxisGroup obj);

		// Token: 0x06000E1C RID: 3612
		public abstract bool Equals(ResolvedEntitySource left, ResolvedEntitySource right);

		// Token: 0x06000E1D RID: 3613
		public abstract int GetHashCode(ResolvedEntitySource obj);

		// Token: 0x06000E1E RID: 3614
		public abstract bool Equals(ResolvedExpressionSource left, ResolvedExpressionSource right);

		// Token: 0x06000E1F RID: 3615
		public abstract int GetHashCode(ResolvedExpressionSource obj);
	}
}

using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200024E RID: 590
	[ImmutableObject(true)]
	public sealed class ResolvedQueryPropertyVariationSourceExpression : ResolvedQueryExpression
	{
		// Token: 0x060011C8 RID: 4552 RVA: 0x0001FBA5 File Offset: 0x0001DDA5
		internal ResolvedQueryPropertyVariationSourceExpression(ResolvedQuerySourceRefExpression sourceRefExpression, IConceptualVariationSource variationSource, IConceptualProperty property)
		{
			this._sourceRefExpression = sourceRefExpression;
			this._variationSource = variationSource;
			this._property = property;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x060011C9 RID: 4553 RVA: 0x0001FBC2 File Offset: 0x0001DDC2
		public ResolvedQuerySourceRefExpression SourceRefExpression
		{
			get
			{
				return this._sourceRefExpression;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x060011CA RID: 4554 RVA: 0x0001FBCA File Offset: 0x0001DDCA
		public IConceptualVariationSource VariationSource
		{
			get
			{
				return this._variationSource;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x060011CB RID: 4555 RVA: 0x0001FBD2 File Offset: 0x0001DDD2
		public IConceptualProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x060011CC RID: 4556 RVA: 0x0001FBDA File Offset: 0x0001DDDA
		public IConceptualEntity SourceEntity
		{
			get
			{
				if (this._variationSource.NavigationProperty != null)
				{
					return this._variationSource.NavigationProperty.TargetEntity;
				}
				return this._sourceRefExpression.SourceEntity;
			}
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0001FC05 File Offset: 0x0001DE05
		public override void Accept(ResolvedQueryExpressionVisitor visitor)
		{
			visitor.Visit(this);
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x0001FC0E File Offset: 0x0001DE0E
		public override T Accept<T>(ResolvedQueryExpressionVisitor<T> visitor)
		{
			return visitor.Visit(this);
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x0001FC17 File Offset: 0x0001DE17
		public override bool AcceptEquals(ResolvedQueryExpressionEqualityComparer comparer, ResolvedQueryExpression other)
		{
			return comparer.VisitEquals(this, other as ResolvedQueryPropertyVariationSourceExpression);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x0001FC26 File Offset: 0x0001DE26
		public override int AcceptGetHashCode(ResolvedQueryExpressionEqualityComparer comparer)
		{
			return comparer.VisitGetHashCode(this);
		}

		// Token: 0x04000799 RID: 1945
		private readonly ResolvedQuerySourceRefExpression _sourceRefExpression;

		// Token: 0x0400079A RID: 1946
		private readonly IConceptualVariationSource _variationSource;

		// Token: 0x0400079B RID: 1947
		private readonly IConceptualProperty _property;
	}
}

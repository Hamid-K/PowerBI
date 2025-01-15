using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001E8 RID: 488
	public class OptionalNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x060019B9 RID: 6585 RVA: 0x00045B0E File Offset: 0x00043D0E
		internal OptionalNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			navigationPropertyConfiguration.Reset();
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
			this._navigationPropertyConfiguration.RelationshipMultiplicity = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
		}

		// Token: 0x060019BA RID: 6586 RVA: 0x00045B34 File Offset: 0x00043D34
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany(Expression<Func<TTargetEntityType, ICollection<TEntityType>>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, ICollection<TEntityType>>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithMany();
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x00045B5E File Offset: 0x00043D5E
		public DependentNavigationPropertyConfiguration<TEntityType> WithMany()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.Many);
			return new DependentNavigationPropertyConfiguration<TEntityType>(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x00045B7C File Offset: 0x00043D7C
		public ForeignKeyNavigationPropertyConfiguration WithRequired(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithRequired();
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x00045BA6 File Offset: 0x00043DA6
		public ForeignKeyNavigationPropertyConfiguration WithRequired()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.One);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x00045BC4 File Offset: 0x00043DC4
		public ForeignKeyNavigationPropertyConfiguration WithOptionalDependent(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptionalDependent();
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x00045BEE File Offset: 0x00043DEE
		public ForeignKeyNavigationPropertyConfiguration WithOptionalDependent()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			this._navigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(false);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x00045C2D File Offset: 0x00043E2D
		public ForeignKeyNavigationPropertyConfiguration WithOptionalPrincipal(Expression<Func<TTargetEntityType, TEntityType>> navigationPropertyExpression)
		{
			Check.NotNull<Expression<Func<TTargetEntityType, TEntityType>>>(navigationPropertyExpression, "navigationPropertyExpression");
			this._navigationPropertyConfiguration.InverseNavigationProperty = navigationPropertyExpression.GetSimplePropertyAccess().Single<PropertyInfo>();
			return this.WithOptionalPrincipal();
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x00045C57 File Offset: 0x00043E57
		public ForeignKeyNavigationPropertyConfiguration WithOptionalPrincipal()
		{
			this._navigationPropertyConfiguration.InverseEndKind = new RelationshipMultiplicity?(RelationshipMultiplicity.ZeroOrOne);
			this._navigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			this._navigationPropertyConfiguration.IsNavigationPropertyDeclaringTypePrincipal = new bool?(true);
			return new ForeignKeyNavigationPropertyConfiguration(this._navigationPropertyConfiguration);
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x00045C96 File Offset: 0x00043E96
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x00045C9E File Offset: 0x00043E9E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x00045CA7 File Offset: 0x00043EA7
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060019C5 RID: 6597 RVA: 0x00045CAF File Offset: 0x00043EAF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A84 RID: 2692
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}

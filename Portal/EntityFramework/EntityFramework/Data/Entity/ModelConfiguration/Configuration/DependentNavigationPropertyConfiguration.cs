using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001EC RID: 492
	public class DependentNavigationPropertyConfiguration<TDependentEntityType> : ForeignKeyNavigationPropertyConfiguration where TDependentEntityType : class
	{
		// Token: 0x060019DE RID: 6622 RVA: 0x00045EA2 File Offset: 0x000440A2
		internal DependentNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
			: base(navigationPropertyConfiguration)
		{
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x00045EAC File Offset: 0x000440AC
		public CascadableNavigationPropertyConfiguration HasForeignKey<TKey>(Expression<Func<TDependentEntityType, TKey>> foreignKeyExpression)
		{
			Check.NotNull<Expression<Func<TDependentEntityType, TKey>>>(foreignKeyExpression, "foreignKeyExpression");
			base.NavigationPropertyConfiguration.Constraint = new ForeignKeyConstraintConfiguration(from p in foreignKeyExpression.GetSimplePropertyAccessList()
				select p.Single<PropertyInfo>());
			return this;
		}

		// Token: 0x060019E0 RID: 6624 RVA: 0x00045F00 File Offset: 0x00044100
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019E1 RID: 6625 RVA: 0x00045F08 File Offset: 0x00044108
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00045F11 File Offset: 0x00044111
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00045F19 File Offset: 0x00044119
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

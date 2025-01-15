using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001EE RID: 494
	public class ForeignKeyNavigationPropertyConfiguration : CascadableNavigationPropertyConfiguration
	{
		// Token: 0x060019F1 RID: 6641 RVA: 0x0004645D File Offset: 0x0004465D
		internal ForeignKeyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
			: base(navigationPropertyConfiguration)
		{
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00046468 File Offset: 0x00044668
		public CascadableNavigationPropertyConfiguration Map(Action<ForeignKeyAssociationMappingConfiguration> configurationAction)
		{
			Check.NotNull<Action<ForeignKeyAssociationMappingConfiguration>>(configurationAction, "configurationAction");
			base.NavigationPropertyConfiguration.Constraint = IndependentConstraintConfiguration.Instance;
			ForeignKeyAssociationMappingConfiguration foreignKeyAssociationMappingConfiguration = new ForeignKeyAssociationMappingConfiguration();
			configurationAction(foreignKeyAssociationMappingConfiguration);
			base.NavigationPropertyConfiguration.AssociationMappingConfiguration = foreignKeyAssociationMappingConfiguration;
			return this;
		}

		// Token: 0x060019F3 RID: 6643 RVA: 0x000464AB File Offset: 0x000446AB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019F4 RID: 6644 RVA: 0x000464B3 File Offset: 0x000446B3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060019F5 RID: 6645 RVA: 0x000464BC File Offset: 0x000446BC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060019F6 RID: 6646 RVA: 0x000464C4 File Offset: 0x000446C4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

using System;
using System.ComponentModel;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F0 RID: 496
	public class ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x06001A06 RID: 6662 RVA: 0x00046999 File Offset: 0x00044B99
		internal ManyToManyNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
		}

		// Token: 0x06001A07 RID: 6663 RVA: 0x000469A8 File Offset: 0x00044BA8
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> Map(Action<ManyToManyAssociationMappingConfiguration> configurationAction)
		{
			Check.NotNull<Action<ManyToManyAssociationMappingConfiguration>>(configurationAction, "configurationAction");
			ManyToManyAssociationMappingConfiguration manyToManyAssociationMappingConfiguration = new ManyToManyAssociationMappingConfiguration();
			configurationAction(manyToManyAssociationMappingConfiguration);
			this._navigationPropertyConfiguration.AssociationMappingConfiguration = manyToManyAssociationMappingConfiguration;
			return this;
		}

		// Token: 0x06001A08 RID: 6664 RVA: 0x000469DB File Offset: 0x00044BDB
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> MapToStoredProcedures()
		{
			if (this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = new ModificationStoredProceduresConfiguration();
			}
			return this;
		}

		// Token: 0x06001A09 RID: 6665 RVA: 0x000469FC File Offset: 0x00044BFC
		public ManyToManyNavigationPropertyConfiguration<TEntityType, TTargetEntityType> MapToStoredProcedures(Action<ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureMappingConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureMappingConfigurationAction, "modificationStoredProcedureMappingConfigurationAction");
			ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProceduresConfiguration = new ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureMappingConfigurationAction(manyToManyModificationStoredProceduresConfiguration);
			if (this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration == null)
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration = manyToManyModificationStoredProceduresConfiguration.Configuration;
			}
			else
			{
				this._navigationPropertyConfiguration.ModificationStoredProceduresConfiguration.Merge(manyToManyModificationStoredProceduresConfiguration.Configuration, true);
			}
			return this;
		}

		// Token: 0x06001A0A RID: 6666 RVA: 0x00046A5A File Offset: 0x00044C5A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001A0B RID: 6667 RVA: 0x00046A62 File Offset: 0x00044C62
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001A0C RID: 6668 RVA: 0x00046A6B File Offset: 0x00044C6B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001A0D RID: 6669 RVA: 0x00046A73 File Offset: 0x00044C73
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A8E RID: 2702
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}

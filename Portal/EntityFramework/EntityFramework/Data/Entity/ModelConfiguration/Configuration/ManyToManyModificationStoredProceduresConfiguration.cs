using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001D8 RID: 472
	public class ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> where TEntityType : class where TTargetEntityType : class
	{
		// Token: 0x060018BB RID: 6331 RVA: 0x00042A39 File Offset: 0x00040C39
		internal ManyToManyModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x170005E1 RID: 1505
		// (get) Token: 0x060018BC RID: 6332 RVA: 0x00042A4C File Offset: 0x00040C4C
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00042A54 File Offset: 0x00040C54
		public ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> Insert(Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProcedureConfiguration = new ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureConfigurationAction(manyToManyModificationStoredProcedureConfiguration);
			this._configuration.Insert(manyToManyModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00042A8C File Offset: 0x00040C8C
		public ManyToManyModificationStoredProceduresConfiguration<TEntityType, TTargetEntityType> Delete(Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType> manyToManyModificationStoredProcedureConfiguration = new ManyToManyModificationStoredProcedureConfiguration<TEntityType, TTargetEntityType>();
			modificationStoredProcedureConfigurationAction(manyToManyModificationStoredProcedureConfiguration);
			this._configuration.Delete(manyToManyModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00042AC4 File Offset: 0x00040CC4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00042ACC File Offset: 0x00040CCC
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x00042AD5 File Offset: 0x00040CD5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x00042ADD File Offset: 0x00040CDD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A68 RID: 2664
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}

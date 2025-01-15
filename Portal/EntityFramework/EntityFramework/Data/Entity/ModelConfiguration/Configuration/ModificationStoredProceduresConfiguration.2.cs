using System;
using System.ComponentModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DC RID: 476
	public class ModificationStoredProceduresConfiguration<TEntityType> where TEntityType : class
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x000435B8 File Offset: 0x000417B8
		internal ModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x060018EB RID: 6379 RVA: 0x000435CB File Offset: 0x000417CB
		internal ModificationStoredProceduresConfiguration Configuration
		{
			get
			{
				return this._configuration;
			}
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000435D4 File Offset: 0x000417D4
		public ModificationStoredProceduresConfiguration<TEntityType> Insert(Action<InsertModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<InsertModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			InsertModificationStoredProcedureConfiguration<TEntityType> insertModificationStoredProcedureConfiguration = new InsertModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(insertModificationStoredProcedureConfiguration);
			this._configuration.Insert(insertModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x0004360C File Offset: 0x0004180C
		public ModificationStoredProceduresConfiguration<TEntityType> Update(Action<UpdateModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<UpdateModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			UpdateModificationStoredProcedureConfiguration<TEntityType> updateModificationStoredProcedureConfiguration = new UpdateModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(updateModificationStoredProcedureConfiguration);
			this._configuration.Update(updateModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00043644 File Offset: 0x00041844
		public ModificationStoredProceduresConfiguration<TEntityType> Delete(Action<DeleteModificationStoredProcedureConfiguration<TEntityType>> modificationStoredProcedureConfigurationAction)
		{
			Check.NotNull<Action<DeleteModificationStoredProcedureConfiguration<TEntityType>>>(modificationStoredProcedureConfigurationAction, "modificationStoredProcedureConfigurationAction");
			DeleteModificationStoredProcedureConfiguration<TEntityType> deleteModificationStoredProcedureConfiguration = new DeleteModificationStoredProcedureConfiguration<TEntityType>();
			modificationStoredProcedureConfigurationAction(deleteModificationStoredProcedureConfiguration);
			this._configuration.Delete(deleteModificationStoredProcedureConfiguration.Configuration);
			return this;
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x0004367C File Offset: 0x0004187C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00043684 File Offset: 0x00041884
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0004368D File Offset: 0x0004188D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x00043695 File Offset: 0x00041895
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A73 RID: 2675
		private readonly ModificationStoredProceduresConfiguration _configuration = new ModificationStoredProceduresConfiguration();
	}
}

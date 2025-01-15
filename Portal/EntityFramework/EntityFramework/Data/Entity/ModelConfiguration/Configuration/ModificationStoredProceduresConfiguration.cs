using System;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001DB RID: 475
	internal class ModificationStoredProceduresConfiguration
	{
		// Token: 0x060018DD RID: 6365 RVA: 0x0004338B File Offset: 0x0004158B
		public ModificationStoredProceduresConfiguration()
		{
		}

		// Token: 0x060018DE RID: 6366 RVA: 0x00043394 File Offset: 0x00041594
		private ModificationStoredProceduresConfiguration(ModificationStoredProceduresConfiguration source)
		{
			if (source._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration = source._insertModificationStoredProcedureConfiguration.Clone();
			}
			if (source._updateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration = source._updateModificationStoredProcedureConfiguration.Clone();
			}
			if (source._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration = source._deleteModificationStoredProcedureConfiguration.Clone();
			}
		}

		// Token: 0x060018DF RID: 6367 RVA: 0x000433F2 File Offset: 0x000415F2
		public virtual ModificationStoredProceduresConfiguration Clone()
		{
			return new ModificationStoredProceduresConfiguration(this);
		}

		// Token: 0x060018E0 RID: 6368 RVA: 0x000433FA File Offset: 0x000415FA
		public virtual void Insert(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._insertModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x060018E1 RID: 6369 RVA: 0x00043403 File Offset: 0x00041603
		public virtual void Update(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._updateModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x060018E2 RID: 6370 RVA: 0x0004340C File Offset: 0x0004160C
		public virtual void Delete(ModificationStoredProcedureConfiguration modificationStoredProcedureConfiguration)
		{
			this._deleteModificationStoredProcedureConfiguration = modificationStoredProcedureConfiguration;
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00043415 File Offset: 0x00041615
		public ModificationStoredProcedureConfiguration InsertModificationStoredProcedureConfiguration
		{
			get
			{
				return this._insertModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x0004341D File Offset: 0x0004161D
		public ModificationStoredProcedureConfiguration UpdateModificationStoredProcedureConfiguration
		{
			get
			{
				return this._updateModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060018E5 RID: 6373 RVA: 0x00043425 File Offset: 0x00041625
		public ModificationStoredProcedureConfiguration DeleteModificationStoredProcedureConfiguration
		{
			get
			{
				return this._deleteModificationStoredProcedureConfiguration;
			}
		}

		// Token: 0x060018E6 RID: 6374 RVA: 0x00043430 File Offset: 0x00041630
		public virtual void Configure(EntityTypeModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (this._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.InsertFunctionMapping, providerManifest);
			}
			if (this._updateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.UpdateFunctionMapping, providerManifest);
			}
			if (this._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.DeleteFunctionMapping, providerManifest);
			}
		}

		// Token: 0x060018E7 RID: 6375 RVA: 0x0004348B File Offset: 0x0004168B
		public void Configure(AssociationSetModificationFunctionMapping modificationStoredProcedureMapping, DbProviderManifest providerManifest)
		{
			if (this._insertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.InsertFunctionMapping, providerManifest);
			}
			if (this._deleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Configure(modificationStoredProcedureMapping.DeleteFunctionMapping, providerManifest);
			}
		}

		// Token: 0x060018E8 RID: 6376 RVA: 0x000434C4 File Offset: 0x000416C4
		public bool IsCompatibleWith(ModificationStoredProceduresConfiguration other)
		{
			return (this._insertModificationStoredProcedureConfiguration == null || other._insertModificationStoredProcedureConfiguration == null || this._insertModificationStoredProcedureConfiguration.IsCompatibleWith(other._insertModificationStoredProcedureConfiguration)) && (this._deleteModificationStoredProcedureConfiguration == null || other._deleteModificationStoredProcedureConfiguration == null || this._deleteModificationStoredProcedureConfiguration.IsCompatibleWith(other._deleteModificationStoredProcedureConfiguration));
		}

		// Token: 0x060018E9 RID: 6377 RVA: 0x0004351C File Offset: 0x0004171C
		public void Merge(ModificationStoredProceduresConfiguration modificationStoredProceduresConfiguration, bool allowOverride)
		{
			if (this._insertModificationStoredProcedureConfiguration == null)
			{
				this._insertModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration;
			}
			else if (modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration != null)
			{
				this._insertModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.InsertModificationStoredProcedureConfiguration, allowOverride);
			}
			if (this._updateModificationStoredProcedureConfiguration == null)
			{
				this._updateModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration;
			}
			else if (modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration != null)
			{
				this._updateModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.UpdateModificationStoredProcedureConfiguration, allowOverride);
			}
			if (this._deleteModificationStoredProcedureConfiguration == null)
			{
				this._deleteModificationStoredProcedureConfiguration = modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration;
				return;
			}
			if (modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration != null)
			{
				this._deleteModificationStoredProcedureConfiguration.Merge(modificationStoredProceduresConfiguration.DeleteModificationStoredProcedureConfiguration, allowOverride);
			}
		}

		// Token: 0x04000A70 RID: 2672
		private ModificationStoredProcedureConfiguration _insertModificationStoredProcedureConfiguration;

		// Token: 0x04000A71 RID: 2673
		private ModificationStoredProcedureConfiguration _updateModificationStoredProcedureConfiguration;

		// Token: 0x04000A72 RID: 2674
		private ModificationStoredProcedureConfiguration _deleteModificationStoredProcedureConfiguration;
	}
}

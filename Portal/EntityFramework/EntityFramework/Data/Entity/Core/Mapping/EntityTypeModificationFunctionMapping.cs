using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200052E RID: 1326
	public sealed class EntityTypeModificationFunctionMapping : MappingItem
	{
		// Token: 0x06004174 RID: 16756 RVA: 0x000DD3A4 File Offset: 0x000DB5A4
		public EntityTypeModificationFunctionMapping(EntityType entityType, ModificationFunctionMapping deleteFunctionMapping, ModificationFunctionMapping insertFunctionMapping, ModificationFunctionMapping updateFunctionMapping)
		{
			Check.NotNull<EntityType>(entityType, "entityType");
			this._entityType = entityType;
			this._deleteFunctionMapping = deleteFunctionMapping;
			this._insertFunctionMapping = insertFunctionMapping;
			this._updateFunctionMapping = updateFunctionMapping;
		}

		// Token: 0x17000CE9 RID: 3305
		// (get) Token: 0x06004175 RID: 16757 RVA: 0x000DD3D5 File Offset: 0x000DB5D5
		public EntityType EntityType
		{
			get
			{
				return this._entityType;
			}
		}

		// Token: 0x17000CEA RID: 3306
		// (get) Token: 0x06004176 RID: 16758 RVA: 0x000DD3DD File Offset: 0x000DB5DD
		public ModificationFunctionMapping DeleteFunctionMapping
		{
			get
			{
				return this._deleteFunctionMapping;
			}
		}

		// Token: 0x17000CEB RID: 3307
		// (get) Token: 0x06004177 RID: 16759 RVA: 0x000DD3E5 File Offset: 0x000DB5E5
		public ModificationFunctionMapping InsertFunctionMapping
		{
			get
			{
				return this._insertFunctionMapping;
			}
		}

		// Token: 0x17000CEC RID: 3308
		// (get) Token: 0x06004178 RID: 16760 RVA: 0x000DD3ED File Offset: 0x000DB5ED
		public ModificationFunctionMapping UpdateFunctionMapping
		{
			get
			{
				return this._updateFunctionMapping;
			}
		}

		// Token: 0x06004179 RID: 16761 RVA: 0x000DD3F8 File Offset: 0x000DB5F8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "ET{{{0}}}:{4}DFunc={{{1}}},{4}IFunc={{{2}}},{4}UFunc={{{3}}}", new object[]
			{
				this.EntityType,
				this.DeleteFunctionMapping,
				this.InsertFunctionMapping,
				this.UpdateFunctionMapping,
				Environment.NewLine + "  "
			});
		}

		// Token: 0x0600417A RID: 16762 RVA: 0x000DD450 File Offset: 0x000DB650
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._deleteFunctionMapping);
			MappingItem.SetReadOnly(this._insertFunctionMapping);
			MappingItem.SetReadOnly(this._updateFunctionMapping);
			base.SetReadOnly();
		}

		// Token: 0x17000CED RID: 3309
		// (get) Token: 0x0600417B RID: 16763 RVA: 0x000DD47C File Offset: 0x000DB67C
		internal IEnumerable<ModificationFunctionParameterBinding> PrimaryParameterBindings
		{
			get
			{
				IEnumerable<ModificationFunctionParameterBinding> enumerable = Enumerable.Empty<ModificationFunctionParameterBinding>();
				if (this.DeleteFunctionMapping != null)
				{
					enumerable = enumerable.Concat(this.DeleteFunctionMapping.ParameterBindings);
				}
				if (this.InsertFunctionMapping != null)
				{
					enumerable = enumerable.Concat(this.InsertFunctionMapping.ParameterBindings);
				}
				if (this.UpdateFunctionMapping != null)
				{
					enumerable = enumerable.Concat(this.UpdateFunctionMapping.ParameterBindings.Where((ModificationFunctionParameterBinding pb) => pb.IsCurrent));
				}
				return enumerable;
			}
		}

		// Token: 0x040016B1 RID: 5809
		private readonly EntityType _entityType;

		// Token: 0x040016B2 RID: 5810
		private readonly ModificationFunctionMapping _deleteFunctionMapping;

		// Token: 0x040016B3 RID: 5811
		private readonly ModificationFunctionMapping _insertFunctionMapping;

		// Token: 0x040016B4 RID: 5812
		private readonly ModificationFunctionMapping _updateFunctionMapping;
	}
}

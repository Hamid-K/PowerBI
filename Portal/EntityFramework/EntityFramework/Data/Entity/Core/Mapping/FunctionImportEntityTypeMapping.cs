using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000532 RID: 1330
	public sealed class FunctionImportEntityTypeMapping : FunctionImportStructuralTypeMapping
	{
		// Token: 0x0600418E RID: 16782 RVA: 0x000DD5CE File Offset: 0x000DB7CE
		public FunctionImportEntityTypeMapping(IEnumerable<EntityType> isOfTypeEntityTypes, IEnumerable<EntityType> entityTypes, Collection<FunctionImportReturnTypePropertyMapping> properties, IEnumerable<FunctionImportEntityTypeMappingCondition> conditions)
			: this(Check.NotNull<IEnumerable<EntityType>>(isOfTypeEntityTypes, "isOfTypeEntityTypes"), Check.NotNull<IEnumerable<EntityType>>(entityTypes, "entityTypes"), Check.NotNull<IEnumerable<FunctionImportEntityTypeMappingCondition>>(conditions, "conditions"), Check.NotNull<Collection<FunctionImportReturnTypePropertyMapping>>(properties, "properties"), LineInfo.Empty)
		{
		}

		// Token: 0x0600418F RID: 16783 RVA: 0x000DD608 File Offset: 0x000DB808
		internal FunctionImportEntityTypeMapping(IEnumerable<EntityType> isOfTypeEntityTypes, IEnumerable<EntityType> entityTypes, IEnumerable<FunctionImportEntityTypeMappingCondition> conditions, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameList, LineInfo lineInfo)
			: base(columnsRenameList, lineInfo)
		{
			this._isOfTypeEntityTypes = new ReadOnlyCollection<EntityType>(isOfTypeEntityTypes.ToList<EntityType>());
			this._entityTypes = new ReadOnlyCollection<EntityType>(entityTypes.ToList<EntityType>());
			this._conditions = new ReadOnlyCollection<FunctionImportEntityTypeMappingCondition>(conditions.ToList<FunctionImportEntityTypeMappingCondition>());
		}

		// Token: 0x17000CF6 RID: 3318
		// (get) Token: 0x06004190 RID: 16784 RVA: 0x000DD647 File Offset: 0x000DB847
		public ReadOnlyCollection<EntityType> EntityTypes
		{
			get
			{
				return this._entityTypes;
			}
		}

		// Token: 0x17000CF7 RID: 3319
		// (get) Token: 0x06004191 RID: 16785 RVA: 0x000DD64F File Offset: 0x000DB84F
		public ReadOnlyCollection<EntityType> IsOfTypeEntityTypes
		{
			get
			{
				return this._isOfTypeEntityTypes;
			}
		}

		// Token: 0x17000CF8 RID: 3320
		// (get) Token: 0x06004192 RID: 16786 RVA: 0x000DD657 File Offset: 0x000DB857
		public ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> Conditions
		{
			get
			{
				return this._conditions;
			}
		}

		// Token: 0x06004193 RID: 16787 RVA: 0x000DD65F File Offset: 0x000DB85F
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._conditions);
			base.SetReadOnly();
		}

		// Token: 0x06004194 RID: 16788 RVA: 0x000DD674 File Offset: 0x000DB874
		internal IEnumerable<EntityType> GetMappedEntityTypes(ItemCollection itemCollection)
		{
			return this.EntityTypes.Concat(this.IsOfTypeEntityTypes.SelectMany((EntityType entityType) => MetadataHelper.GetTypeAndSubtypesOf(entityType, itemCollection, false).Cast<EntityType>()));
		}

		// Token: 0x06004195 RID: 16789 RVA: 0x000DD6B0 File Offset: 0x000DB8B0
		internal IEnumerable<string> GetDiscriminatorColumns()
		{
			return this.Conditions.Select((FunctionImportEntityTypeMappingCondition condition) => condition.ColumnName);
		}

		// Token: 0x040016BC RID: 5820
		private readonly ReadOnlyCollection<EntityType> _entityTypes;

		// Token: 0x040016BD RID: 5821
		private readonly ReadOnlyCollection<EntityType> _isOfTypeEntityTypes;

		// Token: 0x040016BE RID: 5822
		private readonly ReadOnlyCollection<FunctionImportEntityTypeMappingCondition> _conditions;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053C RID: 1340
	internal sealed class FunctionImportReturnTypeEntityTypeColumnsRenameBuilder
	{
		// Token: 0x060041DA RID: 16858 RVA: 0x000DF0CC File Offset: 0x000DD2CC
		internal FunctionImportReturnTypeEntityTypeColumnsRenameBuilder(Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> isOfTypeEntityTypeColumnsRenameMapping, Dictionary<EntityType, Collection<FunctionImportReturnTypePropertyMapping>> entityTypeColumnsRenameMapping)
		{
			this.ColumnRenameMapping = new Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping>();
			foreach (EntityType entityType in isOfTypeEntityTypeColumnsRenameMapping.Keys)
			{
				this.SetStructuralTypeColumnsRename(entityType, isOfTypeEntityTypeColumnsRenameMapping[entityType], true);
			}
			foreach (EntityType entityType2 in entityTypeColumnsRenameMapping.Keys)
			{
				this.SetStructuralTypeColumnsRename(entityType2, entityTypeColumnsRenameMapping[entityType2], false);
			}
		}

		// Token: 0x060041DB RID: 16859 RVA: 0x000DF184 File Offset: 0x000DD384
		private void SetStructuralTypeColumnsRename(EntityType entityType, Collection<FunctionImportReturnTypePropertyMapping> columnsRenameMapping, bool isTypeOf)
		{
			foreach (FunctionImportReturnTypePropertyMapping functionImportReturnTypePropertyMapping in columnsRenameMapping)
			{
				if (!this.ColumnRenameMapping.Keys.Contains(functionImportReturnTypePropertyMapping.CMember))
				{
					this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember] = new FunctionImportReturnTypeStructuralTypeColumnRenameMapping(functionImportReturnTypePropertyMapping.CMember);
				}
				this.ColumnRenameMapping[functionImportReturnTypePropertyMapping.CMember].AddRename(new FunctionImportReturnTypeStructuralTypeColumn(functionImportReturnTypePropertyMapping.SColumn, entityType, isTypeOf, functionImportReturnTypePropertyMapping.LineInfo));
			}
		}

		// Token: 0x040016D7 RID: 5847
		internal Dictionary<string, FunctionImportReturnTypeStructuralTypeColumnRenameMapping> ColumnRenameMapping;
	}
}

using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x02000211 RID: 529
	internal static class TableOperations
	{
		// Token: 0x06001C18 RID: 7192 RVA: 0x0004F680 File Offset: 0x0004D880
		public static EdmProperty CopyColumnAndAnyConstraints(EdmModel database, EntityType fromTable, EntityType toTable, EdmProperty column, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty edmProperty = column;
			if (fromTable != toTable)
			{
				edmProperty = TablePrimitiveOperations.IncludeColumn(toTable, column, isCompatible, useExisting);
				if (!edmProperty.IsPrimaryKeyColumn)
				{
					ForeignKeyPrimitiveOperations.CopyAllForeignKeyConstraintsForColumn(database, fromTable, toTable, column, edmProperty);
				}
			}
			return edmProperty;
		}

		// Token: 0x06001C19 RID: 7193 RVA: 0x0004F6B4 File Offset: 0x0004D8B4
		public static EdmProperty MoveColumnAndAnyConstraints(EntityType fromTable, EntityType toTable, EdmProperty column, bool useExisting)
		{
			EdmProperty edmProperty = column;
			if (fromTable != toTable)
			{
				edmProperty = TablePrimitiveOperations.IncludeColumn(toTable, column, TablePrimitiveOperations.GetNameMatcher(column.Name), useExisting);
				TablePrimitiveOperations.RemoveColumn(fromTable, column);
				ForeignKeyPrimitiveOperations.MoveAllForeignKeyConstraintsForColumn(fromTable, toTable, column);
			}
			return edmProperty;
		}
	}
}

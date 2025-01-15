using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration.Mapping
{
	// Token: 0x0200020F RID: 527
	internal static class TablePrimitiveOperations
	{
		// Token: 0x06001C07 RID: 7175 RVA: 0x0004ED50 File Offset: 0x0004CF50
		public static void AddColumn(EntityType table, EdmProperty column)
		{
			if (!table.Properties.Contains(column))
			{
				PrimitivePropertyConfiguration primitivePropertyConfiguration = column.GetConfiguration() as PrimitivePropertyConfiguration;
				if (primitivePropertyConfiguration == null || string.IsNullOrWhiteSpace(primitivePropertyConfiguration.ColumnName))
				{
					string text = column.GetPreferredName() ?? column.Name;
					column.SetUnpreferredUniqueName(column.Name);
					column.Name = table.Properties.UniquifyName(text);
				}
				table.AddMember(column);
			}
		}

		// Token: 0x06001C08 RID: 7176 RVA: 0x0004EDBD File Offset: 0x0004CFBD
		public static EdmProperty RemoveColumn(EntityType table, EdmProperty column)
		{
			if (!column.IsPrimaryKeyColumn)
			{
				table.RemoveMember(column);
			}
			return column;
		}

		// Token: 0x06001C09 RID: 7177 RVA: 0x0004EDD0 File Offset: 0x0004CFD0
		public static EdmProperty IncludeColumn(EntityType table, EdmProperty templateColumn, Func<EdmProperty, bool> isCompatible, bool useExisting)
		{
			EdmProperty edmProperty = table.Properties.FirstOrDefault(isCompatible);
			if (edmProperty == null)
			{
				templateColumn = templateColumn.Clone();
			}
			else if (!useExisting && !edmProperty.IsPrimaryKeyColumn)
			{
				templateColumn = templateColumn.Clone();
			}
			else
			{
				templateColumn = edmProperty;
			}
			TablePrimitiveOperations.AddColumn(table, templateColumn);
			return templateColumn;
		}

		// Token: 0x06001C0A RID: 7178 RVA: 0x0004EE17 File Offset: 0x0004D017
		public static Func<EdmProperty, bool> GetNameMatcher(string name)
		{
			return (EdmProperty c) => string.Equals(c.Name, name, StringComparison.Ordinal);
		}
	}
}

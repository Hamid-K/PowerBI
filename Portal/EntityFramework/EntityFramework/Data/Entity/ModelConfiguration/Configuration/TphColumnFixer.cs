using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Primitive;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001F9 RID: 505
	internal class TphColumnFixer
	{
		// Token: 0x06001A81 RID: 6785 RVA: 0x00047884 File Offset: 0x00045A84
		public TphColumnFixer(IEnumerable<ColumnMappingBuilder> columnMappings, EntityType table, EdmModel storeModel)
		{
			this._columnMappings = columnMappings.OrderBy((ColumnMappingBuilder m) => m.ColumnProperty.Name).ToList<ColumnMappingBuilder>();
			this._table = table;
			this._storeModel = storeModel;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000478D8 File Offset: 0x00045AD8
		public void RemoveDuplicateTphColumns()
		{
			int num;
			for (int i = 0; i < this._columnMappings.Count - 1; i = num)
			{
				StructuralType declaringType = this._columnMappings[i].PropertyPath[0].DeclaringType;
				EdmProperty column = this._columnMappings[i].ColumnProperty;
				num = i + 1;
				EdmType edmType;
				while (num < this._columnMappings.Count && column.Name == this._columnMappings[num].ColumnProperty.Name && declaringType != this._columnMappings[num].PropertyPath[0].DeclaringType && TypeSemantics.TryGetCommonBaseType(declaringType, this._columnMappings[num].PropertyPath[0].DeclaringType, out edmType))
				{
					num++;
				}
				PrimitivePropertyConfiguration primitivePropertyConfiguration = column.GetConfiguration() as PrimitivePropertyConfiguration;
				for (int j = i + 1; j < num; j++)
				{
					ColumnMappingBuilder toFixup = this._columnMappings[j];
					PrimitivePropertyConfiguration primitivePropertyConfiguration2 = toFixup.ColumnProperty.GetConfiguration() as PrimitivePropertyConfiguration;
					string text;
					if (primitivePropertyConfiguration != null && !primitivePropertyConfiguration.IsCompatible(primitivePropertyConfiguration2, false, out text))
					{
						throw new MappingException(Strings.BadTphMappingToSharedColumn(string.Join(".", this._columnMappings[i].PropertyPath.Select((EdmProperty p) => p.Name)), declaringType.Name, string.Join(".", toFixup.PropertyPath.Select((EdmProperty p) => p.Name)), toFixup.PropertyPath[0].DeclaringType.Name, column.Name, column.DeclaringType.Name, text));
					}
					if (primitivePropertyConfiguration2 != null)
					{
						primitivePropertyConfiguration2.Configure(column, this._table, this._storeModel.ProviderManifest, false, false);
					}
					column.Nullable = true;
					foreach (AssociationType associationType in (from a in this._storeModel.AssociationTypes
						where a.Constraint != null
						let p = a.Constraint.ToProperties
						where p.Contains(column) || p.Contains(toFixup.ColumnProperty)
						select a).ToArray<AssociationType>())
					{
						this._storeModel.RemoveAssociationType(associationType);
					}
					if (toFixup.ColumnProperty.DeclaringType.HasMember(toFixup.ColumnProperty))
					{
						toFixup.ColumnProperty.DeclaringType.RemoveMember(toFixup.ColumnProperty);
					}
					toFixup.ColumnProperty = column;
				}
			}
		}

		// Token: 0x04000A98 RID: 2712
		private readonly IList<ColumnMappingBuilder> _columnMappings;

		// Token: 0x04000A99 RID: 2713
		private readonly EntityType _table;

		// Token: 0x04000A9A RID: 2714
		private readonly EdmModel _storeModel;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Core.Mapping;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.ModelConfiguration.Edm.Services
{
	// Token: 0x02000178 RID: 376
	internal class PropertyMappingGenerator : StructuralTypeMappingGenerator
	{
		// Token: 0x060016D9 RID: 5849 RVA: 0x0003CA18 File Offset: 0x0003AC18
		public PropertyMappingGenerator(DbProviderManifest providerManifest)
			: base(providerManifest)
		{
		}

		// Token: 0x060016DA RID: 5850 RVA: 0x0003CA24 File Offset: 0x0003AC24
		public void Generate(EntityType entityType, IEnumerable<EdmProperty> properties, EntitySetMapping entitySetMapping, MappingFragment entityTypeMappingFragment, IList<EdmProperty> propertyPath, bool createNewColumn)
		{
			ReadOnlyMetadataCollection<EdmProperty> declaredProperties = entityType.GetRootType().DeclaredProperties;
			using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
			{
				Func<ColumnMappingBuilder, bool> <>9__3;
				while (enumerator.MoveNext())
				{
					EdmProperty property = enumerator.Current;
					if (property.IsComplexType && propertyPath.Any((EdmProperty p) => p.IsComplexType && p.ComplexType == property.ComplexType))
					{
						throw Error.CircularComplexTypeHierarchy();
					}
					propertyPath.Add(property);
					if (property.IsComplexType)
					{
						this.Generate(entityType, property.ComplexType.Properties, entitySetMapping, entityTypeMappingFragment, propertyPath, createNewColumn);
					}
					else
					{
						IEnumerable<ColumnMappingBuilder> enumerable = entitySetMapping.EntityTypeMappings.SelectMany((EntityTypeMapping etm) => etm.MappingFragments).SelectMany((MappingFragment etmf) => etmf.ColumnMappings);
						Func<ColumnMappingBuilder, bool> func;
						if ((func = <>9__3) == null)
						{
							func = (<>9__3 = (ColumnMappingBuilder pm) => pm.PropertyPath.SequenceEqual(propertyPath));
						}
						EdmProperty edmProperty = (from pm in enumerable.Where(func)
							select pm.ColumnProperty).FirstOrDefault<EdmProperty>();
						if (edmProperty == null || createNewColumn)
						{
							string text = string.Join("_", propertyPath.Select((EdmProperty p) => p.Name));
							edmProperty = base.MapTableColumn(property, text, !declaredProperties.Contains(propertyPath.First<EdmProperty>()));
							entityTypeMappingFragment.Table.AddColumn(edmProperty);
							if (entityType.KeyProperties().Contains(property))
							{
								entityTypeMappingFragment.Table.AddKeyMember(edmProperty);
							}
						}
						entityTypeMappingFragment.AddColumnMapping(new ColumnMappingBuilder(edmProperty, propertyPath.ToList<EdmProperty>()));
					}
					propertyPath.Remove(property);
				}
			}
		}
	}
}

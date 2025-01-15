using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Mapping.ViewGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x020003A4 RID: 932
	internal class ExplicitDiscriminatorMap
	{
		// Token: 0x06002D41 RID: 11585 RVA: 0x00091A8C File Offset: 0x0008FC8C
		internal ExplicitDiscriminatorMap(DiscriminatorMap template)
		{
			this.m_typeMap = template.TypeMap;
			this.m_discriminatorProperty = template.Discriminator.Property;
			this.m_properties = new ReadOnlyCollection<EdmProperty>(template.PropertyMap.Select((KeyValuePair<EdmProperty, DbExpression> propertyValuePair) => propertyValuePair.Key).ToList<EdmProperty>());
		}

		// Token: 0x170008DA RID: 2266
		// (get) Token: 0x06002D42 RID: 11586 RVA: 0x00091AF6 File Offset: 0x0008FCF6
		internal ReadOnlyCollection<KeyValuePair<object, EntityType>> TypeMap
		{
			get
			{
				return this.m_typeMap;
			}
		}

		// Token: 0x170008DB RID: 2267
		// (get) Token: 0x06002D43 RID: 11587 RVA: 0x00091AFE File Offset: 0x0008FCFE
		internal EdmMember DiscriminatorProperty
		{
			get
			{
				return this.m_discriminatorProperty;
			}
		}

		// Token: 0x170008DC RID: 2268
		// (get) Token: 0x06002D44 RID: 11588 RVA: 0x00091B06 File Offset: 0x0008FD06
		internal ReadOnlyCollection<EdmProperty> Properties
		{
			get
			{
				return this.m_properties;
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00091B10 File Offset: 0x0008FD10
		internal object GetTypeId(EntityType entityType)
		{
			object obj = null;
			foreach (KeyValuePair<object, EntityType> keyValuePair in this.TypeMap)
			{
				if (keyValuePair.Value.EdmEquals(entityType))
				{
					obj = keyValuePair.Key;
					break;
				}
			}
			return obj;
		}

		// Token: 0x04000F23 RID: 3875
		private readonly ReadOnlyCollection<KeyValuePair<object, EntityType>> m_typeMap;

		// Token: 0x04000F24 RID: 3876
		private readonly EdmMember m_discriminatorProperty;

		// Token: 0x04000F25 RID: 3877
		private readonly ReadOnlyCollection<EdmProperty> m_properties;
	}
}

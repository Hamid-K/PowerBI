using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Edm;
using System.Data.Entity.ModelConfiguration.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Mappers
{
	// Token: 0x02000158 RID: 344
	internal sealed class AttributeMapper
	{
		// Token: 0x060015F2 RID: 5618 RVA: 0x00038D4B File Offset: 0x00036F4B
		public AttributeMapper(AttributeProvider attributeProvider)
		{
			this._attributeProvider = attributeProvider;
		}

		// Token: 0x060015F3 RID: 5619 RVA: 0x00038D5A File Offset: 0x00036F5A
		public void Map(PropertyInfo propertyInfo, ICollection<MetadataProperty> annotations)
		{
			annotations.SetClrAttributes(this._attributeProvider.GetAttributes(propertyInfo).ToList<Attribute>());
		}

		// Token: 0x060015F4 RID: 5620 RVA: 0x00038D73 File Offset: 0x00036F73
		public void Map(Type type, ICollection<MetadataProperty> annotations)
		{
			annotations.SetClrAttributes(this._attributeProvider.GetAttributes(type).ToList<Attribute>());
		}

		// Token: 0x040009F5 RID: 2549
		private readonly AttributeProvider _attributeProvider;
	}
}

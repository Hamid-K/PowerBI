using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000522 RID: 1314
	public class ComplexPropertyMapping : PropertyMapping
	{
		// Token: 0x060040BF RID: 16575 RVA: 0x000DAD1E File Offset: 0x000D8F1E
		public ComplexPropertyMapping(EdmProperty property)
			: base(property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			if (!TypeSemantics.IsComplexType(property.TypeUsage))
			{
				throw new ArgumentException(Strings.StorageComplexPropertyMapping_OnlyComplexPropertyAllowed, "property");
			}
			this._typeMappings = new List<ComplexTypeMapping>();
		}

		// Token: 0x17000CA9 RID: 3241
		// (get) Token: 0x060040C0 RID: 16576 RVA: 0x000DAD5B File Offset: 0x000D8F5B
		public ReadOnlyCollection<ComplexTypeMapping> TypeMappings
		{
			get
			{
				return new ReadOnlyCollection<ComplexTypeMapping>(this._typeMappings);
			}
		}

		// Token: 0x060040C1 RID: 16577 RVA: 0x000DAD68 File Offset: 0x000D8F68
		public void AddTypeMapping(ComplexTypeMapping typeMapping)
		{
			Check.NotNull<ComplexTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Add(typeMapping);
		}

		// Token: 0x060040C2 RID: 16578 RVA: 0x000DAD88 File Offset: 0x000D8F88
		public void RemoveTypeMapping(ComplexTypeMapping typeMapping)
		{
			Check.NotNull<ComplexTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Remove(typeMapping);
		}

		// Token: 0x060040C3 RID: 16579 RVA: 0x000DADA9 File Offset: 0x000D8FA9
		internal override void SetReadOnly()
		{
			this._typeMappings.TrimExcess();
			MappingItem.SetReadOnly(this._typeMappings);
			base.SetReadOnly();
		}

		// Token: 0x0400167B RID: 5755
		private readonly List<ComplexTypeMapping> _typeMappings;
	}
}

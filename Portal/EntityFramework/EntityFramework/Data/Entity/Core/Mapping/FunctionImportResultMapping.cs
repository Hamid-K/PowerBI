using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200053B RID: 1339
	public sealed class FunctionImportResultMapping : MappingItem
	{
		// Token: 0x17000D06 RID: 3334
		// (get) Token: 0x060041D4 RID: 16852 RVA: 0x000DF045 File Offset: 0x000DD245
		public ReadOnlyCollection<FunctionImportStructuralTypeMapping> TypeMappings
		{
			get
			{
				return new ReadOnlyCollection<FunctionImportStructuralTypeMapping>(this._typeMappings);
			}
		}

		// Token: 0x060041D5 RID: 16853 RVA: 0x000DF052 File Offset: 0x000DD252
		public void AddTypeMapping(FunctionImportStructuralTypeMapping typeMapping)
		{
			Check.NotNull<FunctionImportStructuralTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Add(typeMapping);
		}

		// Token: 0x060041D6 RID: 16854 RVA: 0x000DF072 File Offset: 0x000DD272
		public void RemoveTypeMapping(FunctionImportStructuralTypeMapping typeMapping)
		{
			Check.NotNull<FunctionImportStructuralTypeMapping>(typeMapping, "typeMapping");
			base.ThrowIfReadOnly();
			this._typeMappings.Remove(typeMapping);
		}

		// Token: 0x060041D7 RID: 16855 RVA: 0x000DF093 File Offset: 0x000DD293
		internal override void SetReadOnly()
		{
			this._typeMappings.TrimExcess();
			MappingItem.SetReadOnly(this._typeMappings);
			base.SetReadOnly();
		}

		// Token: 0x17000D07 RID: 3335
		// (get) Token: 0x060041D8 RID: 16856 RVA: 0x000DF0B1 File Offset: 0x000DD2B1
		internal List<FunctionImportStructuralTypeMapping> SourceList
		{
			get
			{
				return this._typeMappings;
			}
		}

		// Token: 0x040016D6 RID: 5846
		private readonly List<FunctionImportStructuralTypeMapping> _typeMappings = new List<FunctionImportStructuralTypeMapping>();
	}
}

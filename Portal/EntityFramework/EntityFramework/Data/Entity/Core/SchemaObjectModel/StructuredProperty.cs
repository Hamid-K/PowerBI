using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200031E RID: 798
	internal class StructuredProperty : Property
	{
		// Token: 0x060025EB RID: 9707 RVA: 0x0006C645 File Offset: 0x0006A845
		internal StructuredProperty(StructuredType parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x060025EC RID: 9708 RVA: 0x0006C65A File Offset: 0x0006A85A
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x060025ED RID: 9709 RVA: 0x0006C662 File Offset: 0x0006A862
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x060025EE RID: 9710 RVA: 0x0006C66F File Offset: 0x0006A86F
		public bool Nullable
		{
			get
			{
				return this._typeUsageBuilder.Nullable;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x060025EF RID: 9711 RVA: 0x0006C67C File Offset: 0x0006A87C
		public string Default
		{
			get
			{
				return this._typeUsageBuilder.Default;
			}
		}

		// Token: 0x17000812 RID: 2066
		// (get) Token: 0x060025F0 RID: 9712 RVA: 0x0006C689 File Offset: 0x0006A889
		public object DefaultAsObject
		{
			get
			{
				return this._typeUsageBuilder.DefaultAsObject;
			}
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x060025F1 RID: 9713 RVA: 0x0006C696 File Offset: 0x0006A896
		public CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x060025F2 RID: 9714 RVA: 0x0006C6A0 File Offset: 0x0006A8A0
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (this._type != null)
			{
				return;
			}
			this._type = this.ResolveType(this.UnresolvedType);
			this._typeUsageBuilder.ValidateDefaultValue(this._type);
			ScalarType scalarType = this._type as ScalarType;
			if (scalarType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(scalarType, true);
			}
		}

		// Token: 0x060025F3 RID: 9715 RVA: 0x0006C6FC File Offset: 0x0006A8FC
		internal void EnsureEnumTypeFacets(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this.Type, this.Type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x060025F4 RID: 9716 RVA: 0x0006C73C File Offset: 0x0006A93C
		protected virtual SchemaType ResolveType(string typeName)
		{
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, typeName, out schemaType))
			{
				return null;
			}
			if (!(schemaType is SchemaComplexType) && !(schemaType is ScalarType) && !(schemaType is SchemaEnumType))
			{
				base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidPropertyType(this.UnresolvedType));
				return null;
			}
			return schemaType;
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x060025F5 RID: 9717 RVA: 0x0006C78B File Offset: 0x0006A98B
		// (set) Token: 0x060025F6 RID: 9718 RVA: 0x0006C793 File Offset: 0x0006A993
		internal string UnresolvedType { get; set; }

		// Token: 0x060025F7 RID: 9719 RVA: 0x0006C79C File Offset: 0x0006A99C
		internal override void Validate()
		{
			base.Validate();
			if (this._collectionKind != CollectionKind.Bag)
			{
				CollectionKind collectionKind = this._collectionKind;
			}
			SchemaEnumType schemaEnumType = this._type as SchemaEnumType;
			if (schemaEnumType != null)
			{
				this._typeUsageBuilder.ValidateEnumFacets(schemaEnumType);
				return;
			}
			if (this.Nullable && base.Schema.SchemaVersion != 1.1 && this._type is SchemaComplexType)
			{
				base.AddError(ErrorCode.NullableComplexType, EdmSchemaErrorSeverity.Error, Strings.ComplexObject_NullableComplexTypesNotSupported(this.FQName));
			}
		}

		// Token: 0x060025F8 RID: 9720 RVA: 0x0006C820 File Offset: 0x0006AA20
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "CollectionKind"))
			{
				this.HandleCollectionKindAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x060025F9 RID: 9721 RVA: 0x0006C878 File Offset: 0x0006AA78
		private void HandleTypeAttribute(XmlReader reader)
		{
			if (this.UnresolvedType != null)
			{
				base.AddError(ErrorCode.AlreadyDefined, EdmSchemaErrorSeverity.Error, reader, Strings.PropertyTypeAlreadyDefined(reader.Name));
				return;
			}
			string text;
			if (!Utils.GetDottedName(base.Schema, reader, out text))
			{
				return;
			}
			this.UnresolvedType = text;
		}

		// Token: 0x060025FA RID: 9722 RVA: 0x0006C8BC File Offset: 0x0006AABC
		private void HandleCollectionKindAttribute(XmlReader reader)
		{
			string value = reader.Value;
			if (value == "None")
			{
				this._collectionKind = CollectionKind.None;
				return;
			}
			if (value == "List")
			{
				this._collectionKind = CollectionKind.List;
				return;
			}
			if (value == "Bag")
			{
				this._collectionKind = CollectionKind.Bag;
				return;
			}
		}

		// Token: 0x04000D53 RID: 3411
		private SchemaType _type;

		// Token: 0x04000D54 RID: 3412
		private readonly TypeUsageBuilder _typeUsageBuilder;

		// Token: 0x04000D55 RID: 3413
		private CollectionKind _collectionKind;
	}
}

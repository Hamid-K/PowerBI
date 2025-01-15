using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000053 RID: 83
	internal class StructuredProperty : Property
	{
		// Token: 0x0600085B RID: 2139 RVA: 0x0001196E File Offset: 0x0000FB6E
		internal StructuredProperty(StructuredType parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x0600085C RID: 2140 RVA: 0x00011983 File Offset: 0x0000FB83
		public override SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x0600085D RID: 2141 RVA: 0x0001198B File Offset: 0x0000FB8B
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00011998 File Offset: 0x0000FB98
		public bool Nullable
		{
			get
			{
				return this._typeUsageBuilder.Nullable;
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x0600085F RID: 2143 RVA: 0x000119A5 File Offset: 0x0000FBA5
		public string Default
		{
			get
			{
				return this._typeUsageBuilder.Default;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x000119B2 File Offset: 0x0000FBB2
		public object DefaultAsObject
		{
			get
			{
				return this._typeUsageBuilder.DefaultAsObject;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000861 RID: 2145 RVA: 0x000119BF File Offset: 0x0000FBBF
		public CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x000119C8 File Offset: 0x0000FBC8
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

		// Token: 0x06000863 RID: 2147 RVA: 0x00011A24 File Offset: 0x0000FC24
		protected virtual SchemaType ResolveType(string typeName)
		{
			SchemaType schemaType;
			if (!base.Schema.ResolveTypeName(this, typeName, out schemaType))
			{
				return null;
			}
			if (!(schemaType is SchemaComplexType) && !(schemaType is ScalarType))
			{
				base.AddError(ErrorCode.InvalidPropertyType, EdmSchemaErrorSeverity.Error, Strings.InvalidPropertyType(this.UnresolvedType));
				return null;
			}
			return schemaType;
		}

		// Token: 0x17000329 RID: 809
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00011A6B File Offset: 0x0000FC6B
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00011A73 File Offset: 0x0000FC73
		internal string UnresolvedType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x00011A7C File Offset: 0x0000FC7C
		internal override void Validate()
		{
			base.Validate();
			if (this._collectionKind != CollectionKind.Bag)
			{
				CollectionKind collectionKind = this._collectionKind;
			}
			if (this.Nullable && (base.Schema.SchemaVersion == 1.0 || base.Schema.SchemaVersion == 2.0) && this._type is SchemaComplexType)
			{
				base.AddError(ErrorCode.NullableComplexType, EdmSchemaErrorSeverity.Error, Strings.NullableComplexType(this.FQName));
			}
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00011AFC File Offset: 0x0000FCFC
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

		// Token: 0x06000868 RID: 2152 RVA: 0x00011B54 File Offset: 0x0000FD54
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

		// Token: 0x06000869 RID: 2153 RVA: 0x00011B98 File Offset: 0x0000FD98
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
			throw new Exception("Xsd should have changed, XSD validation should have ensured that Multiplicity attribute has only 'None' or 'Bag' or 'List' as the values");
		}

		// Token: 0x040006C6 RID: 1734
		private SchemaType _type;

		// Token: 0x040006C7 RID: 1735
		private string _unresolvedType;

		// Token: 0x040006C8 RID: 1736
		private TypeUsageBuilder _typeUsageBuilder;

		// Token: 0x040006C9 RID: 1737
		private CollectionKind _collectionKind;
	}
}

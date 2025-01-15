using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200001B RID: 27
	internal class CollectionTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x060005F0 RID: 1520 RVA: 0x00009AAE File Offset: 0x00007CAE
		internal CollectionTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x060005F1 RID: 1521 RVA: 0x00009AB7 File Offset: 0x00007CB7
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "ElementType"))
			{
				this.HandleElementTypeAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00009ADC File Offset: 0x00007CDC
		protected void HandleElementTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x00009B04 File Offset: 0x00007D04
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.CanHandleElement(reader, "CollectionType"))
			{
				this.HandleCollectionTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "ReferenceType"))
			{
				this.HandleReferenceTypeElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "TypeRef"))
			{
				this.HandleTypeRefElement(reader);
				return true;
			}
			if (base.CanHandleElement(reader, "RowType"))
			{
				this.HandleRowTypeElement(reader);
				return true;
			}
			return false;
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00009B70 File Offset: 0x00007D70
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00009B94 File Offset: 0x00007D94
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00009BB8 File Offset: 0x00007DB8
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00009BDC File Offset: 0x00007DDC
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00009BFE File Offset: 0x00007DFE
		internal override void ResolveTopLevelNames()
		{
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00009C24 File Offset: 0x00007E24
		internal override void WriteIdentity(StringBuilder builder)
		{
			if (base.UnresolvedType != null && !base.UnresolvedType.Trim().Equals(string.Empty))
			{
				builder.Append("Collection(" + base.UnresolvedType + ")");
				return;
			}
			builder.Append("Collection(");
			this._typeSubElement.WriteIdentity(builder);
			builder.Append(")");
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x00009C94 File Offset: 0x00007E94
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage != null)
			{
				return this._typeUsage;
			}
			if (this._typeSubElement != null)
			{
				CollectionType collectionType = new CollectionType(this._typeSubElement.GetTypeUsage());
				collectionType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(collectionType);
			}
			return this._typeUsage;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x00009CE8 File Offset: 0x00007EE8
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage != null)
			{
				return true;
			}
			if (this._typeSubElement != null)
			{
				return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
			}
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
				return true;
			}
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			if (edmType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsageBuilder.TypeUsage));
			}
			return this._typeUsage != null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00009DB0 File Offset: 0x00007FB0
		internal override void Validate()
		{
			base.Validate();
			if (this._type != null && !(this._type is ScalarType) && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				base.AddError(ErrorCode.ModelFuncionFacetOnNonScalarType, EdmSchemaErrorSeverity.Error, Strings.FacetsOnNonScalarType(this._type.FQName));
			}
			if (this._type == null && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				base.AddError(ErrorCode.ModelFunctionIncorrectlyPlacedFacet, EdmSchemaErrorSeverity.Error, Strings.FacetDeclarationRequiresTypeAttribute);
			}
			if (this._type == null && this._typeSubElement == null)
			{
				base.AddError(ErrorCode.ModelFunctionTypeNotDeclared, EdmSchemaErrorSeverity.Error, Strings.TypeMustBeDeclared);
			}
			if (this._type != null && this._typeSubElement != null)
			{
				base.AddError(ErrorCode.TypeDeclaredAsAttributeAndElement, EdmSchemaErrorSeverity.Error, Strings.TypeDeclaredAsAttributeAndElement);
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x040005A3 RID: 1443
		private ModelFunctionTypeElement _typeSubElement;
	}
}

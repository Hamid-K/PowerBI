using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000044 RID: 68
	internal class RowTypePropertyElement : ModelFunctionTypeElement
	{
		// Token: 0x06000794 RID: 1940 RVA: 0x0000F040 File Offset: 0x0000D240
		internal RowTypePropertyElement(SchemaElement parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x0000F055 File Offset: 0x0000D255
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

		// Token: 0x06000796 RID: 1942 RVA: 0x0000F078 File Offset: 0x0000D278
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
			return false;
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x0000F09C File Offset: 0x0000D29C
		protected void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			TypeModifier typeModifier;
			Function.RemoveTypeModifier(ref text, out typeModifier, out this._isRefType);
			if (typeModifier == TypeModifier.Array)
			{
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0000F0EC File Offset: 0x0000D2EC
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

		// Token: 0x06000799 RID: 1945 RVA: 0x0000F158 File Offset: 0x0000D358
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x0000F17C File Offset: 0x0000D37C
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x0000F1A0 File Offset: 0x0000D3A0
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x0000F1C4 File Offset: 0x0000D3C4
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0000F1E8 File Offset: 0x0000D3E8
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Property(");
			if (base.UnresolvedType != null && !base.UnresolvedType.Trim().Equals(string.Empty))
			{
				if (this._collectionKind != CollectionKind.None)
				{
					builder.Append("Collection(" + base.UnresolvedType + ")");
				}
				else if (this._isRefType)
				{
					builder.Append("Ref(" + base.UnresolvedType + ")");
				}
				else
				{
					builder.Append(base.UnresolvedType);
				}
			}
			else
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0000F293 File Offset: 0x0000D493
		internal override TypeUsage GetTypeUsage()
		{
			if (this._typeUsage != null)
			{
				return this._typeUsage;
			}
			if (this._typeSubElement != null)
			{
				this._typeUsage = this._typeSubElement.GetTypeUsage();
			}
			return this._typeUsage;
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x0000F2C4 File Offset: 0x0000D4C4
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
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
			}
			else
			{
				EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
				if (edmType != null)
				{
					if (this._isRefType)
					{
						EntityType entityType = edmType as EntityType;
						this._typeUsage = TypeUsage.Create(new RefType(entityType));
					}
					else
					{
						this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
						this._typeUsage = this._typeUsageBuilder.TypeUsage;
					}
				}
			}
			if (this._collectionKind != CollectionKind.None)
			{
				this._typeUsage = TypeUsage.Create(new CollectionType(this._typeUsage));
			}
			return this._typeUsage != null;
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
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
			if (this._type != null && this._isRefType && !(this._type is SchemaEntityType))
			{
				base.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(this._type.FQName));
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x0400068D RID: 1677
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x0400068E RID: 1678
		private bool _isRefType;

		// Token: 0x0400068F RID: 1679
		private CollectionKind _collectionKind;
	}
}

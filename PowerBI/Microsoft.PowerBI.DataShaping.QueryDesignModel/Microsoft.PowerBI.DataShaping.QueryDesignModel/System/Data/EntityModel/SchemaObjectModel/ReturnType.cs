using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000041 RID: 65
	internal class ReturnType : ModelFunctionTypeElement
	{
		// Token: 0x06000776 RID: 1910 RVA: 0x0000E8EC File Offset: 0x0000CAEC
		internal ReturnType(Function parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170002F9 RID: 761
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0000E901 File Offset: 0x0000CB01
		// (set) Token: 0x06000778 RID: 1912 RVA: 0x0000E909 File Offset: 0x0000CB09
		internal CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
			set
			{
				this._collectionKind = value;
			}
		}

		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000779 RID: 1913 RVA: 0x0000E914 File Offset: 0x0000CB14
		internal override TypeUsage TypeUsage
		{
			get
			{
				if (this._typeSubElement != null)
				{
					return this._typeSubElement.GetTypeUsage();
				}
				if (this._typeUsage != null)
				{
					return this._typeUsage;
				}
				if (base.TypeUsage == null)
				{
					return null;
				}
				if (this.CollectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(base.TypeUsage));
				}
				return base.TypeUsage;
			}
		}

		// Token: 0x0600077A RID: 1914 RVA: 0x0000E96D File Offset: 0x0000CB6D
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new ReturnType((Function)parentElement)
			{
				_type = this._type,
				Name = this.Name,
				_typeUsageBuilder = this._typeUsageBuilder,
				_unresolvedType = this._unresolvedType
			};
		}

		// Token: 0x0600077B RID: 1915 RVA: 0x0000E9AA File Offset: 0x0000CBAA
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
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0000E9E0 File Offset: 0x0000CBE0
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeSubElement != null)
			{
				return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
			}
			if (this._type != null)
			{
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
			return true;
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0000EAD4 File Offset: 0x0000CCD4
		private void HandleTypeAttribute(XmlReader reader)
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
				this.CollectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0000EB24 File Offset: 0x0000CD24
		protected override bool HandleElement(XmlReader reader)
		{
			if (base.HandleElement(reader))
			{
				return true;
			}
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

		// Token: 0x0600077F RID: 1919 RVA: 0x0000EB9C File Offset: 0x0000CD9C
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06000780 RID: 1920 RVA: 0x0000EBC0 File Offset: 0x0000CDC0
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06000781 RID: 1921 RVA: 0x0000EBE4 File Offset: 0x0000CDE4
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06000782 RID: 1922 RVA: 0x0000EC08 File Offset: 0x0000CE08
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06000783 RID: 1923 RVA: 0x0000EC2C File Offset: 0x0000CE2C
		internal override void ResolveTopLevelNames()
		{
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (this._unresolvedType != null)
				{
					base.ResolveTopLevelNames();
				}
				if (this._typeSubElement != null)
				{
					this._typeSubElement.ResolveTopLevelNames();
					return;
				}
			}
			else
			{
				base.ResolveTopLevelNames();
				if (base.Schema.ResolveTypeName(this, base.UnresolvedType, out this._type) && !(this._type is ScalarType))
				{
					base.AddError(ErrorCode.FunctionWithNonEdmTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonEdmPrimitiveTypeNotSupported(this._type.FQName, this.FQName));
				}
			}
		}

		// Token: 0x06000784 RID: 1924 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		internal void ValidateForModelFunction()
		{
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
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0000ED5D File Offset: 0x0000CF5D
		internal override void WriteIdentity(StringBuilder builder)
		{
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0000ED5F File Offset: 0x0000CF5F
		internal override TypeUsage GetTypeUsage()
		{
			return this.TypeUsage;
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0000ED67 File Offset: 0x0000CF67
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this.ResolveNestedTypeNames(convertedItemCache, newGlobalItems);
		}

		// Token: 0x04000687 RID: 1671
		private CollectionKind _collectionKind;

		// Token: 0x04000688 RID: 1672
		private bool _isRefType;

		// Token: 0x04000689 RID: 1673
		private ModelFunctionTypeElement _typeSubElement;
	}
}

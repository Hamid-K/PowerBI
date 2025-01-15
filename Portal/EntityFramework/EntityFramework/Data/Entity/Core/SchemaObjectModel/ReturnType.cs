using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x0200030B RID: 779
	internal class ReturnType : ModelFunctionTypeElement
	{
		// Token: 0x060024E6 RID: 9446 RVA: 0x00068A9C File Offset: 0x00066C9C
		internal ReturnType(Function parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170007D8 RID: 2008
		// (get) Token: 0x060024E7 RID: 9447 RVA: 0x00068AB1 File Offset: 0x00066CB1
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170007D9 RID: 2009
		// (get) Token: 0x060024E8 RID: 9448 RVA: 0x00068AB9 File Offset: 0x00066CB9
		internal CollectionKind CollectionKind
		{
			get
			{
				return this._collectionKind;
			}
		}

		// Token: 0x170007DA RID: 2010
		// (get) Token: 0x060024E9 RID: 9449 RVA: 0x00068AC1 File Offset: 0x00066CC1
		internal EntityContainerEntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x170007DB RID: 2011
		// (get) Token: 0x060024EA RID: 9450 RVA: 0x00068AC9 File Offset: 0x00066CC9
		internal bool EntitySetPathDefined
		{
			get
			{
				return this._entitySetPathDefined;
			}
		}

		// Token: 0x170007DC RID: 2012
		// (get) Token: 0x060024EB RID: 9451 RVA: 0x00068AD1 File Offset: 0x00066CD1
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x060024EC RID: 9452 RVA: 0x00068ADC File Offset: 0x00066CDC
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
				if (this._collectionKind != CollectionKind.None)
				{
					return TypeUsage.Create(new CollectionType(base.TypeUsage));
				}
				return base.TypeUsage;
			}
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x00068B38 File Offset: 0x00066D38
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new ReturnType((Function)parentElement)
			{
				_type = this._type,
				Name = this.Name,
				_typeUsageBuilder = this._typeUsageBuilder,
				_unresolvedType = this._unresolvedType,
				_unresolvedEntitySet = this._unresolvedEntitySet,
				_entitySetPathDefined = this._entitySetPathDefined,
				_entitySet = this._entitySet
			};
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x00068BA4 File Offset: 0x00066DA4
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
			if (SchemaElement.CanHandleAttribute(reader, "EntitySet"))
			{
				this.HandleEntitySetAttribute(reader);
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "EntitySetPath"))
			{
				this.HandleEntitySetPathAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x00068C0F File Offset: 0x00066E0F
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x00068C20 File Offset: 0x00066E20
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
				this._collectionKind = CollectionKind.Bag;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			base.UnresolvedType = text;
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x00068C70 File Offset: 0x00066E70
		private void HandleEntitySetAttribute(XmlReader reader)
		{
			string text;
			if (Utils.GetString(base.Schema, reader, out text))
			{
				this._unresolvedEntitySet = text;
			}
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x00068C94 File Offset: 0x00066E94
		private void HandleEntitySetPathAttribute(XmlReader reader)
		{
			string text;
			if (Utils.GetString(base.Schema, reader, out text))
			{
				this._entitySetPathDefined = true;
			}
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x00068CB8 File Offset: 0x00066EB8
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

		// Token: 0x060024F4 RID: 9460 RVA: 0x00068D30 File Offset: 0x00066F30
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00068D54 File Offset: 0x00066F54
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00068D78 File Offset: 0x00066F78
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x00068D9C File Offset: 0x00066F9C
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00068DC0 File Offset: 0x00066FC0
		internal override void ResolveTopLevelNames()
		{
			if (this._unresolvedType != null)
			{
				base.ResolveTopLevelNames();
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.ResolveTopLevelNames();
			}
			if (base.ParentElement.IsFunctionImport && this._unresolvedEntitySet != null)
			{
				((FunctionImportElement)base.ParentElement).ResolveEntitySet(this, this._unresolvedEntitySet, ref this._entitySet);
			}
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x00068E20 File Offset: 0x00067020
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				if (base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel)
				{
					if ((this._type != null && (!(this._type is ScalarType) || this._collectionKind != CollectionKind.None)) || (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType)))
					{
						string text = "";
						if (this._type != null)
						{
							text = Function.GetTypeNameForErrorMessage(this._type, this._collectionKind, this._isRefType);
						}
						else if (this._typeSubElement != null)
						{
							text = this._typeSubElement.FQName;
						}
						base.AddError(ErrorCode.FunctionWithNonEdmTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonEdmPrimitiveTypeNotSupported(text, base.ParentElement.FQName));
					}
				}
				else if (this._type != null)
				{
					if (!(this._type is ScalarType) || this._collectionKind != CollectionKind.None)
					{
						base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._isRefType ? this._unresolvedType : this._type.FQName, base.ParentElement.FQName));
					}
				}
				else if (this._typeSubElement != null && !(this._typeSubElement.Type is ScalarType))
				{
					if (base.Schema.SchemaVersion < 3.0)
					{
						base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._typeSubElement.FQName, base.ParentElement.FQName));
					}
					else
					{
						CollectionTypeElement collectionTypeElement = this._typeSubElement as CollectionTypeElement;
						if (collectionTypeElement != null)
						{
							RowTypeElement rowTypeElement = collectionTypeElement.SubElement as RowTypeElement;
							if (rowTypeElement != null)
							{
								if (rowTypeElement.Properties.Any((RowTypePropertyElement p) => !p.ValidateIsScalar()))
								{
									base.AddError(ErrorCode.TVFReturnTypeRowHasNonScalarProperty, EdmSchemaErrorSeverity.Error, this, Strings.TVFReturnTypeRowHasNonScalarProperty);
								}
							}
						}
					}
				}
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x0006904E File Offset: 0x0006724E
		internal override void WriteIdentity(StringBuilder builder)
		{
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x00069050 File Offset: 0x00067250
		internal override TypeUsage GetTypeUsage()
		{
			return this.TypeUsage;
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x00069058 File Offset: 0x00067258
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return false;
		}

		// Token: 0x04000D0D RID: 3341
		private CollectionKind _collectionKind;

		// Token: 0x04000D0E RID: 3342
		private bool _isRefType;

		// Token: 0x04000D0F RID: 3343
		private string _unresolvedEntitySet;

		// Token: 0x04000D10 RID: 3344
		private bool _entitySetPathDefined;

		// Token: 0x04000D11 RID: 3345
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000D12 RID: 3346
		private EntityContainerEntitySet _entitySet;
	}
}

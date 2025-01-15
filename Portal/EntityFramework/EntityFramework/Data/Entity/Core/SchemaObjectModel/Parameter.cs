using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000300 RID: 768
	internal class Parameter : FacetEnabledSchemaElement
	{
		// Token: 0x0600246E RID: 9326 RVA: 0x000670A5 File Offset: 0x000652A5
		internal Parameter(Function parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170007BA RID: 1978
		// (get) Token: 0x0600246F RID: 9327 RVA: 0x000670C1 File Offset: 0x000652C1
		internal ParameterDirection ParameterDirection
		{
			get
			{
				return this._parameterDirection;
			}
		}

		// Token: 0x170007BB RID: 1979
		// (get) Token: 0x06002470 RID: 9328 RVA: 0x000670C9 File Offset: 0x000652C9
		// (set) Token: 0x06002471 RID: 9329 RVA: 0x000670D1 File Offset: 0x000652D1
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

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06002472 RID: 9330 RVA: 0x000670DA File Offset: 0x000652DA
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06002473 RID: 9331 RVA: 0x000670E2 File Offset: 0x000652E2
		internal override TypeUsage TypeUsage
		{
			get
			{
				if (this._typeSubElement != null)
				{
					return this._typeSubElement.GetTypeUsage();
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

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06002474 RID: 9332 RVA: 0x00067121 File Offset: 0x00065321
		internal new SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x06002475 RID: 9333 RVA: 0x0006712C File Offset: 0x0006532C
		internal void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Parameter(");
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
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
			else if (this._typeSubElement != null)
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x06002476 RID: 9334 RVA: 0x000671D0 File Offset: 0x000653D0
		internal override SchemaElement Clone(SchemaElement parentElement)
		{
			return new Parameter((Function)parentElement)
			{
				_collectionKind = this._collectionKind,
				_parameterDirection = this._parameterDirection,
				_type = this._type,
				Name = this.Name,
				_typeUsageBuilder = this._typeUsageBuilder
			};
		}

		// Token: 0x06002477 RID: 9335 RVA: 0x00067224 File Offset: 0x00065424
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement != null && this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x06002478 RID: 9336 RVA: 0x00067240 File Offset: 0x00065440
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
			if (SchemaElement.CanHandleAttribute(reader, "Mode"))
			{
				this.HandleModeAttribute(reader);
				return true;
			}
			return this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x06002479 RID: 9337 RVA: 0x00067298 File Offset: 0x00065498
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

		// Token: 0x0600247A RID: 9338 RVA: 0x000672E8 File Offset: 0x000654E8
		private void HandleModeAttribute(XmlReader reader)
		{
			string text = reader.Value;
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			text = text.Trim();
			if (!string.IsNullOrEmpty(text))
			{
				if (text != null)
				{
					if (text == "In")
					{
						this._parameterDirection = ParameterDirection.Input;
						return;
					}
					if (!(text == "Out"))
					{
						if (text == "InOut")
						{
							this._parameterDirection = ParameterDirection.InputOutput;
							if (base.ParentElement.IsComposable && base.ParentElement.IsFunctionImport)
							{
								this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirectionForComposableFunctions));
								return;
							}
							return;
						}
					}
					else
					{
						this._parameterDirection = ParameterDirection.Output;
						if (base.ParentElement.IsComposable && base.ParentElement.IsFunctionImport)
						{
							this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirectionForComposableFunctions));
							return;
						}
						return;
					}
				}
				this.AddErrorBadParameterDirection(text, reader, new Func<object, object, object, object, string>(Strings.BadParameterDirection));
			}
		}

		// Token: 0x0600247B RID: 9339 RVA: 0x000673D0 File Offset: 0x000655D0
		private void AddErrorBadParameterDirection(string value, XmlReader reader, Func<object, object, object, object, string> errorFunc)
		{
			base.AddError(ErrorCode.BadParameterDirection, EdmSchemaErrorSeverity.Error, reader, errorFunc(base.ParentElement.Parameters.Count, base.ParentElement.Name, base.ParentElement.ParentElement.FQName, value));
		}

		// Token: 0x0600247C RID: 9340 RVA: 0x00067420 File Offset: 0x00065620
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
			if (base.Schema.DataModel == SchemaDataModelOption.EntityDataModel)
			{
				if (base.CanHandleElement(reader, "ValueAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
				if (base.CanHandleElement(reader, "TypeAnnotation"))
				{
					this.SkipElement(reader);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000674D0 File Offset: 0x000656D0
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x000674F4 File Offset: 0x000656F4
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x00067518 File Offset: 0x00065718
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0006753C File Offset: 0x0006573C
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0006755E File Offset: 0x0006575E
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
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x00067584 File Offset: 0x00065784
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
			{
				bool isAggregate = base.ParentElement.IsAggregate;
				if (this._type != null && (!(this._type is ScalarType) || (!isAggregate && this._collectionKind != CollectionKind.None)))
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
					if (base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel)
					{
						base.AddError(ErrorCode.FunctionWithNonEdmTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonEdmPrimitiveTypeNotSupported(text, base.ParentElement.FQName));
						return;
					}
					base.AddError(ErrorCode.FunctionWithNonPrimitiveTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(text, base.ParentElement.FQName));
					return;
				}
			}
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			if (this._isRefType)
			{
				ValidationHelper.ValidateRefType(this, this._type);
			}
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x04000CF7 RID: 3319
		private ParameterDirection _parameterDirection = ParameterDirection.Input;

		// Token: 0x04000CF8 RID: 3320
		private CollectionKind _collectionKind;

		// Token: 0x04000CF9 RID: 3321
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000CFA RID: 3322
		private bool _isRefType;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000037 RID: 55
	internal class Parameter : FacetEnabledSchemaElement
	{
		// Token: 0x06000703 RID: 1795 RVA: 0x0000D0B2 File Offset: 0x0000B2B2
		internal Parameter(Function parentElement)
			: base(parentElement)
		{
			this._typeUsageBuilder = new TypeUsageBuilder(this);
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000704 RID: 1796 RVA: 0x0000D0CE File Offset: 0x0000B2CE
		internal ParameterDirection ParameterDirection
		{
			get
			{
				return this._parameterDirection;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000705 RID: 1797 RVA: 0x0000D0D6 File Offset: 0x0000B2D6
		// (set) Token: 0x06000706 RID: 1798 RVA: 0x0000D0DE File Offset: 0x0000B2DE
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

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000707 RID: 1799 RVA: 0x0000D0E7 File Offset: 0x0000B2E7
		internal bool IsRefType
		{
			get
			{
				return this._isRefType;
			}
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000708 RID: 1800 RVA: 0x0000D0EF File Offset: 0x0000B2EF
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

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000709 RID: 1801 RVA: 0x0000D12E File Offset: 0x0000B32E
		internal new SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x0600070A RID: 1802 RVA: 0x0000D138 File Offset: 0x0000B338
		internal void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Parameter(");
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
			else if (this._typeSubElement != null)
			{
				this._typeSubElement.WriteIdentity(builder);
			}
			builder.Append(")");
		}

		// Token: 0x0600070B RID: 1803 RVA: 0x0000D1EC File Offset: 0x0000B3EC
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

		// Token: 0x0600070C RID: 1804 RVA: 0x0000D240 File Offset: 0x0000B440
		internal bool ResolveNestedTypeNames(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			return this._typeSubElement != null && this._typeSubElement.ResolveNameAndSetTypeUsage(convertedItemCache, newGlobalItems);
		}

		// Token: 0x0600070D RID: 1805 RVA: 0x0000D25C File Offset: 0x0000B45C
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

		// Token: 0x0600070E RID: 1806 RVA: 0x0000D2B4 File Offset: 0x0000B4B4
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

		// Token: 0x0600070F RID: 1807 RVA: 0x0000D304 File Offset: 0x0000B504
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
				if (text == "In")
				{
					this._parameterDirection = ParameterDirection.Input;
					return;
				}
				if (text == "Out")
				{
					this._parameterDirection = ParameterDirection.Output;
					return;
				}
				if (text == "InOut")
				{
					this._parameterDirection = ParameterDirection.InputOutput;
					return;
				}
				int count = base.ParentElement.Parameters.Count;
				base.AddError(ErrorCode.BadParameterDirection, EdmSchemaErrorSeverity.Error, reader, Strings.BadParameterDirection(text, count, base.ParentElement.Name, base.ParentElement.ParentElement.FQName));
			}
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
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

		// Token: 0x06000711 RID: 1809 RVA: 0x0000D430 File Offset: 0x0000B630
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06000712 RID: 1810 RVA: 0x0000D454 File Offset: 0x0000B654
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06000713 RID: 1811 RVA: 0x0000D478 File Offset: 0x0000B678
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0000D49C File Offset: 0x0000B69C
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x06000715 RID: 1813 RVA: 0x0000D4BE File Offset: 0x0000B6BE
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

		// Token: 0x06000716 RID: 1814 RVA: 0x0000D4E4 File Offset: 0x0000B6E4
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

		// Token: 0x04000673 RID: 1651
		private ParameterDirection _parameterDirection = ParameterDirection.Input;

		// Token: 0x04000674 RID: 1652
		private CollectionKind _collectionKind;

		// Token: 0x04000675 RID: 1653
		private ModelFunctionTypeElement _typeSubElement;

		// Token: 0x04000676 RID: 1654
		private bool _isRefType;
	}
}

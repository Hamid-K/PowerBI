using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000058 RID: 88
	internal class TypeRefElement : ModelFunctionTypeElement
	{
		// Token: 0x06000896 RID: 2198 RVA: 0x0001258C File Offset: 0x0001078C
		internal TypeRefElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000897 RID: 2199 RVA: 0x00012595 File Offset: 0x00010795
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

		// Token: 0x06000898 RID: 2200 RVA: 0x000125BC File Offset: 0x000107BC
		protected void HandleTypeAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			if (!Utils.ValidateDottedName(base.Schema, reader, text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x06000899 RID: 2201 RVA: 0x000125F4 File Offset: 0x000107F4
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._type is ScalarType)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(this._type as ScalarType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
				return true;
			}
			EdmType edmType = (EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems);
			if (edmType != null)
			{
				this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
				this._typeUsage = this._typeUsageBuilder.TypeUsage;
			}
			return this._typeUsage != null;
		}

		// Token: 0x0600089A RID: 2202 RVA: 0x00012685 File Offset: 0x00010885
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append(base.UnresolvedType);
		}

		// Token: 0x0600089B RID: 2203 RVA: 0x00012694 File Offset: 0x00010894
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x0001269C File Offset: 0x0001089C
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
		}
	}
}

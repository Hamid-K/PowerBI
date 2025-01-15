using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x0200003B RID: 59
	internal class ReferenceTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06000727 RID: 1831 RVA: 0x0000D751 File Offset: 0x0000B951
		internal ReferenceTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0000D75A File Offset: 0x0000B95A
		protected override bool HandleAttribute(XmlReader reader)
		{
			if (base.HandleAttribute(reader))
			{
				return true;
			}
			if (SchemaElement.CanHandleAttribute(reader, "Type"))
			{
				this.HandleTypeElementAttribute(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0000D780 File Offset: 0x0000B980
		protected void HandleTypeElementAttribute(XmlReader reader)
		{
			string text;
			if (!Utils.GetString(base.Schema, reader, out text))
			{
				return;
			}
			this._unresolvedType = text;
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0000D7A5 File Offset: 0x0000B9A5
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Ref(" + base.UnresolvedType + ")");
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0000D7C3 File Offset: 0x0000B9C3
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0000D7CC File Offset: 0x0000B9CC
		internal override bool ResolveNameAndSetTypeUsage(Converter.ConversionCache convertedItemCache, Dictionary<SchemaElement, GlobalItem> newGlobalItems)
		{
			if (this._typeUsage == null)
			{
				RefType refType = new RefType(((EdmType)Converter.LoadSchemaElement(this._type, this._type.Schema.ProviderManifest, convertedItemCache, newGlobalItems)) as EntityType);
				refType.AddMetadataProperties(base.OtherContent);
				this._typeUsage = TypeUsage.Create(refType);
			}
			return true;
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0000D827 File Offset: 0x0000BA27
		internal override void Validate()
		{
			base.Validate();
			if (this._type != null && !(this._type is SchemaEntityType))
			{
				base.AddError(ErrorCode.ReferenceToNonEntityType, EdmSchemaErrorSeverity.Error, Strings.ReferenceToNonEntityType(this._type.FQName));
			}
		}
	}
}

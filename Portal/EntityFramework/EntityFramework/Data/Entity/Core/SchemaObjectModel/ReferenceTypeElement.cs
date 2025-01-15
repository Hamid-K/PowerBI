using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000305 RID: 773
	internal class ReferenceTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002498 RID: 9368 RVA: 0x00067918 File Offset: 0x00065B18
		internal ReferenceTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x00067921 File Offset: 0x00065B21
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

		// Token: 0x0600249A RID: 9370 RVA: 0x00067948 File Offset: 0x00065B48
		protected void HandleTypeElementAttribute(XmlReader reader)
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

		// Token: 0x0600249B RID: 9371 RVA: 0x0006797D File Offset: 0x00065B7D
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append("Ref(" + base.UnresolvedType + ")");
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0006799B File Offset: 0x00065B9B
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x000679A4 File Offset: 0x00065BA4
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

		// Token: 0x0600249E RID: 9374 RVA: 0x000679FF File Offset: 0x00065BFF
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateRefType(this, this._type);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x02000323 RID: 803
	internal class TypeRefElement : ModelFunctionTypeElement
	{
		// Token: 0x06002629 RID: 9769 RVA: 0x0006D318 File Offset: 0x0006B518
		internal TypeRefElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x0600262A RID: 9770 RVA: 0x0006D321 File Offset: 0x0006B521
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

		// Token: 0x0600262B RID: 9771 RVA: 0x0006D348 File Offset: 0x0006B548
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

		// Token: 0x0600262C RID: 9772 RVA: 0x0006D380 File Offset: 0x0006B580
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

		// Token: 0x0600262D RID: 9773 RVA: 0x0006D411 File Offset: 0x0006B611
		internal override void WriteIdentity(StringBuilder builder)
		{
			builder.Append(base.UnresolvedType);
		}

		// Token: 0x0600262E RID: 9774 RVA: 0x0006D420 File Offset: 0x0006B620
		internal override TypeUsage GetTypeUsage()
		{
			return this._typeUsage;
		}

		// Token: 0x0600262F RID: 9775 RVA: 0x0006D428 File Offset: 0x0006B628
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Text;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002E5 RID: 741
	internal class CollectionTypeElement : ModelFunctionTypeElement
	{
		// Token: 0x06002352 RID: 9042 RVA: 0x0006385A File Offset: 0x00061A5A
		internal CollectionTypeElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06002353 RID: 9043 RVA: 0x00063863 File Offset: 0x00061A63
		internal ModelFunctionTypeElement SubElement
		{
			get
			{
				return this._typeSubElement;
			}
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x0006386B File Offset: 0x00061A6B
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

		// Token: 0x06002355 RID: 9045 RVA: 0x00063890 File Offset: 0x00061A90
		protected void HandleElementTypeAttribute(XmlReader reader)
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

		// Token: 0x06002356 RID: 9046 RVA: 0x000638C8 File Offset: 0x00061AC8
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

		// Token: 0x06002357 RID: 9047 RVA: 0x00063934 File Offset: 0x00061B34
		protected void HandleCollectionTypeElement(XmlReader reader)
		{
			CollectionTypeElement collectionTypeElement = new CollectionTypeElement(this);
			collectionTypeElement.Parse(reader);
			this._typeSubElement = collectionTypeElement;
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00063958 File Offset: 0x00061B58
		protected void HandleReferenceTypeElement(XmlReader reader)
		{
			ReferenceTypeElement referenceTypeElement = new ReferenceTypeElement(this);
			referenceTypeElement.Parse(reader);
			this._typeSubElement = referenceTypeElement;
		}

		// Token: 0x06002359 RID: 9049 RVA: 0x0006397C File Offset: 0x00061B7C
		protected void HandleTypeRefElement(XmlReader reader)
		{
			TypeRefElement typeRefElement = new TypeRefElement(this);
			typeRefElement.Parse(reader);
			this._typeSubElement = typeRefElement;
		}

		// Token: 0x0600235A RID: 9050 RVA: 0x000639A0 File Offset: 0x00061BA0
		protected void HandleRowTypeElement(XmlReader reader)
		{
			RowTypeElement rowTypeElement = new RowTypeElement(this);
			rowTypeElement.Parse(reader);
			this._typeSubElement = rowTypeElement;
		}

		// Token: 0x0600235B RID: 9051 RVA: 0x000639C2 File Offset: 0x00061BC2
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

		// Token: 0x0600235C RID: 9052 RVA: 0x000639E8 File Offset: 0x00061BE8
		internal override void WriteIdentity(StringBuilder builder)
		{
			if (!string.IsNullOrWhiteSpace(base.UnresolvedType))
			{
				builder.Append("Collection(" + base.UnresolvedType + ")");
				return;
			}
			builder.Append("Collection(");
			this._typeSubElement.WriteIdentity(builder);
			builder.Append(")");
		}

		// Token: 0x0600235D RID: 9053 RVA: 0x00063A44 File Offset: 0x00061C44
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

		// Token: 0x0600235E RID: 9054 RVA: 0x00063A98 File Offset: 0x00061C98
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

		// Token: 0x0600235F RID: 9055 RVA: 0x00063B60 File Offset: 0x00061D60
		internal override void Validate()
		{
			base.Validate();
			ValidationHelper.ValidateFacets(this, this._type, this._typeUsageBuilder);
			ValidationHelper.ValidateTypeDeclaration(this, this._type, this._typeSubElement);
			if (this._typeSubElement != null)
			{
				this._typeSubElement.Validate();
			}
		}

		// Token: 0x04000C16 RID: 3094
		private ModelFunctionTypeElement _typeSubElement;
	}
}

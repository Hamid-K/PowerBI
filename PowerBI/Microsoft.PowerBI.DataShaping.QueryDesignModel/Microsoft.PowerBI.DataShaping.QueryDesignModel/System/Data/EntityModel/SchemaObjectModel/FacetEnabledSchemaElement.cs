using System;
using System.Data.Entity;
using System.Xml;
using Microsoft.Data.Metadata.Edm;

namespace System.Data.EntityModel.SchemaObjectModel
{
	// Token: 0x02000027 RID: 39
	internal abstract class FacetEnabledSchemaElement : SchemaElement
	{
		// Token: 0x170002A4 RID: 676
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0000B878 File Offset: 0x00009A78
		internal new Function ParentElement
		{
			get
			{
				return base.ParentElement as Function;
			}
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000671 RID: 1649 RVA: 0x0000B885 File Offset: 0x00009A85
		internal SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000672 RID: 1650 RVA: 0x0000B88D File Offset: 0x00009A8D
		internal virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000673 RID: 1651 RVA: 0x0000B89A File Offset: 0x00009A9A
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._typeUsageBuilder.HasUserDefinedFacets;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000674 RID: 1652 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		// (set) Token: 0x06000675 RID: 1653 RVA: 0x0000B8AF File Offset: 0x00009AAF
		internal string UnresolvedType
		{
			get
			{
				return this._unresolvedType;
			}
			set
			{
				this._unresolvedType = value;
			}
		}

		// Token: 0x06000676 RID: 1654 RVA: 0x0000B8B8 File Offset: 0x00009AB8
		internal FacetEnabledSchemaElement(Function parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x0000B8C1 File Offset: 0x00009AC1
		internal FacetEnabledSchemaElement(SchemaElement parentElement)
			: base(parentElement)
		{
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x0000B8CC File Offset: 0x00009ACC
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.Schema.ResolveTypeName(this, this.UnresolvedType, out this._type))
			{
				if (!(this._type is ScalarType) && base.Schema.DataModel != SchemaDataModelOption.EntityDataModel)
				{
					base.AddError(ErrorCode.FunctionWithNonScalarTypeNotSupported, EdmSchemaErrorSeverity.Error, this, Strings.FunctionWithNonPrimitiveTypeNotSupported(this._type.FQName, this.ParentElement.FQName));
					return;
				}
				if (base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel && this._typeUsageBuilder.HasUserDefinedFacets)
				{
					bool flag = base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
					this._typeUsageBuilder.ValidateAndSetTypeUsage((ScalarType)this._type, !flag);
				}
			}
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0000B980 File Offset: 0x00009B80
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x0000B98F File Offset: 0x00009B8F
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x0400064B RID: 1611
		protected SchemaType _type;

		// Token: 0x0400064C RID: 1612
		protected string _unresolvedType;

		// Token: 0x0400064D RID: 1613
		protected TypeUsageBuilder _typeUsageBuilder;
	}
}

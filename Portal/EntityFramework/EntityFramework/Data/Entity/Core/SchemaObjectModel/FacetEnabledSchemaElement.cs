using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Xml;

namespace System.Data.Entity.Core.SchemaObjectModel
{
	// Token: 0x020002F1 RID: 753
	internal abstract class FacetEnabledSchemaElement : SchemaElement
	{
		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060023D5 RID: 9173 RVA: 0x00065648 File Offset: 0x00063848
		internal new Function ParentElement
		{
			get
			{
				return base.ParentElement as Function;
			}
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x060023D6 RID: 9174 RVA: 0x00065655 File Offset: 0x00063855
		internal SchemaType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x060023D7 RID: 9175 RVA: 0x0006565D File Offset: 0x0006385D
		internal virtual TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsageBuilder.TypeUsage;
			}
		}

		// Token: 0x17000784 RID: 1924
		// (get) Token: 0x060023D8 RID: 9176 RVA: 0x0006566A File Offset: 0x0006386A
		internal TypeUsageBuilder TypeUsageBuilder
		{
			get
			{
				return this._typeUsageBuilder;
			}
		}

		// Token: 0x17000785 RID: 1925
		// (get) Token: 0x060023D9 RID: 9177 RVA: 0x00065672 File Offset: 0x00063872
		internal bool HasUserDefinedFacets
		{
			get
			{
				return this._typeUsageBuilder.HasUserDefinedFacets;
			}
		}

		// Token: 0x17000786 RID: 1926
		// (get) Token: 0x060023DA RID: 9178 RVA: 0x0006567F File Offset: 0x0006387F
		// (set) Token: 0x060023DB RID: 9179 RVA: 0x00065687 File Offset: 0x00063887
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

		// Token: 0x060023DC RID: 9180 RVA: 0x00065690 File Offset: 0x00063890
		internal FacetEnabledSchemaElement(Function parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x060023DD RID: 9181 RVA: 0x0006569A File Offset: 0x0006389A
		internal FacetEnabledSchemaElement(SchemaElement parentElement)
			: base(parentElement, null)
		{
		}

		// Token: 0x060023DE RID: 9182 RVA: 0x000656A4 File Offset: 0x000638A4
		internal override void ResolveTopLevelNames()
		{
			base.ResolveTopLevelNames();
			if (base.Schema.ResolveTypeName(this, this.UnresolvedType, out this._type) && base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel && this._typeUsageBuilder.HasUserDefinedFacets)
			{
				bool flag = base.Schema.DataModel == SchemaDataModelOption.ProviderManifestModel;
				this._typeUsageBuilder.ValidateAndSetTypeUsage((ScalarType)this._type, !flag);
			}
		}

		// Token: 0x060023DF RID: 9183 RVA: 0x00065715 File Offset: 0x00063915
		internal void ValidateAndSetTypeUsage(ScalarType scalar)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(scalar, false);
		}

		// Token: 0x060023E0 RID: 9184 RVA: 0x00065724 File Offset: 0x00063924
		internal void ValidateAndSetTypeUsage(EdmType edmType)
		{
			this._typeUsageBuilder.ValidateAndSetTypeUsage(edmType, false);
		}

		// Token: 0x060023E1 RID: 9185 RVA: 0x00065733 File Offset: 0x00063933
		protected override bool HandleAttribute(XmlReader reader)
		{
			return base.HandleAttribute(reader) || this._typeUsageBuilder.HandleAttribute(reader);
		}

		// Token: 0x04000CCD RID: 3277
		protected SchemaType _type;

		// Token: 0x04000CCE RID: 3278
		protected string _unresolvedType;

		// Token: 0x04000CCF RID: 3279
		protected TypeUsageBuilder _typeUsageBuilder;
	}
}

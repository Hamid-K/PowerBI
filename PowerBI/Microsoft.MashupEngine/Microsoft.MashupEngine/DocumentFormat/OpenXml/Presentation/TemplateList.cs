using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A3D RID: 10813
	[ChildElementInfo(typeof(Template))]
	[GeneratedCode("DomGen", "2.0")]
	internal class TemplateList : OpenXmlCompositeElement
	{
		// Token: 0x17007134 RID: 28980
		// (get) Token: 0x06015BF9 RID: 89081 RVA: 0x00322B2E File Offset: 0x00320D2E
		public override string LocalName
		{
			get
			{
				return "tmplLst";
			}
		}

		// Token: 0x17007135 RID: 28981
		// (get) Token: 0x06015BFA RID: 89082 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007136 RID: 28982
		// (get) Token: 0x06015BFB RID: 89083 RVA: 0x00322B35 File Offset: 0x00320D35
		internal override int ElementTypeId
		{
			get
			{
				return 12231;
			}
		}

		// Token: 0x06015BFC RID: 89084 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015BFD RID: 89085 RVA: 0x00293ECF File Offset: 0x002920CF
		public TemplateList()
		{
		}

		// Token: 0x06015BFE RID: 89086 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TemplateList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BFF RID: 89087 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TemplateList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C00 RID: 89088 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TemplateList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C01 RID: 89089 RVA: 0x00322B3C File Offset: 0x00320D3C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "tmpl" == name)
			{
				return new Template();
			}
			return null;
		}

		// Token: 0x06015C02 RID: 89090 RVA: 0x00322B57 File Offset: 0x00320D57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TemplateList>(deep);
		}

		// Token: 0x040094A7 RID: 38055
		private const string tagName = "tmplLst";

		// Token: 0x040094A8 RID: 38056
		private const byte tagNsId = 24;

		// Token: 0x040094A9 RID: 38057
		internal const int ElementTypeIdConst = 12231;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A4C RID: 10828
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170071A3 RID: 29091
		// (get) Token: 0x06015CF2 RID: 89330 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170071A4 RID: 29092
		// (get) Token: 0x06015CF3 RID: 89331 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170071A5 RID: 29093
		// (get) Token: 0x06015CF4 RID: 89332 RVA: 0x00323538 File Offset: 0x00321738
		internal override int ElementTypeId
		{
			get
			{
				return 12247;
			}
		}

		// Token: 0x06015CF5 RID: 89333 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015CF6 RID: 89334 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x06015CF7 RID: 89335 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CF8 RID: 89336 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CF9 RID: 89337 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015CFA RID: 89338 RVA: 0x002E3E40 File Offset: 0x002E2040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06015CFB RID: 89339 RVA: 0x0032353F File Offset: 0x0032173F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x040094EB RID: 38123
		private const string tagName = "extLst";

		// Token: 0x040094EC RID: 38124
		private const byte tagNsId = 24;

		// Token: 0x040094ED RID: 38125
		internal const int ElementTypeIdConst = 12247;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2A RID: 10794
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AttributeName))]
	internal class AttributeNameList : OpenXmlCompositeElement
	{
		// Token: 0x170070A8 RID: 28840
		// (get) Token: 0x06015AC3 RID: 88771 RVA: 0x00321E94 File Offset: 0x00320094
		public override string LocalName
		{
			get
			{
				return "attrNameLst";
			}
		}

		// Token: 0x170070A9 RID: 28841
		// (get) Token: 0x06015AC4 RID: 88772 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170070AA RID: 28842
		// (get) Token: 0x06015AC5 RID: 88773 RVA: 0x00321E9B File Offset: 0x0032009B
		internal override int ElementTypeId
		{
			get
			{
				return 12216;
			}
		}

		// Token: 0x06015AC6 RID: 88774 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015AC7 RID: 88775 RVA: 0x00293ECF File Offset: 0x002920CF
		public AttributeNameList()
		{
		}

		// Token: 0x06015AC8 RID: 88776 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AttributeNameList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015AC9 RID: 88777 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AttributeNameList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015ACA RID: 88778 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AttributeNameList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015ACB RID: 88779 RVA: 0x00321EA2 File Offset: 0x003200A2
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "attrName" == name)
			{
				return new AttributeName();
			}
			return null;
		}

		// Token: 0x06015ACC RID: 88780 RVA: 0x00321EBD File Offset: 0x003200BD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AttributeNameList>(deep);
		}

		// Token: 0x04009450 RID: 37968
		private const string tagName = "attrNameLst";

		// Token: 0x04009451 RID: 37969
		private const byte tagNsId = 24;

		// Token: 0x04009452 RID: 37970
		internal const int ElementTypeIdConst = 12216;
	}
}

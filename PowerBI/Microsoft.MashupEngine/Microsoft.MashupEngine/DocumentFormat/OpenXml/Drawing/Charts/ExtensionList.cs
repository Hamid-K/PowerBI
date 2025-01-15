using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002579 RID: 9593
	[ChildElementInfo(typeof(Extension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170055FA RID: 22010
		// (get) Token: 0x06011E4D RID: 73293 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170055FB RID: 22011
		// (get) Token: 0x06011E4E RID: 73294 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055FC RID: 22012
		// (get) Token: 0x06011E4F RID: 73295 RVA: 0x002F3692 File Offset: 0x002F1892
		internal override int ElementTypeId
		{
			get
			{
				return 10394;
			}
		}

		// Token: 0x06011E50 RID: 73296 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E51 RID: 73297 RVA: 0x00293ECF File Offset: 0x002920CF
		public ExtensionList()
		{
		}

		// Token: 0x06011E52 RID: 73298 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E53 RID: 73299 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E54 RID: 73300 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E55 RID: 73301 RVA: 0x002F3699 File Offset: 0x002F1899
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06011E56 RID: 73302 RVA: 0x002F36B4 File Offset: 0x002F18B4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ExtensionList>(deep);
		}

		// Token: 0x04007D22 RID: 32034
		private const string tagName = "extLst";

		// Token: 0x04007D23 RID: 32035
		private const byte tagNsId = 11;

		// Token: 0x04007D24 RID: 32036
		internal const int ElementTypeIdConst = 10394;
	}
}

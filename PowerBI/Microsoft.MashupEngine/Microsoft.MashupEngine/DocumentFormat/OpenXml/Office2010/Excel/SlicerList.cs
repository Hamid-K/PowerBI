using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023D6 RID: 9174
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlicerRef), FileFormatVersions.Office2010)]
	internal class SlicerList : OpenXmlCompositeElement
	{
		// Token: 0x17004D2F RID: 19759
		// (get) Token: 0x06010A91 RID: 68241 RVA: 0x002E5C29 File Offset: 0x002E3E29
		public override string LocalName
		{
			get
			{
				return "slicerList";
			}
		}

		// Token: 0x17004D30 RID: 19760
		// (get) Token: 0x06010A92 RID: 68242 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004D31 RID: 19761
		// (get) Token: 0x06010A93 RID: 68243 RVA: 0x002E5C30 File Offset: 0x002E3E30
		internal override int ElementTypeId
		{
			get
			{
				return 12900;
			}
		}

		// Token: 0x06010A94 RID: 68244 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06010A95 RID: 68245 RVA: 0x00293ECF File Offset: 0x002920CF
		public SlicerList()
		{
		}

		// Token: 0x06010A96 RID: 68246 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SlicerList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A97 RID: 68247 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SlicerList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010A98 RID: 68248 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SlicerList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010A99 RID: 68249 RVA: 0x002E5C37 File Offset: 0x002E3E37
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "slicer" == name)
			{
				return new SlicerRef();
			}
			return null;
		}

		// Token: 0x06010A9A RID: 68250 RVA: 0x002E5C52 File Offset: 0x002E3E52
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SlicerList>(deep);
		}

		// Token: 0x040075CB RID: 30155
		private const string tagName = "slicerList";

		// Token: 0x040075CC RID: 30156
		private const byte tagNsId = 53;

		// Token: 0x040075CD RID: 30157
		internal const int ElementTypeIdConst = 12900;
	}
}

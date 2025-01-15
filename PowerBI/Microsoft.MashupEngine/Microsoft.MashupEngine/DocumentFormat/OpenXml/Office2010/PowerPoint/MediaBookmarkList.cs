using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.PowerPoint
{
	// Token: 0x020023BF RID: 9151
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MediaBookmark), FileFormatVersions.Office2010)]
	internal class MediaBookmarkList : OpenXmlCompositeElement
	{
		// Token: 0x17004CC7 RID: 19655
		// (get) Token: 0x060109A2 RID: 68002 RVA: 0x002E53BF File Offset: 0x002E35BF
		public override string LocalName
		{
			get
			{
				return "bmkLst";
			}
		}

		// Token: 0x17004CC8 RID: 19656
		// (get) Token: 0x060109A3 RID: 68003 RVA: 0x002E3C37 File Offset: 0x002E1E37
		internal override byte NamespaceId
		{
			get
			{
				return 49;
			}
		}

		// Token: 0x17004CC9 RID: 19657
		// (get) Token: 0x060109A4 RID: 68004 RVA: 0x002E53C6 File Offset: 0x002E35C6
		internal override int ElementTypeId
		{
			get
			{
				return 12805;
			}
		}

		// Token: 0x060109A5 RID: 68005 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060109A6 RID: 68006 RVA: 0x00293ECF File Offset: 0x002920CF
		public MediaBookmarkList()
		{
		}

		// Token: 0x060109A7 RID: 68007 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MediaBookmarkList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109A8 RID: 68008 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MediaBookmarkList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060109A9 RID: 68009 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MediaBookmarkList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060109AA RID: 68010 RVA: 0x002E53CD File Offset: 0x002E35CD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (49 == namespaceId && "bmk" == name)
			{
				return new MediaBookmark();
			}
			return null;
		}

		// Token: 0x060109AB RID: 68011 RVA: 0x002E53E8 File Offset: 0x002E35E8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MediaBookmarkList>(deep);
		}

		// Token: 0x04007574 RID: 30068
		private const string tagName = "bmkLst";

		// Token: 0x04007575 RID: 30069
		private const byte tagNsId = 49;

		// Token: 0x04007576 RID: 30070
		internal const int ElementTypeIdConst = 12805;
	}
}

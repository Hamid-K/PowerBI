using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A74 RID: 10868
	[GeneratedCode("DomGen", "2.0")]
	internal class OtherStyle : TextListStyleType
	{
		// Token: 0x17007313 RID: 29459
		// (get) Token: 0x0601600F RID: 90127 RVA: 0x00325A71 File Offset: 0x00323C71
		public override string LocalName
		{
			get
			{
				return "otherStyle";
			}
		}

		// Token: 0x17007314 RID: 29460
		// (get) Token: 0x06016010 RID: 90128 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007315 RID: 29461
		// (get) Token: 0x06016011 RID: 90129 RVA: 0x00325A78 File Offset: 0x00323C78
		internal override int ElementTypeId
		{
			get
			{
				return 12285;
			}
		}

		// Token: 0x06016012 RID: 90130 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016013 RID: 90131 RVA: 0x00325A2E File Offset: 0x00323C2E
		public OtherStyle()
		{
		}

		// Token: 0x06016014 RID: 90132 RVA: 0x00325A36 File Offset: 0x00323C36
		public OtherStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016015 RID: 90133 RVA: 0x00325A3F File Offset: 0x00323C3F
		public OtherStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016016 RID: 90134 RVA: 0x00325A48 File Offset: 0x00323C48
		public OtherStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016017 RID: 90135 RVA: 0x00325A7F File Offset: 0x00323C7F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OtherStyle>(deep);
		}

		// Token: 0x040095BF RID: 38335
		private const string tagName = "otherStyle";

		// Token: 0x040095C0 RID: 38336
		private const byte tagNsId = 24;

		// Token: 0x040095C1 RID: 38337
		internal const int ElementTypeIdConst = 12285;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A75 RID: 10869
	[GeneratedCode("DomGen", "2.0")]
	internal class NotesStyle : TextListStyleType
	{
		// Token: 0x17007316 RID: 29462
		// (get) Token: 0x06016018 RID: 90136 RVA: 0x00325A88 File Offset: 0x00323C88
		public override string LocalName
		{
			get
			{
				return "notesStyle";
			}
		}

		// Token: 0x17007317 RID: 29463
		// (get) Token: 0x06016019 RID: 90137 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007318 RID: 29464
		// (get) Token: 0x0601601A RID: 90138 RVA: 0x00325A8F File Offset: 0x00323C8F
		internal override int ElementTypeId
		{
			get
			{
				return 12289;
			}
		}

		// Token: 0x0601601B RID: 90139 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601601C RID: 90140 RVA: 0x00325A2E File Offset: 0x00323C2E
		public NotesStyle()
		{
		}

		// Token: 0x0601601D RID: 90141 RVA: 0x00325A36 File Offset: 0x00323C36
		public NotesStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601601E RID: 90142 RVA: 0x00325A3F File Offset: 0x00323C3F
		public NotesStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601601F RID: 90143 RVA: 0x00325A48 File Offset: 0x00323C48
		public NotesStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016020 RID: 90144 RVA: 0x00325A96 File Offset: 0x00323C96
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotesStyle>(deep);
		}

		// Token: 0x040095C2 RID: 38338
		private const string tagName = "notesStyle";

		// Token: 0x040095C3 RID: 38339
		private const byte tagNsId = 24;

		// Token: 0x040095C4 RID: 38340
		internal const int ElementTypeIdConst = 12289;
	}
}

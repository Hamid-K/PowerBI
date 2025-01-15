using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002813 RID: 10259
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphProperties : TextParagraphPropertiesType
	{
		// Token: 0x17006578 RID: 25976
		// (get) Token: 0x06014121 RID: 82209 RVA: 0x0030F000 File Offset: 0x0030D200
		public override string LocalName
		{
			get
			{
				return "pPr";
			}
		}

		// Token: 0x17006579 RID: 25977
		// (get) Token: 0x06014122 RID: 82210 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700657A RID: 25978
		// (get) Token: 0x06014123 RID: 82211 RVA: 0x0030F007 File Offset: 0x0030D207
		internal override int ElementTypeId
		{
			get
			{
				return 10294;
			}
		}

		// Token: 0x06014124 RID: 82212 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014125 RID: 82213 RVA: 0x0030F00E File Offset: 0x0030D20E
		public ParagraphProperties()
		{
		}

		// Token: 0x06014126 RID: 82214 RVA: 0x0030F016 File Offset: 0x0030D216
		public ParagraphProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014127 RID: 82215 RVA: 0x0030F01F File Offset: 0x0030D21F
		public ParagraphProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014128 RID: 82216 RVA: 0x0030F028 File Offset: 0x0030D228
		public ParagraphProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014129 RID: 82217 RVA: 0x0030F031 File Offset: 0x0030D231
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphProperties>(deep);
		}

		// Token: 0x040088E3 RID: 35043
		private const string tagName = "pPr";

		// Token: 0x040088E4 RID: 35044
		private const byte tagNsId = 10;

		// Token: 0x040088E5 RID: 35045
		internal const int ElementTypeIdConst = 10294;
	}
}

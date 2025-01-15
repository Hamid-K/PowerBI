using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A48 RID: 10824
	[GeneratedCode("DomGen", "2.0")]
	internal class ToColor : Color3Type
	{
		// Token: 0x1700718E RID: 29070
		// (get) Token: 0x06015CC5 RID: 89285 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x1700718F RID: 29071
		// (get) Token: 0x06015CC6 RID: 89286 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007190 RID: 29072
		// (get) Token: 0x06015CC7 RID: 89287 RVA: 0x00323423 File Offset: 0x00321623
		internal override int ElementTypeId
		{
			get
			{
				return 12242;
			}
		}

		// Token: 0x06015CC8 RID: 89288 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015CC9 RID: 89289 RVA: 0x003233F7 File Offset: 0x003215F7
		public ToColor()
		{
		}

		// Token: 0x06015CCA RID: 89290 RVA: 0x003233FF File Offset: 0x003215FF
		public ToColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CCB RID: 89291 RVA: 0x00323408 File Offset: 0x00321608
		public ToColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CCC RID: 89292 RVA: 0x00323411 File Offset: 0x00321611
		public ToColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015CCD RID: 89293 RVA: 0x0032342A File Offset: 0x0032162A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToColor>(deep);
		}

		// Token: 0x040094D9 RID: 38105
		private const string tagName = "to";

		// Token: 0x040094DA RID: 38106
		private const byte tagNsId = 24;

		// Token: 0x040094DB RID: 38107
		internal const int ElementTypeIdConst = 12242;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A47 RID: 10823
	[GeneratedCode("DomGen", "2.0")]
	internal class FromColor : Color3Type
	{
		// Token: 0x1700718B RID: 29067
		// (get) Token: 0x06015CBC RID: 89276 RVA: 0x002FCA49 File Offset: 0x002FAC49
		public override string LocalName
		{
			get
			{
				return "from";
			}
		}

		// Token: 0x1700718C RID: 29068
		// (get) Token: 0x06015CBD RID: 89277 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700718D RID: 29069
		// (get) Token: 0x06015CBE RID: 89278 RVA: 0x003233F0 File Offset: 0x003215F0
		internal override int ElementTypeId
		{
			get
			{
				return 12241;
			}
		}

		// Token: 0x06015CBF RID: 89279 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015CC0 RID: 89280 RVA: 0x003233F7 File Offset: 0x003215F7
		public FromColor()
		{
		}

		// Token: 0x06015CC1 RID: 89281 RVA: 0x003233FF File Offset: 0x003215FF
		public FromColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CC2 RID: 89282 RVA: 0x00323408 File Offset: 0x00321608
		public FromColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CC3 RID: 89283 RVA: 0x00323411 File Offset: 0x00321611
		public FromColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015CC4 RID: 89284 RVA: 0x0032341A File Offset: 0x0032161A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FromColor>(deep);
		}

		// Token: 0x040094D6 RID: 38102
		private const string tagName = "from";

		// Token: 0x040094D7 RID: 38103
		private const byte tagNsId = 24;

		// Token: 0x040094D8 RID: 38104
		internal const int ElementTypeIdConst = 12241;
	}
}

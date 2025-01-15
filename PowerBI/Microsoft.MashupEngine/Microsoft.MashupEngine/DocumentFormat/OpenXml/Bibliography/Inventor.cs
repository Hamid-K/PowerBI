using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028FA RID: 10490
	[GeneratedCode("DomGen", "2.0")]
	internal class Inventor : NameType
	{
		// Token: 0x17006997 RID: 27031
		// (get) Token: 0x06014AD3 RID: 84691 RVA: 0x003154D1 File Offset: 0x003136D1
		public override string LocalName
		{
			get
			{
				return "Inventor";
			}
		}

		// Token: 0x17006998 RID: 27032
		// (get) Token: 0x06014AD4 RID: 84692 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006999 RID: 27033
		// (get) Token: 0x06014AD5 RID: 84693 RVA: 0x003154D8 File Offset: 0x003136D8
		internal override int ElementTypeId
		{
			get
			{
				return 10776;
			}
		}

		// Token: 0x06014AD6 RID: 84694 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014AD7 RID: 84695 RVA: 0x003153D6 File Offset: 0x003135D6
		public Inventor()
		{
		}

		// Token: 0x06014AD8 RID: 84696 RVA: 0x003153DE File Offset: 0x003135DE
		public Inventor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014AD9 RID: 84697 RVA: 0x003153E7 File Offset: 0x003135E7
		public Inventor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014ADA RID: 84698 RVA: 0x003153F0 File Offset: 0x003135F0
		public Inventor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014ADB RID: 84699 RVA: 0x003154DF File Offset: 0x003136DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Inventor>(deep);
		}

		// Token: 0x04008F79 RID: 36729
		private const string tagName = "Inventor";

		// Token: 0x04008F7A RID: 36730
		private const byte tagNsId = 9;

		// Token: 0x04008F7B RID: 36731
		internal const int ElementTypeIdConst = 10776;
	}
}

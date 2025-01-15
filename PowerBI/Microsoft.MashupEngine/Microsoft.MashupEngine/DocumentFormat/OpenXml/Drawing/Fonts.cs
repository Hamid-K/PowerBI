using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002735 RID: 10037
	[GeneratedCode("DomGen", "2.0")]
	internal class Fonts : FontCollectionType
	{
		// Token: 0x17006027 RID: 24615
		// (get) Token: 0x060134D9 RID: 79065 RVA: 0x002AD88F File Offset: 0x002ABA8F
		public override string LocalName
		{
			get
			{
				return "font";
			}
		}

		// Token: 0x17006028 RID: 24616
		// (get) Token: 0x060134DA RID: 79066 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006029 RID: 24617
		// (get) Token: 0x060134DB RID: 79067 RVA: 0x00305ED0 File Offset: 0x003040D0
		internal override int ElementTypeId
		{
			get
			{
				return 10097;
			}
		}

		// Token: 0x060134DC RID: 79068 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134DD RID: 79069 RVA: 0x00305ED7 File Offset: 0x003040D7
		public Fonts()
		{
		}

		// Token: 0x060134DE RID: 79070 RVA: 0x00305EDF File Offset: 0x003040DF
		public Fonts(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134DF RID: 79071 RVA: 0x00305EE8 File Offset: 0x003040E8
		public Fonts(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134E0 RID: 79072 RVA: 0x00305EF1 File Offset: 0x003040F1
		public Fonts(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134E1 RID: 79073 RVA: 0x00305EFA File Offset: 0x003040FA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Fonts>(deep);
		}

		// Token: 0x04008588 RID: 34184
		private const string tagName = "font";

		// Token: 0x04008589 RID: 34185
		private const byte tagNsId = 10;

		// Token: 0x0400858A RID: 34186
		internal const int ElementTypeIdConst = 10097;
	}
}

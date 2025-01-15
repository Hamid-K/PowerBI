using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267D RID: 9853
	[GeneratedCode("DomGen", "2.0")]
	internal class StyleData : SampleDataType
	{
		// Token: 0x17005C91 RID: 23697
		// (get) Token: 0x06012D25 RID: 77093 RVA: 0x002FFCC0 File Offset: 0x002FDEC0
		public override string LocalName
		{
			get
			{
				return "styleData";
			}
		}

		// Token: 0x17005C92 RID: 23698
		// (get) Token: 0x06012D26 RID: 77094 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C93 RID: 23699
		// (get) Token: 0x06012D27 RID: 77095 RVA: 0x002FFCC7 File Offset: 0x002FDEC7
		internal override int ElementTypeId
		{
			get
			{
				return 10667;
			}
		}

		// Token: 0x06012D28 RID: 77096 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012D29 RID: 77097 RVA: 0x002FFC94 File Offset: 0x002FDE94
		public StyleData()
		{
		}

		// Token: 0x06012D2A RID: 77098 RVA: 0x002FFC9C File Offset: 0x002FDE9C
		public StyleData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D2B RID: 77099 RVA: 0x002FFCA5 File Offset: 0x002FDEA5
		public StyleData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D2C RID: 77100 RVA: 0x002FFCAE File Offset: 0x002FDEAE
		public StyleData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D2D RID: 77101 RVA: 0x002FFCCE File Offset: 0x002FDECE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleData>(deep);
		}

		// Token: 0x040081BB RID: 33211
		private const string tagName = "styleData";

		// Token: 0x040081BC RID: 33212
		private const byte tagNsId = 14;

		// Token: 0x040081BD RID: 33213
		internal const int ElementTypeIdConst = 10667;
	}
}

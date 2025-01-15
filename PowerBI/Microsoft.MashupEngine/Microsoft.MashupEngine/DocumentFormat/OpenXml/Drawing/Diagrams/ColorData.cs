using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267E RID: 9854
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorData : SampleDataType
	{
		// Token: 0x17005C94 RID: 23700
		// (get) Token: 0x06012D2E RID: 77102 RVA: 0x002FFCD7 File Offset: 0x002FDED7
		public override string LocalName
		{
			get
			{
				return "clrData";
			}
		}

		// Token: 0x17005C95 RID: 23701
		// (get) Token: 0x06012D2F RID: 77103 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C96 RID: 23702
		// (get) Token: 0x06012D30 RID: 77104 RVA: 0x002FFCDE File Offset: 0x002FDEDE
		internal override int ElementTypeId
		{
			get
			{
				return 10668;
			}
		}

		// Token: 0x06012D31 RID: 77105 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012D32 RID: 77106 RVA: 0x002FFC94 File Offset: 0x002FDE94
		public ColorData()
		{
		}

		// Token: 0x06012D33 RID: 77107 RVA: 0x002FFC9C File Offset: 0x002FDE9C
		public ColorData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D34 RID: 77108 RVA: 0x002FFCA5 File Offset: 0x002FDEA5
		public ColorData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D35 RID: 77109 RVA: 0x002FFCAE File Offset: 0x002FDEAE
		public ColorData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D36 RID: 77110 RVA: 0x002FFCE5 File Offset: 0x002FDEE5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorData>(deep);
		}

		// Token: 0x040081BE RID: 33214
		private const string tagName = "clrData";

		// Token: 0x040081BF RID: 33215
		private const byte tagNsId = 14;

		// Token: 0x040081C0 RID: 33216
		internal const int ElementTypeIdConst = 10668;
	}
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200267C RID: 9852
	[GeneratedCode("DomGen", "2.0")]
	internal class SampleData : SampleDataType
	{
		// Token: 0x17005C8E RID: 23694
		// (get) Token: 0x06012D1C RID: 77084 RVA: 0x002FFC86 File Offset: 0x002FDE86
		public override string LocalName
		{
			get
			{
				return "sampData";
			}
		}

		// Token: 0x17005C8F RID: 23695
		// (get) Token: 0x06012D1D RID: 77085 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C90 RID: 23696
		// (get) Token: 0x06012D1E RID: 77086 RVA: 0x002FFC8D File Offset: 0x002FDE8D
		internal override int ElementTypeId
		{
			get
			{
				return 10666;
			}
		}

		// Token: 0x06012D1F RID: 77087 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012D20 RID: 77088 RVA: 0x002FFC94 File Offset: 0x002FDE94
		public SampleData()
		{
		}

		// Token: 0x06012D21 RID: 77089 RVA: 0x002FFC9C File Offset: 0x002FDE9C
		public SampleData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D22 RID: 77090 RVA: 0x002FFCA5 File Offset: 0x002FDEA5
		public SampleData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012D23 RID: 77091 RVA: 0x002FFCAE File Offset: 0x002FDEAE
		public SampleData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012D24 RID: 77092 RVA: 0x002FFCB7 File Offset: 0x002FDEB7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SampleData>(deep);
		}

		// Token: 0x040081B8 RID: 33208
		private const string tagName = "sampData";

		// Token: 0x040081B9 RID: 33209
		private const byte tagNsId = 14;

		// Token: 0x040081BA RID: 33210
		internal const int ElementTypeIdConst = 10666;
	}
}

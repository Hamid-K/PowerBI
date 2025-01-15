using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024CE RID: 9422
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ContourColor : ColorType
	{
		// Token: 0x17005300 RID: 21248
		// (get) Token: 0x060117B1 RID: 71601 RVA: 0x002EEE07 File Offset: 0x002ED007
		public override string LocalName
		{
			get
			{
				return "contourClr";
			}
		}

		// Token: 0x17005301 RID: 21249
		// (get) Token: 0x060117B2 RID: 71602 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005302 RID: 21250
		// (get) Token: 0x060117B3 RID: 71603 RVA: 0x002EEE0E File Offset: 0x002ED00E
		internal override int ElementTypeId
		{
			get
			{
				return 12892;
			}
		}

		// Token: 0x060117B4 RID: 71604 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117B5 RID: 71605 RVA: 0x002EEDDB File Offset: 0x002ECFDB
		public ContourColor()
		{
		}

		// Token: 0x060117B6 RID: 71606 RVA: 0x002EEDE3 File Offset: 0x002ECFE3
		public ContourColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117B7 RID: 71607 RVA: 0x002EEDEC File Offset: 0x002ECFEC
		public ContourColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117B8 RID: 71608 RVA: 0x002EEDF5 File Offset: 0x002ECFF5
		public ContourColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060117B9 RID: 71609 RVA: 0x002EEE15 File Offset: 0x002ED015
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContourColor>(deep);
		}

		// Token: 0x04007A08 RID: 31240
		private const string tagName = "contourClr";

		// Token: 0x04007A09 RID: 31241
		private const byte tagNsId = 52;

		// Token: 0x04007A0A RID: 31242
		internal const int ElementTypeIdConst = 12892;
	}
}

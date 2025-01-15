using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F0 RID: 9456
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class WordprocessingGroup : WordprocessingGroupType
	{
		// Token: 0x1700534F RID: 21327
		// (get) Token: 0x0601186C RID: 71788 RVA: 0x002EF70E File Offset: 0x002ED90E
		public override string LocalName
		{
			get
			{
				return "wgp";
			}
		}

		// Token: 0x17005350 RID: 21328
		// (get) Token: 0x0601186D RID: 71789 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005351 RID: 21329
		// (get) Token: 0x0601186E RID: 71790 RVA: 0x002EF719 File Offset: 0x002ED919
		internal override int ElementTypeId
		{
			get
			{
				return 13122;
			}
		}

		// Token: 0x0601186F RID: 71791 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011870 RID: 71792 RVA: 0x002EF720 File Offset: 0x002ED920
		public WordprocessingGroup()
		{
		}

		// Token: 0x06011871 RID: 71793 RVA: 0x002EF728 File Offset: 0x002ED928
		public WordprocessingGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011872 RID: 71794 RVA: 0x002EF731 File Offset: 0x002ED931
		public WordprocessingGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011873 RID: 71795 RVA: 0x002EF73A File Offset: 0x002ED93A
		public WordprocessingGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011874 RID: 71796 RVA: 0x002EF743 File Offset: 0x002ED943
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WordprocessingGroup>(deep);
		}

		// Token: 0x04007B2C RID: 31532
		private const string tagName = "wgp";

		// Token: 0x04007B2D RID: 31533
		private const byte tagNsId = 60;

		// Token: 0x04007B2E RID: 31534
		internal const int ElementTypeIdConst = 13122;
	}
}

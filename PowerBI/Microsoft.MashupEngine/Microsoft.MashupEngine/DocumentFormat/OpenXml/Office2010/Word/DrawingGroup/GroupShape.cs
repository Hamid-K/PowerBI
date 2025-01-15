using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F1 RID: 9457
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class GroupShape : WordprocessingGroupType
	{
		// Token: 0x17005352 RID: 21330
		// (get) Token: 0x06011875 RID: 71797 RVA: 0x002DF94C File Offset: 0x002DDB4C
		public override string LocalName
		{
			get
			{
				return "grpSp";
			}
		}

		// Token: 0x17005353 RID: 21331
		// (get) Token: 0x06011876 RID: 71798 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005354 RID: 21332
		// (get) Token: 0x06011877 RID: 71799 RVA: 0x002EF74C File Offset: 0x002ED94C
		internal override int ElementTypeId
		{
			get
			{
				return 13129;
			}
		}

		// Token: 0x06011878 RID: 71800 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011879 RID: 71801 RVA: 0x002EF720 File Offset: 0x002ED920
		public GroupShape()
		{
		}

		// Token: 0x0601187A RID: 71802 RVA: 0x002EF728 File Offset: 0x002ED928
		public GroupShape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601187B RID: 71803 RVA: 0x002EF731 File Offset: 0x002ED931
		public GroupShape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601187C RID: 71804 RVA: 0x002EF73A File Offset: 0x002ED93A
		public GroupShape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601187D RID: 71805 RVA: 0x002EF753 File Offset: 0x002ED953
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GroupShape>(deep);
		}

		// Token: 0x04007B2F RID: 31535
		private const string tagName = "grpSp";

		// Token: 0x04007B30 RID: 31536
		private const byte tagNsId = 60;

		// Token: 0x04007B31 RID: 31537
		internal const int ElementTypeIdConst = 13129;
	}
}

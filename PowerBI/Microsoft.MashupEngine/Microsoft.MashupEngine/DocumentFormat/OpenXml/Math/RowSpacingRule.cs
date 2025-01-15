using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029A4 RID: 10660
	[GeneratedCode("DomGen", "2.0")]
	internal class RowSpacingRule : SpacingRuleType
	{
		// Token: 0x17006D31 RID: 27953
		// (get) Token: 0x06015320 RID: 86816 RVA: 0x0031CC0C File Offset: 0x0031AE0C
		public override string LocalName
		{
			get
			{
				return "rSpRule";
			}
		}

		// Token: 0x17006D32 RID: 27954
		// (get) Token: 0x06015321 RID: 86817 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D33 RID: 27955
		// (get) Token: 0x06015322 RID: 86818 RVA: 0x0031CC13 File Offset: 0x0031AE13
		internal override int ElementTypeId
		{
			get
			{
				return 10898;
			}
		}

		// Token: 0x06015323 RID: 86819 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015325 RID: 86821 RVA: 0x0031CC22 File Offset: 0x0031AE22
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RowSpacingRule>(deep);
		}

		// Token: 0x04009208 RID: 37384
		private const string tagName = "rSpRule";

		// Token: 0x04009209 RID: 37385
		private const byte tagNsId = 21;

		// Token: 0x0400920A RID: 37386
		internal const int ElementTypeIdConst = 10898;
	}
}

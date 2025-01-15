using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x02002900 RID: 10496
	[GeneratedCode("DomGen", "2.0")]
	internal class Performer : NameOrCorporateType
	{
		// Token: 0x170069AB RID: 27051
		// (get) Token: 0x06014B0D RID: 84749 RVA: 0x003155FF File Offset: 0x003137FF
		public override string LocalName
		{
			get
			{
				return "Performer";
			}
		}

		// Token: 0x170069AC RID: 27052
		// (get) Token: 0x06014B0E RID: 84750 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x170069AD RID: 27053
		// (get) Token: 0x06014B0F RID: 84751 RVA: 0x00315606 File Offset: 0x00313806
		internal override int ElementTypeId
		{
			get
			{
				return 10777;
			}
		}

		// Token: 0x06014B10 RID: 84752 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014B11 RID: 84753 RVA: 0x003155D3 File Offset: 0x003137D3
		public Performer()
		{
		}

		// Token: 0x06014B12 RID: 84754 RVA: 0x003155DB File Offset: 0x003137DB
		public Performer(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B13 RID: 84755 RVA: 0x003155E4 File Offset: 0x003137E4
		public Performer(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014B14 RID: 84756 RVA: 0x003155ED File Offset: 0x003137ED
		public Performer(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014B15 RID: 84757 RVA: 0x0031560D File Offset: 0x0031380D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Performer>(deep);
		}

		// Token: 0x04008F8A RID: 36746
		private const string tagName = "Performer";

		// Token: 0x04008F8B RID: 36747
		private const byte tagNsId = 9;

		// Token: 0x04008F8C RID: 36748
		internal const int ElementTypeIdConst = 10777;
	}
}

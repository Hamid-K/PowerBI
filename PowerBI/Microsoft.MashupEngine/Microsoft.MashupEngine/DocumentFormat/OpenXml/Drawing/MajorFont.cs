using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002736 RID: 10038
	[GeneratedCode("DomGen", "2.0")]
	internal class MajorFont : FontCollectionType
	{
		// Token: 0x1700602A RID: 24618
		// (get) Token: 0x060134E2 RID: 79074 RVA: 0x00305F03 File Offset: 0x00304103
		public override string LocalName
		{
			get
			{
				return "majorFont";
			}
		}

		// Token: 0x1700602B RID: 24619
		// (get) Token: 0x060134E3 RID: 79075 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700602C RID: 24620
		// (get) Token: 0x060134E4 RID: 79076 RVA: 0x00305F0A File Offset: 0x0030410A
		internal override int ElementTypeId
		{
			get
			{
				return 10136;
			}
		}

		// Token: 0x060134E5 RID: 79077 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134E6 RID: 79078 RVA: 0x00305ED7 File Offset: 0x003040D7
		public MajorFont()
		{
		}

		// Token: 0x060134E7 RID: 79079 RVA: 0x00305EDF File Offset: 0x003040DF
		public MajorFont(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134E8 RID: 79080 RVA: 0x00305EE8 File Offset: 0x003040E8
		public MajorFont(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134E9 RID: 79081 RVA: 0x00305EF1 File Offset: 0x003040F1
		public MajorFont(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134EA RID: 79082 RVA: 0x00305F11 File Offset: 0x00304111
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MajorFont>(deep);
		}

		// Token: 0x0400858B RID: 34187
		private const string tagName = "majorFont";

		// Token: 0x0400858C RID: 34188
		private const byte tagNsId = 10;

		// Token: 0x0400858D RID: 34189
		internal const int ElementTypeIdConst = 10136;
	}
}

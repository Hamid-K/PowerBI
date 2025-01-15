using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002737 RID: 10039
	[GeneratedCode("DomGen", "2.0")]
	internal class MinorFont : FontCollectionType
	{
		// Token: 0x1700602D RID: 24621
		// (get) Token: 0x060134EB RID: 79083 RVA: 0x00305F1A File Offset: 0x0030411A
		public override string LocalName
		{
			get
			{
				return "minorFont";
			}
		}

		// Token: 0x1700602E RID: 24622
		// (get) Token: 0x060134EC RID: 79084 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700602F RID: 24623
		// (get) Token: 0x060134ED RID: 79085 RVA: 0x00305F21 File Offset: 0x00304121
		internal override int ElementTypeId
		{
			get
			{
				return 10137;
			}
		}

		// Token: 0x060134EE RID: 79086 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060134EF RID: 79087 RVA: 0x00305ED7 File Offset: 0x003040D7
		public MinorFont()
		{
		}

		// Token: 0x060134F0 RID: 79088 RVA: 0x00305EDF File Offset: 0x003040DF
		public MinorFont(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134F1 RID: 79089 RVA: 0x00305EE8 File Offset: 0x003040E8
		public MinorFont(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060134F2 RID: 79090 RVA: 0x00305EF1 File Offset: 0x003040F1
		public MinorFont(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060134F3 RID: 79091 RVA: 0x00305F28 File Offset: 0x00304128
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MinorFont>(deep);
		}

		// Token: 0x0400858E RID: 34190
		private const string tagName = "minorFont";

		// Token: 0x0400858F RID: 34191
		private const byte tagNsId = 10;

		// Token: 0x04008590 RID: 34192
		internal const int ElementTypeIdConst = 10137;
	}
}

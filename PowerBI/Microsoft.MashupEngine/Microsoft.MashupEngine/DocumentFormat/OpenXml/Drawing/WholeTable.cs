using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002805 RID: 10245
	[GeneratedCode("DomGen", "2.0")]
	internal class WholeTable : TablePartStyleType
	{
		// Token: 0x1700653E RID: 25918
		// (get) Token: 0x06014084 RID: 82052 RVA: 0x0030EA55 File Offset: 0x0030CC55
		public override string LocalName
		{
			get
			{
				return "wholeTbl";
			}
		}

		// Token: 0x1700653F RID: 25919
		// (get) Token: 0x06014085 RID: 82053 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006540 RID: 25920
		// (get) Token: 0x06014086 RID: 82054 RVA: 0x0030EA5C File Offset: 0x0030CC5C
		internal override int ElementTypeId
		{
			get
			{
				return 10280;
			}
		}

		// Token: 0x06014087 RID: 82055 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014088 RID: 82056 RVA: 0x0030EA63 File Offset: 0x0030CC63
		public WholeTable()
		{
		}

		// Token: 0x06014089 RID: 82057 RVA: 0x0030EA6B File Offset: 0x0030CC6B
		public WholeTable(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601408A RID: 82058 RVA: 0x0030EA74 File Offset: 0x0030CC74
		public WholeTable(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601408B RID: 82059 RVA: 0x0030EA7D File Offset: 0x0030CC7D
		public WholeTable(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601408C RID: 82060 RVA: 0x0030EA86 File Offset: 0x0030CC86
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WholeTable>(deep);
		}

		// Token: 0x040088B8 RID: 35000
		private const string tagName = "wholeTbl";

		// Token: 0x040088B9 RID: 35001
		private const byte tagNsId = 10;

		// Token: 0x040088BA RID: 35002
		internal const int ElementTypeIdConst = 10280;
	}
}

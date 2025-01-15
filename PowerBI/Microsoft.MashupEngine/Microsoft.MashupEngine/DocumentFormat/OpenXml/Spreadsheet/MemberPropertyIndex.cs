using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B5E RID: 11102
	[GeneratedCode("DomGen", "2.0")]
	internal class MemberPropertyIndex : XType
	{
		// Token: 0x170078CA RID: 30922
		// (get) Token: 0x06016D2F RID: 93487 RVA: 0x002F2EEE File Offset: 0x002F10EE
		public override string LocalName
		{
			get
			{
				return "x";
			}
		}

		// Token: 0x170078CB RID: 30923
		// (get) Token: 0x06016D30 RID: 93488 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170078CC RID: 30924
		// (get) Token: 0x06016D31 RID: 93489 RVA: 0x0032F7E3 File Offset: 0x0032D9E3
		internal override int ElementTypeId
		{
			get
			{
				return 11082;
			}
		}

		// Token: 0x06016D32 RID: 93490 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016D34 RID: 93492 RVA: 0x0032F7F2 File Offset: 0x0032D9F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MemberPropertyIndex>(deep);
		}

		// Token: 0x04009A0C RID: 39436
		private const string tagName = "x";

		// Token: 0x04009A0D RID: 39437
		private const byte tagNsId = 22;

		// Token: 0x04009A0E RID: 39438
		internal const int ElementTypeIdConst = 11082;
	}
}

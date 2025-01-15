using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Vml.Wordprocessing
{
	// Token: 0x02002239 RID: 8761
	[GeneratedCode("DomGen", "2.0")]
	internal class RightBorder : BorderType
	{
		// Token: 0x17003969 RID: 14697
		// (get) Token: 0x0600E082 RID: 57474 RVA: 0x002BFE59 File Offset: 0x002BE059
		public override string LocalName
		{
			get
			{
				return "borderright";
			}
		}

		// Token: 0x1700396A RID: 14698
		// (get) Token: 0x0600E083 RID: 57475 RVA: 0x002BFE26 File Offset: 0x002BE026
		internal override byte NamespaceId
		{
			get
			{
				return 28;
			}
		}

		// Token: 0x1700396B RID: 14699
		// (get) Token: 0x0600E084 RID: 57476 RVA: 0x002BFE60 File Offset: 0x002BE060
		internal override int ElementTypeId
		{
			get
			{
				return 12432;
			}
		}

		// Token: 0x0600E085 RID: 57477 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0600E087 RID: 57479 RVA: 0x002BFE67 File Offset: 0x002BE067
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightBorder>(deep);
		}

		// Token: 0x04006E53 RID: 28243
		private const string tagName = "borderright";

		// Token: 0x04006E54 RID: 28244
		private const byte tagNsId = 28;

		// Token: 0x04006E55 RID: 28245
		internal const int ElementTypeIdConst = 12432;
	}
}

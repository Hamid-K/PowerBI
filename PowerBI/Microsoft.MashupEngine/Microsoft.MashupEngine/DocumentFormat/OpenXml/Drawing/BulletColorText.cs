using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273C RID: 10044
	[GeneratedCode("DomGen", "2.0")]
	internal class BulletColorText : OpenXmlLeafElement
	{
		// Token: 0x1700604C RID: 24652
		// (get) Token: 0x0601352D RID: 79149 RVA: 0x00306176 File Offset: 0x00304376
		public override string LocalName
		{
			get
			{
				return "buClrTx";
			}
		}

		// Token: 0x1700604D RID: 24653
		// (get) Token: 0x0601352E RID: 79150 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700604E RID: 24654
		// (get) Token: 0x0601352F RID: 79151 RVA: 0x0030617D File Offset: 0x0030437D
		internal override int ElementTypeId
		{
			get
			{
				return 10102;
			}
		}

		// Token: 0x06013530 RID: 79152 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013532 RID: 79154 RVA: 0x00306184 File Offset: 0x00304384
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BulletColorText>(deep);
		}

		// Token: 0x040085A3 RID: 34211
		private const string tagName = "buClrTx";

		// Token: 0x040085A4 RID: 34212
		private const byte tagNsId = 10;

		// Token: 0x040085A5 RID: 34213
		internal const int ElementTypeIdConst = 10102;
	}
}

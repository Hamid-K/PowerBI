using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D6A RID: 11626
	[GeneratedCode("DomGen", "2.0")]
	internal class KeepNext : OnOffType
	{
		// Token: 0x170086DA RID: 34522
		// (get) Token: 0x06018C74 RID: 101492 RVA: 0x003449D8 File Offset: 0x00342BD8
		public override string LocalName
		{
			get
			{
				return "keepNext";
			}
		}

		// Token: 0x170086DB RID: 34523
		// (get) Token: 0x06018C75 RID: 101493 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086DC RID: 34524
		// (get) Token: 0x06018C76 RID: 101494 RVA: 0x003449DF File Offset: 0x00342BDF
		internal override int ElementTypeId
		{
			get
			{
				return 11493;
			}
		}

		// Token: 0x06018C77 RID: 101495 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C79 RID: 101497 RVA: 0x003449EE File Offset: 0x00342BEE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<KeepNext>(deep);
		}

		// Token: 0x0400A4A9 RID: 42153
		private const string tagName = "keepNext";

		// Token: 0x0400A4AA RID: 42154
		private const byte tagNsId = 23;

		// Token: 0x0400A4AB RID: 42155
		internal const int ElementTypeIdConst = 11493;
	}
}

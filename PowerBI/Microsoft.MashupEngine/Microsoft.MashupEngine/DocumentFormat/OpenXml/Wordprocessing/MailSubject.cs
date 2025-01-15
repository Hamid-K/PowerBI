using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D60 RID: 11616
	[GeneratedCode("DomGen", "2.0")]
	internal class MailSubject : StringType
	{
		// Token: 0x170086BC RID: 34492
		// (get) Token: 0x06018C37 RID: 101431 RVA: 0x003448B8 File Offset: 0x00342AB8
		public override string LocalName
		{
			get
			{
				return "mailSubject";
			}
		}

		// Token: 0x170086BD RID: 34493
		// (get) Token: 0x06018C38 RID: 101432 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086BE RID: 34494
		// (get) Token: 0x06018C39 RID: 101433 RVA: 0x003448BF File Offset: 0x00342ABF
		internal override int ElementTypeId
		{
			get
			{
				return 11822;
			}
		}

		// Token: 0x06018C3A RID: 101434 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C3C RID: 101436 RVA: 0x003448C6 File Offset: 0x00342AC6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MailSubject>(deep);
		}

		// Token: 0x0400A48C RID: 42124
		private const string tagName = "mailSubject";

		// Token: 0x0400A48D RID: 42125
		private const byte tagNsId = 23;

		// Token: 0x0400A48E RID: 42126
		internal const int ElementTypeIdConst = 11822;
	}
}

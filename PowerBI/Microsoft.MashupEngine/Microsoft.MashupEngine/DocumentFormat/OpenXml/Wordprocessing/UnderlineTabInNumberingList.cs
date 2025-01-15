using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E1A RID: 11802
	[GeneratedCode("DomGen", "2.0")]
	internal class UnderlineTabInNumberingList : OnOffType
	{
		// Token: 0x170088EA RID: 35050
		// (get) Token: 0x06019094 RID: 102548 RVA: 0x00345963 File Offset: 0x00343B63
		public override string LocalName
		{
			get
			{
				return "underlineTabInNumList";
			}
		}

		// Token: 0x170088EB RID: 35051
		// (get) Token: 0x06019095 RID: 102549 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088EC RID: 35052
		// (get) Token: 0x06019096 RID: 102550 RVA: 0x0034596A File Offset: 0x00343B6A
		internal override int ElementTypeId
		{
			get
			{
				return 12112;
			}
		}

		// Token: 0x06019097 RID: 102551 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019099 RID: 102553 RVA: 0x00345971 File Offset: 0x00343B71
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UnderlineTabInNumberingList>(deep);
		}

		// Token: 0x0400A6B9 RID: 42681
		private const string tagName = "underlineTabInNumList";

		// Token: 0x0400A6BA RID: 42682
		private const byte tagNsId = 23;

		// Token: 0x0400A6BB RID: 42683
		internal const int ElementTypeIdConst = 12112;
	}
}

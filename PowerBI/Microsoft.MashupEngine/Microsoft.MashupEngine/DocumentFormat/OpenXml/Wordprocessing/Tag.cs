using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D68 RID: 11624
	[GeneratedCode("DomGen", "2.0")]
	internal class Tag : StringType
	{
		// Token: 0x170086D4 RID: 34516
		// (get) Token: 0x06018C67 RID: 101479 RVA: 0x002AC58A File Offset: 0x002AA78A
		public override string LocalName
		{
			get
			{
				return "tag";
			}
		}

		// Token: 0x170086D5 RID: 34517
		// (get) Token: 0x06018C68 RID: 101480 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086D6 RID: 34518
		// (get) Token: 0x06018C69 RID: 101481 RVA: 0x00344962 File Offset: 0x00342B62
		internal override int ElementTypeId
		{
			get
			{
				return 12146;
			}
		}

		// Token: 0x06018C6A RID: 101482 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C6C RID: 101484 RVA: 0x00344969 File Offset: 0x00342B69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Tag>(deep);
		}

		// Token: 0x0400A4A4 RID: 42148
		private const string tagName = "tag";

		// Token: 0x0400A4A5 RID: 42149
		private const byte tagNsId = 23;

		// Token: 0x0400A4A6 RID: 42150
		internal const int ElementTypeIdConst = 12146;
	}
}

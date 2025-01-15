using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D94 RID: 11668
	[GeneratedCode("DomGen", "2.0")]
	internal class Enabled : OnOffType
	{
		// Token: 0x17008758 RID: 34648
		// (get) Token: 0x06018D70 RID: 101744 RVA: 0x00344D67 File Offset: 0x00342F67
		public override string LocalName
		{
			get
			{
				return "enabled";
			}
		}

		// Token: 0x17008759 RID: 34649
		// (get) Token: 0x06018D71 RID: 101745 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700875A RID: 34650
		// (get) Token: 0x06018D72 RID: 101746 RVA: 0x00344D6E File Offset: 0x00342F6E
		internal override int ElementTypeId
		{
			get
			{
				return 11727;
			}
		}

		// Token: 0x06018D73 RID: 101747 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D75 RID: 101749 RVA: 0x00344D75 File Offset: 0x00342F75
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Enabled>(deep);
		}

		// Token: 0x0400A527 RID: 42279
		private const string tagName = "enabled";

		// Token: 0x0400A528 RID: 42280
		private const byte tagNsId = 23;

		// Token: 0x0400A529 RID: 42281
		internal const int ElementTypeIdConst = 11727;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D92 RID: 11666
	[GeneratedCode("DomGen", "2.0")]
	internal class OfficeMath : OnOffType
	{
		// Token: 0x17008752 RID: 34642
		// (get) Token: 0x06018D64 RID: 101732 RVA: 0x0031AA7F File Offset: 0x00318C7F
		public override string LocalName
		{
			get
			{
				return "oMath";
			}
		}

		// Token: 0x17008753 RID: 34643
		// (get) Token: 0x06018D65 RID: 101733 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008754 RID: 34644
		// (get) Token: 0x06018D66 RID: 101734 RVA: 0x00344D40 File Offset: 0x00342F40
		internal override int ElementTypeId
		{
			get
			{
				return 11611;
			}
		}

		// Token: 0x06018D67 RID: 101735 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D69 RID: 101737 RVA: 0x00344D47 File Offset: 0x00342F47
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeMath>(deep);
		}

		// Token: 0x0400A521 RID: 42273
		private const string tagName = "oMath";

		// Token: 0x0400A522 RID: 42274
		private const byte tagNsId = 23;

		// Token: 0x0400A523 RID: 42275
		internal const int ElementTypeIdConst = 11611;
	}
}

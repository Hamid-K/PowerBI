using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E05 RID: 11781
	[GeneratedCode("DomGen", "2.0")]
	internal class NoSpaceRaiseLower : OnOffType
	{
		// Token: 0x170088AB RID: 34987
		// (get) Token: 0x06019016 RID: 102422 RVA: 0x00345780 File Offset: 0x00343980
		public override string LocalName
		{
			get
			{
				return "noSpaceRaiseLower";
			}
		}

		// Token: 0x170088AC RID: 34988
		// (get) Token: 0x06019017 RID: 102423 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088AD RID: 34989
		// (get) Token: 0x06019018 RID: 102424 RVA: 0x00345787 File Offset: 0x00343987
		internal override int ElementTypeId
		{
			get
			{
				return 12091;
			}
		}

		// Token: 0x06019019 RID: 102425 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601901B RID: 102427 RVA: 0x0034578E File Offset: 0x0034398E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoSpaceRaiseLower>(deep);
		}

		// Token: 0x0400A67A RID: 42618
		private const string tagName = "noSpaceRaiseLower";

		// Token: 0x0400A67B RID: 42619
		private const byte tagNsId = 23;

		// Token: 0x0400A67C RID: 42620
		internal const int ElementTypeIdConst = 12091;
	}
}

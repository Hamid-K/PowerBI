using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E0B RID: 11787
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotSnapToGridInCell : OnOffType
	{
		// Token: 0x170088BD RID: 35005
		// (get) Token: 0x0601903A RID: 102458 RVA: 0x0034580A File Offset: 0x00343A0A
		public override string LocalName
		{
			get
			{
				return "doNotSnapToGridInCell";
			}
		}

		// Token: 0x170088BE RID: 35006
		// (get) Token: 0x0601903B RID: 102459 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088BF RID: 35007
		// (get) Token: 0x0601903C RID: 102460 RVA: 0x00345811 File Offset: 0x00343A11
		internal override int ElementTypeId
		{
			get
			{
				return 12097;
			}
		}

		// Token: 0x0601903D RID: 102461 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601903F RID: 102463 RVA: 0x00345818 File Offset: 0x00343A18
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotSnapToGridInCell>(deep);
		}

		// Token: 0x0400A68C RID: 42636
		private const string tagName = "doNotSnapToGridInCell";

		// Token: 0x0400A68D RID: 42637
		private const byte tagNsId = 23;

		// Token: 0x0400A68E RID: 42638
		internal const int ElementTypeIdConst = 12097;
	}
}

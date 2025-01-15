using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E11 RID: 11793
	[GeneratedCode("DomGen", "2.0")]
	internal class GrowAutofit : OnOffType
	{
		// Token: 0x170088CF RID: 35023
		// (get) Token: 0x0601905E RID: 102494 RVA: 0x00345894 File Offset: 0x00343A94
		public override string LocalName
		{
			get
			{
				return "growAutofit";
			}
		}

		// Token: 0x170088D0 RID: 35024
		// (get) Token: 0x0601905F RID: 102495 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088D1 RID: 35025
		// (get) Token: 0x06019060 RID: 102496 RVA: 0x0034589B File Offset: 0x00343A9B
		internal override int ElementTypeId
		{
			get
			{
				return 12103;
			}
		}

		// Token: 0x06019061 RID: 102497 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019063 RID: 102499 RVA: 0x003458A2 File Offset: 0x00343AA2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GrowAutofit>(deep);
		}

		// Token: 0x0400A69E RID: 42654
		private const string tagName = "growAutofit";

		// Token: 0x0400A69F RID: 42655
		private const byte tagNsId = 23;

		// Token: 0x0400A6A0 RID: 42656
		internal const int ElementTypeIdConst = 12103;
	}
}

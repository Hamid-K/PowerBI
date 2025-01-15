using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E72 RID: 11890
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtContentRichText : EmptyType
	{
		// Token: 0x17008A97 RID: 35479
		// (get) Token: 0x0601940F RID: 103439 RVA: 0x00347BE2 File Offset: 0x00345DE2
		public override string LocalName
		{
			get
			{
				return "richText";
			}
		}

		// Token: 0x17008A98 RID: 35480
		// (get) Token: 0x06019410 RID: 103440 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A99 RID: 35481
		// (get) Token: 0x06019411 RID: 103441 RVA: 0x00347BE9 File Offset: 0x00345DE9
		internal override int ElementTypeId
		{
			get
			{
				return 12154;
			}
		}

		// Token: 0x06019412 RID: 103442 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019414 RID: 103444 RVA: 0x00347BF0 File Offset: 0x00345DF0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtContentRichText>(deep);
		}

		// Token: 0x0400A7EE RID: 42990
		private const string tagName = "richText";

		// Token: 0x0400A7EF RID: 42991
		private const byte tagNsId = 23;

		// Token: 0x0400A7F0 RID: 42992
		internal const int ElementTypeIdConst = 12154;
	}
}

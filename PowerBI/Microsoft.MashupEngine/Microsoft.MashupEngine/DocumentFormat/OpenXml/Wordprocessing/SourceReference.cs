using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4F RID: 11855
	[GeneratedCode("DomGen", "2.0")]
	internal class SourceReference : RelationshipType
	{
		// Token: 0x17008A2D RID: 35373
		// (get) Token: 0x0601932F RID: 103215 RVA: 0x00347783 File Offset: 0x00345983
		public override string LocalName
		{
			get
			{
				return "src";
			}
		}

		// Token: 0x17008A2E RID: 35374
		// (get) Token: 0x06019330 RID: 103216 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A2F RID: 35375
		// (get) Token: 0x06019331 RID: 103217 RVA: 0x0034778A File Offset: 0x0034598A
		internal override int ElementTypeId
		{
			get
			{
				return 11806;
			}
		}

		// Token: 0x06019332 RID: 103218 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019334 RID: 103220 RVA: 0x00347791 File Offset: 0x00345991
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SourceReference>(deep);
		}

		// Token: 0x0400A787 RID: 42887
		private const string tagName = "src";

		// Token: 0x0400A788 RID: 42888
		private const byte tagNsId = 23;

		// Token: 0x0400A789 RID: 42889
		internal const int ElementTypeIdConst = 11806;
	}
}

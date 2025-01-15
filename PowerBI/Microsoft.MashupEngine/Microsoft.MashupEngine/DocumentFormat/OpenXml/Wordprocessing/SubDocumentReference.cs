using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E4E RID: 11854
	[GeneratedCode("DomGen", "2.0")]
	internal class SubDocumentReference : RelationshipType
	{
		// Token: 0x17008A2A RID: 35370
		// (get) Token: 0x06019329 RID: 103209 RVA: 0x0034776C File Offset: 0x0034596C
		public override string LocalName
		{
			get
			{
				return "subDoc";
			}
		}

		// Token: 0x17008A2B RID: 35371
		// (get) Token: 0x0601932A RID: 103210 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008A2C RID: 35372
		// (get) Token: 0x0601932B RID: 103211 RVA: 0x00347773 File Offset: 0x00345973
		internal override int ElementTypeId
		{
			get
			{
				return 11648;
			}
		}

		// Token: 0x0601932C RID: 103212 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601932E RID: 103214 RVA: 0x0034777A File Offset: 0x0034597A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubDocumentReference>(deep);
		}

		// Token: 0x0400A784 RID: 42884
		private const string tagName = "subDoc";

		// Token: 0x0400A785 RID: 42885
		private const byte tagNsId = 23;

		// Token: 0x0400A786 RID: 42886
		internal const int ElementTypeIdConst = 11648;
	}
}

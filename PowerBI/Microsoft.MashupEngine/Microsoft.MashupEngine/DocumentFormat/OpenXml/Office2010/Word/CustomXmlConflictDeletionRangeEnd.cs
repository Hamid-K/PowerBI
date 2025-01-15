using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024B7 RID: 9399
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlConflictDeletionRangeEnd : MarkupType
	{
		// Token: 0x17005271 RID: 21105
		// (get) Token: 0x0601166D RID: 71277 RVA: 0x002EE233 File Offset: 0x002EC433
		public override string LocalName
		{
			get
			{
				return "customXmlConflictDelRangeEnd";
			}
		}

		// Token: 0x17005272 RID: 21106
		// (get) Token: 0x0601166E RID: 71278 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005273 RID: 21107
		// (get) Token: 0x0601166F RID: 71279 RVA: 0x002EE23A File Offset: 0x002EC43A
		internal override int ElementTypeId
		{
			get
			{
				return 12871;
			}
		}

		// Token: 0x06011670 RID: 71280 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011672 RID: 71282 RVA: 0x002EE241 File Offset: 0x002EC441
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlConflictDeletionRangeEnd>(deep);
		}

		// Token: 0x040079AB RID: 31147
		private const string tagName = "customXmlConflictDelRangeEnd";

		// Token: 0x040079AC RID: 31148
		private const byte tagNsId = 52;

		// Token: 0x040079AD RID: 31149
		internal const int ElementTypeIdConst = 12871;
	}
}

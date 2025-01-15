using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002487 RID: 9351
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ConflictDeletion : TrackChangeType
	{
		// Token: 0x17005167 RID: 20839
		// (get) Token: 0x06011437 RID: 70711 RVA: 0x002EC7EE File Offset: 0x002EA9EE
		public override string LocalName
		{
			get
			{
				return "conflictDel";
			}
		}

		// Token: 0x17005168 RID: 20840
		// (get) Token: 0x06011438 RID: 70712 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005169 RID: 20841
		// (get) Token: 0x06011439 RID: 70713 RVA: 0x002EC8D4 File Offset: 0x002EAAD4
		internal override int ElementTypeId
		{
			get
			{
				return 12831;
			}
		}

		// Token: 0x0601143A RID: 70714 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601143C RID: 70716 RVA: 0x002EC8DB File Offset: 0x002EAADB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConflictDeletion>(deep);
		}

		// Token: 0x040078EE RID: 30958
		private const string tagName = "conflictDel";

		// Token: 0x040078EF RID: 30959
		private const byte tagNsId = 52;

		// Token: 0x040078F0 RID: 30960
		internal const int ElementTypeIdConst = 12831;
	}
}

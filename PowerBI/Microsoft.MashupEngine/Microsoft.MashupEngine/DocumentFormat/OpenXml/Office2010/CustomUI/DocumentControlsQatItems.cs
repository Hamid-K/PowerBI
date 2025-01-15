using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022EF RID: 8943
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class DocumentControlsQatItems : QatItemsType
	{
		// Token: 0x170046D3 RID: 18131
		// (get) Token: 0x0600FC99 RID: 64665 RVA: 0x002D0004 File Offset: 0x002CE204
		public override string LocalName
		{
			get
			{
				return "documentControls";
			}
		}

		// Token: 0x170046D4 RID: 18132
		// (get) Token: 0x0600FC9A RID: 64666 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046D5 RID: 18133
		// (get) Token: 0x0600FC9B RID: 64667 RVA: 0x002DBAED File Offset: 0x002D9CED
		internal override int ElementTypeId
		{
			get
			{
				return 13087;
			}
		}

		// Token: 0x0600FC9C RID: 64668 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FC9D RID: 64669 RVA: 0x002DBAC1 File Offset: 0x002D9CC1
		public DocumentControlsQatItems()
		{
		}

		// Token: 0x0600FC9E RID: 64670 RVA: 0x002DBAC9 File Offset: 0x002D9CC9
		public DocumentControlsQatItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC9F RID: 64671 RVA: 0x002DBAD2 File Offset: 0x002D9CD2
		public DocumentControlsQatItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FCA0 RID: 64672 RVA: 0x002DBADB File Offset: 0x002D9CDB
		public DocumentControlsQatItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FCA1 RID: 64673 RVA: 0x002DBAF4 File Offset: 0x002D9CF4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocumentControlsQatItems>(deep);
		}

		// Token: 0x040071D3 RID: 29139
		private const string tagName = "documentControls";

		// Token: 0x040071D4 RID: 29140
		private const byte tagNsId = 57;

		// Token: 0x040071D5 RID: 29141
		internal const int ElementTypeIdConst = 13087;
	}
}

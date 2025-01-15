using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022EE RID: 8942
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SharedControlsQatItems : QatItemsType
	{
		// Token: 0x170046D0 RID: 18128
		// (get) Token: 0x0600FC90 RID: 64656 RVA: 0x002CFFCA File Offset: 0x002CE1CA
		public override string LocalName
		{
			get
			{
				return "sharedControls";
			}
		}

		// Token: 0x170046D1 RID: 18129
		// (get) Token: 0x0600FC91 RID: 64657 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x170046D2 RID: 18130
		// (get) Token: 0x0600FC92 RID: 64658 RVA: 0x002DBABA File Offset: 0x002D9CBA
		internal override int ElementTypeId
		{
			get
			{
				return 13086;
			}
		}

		// Token: 0x0600FC93 RID: 64659 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FC94 RID: 64660 RVA: 0x002DBAC1 File Offset: 0x002D9CC1
		public SharedControlsQatItems()
		{
		}

		// Token: 0x0600FC95 RID: 64661 RVA: 0x002DBAC9 File Offset: 0x002D9CC9
		public SharedControlsQatItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC96 RID: 64662 RVA: 0x002DBAD2 File Offset: 0x002D9CD2
		public SharedControlsQatItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FC97 RID: 64663 RVA: 0x002DBADB File Offset: 0x002D9CDB
		public SharedControlsQatItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FC98 RID: 64664 RVA: 0x002DBAE4 File Offset: 0x002D9CE4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SharedControlsQatItems>(deep);
		}

		// Token: 0x040071D0 RID: 29136
		private const string tagName = "sharedControls";

		// Token: 0x040071D1 RID: 29137
		private const byte tagNsId = 57;

		// Token: 0x040071D2 RID: 29138
		internal const int ElementTypeIdConst = 13086;
	}
}

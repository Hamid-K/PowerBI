using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.CustomUI
{
	// Token: 0x020022FF RID: 8959
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class TopItemsGroupControls : GroupControlsType
	{
		// Token: 0x1700475C RID: 18268
		// (get) Token: 0x0600FDD4 RID: 64980 RVA: 0x002DCAAA File Offset: 0x002DACAA
		public override string LocalName
		{
			get
			{
				return "topItems";
			}
		}

		// Token: 0x1700475D RID: 18269
		// (get) Token: 0x0600FDD5 RID: 64981 RVA: 0x002D1769 File Offset: 0x002CF969
		internal override byte NamespaceId
		{
			get
			{
				return 57;
			}
		}

		// Token: 0x1700475E RID: 18270
		// (get) Token: 0x0600FDD6 RID: 64982 RVA: 0x002DCAB1 File Offset: 0x002DACB1
		internal override int ElementTypeId
		{
			get
			{
				return 13101;
			}
		}

		// Token: 0x0600FDD7 RID: 64983 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FDD8 RID: 64984 RVA: 0x002DCAB8 File Offset: 0x002DACB8
		public TopItemsGroupControls()
		{
		}

		// Token: 0x0600FDD9 RID: 64985 RVA: 0x002DCAC0 File Offset: 0x002DACC0
		public TopItemsGroupControls(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDDA RID: 64986 RVA: 0x002DCAC9 File Offset: 0x002DACC9
		public TopItemsGroupControls(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FDDB RID: 64987 RVA: 0x002DCAD2 File Offset: 0x002DACD2
		public TopItemsGroupControls(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FDDC RID: 64988 RVA: 0x002DCADB File Offset: 0x002DACDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopItemsGroupControls>(deep);
		}

		// Token: 0x04007211 RID: 29201
		private const string tagName = "topItems";

		// Token: 0x04007212 RID: 29202
		private const byte tagNsId = 57;

		// Token: 0x04007213 RID: 29203
		internal const int ElementTypeIdConst = 13101;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DCD RID: 11725
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotUseMarginsForDrawingGridOrigin : OnOffType
	{
		// Token: 0x17008803 RID: 34819
		// (get) Token: 0x06018EC6 RID: 102086 RVA: 0x00345278 File Offset: 0x00343478
		public override string LocalName
		{
			get
			{
				return "doNotUseMarginsForDrawingGridOrigin";
			}
		}

		// Token: 0x17008804 RID: 34820
		// (get) Token: 0x06018EC7 RID: 102087 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008805 RID: 34821
		// (get) Token: 0x06018EC8 RID: 102088 RVA: 0x0034527F File Offset: 0x0034347F
		internal override int ElementTypeId
		{
			get
			{
				return 12013;
			}
		}

		// Token: 0x06018EC9 RID: 102089 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018ECB RID: 102091 RVA: 0x00345286 File Offset: 0x00343486
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotUseMarginsForDrawingGridOrigin>(deep);
		}

		// Token: 0x0400A5D2 RID: 42450
		private const string tagName = "doNotUseMarginsForDrawingGridOrigin";

		// Token: 0x0400A5D3 RID: 42451
		private const byte tagNsId = 23;

		// Token: 0x0400A5D4 RID: 42452
		internal const int ElementTypeIdConst = 12013;
	}
}

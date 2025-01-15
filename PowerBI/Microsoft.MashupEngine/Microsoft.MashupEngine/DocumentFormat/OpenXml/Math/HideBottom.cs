using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002970 RID: 10608
	[GeneratedCode("DomGen", "2.0")]
	internal class HideBottom : OnOffType
	{
		// Token: 0x17006C5B RID: 27739
		// (get) Token: 0x06015144 RID: 86340 RVA: 0x0031B475 File Offset: 0x00319675
		public override string LocalName
		{
			get
			{
				return "hideBot";
			}
		}

		// Token: 0x17006C5C RID: 27740
		// (get) Token: 0x06015145 RID: 86341 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C5D RID: 27741
		// (get) Token: 0x06015146 RID: 86342 RVA: 0x0031B47C File Offset: 0x0031967C
		internal override int ElementTypeId
		{
			get
			{
				return 10881;
			}
		}

		// Token: 0x06015147 RID: 86343 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015149 RID: 86345 RVA: 0x0031B483 File Offset: 0x00319683
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<HideBottom>(deep);
		}

		// Token: 0x0400915C RID: 37212
		private const string tagName = "hideBot";

		// Token: 0x0400915D RID: 37213
		private const byte tagNsId = 21;

		// Token: 0x0400915E RID: 37214
		internal const int ElementTypeIdConst = 10881;
	}
}

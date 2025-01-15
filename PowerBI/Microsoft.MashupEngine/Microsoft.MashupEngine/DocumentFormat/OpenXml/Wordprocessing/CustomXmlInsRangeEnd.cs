using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D4B RID: 11595
	[GeneratedCode("DomGen", "2.0")]
	internal class CustomXmlInsRangeEnd : MarkupType
	{
		// Token: 0x1700867D RID: 34429
		// (get) Token: 0x06018BB8 RID: 101304 RVA: 0x0034468C File Offset: 0x0034288C
		public override string LocalName
		{
			get
			{
				return "customXmlInsRangeEnd";
			}
		}

		// Token: 0x1700867E RID: 34430
		// (get) Token: 0x06018BB9 RID: 101305 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700867F RID: 34431
		// (get) Token: 0x06018BBA RID: 101306 RVA: 0x00344693 File Offset: 0x00342893
		internal override int ElementTypeId
		{
			get
			{
				return 11485;
			}
		}

		// Token: 0x06018BBB RID: 101307 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BBD RID: 101309 RVA: 0x003446A2 File Offset: 0x003428A2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlInsRangeEnd>(deep);
		}

		// Token: 0x0400A44E RID: 42062
		private const string tagName = "customXmlInsRangeEnd";

		// Token: 0x0400A44F RID: 42063
		private const byte tagNsId = 23;

		// Token: 0x0400A450 RID: 42064
		internal const int ElementTypeIdConst = 11485;
	}
}

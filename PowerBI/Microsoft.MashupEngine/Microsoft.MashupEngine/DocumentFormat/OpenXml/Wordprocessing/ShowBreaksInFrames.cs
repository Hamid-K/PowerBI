using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF1 RID: 11761
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowBreaksInFrames : OnOffType
	{
		// Token: 0x1700886F RID: 34927
		// (get) Token: 0x06018F9E RID: 102302 RVA: 0x003455B4 File Offset: 0x003437B4
		public override string LocalName
		{
			get
			{
				return "showBreaksInFrames";
			}
		}

		// Token: 0x17008870 RID: 34928
		// (get) Token: 0x06018F9F RID: 102303 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008871 RID: 34929
		// (get) Token: 0x06018FA0 RID: 102304 RVA: 0x003455BB File Offset: 0x003437BB
		internal override int ElementTypeId
		{
			get
			{
				return 12071;
			}
		}

		// Token: 0x06018FA1 RID: 102305 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FA3 RID: 102307 RVA: 0x003455C2 File Offset: 0x003437C2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowBreaksInFrames>(deep);
		}

		// Token: 0x0400A63E RID: 42558
		private const string tagName = "showBreaksInFrames";

		// Token: 0x0400A63F RID: 42559
		private const byte tagNsId = 23;

		// Token: 0x0400A640 RID: 42560
		internal const int ElementTypeIdConst = 12071;
	}
}

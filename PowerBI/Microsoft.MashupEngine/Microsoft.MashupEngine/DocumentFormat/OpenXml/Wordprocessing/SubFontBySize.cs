using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF2 RID: 11762
	[GeneratedCode("DomGen", "2.0")]
	internal class SubFontBySize : OnOffType
	{
		// Token: 0x17008872 RID: 34930
		// (get) Token: 0x06018FA4 RID: 102308 RVA: 0x003455CB File Offset: 0x003437CB
		public override string LocalName
		{
			get
			{
				return "subFontBySize";
			}
		}

		// Token: 0x17008873 RID: 34931
		// (get) Token: 0x06018FA5 RID: 102309 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008874 RID: 34932
		// (get) Token: 0x06018FA6 RID: 102310 RVA: 0x003455D2 File Offset: 0x003437D2
		internal override int ElementTypeId
		{
			get
			{
				return 12072;
			}
		}

		// Token: 0x06018FA7 RID: 102311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FA9 RID: 102313 RVA: 0x003455D9 File Offset: 0x003437D9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubFontBySize>(deep);
		}

		// Token: 0x0400A641 RID: 42561
		private const string tagName = "subFontBySize";

		// Token: 0x0400A642 RID: 42562
		private const byte tagNsId = 23;

		// Token: 0x0400A643 RID: 42563
		internal const int ElementTypeIdConst = 12072;
	}
}

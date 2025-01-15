using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBC RID: 11708
	[GeneratedCode("DomGen", "2.0")]
	internal class GutterAtTop : OnOffType
	{
		// Token: 0x170087D0 RID: 34768
		// (get) Token: 0x06018E60 RID: 101984 RVA: 0x003450F1 File Offset: 0x003432F1
		public override string LocalName
		{
			get
			{
				return "gutterAtTop";
			}
		}

		// Token: 0x170087D1 RID: 34769
		// (get) Token: 0x06018E61 RID: 101985 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087D2 RID: 34770
		// (get) Token: 0x06018E62 RID: 101986 RVA: 0x003450F8 File Offset: 0x003432F8
		internal override int ElementTypeId
		{
			get
			{
				return 11976;
			}
		}

		// Token: 0x06018E63 RID: 101987 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E65 RID: 101989 RVA: 0x003450FF File Offset: 0x003432FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GutterAtTop>(deep);
		}

		// Token: 0x0400A59F RID: 42399
		private const string tagName = "gutterAtTop";

		// Token: 0x0400A5A0 RID: 42400
		private const byte tagNsId = 23;

		// Token: 0x0400A5A1 RID: 42401
		internal const int ElementTypeIdConst = 11976;
	}
}

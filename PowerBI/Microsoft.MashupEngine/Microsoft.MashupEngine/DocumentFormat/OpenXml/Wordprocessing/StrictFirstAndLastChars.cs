using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD1 RID: 11729
	[GeneratedCode("DomGen", "2.0")]
	internal class StrictFirstAndLastChars : OnOffType
	{
		// Token: 0x1700880F RID: 34831
		// (get) Token: 0x06018EDE RID: 102110 RVA: 0x003452D4 File Offset: 0x003434D4
		public override string LocalName
		{
			get
			{
				return "strictFirstAndLastChars";
			}
		}

		// Token: 0x17008810 RID: 34832
		// (get) Token: 0x06018EDF RID: 102111 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008811 RID: 34833
		// (get) Token: 0x06018EE0 RID: 102112 RVA: 0x003452DB File Offset: 0x003434DB
		internal override int ElementTypeId
		{
			get
			{
				return 12020;
			}
		}

		// Token: 0x06018EE1 RID: 102113 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EE3 RID: 102115 RVA: 0x003452E2 File Offset: 0x003434E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StrictFirstAndLastChars>(deep);
		}

		// Token: 0x0400A5DE RID: 42462
		private const string tagName = "strictFirstAndLastChars";

		// Token: 0x0400A5DF RID: 42463
		private const byte tagNsId = 23;

		// Token: 0x0400A5E0 RID: 42464
		internal const int ElementTypeIdConst = 12020;
	}
}

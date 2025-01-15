using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF4 RID: 12020
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoRedefine : OnOffOnlyType
	{
		// Token: 0x17008D90 RID: 36240
		// (get) Token: 0x06019A70 RID: 105072 RVA: 0x0035394E File Offset: 0x00351B4E
		public override string LocalName
		{
			get
			{
				return "autoRedefine";
			}
		}

		// Token: 0x17008D91 RID: 36241
		// (get) Token: 0x06019A71 RID: 105073 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D92 RID: 36242
		// (get) Token: 0x06019A72 RID: 105074 RVA: 0x00353955 File Offset: 0x00351B55
		internal override int ElementTypeId
		{
			get
			{
				return 11897;
			}
		}

		// Token: 0x06019A73 RID: 105075 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A75 RID: 105077 RVA: 0x0035395C File Offset: 0x00351B5C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoRedefine>(deep);
		}

		// Token: 0x0400A9E9 RID: 43497
		private const string tagName = "autoRedefine";

		// Token: 0x0400A9EA RID: 43498
		private const byte tagNsId = 23;

		// Token: 0x0400A9EB RID: 43499
		internal const int ElementTypeIdConst = 11897;
	}
}

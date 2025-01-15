using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFB RID: 12027
	[GeneratedCode("DomGen", "2.0")]
	internal class PersonalCompose : OnOffOnlyType
	{
		// Token: 0x17008DA5 RID: 36261
		// (get) Token: 0x06019A9A RID: 105114 RVA: 0x003539E8 File Offset: 0x00351BE8
		public override string LocalName
		{
			get
			{
				return "personalCompose";
			}
		}

		// Token: 0x17008DA6 RID: 36262
		// (get) Token: 0x06019A9B RID: 105115 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DA7 RID: 36263
		// (get) Token: 0x06019A9C RID: 105116 RVA: 0x003539EF File Offset: 0x00351BEF
		internal override int ElementTypeId
		{
			get
			{
				return 11905;
			}
		}

		// Token: 0x06019A9D RID: 105117 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A9F RID: 105119 RVA: 0x003539F6 File Offset: 0x00351BF6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PersonalCompose>(deep);
		}

		// Token: 0x0400A9FE RID: 43518
		private const string tagName = "personalCompose";

		// Token: 0x0400A9FF RID: 43519
		private const byte tagNsId = 23;

		// Token: 0x0400AA00 RID: 43520
		internal const int ElementTypeIdConst = 11905;
	}
}

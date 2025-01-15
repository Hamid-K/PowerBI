using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFC RID: 12028
	[GeneratedCode("DomGen", "2.0")]
	internal class PersonalReply : OnOffOnlyType
	{
		// Token: 0x17008DA8 RID: 36264
		// (get) Token: 0x06019AA0 RID: 105120 RVA: 0x003539FF File Offset: 0x00351BFF
		public override string LocalName
		{
			get
			{
				return "personalReply";
			}
		}

		// Token: 0x17008DA9 RID: 36265
		// (get) Token: 0x06019AA1 RID: 105121 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DAA RID: 36266
		// (get) Token: 0x06019AA2 RID: 105122 RVA: 0x00353A06 File Offset: 0x00351C06
		internal override int ElementTypeId
		{
			get
			{
				return 11906;
			}
		}

		// Token: 0x06019AA3 RID: 105123 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019AA5 RID: 105125 RVA: 0x00353A0D File Offset: 0x00351C0D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PersonalReply>(deep);
		}

		// Token: 0x0400AA01 RID: 43521
		private const string tagName = "personalReply";

		// Token: 0x0400AA02 RID: 43522
		private const byte tagNsId = 23;

		// Token: 0x0400AA03 RID: 43523
		internal const int ElementTypeIdConst = 11906;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DD3 RID: 11731
	[GeneratedCode("DomGen", "2.0")]
	internal class DoNotValidateAgainstSchema : OnOffType
	{
		// Token: 0x17008815 RID: 34837
		// (get) Token: 0x06018EEA RID: 102122 RVA: 0x00345302 File Offset: 0x00343502
		public override string LocalName
		{
			get
			{
				return "doNotValidateAgainstSchema";
			}
		}

		// Token: 0x17008816 RID: 34838
		// (get) Token: 0x06018EEB RID: 102123 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008817 RID: 34839
		// (get) Token: 0x06018EEC RID: 102124 RVA: 0x00345309 File Offset: 0x00343509
		internal override int ElementTypeId
		{
			get
			{
				return 12024;
			}
		}

		// Token: 0x06018EED RID: 102125 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018EEF RID: 102127 RVA: 0x00345310 File Offset: 0x00343510
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DoNotValidateAgainstSchema>(deep);
		}

		// Token: 0x0400A5E4 RID: 42468
		private const string tagName = "doNotValidateAgainstSchema";

		// Token: 0x0400A5E5 RID: 42469
		private const byte tagNsId = 23;

		// Token: 0x0400A5E6 RID: 42470
		internal const int ElementTypeIdConst = 12024;
	}
}

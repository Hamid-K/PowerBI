using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F40 RID: 12096
	[GeneratedCode("DomGen", "2.0")]
	internal class FrameName : String255Type
	{
		// Token: 0x17008FB4 RID: 36788
		// (get) Token: 0x06019F44 RID: 106308 RVA: 0x002F15F0 File Offset: 0x002EF7F0
		public override string LocalName
		{
			get
			{
				return "name";
			}
		}

		// Token: 0x17008FB5 RID: 36789
		// (get) Token: 0x06019F45 RID: 106309 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FB6 RID: 36790
		// (get) Token: 0x06019F46 RID: 106310 RVA: 0x0035A392 File Offset: 0x00358592
		internal override int ElementTypeId
		{
			get
			{
				return 11849;
			}
		}

		// Token: 0x06019F47 RID: 106311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019F49 RID: 106313 RVA: 0x0035A399 File Offset: 0x00358599
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FrameName>(deep);
		}

		// Token: 0x0400AB14 RID: 43796
		private const string tagName = "name";

		// Token: 0x0400AB15 RID: 43797
		private const byte tagNsId = 23;

		// Token: 0x0400AB16 RID: 43798
		internal const int ElementTypeIdConst = 11849;
	}
}

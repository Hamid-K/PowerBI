using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E12 RID: 11794
	[GeneratedCode("DomGen", "2.0")]
	internal class UseFarEastLayout : OnOffType
	{
		// Token: 0x170088D2 RID: 35026
		// (get) Token: 0x06019064 RID: 102500 RVA: 0x003458AB File Offset: 0x00343AAB
		public override string LocalName
		{
			get
			{
				return "useFELayout";
			}
		}

		// Token: 0x170088D3 RID: 35027
		// (get) Token: 0x06019065 RID: 102501 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170088D4 RID: 35028
		// (get) Token: 0x06019066 RID: 102502 RVA: 0x003458B2 File Offset: 0x00343AB2
		internal override int ElementTypeId
		{
			get
			{
				return 12104;
			}
		}

		// Token: 0x06019067 RID: 102503 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019069 RID: 102505 RVA: 0x003458B9 File Offset: 0x00343AB9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UseFarEastLayout>(deep);
		}

		// Token: 0x0400A6A1 RID: 42657
		private const string tagName = "useFELayout";

		// Token: 0x0400A6A2 RID: 42658
		private const byte tagNsId = 23;

		// Token: 0x0400A6A3 RID: 42659
		internal const int ElementTypeIdConst = 12104;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DBA RID: 11706
	[GeneratedCode("DomGen", "2.0")]
	internal class BordersDoNotSurroundHeader : OnOffType
	{
		// Token: 0x170087CA RID: 34762
		// (get) Token: 0x06018E54 RID: 101972 RVA: 0x003450C3 File Offset: 0x003432C3
		public override string LocalName
		{
			get
			{
				return "bordersDoNotSurroundHeader";
			}
		}

		// Token: 0x170087CB RID: 34763
		// (get) Token: 0x06018E55 RID: 101973 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087CC RID: 34764
		// (get) Token: 0x06018E56 RID: 101974 RVA: 0x003450CA File Offset: 0x003432CA
		internal override int ElementTypeId
		{
			get
			{
				return 11974;
			}
		}

		// Token: 0x06018E57 RID: 101975 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E59 RID: 101977 RVA: 0x003450D1 File Offset: 0x003432D1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BordersDoNotSurroundHeader>(deep);
		}

		// Token: 0x0400A599 RID: 42393
		private const string tagName = "bordersDoNotSurroundHeader";

		// Token: 0x0400A59A RID: 42394
		private const byte tagNsId = 23;

		// Token: 0x0400A59B RID: 42395
		internal const int ElementTypeIdConst = 11974;
	}
}

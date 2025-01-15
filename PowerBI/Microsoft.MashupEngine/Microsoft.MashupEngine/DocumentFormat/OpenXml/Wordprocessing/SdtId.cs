using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E3B RID: 11835
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtId : DecimalNumberType
	{
		// Token: 0x1700899D RID: 35229
		// (get) Token: 0x06019203 RID: 102915 RVA: 0x002E6A3B File Offset: 0x002E4C3B
		public override string LocalName
		{
			get
			{
				return "id";
			}
		}

		// Token: 0x1700899E RID: 35230
		// (get) Token: 0x06019204 RID: 102916 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700899F RID: 35231
		// (get) Token: 0x06019205 RID: 102917 RVA: 0x003468ED File Offset: 0x00344AED
		internal override int ElementTypeId
		{
			get
			{
				return 12145;
			}
		}

		// Token: 0x06019206 RID: 102918 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019208 RID: 102920 RVA: 0x003468F4 File Offset: 0x00344AF4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtId>(deep);
		}

		// Token: 0x0400A72F RID: 42799
		private const string tagName = "id";

		// Token: 0x0400A730 RID: 42800
		private const byte tagNsId = 23;

		// Token: 0x0400A731 RID: 42801
		internal const int ElementTypeIdConst = 12145;
	}
}

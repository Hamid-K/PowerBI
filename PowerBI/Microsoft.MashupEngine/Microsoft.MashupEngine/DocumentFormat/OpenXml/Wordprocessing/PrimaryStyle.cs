using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EF8 RID: 12024
	[GeneratedCode("DomGen", "2.0")]
	internal class PrimaryStyle : OnOffOnlyType
	{
		// Token: 0x17008D9C RID: 36252
		// (get) Token: 0x06019A88 RID: 105096 RVA: 0x003539A3 File Offset: 0x00351BA3
		public override string LocalName
		{
			get
			{
				return "qFormat";
			}
		}

		// Token: 0x17008D9D RID: 36253
		// (get) Token: 0x06019A89 RID: 105097 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D9E RID: 36254
		// (get) Token: 0x06019A8A RID: 105098 RVA: 0x003539AA File Offset: 0x00351BAA
		internal override int ElementTypeId
		{
			get
			{
				return 11902;
			}
		}

		// Token: 0x06019A8B RID: 105099 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A8D RID: 105101 RVA: 0x003539B1 File Offset: 0x00351BB1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PrimaryStyle>(deep);
		}

		// Token: 0x0400A9F5 RID: 43509
		private const string tagName = "qFormat";

		// Token: 0x0400A9F6 RID: 43510
		private const byte tagNsId = 23;

		// Token: 0x0400A9F7 RID: 43511
		internal const int ElementTypeIdConst = 11902;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D67 RID: 11623
	[GeneratedCode("DomGen", "2.0")]
	internal class SdtAlias : StringType
	{
		// Token: 0x170086D1 RID: 34513
		// (get) Token: 0x06018C61 RID: 101473 RVA: 0x0034494B File Offset: 0x00342B4B
		public override string LocalName
		{
			get
			{
				return "alias";
			}
		}

		// Token: 0x170086D2 RID: 34514
		// (get) Token: 0x06018C62 RID: 101474 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086D3 RID: 34515
		// (get) Token: 0x06018C63 RID: 101475 RVA: 0x00344952 File Offset: 0x00342B52
		internal override int ElementTypeId
		{
			get
			{
				return 12139;
			}
		}

		// Token: 0x06018C64 RID: 101476 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C66 RID: 101478 RVA: 0x00344959 File Offset: 0x00342B59
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SdtAlias>(deep);
		}

		// Token: 0x0400A4A1 RID: 42145
		private const string tagName = "alias";

		// Token: 0x0400A4A2 RID: 42146
		private const byte tagNsId = 23;

		// Token: 0x0400A4A3 RID: 42147
		internal const int ElementTypeIdConst = 12139;
	}
}

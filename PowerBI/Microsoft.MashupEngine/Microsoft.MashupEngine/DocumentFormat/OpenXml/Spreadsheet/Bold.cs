using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B98 RID: 11160
	[GeneratedCode("DomGen", "2.0")]
	internal class Bold : BooleanPropertyType
	{
		// Token: 0x17007B24 RID: 31524
		// (get) Token: 0x06017246 RID: 94790 RVA: 0x0032F0BC File Offset: 0x0032D2BC
		public override string LocalName
		{
			get
			{
				return "b";
			}
		}

		// Token: 0x17007B25 RID: 31525
		// (get) Token: 0x06017247 RID: 94791 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B26 RID: 31526
		// (get) Token: 0x06017248 RID: 94792 RVA: 0x00333393 File Offset: 0x00331593
		internal override int ElementTypeId
		{
			get
			{
				return 11135;
			}
		}

		// Token: 0x06017249 RID: 94793 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601724B RID: 94795 RVA: 0x003333A2 File Offset: 0x003315A2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Bold>(deep);
		}

		// Token: 0x04009B37 RID: 39735
		private const string tagName = "b";

		// Token: 0x04009B38 RID: 39736
		private const byte tagNsId = 22;

		// Token: 0x04009B39 RID: 39737
		internal const int ElementTypeIdConst = 11135;
	}
}

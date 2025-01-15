using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EEA RID: 12010
	[GeneratedCode("DomGen", "2.0")]
	internal class NoWrap : OnOffOnlyType
	{
		// Token: 0x17008D72 RID: 36210
		// (get) Token: 0x06019A34 RID: 105012 RVA: 0x00353860 File Offset: 0x00351A60
		public override string LocalName
		{
			get
			{
				return "noWrap";
			}
		}

		// Token: 0x17008D73 RID: 36211
		// (get) Token: 0x06019A35 RID: 105013 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008D74 RID: 36212
		// (get) Token: 0x06019A36 RID: 105014 RVA: 0x00353867 File Offset: 0x00351A67
		internal override int ElementTypeId
		{
			get
			{
				return 11655;
			}
		}

		// Token: 0x06019A37 RID: 105015 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019A39 RID: 105017 RVA: 0x00353876 File Offset: 0x00351A76
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoWrap>(deep);
		}

		// Token: 0x0400A9CB RID: 43467
		private const string tagName = "noWrap";

		// Token: 0x0400A9CC RID: 43468
		private const byte tagNsId = 23;

		// Token: 0x0400A9CD RID: 43469
		internal const int ElementTypeIdConst = 11655;
	}
}

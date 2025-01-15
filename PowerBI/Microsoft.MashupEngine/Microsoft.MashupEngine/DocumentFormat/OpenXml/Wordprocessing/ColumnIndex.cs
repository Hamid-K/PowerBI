using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F6C RID: 12140
	[GeneratedCode("DomGen", "2.0")]
	internal class ColumnIndex : UnsignedDecimalNumberType
	{
		// Token: 0x170090D4 RID: 37076
		// (get) Token: 0x0601A1C5 RID: 106949 RVA: 0x002BF3C4 File Offset: 0x002BD5C4
		public override string LocalName
		{
			get
			{
				return "column";
			}
		}

		// Token: 0x170090D5 RID: 37077
		// (get) Token: 0x0601A1C6 RID: 106950 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170090D6 RID: 37078
		// (get) Token: 0x0601A1C7 RID: 106951 RVA: 0x0035D934 File Offset: 0x0035BB34
		internal override int ElementTypeId
		{
			get
			{
				return 11797;
			}
		}

		// Token: 0x0601A1C8 RID: 106952 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A1CA RID: 106954 RVA: 0x0035D943 File Offset: 0x0035BB43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnIndex>(deep);
		}

		// Token: 0x0400ABD5 RID: 43989
		private const string tagName = "column";

		// Token: 0x0400ABD6 RID: 43990
		private const byte tagNsId = 23;

		// Token: 0x0400ABD7 RID: 43991
		internal const int ElementTypeIdConst = 11797;
	}
}

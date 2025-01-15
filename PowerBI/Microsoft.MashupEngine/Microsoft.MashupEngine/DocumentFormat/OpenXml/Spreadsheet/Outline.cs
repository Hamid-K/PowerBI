using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B9D RID: 11165
	[GeneratedCode("DomGen", "2.0")]
	internal class Outline : BooleanPropertyType
	{
		// Token: 0x17007B33 RID: 31539
		// (get) Token: 0x06017264 RID: 94820 RVA: 0x00333400 File Offset: 0x00331600
		public override string LocalName
		{
			get
			{
				return "outline";
			}
		}

		// Token: 0x17007B34 RID: 31540
		// (get) Token: 0x06017265 RID: 94821 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007B35 RID: 31541
		// (get) Token: 0x06017266 RID: 94822 RVA: 0x00333407 File Offset: 0x00331607
		internal override int ElementTypeId
		{
			get
			{
				return 11140;
			}
		}

		// Token: 0x06017267 RID: 94823 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017269 RID: 94825 RVA: 0x0033340E File Offset: 0x0033160E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Outline>(deep);
		}

		// Token: 0x04009B46 RID: 39750
		private const string tagName = "outline";

		// Token: 0x04009B47 RID: 39751
		private const byte tagNsId = 22;

		// Token: 0x04009B48 RID: 39752
		internal const int ElementTypeIdConst = 11140;
	}
}

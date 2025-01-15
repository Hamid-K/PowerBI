using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EFD RID: 12029
	[GeneratedCode("DomGen", "2.0")]
	internal class NotTrueType : OnOffOnlyType
	{
		// Token: 0x17008DAB RID: 36267
		// (get) Token: 0x06019AA6 RID: 105126 RVA: 0x00353A16 File Offset: 0x00351C16
		public override string LocalName
		{
			get
			{
				return "notTrueType";
			}
		}

		// Token: 0x17008DAC RID: 36268
		// (get) Token: 0x06019AA7 RID: 105127 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008DAD RID: 36269
		// (get) Token: 0x06019AA8 RID: 105128 RVA: 0x00353A1D File Offset: 0x00351C1D
		internal override int ElementTypeId
		{
			get
			{
				return 11919;
			}
		}

		// Token: 0x06019AA9 RID: 105129 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019AAB RID: 105131 RVA: 0x00353A24 File Offset: 0x00351C24
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NotTrueType>(deep);
		}

		// Token: 0x0400AA04 RID: 43524
		private const string tagName = "notTrueType";

		// Token: 0x0400AA05 RID: 43525
		private const byte tagNsId = 23;

		// Token: 0x0400AA06 RID: 43526
		internal const int ElementTypeIdConst = 11919;
	}
}

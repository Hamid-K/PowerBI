using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F2B RID: 12075
	[GeneratedCode("DomGen", "2.0")]
	internal class AbstractNumId : NonNegativeDecimalNumberType
	{
		// Token: 0x17008F43 RID: 36675
		// (get) Token: 0x06019E4C RID: 106060 RVA: 0x00359023 File Offset: 0x00357223
		public override string LocalName
		{
			get
			{
				return "abstractNumId";
			}
		}

		// Token: 0x17008F44 RID: 36676
		// (get) Token: 0x06019E4D RID: 106061 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F45 RID: 36677
		// (get) Token: 0x06019E4E RID: 106062 RVA: 0x0035902A File Offset: 0x0035722A
		internal override int ElementTypeId
		{
			get
			{
				return 11882;
			}
		}

		// Token: 0x06019E4F RID: 106063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019E51 RID: 106065 RVA: 0x00359031 File Offset: 0x00357231
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AbstractNumId>(deep);
		}

		// Token: 0x0400AAC3 RID: 43715
		private const string tagName = "abstractNumId";

		// Token: 0x0400AAC4 RID: 43716
		private const byte tagNsId = 23;

		// Token: 0x0400AAC5 RID: 43717
		internal const int ElementTypeIdConst = 11882;
	}
}

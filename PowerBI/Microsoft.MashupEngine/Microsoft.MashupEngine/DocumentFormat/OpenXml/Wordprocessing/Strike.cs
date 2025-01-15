using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D86 RID: 11654
	[GeneratedCode("DomGen", "2.0")]
	internal class Strike : OnOffType
	{
		// Token: 0x1700872E RID: 34606
		// (get) Token: 0x06018D1C RID: 101660 RVA: 0x003333BB File Offset: 0x003315BB
		public override string LocalName
		{
			get
			{
				return "strike";
			}
		}

		// Token: 0x1700872F RID: 34607
		// (get) Token: 0x06018D1D RID: 101661 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008730 RID: 34608
		// (get) Token: 0x06018D1E RID: 101662 RVA: 0x00344C4F File Offset: 0x00342E4F
		internal override int ElementTypeId
		{
			get
			{
				return 11583;
			}
		}

		// Token: 0x06018D1F RID: 101663 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D21 RID: 101665 RVA: 0x00344C56 File Offset: 0x00342E56
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Strike>(deep);
		}

		// Token: 0x0400A4FD RID: 42237
		private const string tagName = "strike";

		// Token: 0x0400A4FE RID: 42238
		private const byte tagNsId = 23;

		// Token: 0x0400A4FF RID: 42239
		internal const int ElementTypeIdConst = 11583;
	}
}

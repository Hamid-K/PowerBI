using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D54 RID: 11604
	[GeneratedCode("DomGen", "2.0")]
	internal class DocPartCategory : StringType
	{
		// Token: 0x17008698 RID: 34456
		// (get) Token: 0x06018BEF RID: 101359 RVA: 0x003447B9 File Offset: 0x003429B9
		public override string LocalName
		{
			get
			{
				return "docPartCategory";
			}
		}

		// Token: 0x17008699 RID: 34457
		// (get) Token: 0x06018BF0 RID: 101360 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700869A RID: 34458
		// (get) Token: 0x06018BF1 RID: 101361 RVA: 0x003447C0 File Offset: 0x003429C0
		internal override int ElementTypeId
		{
			get
			{
				return 11766;
			}
		}

		// Token: 0x06018BF2 RID: 101362 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018BF4 RID: 101364 RVA: 0x003447C7 File Offset: 0x003429C7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocPartCategory>(deep);
		}

		// Token: 0x0400A468 RID: 42088
		private const string tagName = "docPartCategory";

		// Token: 0x0400A469 RID: 42089
		private const byte tagNsId = 23;

		// Token: 0x0400A46A RID: 42090
		internal const int ElementTypeIdConst = 11766;
	}
}

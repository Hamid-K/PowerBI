using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FBE RID: 12222
	[GeneratedCode("DomGen", "2.0")]
	internal class RightMarginDiv : SignedTwipsMeasureType
	{
		// Token: 0x170093D1 RID: 37841
		// (get) Token: 0x0601A809 RID: 108553 RVA: 0x003632A3 File Offset: 0x003614A3
		public override string LocalName
		{
			get
			{
				return "marRight";
			}
		}

		// Token: 0x170093D2 RID: 37842
		// (get) Token: 0x0601A80A RID: 108554 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170093D3 RID: 37843
		// (get) Token: 0x0601A80B RID: 108555 RVA: 0x003632AA File Offset: 0x003614AA
		internal override int ElementTypeId
		{
			get
			{
				return 11930;
			}
		}

		// Token: 0x0601A80C RID: 108556 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A80E RID: 108558 RVA: 0x003632B1 File Offset: 0x003614B1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RightMarginDiv>(deep);
		}

		// Token: 0x0400AD3D RID: 44349
		private const string tagName = "marRight";

		// Token: 0x0400AD3E RID: 44350
		private const byte tagNsId = 23;

		// Token: 0x0400AD3F RID: 44351
		internal const int ElementTypeIdConst = 11930;
	}
}

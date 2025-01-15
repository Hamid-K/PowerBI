using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002572 RID: 9586
	[GeneratedCode("DomGen", "2.0")]
	internal class OddFooter : OpenXmlLeafTextElement
	{
		// Token: 0x170055DD RID: 21981
		// (get) Token: 0x06011E06 RID: 73222 RVA: 0x002F346C File Offset: 0x002F166C
		public override string LocalName
		{
			get
			{
				return "oddFooter";
			}
		}

		// Token: 0x170055DE RID: 21982
		// (get) Token: 0x06011E07 RID: 73223 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055DF RID: 21983
		// (get) Token: 0x06011E08 RID: 73224 RVA: 0x002F3473 File Offset: 0x002F1673
		internal override int ElementTypeId
		{
			get
			{
				return 10511;
			}
		}

		// Token: 0x06011E09 RID: 73225 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E0A RID: 73226 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public OddFooter()
		{
		}

		// Token: 0x06011E0B RID: 73227 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public OddFooter(string text)
			: base(text)
		{
		}

		// Token: 0x06011E0C RID: 73228 RVA: 0x002F347C File Offset: 0x002F167C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E0D RID: 73229 RVA: 0x002F3497 File Offset: 0x002F1697
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OddFooter>(deep);
		}

		// Token: 0x04007D09 RID: 32009
		private const string tagName = "oddFooter";

		// Token: 0x04007D0A RID: 32010
		private const byte tagNsId = 11;

		// Token: 0x04007D0B RID: 32011
		internal const int ElementTypeIdConst = 10511;
	}
}

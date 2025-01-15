using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002571 RID: 9585
	[GeneratedCode("DomGen", "2.0")]
	internal class OddHeader : OpenXmlLeafTextElement
	{
		// Token: 0x170055DA RID: 21978
		// (get) Token: 0x06011DFE RID: 73214 RVA: 0x002F3438 File Offset: 0x002F1638
		public override string LocalName
		{
			get
			{
				return "oddHeader";
			}
		}

		// Token: 0x170055DB RID: 21979
		// (get) Token: 0x06011DFF RID: 73215 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055DC RID: 21980
		// (get) Token: 0x06011E00 RID: 73216 RVA: 0x002F343F File Offset: 0x002F163F
		internal override int ElementTypeId
		{
			get
			{
				return 10510;
			}
		}

		// Token: 0x06011E01 RID: 73217 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E02 RID: 73218 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public OddHeader()
		{
		}

		// Token: 0x06011E03 RID: 73219 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public OddHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06011E04 RID: 73220 RVA: 0x002F3448 File Offset: 0x002F1648
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E05 RID: 73221 RVA: 0x002F3463 File Offset: 0x002F1663
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OddHeader>(deep);
		}

		// Token: 0x04007D06 RID: 32006
		private const string tagName = "oddHeader";

		// Token: 0x04007D07 RID: 32007
		private const byte tagNsId = 11;

		// Token: 0x04007D08 RID: 32008
		internal const int ElementTypeIdConst = 10510;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002913 RID: 10515
	[GeneratedCode("DomGen", "2.0")]
	internal class VTOStreamData : OpenXmlLeafTextElement
	{
		// Token: 0x17006A94 RID: 27284
		// (get) Token: 0x06014D0F RID: 85263 RVA: 0x00317D4C File Offset: 0x00315F4C
		public override string LocalName
		{
			get
			{
				return "ostream";
			}
		}

		// Token: 0x17006A95 RID: 27285
		// (get) Token: 0x06014D10 RID: 85264 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A96 RID: 27286
		// (get) Token: 0x06014D11 RID: 85265 RVA: 0x00317D53 File Offset: 0x00315F53
		internal override int ElementTypeId
		{
			get
			{
				return 10992;
			}
		}

		// Token: 0x06014D12 RID: 85266 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D13 RID: 85267 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTOStreamData()
		{
		}

		// Token: 0x06014D14 RID: 85268 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTOStreamData(string text)
			: base(text)
		{
		}

		// Token: 0x06014D15 RID: 85269 RVA: 0x00317D5C File Offset: 0x00315F5C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D16 RID: 85270 RVA: 0x00317D77 File Offset: 0x00315F77
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTOStreamData>(deep);
		}

		// Token: 0x04008FEB RID: 36843
		private const string tagName = "ostream";

		// Token: 0x04008FEC RID: 36844
		private const byte tagNsId = 5;

		// Token: 0x04008FED RID: 36845
		internal const int ElementTypeIdConst = 10992;
	}
}

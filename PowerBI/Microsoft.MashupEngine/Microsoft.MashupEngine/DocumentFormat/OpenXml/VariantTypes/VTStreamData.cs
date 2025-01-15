using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002912 RID: 10514
	[GeneratedCode("DomGen", "2.0")]
	internal class VTStreamData : OpenXmlLeafTextElement
	{
		// Token: 0x17006A91 RID: 27281
		// (get) Token: 0x06014D07 RID: 85255 RVA: 0x00317D18 File Offset: 0x00315F18
		public override string LocalName
		{
			get
			{
				return "stream";
			}
		}

		// Token: 0x17006A92 RID: 27282
		// (get) Token: 0x06014D08 RID: 85256 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006A93 RID: 27283
		// (get) Token: 0x06014D09 RID: 85257 RVA: 0x00317D1F File Offset: 0x00315F1F
		internal override int ElementTypeId
		{
			get
			{
				return 10991;
			}
		}

		// Token: 0x06014D0A RID: 85258 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D0B RID: 85259 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTStreamData()
		{
		}

		// Token: 0x06014D0C RID: 85260 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTStreamData(string text)
			: base(text)
		{
		}

		// Token: 0x06014D0D RID: 85261 RVA: 0x00317D28 File Offset: 0x00315F28
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Base64BinaryValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D0E RID: 85262 RVA: 0x00317D43 File Offset: 0x00315F43
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTStreamData>(deep);
		}

		// Token: 0x04008FE8 RID: 36840
		private const string tagName = "stream";

		// Token: 0x04008FE9 RID: 36841
		private const byte tagNsId = 5;

		// Token: 0x04008FEA RID: 36842
		internal const int ElementTypeIdConst = 10991;
	}
}

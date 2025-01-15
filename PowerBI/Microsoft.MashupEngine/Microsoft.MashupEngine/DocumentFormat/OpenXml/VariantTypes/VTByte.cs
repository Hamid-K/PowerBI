using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002918 RID: 10520
	[GeneratedCode("DomGen", "2.0")]
	internal class VTByte : OpenXmlLeafTextElement
	{
		// Token: 0x17006AA3 RID: 27299
		// (get) Token: 0x06014D33 RID: 85299 RVA: 0x00317E0F File Offset: 0x0031600F
		public override string LocalName
		{
			get
			{
				return "i1";
			}
		}

		// Token: 0x17006AA4 RID: 27300
		// (get) Token: 0x06014D34 RID: 85300 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AA5 RID: 27301
		// (get) Token: 0x06014D35 RID: 85301 RVA: 0x00317E16 File Offset: 0x00316016
		internal override int ElementTypeId
		{
			get
			{
				return 10970;
			}
		}

		// Token: 0x06014D36 RID: 85302 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D37 RID: 85303 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTByte()
		{
		}

		// Token: 0x06014D38 RID: 85304 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTByte(string text)
			: base(text)
		{
		}

		// Token: 0x06014D39 RID: 85305 RVA: 0x00317E20 File Offset: 0x00316020
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new SByteValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D3A RID: 85306 RVA: 0x00317E3B File Offset: 0x0031603B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTByte>(deep);
		}

		// Token: 0x04008FFA RID: 36858
		private const string tagName = "i1";

		// Token: 0x04008FFB RID: 36859
		private const byte tagNsId = 5;

		// Token: 0x04008FFC RID: 36860
		internal const int ElementTypeIdConst = 10970;
	}
}

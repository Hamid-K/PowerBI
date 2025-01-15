using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002919 RID: 10521
	[GeneratedCode("DomGen", "2.0")]
	internal class VTShort : OpenXmlLeafTextElement
	{
		// Token: 0x17006AA6 RID: 27302
		// (get) Token: 0x06014D3B RID: 85307 RVA: 0x00317E44 File Offset: 0x00316044
		public override string LocalName
		{
			get
			{
				return "i2";
			}
		}

		// Token: 0x17006AA7 RID: 27303
		// (get) Token: 0x06014D3C RID: 85308 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AA8 RID: 27304
		// (get) Token: 0x06014D3D RID: 85309 RVA: 0x00317E4B File Offset: 0x0031604B
		internal override int ElementTypeId
		{
			get
			{
				return 10971;
			}
		}

		// Token: 0x06014D3E RID: 85310 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D3F RID: 85311 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTShort()
		{
		}

		// Token: 0x06014D40 RID: 85312 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTShort(string text)
			: base(text)
		{
		}

		// Token: 0x06014D41 RID: 85313 RVA: 0x00317E54 File Offset: 0x00316054
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int16Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D42 RID: 85314 RVA: 0x00317E6F File Offset: 0x0031606F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTShort>(deep);
		}

		// Token: 0x04008FFD RID: 36861
		private const string tagName = "i2";

		// Token: 0x04008FFE RID: 36862
		private const byte tagNsId = 5;

		// Token: 0x04008FFF RID: 36863
		internal const int ElementTypeIdConst = 10971;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002921 RID: 10529
	[GeneratedCode("DomGen", "2.0")]
	internal class VTUnsignedInt64 : OpenXmlLeafTextElement
	{
		// Token: 0x17006ABE RID: 27326
		// (get) Token: 0x06014D7B RID: 85371 RVA: 0x00317FE4 File Offset: 0x003161E4
		public override string LocalName
		{
			get
			{
				return "ui8";
			}
		}

		// Token: 0x17006ABF RID: 27327
		// (get) Token: 0x06014D7C RID: 85372 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AC0 RID: 27328
		// (get) Token: 0x06014D7D RID: 85373 RVA: 0x00317FEB File Offset: 0x003161EB
		internal override int ElementTypeId
		{
			get
			{
				return 10978;
			}
		}

		// Token: 0x06014D7E RID: 85374 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D7F RID: 85375 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTUnsignedInt64()
		{
		}

		// Token: 0x06014D80 RID: 85376 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTUnsignedInt64(string text)
			: base(text)
		{
		}

		// Token: 0x06014D81 RID: 85377 RVA: 0x00317FF4 File Offset: 0x003161F4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new UInt64Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D82 RID: 85378 RVA: 0x0031800F File Offset: 0x0031620F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTUnsignedInt64>(deep);
		}

		// Token: 0x04009015 RID: 36885
		private const string tagName = "ui8";

		// Token: 0x04009016 RID: 36886
		private const byte tagNsId = 5;

		// Token: 0x04009017 RID: 36887
		internal const int ElementTypeIdConst = 10978;
	}
}

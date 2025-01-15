using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291E RID: 10526
	[GeneratedCode("DomGen", "2.0")]
	internal class VTUnsignedShort : OpenXmlLeafTextElement
	{
		// Token: 0x17006AB5 RID: 27317
		// (get) Token: 0x06014D63 RID: 85347 RVA: 0x00317F48 File Offset: 0x00316148
		public override string LocalName
		{
			get
			{
				return "ui2";
			}
		}

		// Token: 0x17006AB6 RID: 27318
		// (get) Token: 0x06014D64 RID: 85348 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AB7 RID: 27319
		// (get) Token: 0x06014D65 RID: 85349 RVA: 0x00317F4F File Offset: 0x0031614F
		internal override int ElementTypeId
		{
			get
			{
				return 10976;
			}
		}

		// Token: 0x06014D66 RID: 85350 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D67 RID: 85351 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTUnsignedShort()
		{
		}

		// Token: 0x06014D68 RID: 85352 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTUnsignedShort(string text)
			: base(text)
		{
		}

		// Token: 0x06014D69 RID: 85353 RVA: 0x00317F58 File Offset: 0x00316158
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new UInt16Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014D6A RID: 85354 RVA: 0x00317F73 File Offset: 0x00316173
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTUnsignedShort>(deep);
		}

		// Token: 0x0400900C RID: 36876
		private const string tagName = "ui2";

		// Token: 0x0400900D RID: 36877
		private const byte tagNsId = 5;

		// Token: 0x0400900E RID: 36878
		internal const int ElementTypeIdConst = 10976;
	}
}

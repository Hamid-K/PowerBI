using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200291D RID: 10525
	[GeneratedCode("DomGen", "2.0")]
	internal class VTUnsignedByte : OpenXmlLeafTextElement
	{
		// Token: 0x17006AB2 RID: 27314
		// (get) Token: 0x06014D5B RID: 85339 RVA: 0x00317F14 File Offset: 0x00316114
		public override string LocalName
		{
			get
			{
				return "ui1";
			}
		}

		// Token: 0x17006AB3 RID: 27315
		// (get) Token: 0x06014D5C RID: 85340 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AB4 RID: 27316
		// (get) Token: 0x06014D5D RID: 85341 RVA: 0x00317F1B File Offset: 0x0031611B
		internal override int ElementTypeId
		{
			get
			{
				return 10975;
			}
		}

		// Token: 0x06014D5E RID: 85342 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D5F RID: 85343 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTUnsignedByte()
		{
		}

		// Token: 0x06014D60 RID: 85344 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTUnsignedByte(string text)
			: base(text)
		{
		}

		// Token: 0x06014D61 RID: 85345 RVA: 0x00317F24 File Offset: 0x00316124
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new ByteValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D62 RID: 85346 RVA: 0x00317F3F File Offset: 0x0031613F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTUnsignedByte>(deep);
		}

		// Token: 0x04009009 RID: 36873
		private const string tagName = "ui1";

		// Token: 0x0400900A RID: 36874
		private const byte tagNsId = 5;

		// Token: 0x0400900B RID: 36875
		internal const int ElementTypeIdConst = 10975;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002924 RID: 10532
	[GeneratedCode("DomGen", "2.0")]
	internal class VTDecimal : OpenXmlLeafTextElement
	{
		// Token: 0x17006AC7 RID: 27335
		// (get) Token: 0x06014D93 RID: 85395 RVA: 0x00318080 File Offset: 0x00316280
		public override string LocalName
		{
			get
			{
				return "decimal";
			}
		}

		// Token: 0x17006AC8 RID: 27336
		// (get) Token: 0x06014D94 RID: 85396 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AC9 RID: 27337
		// (get) Token: 0x06014D95 RID: 85397 RVA: 0x00318087 File Offset: 0x00316287
		internal override int ElementTypeId
		{
			get
			{
				return 10982;
			}
		}

		// Token: 0x06014D96 RID: 85398 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014D97 RID: 85399 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTDecimal()
		{
		}

		// Token: 0x06014D98 RID: 85400 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTDecimal(string text)
			: base(text)
		{
		}

		// Token: 0x06014D99 RID: 85401 RVA: 0x00318090 File Offset: 0x00316290
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DecimalValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014D9A RID: 85402 RVA: 0x003180AB File Offset: 0x003162AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTDecimal>(deep);
		}

		// Token: 0x0400901E RID: 36894
		private const string tagName = "decimal";

		// Token: 0x0400901F RID: 36895
		private const byte tagNsId = 5;

		// Token: 0x04009020 RID: 36896
		internal const int ElementTypeIdConst = 10982;
	}
}

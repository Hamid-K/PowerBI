using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x02002936 RID: 10550
	[GeneratedCode("DomGen", "2.0")]
	internal class PresentationFormat : OpenXmlLeafTextElement
	{
		// Token: 0x17006B1D RID: 27421
		// (get) Token: 0x06014E62 RID: 85602 RVA: 0x00318A4C File Offset: 0x00316C4C
		public override string LocalName
		{
			get
			{
				return "PresentationFormat";
			}
		}

		// Token: 0x17006B1E RID: 27422
		// (get) Token: 0x06014E63 RID: 85603 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B1F RID: 27423
		// (get) Token: 0x06014E64 RID: 85604 RVA: 0x00318A53 File Offset: 0x00316C53
		internal override int ElementTypeId
		{
			get
			{
				return 11005;
			}
		}

		// Token: 0x06014E65 RID: 85605 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E66 RID: 85606 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PresentationFormat()
		{
		}

		// Token: 0x06014E67 RID: 85607 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PresentationFormat(string text)
			: base(text)
		{
		}

		// Token: 0x06014E68 RID: 85608 RVA: 0x00318A5C File Offset: 0x00316C5C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014E69 RID: 85609 RVA: 0x00318A77 File Offset: 0x00316C77
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresentationFormat>(deep);
		}

		// Token: 0x0400907C RID: 36988
		private const string tagName = "PresentationFormat";

		// Token: 0x0400907D RID: 36989
		private const byte tagNsId = 3;

		// Token: 0x0400907E RID: 36990
		internal const int ElementTypeIdConst = 11005;
	}
}

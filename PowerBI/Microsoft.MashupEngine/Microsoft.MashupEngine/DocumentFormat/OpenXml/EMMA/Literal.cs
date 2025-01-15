using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.EMMA
{
	// Token: 0x02003074 RID: 12404
	[GeneratedCode("DomGen", "2.0")]
	internal class Literal : OpenXmlLeafTextElement
	{
		// Token: 0x1700968B RID: 38539
		// (get) Token: 0x0601ADFA RID: 110074 RVA: 0x00368A69 File Offset: 0x00366C69
		public override string LocalName
		{
			get
			{
				return "literal";
			}
		}

		// Token: 0x1700968C RID: 38540
		// (get) Token: 0x0601ADFB RID: 110075 RVA: 0x0036884A File Offset: 0x00366A4A
		internal override byte NamespaceId
		{
			get
			{
				return 44;
			}
		}

		// Token: 0x1700968D RID: 38541
		// (get) Token: 0x0601ADFC RID: 110076 RVA: 0x00368A70 File Offset: 0x00366C70
		internal override int ElementTypeId
		{
			get
			{
				return 12673;
			}
		}

		// Token: 0x0601ADFD RID: 110077 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ADFE RID: 110078 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Literal()
		{
		}

		// Token: 0x0601ADFF RID: 110079 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Literal(string text)
			: base(text)
		{
		}

		// Token: 0x0601AE00 RID: 110080 RVA: 0x00368A78 File Offset: 0x00366C78
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601AE01 RID: 110081 RVA: 0x00368A93 File Offset: 0x00366C93
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Literal>(deep);
		}

		// Token: 0x0400B21A RID: 45594
		private const string tagName = "literal";

		// Token: 0x0400B21B RID: 45595
		private const byte tagNsId = 44;

		// Token: 0x0400B21C RID: 45596
		internal const int ElementTypeIdConst = 12673;
	}
}

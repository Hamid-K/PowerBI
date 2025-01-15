using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DE RID: 10462
	[GeneratedCode("DomGen", "2.0")]
	internal class ReferenceOrder : OpenXmlLeafTextElement
	{
		// Token: 0x17006942 RID: 26946
		// (get) Token: 0x060149E4 RID: 84452 RVA: 0x00314FEC File Offset: 0x003131EC
		public override string LocalName
		{
			get
			{
				return "RefOrder";
			}
		}

		// Token: 0x17006943 RID: 26947
		// (get) Token: 0x060149E5 RID: 84453 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006944 RID: 26948
		// (get) Token: 0x060149E6 RID: 84454 RVA: 0x00314FF3 File Offset: 0x003131F3
		internal override int ElementTypeId
		{
			get
			{
				return 10816;
			}
		}

		// Token: 0x060149E7 RID: 84455 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149E8 RID: 84456 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ReferenceOrder()
		{
		}

		// Token: 0x060149E9 RID: 84457 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ReferenceOrder(string text)
			: base(text)
		{
		}

		// Token: 0x060149EA RID: 84458 RVA: 0x00314FFC File Offset: 0x003131FC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149EB RID: 84459 RVA: 0x00315017 File Offset: 0x00313217
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ReferenceOrder>(deep);
		}

		// Token: 0x04008F26 RID: 36646
		private const string tagName = "RefOrder";

		// Token: 0x04008F27 RID: 36647
		private const byte tagNsId = 9;

		// Token: 0x04008F28 RID: 36648
		internal const int ElementTypeIdConst = 10816;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002575 RID: 9589
	[GeneratedCode("DomGen", "2.0")]
	internal class FirstHeader : OpenXmlLeafTextElement
	{
		// Token: 0x170055E6 RID: 21990
		// (get) Token: 0x06011E1E RID: 73246 RVA: 0x002F3508 File Offset: 0x002F1708
		public override string LocalName
		{
			get
			{
				return "firstHeader";
			}
		}

		// Token: 0x170055E7 RID: 21991
		// (get) Token: 0x06011E1F RID: 73247 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055E8 RID: 21992
		// (get) Token: 0x06011E20 RID: 73248 RVA: 0x002F350F File Offset: 0x002F170F
		internal override int ElementTypeId
		{
			get
			{
				return 10514;
			}
		}

		// Token: 0x06011E21 RID: 73249 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E22 RID: 73250 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public FirstHeader()
		{
		}

		// Token: 0x06011E23 RID: 73251 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public FirstHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06011E24 RID: 73252 RVA: 0x002F3518 File Offset: 0x002F1718
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E25 RID: 73253 RVA: 0x002F3533 File Offset: 0x002F1733
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FirstHeader>(deep);
		}

		// Token: 0x04007D12 RID: 32018
		private const string tagName = "firstHeader";

		// Token: 0x04007D13 RID: 32019
		private const byte tagNsId = 11;

		// Token: 0x04007D14 RID: 32020
		internal const int ElementTypeIdConst = 10514;
	}
}

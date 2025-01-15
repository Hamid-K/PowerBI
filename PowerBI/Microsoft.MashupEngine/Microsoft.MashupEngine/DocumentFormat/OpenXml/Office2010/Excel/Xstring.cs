using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242A RID: 9258
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class Xstring : OpenXmlLeafTextElement
	{
		// Token: 0x17004FB0 RID: 20400
		// (get) Token: 0x06011032 RID: 69682 RVA: 0x002E9ADF File Offset: 0x002E7CDF
		public override string LocalName
		{
			get
			{
				return "tupleItem";
			}
		}

		// Token: 0x17004FB1 RID: 20401
		// (get) Token: 0x06011033 RID: 69683 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FB2 RID: 20402
		// (get) Token: 0x06011034 RID: 69684 RVA: 0x002E9AE6 File Offset: 0x002E7CE6
		internal override int ElementTypeId
		{
			get
			{
				return 12982;
			}
		}

		// Token: 0x06011035 RID: 69685 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x06011036 RID: 69686 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Xstring()
		{
		}

		// Token: 0x06011037 RID: 69687 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Xstring(string text)
			: base(text)
		{
		}

		// Token: 0x06011038 RID: 69688 RVA: 0x002E9AF0 File Offset: 0x002E7CF0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011039 RID: 69689 RVA: 0x002E9B0B File Offset: 0x002E7D0B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Xstring>(deep);
		}

		// Token: 0x04007745 RID: 30533
		private const string tagName = "tupleItem";

		// Token: 0x04007746 RID: 30534
		private const byte tagNsId = 53;

		// Token: 0x04007747 RID: 30535
		internal const int ElementTypeIdConst = 12982;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E1 RID: 10465
	[GeneratedCode("DomGen", "2.0")]
	internal class StandardNumber : OpenXmlLeafTextElement
	{
		// Token: 0x1700694B RID: 26955
		// (get) Token: 0x060149FC RID: 84476 RVA: 0x00315088 File Offset: 0x00313288
		public override string LocalName
		{
			get
			{
				return "StandardNumber";
			}
		}

		// Token: 0x1700694C RID: 26956
		// (get) Token: 0x060149FD RID: 84477 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700694D RID: 26957
		// (get) Token: 0x060149FE RID: 84478 RVA: 0x0031508F File Offset: 0x0031328F
		internal override int ElementTypeId
		{
			get
			{
				return 10820;
			}
		}

		// Token: 0x060149FF RID: 84479 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A00 RID: 84480 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public StandardNumber()
		{
		}

		// Token: 0x06014A01 RID: 84481 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public StandardNumber(string text)
			: base(text)
		{
		}

		// Token: 0x06014A02 RID: 84482 RVA: 0x00315098 File Offset: 0x00313298
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A03 RID: 84483 RVA: 0x003150B3 File Offset: 0x003132B3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StandardNumber>(deep);
		}

		// Token: 0x04008F2F RID: 36655
		private const string tagName = "StandardNumber";

		// Token: 0x04008F30 RID: 36656
		private const byte tagNsId = 9;

		// Token: 0x04008F31 RID: 36657
		internal const int ElementTypeIdConst = 10820;
	}
}

using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002573 RID: 9587
	[GeneratedCode("DomGen", "2.0")]
	internal class EvenHeader : OpenXmlLeafTextElement
	{
		// Token: 0x170055E0 RID: 21984
		// (get) Token: 0x06011E0E RID: 73230 RVA: 0x002F34A0 File Offset: 0x002F16A0
		public override string LocalName
		{
			get
			{
				return "evenHeader";
			}
		}

		// Token: 0x170055E1 RID: 21985
		// (get) Token: 0x06011E0F RID: 73231 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055E2 RID: 21986
		// (get) Token: 0x06011E10 RID: 73232 RVA: 0x002F34A7 File Offset: 0x002F16A7
		internal override int ElementTypeId
		{
			get
			{
				return 10512;
			}
		}

		// Token: 0x06011E11 RID: 73233 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E12 RID: 73234 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public EvenHeader()
		{
		}

		// Token: 0x06011E13 RID: 73235 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public EvenHeader(string text)
			: base(text)
		{
		}

		// Token: 0x06011E14 RID: 73236 RVA: 0x002F34B0 File Offset: 0x002F16B0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06011E15 RID: 73237 RVA: 0x002F34CB File Offset: 0x002F16CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EvenHeader>(deep);
		}

		// Token: 0x04007D0C RID: 32012
		private const string tagName = "evenHeader";

		// Token: 0x04007D0D RID: 32013
		private const byte tagNsId = 11;

		// Token: 0x04007D0E RID: 32014
		internal const int ElementTypeIdConst = 10512;
	}
}

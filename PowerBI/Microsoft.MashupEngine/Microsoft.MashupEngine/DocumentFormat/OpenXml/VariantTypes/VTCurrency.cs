using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x0200292B RID: 10539
	[GeneratedCode("DomGen", "2.0")]
	internal class VTCurrency : OpenXmlLeafTextElement
	{
		// Token: 0x17006ADC RID: 27356
		// (get) Token: 0x06014DCB RID: 85451 RVA: 0x003181EC File Offset: 0x003163EC
		public override string LocalName
		{
			get
			{
				return "cy";
			}
		}

		// Token: 0x17006ADD RID: 27357
		// (get) Token: 0x06014DCC RID: 85452 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006ADE RID: 27358
		// (get) Token: 0x06014DCD RID: 85453 RVA: 0x003181F3 File Offset: 0x003163F3
		internal override int ElementTypeId
		{
			get
			{
				return 10989;
			}
		}

		// Token: 0x06014DCE RID: 85454 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DCF RID: 85455 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTCurrency()
		{
		}

		// Token: 0x06014DD0 RID: 85456 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTCurrency(string text)
			: base(text)
		{
		}

		// Token: 0x06014DD1 RID: 85457 RVA: 0x003181FC File Offset: 0x003163FC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DD2 RID: 85458 RVA: 0x00318217 File Offset: 0x00316417
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTCurrency>(deep);
		}

		// Token: 0x04009033 RID: 36915
		private const string tagName = "cy";

		// Token: 0x04009034 RID: 36916
		private const byte tagNsId = 5;

		// Token: 0x04009035 RID: 36917
		internal const int ElementTypeIdConst = 10989;
	}
}

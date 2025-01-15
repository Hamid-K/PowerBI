using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C7 RID: 455
	internal class XmlTextValue : XmlElementValue<string>
	{
		// Token: 0x06000D17 RID: 3351 RVA: 0x00025979 File Offset: 0x00023B79
		internal XmlTextValue(CsdlLocation textLocation, string textValue)
			: base("<\"Text\">", textLocation, textValue)
		{
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D18 RID: 3352 RVA: 0x0000268E File Offset: 0x0000088E
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D19 RID: 3353 RVA: 0x00025988 File Offset: 0x00023B88
		internal override string TextValue
		{
			get
			{
				return base.Value;
			}
		}

		// Token: 0x0400073E RID: 1854
		internal static readonly XmlTextValue Missing = new XmlTextValue(null, null);

		// Token: 0x0400073F RID: 1855
		internal const string ElementName = "<\"Text\">";
	}
}

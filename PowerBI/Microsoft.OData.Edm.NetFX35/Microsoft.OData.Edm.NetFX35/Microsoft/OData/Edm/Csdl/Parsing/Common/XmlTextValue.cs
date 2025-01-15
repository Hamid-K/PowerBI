using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x0200019F RID: 415
	internal class XmlTextValue : XmlElementValue<string>
	{
		// Token: 0x06000813 RID: 2067 RVA: 0x00013CAE File Offset: 0x00011EAE
		internal XmlTextValue(CsdlLocation textLocation, string textValue)
			: base("<\"Text\">", textLocation, textValue)
		{
		}

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x00013CBD File Offset: 0x00011EBD
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000352 RID: 850
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x00013CC0 File Offset: 0x00011EC0
		internal override string TextValue
		{
			get
			{
				return base.Value;
			}
		}

		// Token: 0x04000420 RID: 1056
		internal const string ElementName = "<\"Text\">";

		// Token: 0x04000421 RID: 1057
		internal static readonly XmlTextValue Missing = new XmlTextValue(null, null);
	}
}

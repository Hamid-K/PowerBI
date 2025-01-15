using System;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001BA RID: 442
	internal class XmlTextValue : XmlElementValue<string>
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x000237B1 File Offset: 0x000219B1
		internal XmlTextValue(CsdlLocation textLocation, string textValue)
			: base("<\"Text\">", textLocation, textValue)
		{
		}

		// Token: 0x170003F7 RID: 1015
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x00008D76 File Offset: 0x00006F76
		internal override bool IsText
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170003F8 RID: 1016
		// (get) Token: 0x06000C67 RID: 3175 RVA: 0x000237C0 File Offset: 0x000219C0
		internal override string TextValue
		{
			get
			{
				return base.Value;
			}
		}

		// Token: 0x040006C5 RID: 1733
		internal static readonly XmlTextValue Missing = new XmlTextValue(null, null);

		// Token: 0x040006C6 RID: 1734
		internal const string ElementName = "<\"Text\">";
	}
}

using System;
using System.Xml;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200007A RID: 122
	public sealed class XmlYearValue
	{
		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000521D File Offset: 0x0000341D
		// (set) Token: 0x0600022B RID: 555 RVA: 0x00005225 File Offset: 0x00003425
		[XmlText]
		public string Value { get; set; }

		// Token: 0x0600022C RID: 556 RVA: 0x00005230 File Offset: 0x00003430
		internal DataValue ToSemanticValue()
		{
			return new DateItemValue(new int?(XmlConvert.ToInt32(this.Value)), null, null, null, null, null, null);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00005288 File Offset: 0x00003488
		internal static XmlYearValue FromSemanticValue(DataValue value)
		{
			DateItemValue dateItemValue = (DateItemValue)value;
			return new XmlYearValue
			{
				Value = XmlConvert.ToString(dateItemValue.Value.Year.Value)
			};
		}
	}
}

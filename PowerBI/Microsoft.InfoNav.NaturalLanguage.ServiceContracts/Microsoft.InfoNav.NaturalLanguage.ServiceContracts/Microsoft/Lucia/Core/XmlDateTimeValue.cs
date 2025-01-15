using System;
using System.Xml.Serialization;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200007B RID: 123
	public sealed class XmlDateTimeValue
	{
		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600022F RID: 559 RVA: 0x000052C7 File Offset: 0x000034C7
		// (set) Token: 0x06000230 RID: 560 RVA: 0x000052CF File Offset: 0x000034CF
		[XmlText]
		public DateTime Value { get; set; }

		// Token: 0x06000231 RID: 561 RVA: 0x000052D8 File Offset: 0x000034D8
		internal DataValue ToSemanticValue()
		{
			return new DateItemValue(this.Value);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000052EC File Offset: 0x000034EC
		internal static XmlDateTimeValue FromSemanticValue(DataValue value)
		{
			DateItem value2 = ((DateItemValue)value).Value;
			return new XmlDateTimeValue
			{
				Value = new DateTime(value2.Year.Value, value2.Month.Value, value2.Day.Value, value2.Hour.Value, value2.Minute.Value, value2.Second.Value, value2.Millisecond.Value, DateTimeKind.Utc)
			};
		}
	}
}

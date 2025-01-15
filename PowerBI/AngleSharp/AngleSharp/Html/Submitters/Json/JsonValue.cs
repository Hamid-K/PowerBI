using System;
using System.Globalization;
using AngleSharp.Extensions;

namespace AngleSharp.Html.Submitters.Json
{
	// Token: 0x020000CB RID: 203
	internal sealed class JsonValue : JsonElement
	{
		// Token: 0x060005ED RID: 1517 RVA: 0x0002EE87 File Offset: 0x0002D087
		public JsonValue(string value)
		{
			this._value = value.CssString();
		}

		// Token: 0x060005EE RID: 1518 RVA: 0x0002EE9B File Offset: 0x0002D09B
		public JsonValue(double value)
		{
			this._value = value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060005EF RID: 1519 RVA: 0x0002EEB5 File Offset: 0x0002D0B5
		public JsonValue(bool value)
		{
			this._value = (value ? "true" : "false");
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x0002EED2 File Offset: 0x0002D0D2
		public override string ToString()
		{
			return this._value;
		}

		// Token: 0x040005F5 RID: 1525
		private readonly string _value;
	}
}

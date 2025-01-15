using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using Microsoft.OData.Json;

namespace Microsoft.OData
{
	// Token: 0x0200008E RID: 142
	[DebuggerDisplay("{Message}")]
	public sealed class ODataInnerError
	{
		// Token: 0x060004EE RID: 1262 RVA: 0x0000C464 File Offset: 0x0000A664
		public ODataInnerError()
		{
			this.Properties = new Dictionary<string, ODataValue>();
			this.Properties.Add("message", new ODataNullValue());
			this.Properties.Add("type", new ODataNullValue());
			this.Properties.Add("stacktrace", new ODataNullValue());
		}

		// Token: 0x060004EF RID: 1263 RVA: 0x0000C4C4 File Offset: 0x0000A6C4
		public ODataInnerError(Exception exception)
		{
			ExceptionUtils.CheckArgumentNotNull<Exception>(exception, "exception");
			if (exception.InnerException != null)
			{
				this.InnerError = new ODataInnerError(exception.InnerException);
			}
			this.Properties = new Dictionary<string, ODataValue>();
			this.Properties.Add("message", exception.Message.ToODataValue() ?? new ODataNullValue());
			this.Properties.Add("type", exception.GetType().FullName.ToODataValue() ?? new ODataNullValue());
			this.Properties.Add("stacktrace", exception.StackTrace.ToODataValue() ?? new ODataNullValue());
		}

		// Token: 0x060004F0 RID: 1264 RVA: 0x0000C578 File Offset: 0x0000A778
		public ODataInnerError(IDictionary<string, ODataValue> properties)
		{
			ExceptionUtils.CheckArgumentNotNull<IDictionary<string, ODataValue>>(properties, "properties");
			this.Properties = new Dictionary<string, ODataValue>(properties);
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x0000C598 File Offset: 0x0000A798
		// (set) Token: 0x060004F2 RID: 1266 RVA: 0x0000C5A0 File Offset: 0x0000A7A0
		public IDictionary<string, ODataValue> Properties { get; private set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0000C5A9 File Offset: 0x0000A7A9
		// (set) Token: 0x060004F4 RID: 1268 RVA: 0x0000C5B6 File Offset: 0x0000A7B6
		public string Message
		{
			get
			{
				return this.GetStringValue("message");
			}
			set
			{
				this.SetStringValue("message", value);
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		// (set) Token: 0x060004F6 RID: 1270 RVA: 0x0000C5D1 File Offset: 0x0000A7D1
		public string TypeName
		{
			get
			{
				return this.GetStringValue("type");
			}
			set
			{
				this.SetStringValue("type", value);
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x0000C5DF File Offset: 0x0000A7DF
		// (set) Token: 0x060004F8 RID: 1272 RVA: 0x0000C5EC File Offset: 0x0000A7EC
		public string StackTrace
		{
			get
			{
				return this.GetStringValue("stacktrace");
			}
			set
			{
				this.SetStringValue("stacktrace", value);
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004F9 RID: 1273 RVA: 0x0000C5FA File Offset: 0x0000A7FA
		// (set) Token: 0x060004FA RID: 1274 RVA: 0x0000C602 File Offset: 0x0000A802
		public ODataInnerError InnerError { get; set; }

		// Token: 0x060004FB RID: 1275 RVA: 0x0000C60C File Offset: 0x0000A80C
		internal string ToJson()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (KeyValuePair<string, ODataValue> keyValuePair in this.Properties)
			{
				if (!(keyValuePair.Key == "message") && !(keyValuePair.Key == "stacktrace") && !(keyValuePair.Key == "type") && !(keyValuePair.Key == "internalexception"))
				{
					stringBuilder.Append(",");
					stringBuilder.Append("\"").Append(keyValuePair.Key).Append("\"")
						.Append(":");
					ODataJsonWriterUtils.ODataValueToString(stringBuilder, keyValuePair.Value);
				}
			}
			return string.Format(CultureInfo.InvariantCulture, "{{\"message\":\"{0}\",\"type\":\"{1}\",\"stacktrace\":\"{2}\",\"innererror\":{3}{4}}}", new object[]
			{
				(this.Message == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.Message),
				(this.TypeName == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.TypeName),
				(this.StackTrace == null) ? "" : JsonValueUtils.GetEscapedJsonString(this.StackTrace),
				(this.InnerError == null) ? "{}" : this.InnerError.ToJson(),
				stringBuilder.ToString()
			});
		}

		// Token: 0x060004FC RID: 1276 RVA: 0x0000C784 File Offset: 0x0000A984
		private string GetStringValue(string propertyKey)
		{
			if (!this.Properties.ContainsKey(propertyKey))
			{
				return string.Empty;
			}
			object obj = this.Properties[propertyKey].FromODataValue();
			if (obj == null)
			{
				return null;
			}
			return obj.ToString();
		}

		// Token: 0x060004FD RID: 1277 RVA: 0x0000C7B8 File Offset: 0x0000A9B8
		private void SetStringValue(string propertyKey, string value)
		{
			ODataValue odataValue = ((value == null) ? new ODataNullValue() : value.ToODataValue());
			if (!this.Properties.ContainsKey(propertyKey))
			{
				this.Properties.Add(propertyKey, odataValue);
				return;
			}
			this.Properties[propertyKey] = odataValue;
		}
	}
}

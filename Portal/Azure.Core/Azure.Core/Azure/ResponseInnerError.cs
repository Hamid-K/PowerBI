using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Azure
{
	// Token: 0x02000030 RID: 48
	[NullableContext(2)]
	[Nullable(0)]
	[JsonConverter(typeof(ResponseInnerError.Converter))]
	internal sealed class ResponseInnerError
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00003B74 File Offset: 0x00001D74
		internal ResponseInnerError(string code, ResponseInnerError innerError, JsonElement innerErrorElement)
		{
			this._innerErrorElement = innerErrorElement;
			this.Code = code;
			this.InnerError = innerError;
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00003B91 File Offset: 0x00001D91
		public string Code { get; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00003B99 File Offset: 0x00001D99
		public ResponseInnerError InnerError { get; }

		// Token: 0x06000108 RID: 264 RVA: 0x00003BA4 File Offset: 0x00001DA4
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.Append(stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003BC4 File Offset: 0x00001DC4
		[NullableContext(1)]
		internal void Append(StringBuilder builder)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}", this.Code, Environment.NewLine);
			if (this.InnerError != null)
			{
				builder.AppendLine("Inner Error:");
				builder.Append(this.InnerError);
			}
		}

		// Token: 0x0400005D RID: 93
		private readonly JsonElement _innerErrorElement;

		// Token: 0x020000DD RID: 221
		[NullableContext(1)]
		[Nullable(new byte[] { 0, 2 })]
		internal class Converter : JsonConverter<ResponseInnerError>
		{
			// Token: 0x06000706 RID: 1798 RVA: 0x00018034 File Offset: 0x00016234
			[return: Nullable(2)]
			public override ResponseInnerError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				ResponseInnerError responseInnerError;
				using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
				{
					responseInnerError = ResponseInnerError.Converter.Read(jsonDocument.RootElement);
				}
				return responseInnerError;
			}

			// Token: 0x06000707 RID: 1799 RVA: 0x00018074 File Offset: 0x00016274
			[NullableContext(2)]
			internal static ResponseInnerError Read(JsonElement element)
			{
				if (element.ValueKind == 7)
				{
					return null;
				}
				string text = null;
				JsonElement jsonElement;
				if (element.TryGetProperty("code", ref jsonElement))
				{
					text = jsonElement.GetString();
				}
				ResponseInnerError responseInnerError = null;
				if (element.TryGetProperty("innererror", ref jsonElement))
				{
					responseInnerError = ResponseInnerError.Converter.Read(jsonElement);
				}
				return new ResponseInnerError(text, responseInnerError, element.Clone());
			}

			// Token: 0x06000708 RID: 1800 RVA: 0x000180CE File Offset: 0x000162CE
			public override void Write(Utf8JsonWriter writer, [Nullable(2)] ResponseInnerError value, JsonSerializerOptions options)
			{
				throw new NotImplementedException();
			}
		}
	}
}

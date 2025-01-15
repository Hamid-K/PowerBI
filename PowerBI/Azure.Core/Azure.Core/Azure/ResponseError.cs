using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200002F RID: 47
	[NullableContext(2)]
	[Nullable(0)]
	[JsonConverter(typeof(ResponseError.Converter))]
	[TypeReferenceType(true, new string[] { "Target", "Details" })]
	public sealed class ResponseError
	{
		// Token: 0x060000FC RID: 252 RVA: 0x000039A8 File Offset: 0x00001BA8
		[InitializationConstructor]
		public ResponseError(string code, string message)
			: this(code, message, null, default(JsonElement), null, null)
		{
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000039C9 File Offset: 0x00001BC9
		[SerializationConstructor]
		internal ResponseError(string code, string message, string target, JsonElement element, ResponseInnerError innerError = null, [Nullable(new byte[] { 2, 1 })] IReadOnlyList<ResponseError> details = null)
		{
			this._element = element;
			this.Code = code;
			this.Message = message;
			this.InnerError = innerError;
			this.Target = target;
			this.Details = details ?? Array.Empty<ResponseError>();
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000FE RID: 254 RVA: 0x00003A07 File Offset: 0x00001C07
		public string Code { get; }

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000FF RID: 255 RVA: 0x00003A0F File Offset: 0x00001C0F
		public string Message { get; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000100 RID: 256 RVA: 0x00003A17 File Offset: 0x00001C17
		internal ResponseInnerError InnerError { get; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00003A1F File Offset: 0x00001C1F
		internal string Target { get; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000102 RID: 258 RVA: 0x00003A27 File Offset: 0x00001C27
		[Nullable(1)]
		internal IReadOnlyList<ResponseError> Details
		{
			[NullableContext(1)]
			get;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00003A30 File Offset: 0x00001C30
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			this.Append(stringBuilder, true);
			return stringBuilder.ToString();
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00003A54 File Offset: 0x00001C54
		[NullableContext(1)]
		internal void Append(StringBuilder builder, bool includeRaw)
		{
			builder.AppendFormat(CultureInfo.InvariantCulture, "{0}: {1}{2}", this.Code, this.Message, Environment.NewLine);
			if (this.Target != null)
			{
				builder.AppendFormat(CultureInfo.InvariantCulture, "Target: {0}{1}", this.Target, Environment.NewLine);
			}
			ResponseInnerError responseInnerError = this.InnerError;
			if (responseInnerError != null)
			{
				builder.AppendLine();
				builder.AppendLine("Inner Errors:");
				while (responseInnerError != null)
				{
					builder.AppendLine(responseInnerError.Code);
					responseInnerError = responseInnerError.InnerError;
				}
			}
			if (this.Details.Count > 0)
			{
				builder.AppendLine();
				builder.AppendLine("Details:");
				foreach (ResponseError responseError in this.Details)
				{
					responseError.Append(builder, false);
				}
			}
			if (includeRaw && this._element.ValueKind != null)
			{
				builder.AppendLine();
				builder.AppendLine("Raw:");
				builder.Append(this._element.GetRawText());
			}
		}

		// Token: 0x04000057 RID: 87
		private readonly JsonElement _element;

		// Token: 0x020000DC RID: 220
		[NullableContext(1)]
		[Nullable(new byte[] { 0, 2 })]
		internal class Converter : JsonConverter<ResponseError>
		{
			// Token: 0x06000702 RID: 1794 RVA: 0x00017ECC File Offset: 0x000160CC
			[return: Nullable(2)]
			public override ResponseError Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
			{
				ResponseError responseError;
				using (JsonDocument jsonDocument = JsonDocument.ParseValue(ref reader))
				{
					responseError = ResponseError.Converter.Read(jsonDocument.RootElement);
				}
				return responseError;
			}

			// Token: 0x06000703 RID: 1795 RVA: 0x00017F0C File Offset: 0x0001610C
			[NullableContext(2)]
			private static ResponseError Read(JsonElement element)
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
				string text2 = null;
				if (element.TryGetProperty("message", ref jsonElement))
				{
					text2 = jsonElement.GetString();
				}
				string text3 = null;
				if (element.TryGetProperty("target", ref jsonElement))
				{
					text3 = jsonElement.GetString();
				}
				ResponseInnerError responseInnerError = null;
				if (element.TryGetProperty("innererror", ref jsonElement))
				{
					responseInnerError = ResponseInnerError.Converter.Read(jsonElement);
				}
				List<ResponseError> list = null;
				if (element.TryGetProperty("details", ref jsonElement) && jsonElement.ValueKind == 2)
				{
					foreach (JsonElement jsonElement2 in jsonElement.EnumerateArray())
					{
						ResponseError responseError = ResponseError.Converter.Read(jsonElement2);
						if (responseError != null)
						{
							if (list == null)
							{
								list = new List<ResponseError>();
							}
							list.Add(responseError);
						}
					}
				}
				return new ResponseError(text, text2, text3, element.Clone(), responseInnerError, list);
			}

			// Token: 0x06000704 RID: 1796 RVA: 0x00018024 File Offset: 0x00016224
			public override void Write(Utf8JsonWriter writer, [Nullable(2)] ResponseError value, JsonSerializerOptions options)
			{
				throw new NotImplementedException();
			}
		}
	}
}

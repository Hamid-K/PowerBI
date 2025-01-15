using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure
{
	// Token: 0x0200002D RID: 45
	[NullableContext(1)]
	[Nullable(0)]
	[Serializable]
	public class RequestFailedException : Exception, ISerializable
	{
		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00003406 File Offset: 0x00001606
		public int Status { get; }

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x0000340E File Offset: 0x0000160E
		[Nullable(2)]
		public string ErrorCode
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00003416 File Offset: 0x00001616
		public RequestFailedException(string message)
			: this(0, message)
		{
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00003420 File Offset: 0x00001620
		public RequestFailedException(string message, [Nullable(2)] Exception innerException)
			: this(0, message, innerException)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x0000342B File Offset: 0x0000162B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, string message)
			: this(status, message, null)
		{
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00003436 File Offset: 0x00001636
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, string message, [Nullable(2)] Exception innerException)
			: this(status, message, null, innerException)
		{
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00003442 File Offset: 0x00001642
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public RequestFailedException(int status, [Nullable(1)] string message, string errorCode, Exception innerException)
			: base(message, innerException)
		{
			this.Status = status;
			this.ErrorCode = errorCode;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x0000345B File Offset: 0x0000165B
		internal RequestFailedException(int status, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Message", "Error" })] [Nullable(new byte[] { 0, 1, 2 })] global::System.ValueTuple<string, ResponseError> details)
		{
			string item = details.Item1;
			ResponseError item2 = details.Item2;
			this..ctor(status, item, (item2 != null) ? item2.Code : null, null);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003480 File Offset: 0x00001680
		[NullableContext(2)]
		internal RequestFailedException(int status, [global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "FormatMessage", "ErrorCode", "Data" })] [Nullable(new byte[] { 0, 1, 2, 2, 1, 1 })] global::System.ValueTuple<string, string, IDictionary<string, string>> details, Exception innerException)
			: this(status, details.Item1, details.Item2, innerException)
		{
			if (details.Item3 != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in details.Item3)
				{
					this.Data.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000034FC File Offset: 0x000016FC
		public RequestFailedException(Response response)
			: this(response, null)
		{
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003506 File Offset: 0x00001706
		public RequestFailedException(Response response, [Nullable(2)] Exception innerException)
			: this(response, innerException, null)
		{
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003511 File Offset: 0x00001711
		[NullableContext(2)]
		public RequestFailedException([Nullable(1)] Response response, Exception innerException, RequestFailedDetailsParser detailsParser)
			: this(response.Status, RequestFailedException.GetRequestFailedExceptionContent(response, detailsParser), innerException)
		{
			this._response = response;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000352E File Offset: 0x0000172E
		protected RequestFailedException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
			this.Status = info.GetInt32("Status");
			this.ErrorCode = info.GetString("ErrorCode");
		}

		// Token: 0x060000DF RID: 223 RVA: 0x0000355A File Offset: 0x0000175A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			Argument.AssertNotNull<SerializationInfo>(info, "info");
			info.AddValue("Status", this.Status);
			info.AddValue("ErrorCode", this.ErrorCode);
			base.GetObjectData(info, context);
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003591 File Offset: 0x00001791
		[NullableContext(2)]
		public Response GetRawResponse()
		{
			return this._response;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000359C File Offset: 0x0000179C
		[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "FormattedError", "ErrorCode", "Data" })]
		[return: Nullable(new byte[] { 0, 1, 2, 2, 1, 1 })]
		internal static global::System.ValueTuple<string, string, IDictionary<string, string>> GetRequestFailedExceptionContent(Response response, [Nullable(2)] RequestFailedDetailsParser parser)
		{
			RequestFailedException.BufferResponseIfNeeded(response);
			if (parser == null)
			{
				parser = response.RequestFailedDetailsParser;
			}
			ResponseError responseError;
			IDictionary<string, string> dictionary;
			if (!((parser == null) ? RequestFailedException.TryExtractErrorContent(response, out responseError, out dictionary) : parser.TryParse(response, out responseError, out dictionary)))
			{
				responseError = null;
				dictionary = null;
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(((responseError != null) ? responseError.Message : null) ?? "Service request failed.").Append("Status: ").Append(response.Status.ToString(CultureInfo.InvariantCulture));
			if (!string.IsNullOrEmpty(response.ReasonPhrase))
			{
				stringBuilder.Append(" (").Append(response.ReasonPhrase).AppendLine(")");
			}
			else
			{
				stringBuilder.AppendLine();
			}
			if (!string.IsNullOrWhiteSpace((responseError != null) ? responseError.Code : null))
			{
				stringBuilder.Append("ErrorCode: ").Append((responseError != null) ? responseError.Code : null).AppendLine();
			}
			if (dictionary != null && dictionary.Count > 0)
			{
				stringBuilder.AppendLine().AppendLine("Additional Information:");
				foreach (KeyValuePair<string, string> keyValuePair in dictionary)
				{
					stringBuilder.Append(keyValuePair.Key).Append(": ").AppendLine(keyValuePair.Value);
				}
			}
			Encoding encoding;
			if (response.ContentStream is MemoryStream && ContentTypeUtilities.TryGetTextEncoding(response.Headers.ContentType, out encoding))
			{
				stringBuilder.AppendLine().AppendLine("Content:").AppendLine(response.Content.ToString());
			}
			stringBuilder.AppendLine().AppendLine("Headers:");
			foreach (HttpHeader httpHeader in response.Headers)
			{
				string text = response.Sanitizer.SanitizeHeader(httpHeader.Name, httpHeader.Value);
				string text2 = httpHeader.Name + ": " + text;
				stringBuilder.AppendLine(text2);
			}
			return new global::System.ValueTuple<string, string, IDictionary<string, string>>(stringBuilder.ToString(), (responseError != null) ? responseError.Code : null, dictionary);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000037EC File Offset: 0x000019EC
		private static void BufferResponseIfNeeded(Response response)
		{
			Stream contentStream = response.ContentStream;
			if (contentStream == null || contentStream is MemoryStream)
			{
				return;
			}
			MemoryStream memoryStream = new MemoryStream();
			response.ContentStream.CopyTo(memoryStream);
			response.ContentStream.Dispose();
			memoryStream.Position = 0L;
			response.ContentStream = memoryStream;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00003838 File Offset: 0x00001A38
		internal static bool TryExtractErrorContent(Response response, [Nullable(2)] out ResponseError error, [Nullable(new byte[] { 2, 1, 1 })] out IDictionary<string, string> data)
		{
			error = null;
			data = null;
			try
			{
				string text = response.Content.ToString();
				if (text == null || !text.StartsWith("{", StringComparison.OrdinalIgnoreCase))
				{
					return false;
				}
				RequestFailedException.ErrorResponse errorResponse = JsonSerializer.Deserialize<RequestFailedException.ErrorResponse>(text, null);
				error = ((errorResponse != null) ? errorResponse.Error : null);
				if (error == null)
				{
					error = JsonSerializer.Deserialize<ResponseError>(text, null);
				}
			}
			catch (Exception)
			{
			}
			return error != null;
		}

		// Token: 0x0400004F RID: 79
		private const string DefaultMessage = "Service request failed.";

		// Token: 0x04000052 RID: 82
		[Nullable(2)]
		private readonly Response _response;

		// Token: 0x020000DB RID: 219
		[NullableContext(2)]
		[Nullable(0)]
		internal class ErrorResponse
		{
			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060006FF RID: 1791 RVA: 0x00017EB1 File Offset: 0x000160B1
			// (set) Token: 0x06000700 RID: 1792 RVA: 0x00017EB9 File Offset: 0x000160B9
			[JsonPropertyName("error")]
			public ResponseError Error { get; set; }
		}
	}
}

using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace System.Web.Http.Results
{
	// Token: 0x020000AF RID: 175
	public class JsonResult<T> : IHttpActionResult
	{
		// Token: 0x06000433 RID: 1075 RVA: 0x0000C209 File Offset: 0x0000A409
		public JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, HttpRequestMessage request)
			: this(content, serializerSettings, encoding, new StatusCodeResult.DirectDependencyProvider(request))
		{
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x0000C21B File Offset: 0x0000A41B
		public JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, ApiController controller)
			: this(content, serializerSettings, encoding, new StatusCodeResult.ApiControllerDependencyProvider(controller))
		{
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x0000C230 File Offset: 0x0000A430
		private JsonResult(T content, JsonSerializerSettings serializerSettings, Encoding encoding, StatusCodeResult.IDependencyProvider dependencies)
		{
			if (serializerSettings == null)
			{
				throw new ArgumentNullException("serializerSettings");
			}
			if (encoding == null)
			{
				throw new ArgumentNullException("encoding");
			}
			this._content = content;
			this._serializerSettings = serializerSettings;
			this._encoding = encoding;
			this._dependencies = dependencies;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000436 RID: 1078 RVA: 0x0000C27C File Offset: 0x0000A47C
		public T Content
		{
			get
			{
				return this._content;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000437 RID: 1079 RVA: 0x0000C284 File Offset: 0x0000A484
		public JsonSerializerSettings SerializerSettings
		{
			get
			{
				return this._serializerSettings;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000438 RID: 1080 RVA: 0x0000C28C File Offset: 0x0000A48C
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000439 RID: 1081 RVA: 0x0000C294 File Offset: 0x0000A494
		public HttpRequestMessage Request
		{
			get
			{
				return this._dependencies.Request;
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x0000C2A1 File Offset: 0x0000A4A1
		public virtual Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<HttpResponseMessage>(this.Execute());
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		private HttpResponseMessage Execute()
		{
			HttpResponseMessage httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
			try
			{
				ArraySegment<byte> arraySegment = this.Serialize();
				httpResponseMessage.Content = new ByteArrayContent(arraySegment.Array, arraySegment.Offset, arraySegment.Count);
				MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue("application/json");
				mediaTypeHeaderValue.CharSet = this._encoding.WebName;
				httpResponseMessage.Content.Headers.ContentType = mediaTypeHeaderValue;
				httpResponseMessage.RequestMessage = this._dependencies.Request;
			}
			catch
			{
				httpResponseMessage.Dispose();
				throw;
			}
			return httpResponseMessage;
		}

		// Token: 0x0600043C RID: 1084 RVA: 0x0000C34C File Offset: 0x0000A54C
		private ArraySegment<byte> Serialize()
		{
			JsonSerializer jsonSerializer = JsonSerializer.Create(this._serializerSettings);
			ArraySegment<byte> arraySegment;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (TextWriter textWriter = new StreamWriter(memoryStream, this._encoding, 1024, true))
				{
					using (JsonWriter jsonWriter = new JsonTextWriter(textWriter)
					{
						CloseOutput = false
					})
					{
						jsonSerializer.Serialize(jsonWriter, this._content);
						jsonWriter.Flush();
					}
				}
				arraySegment = new ArraySegment<byte>(memoryStream.GetBuffer(), 0, (int)memoryStream.Length);
			}
			return arraySegment;
		}

		// Token: 0x0400010B RID: 267
		private readonly T _content;

		// Token: 0x0400010C RID: 268
		private readonly JsonSerializerSettings _serializerSettings;

		// Token: 0x0400010D RID: 269
		private readonly Encoding _encoding;

		// Token: 0x0400010E RID: 270
		private readonly StatusCodeResult.IDependencyProvider _dependencies;
	}
}

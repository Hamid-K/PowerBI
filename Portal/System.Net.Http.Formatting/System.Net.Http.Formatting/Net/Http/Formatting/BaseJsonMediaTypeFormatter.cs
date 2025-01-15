using System;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000032 RID: 50
	public abstract class BaseJsonMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x060001F5 RID: 501 RVA: 0x00006954 File Offset: 0x00004B54
		protected BaseJsonMediaTypeFormatter()
		{
			this._defaultContractResolver = new JsonContractResolver(this);
			this._jsonSerializerSettings = this.CreateDefaultSerializerSettings();
			base.SupportedEncodings.Add(new UTF8Encoding(false, true));
			base.SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000069AF File Offset: 0x00004BAF
		protected BaseJsonMediaTypeFormatter(BaseJsonMediaTypeFormatter formatter)
			: base(formatter)
		{
			this.SerializerSettings = formatter.SerializerSettings;
			this.MaxDepth = formatter._maxDepth;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000069DB File Offset: 0x00004BDB
		// (set) Token: 0x060001F8 RID: 504 RVA: 0x000069E3 File Offset: 0x00004BE3
		public JsonSerializerSettings SerializerSettings
		{
			get
			{
				return this._jsonSerializerSettings;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._jsonSerializerSettings = value;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000069FA File Offset: 0x00004BFA
		// (set) Token: 0x060001FA RID: 506 RVA: 0x00006A02 File Offset: 0x00004C02
		public virtual int MaxDepth
		{
			get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006A26 File Offset: 0x00004C26
		public JsonSerializerSettings CreateDefaultSerializerSettings()
		{
			return new JsonSerializerSettings
			{
				ContractResolver = this._defaultContractResolver,
				MissingMemberHandling = 0,
				TypeNameHandling = 0
			};
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006A47 File Offset: 0x00004C47
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return true;
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00006A47 File Offset: 0x00004C47
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return true;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00006A60 File Offset: 0x00004C60
		public override Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			Task<object> task;
			try
			{
				task = Task.FromResult<object>(this.ReadFromStream(type, readStream, content, formatterLogger));
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError<object>(ex);
			}
			return task;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00006ABC File Offset: 0x00004CBC
		private object ReadFromStream(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			HttpContentHeaders httpContentHeaders = ((content == null) ? null : content.Headers);
			if (httpContentHeaders != null)
			{
				long? contentLength = httpContentHeaders.ContentLength;
				long num = 0L;
				if ((contentLength.GetValueOrDefault() == num) & (contentLength != null))
				{
					return MediaTypeFormatter.GetDefaultValueForType(type);
				}
			}
			Encoding encoding = base.SelectCharacterEncoding(httpContentHeaders);
			object obj;
			try
			{
				obj = this.ReadFromStream(type, readStream, encoding, formatterLogger);
			}
			catch (Exception ex)
			{
				if (formatterLogger == null)
				{
					throw;
				}
				formatterLogger.LogError(string.Empty, ex);
				obj = MediaTypeFormatter.GetDefaultValueForType(type);
			}
			return obj;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x00006B48 File Offset: 0x00004D48
		public virtual object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (readStream == null)
			{
				throw Error.ArgumentNull("readStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			object obj;
			using (JsonReader jsonReader = this.CreateJsonReaderInternal(type, readStream, effectiveEncoding))
			{
				jsonReader.CloseInput = false;
				jsonReader.MaxDepth = new int?(this._maxDepth);
				JsonSerializer jsonSerializer = this.CreateJsonSerializerInternal();
				EventHandler<ErrorEventArgs> eventHandler = null;
				if (formatterLogger != null)
				{
					eventHandler = delegate(object sender, ErrorEventArgs e)
					{
						Exception error = e.ErrorContext.Error;
						formatterLogger.LogError(e.ErrorContext.Path, error);
						e.ErrorContext.Handled = true;
					};
					jsonSerializer.Error += eventHandler;
				}
				try
				{
					obj = jsonSerializer.Deserialize(jsonReader, type);
				}
				finally
				{
					if (eventHandler != null)
					{
						jsonSerializer.Error -= eventHandler;
					}
				}
			}
			return obj;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00006C1C File Offset: 0x00004E1C
		private JsonReader CreateJsonReaderInternal(Type type, Stream readStream, Encoding effectiveEncoding)
		{
			JsonReader jsonReader = this.CreateJsonReader(type, readStream, effectiveEncoding);
			if (jsonReader == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_JsonReaderFactoryReturnedNull, new object[] { "CreateJsonReader" });
			}
			return jsonReader;
		}

		// Token: 0x06000202 RID: 514
		public abstract JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding);

		// Token: 0x06000203 RID: 515 RVA: 0x00006C50 File Offset: 0x00004E50
		private JsonSerializer CreateJsonSerializerInternal()
		{
			JsonSerializer jsonSerializer = null;
			try
			{
				jsonSerializer = this.CreateJsonSerializer();
			}
			catch (Exception ex)
			{
				throw Error.InvalidOperation(ex, Resources.JsonSerializerFactoryThrew, new object[] { "CreateJsonSerializer" });
			}
			if (jsonSerializer == null)
			{
				throw Error.InvalidOperation(Resources.JsonSerializerFactoryReturnedNull, new object[] { "CreateJsonSerializer" });
			}
			return jsonSerializer;
		}

		// Token: 0x06000204 RID: 516 RVA: 0x00006CB0 File Offset: 0x00004EB0
		public virtual JsonSerializer CreateJsonSerializer()
		{
			return JsonSerializer.Create(this.SerializerSettings);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x00006CC0 File Offset: 0x00004EC0
		public override Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled();
			}
			Task task;
			try
			{
				this.WriteToStream(type, value, writeStream, content);
				task = TaskHelpers.Completed();
			}
			catch (Exception ex)
			{
				task = TaskHelpers.FromError(ex);
			}
			return task;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00006D2C File Offset: 0x00004F2C
		private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			Encoding encoding = base.SelectCharacterEncoding((content == null) ? null : content.Headers);
			this.WriteToStream(type, value, writeStream, encoding);
		}

		// Token: 0x06000207 RID: 519 RVA: 0x00006D58 File Offset: 0x00004F58
		public virtual void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (writeStream == null)
			{
				throw Error.ArgumentNull("writeStream");
			}
			if (effectiveEncoding == null)
			{
				throw Error.ArgumentNull("effectiveEncoding");
			}
			using (JsonWriter jsonWriter = this.CreateJsonWriterInternal(type, writeStream, effectiveEncoding))
			{
				jsonWriter.CloseOutput = false;
				this.CreateJsonSerializerInternal().Serialize(jsonWriter, value);
				jsonWriter.Flush();
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00006DD8 File Offset: 0x00004FD8
		private JsonWriter CreateJsonWriterInternal(Type type, Stream writeStream, Encoding effectiveEncoding)
		{
			JsonWriter jsonWriter = this.CreateJsonWriter(type, writeStream, effectiveEncoding);
			if (jsonWriter == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_JsonWriterFactoryReturnedNull, new object[] { "CreateJsonWriter" });
			}
			return jsonWriter;
		}

		// Token: 0x06000209 RID: 521
		public abstract JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding);

		// Token: 0x04000096 RID: 150
		private int _maxDepth = 256;

		// Token: 0x04000097 RID: 151
		private readonly IContractResolver _defaultContractResolver;

		// Token: 0x04000098 RID: 152
		private JsonSerializerSettings _jsonSerializerSettings;
	}
}

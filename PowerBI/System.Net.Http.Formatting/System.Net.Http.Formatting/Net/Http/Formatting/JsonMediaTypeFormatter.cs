using System;
using System.Collections.Concurrent;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using Newtonsoft.Json;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000042 RID: 66
	public class JsonMediaTypeFormatter : BaseJsonMediaTypeFormatter
	{
		// Token: 0x06000274 RID: 628 RVA: 0x00008B74 File Offset: 0x00006D74
		public JsonMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationJsonMediaType);
			base.SupportedMediaTypes.Add(MediaTypeConstants.TextJsonMediaType);
			this._requestHeaderMapping = new XmlHttpRequestHeaderMapping();
			base.MediaTypeMappings.Add(this._requestHeaderMapping);
		}

		// Token: 0x06000275 RID: 629 RVA: 0x00008BD9 File Offset: 0x00006DD9
		protected JsonMediaTypeFormatter(JsonMediaTypeFormatter formatter)
			: base(formatter)
		{
			this.UseDataContractJsonSerializer = formatter.UseDataContractJsonSerializer;
			this.Indent = formatter.Indent;
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000276 RID: 630 RVA: 0x00008C10 File Offset: 0x00006E10
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationJsonMediaType;
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000277 RID: 631 RVA: 0x00008C17 File Offset: 0x00006E17
		// (set) Token: 0x06000278 RID: 632 RVA: 0x00008C1F File Offset: 0x00006E1F
		public bool UseDataContractJsonSerializer { get; set; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000279 RID: 633 RVA: 0x00008C28 File Offset: 0x00006E28
		// (set) Token: 0x0600027A RID: 634 RVA: 0x00008C30 File Offset: 0x00006E30
		public bool Indent { get; set; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600027B RID: 635 RVA: 0x00006E34 File Offset: 0x00005034
		// (set) Token: 0x0600027C RID: 636 RVA: 0x00008C39 File Offset: 0x00006E39
		public sealed override int MaxDepth
		{
			get
			{
				return base.MaxDepth;
			}
			set
			{
				base.MaxDepth = value;
				this._readerQuotas.MaxDepth = value;
			}
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00008C4E File Offset: 0x00006E4E
		public override JsonReader CreateJsonReader(Type type, Stream readStream, Encoding effectiveEncoding)
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
			return new JsonTextReader(new StreamReader(readStream, effectiveEncoding));
		}

		// Token: 0x0600027E RID: 638 RVA: 0x00008C8C File Offset: 0x00006E8C
		public override JsonWriter CreateJsonWriter(Type type, Stream writeStream, Encoding effectiveEncoding)
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
			JsonWriter jsonWriter = new JsonTextWriter(new StreamWriter(writeStream, effectiveEncoding));
			if (this.Indent)
			{
				jsonWriter.Formatting = 1;
			}
			return jsonWriter;
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00008CE8 File Offset: 0x00006EE8
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseDataContractJsonSerializer)
			{
				return this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(t, false)) != null;
			}
			return base.CanReadType(type);
		}

		// Token: 0x06000280 RID: 640 RVA: 0x00008D34 File Offset: 0x00006F34
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseDataContractJsonSerializer)
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
				return this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(t, false)) != null;
			}
			return base.CanWriteType(type);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00008D88 File Offset: 0x00006F88
		public override object ReadFromStream(Type type, Stream readStream, Encoding effectiveEncoding, IFormatterLogger formatterLogger)
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
			if (this.UseDataContractJsonSerializer)
			{
				DataContractJsonSerializer dataContractSerializer = this.GetDataContractSerializer(type);
				using (XmlReader xmlReader = JsonReaderWriterFactory.CreateJsonReader(new NonClosingDelegatingStream(readStream), effectiveEncoding, this._readerQuotas, null))
				{
					return dataContractSerializer.ReadObject(xmlReader);
				}
			}
			return base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00008E1C File Offset: 0x0000701C
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
			if (this.UseDataContractJsonSerializer && this.Indent)
			{
				throw Error.NotSupported(Resources.UnsupportedIndent, new object[] { typeof(DataContractJsonSerializer) });
			}
			return base.WriteToStreamAsync(type, value, writeStream, content, transportContext, cancellationToken);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x00008E88 File Offset: 0x00007088
		public override void WriteToStream(Type type, object value, Stream writeStream, Encoding effectiveEncoding)
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
			if (this.UseDataContractJsonSerializer)
			{
				if (MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type) && value != null)
				{
					value = MediaTypeFormatter.GetTypeRemappingConstructor(type).Invoke(new object[] { value });
				}
				DataContractJsonSerializer dataContractSerializer = this.GetDataContractSerializer(type);
				using (XmlWriter xmlWriter = JsonReaderWriterFactory.CreateJsonWriter(writeStream, effectiveEncoding, false))
				{
					dataContractSerializer.WriteObject(xmlWriter, value);
					return;
				}
			}
			base.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x00008F34 File Offset: 0x00007134
		private DataContractJsonSerializer CreateDataContractSerializer(Type type, bool throwOnError)
		{
			DataContractJsonSerializer dataContractJsonSerializer = null;
			Exception ex = null;
			try
			{
				FormattingUtilities.XsdDataContractExporter.GetRootElementName(type);
				dataContractJsonSerializer = this.CreateDataContractSerializer(type);
			}
			catch (Exception ex)
			{
			}
			if (dataContractJsonSerializer != null || !throwOnError)
			{
				return dataContractJsonSerializer;
			}
			if (ex != null)
			{
				throw Error.InvalidOperation(ex, Resources.SerializerCannotSerializeType, new object[]
				{
					typeof(DataContractJsonSerializer).Name,
					type.Name
				});
			}
			throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
			{
				typeof(DataContractJsonSerializer).Name,
				type.Name
			});
		}

		// Token: 0x06000285 RID: 645 RVA: 0x00008FD4 File Offset: 0x000071D4
		public virtual DataContractJsonSerializer CreateDataContractSerializer(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return new DataContractJsonSerializer(type);
		}

		// Token: 0x06000286 RID: 646 RVA: 0x00008FF0 File Offset: 0x000071F0
		private DataContractJsonSerializer GetDataContractSerializer(Type type)
		{
			DataContractJsonSerializer orAdd = this._dataContractSerializerCache.GetOrAdd(type, (Type t) => this.CreateDataContractSerializer(type, true));
			if (orAdd == null)
			{
				throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
				{
					typeof(DataContractJsonSerializer).Name,
					type.Name
				});
			}
			return orAdd;
		}

		// Token: 0x040000B1 RID: 177
		private ConcurrentDictionary<Type, DataContractJsonSerializer> _dataContractSerializerCache = new ConcurrentDictionary<Type, DataContractJsonSerializer>();

		// Token: 0x040000B2 RID: 178
		private XmlDictionaryReaderQuotas _readerQuotas = FormattingUtilities.CreateDefaultReaderQuotas();

		// Token: 0x040000B3 RID: 179
		private RequestHeaderMapping _requestHeaderMapping;
	}
}

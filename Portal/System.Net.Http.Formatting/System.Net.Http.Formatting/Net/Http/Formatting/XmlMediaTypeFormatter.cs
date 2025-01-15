using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Internal;
using System.Net.Http.Properties;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Xml;
using System.Xml.Serialization;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000050 RID: 80
	public class XmlMediaTypeFormatter : MediaTypeFormatter
	{
		// Token: 0x06000300 RID: 768 RVA: 0x0000A22C File Offset: 0x0000842C
		public XmlMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationXmlMediaType);
			base.SupportedMediaTypes.Add(MediaTypeConstants.TextXmlMediaType);
			base.SupportedEncodings.Add(new UTF8Encoding(false, true));
			base.SupportedEncodings.Add(new UnicodeEncoding(false, true, true));
			this.WriterSettings = new XmlWriterSettings
			{
				OmitXmlDeclaration = true,
				CloseOutput = false,
				CheckCharacters = false
			};
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000A2BC File Offset: 0x000084BC
		protected XmlMediaTypeFormatter(XmlMediaTypeFormatter formatter)
			: base(formatter)
		{
			this.UseXmlSerializer = formatter.UseXmlSerializer;
			this.WriterSettings = formatter.WriterSettings;
			this.MaxDepth = formatter.MaxDepth;
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000A30A File Offset: 0x0000850A
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationXmlMediaType;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000A311 File Offset: 0x00008511
		// (set) Token: 0x06000304 RID: 772 RVA: 0x0000A319 File Offset: 0x00008519
		[DefaultValue(false)]
		public bool UseXmlSerializer { get; set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000A322 File Offset: 0x00008522
		// (set) Token: 0x06000306 RID: 774 RVA: 0x0000A32F File Offset: 0x0000852F
		public bool Indent
		{
			get
			{
				return this.WriterSettings.Indent;
			}
			set
			{
				this.WriterSettings.Indent = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000307 RID: 775 RVA: 0x0000A33D File Offset: 0x0000853D
		// (set) Token: 0x06000308 RID: 776 RVA: 0x0000A345 File Offset: 0x00008545
		public XmlWriterSettings WriterSettings { get; private set; }

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000309 RID: 777 RVA: 0x0000A34E File Offset: 0x0000854E
		// (set) Token: 0x0600030A RID: 778 RVA: 0x0000A35B File Offset: 0x0000855B
		public int MaxDepth
		{
			get
			{
				return this._readerQuotas.MaxDepth;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				this._readerQuotas.MaxDepth = value;
			}
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000A384 File Offset: 0x00008584
		public void SetSerializer(Type type, XmlObjectSerializer serializer)
		{
			this.VerifyAndSetSerializer(type, serializer);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000A38E File Offset: 0x0000858E
		public void SetSerializer<T>(XmlObjectSerializer serializer)
		{
			this.SetSerializer(typeof(T), serializer);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000A384 File Offset: 0x00008584
		public void SetSerializer(Type type, XmlSerializer serializer)
		{
			this.VerifyAndSetSerializer(type, serializer);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000A3A1 File Offset: 0x000085A1
		public void SetSerializer<T>(XmlSerializer serializer)
		{
			this.SetSerializer(typeof(T), serializer);
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000A3B4 File Offset: 0x000085B4
		public bool RemoveSerializer(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			object obj;
			return this._serializerCache.TryRemove(type, out obj);
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000A3E3 File Offset: 0x000085E3
		public override bool CanReadType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			return this.GetCachedSerializer(type, false) != null;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000A404 File Offset: 0x00008604
		public override bool CanWriteType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (this.UseXmlSerializer)
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIEnumerableGenericOrSame(ref type);
			}
			else
			{
				MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
			}
			return this.GetCachedSerializer(type, false) != null;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000A440 File Offset: 0x00008640
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

		// Token: 0x06000313 RID: 787 RVA: 0x0000A49C File Offset: 0x0000869C
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
			object deserializer = this.GetDeserializer(type, content);
			object obj;
			try
			{
				using (XmlReader xmlReader = this.CreateXmlReader(readStream, content))
				{
					XmlSerializer xmlSerializer = deserializer as XmlSerializer;
					if (xmlSerializer != null)
					{
						obj = xmlSerializer.Deserialize(xmlReader);
					}
					else
					{
						XmlObjectSerializer xmlObjectSerializer = deserializer as XmlObjectSerializer;
						if (xmlObjectSerializer == null)
						{
							XmlMediaTypeFormatter.ThrowInvalidSerializerException(deserializer, "GetDeserializer");
						}
						obj = xmlObjectSerializer.ReadObject(xmlReader);
					}
				}
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

		// Token: 0x06000314 RID: 788 RVA: 0x0000A574 File Offset: 0x00008774
		protected internal virtual object GetDeserializer(Type type, HttpContent content)
		{
			return this.GetSerializerForType(type);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000A580 File Offset: 0x00008780
		protected internal virtual XmlReader CreateXmlReader(Stream readStream, HttpContent content)
		{
			Encoding encoding = base.SelectCharacterEncoding((content == null) ? null : content.Headers);
			return XmlDictionaryReader.CreateTextReader(new NonClosingDelegatingStream(readStream), encoding, this._readerQuotas, null);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000A5B4 File Offset: 0x000087B4
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

		// Token: 0x06000317 RID: 791 RVA: 0x0000A620 File Offset: 0x00008820
		private void WriteToStream(Type type, object value, Stream writeStream, HttpContent content)
		{
			bool flag;
			if (this.UseXmlSerializer)
			{
				flag = MediaTypeFormatter.TryGetDelegatingTypeForIEnumerableGenericOrSame(ref type);
			}
			else
			{
				flag = MediaTypeFormatter.TryGetDelegatingTypeForIQueryableGenericOrSame(ref type);
			}
			if (flag && value != null)
			{
				value = MediaTypeFormatter.GetTypeRemappingConstructor(type).Invoke(new object[] { value });
			}
			object serializer = this.GetSerializer(type, value, content);
			using (XmlWriter xmlWriter = this.CreateXmlWriter(writeStream, content))
			{
				XmlSerializer xmlSerializer = serializer as XmlSerializer;
				if (xmlSerializer != null)
				{
					xmlSerializer.Serialize(xmlWriter, value);
				}
				else
				{
					XmlObjectSerializer xmlObjectSerializer = serializer as XmlObjectSerializer;
					if (xmlObjectSerializer == null)
					{
						XmlMediaTypeFormatter.ThrowInvalidSerializerException(serializer, "GetSerializer");
					}
					xmlObjectSerializer.WriteObject(xmlWriter, value);
				}
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000A574 File Offset: 0x00008774
		protected internal virtual object GetSerializer(Type type, object value, HttpContent content)
		{
			return this.GetSerializerForType(type);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000A6C8 File Offset: 0x000088C8
		protected internal virtual XmlWriter CreateXmlWriter(Stream writeStream, HttpContent content)
		{
			Encoding encoding = base.SelectCharacterEncoding((content != null) ? content.Headers : null);
			XmlWriterSettings xmlWriterSettings = this.WriterSettings.Clone();
			xmlWriterSettings.Encoding = encoding;
			return XmlWriter.Create(writeStream, xmlWriterSettings);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000A702 File Offset: 0x00008902
		public virtual XmlSerializer CreateXmlSerializer(Type type)
		{
			return new XmlSerializer(type);
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000A70A File Offset: 0x0000890A
		public virtual DataContractSerializer CreateDataContractSerializer(Type type)
		{
			return new DataContractSerializer(type);
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000A712 File Offset: 0x00008912
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlReader InvokeCreateXmlReader(Stream readStream, HttpContent content)
		{
			return this.CreateXmlReader(readStream, content);
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000A71C File Offset: 0x0000891C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public XmlWriter InvokeCreateXmlWriter(Stream writeStream, HttpContent content)
		{
			return this.CreateXmlWriter(writeStream, content);
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000A726 File Offset: 0x00008926
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object InvokeGetDeserializer(Type type, HttpContent content)
		{
			return this.GetDeserializer(type, content);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0000A730 File Offset: 0x00008930
		[EditorBrowsable(EditorBrowsableState.Never)]
		public object InvokeGetSerializer(Type type, object value, HttpContent content)
		{
			return this.GetSerializer(type, value, content);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0000A73C File Offset: 0x0000893C
		private object CreateDefaultSerializer(Type type, bool throwOnError)
		{
			Exception ex = null;
			object obj = null;
			try
			{
				if (this.UseXmlSerializer)
				{
					obj = this.CreateXmlSerializer(type);
				}
				else
				{
					FormattingUtilities.XsdDataContractExporter.GetRootElementName(type);
					obj = this.CreateDataContractSerializer(type);
				}
			}
			catch (Exception ex)
			{
			}
			if (obj != null || !throwOnError)
			{
				return obj;
			}
			if (ex != null)
			{
				throw Error.InvalidOperation(ex, Resources.SerializerCannotSerializeType, new object[]
				{
					this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
					type.Name
				});
			}
			throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
			{
				this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
				type.Name
			});
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0000A824 File Offset: 0x00008A24
		private object GetCachedSerializer(Type type, bool throwOnError)
		{
			object obj;
			if (!this._serializerCache.TryGetValue(type, out obj))
			{
				obj = this.CreateDefaultSerializer(type, throwOnError);
				this._serializerCache.TryAdd(type, obj);
			}
			return obj;
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0000A859 File Offset: 0x00008A59
		private void VerifyAndSetSerializer(Type type, object serializer)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (serializer == null)
			{
				throw Error.ArgumentNull("serializer");
			}
			this.SetSerializerInternal(type, serializer);
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000A888 File Offset: 0x00008A88
		private void SetSerializerInternal(Type type, object serializer)
		{
			this._serializerCache.AddOrUpdate(type, serializer, (Type key, object value) => serializer);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000A8C4 File Offset: 0x00008AC4
		private object GetSerializerForType(Type type)
		{
			object cachedSerializer = this.GetCachedSerializer(type, true);
			if (cachedSerializer == null)
			{
				throw Error.InvalidOperation(Resources.SerializerCannotSerializeType, new object[]
				{
					this.UseXmlSerializer ? typeof(XmlSerializer).Name : typeof(DataContractSerializer).Name,
					type.Name
				});
			}
			return cachedSerializer;
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0000A923 File Offset: 0x00008B23
		private static void ThrowInvalidSerializerException(object serializer, string getSerializerMethodName)
		{
			if (serializer == null)
			{
				throw Error.InvalidOperation(Resources.XmlMediaTypeFormatter_NullReturnedSerializer, new object[] { getSerializerMethodName });
			}
			throw Error.InvalidOperation(Resources.XmlMediaTypeFormatter_InvalidSerializerType, new object[]
			{
				serializer.GetType().Name,
				getSerializerMethodName
			});
		}

		// Token: 0x040000E9 RID: 233
		private ConcurrentDictionary<Type, object> _serializerCache = new ConcurrentDictionary<Type, object>();

		// Token: 0x040000EA RID: 234
		private XmlDictionaryReaderQuotas _readerQuotas = FormattingUtilities.CreateDefaultReaderQuotas();
	}
}

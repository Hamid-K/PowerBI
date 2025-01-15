using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Bson;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000033 RID: 51
	public class BsonMediaTypeFormatter : BaseJsonMediaTypeFormatter
	{
		// Token: 0x0600020A RID: 522 RVA: 0x00006E0C File Offset: 0x0000500C
		public BsonMediaTypeFormatter()
		{
			base.SupportedMediaTypes.Add(MediaTypeConstants.ApplicationBsonMediaType);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00006E24 File Offset: 0x00005024
		protected BsonMediaTypeFormatter(BsonMediaTypeFormatter formatter)
			: base(formatter)
		{
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00006E2D File Offset: 0x0000502D
		public static MediaTypeHeaderValue DefaultMediaType
		{
			get
			{
				return MediaTypeConstants.ApplicationBsonMediaType;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00006E34 File Offset: 0x00005034
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006E3C File Offset: 0x0000503C
		public sealed override int MaxDepth
		{
			get
			{
				return base.MaxDepth;
			}
			set
			{
				base.MaxDepth = value;
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00006E48 File Offset: 0x00005048
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
			if (type == typeof(DBNull) && content != null && content.Headers != null)
			{
				long? contentLength = content.Headers.ContentLength;
				long num = 0L;
				if ((contentLength.GetValueOrDefault() == num) & (contentLength != null))
				{
					return Task.FromResult<object>(DBNull.Value);
				}
			}
			return base.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006ED0 File Offset: 0x000050D0
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
			if (!BsonMediaTypeFormatter.IsSimpleType(type) && !(type == typeof(byte[])))
			{
				return base.ReadFromStream(type, readStream, effectiveEncoding, formatterLogger);
			}
			Type type2 = BsonMediaTypeFormatter.OpenDictionaryType.MakeGenericType(new Type[]
			{
				typeof(string),
				type
			});
			IDictionary dictionary = base.ReadFromStream(type2, readStream, effectiveEncoding, formatterLogger) as IDictionary;
			if (dictionary == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatter_BsonParseError_MissingData, new object[] { type2.Name });
			}
			string text = string.Empty;
			using (IDictionaryEnumerator enumerator = dictionary.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)enumerator.Current;
					if (dictionary.Count == 1 && dictionaryEntry.Key as string == "Value")
					{
						return dictionaryEntry.Value;
					}
					if (dictionaryEntry.Key != null)
					{
						text = dictionaryEntry.Key.ToString();
					}
				}
			}
			throw Error.InvalidOperation(Resources.MediaTypeFormatter_BsonParseError_UnexpectedData, new object[] { dictionary.Count, text });
		}

		// Token: 0x06000211 RID: 529 RVA: 0x0000703C File Offset: 0x0000523C
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
			BsonReader bsonReader = new BsonReader(new BinaryReader(readStream, effectiveEncoding));
			try
			{
				bsonReader.ReadRootValueAsArray = typeof(IEnumerable).IsAssignableFrom(type) && !typeof(IDictionary).IsAssignableFrom(type);
			}
			catch
			{
				bsonReader.Dispose();
				throw;
			}
			return bsonReader;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000070D0 File Offset: 0x000052D0
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
			if (value == null)
			{
				return;
			}
			if (value == DBNull.Value)
			{
				return;
			}
			Type type2 = value.GetType();
			if (BsonMediaTypeFormatter.IsSimpleType(type2) || type2 == typeof(byte[]))
			{
				Dictionary<string, object> dictionary = new Dictionary<string, object> { { "Value", value } };
				base.WriteToStream(typeof(Dictionary<string, object>), dictionary, writeStream, effectiveEncoding);
				return;
			}
			base.WriteToStream(type, value, writeStream, effectiveEncoding);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x0000716E File Offset: 0x0000536E
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
			return new BsonWriter(new BinaryWriter(writeStream, effectiveEncoding));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000071AC File Offset: 0x000053AC
		private static bool IsSimpleType(Type type)
		{
			return TypeDescriptor.GetConverter(type).CanConvertFrom(typeof(string));
		}

		// Token: 0x04000099 RID: 153
		private static readonly Type OpenDictionaryType = typeof(Dictionary<, >);
	}
}

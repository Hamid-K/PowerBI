using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000023 RID: 35
	public class ObjectContent : HttpContent
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00004904 File Offset: 0x00002B04
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter)
			: this(type, value, formatter, null)
		{
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004910 File Offset: 0x00002B10
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter, string mediaType)
			: this(type, value, formatter, ObjectContent.BuildHeaderValue(mediaType))
		{
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00004924 File Offset: 0x00002B24
		public ObjectContent(Type type, object value, MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			if (!formatter.CanWriteType(type))
			{
				throw Error.InvalidOperation(Resources.ObjectContent_FormatterCannotWriteType, new object[]
				{
					formatter.GetType().FullName,
					type.Name
				});
			}
			this._formatter = formatter;
			this.ObjectType = type;
			this.VerifyAndSetObject(value);
			this._formatter.SetDefaultContentHeaders(type, base.Headers, mediaType);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000100 RID: 256 RVA: 0x000049B3 File Offset: 0x00002BB3
		// (set) Token: 0x06000101 RID: 257 RVA: 0x000049BB File Offset: 0x00002BBB
		public Type ObjectType { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000102 RID: 258 RVA: 0x000049C4 File Offset: 0x00002BC4
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000103 RID: 259 RVA: 0x000049CC File Offset: 0x00002BCC
		// (set) Token: 0x06000104 RID: 260 RVA: 0x000049D4 File Offset: 0x00002BD4
		public object Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000049DD File Offset: 0x00002BDD
		internal static MediaTypeHeaderValue BuildHeaderValue(string mediaType)
		{
			if (mediaType == null)
			{
				return null;
			}
			return new MediaTypeHeaderValue(mediaType);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x000049EA File Offset: 0x00002BEA
		protected override Task SerializeToStreamAsync(Stream stream, TransportContext context)
		{
			return this._formatter.WriteToStreamAsync(this.ObjectType, this.Value, stream, this, context);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00002B15 File Offset: 0x00000D15
		protected override bool TryComputeLength(out long length)
		{
			length = -1L;
			return false;
		}

		// Token: 0x06000108 RID: 264 RVA: 0x00004A06 File Offset: 0x00002C06
		private static bool IsTypeNullable(Type type)
		{
			return !type.IsValueType() || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00004A34 File Offset: 0x00002C34
		private void VerifyAndSetObject(object value)
		{
			if (value == null)
			{
				if (!ObjectContent.IsTypeNullable(this.ObjectType))
				{
					throw Error.InvalidOperation(Resources.CannotUseNullValueType, new object[]
					{
						typeof(ObjectContent).Name,
						this.ObjectType.Name
					});
				}
			}
			else
			{
				Type type = value.GetType();
				if (!this.ObjectType.IsAssignableFrom(type))
				{
					throw Error.Argument("value", Resources.ObjectAndTypeDisagree, new object[]
					{
						type.Name,
						this.ObjectType.Name
					});
				}
			}
			this._value = value;
		}

		// Token: 0x04000060 RID: 96
		private object _value;

		// Token: 0x04000061 RID: 97
		private readonly MediaTypeFormatter _formatter;
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000044 RID: 68
	public abstract class MediaTypeFormatter
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000913C File Offset: 0x0000733C
		protected MediaTypeFormatter()
		{
			this._supportedMediaTypes = new List<MediaTypeHeaderValue>();
			this.SupportedMediaTypes = new MediaTypeFormatter.MediaTypeHeaderValueCollection(this._supportedMediaTypes);
			this._supportedEncodings = new List<Encoding>();
			this.SupportedEncodings = new Collection<Encoding>(this._supportedEncodings);
			this._mediaTypeMappings = new List<MediaTypeMapping>();
			this.MediaTypeMappings = new Collection<MediaTypeMapping>(this._mediaTypeMappings);
		}

		// Token: 0x06000292 RID: 658 RVA: 0x000091A4 File Offset: 0x000073A4
		protected MediaTypeFormatter(MediaTypeFormatter formatter)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._supportedMediaTypes = formatter._supportedMediaTypes;
			this.SupportedMediaTypes = formatter.SupportedMediaTypes;
			this._supportedEncodings = formatter._supportedEncodings;
			this.SupportedEncodings = formatter.SupportedEncodings;
			this._mediaTypeMappings = formatter._mediaTypeMappings;
			this.MediaTypeMappings = formatter.MediaTypeMappings;
			this._requiredMemberSelector = formatter._requiredMemberSelector;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000293 RID: 659 RVA: 0x00009219 File Offset: 0x00007419
		// (set) Token: 0x06000294 RID: 660 RVA: 0x00009237 File Offset: 0x00007437
		public static int MaxHttpCollectionKeys
		{
			get
			{
				if (MediaTypeFormatter._maxHttpCollectionKeys < 0)
				{
					MediaTypeFormatter._maxHttpCollectionKeys = MediaTypeFormatter._defaultMaxHttpCollectionKeys.Value;
				}
				return MediaTypeFormatter._maxHttpCollectionKeys;
			}
			set
			{
				if (value < 1)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("value", value, 1);
				}
				MediaTypeFormatter._maxHttpCollectionKeys = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000295 RID: 661 RVA: 0x0000925A File Offset: 0x0000745A
		// (set) Token: 0x06000296 RID: 662 RVA: 0x00009262 File Offset: 0x00007462
		public Collection<MediaTypeHeaderValue> SupportedMediaTypes { get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000297 RID: 663 RVA: 0x0000926B File Offset: 0x0000746B
		internal List<MediaTypeHeaderValue> SupportedMediaTypesInternal
		{
			get
			{
				return this._supportedMediaTypes;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00009273 File Offset: 0x00007473
		// (set) Token: 0x06000299 RID: 665 RVA: 0x0000927B File Offset: 0x0000747B
		public Collection<Encoding> SupportedEncodings { get; private set; }

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00009284 File Offset: 0x00007484
		internal List<Encoding> SupportedEncodingsInternal
		{
			get
			{
				return this._supportedEncodings;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000928C File Offset: 0x0000748C
		// (set) Token: 0x0600029C RID: 668 RVA: 0x00009294 File Offset: 0x00007494
		public Collection<MediaTypeMapping> MediaTypeMappings { get; private set; }

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000929D File Offset: 0x0000749D
		internal List<MediaTypeMapping> MediaTypeMappingsInternal
		{
			get
			{
				return this._mediaTypeMappings;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600029E RID: 670 RVA: 0x000092A5 File Offset: 0x000074A5
		// (set) Token: 0x0600029F RID: 671 RVA: 0x000092AD File Offset: 0x000074AD
		public virtual IRequiredMemberSelector RequiredMemberSelector
		{
			get
			{
				return this._requiredMemberSelector;
			}
			set
			{
				this._requiredMemberSelector = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000653C File Offset: 0x0000473C
		internal virtual bool CanWriteAnyTypes
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x000092B6 File Offset: 0x000074B6
		public virtual Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotRead, new object[] { base.GetType().Name });
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x000092D6 File Offset: 0x000074D6
		public virtual Task<object> ReadFromStreamAsync(Type type, Stream readStream, HttpContent content, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (cancellationToken.IsCancellationRequested)
			{
				return TaskHelpers.Canceled<object>();
			}
			return this.ReadFromStreamAsync(type, readStream, content, formatterLogger);
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00007F38 File Offset: 0x00006138
		public virtual Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext)
		{
			return this.WriteToStreamAsync(type, value, writeStream, content, transportContext, CancellationToken.None);
		}

		// Token: 0x060002A4 RID: 676 RVA: 0x000092F2 File Offset: 0x000074F2
		public virtual Task WriteToStreamAsync(Type type, object value, Stream writeStream, HttpContent content, TransportContext transportContext, CancellationToken cancellationToken)
		{
			throw Error.NotSupported(Resources.MediaTypeFormatterCannotWrite, new object[] { base.GetType().Name });
		}

		// Token: 0x060002A5 RID: 677 RVA: 0x00009314 File Offset: 0x00007514
		private static bool TryGetDelegatingType(Type interfaceType, ref Type type)
		{
			if (type != null && type.IsInterface() && type.IsGenericType())
			{
				Type type2 = type.ExtractGenericInterface(interfaceType);
				if (type2 != null)
				{
					type = MediaTypeFormatter.GetOrAddDelegatingType(type, type2);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x0000935C File Offset: 0x0000755C
		private static int InitializeDefaultCollectionKeySize()
		{
			return int.MaxValue;
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00009363 File Offset: 0x00007563
		internal static bool TryGetDelegatingTypeForIEnumerableGenericOrSame(ref Type type)
		{
			return MediaTypeFormatter.TryGetDelegatingType(FormattingUtilities.EnumerableInterfaceGenericType, ref type);
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00009370 File Offset: 0x00007570
		internal static bool TryGetDelegatingTypeForIQueryableGenericOrSame(ref Type type)
		{
			return MediaTypeFormatter.TryGetDelegatingType(FormattingUtilities.QueryableInterfaceGenericType, ref type);
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00009380 File Offset: 0x00007580
		internal static ConstructorInfo GetTypeRemappingConstructor(Type type)
		{
			ConstructorInfo constructorInfo;
			MediaTypeFormatter._delegatingEnumerableConstructorCache.TryGetValue(type, out constructorInfo);
			return constructorInfo;
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0000939C File Offset: 0x0000759C
		public Encoding SelectCharacterEncoding(HttpContentHeaders contentHeaders)
		{
			Encoding encoding = null;
			if (contentHeaders != null && contentHeaders.ContentType != null)
			{
				string charSet = contentHeaders.ContentType.CharSet;
				if (!string.IsNullOrWhiteSpace(charSet))
				{
					for (int i = 0; i < this._supportedEncodings.Count; i++)
					{
						Encoding encoding2 = this._supportedEncodings[i];
						if (charSet.Equals(encoding2.WebName, StringComparison.OrdinalIgnoreCase))
						{
							encoding = encoding2;
							break;
						}
					}
				}
			}
			if (encoding == null && this._supportedEncodings.Count > 0)
			{
				encoding = this._supportedEncodings[0];
			}
			if (encoding == null)
			{
				throw Error.InvalidOperation(Resources.MediaTypeFormatterNoEncoding, new object[] { base.GetType().Name });
			}
			return encoding;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00009444 File Offset: 0x00007644
		public virtual void SetDefaultContentHeaders(Type type, HttpContentHeaders headers, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (mediaType != null)
			{
				headers.ContentType = mediaType.Clone<MediaTypeHeaderValue>();
			}
			if (headers.ContentType == null)
			{
				MediaTypeHeaderValue mediaTypeHeaderValue = null;
				if (this._supportedMediaTypes.Count > 0)
				{
					mediaTypeHeaderValue = this._supportedMediaTypes[0];
				}
				if (mediaTypeHeaderValue != null)
				{
					headers.ContentType = mediaTypeHeaderValue.Clone<MediaTypeHeaderValue>();
				}
			}
			if (headers.ContentType != null && headers.ContentType.CharSet == null)
			{
				Encoding encoding = null;
				if (this._supportedEncodings.Count > 0)
				{
					encoding = this._supportedEncodings[0];
				}
				if (encoding != null)
				{
					headers.ContentType.CharSet = encoding.WebName;
				}
			}
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000094FC File Offset: 0x000076FC
		public virtual MediaTypeFormatter GetPerRequestFormatterInstance(Type type, HttpRequestMessage request, MediaTypeHeaderValue mediaType)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (request == null)
			{
				throw Error.ArgumentNull("request");
			}
			return this;
		}

		// Token: 0x060002AD RID: 685
		public abstract bool CanReadType(Type type);

		// Token: 0x060002AE RID: 686
		public abstract bool CanWriteType(Type type);

		// Token: 0x060002AF RID: 687 RVA: 0x00009524 File Offset: 0x00007724
		private static Type GetOrAddDelegatingType(Type type, Type genericType)
		{
			return MediaTypeFormatter._delegatingEnumerableCache.GetOrAdd(type, delegate(Type typeToRemap)
			{
				Type type2 = genericType.GetGenericArguments()[0];
				Type type3 = FormattingUtilities.DelegatingEnumerableGenericType.MakeGenericType(new Type[] { type2 });
				ConstructorInfo constructor = type3.GetConstructor(new Type[] { FormattingUtilities.EnumerableInterfaceGenericType.MakeGenericType(new Type[] { type2 }) });
				MediaTypeFormatter._delegatingEnumerableConstructorCache.TryAdd(type3, constructor);
				return type3;
			});
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00009555 File Offset: 0x00007755
		public static object GetDefaultValueForType(Type type)
		{
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (type.IsValueType())
			{
				return Activator.CreateInstance(type);
			}
			return null;
		}

		// Token: 0x040000BD RID: 189
		private const int DefaultMinHttpCollectionKeys = 1;

		// Token: 0x040000BE RID: 190
		private const int DefaultMaxHttpCollectionKeys = 1000;

		// Token: 0x040000BF RID: 191
		private const string IWellKnownComparerTypeName = "System.IWellKnownStringEqualityComparer, mscorlib, Version=4.0.0.0, PublicKeyToken=b77a5c561934e089";

		// Token: 0x040000C0 RID: 192
		private static readonly ConcurrentDictionary<Type, Type> _delegatingEnumerableCache = new ConcurrentDictionary<Type, Type>();

		// Token: 0x040000C1 RID: 193
		private static ConcurrentDictionary<Type, ConstructorInfo> _delegatingEnumerableConstructorCache = new ConcurrentDictionary<Type, ConstructorInfo>();

		// Token: 0x040000C2 RID: 194
		private static Lazy<int> _defaultMaxHttpCollectionKeys = new Lazy<int>(new Func<int>(MediaTypeFormatter.InitializeDefaultCollectionKeySize), true);

		// Token: 0x040000C3 RID: 195
		private static int _maxHttpCollectionKeys = -1;

		// Token: 0x040000C4 RID: 196
		private readonly List<MediaTypeHeaderValue> _supportedMediaTypes;

		// Token: 0x040000C5 RID: 197
		private readonly List<Encoding> _supportedEncodings;

		// Token: 0x040000C6 RID: 198
		private readonly List<MediaTypeMapping> _mediaTypeMappings;

		// Token: 0x040000C7 RID: 199
		private IRequiredMemberSelector _requiredMemberSelector;

		// Token: 0x0200007D RID: 125
		internal class MediaTypeHeaderValueCollection : Collection<MediaTypeHeaderValue>
		{
			// Token: 0x060003DB RID: 987 RVA: 0x0000E7AE File Offset: 0x0000C9AE
			internal MediaTypeHeaderValueCollection(IList<MediaTypeHeaderValue> list)
				: base(list)
			{
			}

			// Token: 0x060003DC RID: 988 RVA: 0x0000E7B7 File Offset: 0x0000C9B7
			protected override void InsertItem(int index, MediaTypeHeaderValue item)
			{
				MediaTypeFormatter.MediaTypeHeaderValueCollection.ValidateMediaType(item);
				base.InsertItem(index, item);
			}

			// Token: 0x060003DD RID: 989 RVA: 0x0000E7C7 File Offset: 0x0000C9C7
			protected override void SetItem(int index, MediaTypeHeaderValue item)
			{
				MediaTypeFormatter.MediaTypeHeaderValueCollection.ValidateMediaType(item);
				base.SetItem(index, item);
			}

			// Token: 0x060003DE RID: 990 RVA: 0x0000E7D8 File Offset: 0x0000C9D8
			private static void ValidateMediaType(MediaTypeHeaderValue item)
			{
				if (item == null)
				{
					throw Error.ArgumentNull("item");
				}
				ParsedMediaTypeHeaderValue parsedMediaTypeHeaderValue = new ParsedMediaTypeHeaderValue(item);
				if (parsedMediaTypeHeaderValue.IsAllMediaRange || parsedMediaTypeHeaderValue.IsSubtypeMediaRange)
				{
					throw Error.Argument("item", Resources.CannotUseMediaRangeForSupportedMediaType, new object[]
					{
						MediaTypeFormatter.MediaTypeHeaderValueCollection._mediaTypeHeaderValueType.Name,
						item.MediaType
					});
				}
			}

			// Token: 0x040001C5 RID: 453
			private static readonly Type _mediaTypeHeaderValueType = typeof(MediaTypeHeaderValue);
		}
	}
}

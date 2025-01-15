using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000017 RID: 23
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentExtensions
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000032E8 File Offset: 0x000014E8
		private static MediaTypeFormatterCollection DefaultMediaTypeFormatterCollection
		{
			get
			{
				if (HttpContentExtensions._defaultMediaTypeFormatterCollection == null)
				{
					HttpContentExtensions._defaultMediaTypeFormatterCollection = new MediaTypeFormatterCollection();
				}
				return HttpContentExtensions._defaultMediaTypeFormatterCollection;
			}
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00003300 File Offset: 0x00001500
		public static Task<object> ReadAsAsync(this HttpContent content, Type type)
		{
			return content.ReadAsAsync(type, HttpContentExtensions.DefaultMediaTypeFormatterCollection);
		}

		// Token: 0x06000086 RID: 134 RVA: 0x0000330E File Offset: 0x0000150E
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, CancellationToken cancellationToken)
		{
			return content.ReadAsAsync(type, HttpContentExtensions.DefaultMediaTypeFormatterCollection, cancellationToken);
		}

		// Token: 0x06000087 RID: 135 RVA: 0x0000331D File Offset: 0x0000151D
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, null);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00003328 File Offset: 0x00001528
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, null, cancellationToken);
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00003334 File Offset: 0x00001534
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, formatterLogger);
		}

		// Token: 0x0600008A RID: 138 RVA: 0x0000333F File Offset: 0x0000153F
		public static Task<object> ReadAsAsync(this HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<object>(content, type, formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000334C File Offset: 0x0000154C
		public static Task<T> ReadAsAsync<T>(this HttpContent content)
		{
			return content.ReadAsAsync(HttpContentExtensions.DefaultMediaTypeFormatterCollection);
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003359 File Offset: 0x00001559
		public static Task<T> ReadAsAsync<T>(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsAsync(HttpContentExtensions.DefaultMediaTypeFormatterCollection, cancellationToken);
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003367 File Offset: 0x00001567
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, null);
		}

		// Token: 0x0600008E RID: 142 RVA: 0x0000337B File Offset: 0x0000157B
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, null, cancellationToken);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003390 File Offset: 0x00001590
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, formatterLogger);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x000033A4 File Offset: 0x000015A4
		public static Task<T> ReadAsAsync<T>(this HttpContent content, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, typeof(T), formatters, formatterLogger, cancellationToken);
		}

		// Token: 0x06000091 RID: 145 RVA: 0x000033B9 File Offset: 0x000015B9
		private static Task<T> ReadAsAsync<T>(HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger)
		{
			return HttpContentExtensions.ReadAsAsync<T>(content, type, formatters, formatterLogger, CancellationToken.None);
		}

		// Token: 0x06000092 RID: 146 RVA: 0x000033CC File Offset: 0x000015CC
		private static Task<T> ReadAsAsync<T>(HttpContent content, Type type, IEnumerable<MediaTypeFormatter> formatters, IFormatterLogger formatterLogger, CancellationToken cancellationToken)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (type == null)
			{
				throw Error.ArgumentNull("type");
			}
			if (formatters == null)
			{
				throw Error.ArgumentNull("formatters");
			}
			ObjectContent objectContent = content as ObjectContent;
			if (objectContent != null && objectContent.Value != null && type.IsAssignableFrom(objectContent.Value.GetType()))
			{
				return Task.FromResult<T>((T)((object)objectContent.Value));
			}
			MediaTypeHeaderValue mediaTypeHeaderValue = content.Headers.ContentType ?? MediaTypeConstants.ApplicationOctetStreamMediaType;
			MediaTypeFormatter mediaTypeFormatter = new MediaTypeFormatterCollection(formatters).FindReader(type, mediaTypeHeaderValue);
			if (mediaTypeFormatter != null)
			{
				return HttpContentExtensions.ReadAsAsyncCore<T>(content, type, formatterLogger, mediaTypeFormatter, cancellationToken);
			}
			long? contentLength = content.Headers.ContentLength;
			long num = 0L;
			if ((contentLength.GetValueOrDefault() == num) & (contentLength != null))
			{
				return Task.FromResult<T>((T)((object)MediaTypeFormatter.GetDefaultValueForType(type)));
			}
			throw new UnsupportedMediaTypeException(Error.Format(Resources.NoReadSerializerAvailable, new object[] { type.Name, mediaTypeHeaderValue.MediaType }), mediaTypeHeaderValue);
		}

		// Token: 0x06000093 RID: 147 RVA: 0x000034D4 File Offset: 0x000016D4
		private static async Task<T> ReadAsAsyncCore<T>(HttpContent content, Type type, IFormatterLogger formatterLogger, MediaTypeFormatter formatter, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			Stream stream = await content.ReadAsStreamAsync();
			TaskAwaiter<object> taskAwaiter = formatter.ReadFromStreamAsync(type, stream, content, formatterLogger, cancellationToken).GetAwaiter();
			if (!taskAwaiter.IsCompleted)
			{
				await taskAwaiter;
				TaskAwaiter<object> taskAwaiter2;
				taskAwaiter = taskAwaiter2;
				taskAwaiter2 = default(TaskAwaiter<object>);
			}
			return (T)((object)taskAwaiter.GetResult());
		}

		// Token: 0x04000031 RID: 49
		private static MediaTypeFormatterCollection _defaultMediaTypeFormatterCollection;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net.Http.Formatting.Parsers;
using System.Net.Http.Properties;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000019 RID: 25
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class HttpContentMultipartExtensions
	{
		// Token: 0x060000AB RID: 171 RVA: 0x00003A71 File Offset: 0x00001C71
		public static bool IsMimeMultipartContent(this HttpContent content)
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			return MimeMultipartBodyPartParser.IsMimeMultipartContent(content);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00003A88 File Offset: 0x00001C88
		public static bool IsMimeMultipartContent(this HttpContent content, string subtype)
		{
			if (string.IsNullOrWhiteSpace(subtype))
			{
				throw Error.ArgumentNull("subtype");
			}
			return content.IsMimeMultipartContent() && content.Headers.ContentType.MediaType.Equals("multipart/" + subtype, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003AD6 File Offset: 0x00001CD6
		public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content)
		{
			return content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider(), 32768);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00003AE8 File Offset: 0x00001CE8
		public static Task<MultipartMemoryStreamProvider> ReadAsMultipartAsync(this HttpContent content, CancellationToken cancellationToken)
		{
			return content.ReadAsMultipartAsync(new MultipartMemoryStreamProvider(), 32768, cancellationToken);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00003AFB File Offset: 0x00001CFB
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, 32768);
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003B09 File Offset: 0x00001D09
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, CancellationToken cancellationToken) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, 32768, cancellationToken);
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00003B18 File Offset: 0x00001D18
		public static Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, int bufferSize) where T : MultipartStreamProvider
		{
			return content.ReadAsMultipartAsync(streamProvider, bufferSize, CancellationToken.None);
		}

		// Token: 0x060000B2 RID: 178 RVA: 0x00003B28 File Offset: 0x00001D28
		public static async Task<T> ReadAsMultipartAsync<T>(this HttpContent content, T streamProvider, int bufferSize, CancellationToken cancellationToken) where T : MultipartStreamProvider
		{
			if (content == null)
			{
				throw Error.ArgumentNull("content");
			}
			if (streamProvider == null)
			{
				throw Error.ArgumentNull("streamProvider");
			}
			if (bufferSize < 256)
			{
				throw Error.ArgumentMustBeGreaterThanOrEqualTo("bufferSize", bufferSize, 256);
			}
			Stream stream;
			try
			{
				stream = await content.ReadAsStreamAsync();
			}
			catch (Exception ex)
			{
				throw new IOException(Resources.ReadAsMimeMultipartErrorReading, ex);
			}
			using (MimeMultipartBodyPartParser parser = new MimeMultipartBodyPartParser(content, streamProvider))
			{
				await HttpContentMultipartExtensions.MultipartReadAsync(new HttpContentMultipartExtensions.MultipartAsyncContext(stream, parser, new byte[bufferSize], streamProvider.Contents), cancellationToken);
				await streamProvider.ExecutePostProcessingAsync(cancellationToken);
			}
			return streamProvider;
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00003B88 File Offset: 0x00001D88
		private static async Task MultipartReadAsync(HttpContentMultipartExtensions.MultipartAsyncContext context, CancellationToken cancellationToken)
		{
			for (;;)
			{
				int num;
				try
				{
					num = await context.ContentStream.ReadAsync(context.Data, 0, context.Data.Length, cancellationToken);
				}
				catch (Exception ex)
				{
					throw new IOException(Resources.ReadAsMimeMultipartErrorReading, ex);
				}
				IEnumerable<MimeBodyPart> enumerable = context.MimeParser.ParseBuffer(context.Data, num);
				foreach (MimeBodyPart part in enumerable)
				{
					foreach (ArraySegment<byte> arraySegment in part.Segments)
					{
						try
						{
							await part.WriteSegment(arraySegment, cancellationToken);
						}
						catch (Exception ex2)
						{
							part.Dispose();
							throw new IOException(Resources.ReadAsMimeMultipartErrorWriting, ex2);
						}
					}
					List<ArraySegment<byte>>.Enumerator enumerator2 = default(List<ArraySegment<byte>>.Enumerator);
					if (HttpContentMultipartExtensions.CheckIsFinalPart(part, context.Result))
					{
						return;
					}
					part = null;
				}
				IEnumerator<MimeBodyPart> enumerator = null;
			}
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x00003BD8 File Offset: 0x00001DD8
		private static bool CheckIsFinalPart(MimeBodyPart part, ICollection<HttpContent> result)
		{
			if (part.IsComplete)
			{
				HttpContent completedHttpContent = part.GetCompletedHttpContent();
				if (completedHttpContent != null)
				{
					result.Add(completedHttpContent);
				}
				bool isFinal = part.IsFinal;
				part.Dispose();
				return isFinal;
			}
			return false;
		}

		// Token: 0x04000034 RID: 52
		private const int MinBufferSize = 256;

		// Token: 0x04000035 RID: 53
		private const int DefaultBufferSize = 32768;

		// Token: 0x0200006E RID: 110
		private class MultipartAsyncContext
		{
			// Token: 0x060003AF RID: 943 RVA: 0x0000D95A File Offset: 0x0000BB5A
			public MultipartAsyncContext(Stream contentStream, MimeMultipartBodyPartParser mimeParser, byte[] data, ICollection<HttpContent> result)
			{
				this.ContentStream = contentStream;
				this.Result = result;
				this.MimeParser = mimeParser;
				this.Data = data;
			}

			// Token: 0x170000DC RID: 220
			// (get) Token: 0x060003B0 RID: 944 RVA: 0x0000D97F File Offset: 0x0000BB7F
			// (set) Token: 0x060003B1 RID: 945 RVA: 0x0000D987 File Offset: 0x0000BB87
			public Stream ContentStream { get; private set; }

			// Token: 0x170000DD RID: 221
			// (get) Token: 0x060003B2 RID: 946 RVA: 0x0000D990 File Offset: 0x0000BB90
			// (set) Token: 0x060003B3 RID: 947 RVA: 0x0000D998 File Offset: 0x0000BB98
			public ICollection<HttpContent> Result { get; private set; }

			// Token: 0x170000DE RID: 222
			// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000D9A1 File Offset: 0x0000BBA1
			// (set) Token: 0x060003B5 RID: 949 RVA: 0x0000D9A9 File Offset: 0x0000BBA9
			public byte[] Data { get; private set; }

			// Token: 0x170000DF RID: 223
			// (get) Token: 0x060003B6 RID: 950 RVA: 0x0000D9B2 File Offset: 0x0000BBB2
			// (set) Token: 0x060003B7 RID: 951 RVA: 0x0000D9BA File Offset: 0x0000BBBA
			public MimeMultipartBodyPartParser MimeParser { get; private set; }
		}
	}
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Runtime.CompilerServices;
using Azure.Core;

namespace Azure
{
	// Token: 0x0200002E RID: 46
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Response : IDisposable
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E4 RID: 228
		public abstract int Status { get; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E5 RID: 229
		public abstract string ReasonPhrase { get; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E6 RID: 230
		// (set) Token: 0x060000E7 RID: 231
		[Nullable(2)]
		public abstract Stream ContentStream
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000E8 RID: 232
		// (set) Token: 0x060000E9 RID: 233
		public abstract string ClientRequestId { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EA RID: 234 RVA: 0x000038AC File Offset: 0x00001AAC
		public virtual ResponseHeaders Headers
		{
			get
			{
				return new ResponseHeaders(this);
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000EB RID: 235 RVA: 0x000038B4 File Offset: 0x00001AB4
		public virtual BinaryData Content
		{
			get
			{
				if (this.ContentStream == null)
				{
					return Response.s_EmptyBinaryData;
				}
				MemoryStream memoryStream = this.ContentStream as MemoryStream;
				if (memoryStream == null)
				{
					throw new InvalidOperationException("The response is not fully buffered.");
				}
				ArraySegment<byte> arraySegment;
				if (memoryStream.TryGetBuffer(out arraySegment))
				{
					return new BinaryData(MemoryExtensions.AsMemory<byte>(arraySegment));
				}
				return new BinaryData(memoryStream.ToArray());
			}
		}

		// Token: 0x060000EC RID: 236
		public abstract void Dispose();

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000ED RID: 237 RVA: 0x0000390F File Offset: 0x00001B0F
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00003917 File Offset: 0x00001B17
		public virtual bool IsError { get; internal set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003920 File Offset: 0x00001B20
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00003928 File Offset: 0x00001B28
		internal HttpMessageSanitizer Sanitizer { get; set; } = HttpMessageSanitizer.Default;

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00003931 File Offset: 0x00001B31
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00003939 File Offset: 0x00001B39
		[Nullable(2)]
		internal RequestFailedDetailsParser RequestFailedDetailsParser
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x060000F3 RID: 243
		protected internal abstract bool TryGetHeader(string name, [Nullable(2)] [NotNullWhen(true)] out string value);

		// Token: 0x060000F4 RID: 244
		protected internal abstract bool TryGetHeaderValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values);

		// Token: 0x060000F5 RID: 245
		protected internal abstract bool ContainsHeader(string name);

		// Token: 0x060000F6 RID: 246
		protected internal abstract IEnumerable<HttpHeader> EnumerateHeaders();

		// Token: 0x060000F7 RID: 247 RVA: 0x00003942 File Offset: 0x00001B42
		public static Response<T> FromValue<[Nullable(2)] T>(T value, Response response)
		{
			return new ValueResponse<T>(response, value);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x0000394B File Offset: 0x00001B4B
		public override string ToString()
		{
			return string.Format("Status: {0}, ReasonPhrase: {1}", this.Status, this.ReasonPhrase);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00003968 File Offset: 0x00001B68
		[NullableContext(2)]
		internal static void DisposeStreamIfNotBuffered(ref Stream stream)
		{
			if (!(stream is MemoryStream))
			{
				Stream stream2 = stream;
				if (stream2 != null)
				{
					stream2.Dispose();
				}
				stream = null;
			}
		}

		// Token: 0x04000053 RID: 83
		private static readonly BinaryData s_EmptyBinaryData = new BinaryData(Array.Empty<byte>());
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Http;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.OData
{
	// Token: 0x02000725 RID: 1829
	internal class HttpResponseData : IDisposable
	{
		// Token: 0x0600366C RID: 13932 RVA: 0x000AD9C4 File Offset: 0x000ABBC4
		public HttpResponseData(Stream stream)
		{
			BinaryReader binaryReader = new BinaryReader(stream);
			this.contentType = binaryReader.ReadString();
			this.contentLength = binaryReader.ReadInt64();
			this.statusCode = binaryReader.ReadInt32();
			int num = binaryReader.ReadInt32();
			this.headers = new Dictionary<string, string>(num, StringComparer.OrdinalIgnoreCase);
			for (int i = 0; i < num; i++)
			{
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				this.headers.Add(text, text2);
			}
			this.responseUri = new Uri(binaryReader.ReadString());
			this.stream = stream;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000ADA5B File Offset: 0x000ABC5B
		public HttpResponseData(string contentType, long contentLength, int statusCode, Dictionary<string, string> headers, Uri responseUri, Stream responseStream)
		{
			this.contentType = contentType;
			this.contentLength = contentLength;
			this.statusCode = statusCode;
			this.headers = headers;
			this.responseUri = responseUri;
			this.stream = responseStream;
		}

		// Token: 0x170012BF RID: 4799
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000ADA90 File Offset: 0x000ABC90
		public long ContentLength
		{
			get
			{
				return this.contentLength;
			}
		}

		// Token: 0x170012C0 RID: 4800
		// (get) Token: 0x0600366F RID: 13935 RVA: 0x000ADA98 File Offset: 0x000ABC98
		public string ContentType
		{
			get
			{
				return this.contentType;
			}
		}

		// Token: 0x170012C1 RID: 4801
		// (get) Token: 0x06003670 RID: 13936 RVA: 0x000ADAA0 File Offset: 0x000ABCA0
		public Dictionary<string, string> Headers
		{
			get
			{
				return this.headers;
			}
		}

		// Token: 0x170012C2 RID: 4802
		// (get) Token: 0x06003671 RID: 13937 RVA: 0x000ADAA8 File Offset: 0x000ABCA8
		// (set) Token: 0x06003672 RID: 13938 RVA: 0x000ADAB0 File Offset: 0x000ABCB0
		public int StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x170012C3 RID: 4803
		// (get) Token: 0x06003673 RID: 13939 RVA: 0x000ADAB9 File Offset: 0x000ABCB9
		public Uri ResponseUri
		{
			get
			{
				return this.responseUri;
			}
		}

		// Token: 0x170012C4 RID: 4804
		// (get) Token: 0x06003674 RID: 13940 RVA: 0x000ADAC1 File Offset: 0x000ABCC1
		public Stream Stream
		{
			get
			{
				return this.stream;
			}
		}

		// Token: 0x06003675 RID: 13941 RVA: 0x000ADACC File Offset: 0x000ABCCC
		public static Stream Serialize(string contentType, long contentLength, int statusCode, int headerCount, IEnumerable<KeyValuePair<string, string>> headers, Uri responseUri, Stream responseStream, IHostProgress hostProgress)
		{
			Stream stream = new MemoryStream();
			BinaryWriter binaryWriter = new BinaryWriter(stream);
			binaryWriter.Write(contentType ?? "");
			binaryWriter.Write(contentLength);
			binaryWriter.Write(statusCode);
			binaryWriter.Write(headerCount);
			foreach (KeyValuePair<string, string> keyValuePair in headers)
			{
				string value = keyValuePair.Value;
				if (value != null)
				{
					binaryWriter.Write(keyValuePair.Key);
					binaryWriter.Write(value);
				}
			}
			binaryWriter.Write(responseUri.OriginalString);
			stream.Position = 0L;
			return stream.Concat(new ProgressStream(responseStream, hostProgress));
		}

		// Token: 0x06003676 RID: 13942 RVA: 0x000ADB88 File Offset: 0x000ABD88
		public static Stream Serialize(MashupHttpWebResponse response, IHostProgress hostProgress)
		{
			string[] keys = response.Headers.AllKeys;
			return HttpResponseData.Serialize(response.ContentType, response.ContentLength, (int)response.StatusCode, keys.Length, from i in Enumerable.Range(0, keys.Length)
				select new KeyValuePair<string, string>(keys[i], response.Headers[keys[i]]), response.ResponseUri, response.GetDecompressedResponseStream(), hostProgress);
		}

		// Token: 0x06003677 RID: 13943 RVA: 0x000ADC1C File Offset: 0x000ABE1C
		public void Dispose()
		{
			this.stream.Dispose();
		}

		// Token: 0x170012C5 RID: 4805
		// (get) Token: 0x06003678 RID: 13944 RVA: 0x000ADC29 File Offset: 0x000ABE29
		public HashSet<string> ContentTypes
		{
			get
			{
				if (this.contentTypes == null)
				{
					this.contentTypes = HttpResponseData.GetContentTypes(this.ContentType);
				}
				return this.contentTypes;
			}
		}

		// Token: 0x06003679 RID: 13945 RVA: 0x000ADC4C File Offset: 0x000ABE4C
		public void DetectPriorToRead(bool skipCatchExceptions, Action<Stream, Func<long>> preReadStreamAction)
		{
			if (!this.detectionRun)
			{
				this.detectionRun = true;
				try
				{
					HttpResponseData.PartiallyBufferedStream bufferedStream = new HttpResponseData.PartiallyBufferedStream(this.stream, new MemoryStream(), true);
					preReadStreamAction(bufferedStream, () => bufferedStream.Position);
					bufferedStream.StopBufferingResetToStart();
					this.stream = bufferedStream;
				}
				catch (Exception ex)
				{
					if (!skipCatchExceptions && SafeExceptions.IsSafeException(ex))
					{
						throw ODataCommonErrors.UnableToDetectRequiredPayloadInformation(ex);
					}
					throw;
				}
			}
		}

		// Token: 0x0600367A RID: 13946 RVA: 0x000ADCDC File Offset: 0x000ABEDC
		private static HashSet<string> GetContentTypes(string contentHeader)
		{
			HashSet<string> hashSet = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in contentHeader.Split(HttpResponseData.comma, StringSplitOptions.RemoveEmptyEntries))
			{
				hashSet.Add(text.Trim());
			}
			return hashSet;
		}

		// Token: 0x04001BF9 RID: 7161
		private static readonly char[] comma = new char[] { ',' };

		// Token: 0x04001BFA RID: 7162
		private readonly string contentType;

		// Token: 0x04001BFB RID: 7163
		private readonly long contentLength;

		// Token: 0x04001BFC RID: 7164
		private readonly Dictionary<string, string> headers;

		// Token: 0x04001BFD RID: 7165
		private readonly Uri responseUri;

		// Token: 0x04001BFE RID: 7166
		private Stream stream;

		// Token: 0x04001BFF RID: 7167
		private int statusCode;

		// Token: 0x04001C00 RID: 7168
		private bool detectionRun;

		// Token: 0x04001C01 RID: 7169
		private HashSet<string> contentTypes;

		// Token: 0x02000726 RID: 1830
		private class PartiallyBufferedStream : Stream
		{
			// Token: 0x0600367C RID: 13948 RVA: 0x000ADD33 File Offset: 0x000ABF33
			public PartiallyBufferedStream(Stream underlyingStream, MemoryStream buffer, bool buffering)
			{
				this.underlyingStream = underlyingStream;
				this.buffer = buffer;
				this.buffering = buffering;
			}

			// Token: 0x170012C6 RID: 4806
			// (get) Token: 0x0600367D RID: 13949 RVA: 0x00002139 File Offset: 0x00000339
			public override bool CanRead
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170012C7 RID: 4807
			// (get) Token: 0x0600367E RID: 13950 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170012C8 RID: 4808
			// (get) Token: 0x0600367F RID: 13951 RVA: 0x00002105 File Offset: 0x00000305
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06003680 RID: 13952 RVA: 0x000ADD50 File Offset: 0x000ABF50
			protected override void Dispose(bool disposing)
			{
				if (disposing && !this.buffering)
				{
					this.DisposeUnderlyingStream();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06003681 RID: 13953 RVA: 0x000ADD6A File Offset: 0x000ABF6A
			private Stream GetStream()
			{
				return this.underlyingStream;
			}

			// Token: 0x06003682 RID: 13954 RVA: 0x000ADD72 File Offset: 0x000ABF72
			private void DisposeUnderlyingStream()
			{
				this.underlyingStream.Dispose();
			}

			// Token: 0x06003683 RID: 13955 RVA: 0x000091AE File Offset: 0x000073AE
			public override void Flush()
			{
				throw new NotImplementedException();
			}

			// Token: 0x170012C9 RID: 4809
			// (get) Token: 0x06003684 RID: 13956 RVA: 0x000ADD7F File Offset: 0x000ABF7F
			public override long Length
			{
				get
				{
					return this.buffer.Length + this.GetStream().Length;
				}
			}

			// Token: 0x170012CA RID: 4810
			// (get) Token: 0x06003685 RID: 13957 RVA: 0x000ADD98 File Offset: 0x000ABF98
			// (set) Token: 0x06003686 RID: 13958 RVA: 0x000091AE File Offset: 0x000073AE
			public override long Position
			{
				get
				{
					return this.position;
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x06003687 RID: 13959 RVA: 0x000ADDA0 File Offset: 0x000ABFA0
			public override int Read(byte[] buffer, int offset, int count)
			{
				int num;
				if (this.position < this.buffer.Length)
				{
					this.buffer.Position = this.position;
					num = Math.Min(count, (int)(this.buffer.Length - this.position));
					num = this.buffer.Read(buffer, offset, num);
				}
				else
				{
					num = this.GetStream().Read(buffer, offset, count);
					if (this.buffering)
					{
						this.buffer.Position = this.buffer.Length;
						this.buffer.Write(buffer, offset, num);
					}
				}
				this.position += (long)num;
				return num;
			}

			// Token: 0x06003688 RID: 13960 RVA: 0x000091AE File Offset: 0x000073AE
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new NotImplementedException();
			}

			// Token: 0x06003689 RID: 13961 RVA: 0x000091AE File Offset: 0x000073AE
			public override void SetLength(long value)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600368A RID: 13962 RVA: 0x000091AE File Offset: 0x000073AE
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw new NotImplementedException();
			}

			// Token: 0x0600368B RID: 13963 RVA: 0x000ADE47 File Offset: 0x000AC047
			public void StopBufferingResetToStart()
			{
				this.buffering = false;
				this.position = 0L;
			}

			// Token: 0x04001C02 RID: 7170
			private readonly Stream underlyingStream;

			// Token: 0x04001C03 RID: 7171
			private readonly MemoryStream buffer;

			// Token: 0x04001C04 RID: 7172
			private bool buffering;

			// Token: 0x04001C05 RID: 7173
			private long position;
		}
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Net.Http.Properties;
using System.Web.Http;

namespace System.Net.Http.Formatting.Parsers
{
	// Token: 0x02000057 RID: 87
	internal class MimeMultipartBodyPartParser : IDisposable
	{
		// Token: 0x0600033A RID: 826 RVA: 0x0000BA45 File Offset: 0x00009C45
		public MimeMultipartBodyPartParser(HttpContent content, MultipartStreamProvider streamProvider)
			: this(content, streamProvider, long.MaxValue, 4096)
		{
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0000BA60 File Offset: 0x00009C60
		public MimeMultipartBodyPartParser(HttpContent content, MultipartStreamProvider streamProvider, long maxMessageSize, int maxBodyPartHeaderSize)
		{
			string text = MimeMultipartBodyPartParser.ValidateArguments(content, maxMessageSize, true);
			this._mimeParser = new MimeMultipartParser(text, maxMessageSize);
			this._currentBodyPart = new MimeBodyPart(streamProvider, maxBodyPartHeaderSize, content);
			this._content = content;
			this._maxBodyPartHeaderSize = maxBodyPartHeaderSize;
			this._streamProvider = streamProvider;
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0000BAC4 File Offset: 0x00009CC4
		public static bool IsMimeMultipartContent(HttpContent content)
		{
			bool flag;
			try
			{
				flag = MimeMultipartBodyPartParser.ValidateArguments(content, long.MaxValue, false) != null;
			}
			catch (Exception)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000BB00 File Offset: 0x00009D00
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000BB0F File Offset: 0x00009D0F
		public IEnumerable<MimeBodyPart> ParseBuffer(byte[] data, int bytesRead)
		{
			int bytesConsumed = 0;
			bool flag = false;
			if (bytesRead == 0 && !this._mimeParser.IsWaitingForEndOfMessage)
			{
				this.CleanupCurrentBodyPart();
				throw new IOException(Resources.ReadAsMimeMultipartUnexpectedTermination);
			}
			this._currentBodyPart.Segments.Clear();
			while (this._mimeParser.CanParseMore(bytesRead, bytesConsumed))
			{
				this._mimeStatus = this._mimeParser.ParseBuffer(data, bytesRead, ref bytesConsumed, out this._parsedBodyPart[0], out this._parsedBodyPart[1], out flag);
				if (this._mimeStatus != MimeMultipartParser.State.BodyPartCompleted && this._mimeStatus != MimeMultipartParser.State.NeedMoreData)
				{
					this.CleanupCurrentBodyPart();
					throw Error.InvalidOperation(Resources.ReadAsMimeMultipartParseError, new object[] { bytesConsumed, data });
				}
				if (this._isFirst)
				{
					if (this._mimeStatus == MimeMultipartParser.State.BodyPartCompleted)
					{
						this._isFirst = false;
					}
				}
				else
				{
					foreach (ArraySegment<byte> arraySegment in this._parsedBodyPart)
					{
						if (arraySegment.Count != 0)
						{
							if (this._bodyPartHeaderStatus != ParserState.Done)
							{
								int offset = arraySegment.Offset;
								this._bodyPartHeaderStatus = this._currentBodyPart.HeaderParser.ParseBuffer(arraySegment.Array, arraySegment.Count + arraySegment.Offset, ref offset);
								if (this._bodyPartHeaderStatus == ParserState.Done)
								{
									this._currentBodyPart.Segments.Add(new ArraySegment<byte>(arraySegment.Array, offset, arraySegment.Count + arraySegment.Offset - offset));
								}
								else if (this._bodyPartHeaderStatus != ParserState.NeedMoreData)
								{
									this.CleanupCurrentBodyPart();
									throw Error.InvalidOperation(Resources.ReadAsMimeMultipartHeaderParseError, new object[] { offset, arraySegment.Array });
								}
							}
							else
							{
								this._currentBodyPart.Segments.Add(arraySegment);
							}
						}
					}
					if (this._mimeStatus == MimeMultipartParser.State.BodyPartCompleted)
					{
						MimeBodyPart currentBodyPart = this._currentBodyPart;
						currentBodyPart.IsComplete = true;
						currentBodyPart.IsFinal = flag;
						this._currentBodyPart = new MimeBodyPart(this._streamProvider, this._maxBodyPartHeaderSize, this._content);
						this._mimeStatus = MimeMultipartParser.State.NeedMoreData;
						this._bodyPartHeaderStatus = ParserState.NeedMoreData;
						yield return currentBodyPart;
					}
					else
					{
						yield return this._currentBodyPart;
					}
				}
			}
			yield break;
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000BB2D File Offset: 0x00009D2D
		protected void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._mimeParser = null;
				this.CleanupCurrentBodyPart();
			}
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0000BB40 File Offset: 0x00009D40
		private static string ValidateArguments(HttpContent content, long maxMessageSize, bool throwOnError)
		{
			if (maxMessageSize < 10L)
			{
				if (throwOnError)
				{
					throw Error.ArgumentMustBeGreaterThanOrEqualTo("maxMessageSize", maxMessageSize, 10);
				}
				return null;
			}
			else
			{
				MediaTypeHeaderValue contentType = content.Headers.ContentType;
				if (contentType == null)
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoContentType, new object[]
						{
							typeof(HttpContent).Name,
							"multipart/"
						});
					}
					return null;
				}
				else if (!contentType.MediaType.StartsWith("multipart", StringComparison.OrdinalIgnoreCase))
				{
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoMultipart, new object[]
						{
							typeof(HttpContent).Name,
							"multipart/"
						});
					}
					return null;
				}
				else
				{
					string text = null;
					foreach (NameValueHeaderValue nameValueHeaderValue in contentType.Parameters)
					{
						if (nameValueHeaderValue.Name.Equals("boundary", StringComparison.OrdinalIgnoreCase))
						{
							text = FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value);
							break;
						}
					}
					if (text != null)
					{
						return text;
					}
					if (throwOnError)
					{
						throw Error.Argument("content", Resources.ReadAsMimeMultipartArgumentNoBoundary, new object[]
						{
							typeof(HttpContent).Name,
							"multipart",
							"boundary"
						});
					}
					return null;
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0000BC9C File Offset: 0x00009E9C
		private void CleanupCurrentBodyPart()
		{
			if (this._currentBodyPart != null)
			{
				this._currentBodyPart.Dispose();
				this._currentBodyPart = null;
			}
		}

		// Token: 0x04000115 RID: 277
		internal const long DefaultMaxMessageSize = 9223372036854775807L;

		// Token: 0x04000116 RID: 278
		private const int DefaultMaxBodyPartHeaderSize = 4096;

		// Token: 0x04000117 RID: 279
		private MimeMultipartParser _mimeParser;

		// Token: 0x04000118 RID: 280
		private MimeMultipartParser.State _mimeStatus;

		// Token: 0x04000119 RID: 281
		private ArraySegment<byte>[] _parsedBodyPart = new ArraySegment<byte>[2];

		// Token: 0x0400011A RID: 282
		private MimeBodyPart _currentBodyPart;

		// Token: 0x0400011B RID: 283
		private bool _isFirst = true;

		// Token: 0x0400011C RID: 284
		private ParserState _bodyPartHeaderStatus;

		// Token: 0x0400011D RID: 285
		private int _maxBodyPartHeaderSize;

		// Token: 0x0400011E RID: 286
		private MultipartStreamProvider _streamProvider;

		// Token: 0x0400011F RID: 287
		private HttpContent _content;
	}
}

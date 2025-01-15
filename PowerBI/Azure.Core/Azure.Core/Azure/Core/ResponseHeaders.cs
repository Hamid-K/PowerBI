using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000060 RID: 96
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct ResponseHeaders : IEnumerable<HttpHeader>, IEnumerable
	{
		// Token: 0x0600034C RID: 844 RVA: 0x00009CE2 File Offset: 0x00007EE2
		internal ResponseHeaders(Response response)
		{
			this._response = response;
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00009CEC File Offset: 0x00007EEC
		public DateTimeOffset? Date
		{
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.Date, out text) && !this.TryGetValue(HttpHeader.Names.XMsDate, out text))
				{
					return null;
				}
				return new DateTimeOffset?(DateTimeOffset.Parse(text, CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600034E RID: 846 RVA: 0x00009D34 File Offset: 0x00007F34
		[Nullable(2)]
		public string ContentType
		{
			[NullableContext(2)]
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.ContentType, out text))
				{
					return null;
				}
				return text;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009D54 File Offset: 0x00007F54
		public int? ContentLength
		{
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.ContentLength, out text))
				{
					return null;
				}
				int num;
				if (!int.TryParse(text, NumberStyles.Integer, CultureInfo.InvariantCulture, out num))
				{
					throw new OverflowException(string.Format("Failed to parse value of 'Content-Length' header: '{0}'.  If value exceeds {1}, please use 'Response.Headers.ContentLengthLong' instead.", text, int.MaxValue));
				}
				return new int?(num);
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000350 RID: 848 RVA: 0x00009DAC File Offset: 0x00007FAC
		public long? ContentLengthLong
		{
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.ContentLength, out text))
				{
					return null;
				}
				return new long?(long.Parse(text, CultureInfo.InvariantCulture));
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009DE4 File Offset: 0x00007FE4
		public ETag? ETag
		{
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.ETag, out text))
				{
					return null;
				}
				return new ETag?(Azure.ETag.Parse(text));
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000352 RID: 850 RVA: 0x00009E18 File Offset: 0x00008018
		[Nullable(2)]
		public string RequestId
		{
			[NullableContext(2)]
			get
			{
				string text;
				if (!this.TryGetValue(HttpHeader.Names.XMsRequestId, out text))
				{
					return null;
				}
				return text;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00009E38 File Offset: 0x00008038
		internal TimeSpan? RetryAfter
		{
			get
			{
				string text;
				int num;
				if ((this.TryGetValue("retry-after-ms", out text) || this.TryGetValue("x-ms-retry-after-ms", out text)) && int.TryParse(text, out num))
				{
					return new TimeSpan?(TimeSpan.FromMilliseconds((double)num));
				}
				if (this.TryGetValue("Retry-After", out text))
				{
					int num2;
					if (int.TryParse(text, out num2))
					{
						return new TimeSpan?(TimeSpan.FromSeconds((double)num2));
					}
					DateTimeOffset dateTimeOffset;
					if (DateTimeOffset.TryParse(text, out dateTimeOffset))
					{
						return new TimeSpan?(dateTimeOffset - DateTimeOffset.Now);
					}
				}
				return null;
			}
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00009EC5 File Offset: 0x000080C5
		public IEnumerator<HttpHeader> GetEnumerator()
		{
			return this._response.EnumerateHeaders().GetEnumerator();
		}

		// Token: 0x06000355 RID: 853 RVA: 0x00009ED7 File Offset: 0x000080D7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._response.EnumerateHeaders().GetEnumerator();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x00009EE9 File Offset: 0x000080E9
		public bool TryGetValue(string name, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			return this._response.TryGetHeader(name, out value);
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00009EF8 File Offset: 0x000080F8
		public bool TryGetValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
		{
			return this._response.TryGetHeaderValues(name, out values);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x00009F07 File Offset: 0x00008107
		public bool Contains(string name)
		{
			return this._response.ContainsHeader(name);
		}

		// Token: 0x04000161 RID: 353
		private const string RetryAfterHeaderName = "Retry-After";

		// Token: 0x04000162 RID: 354
		private const string RetryAfterMsHeaderName = "retry-after-ms";

		// Token: 0x04000163 RID: 355
		private const string XRetryAfterMsHeaderName = "x-ms-retry-after-ms";

		// Token: 0x04000164 RID: 356
		private readonly Response _response;
	}
}

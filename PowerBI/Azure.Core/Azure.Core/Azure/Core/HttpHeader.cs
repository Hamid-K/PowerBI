using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000046 RID: 70
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct HttpHeader : IEquatable<HttpHeader>
	{
		// Token: 0x060001F6 RID: 502 RVA: 0x00006387 File Offset: 0x00004587
		public HttpHeader(string name, string value)
		{
			if (string.IsNullOrEmpty(name))
			{
				throw new ArgumentException("name shouldn't be null or empty", "name");
			}
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060001F7 RID: 503 RVA: 0x000063AF File Offset: 0x000045AF
		public string Name { get; }

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060001F8 RID: 504 RVA: 0x000063B7 File Offset: 0x000045B7
		public string Value { get; }

		// Token: 0x060001F9 RID: 505 RVA: 0x000063C0 File Offset: 0x000045C0
		public override int GetHashCode()
		{
			HashCodeBuilder hashCodeBuilder = default(HashCodeBuilder);
			hashCodeBuilder.Add<string>(this.Name, StringComparer.OrdinalIgnoreCase);
			hashCodeBuilder.Add<string>(this.Value);
			return hashCodeBuilder.ToHashCode();
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000063FC File Offset: 0x000045FC
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is HttpHeader)
			{
				HttpHeader httpHeader = (HttpHeader)obj;
				return this.Equals(httpHeader);
			}
			return false;
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00006421 File Offset: 0x00004621
		public override string ToString()
		{
			return this.Name + ":" + this.Value;
		}

		// Token: 0x060001FC RID: 508 RVA: 0x00006439 File Offset: 0x00004639
		public bool Equals(HttpHeader other)
		{
			return string.Equals(this.Name, other.Name, StringComparison.OrdinalIgnoreCase) && this.Value.Equals(other.Value, StringComparison.Ordinal);
		}

		// Token: 0x020000E1 RID: 225
		[Nullable(0)]
		public static class Names
		{
			// Token: 0x170001AC RID: 428
			// (get) Token: 0x06000713 RID: 1811 RVA: 0x00018298 File Offset: 0x00016498
			public static string Date
			{
				get
				{
					return "Date";
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x06000714 RID: 1812 RVA: 0x0001829F File Offset: 0x0001649F
			public static string XMsDate
			{
				get
				{
					return "x-ms-date";
				}
			}

			// Token: 0x170001AE RID: 430
			// (get) Token: 0x06000715 RID: 1813 RVA: 0x000182A6 File Offset: 0x000164A6
			public static string ContentType
			{
				get
				{
					return "Content-Type";
				}
			}

			// Token: 0x170001AF RID: 431
			// (get) Token: 0x06000716 RID: 1814 RVA: 0x000182AD File Offset: 0x000164AD
			public static string ContentLength
			{
				get
				{
					return "Content-Length";
				}
			}

			// Token: 0x170001B0 RID: 432
			// (get) Token: 0x06000717 RID: 1815 RVA: 0x000182B4 File Offset: 0x000164B4
			public static string ETag
			{
				get
				{
					return "ETag";
				}
			}

			// Token: 0x170001B1 RID: 433
			// (get) Token: 0x06000718 RID: 1816 RVA: 0x000182BB File Offset: 0x000164BB
			public static string XMsRequestId
			{
				get
				{
					return "x-ms-request-id";
				}
			}

			// Token: 0x170001B2 RID: 434
			// (get) Token: 0x06000719 RID: 1817 RVA: 0x000182C2 File Offset: 0x000164C2
			public static string UserAgent
			{
				get
				{
					return "User-Agent";
				}
			}

			// Token: 0x170001B3 RID: 435
			// (get) Token: 0x0600071A RID: 1818 RVA: 0x000182C9 File Offset: 0x000164C9
			public static string Accept
			{
				get
				{
					return "Accept";
				}
			}

			// Token: 0x170001B4 RID: 436
			// (get) Token: 0x0600071B RID: 1819 RVA: 0x000182D0 File Offset: 0x000164D0
			public static string Authorization
			{
				get
				{
					return "Authorization";
				}
			}

			// Token: 0x170001B5 RID: 437
			// (get) Token: 0x0600071C RID: 1820 RVA: 0x000182D7 File Offset: 0x000164D7
			public static string Range
			{
				get
				{
					return "Range";
				}
			}

			// Token: 0x170001B6 RID: 438
			// (get) Token: 0x0600071D RID: 1821 RVA: 0x000182DE File Offset: 0x000164DE
			public static string XMsRange
			{
				get
				{
					return "x-ms-range";
				}
			}

			// Token: 0x170001B7 RID: 439
			// (get) Token: 0x0600071E RID: 1822 RVA: 0x000182E5 File Offset: 0x000164E5
			public static string IfMatch
			{
				get
				{
					return "If-Match";
				}
			}

			// Token: 0x170001B8 RID: 440
			// (get) Token: 0x0600071F RID: 1823 RVA: 0x000182EC File Offset: 0x000164EC
			public static string IfNoneMatch
			{
				get
				{
					return "If-None-Match";
				}
			}

			// Token: 0x170001B9 RID: 441
			// (get) Token: 0x06000720 RID: 1824 RVA: 0x000182F3 File Offset: 0x000164F3
			public static string IfModifiedSince
			{
				get
				{
					return "If-Modified-Since";
				}
			}

			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000721 RID: 1825 RVA: 0x000182FA File Offset: 0x000164FA
			public static string IfUnmodifiedSince
			{
				get
				{
					return "If-Unmodified-Since";
				}
			}

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000722 RID: 1826 RVA: 0x00018301 File Offset: 0x00016501
			public static string Prefer
			{
				get
				{
					return "Prefer";
				}
			}

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000723 RID: 1827 RVA: 0x00018308 File Offset: 0x00016508
			public static string Referer
			{
				get
				{
					return "Referer";
				}
			}

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000724 RID: 1828 RVA: 0x0001830F File Offset: 0x0001650F
			public static string Host
			{
				get
				{
					return "Host";
				}
			}

			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000725 RID: 1829 RVA: 0x00018316 File Offset: 0x00016516
			public static string ContentDisposition
			{
				get
				{
					return "Content-Disposition";
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000726 RID: 1830 RVA: 0x0001831D File Offset: 0x0001651D
			public static string WwwAuthenticate
			{
				get
				{
					return "WWW-Authenticate";
				}
			}
		}

		// Token: 0x020000E2 RID: 226
		[Nullable(0)]
		public static class Common
		{
			// Token: 0x04000301 RID: 769
			private const string ApplicationJson = "application/json";

			// Token: 0x04000302 RID: 770
			private const string ApplicationOctetStream = "application/octet-stream";

			// Token: 0x04000303 RID: 771
			private const string ApplicationFormUrlEncoded = "application/x-www-form-urlencoded";

			// Token: 0x04000304 RID: 772
			public static readonly HttpHeader JsonContentType = new HttpHeader(HttpHeader.Names.ContentType, "application/json");

			// Token: 0x04000305 RID: 773
			public static readonly HttpHeader JsonAccept = new HttpHeader(HttpHeader.Names.Accept, "application/json");

			// Token: 0x04000306 RID: 774
			public static readonly HttpHeader OctetStreamContentType = new HttpHeader(HttpHeader.Names.ContentType, "application/octet-stream");

			// Token: 0x04000307 RID: 775
			public static readonly HttpHeader FormUrlEncodedContentType = new HttpHeader(HttpHeader.Names.ContentType, "application/x-www-form-urlencoded");
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200005A RID: 90
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct RequestMethod : IEquatable<RequestMethod>
	{
		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060002DF RID: 735 RVA: 0x000088F0 File Offset: 0x00006AF0
		public string Method { get; }

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x000088F8 File Offset: 0x00006AF8
		public static RequestMethod Get { get; } = new RequestMethod("GET");

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x000088FF File Offset: 0x00006AFF
		public static RequestMethod Post { get; } = new RequestMethod("POST");

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x00008906 File Offset: 0x00006B06
		public static RequestMethod Put { get; } = new RequestMethod("PUT");

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000890D File Offset: 0x00006B0D
		public static RequestMethod Patch { get; } = new RequestMethod("PATCH");

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x00008914 File Offset: 0x00006B14
		public static RequestMethod Delete { get; } = new RequestMethod("DELETE");

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000891B File Offset: 0x00006B1B
		public static RequestMethod Head { get; } = new RequestMethod("HEAD");

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x00008922 File Offset: 0x00006B22
		public static RequestMethod Options { get; } = new RequestMethod("OPTIONS");

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x00008929 File Offset: 0x00006B29
		public static RequestMethod Trace { get; } = new RequestMethod("TRACE");

		// Token: 0x060002E8 RID: 744 RVA: 0x00008930 File Offset: 0x00006B30
		public RequestMethod(string method)
		{
			Argument.AssertNotNull<string>(method, "method");
			this.Method = method.ToUpperInvariant();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0000894C File Offset: 0x00006B4C
		public static RequestMethod Parse(string method)
		{
			Argument.AssertNotNull<string>(method, "method");
			if (method.Length == 3)
			{
				if (string.Equals(method, "GET", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Get;
				}
				if (string.Equals(method, "PUT", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Put;
				}
			}
			else if (method.Length == 4)
			{
				if (string.Equals(method, "POST", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Post;
				}
				if (string.Equals(method, "HEAD", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Head;
				}
			}
			else
			{
				if (string.Equals(method, "PATCH", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Patch;
				}
				if (string.Equals(method, "DELETE", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Delete;
				}
				if (string.Equals(method, "OPTIONS", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Options;
				}
				if (string.Equals(method, "TRACE", StringComparison.OrdinalIgnoreCase))
				{
					return RequestMethod.Trace;
				}
			}
			return new RequestMethod(method);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x00008A1F File Offset: 0x00006C1F
		public bool Equals(RequestMethod other)
		{
			return string.Equals(this.Method, other.Method, StringComparison.Ordinal);
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00008A34 File Offset: 0x00006C34
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is RequestMethod)
			{
				RequestMethod requestMethod = (RequestMethod)obj;
				return this.Equals(requestMethod);
			}
			return false;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x00008A59 File Offset: 0x00006C59
		public override int GetHashCode()
		{
			return this.Method.GetHashCodeOrdinal();
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00008A66 File Offset: 0x00006C66
		public static bool operator ==(RequestMethod left, RequestMethod right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00008A70 File Offset: 0x00006C70
		public static bool operator !=(RequestMethod left, RequestMethod right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00008A7D File Offset: 0x00006C7D
		public override string ToString()
		{
			return this.Method ?? "<null>";
		}
	}
}

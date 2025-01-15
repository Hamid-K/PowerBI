using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000059 RID: 89
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct RequestHeaders : IEnumerable<HttpHeader>, IEnumerable
	{
		// Token: 0x060002D5 RID: 725 RVA: 0x00008850 File Offset: 0x00006A50
		internal RequestHeaders(Request request)
		{
			this._request = request;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00008859 File Offset: 0x00006A59
		public IEnumerator<HttpHeader> GetEnumerator()
		{
			return this._request.EnumerateHeaders().GetEnumerator();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000886B File Offset: 0x00006A6B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this._request.EnumerateHeaders().GetEnumerator();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x0000887D File Offset: 0x00006A7D
		public void Add(HttpHeader header)
		{
			this._request.AddHeader(header.Name, header.Value);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00008898 File Offset: 0x00006A98
		public void Add(string name, string value)
		{
			this._request.AddHeader(name, value);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x000088A7 File Offset: 0x00006AA7
		public bool TryGetValue(string name, [Nullable(2)] [NotNullWhen(true)] out string value)
		{
			return this._request.TryGetHeader(name, out value);
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000088B6 File Offset: 0x00006AB6
		public bool TryGetValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values)
		{
			return this._request.TryGetHeaderValues(name, out values);
		}

		// Token: 0x060002DC RID: 732 RVA: 0x000088C5 File Offset: 0x00006AC5
		public bool Contains(string name)
		{
			return this._request.ContainsHeader(name);
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000088D3 File Offset: 0x00006AD3
		public void SetValue(string name, string value)
		{
			this._request.SetHeader(name, value);
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000088E2 File Offset: 0x00006AE2
		public bool Remove(string name)
		{
			return this._request.RemoveHeader(name);
		}

		// Token: 0x04000133 RID: 307
		private readonly Request _request;
	}
}

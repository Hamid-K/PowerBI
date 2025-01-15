using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000057 RID: 87
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Request : IDisposable
	{
		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060002AF RID: 687 RVA: 0x000086D8 File Offset: 0x000068D8
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x000086FD File Offset: 0x000068FD
		public virtual RequestUriBuilder Uri
		{
			get
			{
				RequestUriBuilder requestUriBuilder;
				if ((requestUriBuilder = this._uri) == null)
				{
					requestUriBuilder = (this._uri = new RequestUriBuilder());
				}
				return requestUriBuilder;
			}
			set
			{
				Argument.AssertNotNull<RequestUriBuilder>(value, "value");
				this._uri = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00008711 File Offset: 0x00006911
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00008719 File Offset: 0x00006919
		public virtual RequestMethod Method { get; set; }

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060002B3 RID: 691 RVA: 0x00008722 File Offset: 0x00006922
		// (set) Token: 0x060002B4 RID: 692 RVA: 0x0000872A File Offset: 0x0000692A
		[Nullable(2)]
		public virtual RequestContent Content
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x060002B5 RID: 693
		protected internal abstract void AddHeader(string name, string value);

		// Token: 0x060002B6 RID: 694
		protected internal abstract bool TryGetHeader(string name, [Nullable(2)] [NotNullWhen(true)] out string value);

		// Token: 0x060002B7 RID: 695
		protected internal abstract bool TryGetHeaderValues(string name, [Nullable(new byte[] { 2, 1 })] [NotNullWhen(true)] out IEnumerable<string> values);

		// Token: 0x060002B8 RID: 696
		protected internal abstract bool ContainsHeader(string name);

		// Token: 0x060002B9 RID: 697 RVA: 0x00008733 File Offset: 0x00006933
		protected internal virtual void SetHeader(string name, string value)
		{
			this.RemoveHeader(name);
			this.AddHeader(name, value);
		}

		// Token: 0x060002BA RID: 698
		protected internal abstract bool RemoveHeader(string name);

		// Token: 0x060002BB RID: 699
		protected internal abstract IEnumerable<HttpHeader> EnumerateHeaders();

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060002BC RID: 700
		// (set) Token: 0x060002BD RID: 701
		public abstract string ClientRequestId { get; set; }

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00008745 File Offset: 0x00006945
		public RequestHeaders Headers
		{
			get
			{
				return new RequestHeaders(this);
			}
		}

		// Token: 0x060002BF RID: 703
		public abstract void Dispose();

		// Token: 0x0400012E RID: 302
		[Nullable(2)]
		private RequestUriBuilder _uri;
	}
}

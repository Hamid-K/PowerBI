using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x0200003E RID: 62
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ClientOptions
	{
		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060001AF RID: 431 RVA: 0x000059A2 File Offset: 0x00003BA2
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x000059AA File Offset: 0x00003BAA
		internal bool IsCustomTransportSet { get; private set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000059B3 File Offset: 0x00003BB3
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000059BA File Offset: 0x00003BBA
		public static ClientOptions Default { get; private set; } = new DefaultClientOptions();

		// Token: 0x060001B3 RID: 435 RVA: 0x000059C2 File Offset: 0x00003BC2
		internal static void ResetDefaultOptions()
		{
			ClientOptions.Default = new DefaultClientOptions();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000059CE File Offset: 0x00003BCE
		protected ClientOptions()
			: this(ClientOptions.Default, null)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000059DC File Offset: 0x00003BDC
		[NullableContext(2)]
		protected ClientOptions(DiagnosticsOptions diagnostics)
			: this(ClientOptions.Default, diagnostics)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000059EC File Offset: 0x00003BEC
		[NullableContext(2)]
		internal ClientOptions(ClientOptions clientOptions, DiagnosticsOptions diagnostics)
		{
			if (clientOptions != null)
			{
				this.Retry = new RetryOptions(clientOptions.Retry);
				this.RetryPolicy = clientOptions.RetryPolicy;
				this.Diagnostics = diagnostics ?? new DiagnosticsOptions(clientOptions.Diagnostics);
				this._transport = clientOptions.Transport;
				if (clientOptions.Policies != null)
				{
					this.Policies = new List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>>(clientOptions.Policies);
					return;
				}
			}
			else
			{
				this._transport = HttpPipelineTransport.Create(null);
				this.Diagnostics = new DiagnosticsOptions(null);
				this.Retry = new RetryOptions(null);
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00005A7F File Offset: 0x00003C7F
		// (set) Token: 0x060001B8 RID: 440 RVA: 0x00005A87 File Offset: 0x00003C87
		public HttpPipelineTransport Transport
		{
			get
			{
				return this._transport;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this._transport = value;
				this.IsCustomTransportSet = true;
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00005AA6 File Offset: 0x00003CA6
		public DiagnosticsOptions Diagnostics { get; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060001BA RID: 442 RVA: 0x00005AAE File Offset: 0x00003CAE
		public RetryOptions Retry { get; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00005AB6 File Offset: 0x00003CB6
		// (set) Token: 0x060001BC RID: 444 RVA: 0x00005ABE File Offset: 0x00003CBE
		[Nullable(2)]
		public HttpPipelinePolicy RetryPolicy
		{
			[NullableContext(2)]
			get;
			[NullableContext(2)]
			set;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00005AC8 File Offset: 0x00003CC8
		public void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
		{
			if (position != HttpPipelinePosition.PerCall && position != HttpPipelinePosition.PerRetry && position != HttpPipelinePosition.BeforeTransport)
			{
				throw new ArgumentOutOfRangeException("position", position, null);
			}
			if (this.Policies == null)
			{
				this.Policies = new List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>>();
			}
			this.Policies.Add(new global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>(position, policy));
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060001BE RID: 446 RVA: 0x00005B19 File Offset: 0x00003D19
		// (set) Token: 0x060001BF RID: 447 RVA: 0x00005B21 File Offset: 0x00003D21
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
		[Nullable(new byte[] { 2, 0, 1 })]
		internal List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> Policies
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
			[return: Nullable(new byte[] { 2, 0, 1 })]
			get;
			[param: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
			[param: Nullable(new byte[] { 2, 0, 1 })]
			private set;
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00005B2A File Offset: 0x00003D2A
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005B33 File Offset: 0x00003D33
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00005B3B File Offset: 0x00003D3B
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x040000CA RID: 202
		private HttpPipelineTransport _transport;
	}
}

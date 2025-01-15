using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Azure.Core;
using Azure.Core.Pipeline;

namespace Azure
{
	// Token: 0x0200002C RID: 44
	[NullableContext(1)]
	[Nullable(0)]
	public class RequestContext
	{
		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x000031FE File Offset: 0x000013FE
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })]
		[Nullable(new byte[] { 2, 0 })]
		internal global::System.ValueTuple<int, bool>[] StatusCodes
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })]
			[return: Nullable(new byte[] { 2, 0 })]
			get
			{
				return this._statusCodes;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00003206 File Offset: 0x00001406
		[Nullable(new byte[] { 2, 1 })]
		internal ResponseClassificationHandler[] Handlers
		{
			[return: Nullable(new byte[] { 2, 1 })]
			get
			{
				return this._handlers;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x0000320E File Offset: 0x0000140E
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00003216 File Offset: 0x00001416
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

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000321F File Offset: 0x0000141F
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00003227 File Offset: 0x00001427
		public ErrorOptions ErrorOptions { get; set; }

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00003230 File Offset: 0x00001430
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00003238 File Offset: 0x00001438
		public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

		// Token: 0x060000CC RID: 204 RVA: 0x00003254 File Offset: 0x00001454
		public static implicit operator RequestContext(ErrorOptions options)
		{
			return new RequestContext
			{
				ErrorOptions = options
			};
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00003264 File Offset: 0x00001464
		public void AddPolicy(HttpPipelinePolicy policy, HttpPipelinePosition position)
		{
			if (this.Policies == null)
			{
				this.Policies = new List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>>();
			}
			this.Policies.Add(new global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>(position, policy));
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00003298 File Offset: 0x00001498
		public void AddClassifier(int statusCode, bool isError)
		{
			Argument.AssertInRange<int>(statusCode, 100, 599, "statusCode");
			if (this._frozen)
			{
				throw new InvalidOperationException("Cannot modify classifiers after this type has been used in a method call.");
			}
			int num = ((this._statusCodes == null) ? 0 : this._statusCodes.Length);
			Array.Resize<global::System.ValueTuple<int, bool>>(ref this._statusCodes, num + 1);
			Array.Copy(this._statusCodes, 0, this._statusCodes, 1, num);
			this._statusCodes[0] = new global::System.ValueTuple<int, bool>(statusCode, isError);
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00003314 File Offset: 0x00001514
		public void AddClassifier(ResponseClassificationHandler classifier)
		{
			if (this._frozen)
			{
				throw new InvalidOperationException("Cannot modify classifiers after this type has been used in a method call.");
			}
			int num = ((this._handlers == null) ? 0 : this._handlers.Length);
			Array.Resize<ResponseClassificationHandler>(ref this._handlers, num + 1);
			Array.Copy(this._handlers, 0, this._handlers, 1, num);
			this._handlers[0] = classifier;
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00003373 File Offset: 0x00001573
		internal void Freeze()
		{
			this._frozen = true;
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x0000337C File Offset: 0x0000157C
		internal ResponseClassifier Apply(ResponseClassifier classifier)
		{
			if (this._statusCodes == null && this._handlers == null)
			{
				return classifier;
			}
			StatusCodeClassifier statusCodeClassifier = classifier as StatusCodeClassifier;
			if (statusCodeClassifier != null)
			{
				StatusCodeClassifier statusCodeClassifier2 = statusCodeClassifier.Clone();
				statusCodeClassifier2.Handlers = this._handlers;
				if (this._statusCodes != null)
				{
					foreach (global::System.ValueTuple<int, bool> valueTuple in this._statusCodes)
					{
						statusCodeClassifier2.AddClassifier(valueTuple.Item1, valueTuple.Item2);
					}
				}
				return statusCodeClassifier2;
			}
			return new ChainingClassifier(this._statusCodes, this._handlers, classifier);
		}

		// Token: 0x04000049 RID: 73
		private bool _frozen;

		// Token: 0x0400004A RID: 74
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })]
		[Nullable(new byte[] { 2, 0 })]
		private global::System.ValueTuple<int, bool>[] _statusCodes;

		// Token: 0x0400004B RID: 75
		[Nullable(new byte[] { 2, 1 })]
		private ResponseClassificationHandler[] _handlers;
	}
}

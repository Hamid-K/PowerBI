using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Threading;
using Azure.Core.Pipeline;

namespace Azure.Core
{
	// Token: 0x02000047 RID: 71
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class HttpMessage : IDisposable
	{
		// Token: 0x060001FD RID: 509 RVA: 0x00006465 File Offset: 0x00004665
		public HttpMessage(Request request, ResponseClassifier responseClassifier)
		{
			Argument.AssertNotNull<Request>(request, "Request");
			this.Request = request;
			this.ResponseClassifier = responseClassifier;
			this.BufferResponse = true;
			this._propertyBag = new ArrayBackedPropertyBag<ulong, object>();
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00006498 File Offset: 0x00004698
		public Request Request { get; }

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060001FF RID: 511 RVA: 0x000064A0 File Offset: 0x000046A0
		// (set) Token: 0x06000200 RID: 512 RVA: 0x000064BB File Offset: 0x000046BB
		public Response Response
		{
			get
			{
				if (this._response == null)
				{
					throw new InvalidOperationException("Response was not set, make sure SendAsync was called");
				}
				return this._response;
			}
			set
			{
				this._response = value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000201 RID: 513 RVA: 0x000064C4 File Offset: 0x000046C4
		public bool HasResponse
		{
			get
			{
				return this._response != null;
			}
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000064CF File Offset: 0x000046CF
		internal void ClearResponse()
		{
			this._response = null;
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x06000203 RID: 515 RVA: 0x000064D8 File Offset: 0x000046D8
		// (set) Token: 0x06000204 RID: 516 RVA: 0x000064E0 File Offset: 0x000046E0
		public CancellationToken CancellationToken { get; internal set; }

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000205 RID: 517 RVA: 0x000064E9 File Offset: 0x000046E9
		// (set) Token: 0x06000206 RID: 518 RVA: 0x000064F1 File Offset: 0x000046F1
		public ResponseClassifier ResponseClassifier { get; set; }

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x06000207 RID: 519 RVA: 0x000064FA File Offset: 0x000046FA
		// (set) Token: 0x06000208 RID: 520 RVA: 0x00006502 File Offset: 0x00004702
		public bool BufferResponse { get; set; }

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x06000209 RID: 521 RVA: 0x0000650B File Offset: 0x0000470B
		// (set) Token: 0x0600020A RID: 522 RVA: 0x00006513 File Offset: 0x00004713
		public TimeSpan? NetworkTimeout { get; set; }

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600020B RID: 523 RVA: 0x0000651C File Offset: 0x0000471C
		// (set) Token: 0x0600020C RID: 524 RVA: 0x00006524 File Offset: 0x00004724
		internal int RetryNumber { get; set; }

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x0600020D RID: 525 RVA: 0x0000652D File Offset: 0x0000472D
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00006535 File Offset: 0x00004735
		internal DateTimeOffset ProcessingStartTime { get; set; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x0600020F RID: 527 RVA: 0x0000653E File Offset: 0x0000473E
		public MessageProcessingContext ProcessingContext
		{
			get
			{
				return new MessageProcessingContext(this);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00006548 File Offset: 0x00004748
		[NullableContext(2)]
		internal void ApplyRequestContext(RequestContext context, ResponseClassifier classifier)
		{
			if (context == null)
			{
				return;
			}
			context.Freeze();
			List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> policies = context.Policies;
			if (policies != null && policies.Count > 0)
			{
				if (this.Policies == null)
				{
					this.Policies = new List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>>(context.Policies.Count);
				}
				this.Policies.AddRange(context.Policies);
			}
			if (classifier != null)
			{
				this.ResponseClassifier = context.Apply(classifier);
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000211 RID: 529 RVA: 0x000065B7 File Offset: 0x000047B7
		// (set) Token: 0x06000212 RID: 530 RVA: 0x000065BF File Offset: 0x000047BF
		[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
		[Nullable(new byte[] { 2, 0, 1 })]
		internal List<global::System.ValueTuple<HttpPipelinePosition, HttpPipelinePolicy>> Policies
		{
			[return: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
			[return: Nullable(new byte[] { 2, 0, 1 })]
			get;
			[param: global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Position", "Policy" })]
			[param: Nullable(new byte[] { 2, 0, 1 })]
			set;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000065C8 File Offset: 0x000047C8
		public bool TryGetProperty(string name, [Nullable(2)] out object value)
		{
			value = null;
			object obj;
			return !this._propertyBag.IsEmpty && this._propertyBag.TryGetValue((ulong)(long)typeof(HttpMessage.MessagePropertyKey).TypeHandle.Value, out obj) && ((Dictionary<string, object>)obj).TryGetValue(name, out value);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00006620 File Offset: 0x00004820
		public void SetProperty(string name, object value)
		{
			object obj;
			Dictionary<string, object> dictionary;
			if (!this._propertyBag.TryGetValue((ulong)(long)typeof(HttpMessage.MessagePropertyKey).TypeHandle.Value, out obj))
			{
				dictionary = new Dictionary<string, object>();
				this._propertyBag.Set((ulong)(long)typeof(HttpMessage.MessagePropertyKey).TypeHandle.Value, dictionary);
			}
			else
			{
				dictionary = (Dictionary<string, object>)obj;
			}
			dictionary[name] = value;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00006698 File Offset: 0x00004898
		public bool TryGetProperty(Type type, [Nullable(2)] out object value)
		{
			return this._propertyBag.TryGetValue((ulong)(long)type.TypeHandle.Value, out value);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000066C4 File Offset: 0x000048C4
		public void SetProperty(Type type, object value)
		{
			this._propertyBag.Set((ulong)(long)type.TypeHandle.Value, value);
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000066F0 File Offset: 0x000048F0
		[NullableContext(2)]
		public Stream ExtractResponseContent()
		{
			Response response = this._response;
			Stream stream = ((response != null) ? response.ContentStream : null);
			HttpMessage.ResponseShouldNotBeUsedStream responseShouldNotBeUsedStream = stream as HttpMessage.ResponseShouldNotBeUsedStream;
			if (responseShouldNotBeUsedStream != null)
			{
				return responseShouldNotBeUsedStream.Original;
			}
			if (stream == null)
			{
				return null;
			}
			this._response.ContentStream = new HttpMessage.ResponseShouldNotBeUsedStream(this._response.ContentStream);
			return stream;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00006744 File Offset: 0x00004944
		public void Dispose()
		{
			this.Request.Dispose();
			this._propertyBag.Dispose();
			Response response = this._response;
			if (response != null)
			{
				this._response = null;
				response.Dispose();
			}
		}

		// Token: 0x040000E7 RID: 231
		[Nullable(new byte[] { 0, 1 })]
		private ArrayBackedPropertyBag<ulong, object> _propertyBag;

		// Token: 0x040000E8 RID: 232
		[Nullable(2)]
		private Response _response;

		// Token: 0x020000E3 RID: 227
		[Nullable(0)]
		private class ResponseShouldNotBeUsedStream : Stream
		{
			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000728 RID: 1832 RVA: 0x00018381 File Offset: 0x00016581
			public Stream Original { get; }

			// Token: 0x06000729 RID: 1833 RVA: 0x00018389 File Offset: 0x00016589
			public ResponseShouldNotBeUsedStream(Stream original)
			{
				this.Original = original;
			}

			// Token: 0x0600072A RID: 1834 RVA: 0x00018398 File Offset: 0x00016598
			private static Exception CreateException()
			{
				return new InvalidOperationException("The operation has called ExtractResponseContent and will provide the stream as part of its response type.");
			}

			// Token: 0x0600072B RID: 1835 RVA: 0x000183A4 File Offset: 0x000165A4
			public override void Flush()
			{
				throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
			}

			// Token: 0x0600072C RID: 1836 RVA: 0x000183AB File Offset: 0x000165AB
			public override int Read(byte[] buffer, int offset, int count)
			{
				throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
			}

			// Token: 0x0600072D RID: 1837 RVA: 0x000183B2 File Offset: 0x000165B2
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
			}

			// Token: 0x0600072E RID: 1838 RVA: 0x000183B9 File Offset: 0x000165B9
			public override void SetLength(long value)
			{
				throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
			}

			// Token: 0x0600072F RID: 1839 RVA: 0x000183C0 File Offset: 0x000165C0
			public override void Write(byte[] buffer, int offset, int count)
			{
				throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000730 RID: 1840 RVA: 0x000183C7 File Offset: 0x000165C7
			public override bool CanRead
			{
				get
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
			}

			// Token: 0x170001C2 RID: 450
			// (get) Token: 0x06000731 RID: 1841 RVA: 0x000183CE File Offset: 0x000165CE
			public override bool CanSeek
			{
				get
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
			}

			// Token: 0x170001C3 RID: 451
			// (get) Token: 0x06000732 RID: 1842 RVA: 0x000183D5 File Offset: 0x000165D5
			public override bool CanWrite
			{
				get
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
			}

			// Token: 0x170001C4 RID: 452
			// (get) Token: 0x06000733 RID: 1843 RVA: 0x000183DC File Offset: 0x000165DC
			public override long Length
			{
				get
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
			}

			// Token: 0x170001C5 RID: 453
			// (get) Token: 0x06000734 RID: 1844 RVA: 0x000183E3 File Offset: 0x000165E3
			// (set) Token: 0x06000735 RID: 1845 RVA: 0x000183EA File Offset: 0x000165EA
			public override long Position
			{
				get
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
				set
				{
					throw HttpMessage.ResponseShouldNotBeUsedStream.CreateException();
				}
			}
		}

		// Token: 0x020000E4 RID: 228
		[NullableContext(0)]
		private class MessagePropertyKey
		{
		}
	}
}

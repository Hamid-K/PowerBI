using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200003D RID: 61
	[NullableContext(1)]
	[Nullable(0)]
	internal class ChainingClassifier : ResponseClassifier
	{
		// Token: 0x060001AC RID: 428 RVA: 0x000058BC File Offset: 0x00003ABC
		public ChainingClassifier([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })] [Nullable(new byte[] { 2, 0 })] global::System.ValueTuple<int, bool>[] statusCodes, [Nullable(new byte[] { 2, 1 })] ResponseClassificationHandler[] handlers, ResponseClassifier endOfChain)
		{
			if (handlers != null)
			{
				this.AddClassifiers(handlers);
			}
			if (statusCodes != null)
			{
				ChainingClassifier.StatusCodeHandler[] array = new ChainingClassifier.StatusCodeHandler[]
				{
					new ChainingClassifier.StatusCodeHandler(statusCodes)
				};
				ResponseClassificationHandler[] array2 = array;
				this.AddClassifiers(new ReadOnlySpan<ResponseClassificationHandler>(array2));
			}
			this._endOfChain = endOfChain;
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00005908 File Offset: 0x00003B08
		public override bool IsErrorResponse(HttpMessage message)
		{
			if (this._handlers != null)
			{
				ResponseClassificationHandler[] handlers = this._handlers;
				for (int i = 0; i < handlers.Length; i++)
				{
					bool flag;
					if (handlers[i].TryClassify(message, out flag))
					{
						return flag;
					}
				}
			}
			return this._endOfChain.IsErrorResponse(message);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00005950 File Offset: 0x00003B50
		private void AddClassifiers([Nullable(new byte[] { 0, 1 })] ReadOnlySpan<ResponseClassificationHandler> handlers)
		{
			int num = ((this._handlers == null) ? 0 : this._handlers.Length);
			Array.Resize<ResponseClassificationHandler>(ref this._handlers, num + handlers.Length);
			Span<ResponseClassificationHandler> span = new Span<ResponseClassificationHandler>(this._handlers, num, handlers.Length);
			handlers.CopyTo(span);
		}

		// Token: 0x040000C8 RID: 200
		[Nullable(new byte[] { 2, 1 })]
		private ResponseClassificationHandler[] _handlers;

		// Token: 0x040000C9 RID: 201
		private ResponseClassifier _endOfChain;

		// Token: 0x020000DF RID: 223
		[NullableContext(0)]
		private class StatusCodeHandler : ResponseClassificationHandler
		{
			// Token: 0x0600070C RID: 1804 RVA: 0x000181D2 File Offset: 0x000163D2
			public StatusCodeHandler([global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })] [Nullable(new byte[] { 1, 0 })] global::System.ValueTuple<int, bool>[] statusCodes)
			{
				this._statusCodes = statusCodes;
			}

			// Token: 0x0600070D RID: 1805 RVA: 0x000181E4 File Offset: 0x000163E4
			[NullableContext(1)]
			public override bool TryClassify(HttpMessage message, out bool isError)
			{
				foreach (global::System.ValueTuple<int, bool> valueTuple in this._statusCodes)
				{
					if (valueTuple.Item1 == message.Response.Status)
					{
						isError = valueTuple.Item2;
						return true;
					}
				}
				isError = false;
				return false;
			}

			// Token: 0x040002FE RID: 766
			[global::System.Runtime.CompilerServices.TupleElementNames(new string[] { "Status", "IsError" })]
			[Nullable(new byte[] { 1, 0 })]
			private global::System.ValueTuple<int, bool>[] _statusCodes;
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x0200005A RID: 90
	internal class FSharpFunction
	{
		// Token: 0x06000522 RID: 1314 RVA: 0x0001607D File Offset: 0x0001427D
		[NullableContext(2)]
		public FSharpFunction(object instance, [Nullable(new byte[] { 0, 2, 0 })] MethodCall<object, object> invoker)
		{
			this._instance = instance;
			this._invoker = invoker;
		}

		// Token: 0x06000523 RID: 1315 RVA: 0x00016093 File Offset: 0x00014293
		public object Invoke(params object[] args)
		{
			return this._invoker(this._instance, args);
		}

		// Token: 0x040001CC RID: 460
		[Nullable(2)]
		private readonly object _instance;

		// Token: 0x040001CD RID: 461
		[Nullable(new byte[] { 0, 2, 0 })]
		private readonly MethodCall<object, object> _invoker;
	}
}

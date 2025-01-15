using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F6 RID: 246
	public interface IFunctionValue : IValue
	{
		// Token: 0x1700018C RID: 396
		// (get) Token: 0x060003C0 RID: 960
		bool IsResourceAccessFunction { get; }

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x060003C1 RID: 961
		IFunctionExpression FunctionExpression { get; }

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x060003C2 RID: 962
		string PrimaryResourceKind { get; }
	}
}

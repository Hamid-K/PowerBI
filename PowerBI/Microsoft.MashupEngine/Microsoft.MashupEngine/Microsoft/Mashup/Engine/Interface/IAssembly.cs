using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000033 RID: 51
	public interface IAssembly
	{
		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000125 RID: 293
		IRecordValue Exports { get; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000126 RID: 294
		IFunctionValue Function { get; }
	}
}

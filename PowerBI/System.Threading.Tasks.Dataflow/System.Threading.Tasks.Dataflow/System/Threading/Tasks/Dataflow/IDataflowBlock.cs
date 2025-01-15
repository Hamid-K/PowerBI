using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Dataflow
{
	// Token: 0x02000021 RID: 33
	[NullableContext(1)]
	public interface IDataflowBlock
	{
		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000AC RID: 172
		Task Completion { get; }

		// Token: 0x060000AD RID: 173
		void Complete();

		// Token: 0x060000AE RID: 174
		void Fault(Exception exception);
	}
}

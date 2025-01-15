using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x02000031 RID: 49
	public interface IFieldReader<T> : IDisposable
	{
		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011C RID: 284
		T Current { get; }

		// Token: 0x0600011D RID: 285
		bool MoveNextRow();

		// Token: 0x0600011E RID: 286
		bool MoveNextField();
	}
}

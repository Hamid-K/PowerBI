using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F1 RID: 241
	public interface ITableTypeValue : ITypeValue, IValue
	{
		// Token: 0x1700017D RID: 381
		// (get) Token: 0x060003AB RID: 939
		IRecordTypeValue ItemType { get; }
	}
}

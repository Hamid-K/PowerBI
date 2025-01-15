using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000F2 RID: 242
	public interface IRecordTypeValue : ITypeValue, IValue
	{
		// Token: 0x1700017E RID: 382
		// (get) Token: 0x060003AC RID: 940
		bool Open { get; }

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x060003AD RID: 941
		IRecordValue Fields { get; }
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000EF RID: 239
	public interface ITypeValue : IValue
	{
		// Token: 0x0600039C RID: 924
		bool IsCompatibleWith(ITypeValue type);

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x0600039D RID: 925
		bool IsNullable { get; }

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x0600039E RID: 926
		bool IsFunctionType { get; }

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x0600039F RID: 927
		bool IsListType { get; }

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060003A0 RID: 928
		bool IsTableType { get; }

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060003A1 RID: 929
		bool IsRecordType { get; }

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x060003A2 RID: 930
		ITypeValue Nullable { get; }

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x060003A3 RID: 931
		ITypeValue NonNullable { get; }

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x060003A4 RID: 932
		IFunctionTypeValue AsFunctionType { get; }

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x060003A5 RID: 933
		IListTypeValue AsListType { get; }

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x060003A6 RID: 934
		ITableTypeValue AsTableType { get; }

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x060003A7 RID: 935
		IRecordTypeValue AsRecordType { get; }

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060003A8 RID: 936
		IEnumerable<string> KeyColumnNames { get; }

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x060003A9 RID: 937
		IValue DomainValues { get; }
	}
}

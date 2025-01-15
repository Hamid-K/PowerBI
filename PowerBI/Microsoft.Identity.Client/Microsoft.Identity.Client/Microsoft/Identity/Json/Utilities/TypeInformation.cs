using System;

namespace Microsoft.Identity.Json.Utilities
{
	// Token: 0x02000048 RID: 72
	internal class TypeInformation
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x00010E1A File Offset: 0x0000F01A
		public Type Type { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x00010E22 File Offset: 0x0000F022
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x06000467 RID: 1127 RVA: 0x00010E2A File Offset: 0x0000F02A
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}

using System;
using System.Runtime.CompilerServices;

namespace Newtonsoft.Json.Utilities
{
	// Token: 0x02000048 RID: 72
	[NullableContext(1)]
	[Nullable(0)]
	internal class TypeInformation
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001135A File Offset: 0x0000F55A
		public Type Type { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x00011362 File Offset: 0x0000F562
		public PrimitiveTypeCode TypeCode { get; }

		// Token: 0x0600046E RID: 1134 RVA: 0x0001136A File Offset: 0x0000F56A
		public TypeInformation(Type type, PrimitiveTypeCode typeCode)
		{
			this.Type = type;
			this.TypeCode = typeCode;
		}
	}
}

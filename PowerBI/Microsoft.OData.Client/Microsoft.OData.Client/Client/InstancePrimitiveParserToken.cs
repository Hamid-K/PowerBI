using System;

namespace Microsoft.OData.Client
{
	// Token: 0x0200008E RID: 142
	internal class InstancePrimitiveParserToken<T> : PrimitiveParserToken
	{
		// Token: 0x06000450 RID: 1104 RVA: 0x0000F3D9 File Offset: 0x0000D5D9
		internal InstancePrimitiveParserToken(T instance)
		{
			this.Instance = instance;
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		internal T Instance { get; private set; }

		// Token: 0x06000453 RID: 1107 RVA: 0x0000F3F9 File Offset: 0x0000D5F9
		internal override object Materialize(Type clrType)
		{
			return this.Instance;
		}
	}
}

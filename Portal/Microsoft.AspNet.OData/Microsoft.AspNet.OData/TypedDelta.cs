using System;

namespace Microsoft.AspNet.OData
{
	// Token: 0x02000056 RID: 86
	public abstract class TypedDelta : Delta
	{
		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600024D RID: 589
		public abstract Type StructuredType { get; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600024E RID: 590
		public abstract Type ExpectedClrType { get; }

		// Token: 0x0600024F RID: 591 RVA: 0x0000A92A File Offset: 0x00008B2A
		internal static bool IsDeltaOfT(Type type)
		{
			return type != null && type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Delta<>);
		}
	}
}

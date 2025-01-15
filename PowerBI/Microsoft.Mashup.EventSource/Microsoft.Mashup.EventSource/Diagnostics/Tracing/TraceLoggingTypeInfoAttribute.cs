using System;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	internal sealed class TraceLoggingTypeInfoAttribute : Attribute
	{
		// Token: 0x06000152 RID: 338 RVA: 0x0000AD03 File Offset: 0x00008F03
		public TraceLoggingTypeInfoAttribute(Type typeInfoType)
		{
			this.typeInfoType = typeInfoType;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000AD12 File Offset: 0x00008F12
		public Type TypeInfoType
		{
			get
			{
				return this.typeInfoType;
			}
		}

		// Token: 0x040000AF RID: 175
		private readonly Type typeInfoType;
	}
}

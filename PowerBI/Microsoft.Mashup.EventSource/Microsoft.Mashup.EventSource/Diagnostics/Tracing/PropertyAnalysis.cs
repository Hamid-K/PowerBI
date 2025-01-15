using System;
using System.Reflection;

namespace Microsoft.Diagnostics.Tracing
{
	// Token: 0x0200003D RID: 61
	internal sealed class PropertyAnalysis
	{
		// Token: 0x060001D8 RID: 472 RVA: 0x0000C34D File Offset: 0x0000A54D
		public PropertyAnalysis(string name, MethodInfo getterInfo, TraceLoggingTypeInfo typeInfo, EventFieldAttribute fieldAttribute)
		{
			this.name = name;
			this.getterInfo = getterInfo;
			this.typeInfo = typeInfo;
			this.fieldAttribute = fieldAttribute;
		}

		// Token: 0x040000F9 RID: 249
		internal readonly string name;

		// Token: 0x040000FA RID: 250
		internal readonly MethodInfo getterInfo;

		// Token: 0x040000FB RID: 251
		internal readonly TraceLoggingTypeInfo typeInfo;

		// Token: 0x040000FC RID: 252
		internal readonly EventFieldAttribute fieldAttribute;
	}
}

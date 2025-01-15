using System;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000082 RID: 130
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class)]
	internal class TypeReferenceTypeAttribute : Attribute
	{
		// Token: 0x0600042E RID: 1070 RVA: 0x0000C8EB File Offset: 0x0000AAEB
		public TypeReferenceTypeAttribute()
			: this(false, Array.Empty<string>())
		{
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x0000C8F9 File Offset: 0x0000AAF9
		public TypeReferenceTypeAttribute(bool ignoreExtraProperties, string[] internalPropertiesToInclude)
		{
			this.IgnoreExtraProperties = ignoreExtraProperties;
			this.InternalPropertiesToInclude = internalPropertiesToInclude;
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000C90F File Offset: 0x0000AB0F
		public bool IgnoreExtraProperties { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000C917 File Offset: 0x0000AB17
		public string[] InternalPropertiesToInclude { get; }
	}
}

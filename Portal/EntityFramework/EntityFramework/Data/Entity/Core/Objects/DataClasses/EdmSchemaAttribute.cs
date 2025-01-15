using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000473 RID: 1139
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = true)]
	public sealed class EdmSchemaAttribute : Attribute
	{
		// Token: 0x0600379B RID: 14235 RVA: 0x000B6065 File Offset: 0x000B4265
		public EdmSchemaAttribute()
		{
		}

		// Token: 0x0600379C RID: 14236 RVA: 0x000B606D File Offset: 0x000B426D
		public EdmSchemaAttribute(string assemblyGuid)
		{
			Check.NotNull<string>(assemblyGuid, "assemblyGuid");
		}
	}
}

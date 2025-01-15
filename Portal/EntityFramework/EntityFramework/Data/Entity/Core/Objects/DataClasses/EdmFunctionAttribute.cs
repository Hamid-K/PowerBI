using System;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x0200046E RID: 1134
	[Obsolete("This attribute has been replaced by System.Data.Entity.DbFunctionAttribute.")]
	[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
	public sealed class EdmFunctionAttribute : DbFunctionAttribute
	{
		// Token: 0x06003785 RID: 14213 RVA: 0x000B5EFC File Offset: 0x000B40FC
		public EdmFunctionAttribute(string namespaceName, string functionName)
			: base(namespaceName, functionName)
		{
		}
	}
}

using System;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000424 RID: 1060
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	[Serializable]
	internal sealed class IgnoreValidationAttribute : Attribute
	{
		// Token: 0x060020AD RID: 8365 RVA: 0x0007AD48 File Offset: 0x00078F48
		public static bool IsDefined(Type type)
		{
			return Attribute.IsDefined(type, typeof(IgnoreValidationAttribute));
		}
	}
}

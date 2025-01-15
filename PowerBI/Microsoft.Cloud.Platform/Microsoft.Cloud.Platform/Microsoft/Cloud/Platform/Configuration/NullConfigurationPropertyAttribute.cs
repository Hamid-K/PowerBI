using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000425 RID: 1061
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	[Serializable]
	public sealed class NullConfigurationPropertyAttribute : Attribute
	{
		// Token: 0x060020AF RID: 8367 RVA: 0x0007AD5A File Offset: 0x00078F5A
		public static bool IsDefined(PropertyInfo property)
		{
			return Attribute.IsDefined(property, typeof(NullConfigurationPropertyAttribute));
		}
	}
}

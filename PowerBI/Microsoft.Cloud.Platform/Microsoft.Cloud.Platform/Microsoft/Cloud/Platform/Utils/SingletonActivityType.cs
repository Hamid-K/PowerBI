using System;
using System.Reflection;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x02000166 RID: 358
	public static class SingletonActivityType
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x000202E4 File Offset: 0x0001E4E4
		public static ActivityType GetActivityTypeFor(Type singletonActivityType)
		{
			ExtendedDiagnostics.EnsureArgumentNotNull<Type>(singletonActivityType, "singletonActivityType");
			Type baseType = singletonActivityType.BaseType;
			ExtendedDiagnostics.EnsureNotNull<Type>(baseType, "baseType");
			PropertyInfo property = baseType.GetProperty("Instance");
			ExtendedDiagnostics.EnsureNotNull<PropertyInfo>(property, "propertyInfo");
			ActivityType activityType = property.GetValue(null) as ActivityType;
			ExtendedDiagnostics.EnsureNotNull<ActivityType>(activityType, "activityTypeInstance");
			return activityType;
		}
	}
}

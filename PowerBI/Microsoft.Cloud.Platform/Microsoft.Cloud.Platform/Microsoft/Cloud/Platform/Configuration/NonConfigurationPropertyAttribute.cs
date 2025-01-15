using System;
using System.Reflection;
using System.Xml.Serialization;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000422 RID: 1058
	[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
	public sealed class NonConfigurationPropertyAttribute : XmlIgnoreAttribute
	{
		// Token: 0x060020A4 RID: 8356 RVA: 0x0007ACDE File Offset: 0x00078EDE
		public static bool IsDefined(PropertyInfo property)
		{
			return Attribute.IsDefined(property, typeof(NonConfigurationPropertyAttribute));
		}
	}
}

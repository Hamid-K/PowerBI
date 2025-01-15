using System;
using System.Reflection;
using Microsoft.ReportingServices.Interfaces;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200004A RID: 74
	internal sealed class ExtensionLocalizedNameAttribute : LocalizedNameAttribute
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00008C46 File Offset: 0x00006E46
		public ExtensionLocalizedNameAttribute(Type stringResourceType, string name)
			: base(name)
		{
			this.m_stringResourceType = stringResourceType;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00008C58 File Offset: 0x00006E58
		protected override string GetLocalizedString(string name)
		{
			PropertyInfo property = this.m_stringResourceType.GetProperty(name, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (property != null)
			{
				return (string)property.GetValue(null, null);
			}
			return null;
		}

		// Token: 0x0400010A RID: 266
		private Type m_stringResourceType;
	}
}

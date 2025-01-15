using System;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C1 RID: 449
	internal sealed class DefaultValueConstantAttribute : DefaultValueAttribute
	{
		// Token: 0x06000EA2 RID: 3746 RVA: 0x00023DE3 File Offset: 0x00021FE3
		public DefaultValueConstantAttribute(string field)
			: base(DefaultValueConstantAttribute.GetConstant(field))
		{
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x00023DF1 File Offset: 0x00021FF1
		internal static object GetConstant(string field)
		{
			return typeof(Constants).InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
		}
	}
}

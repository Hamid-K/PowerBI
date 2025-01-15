using System;
using System.ComponentModel;
using System.Globalization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C4 RID: 452
	public class ReportExpressionDefaultValueAttribute : DefaultValueAttribute
	{
		// Token: 0x06000EAF RID: 3759 RVA: 0x00023F30 File Offset: 0x00022130
		public ReportExpressionDefaultValueAttribute()
			: base(default(ReportExpression))
		{
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00023F51 File Offset: 0x00022151
		public ReportExpressionDefaultValueAttribute(string value)
			: base(new ReportExpression(value))
		{
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00023F64 File Offset: 0x00022164
		public ReportExpressionDefaultValueAttribute(Type type)
			: base(Activator.CreateInstance(ReportExpressionDefaultValueAttribute.ConstructGenericType(type)))
		{
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00023F77 File Offset: 0x00022177
		public ReportExpressionDefaultValueAttribute(Type type, object value)
			: base(ReportExpressionDefaultValueAttribute.CreateInstance(type, value))
		{
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x00023F86 File Offset: 0x00022186
		internal static Type ConstructGenericType(Type type)
		{
			return typeof(ReportExpression<>).MakeGenericType(new Type[] { type });
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x00023FA4 File Offset: 0x000221A4
		internal static object CreateInstance(Type type, object value)
		{
			type = ReportExpressionDefaultValueAttribute.ConstructGenericType(type);
			if (value is string)
			{
				return type.GetConstructor(new Type[]
				{
					typeof(string),
					typeof(IFormatProvider)
				}).Invoke(new object[]
				{
					value,
					CultureInfo.InvariantCulture
				});
			}
			return Activator.CreateInstance(type, new object[] { value });
		}
	}
}

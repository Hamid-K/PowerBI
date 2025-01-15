using System;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C2 RID: 450
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ValidValuesAttribute : Attribute
	{
		// Token: 0x1700052B RID: 1323
		// (get) Token: 0x06000EA4 RID: 3748 RVA: 0x00023E10 File Offset: 0x00022010
		// (set) Token: 0x06000EA5 RID: 3749 RVA: 0x00023E18 File Offset: 0x00022018
		public object Minimum
		{
			get
			{
				return this.m_minimum;
			}
			set
			{
				this.m_minimum = value;
			}
		}

		// Token: 0x1700052C RID: 1324
		// (get) Token: 0x06000EA6 RID: 3750 RVA: 0x00023E21 File Offset: 0x00022021
		// (set) Token: 0x06000EA7 RID: 3751 RVA: 0x00023E29 File Offset: 0x00022029
		public object Maximum
		{
			get
			{
				return this.m_maximum;
			}
			set
			{
				this.m_maximum = value;
			}
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x00023E32 File Offset: 0x00022032
		public ValidValuesAttribute(int minimum, int maximum)
		{
			this.m_minimum = minimum;
			this.m_maximum = maximum;
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x00023E52 File Offset: 0x00022052
		public ValidValuesAttribute(double minimum, double maximum)
		{
			this.m_minimum = minimum;
			this.m_maximum = maximum;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x00023E74 File Offset: 0x00022074
		public ValidValuesAttribute(string minimumField, string maximumField)
		{
			this.m_minimum = typeof(Constants).InvokeMember(minimumField, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
			this.m_maximum = typeof(Constants).InvokeMember(maximumField, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
		}

		// Token: 0x0400054B RID: 1355
		private object m_minimum;

		// Token: 0x0400054C RID: 1356
		private object m_maximum;
	}
}

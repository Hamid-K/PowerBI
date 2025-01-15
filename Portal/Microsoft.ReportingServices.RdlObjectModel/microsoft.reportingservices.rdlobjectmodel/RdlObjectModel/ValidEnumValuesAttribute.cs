using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001C3 RID: 451
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class ValidEnumValuesAttribute : Attribute
	{
		// Token: 0x1700052D RID: 1325
		// (get) Token: 0x06000EAB RID: 3755 RVA: 0x00023ECD File Offset: 0x000220CD
		// (set) Token: 0x06000EAC RID: 3756 RVA: 0x00023ED5 File Offset: 0x000220D5
		public IList<int> ValidValues
		{
			get
			{
				return this.m_validValues;
			}
			set
			{
				this.m_validValues = value;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x00023EDE File Offset: 0x000220DE
		public ValidEnumValuesAttribute(string field)
			: this(typeof(InternalConstants), field)
		{
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00023EF4 File Offset: 0x000220F4
		public ValidEnumValuesAttribute(Type type, string field)
		{
			int[] array = (int[])type.InvokeMember(field, BindingFlags.GetField, null, null, null, CultureInfo.InvariantCulture);
			this.m_validValues = new ReadOnlyCollection<int>(array);
		}

		// Token: 0x0400054D RID: 1357
		private IList<int> m_validValues;
	}
}

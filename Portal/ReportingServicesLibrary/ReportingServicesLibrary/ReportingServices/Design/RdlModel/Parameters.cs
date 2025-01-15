using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F7 RID: 1015
	[Editor("Microsoft.ReportingServices.Design.Design.ParametersDialog", typeof(UITypeEditor))]
	public sealed class Parameters : ArrayList
	{
		// Token: 0x17000917 RID: 2327
		public Parameter this[int index]
		{
			get
			{
				return (Parameter)base[index];
			}
			set
			{
				base[index] = value;
			}
		}
	}
}

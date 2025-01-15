using System;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Drda.As
{
	// Token: 0x02000552 RID: 1362
	public class SqlSet : ConfigurationElement
	{
		// Token: 0x17000995 RID: 2453
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x0009AFB7 File Offset: 0x000991B7
		// (set) Token: 0x06002E12 RID: 11794 RVA: 0x0009AFC9 File Offset: 0x000991C9
		[Description("TBD")]
		[Category("General")]
		[ConfigurationProperty("arithAbort", IsRequired = false)]
		public OnOff ArithAbort
		{
			get
			{
				return (OnOff)base["arithAbort"];
			}
			set
			{
				base["arithAbort"] = value;
			}
		}

		// Token: 0x06002E13 RID: 11795 RVA: 0x0009AFDC File Offset: 0x000991DC
		public object GetElementKey()
		{
			return this.ArithAbort;
		}
	}
}

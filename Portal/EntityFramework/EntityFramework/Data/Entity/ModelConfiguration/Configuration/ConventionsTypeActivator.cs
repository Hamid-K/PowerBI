using System;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001C6 RID: 454
	internal class ConventionsTypeActivator
	{
		// Token: 0x0600180D RID: 6157 RVA: 0x00041753 File Offset: 0x0003F953
		public virtual IConvention Activate(Type conventionType)
		{
			return (IConvention)Activator.CreateInstance(conventionType, true);
		}
	}
}

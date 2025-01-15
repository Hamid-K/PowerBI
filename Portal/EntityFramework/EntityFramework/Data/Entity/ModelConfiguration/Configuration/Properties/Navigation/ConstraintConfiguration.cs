using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x02000208 RID: 520
	internal abstract class ConstraintConfiguration
	{
		// Token: 0x06001B8E RID: 7054
		internal abstract ConstraintConfiguration Clone();

		// Token: 0x06001B8F RID: 7055
		internal abstract void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration);

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x06001B90 RID: 7056 RVA: 0x0004C05D File Offset: 0x0004A25D
		public virtual bool IsFullySpecified
		{
			get
			{
				return true;
			}
		}
	}
}

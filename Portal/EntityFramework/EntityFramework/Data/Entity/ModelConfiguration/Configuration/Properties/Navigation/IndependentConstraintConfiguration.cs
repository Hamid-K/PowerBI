using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Types;
using System.Data.Entity.ModelConfiguration.Edm;

namespace System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation
{
	// Token: 0x0200020B RID: 523
	internal class IndependentConstraintConfiguration : ConstraintConfiguration
	{
		// Token: 0x06001BAE RID: 7086 RVA: 0x0004C92C File Offset: 0x0004AB2C
		private IndependentConstraintConfiguration()
		{
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x06001BAF RID: 7087 RVA: 0x0004C934 File Offset: 0x0004AB34
		public static ConstraintConfiguration Instance
		{
			get
			{
				return IndependentConstraintConfiguration._instance;
			}
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x0004C93B File Offset: 0x0004AB3B
		internal override ConstraintConfiguration Clone()
		{
			return IndependentConstraintConfiguration._instance;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x0004C942 File Offset: 0x0004AB42
		internal override void Configure(AssociationType associationType, AssociationEndMember dependentEnd, EntityTypeConfiguration entityTypeConfiguration)
		{
			associationType.MarkIndependent();
		}

		// Token: 0x04000ACD RID: 2765
		private static readonly ConstraintConfiguration _instance = new IndependentConstraintConfiguration();
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x0200011A RID: 282
	public class EnumTypeConfiguration<TEnumType>
	{
		// Token: 0x060009BA RID: 2490 RVA: 0x00028618 File Offset: 0x00026818
		internal EnumTypeConfiguration(EnumTypeConfiguration configuration)
		{
			this._configuration = configuration;
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00028627 File Offset: 0x00026827
		public IEnumerable<EnumMemberConfiguration> Members
		{
			get
			{
				return this._configuration.Members;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x060009BC RID: 2492 RVA: 0x00028634 File Offset: 0x00026834
		public string FullName
		{
			get
			{
				return this._configuration.FullName;
			}
		}

		// Token: 0x170002E7 RID: 743
		// (get) Token: 0x060009BD RID: 2493 RVA: 0x00028641 File Offset: 0x00026841
		// (set) Token: 0x060009BE RID: 2494 RVA: 0x0002864E File Offset: 0x0002684E
		public string Namespace
		{
			get
			{
				return this._configuration.Namespace;
			}
			set
			{
				this._configuration.Namespace = value;
			}
		}

		// Token: 0x170002E8 RID: 744
		// (get) Token: 0x060009BF RID: 2495 RVA: 0x0002865C File Offset: 0x0002685C
		// (set) Token: 0x060009C0 RID: 2496 RVA: 0x00028669 File Offset: 0x00026869
		public string Name
		{
			get
			{
				return this._configuration.Name;
			}
			set
			{
				this._configuration.Name = value;
			}
		}

		// Token: 0x060009C1 RID: 2497 RVA: 0x00028677 File Offset: 0x00026877
		public virtual void RemoveMember(TEnumType member)
		{
			this._configuration.RemoveMember((Enum)((object)member));
		}

		// Token: 0x060009C2 RID: 2498 RVA: 0x0002868F File Offset: 0x0002688F
		public EnumMemberConfiguration Member(TEnumType enumMember)
		{
			return this._configuration.AddMember((Enum)((object)enumMember));
		}

		// Token: 0x0400031B RID: 795
		private EnumTypeConfiguration _configuration;
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DDF RID: 7647
	public class FirewallGroupInfo2
	{
		// Token: 0x0600BD7D RID: 48509 RVA: 0x00266A9E File Offset: 0x00264C9E
		public FirewallGroupInfo2(bool trusted, params FirewallGroupType2[] groupTypes)
		{
			this.groupTypes = groupTypes;
			this.isTrusted = trusted;
		}

		// Token: 0x17002E9C RID: 11932
		// (get) Token: 0x0600BD7E RID: 48510 RVA: 0x00266AB4 File Offset: 0x00264CB4
		public bool IsFixed
		{
			get
			{
				return this.groupTypes.Length == 1;
			}
		}

		// Token: 0x17002E9D RID: 11933
		// (get) Token: 0x0600BD7F RID: 48511 RVA: 0x00266AC1 File Offset: 0x00264CC1
		public bool IsTrusted
		{
			get
			{
				return this.isTrusted;
			}
		}

		// Token: 0x17002E9E RID: 11934
		// (get) Token: 0x0600BD80 RID: 48512 RVA: 0x00266AC9 File Offset: 0x00264CC9
		public FirewallGroupType2 DefaultGroupType
		{
			get
			{
				return this.groupTypes[0];
			}
		}

		// Token: 0x17002E9F RID: 11935
		// (get) Token: 0x0600BD81 RID: 48513 RVA: 0x00266AD3 File Offset: 0x00264CD3
		public IEnumerable<FirewallGroupType2> GroupTypes
		{
			get
			{
				return this.groupTypes;
			}
		}

		// Token: 0x0400609E RID: 24734
		private readonly bool isTrusted;

		// Token: 0x0400609F RID: 24735
		private readonly FirewallGroupType2[] groupTypes;
	}
}

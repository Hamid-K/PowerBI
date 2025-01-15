using System;
using Microsoft.AspNet.OData.Common;

namespace Microsoft.AspNet.OData.Builder
{
	// Token: 0x02000117 RID: 279
	public class EnumMemberConfiguration
	{
		// Token: 0x0600098F RID: 2447 RVA: 0x00028204 File Offset: 0x00026404
		public EnumMemberConfiguration(Enum member, EnumTypeConfiguration declaringType)
		{
			if (member == null)
			{
				throw Error.ArgumentNull("member");
			}
			if (declaringType == null)
			{
				throw Error.ArgumentNull("declaringType");
			}
			this.MemberInfo = member;
			this.DeclaringType = declaringType;
			this.AddedExplicitly = true;
			this._name = Enum.GetName(member.GetType(), member);
		}

		// Token: 0x170002D1 RID: 721
		// (get) Token: 0x06000990 RID: 2448 RVA: 0x0002825A File Offset: 0x0002645A
		// (set) Token: 0x06000991 RID: 2449 RVA: 0x00028262 File Offset: 0x00026462
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				if (value == null)
				{
					throw Error.PropertyNull();
				}
				this._name = value;
			}
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000992 RID: 2450 RVA: 0x00028274 File Offset: 0x00026474
		// (set) Token: 0x06000993 RID: 2451 RVA: 0x0002827C File Offset: 0x0002647C
		public EnumTypeConfiguration DeclaringType { get; private set; }

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000994 RID: 2452 RVA: 0x00028285 File Offset: 0x00026485
		// (set) Token: 0x06000995 RID: 2453 RVA: 0x0002828D File Offset: 0x0002648D
		public Enum MemberInfo { get; private set; }

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000996 RID: 2454 RVA: 0x00028296 File Offset: 0x00026496
		// (set) Token: 0x06000997 RID: 2455 RVA: 0x0002829E File Offset: 0x0002649E
		public bool AddedExplicitly { get; set; }

		// Token: 0x0400030C RID: 780
		private string _name;
	}
}

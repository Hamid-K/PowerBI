using System;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x02000433 RID: 1075
	internal struct StateManagerValue
	{
		// Token: 0x0600343C RID: 13372 RVA: 0x000A88F0 File Offset: 0x000A6AF0
		public StateManagerValue(StateManagerMemberMetadata metadata, object instance, object value)
		{
			this.MemberMetadata = metadata;
			this.UserObject = instance;
			this.OriginalValue = value;
		}

		// Token: 0x040010DE RID: 4318
		public StateManagerMemberMetadata MemberMetadata;

		// Token: 0x040010DF RID: 4319
		public object UserObject;

		// Token: 0x040010E0 RID: 4320
		public object OriginalValue;
	}
}

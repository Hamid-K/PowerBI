using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A0F RID: 2575
	internal struct DrdaSchemaRestriction
	{
		// Token: 0x06005121 RID: 20769 RVA: 0x00144BA6 File Offset: 0x00142DA6
		public DrdaSchemaRestriction(string name, Type restrictiontype, object defaultNull, int maxLength)
		{
			this.Name = name;
			this.RestrictionType = restrictiontype;
			this.DefaultNull = defaultNull;
			this.UsesMaxLength = true;
			this.MaxLength = maxLength;
		}

		// Token: 0x06005122 RID: 20770 RVA: 0x00144BCC File Offset: 0x00142DCC
		public DrdaSchemaRestriction(string name, Type restrictiontype, object defaultNull)
		{
			this.Name = name;
			this.RestrictionType = restrictiontype;
			this.DefaultNull = defaultNull;
			this.UsesMaxLength = false;
			this.MaxLength = 0;
		}

		// Token: 0x04003FA7 RID: 16295
		public string Name;

		// Token: 0x04003FA8 RID: 16296
		public Type RestrictionType;

		// Token: 0x04003FA9 RID: 16297
		public bool UsesMaxLength;

		// Token: 0x04003FAA RID: 16298
		public int MaxLength;

		// Token: 0x04003FAB RID: 16299
		public object DefaultNull;
	}
}

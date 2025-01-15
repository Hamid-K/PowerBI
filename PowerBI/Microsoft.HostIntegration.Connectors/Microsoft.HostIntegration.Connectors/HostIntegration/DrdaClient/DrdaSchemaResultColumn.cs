using System;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A30 RID: 2608
	internal class DrdaSchemaResultColumn
	{
		// Token: 0x060051A1 RID: 20897 RVA: 0x0014D572 File Offset: 0x0014B772
		public DrdaSchemaResultColumn(string name, Type type, object defaultNull, int maxLength)
		{
			this.Name = name;
			this.Type = type;
			this.UsesMaxLength = true;
			this.MaxLength = maxLength;
		}

		// Token: 0x060051A2 RID: 20898 RVA: 0x0014D597 File Offset: 0x0014B797
		public DrdaSchemaResultColumn(string name, Type type, object defaultNull)
		{
			this.Name = name;
			this.Type = type;
			this.UsesMaxLength = false;
			this.MaxLength = 0;
		}

		// Token: 0x04004040 RID: 16448
		public string Name;

		// Token: 0x04004041 RID: 16449
		public Type Type;

		// Token: 0x04004042 RID: 16450
		public bool UsesMaxLength;

		// Token: 0x04004043 RID: 16451
		public int MaxLength;
	}
}

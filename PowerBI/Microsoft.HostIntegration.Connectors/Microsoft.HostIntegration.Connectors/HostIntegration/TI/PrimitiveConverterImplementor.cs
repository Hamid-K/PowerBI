using System;

namespace Microsoft.HostIntegration.TI
{
	// Token: 0x02000731 RID: 1841
	public class PrimitiveConverterImplementor
	{
		// Token: 0x060039AC RID: 14764 RVA: 0x00002061 File Offset: 0x00000261
		public PrimitiveConverterImplementor()
		{
		}

		// Token: 0x060039AD RID: 14765 RVA: 0x000C6348 File Offset: 0x000C4548
		public PrimitiveConverterImplementor(string PrimitiveConverterClassId, string SystemPlatformName)
		{
			this.systemPlatformName = SystemPlatformName;
			this.primitiveConverterClassId = PrimitiveConverterClassId;
			this.primitiveConverterFullName = RemoteEnvironmentClassFactory.GetPrimitiveConverterFullNameFromClassId(PrimitiveConverterClassId);
		}

		// Token: 0x060039AE RID: 14766 RVA: 0x000C636A File Offset: 0x000C456A
		public PrimitiveConverterImplementor(string PrimitiveConverterClassId, string SystemPlatformName, string PrimitiveConverterFullName)
		{
			this.systemPlatformName = SystemPlatformName;
			this.primitiveConverterClassId = PrimitiveConverterClassId;
			this.primitiveConverterFullName = PrimitiveConverterFullName;
		}

		// Token: 0x17000CDC RID: 3292
		// (get) Token: 0x060039AF RID: 14767 RVA: 0x000C6387 File Offset: 0x000C4587
		// (set) Token: 0x060039B0 RID: 14768 RVA: 0x000C638F File Offset: 0x000C458F
		public string SystemPlatformName
		{
			get
			{
				return this.systemPlatformName;
			}
			set
			{
				this.systemPlatformName = value;
			}
		}

		// Token: 0x17000CDD RID: 3293
		// (get) Token: 0x060039B1 RID: 14769 RVA: 0x000C6398 File Offset: 0x000C4598
		// (set) Token: 0x060039B2 RID: 14770 RVA: 0x000C63A0 File Offset: 0x000C45A0
		public string PrimitiveConverterClassId
		{
			get
			{
				return this.primitiveConverterClassId;
			}
			set
			{
				this.primitiveConverterClassId = value;
			}
		}

		// Token: 0x17000CDE RID: 3294
		// (get) Token: 0x060039B3 RID: 14771 RVA: 0x000C63A9 File Offset: 0x000C45A9
		// (set) Token: 0x060039B4 RID: 14772 RVA: 0x000C63B1 File Offset: 0x000C45B1
		public string PrimitiveConverterFullName
		{
			get
			{
				return this.primitiveConverterFullName;
			}
			set
			{
				this.primitiveConverterFullName = value;
			}
		}

		// Token: 0x040022F7 RID: 8951
		private string systemPlatformName;

		// Token: 0x040022F8 RID: 8952
		private string primitiveConverterClassId;

		// Token: 0x040022F9 RID: 8953
		private string primitiveConverterFullName;
	}
}

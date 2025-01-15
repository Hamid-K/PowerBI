using System;

namespace System.Data.Entity
{
	// Token: 0x02000061 RID: 97
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class DbModelBuilderVersionAttribute : Attribute
	{
		// Token: 0x0600032C RID: 812 RVA: 0x0000BA46 File Offset: 0x00009C46
		public DbModelBuilderVersionAttribute(DbModelBuilderVersion version)
		{
			if (!Enum.IsDefined(typeof(DbModelBuilderVersion), version))
			{
				throw new ArgumentOutOfRangeException("version");
			}
			this.Version = version;
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0000BA77 File Offset: 0x00009C77
		// (set) Token: 0x0600032E RID: 814 RVA: 0x0000BA7F File Offset: 0x00009C7F
		public DbModelBuilderVersion Version { get; private set; }
	}
}

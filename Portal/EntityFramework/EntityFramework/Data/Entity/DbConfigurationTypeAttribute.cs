using System;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200005A RID: 90
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public class DbConfigurationTypeAttribute : Attribute
	{
		// Token: 0x06000278 RID: 632 RVA: 0x0000A0AA File Offset: 0x000082AA
		public DbConfigurationTypeAttribute(Type configurationType)
		{
			Check.NotNull<Type>(configurationType, "configurationType");
			this._configurationType = configurationType;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000A0C8 File Offset: 0x000082C8
		public DbConfigurationTypeAttribute(string configurationTypeName)
		{
			Check.NotEmpty(configurationTypeName, "configurationTypeName");
			try
			{
				this._configurationType = Type.GetType(configurationTypeName, true);
			}
			catch (Exception ex)
			{
				throw new InvalidOperationException(Strings.DbConfigurationTypeInAttributeNotFound(configurationTypeName), ex);
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600027A RID: 634 RVA: 0x0000A114 File Offset: 0x00008314
		public Type ConfigurationType
		{
			get
			{
				return this._configurationType;
			}
		}

		// Token: 0x040000AF RID: 175
		private readonly Type _configurationType;
	}
}

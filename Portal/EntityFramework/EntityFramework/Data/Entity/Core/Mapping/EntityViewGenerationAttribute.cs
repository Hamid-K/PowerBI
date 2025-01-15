using System;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000530 RID: 1328
	[Obsolete("The mechanism to provide pre-generated views has changed. Implement a class that derives from System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCache and has a parameterless constructor, then associate it with a type that derives from DbContext or ObjectContext by using System.Data.Entity.Infrastructure.MappingViews.DbMappingViewCacheTypeAttribute.", true)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class EntityViewGenerationAttribute : Attribute
	{
		// Token: 0x06004189 RID: 16777 RVA: 0x000DD56F File Offset: 0x000DB76F
		public EntityViewGenerationAttribute(Type viewGenerationType)
		{
			Check.NotNull<Type>(viewGenerationType, "viewGenerationType");
			this.m_viewGenType = viewGenerationType;
		}

		// Token: 0x17000CF4 RID: 3316
		// (get) Token: 0x0600418A RID: 16778 RVA: 0x000DD58A File Offset: 0x000DB78A
		public Type ViewGenerationType
		{
			get
			{
				return this.m_viewGenType;
			}
		}

		// Token: 0x040016BA RID: 5818
		private readonly Type m_viewGenType;
	}
}

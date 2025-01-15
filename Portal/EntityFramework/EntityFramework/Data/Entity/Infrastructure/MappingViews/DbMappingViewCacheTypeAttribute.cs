using System;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Infrastructure.MappingViews
{
	// Token: 0x02000271 RID: 625
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	public sealed class DbMappingViewCacheTypeAttribute : Attribute
	{
		// Token: 0x06001F83 RID: 8067 RVA: 0x00059E68 File Offset: 0x00058068
		public DbMappingViewCacheTypeAttribute(Type contextType, Type cacheType)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotNull<Type>(cacheType, "cacheType");
			if (!contextType.IsSubclassOf(typeof(ObjectContext)) && !contextType.IsSubclassOf(typeof(DbContext)))
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_InvalidContextType(contextType), "contextType");
			}
			if (!cacheType.IsSubclassOf(typeof(DbMappingViewCache)))
			{
				throw new ArgumentException(Strings.Generated_View_Type_Super_Class(cacheType), "cacheType");
			}
			this._contextType = contextType;
			this._cacheType = cacheType;
		}

		// Token: 0x06001F84 RID: 8068 RVA: 0x00059EFC File Offset: 0x000580FC
		public DbMappingViewCacheTypeAttribute(Type contextType, string cacheTypeName)
		{
			Check.NotNull<Type>(contextType, "contextType");
			Check.NotEmpty(cacheTypeName, "cacheTypeName");
			if (!contextType.IsSubclassOf(typeof(ObjectContext)) && !contextType.IsSubclassOf(typeof(DbContext)))
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_InvalidContextType(contextType), "contextType");
			}
			this._contextType = contextType;
			try
			{
				this._cacheType = Type.GetType(cacheTypeName, true);
			}
			catch (Exception ex)
			{
				throw new ArgumentException(Strings.DbMappingViewCacheTypeAttribute_CacheTypeNotFound(cacheTypeName), "cacheTypeName", ex);
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x06001F85 RID: 8069 RVA: 0x00059F98 File Offset: 0x00058198
		internal Type ContextType
		{
			get
			{
				return this._contextType;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x06001F86 RID: 8070 RVA: 0x00059FA0 File Offset: 0x000581A0
		internal Type CacheType
		{
			get
			{
				return this._cacheType;
			}
		}

		// Token: 0x04000B68 RID: 2920
		private readonly Type _contextType;

		// Token: 0x04000B69 RID: 2921
		private readonly Type _cacheType;
	}
}

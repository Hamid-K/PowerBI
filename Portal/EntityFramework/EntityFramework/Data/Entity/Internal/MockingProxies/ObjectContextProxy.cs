using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Reflection;

namespace System.Data.Entity.Internal.MockingProxies
{
	// Token: 0x0200013B RID: 315
	internal class ObjectContextProxy : IDisposable
	{
		// Token: 0x060014DE RID: 5342 RVA: 0x00036C44 File Offset: 0x00034E44
		protected ObjectContextProxy()
		{
		}

		// Token: 0x060014DF RID: 5343 RVA: 0x00036C4C File Offset: 0x00034E4C
		public ObjectContextProxy(ObjectContext objectContext)
		{
			this._objectContext = objectContext;
		}

		// Token: 0x060014E0 RID: 5344 RVA: 0x00036C5B File Offset: 0x00034E5B
		public static implicit operator ObjectContext(ObjectContextProxy proxy)
		{
			if (proxy != null)
			{
				return proxy._objectContext;
			}
			return null;
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x060014E1 RID: 5345 RVA: 0x00036C68 File Offset: 0x00034E68
		public virtual EntityConnectionProxy Connection
		{
			get
			{
				return new EntityConnectionProxy((EntityConnection)this._objectContext.Connection);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x060014E2 RID: 5346 RVA: 0x00036C7F File Offset: 0x00034E7F
		// (set) Token: 0x060014E3 RID: 5347 RVA: 0x00036C8C File Offset: 0x00034E8C
		public virtual string DefaultContainerName
		{
			get
			{
				return this._objectContext.DefaultContainerName;
			}
			set
			{
				this._objectContext.DefaultContainerName = value;
			}
		}

		// Token: 0x060014E4 RID: 5348 RVA: 0x00036C9A File Offset: 0x00034E9A
		public virtual void Dispose()
		{
			this._objectContext.Dispose();
		}

		// Token: 0x060014E5 RID: 5349 RVA: 0x00036CA8 File Offset: 0x00034EA8
		public virtual IEnumerable<GlobalItem> GetObjectItemCollection()
		{
			return this._objectItemCollection = (ObjectItemCollection)this._objectContext.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
		}

		// Token: 0x060014E6 RID: 5350 RVA: 0x00036CD4 File Offset: 0x00034ED4
		public virtual Type GetClrType(StructuralType item)
		{
			return this._objectItemCollection.GetClrType(item);
		}

		// Token: 0x060014E7 RID: 5351 RVA: 0x00036CE2 File Offset: 0x00034EE2
		public virtual Type GetClrType(EnumType item)
		{
			return this._objectItemCollection.GetClrType(item);
		}

		// Token: 0x060014E8 RID: 5352 RVA: 0x00036CF0 File Offset: 0x00034EF0
		public virtual void LoadFromAssembly(Assembly assembly)
		{
			this._objectContext.MetadataWorkspace.LoadFromAssembly(assembly);
		}

		// Token: 0x060014E9 RID: 5353 RVA: 0x00036D03 File Offset: 0x00034F03
		public virtual ObjectContextProxy CreateNew(EntityConnectionProxy entityConnection)
		{
			return new ObjectContextProxy(new ObjectContext(entityConnection));
		}

		// Token: 0x060014EA RID: 5354 RVA: 0x00036D18 File Offset: 0x00034F18
		public virtual void CopyContextOptions(ObjectContextProxy source)
		{
			this._objectContext.ContextOptions.LazyLoadingEnabled = source._objectContext.ContextOptions.LazyLoadingEnabled;
			this._objectContext.ContextOptions.ProxyCreationEnabled = source._objectContext.ContextOptions.ProxyCreationEnabled;
			this._objectContext.ContextOptions.UseCSharpNullComparisonBehavior = source._objectContext.ContextOptions.UseCSharpNullComparisonBehavior;
			this._objectContext.ContextOptions.UseConsistentNullReferenceBehavior = source._objectContext.ContextOptions.UseConsistentNullReferenceBehavior;
			this._objectContext.ContextOptions.UseLegacyPreserveChangesBehavior = source._objectContext.ContextOptions.UseLegacyPreserveChangesBehavior;
			this._objectContext.CommandTimeout = source._objectContext.CommandTimeout;
			this._objectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions = source._objectContext.ContextOptions.DisableFilterOverProjectionSimplificationForCustomFunctions;
			this._objectContext.InterceptionContext = source._objectContext.InterceptionContext.WithObjectContext(this._objectContext);
		}

		// Token: 0x040009C6 RID: 2502
		private readonly ObjectContext _objectContext;

		// Token: 0x040009C7 RID: 2503
		private ObjectItemCollection _objectItemCollection;
	}
}

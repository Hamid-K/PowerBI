using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Globalization;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200041D RID: 1053
	public class ObjectSet<TEntity> : ObjectQuery<TEntity>, IObjectSet<TEntity>, IQueryable<TEntity>, IEnumerable<TEntity>, IEnumerable, IQueryable where TEntity : class
	{
		// Token: 0x0600328F RID: 12943 RVA: 0x000A1FA1 File Offset: 0x000A01A1
		internal ObjectSet(EntitySet entitySet, ObjectContext context)
			: base(entitySet, context, MergeOption.AppendOnly)
		{
			this._entitySet = entitySet;
		}

		// Token: 0x170009C0 RID: 2496
		// (get) Token: 0x06003290 RID: 12944 RVA: 0x000A1FB3 File Offset: 0x000A01B3
		public EntitySet EntitySet
		{
			get
			{
				return this._entitySet;
			}
		}

		// Token: 0x06003291 RID: 12945 RVA: 0x000A1FBB File Offset: 0x000A01BB
		public void AddObject(TEntity entity)
		{
			base.Context.AddObject(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x06003292 RID: 12946 RVA: 0x000A1FD4 File Offset: 0x000A01D4
		public void Attach(TEntity entity)
		{
			base.Context.AttachTo(this.FullyQualifiedEntitySetName, entity);
		}

		// Token: 0x06003293 RID: 12947 RVA: 0x000A1FED File Offset: 0x000A01ED
		public void DeleteObject(TEntity entity)
		{
			base.Context.DeleteObject(entity, this.EntitySet);
		}

		// Token: 0x06003294 RID: 12948 RVA: 0x000A2006 File Offset: 0x000A0206
		public void Detach(TEntity entity)
		{
			base.Context.Detach(entity, this.EntitySet);
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x000A201F File Offset: 0x000A021F
		public TEntity ApplyCurrentValues(TEntity currentEntity)
		{
			return base.Context.ApplyCurrentValues<TEntity>(this.FullyQualifiedEntitySetName, currentEntity);
		}

		// Token: 0x06003296 RID: 12950 RVA: 0x000A2033 File Offset: 0x000A0233
		public TEntity ApplyOriginalValues(TEntity originalEntity)
		{
			return base.Context.ApplyOriginalValues<TEntity>(this.FullyQualifiedEntitySetName, originalEntity);
		}

		// Token: 0x06003297 RID: 12951 RVA: 0x000A2047 File Offset: 0x000A0247
		public TEntity CreateObject()
		{
			return base.Context.CreateObject<TEntity>();
		}

		// Token: 0x06003298 RID: 12952 RVA: 0x000A2054 File Offset: 0x000A0254
		public T CreateObject<T>() where T : class, TEntity
		{
			return base.Context.CreateObject<T>();
		}

		// Token: 0x170009C1 RID: 2497
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000A2061 File Offset: 0x000A0261
		private string FullyQualifiedEntitySetName
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}.{1}", new object[]
				{
					this._entitySet.EntityContainer.Name,
					this._entitySet.Name
				});
			}
		}

		// Token: 0x04001085 RID: 4229
		private readonly EntitySet _entitySet;
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000104 RID: 260
	internal class InternalCollectionEntry : InternalNavigationEntry
	{
		// Token: 0x0600129B RID: 4763 RVA: 0x00030A31 File Offset: 0x0002EC31
		public InternalCollectionEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata)
			: base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x0600129C RID: 4764 RVA: 0x00030A3B File Offset: 0x0002EC3B
		protected override object GetNavigationPropertyFromRelatedEnd(object entity)
		{
			return base.RelatedEnd;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x0600129D RID: 4765 RVA: 0x00030A43 File Offset: 0x0002EC43
		// (set) Token: 0x0600129E RID: 4766 RVA: 0x00030A4C File Offset: 0x0002EC4C
		public override object CurrentValue
		{
			get
			{
				return base.CurrentValue;
			}
			set
			{
				if (base.Setter != null)
				{
					base.Setter(this.InternalEntityEntry.Entity, value);
					return;
				}
				if (this.InternalEntityEntry.IsDetached || base.RelatedEnd != value)
				{
					throw Error.DbCollectionEntry_CannotSetCollectionProp(this.Name, this.InternalEntityEntry.Entity.GetType().ToString());
				}
			}
		}

		// Token: 0x0600129F RID: 4767 RVA: 0x00030AB0 File Offset: 0x0002ECB0
		public override DbMemberEntry CreateDbMemberEntry()
		{
			return new DbCollectionEntry(this);
		}

		// Token: 0x060012A0 RID: 4768 RVA: 0x00030AB8 File Offset: 0x0002ECB8
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			return this.CreateDbCollectionEntry<TEntity, TProperty>(this.EntryMetadata.ElementType);
		}

		// Token: 0x060012A1 RID: 4769 RVA: 0x00030ACB File Offset: 0x0002ECCB
		public virtual DbCollectionEntry<TEntity, TElement> CreateDbCollectionEntry<TEntity, TElement>() where TEntity : class
		{
			return new DbCollectionEntry<TEntity, TElement>(this);
		}

		// Token: 0x060012A2 RID: 4770 RVA: 0x00030AD4 File Offset: 0x0002ECD4
		private DbMemberEntry<TEntity, TProperty> CreateDbCollectionEntry<TEntity, TProperty>(Type elementType) where TEntity : class
		{
			Type typeFromHandle = typeof(DbMemberEntry<TEntity, TProperty>);
			Func<InternalCollectionEntry, object> func;
			if (!InternalCollectionEntry._entryFactories.TryGetValue(typeFromHandle, out func))
			{
				Type type = typeof(DbCollectionEntry<, >).MakeGenericType(new Type[]
				{
					typeof(TEntity),
					elementType
				});
				if (!typeFromHandle.IsAssignableFrom(type))
				{
					throw Error.DbEntityEntry_WrongGenericForCollectionNavProp(typeof(TProperty), this.Name, this.EntryMetadata.DeclaringType, typeof(ICollection<>).MakeGenericType(new Type[] { elementType }));
				}
				MethodInfo declaredMethod = type.GetDeclaredMethod("Create", new Type[] { typeof(InternalCollectionEntry) });
				func = (Func<InternalCollectionEntry, object>)Delegate.CreateDelegate(typeof(Func<InternalCollectionEntry, object>), declaredMethod);
				InternalCollectionEntry._entryFactories.TryAdd(typeFromHandle, func);
			}
			return (DbMemberEntry<TEntity, TProperty>)func(this);
		}

		// Token: 0x0400092B RID: 2347
		private static readonly ConcurrentDictionary<Type, Func<InternalCollectionEntry, object>> _entryFactories = new ConcurrentDictionary<Type, Func<InternalCollectionEntry, object>>();
	}
}

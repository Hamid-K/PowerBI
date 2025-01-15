using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200010C RID: 268
	internal class InternalReferenceEntry : InternalNavigationEntry
	{
		// Token: 0x0600131B RID: 4891 RVA: 0x000323C4 File Offset: 0x000305C4
		public InternalReferenceEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata)
			: base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000323D0 File Offset: 0x000305D0
		protected override object GetNavigationPropertyFromRelatedEnd(object entity)
		{
			IEnumerator enumerator = base.RelatedEnd.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return null;
			}
			return enumerator.Current;
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000323FC File Offset: 0x000305FC
		protected virtual void SetNavigationPropertyOnRelatedEnd(object value)
		{
			Type type = base.RelatedEnd.GetType();
			Action<IRelatedEnd, object> action;
			if (!InternalReferenceEntry._entityReferenceValueSetters.TryGetValue(type, out action))
			{
				MethodInfo methodInfo = InternalReferenceEntry.SetValueOnEntityReferenceMethod.MakeGenericMethod(new Type[] { type.GetGenericArguments().Single<Type>() });
				action = (Action<IRelatedEnd, object>)Delegate.CreateDelegate(typeof(Action<IRelatedEnd, object>), methodInfo);
				InternalReferenceEntry._entityReferenceValueSetters.TryAdd(type, action);
			}
			action(base.RelatedEnd, value);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x00032473 File Offset: 0x00030673
		private static void SetValueOnEntityReference<TRelatedEntity>(IRelatedEnd entityReference, object value) where TRelatedEntity : class
		{
			((EntityReference<TRelatedEntity>)entityReference).Value = (TRelatedEntity)((object)value);
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x0600131F RID: 4895 RVA: 0x00032486 File Offset: 0x00030686
		// (set) Token: 0x06001320 RID: 4896 RVA: 0x00032490 File Offset: 0x00030690
		public override object CurrentValue
		{
			get
			{
				return base.CurrentValue;
			}
			set
			{
				if (base.RelatedEnd != null && this.InternalEntityEntry.State != EntityState.Deleted)
				{
					this.SetNavigationPropertyOnRelatedEnd(value);
					return;
				}
				if (base.Setter != null)
				{
					base.Setter(this.InternalEntityEntry.Entity, value);
					return;
				}
				throw Error.DbPropertyEntry_SettingEntityRefNotSupported(this.Name, this.InternalEntityEntry.EntityType.Name, this.InternalEntityEntry.State);
			}
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x00032506 File Offset: 0x00030706
		public override DbMemberEntry CreateDbMemberEntry()
		{
			return new DbReferenceEntry(this);
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x0003250E File Offset: 0x0003070E
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			return new DbReferenceEntry<TEntity, TProperty>(this);
		}

		// Token: 0x04000941 RID: 2369
		private static readonly ConcurrentDictionary<Type, Action<IRelatedEnd, object>> _entityReferenceValueSetters = new ConcurrentDictionary<Type, Action<IRelatedEnd, object>>();

		// Token: 0x04000942 RID: 2370
		public static readonly MethodInfo SetValueOnEntityReferenceMethod = typeof(InternalReferenceEntry).GetOnlyDeclaredMethod("SetValueOnEntityReference");
	}
}

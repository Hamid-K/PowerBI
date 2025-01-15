using System;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Resources;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000108 RID: 264
	internal abstract class InternalNavigationEntry : InternalMemberEntry
	{
		// Token: 0x060012DB RID: 4827 RVA: 0x0003184D File Offset: 0x0002FA4D
		protected InternalNavigationEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata)
			: base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x00031857 File Offset: 0x0002FA57
		public virtual void Load()
		{
			this.ValidateNotDetached("Load");
			this._relatedEnd.Load();
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x0003186F File Offset: 0x0002FA6F
		public virtual Task LoadAsync(CancellationToken cancellationToken)
		{
			this.ValidateNotDetached("LoadAsync");
			return this._relatedEnd.LoadAsync(cancellationToken);
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x060012DE RID: 4830 RVA: 0x00031888 File Offset: 0x0002FA88
		// (set) Token: 0x060012DF RID: 4831 RVA: 0x000318A0 File Offset: 0x0002FAA0
		public virtual bool IsLoaded
		{
			get
			{
				this.ValidateNotDetached("IsLoaded");
				return this._relatedEnd.IsLoaded;
			}
			set
			{
				this.ValidateNotDetached("IsLoaded");
				this._relatedEnd.IsLoaded = value;
			}
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000318B9 File Offset: 0x0002FAB9
		public virtual IQueryable Query()
		{
			this.ValidateNotDetached("Query");
			return (IQueryable)this._relatedEnd.CreateSourceQuery();
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x060012E1 RID: 4833 RVA: 0x000318D6 File Offset: 0x0002FAD6
		protected IRelatedEnd RelatedEnd
		{
			get
			{
				if (this._relatedEnd == null && !this.InternalEntityEntry.IsDetached)
				{
					this._relatedEnd = this.InternalEntityEntry.GetRelatedEnd(this.Name);
				}
				return this._relatedEnd;
			}
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x060012E2 RID: 4834 RVA: 0x0003190A File Offset: 0x0002FB0A
		public override object CurrentValue
		{
			get
			{
				if (this.Getter == null)
				{
					this.ValidateNotDetached("CurrentValue");
					return this.GetNavigationPropertyFromRelatedEnd(this.InternalEntityEntry.Entity);
				}
				return this.Getter(this.InternalEntityEntry.Entity);
			}
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x060012E3 RID: 4835 RVA: 0x00031947 File Offset: 0x0002FB47
		protected Func<object, object> Getter
		{
			get
			{
				if (!this._triedToGetGetter)
				{
					DbHelpers.GetPropertyGetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out this._getter);
					this._triedToGetGetter = true;
				}
				return this._getter;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x060012E4 RID: 4836 RVA: 0x00031980 File Offset: 0x0002FB80
		protected Action<object, object> Setter
		{
			get
			{
				if (!this._triedToGetSetter)
				{
					DbHelpers.GetPropertySetters(this.InternalEntityEntry.EntityType).TryGetValue(this.Name, out this._setter);
					this._triedToGetSetter = true;
				}
				return this._setter;
			}
		}

		// Token: 0x060012E5 RID: 4837
		protected abstract object GetNavigationPropertyFromRelatedEnd(object entity);

		// Token: 0x060012E6 RID: 4838 RVA: 0x000319BC File Offset: 0x0002FBBC
		private void ValidateNotDetached(string method)
		{
			if (this._relatedEnd == null)
			{
				if (this.InternalEntityEntry.IsDetached)
				{
					throw Error.DbPropertyEntry_NotSupportedForDetached(method, this.Name, this.InternalEntityEntry.EntityType.Name);
				}
				this._relatedEnd = this.InternalEntityEntry.GetRelatedEnd(this.Name);
			}
		}

		// Token: 0x04000933 RID: 2355
		private IRelatedEnd _relatedEnd;

		// Token: 0x04000934 RID: 2356
		private Func<object, object> _getter;

		// Token: 0x04000935 RID: 2357
		private bool _triedToGetGetter;

		// Token: 0x04000936 RID: 2358
		private Action<object, object> _setter;

		// Token: 0x04000937 RID: 2359
		private bool _triedToGetSetter;
	}
}

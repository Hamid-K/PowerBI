using System;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004E5 RID: 1253
	internal class NavigationPropertyAccessor
	{
		// Token: 0x06003E6D RID: 15981 RVA: 0x000D0096 File Offset: 0x000CE296
		public NavigationPropertyAccessor(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06003E6E RID: 15982 RVA: 0x000D00A5 File Offset: 0x000CE2A5
		public bool HasProperty
		{
			get
			{
				return this._propertyName != null;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06003E6F RID: 15983 RVA: 0x000D00B0 File Offset: 0x000CE2B0
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06003E70 RID: 15984 RVA: 0x000D00B8 File Offset: 0x000CE2B8
		// (set) Token: 0x06003E71 RID: 15985 RVA: 0x000D00C0 File Offset: 0x000CE2C0
		public Func<object, object> ValueGetter
		{
			get
			{
				return this._memberGetter;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object>>(ref this._memberGetter, value, null);
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06003E72 RID: 15986 RVA: 0x000D00D0 File Offset: 0x000CE2D0
		// (set) Token: 0x06003E73 RID: 15987 RVA: 0x000D00D8 File Offset: 0x000CE2D8
		public Action<object, object> ValueSetter
		{
			get
			{
				return this._memberSetter;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._memberSetter, value, null);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06003E74 RID: 15988 RVA: 0x000D00E8 File Offset: 0x000CE2E8
		// (set) Token: 0x06003E75 RID: 15989 RVA: 0x000D00F0 File Offset: 0x000CE2F0
		public Action<object, object> CollectionAdd
		{
			get
			{
				return this._collectionAdd;
			}
			set
			{
				Interlocked.CompareExchange<Action<object, object>>(ref this._collectionAdd, value, null);
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06003E76 RID: 15990 RVA: 0x000D0100 File Offset: 0x000CE300
		// (set) Token: 0x06003E77 RID: 15991 RVA: 0x000D0108 File Offset: 0x000CE308
		public Func<object, object, bool> CollectionRemove
		{
			get
			{
				return this._collectionRemove;
			}
			set
			{
				Interlocked.CompareExchange<Func<object, object, bool>>(ref this._collectionRemove, value, null);
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06003E78 RID: 15992 RVA: 0x000D0118 File Offset: 0x000CE318
		// (set) Token: 0x06003E79 RID: 15993 RVA: 0x000D0120 File Offset: 0x000CE320
		public Func<object> CollectionCreate
		{
			get
			{
				return this._collectionCreate;
			}
			set
			{
				Interlocked.CompareExchange<Func<object>>(ref this._collectionCreate, value, null);
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06003E7A RID: 15994 RVA: 0x000D0130 File Offset: 0x000CE330
		public static NavigationPropertyAccessor NoNavigationProperty
		{
			get
			{
				return new NavigationPropertyAccessor(null);
			}
		}

		// Token: 0x0400152B RID: 5419
		private Func<object, object> _memberGetter;

		// Token: 0x0400152C RID: 5420
		private Action<object, object> _memberSetter;

		// Token: 0x0400152D RID: 5421
		private Action<object, object> _collectionAdd;

		// Token: 0x0400152E RID: 5422
		private Func<object, object, bool> _collectionRemove;

		// Token: 0x0400152F RID: 5423
		private Func<object> _collectionCreate;

		// Token: 0x04001530 RID: 5424
		private readonly string _propertyName;
	}
}

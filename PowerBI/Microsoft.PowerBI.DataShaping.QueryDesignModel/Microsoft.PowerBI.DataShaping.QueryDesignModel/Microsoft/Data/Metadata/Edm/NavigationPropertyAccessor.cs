using System;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000A1 RID: 161
	internal class NavigationPropertyAccessor
	{
		// Token: 0x06000B21 RID: 2849 RVA: 0x0001B475 File Offset: 0x00019675
		public NavigationPropertyAccessor(string propertyName)
		{
			this._propertyName = propertyName;
		}

		// Token: 0x170003FA RID: 1018
		// (get) Token: 0x06000B22 RID: 2850 RVA: 0x0001B484 File Offset: 0x00019684
		public bool HasProperty
		{
			get
			{
				return this._propertyName != null;
			}
		}

		// Token: 0x170003FB RID: 1019
		// (get) Token: 0x06000B23 RID: 2851 RVA: 0x0001B48F File Offset: 0x0001968F
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x170003FC RID: 1020
		// (get) Token: 0x06000B24 RID: 2852 RVA: 0x0001B497 File Offset: 0x00019697
		// (set) Token: 0x06000B25 RID: 2853 RVA: 0x0001B49F File Offset: 0x0001969F
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

		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000B26 RID: 2854 RVA: 0x0001B4AF File Offset: 0x000196AF
		// (set) Token: 0x06000B27 RID: 2855 RVA: 0x0001B4B7 File Offset: 0x000196B7
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

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000B28 RID: 2856 RVA: 0x0001B4C7 File Offset: 0x000196C7
		// (set) Token: 0x06000B29 RID: 2857 RVA: 0x0001B4CF File Offset: 0x000196CF
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

		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000B2A RID: 2858 RVA: 0x0001B4DF File Offset: 0x000196DF
		// (set) Token: 0x06000B2B RID: 2859 RVA: 0x0001B4E7 File Offset: 0x000196E7
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

		// Token: 0x17000400 RID: 1024
		// (get) Token: 0x06000B2C RID: 2860 RVA: 0x0001B4F7 File Offset: 0x000196F7
		// (set) Token: 0x06000B2D RID: 2861 RVA: 0x0001B4FF File Offset: 0x000196FF
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

		// Token: 0x17000401 RID: 1025
		// (get) Token: 0x06000B2E RID: 2862 RVA: 0x0001B50F File Offset: 0x0001970F
		public static NavigationPropertyAccessor NoNavigationProperty
		{
			get
			{
				return new NavigationPropertyAccessor(null);
			}
		}

		// Token: 0x04000877 RID: 2167
		private Func<object, object> _memberGetter;

		// Token: 0x04000878 RID: 2168
		private Action<object, object> _memberSetter;

		// Token: 0x04000879 RID: 2169
		private Action<object, object> _collectionAdd;

		// Token: 0x0400087A RID: 2170
		private Func<object, object, bool> _collectionRemove;

		// Token: 0x0400087B RID: 2171
		private Func<object> _collectionCreate;

		// Token: 0x0400087C RID: 2172
		private readonly string _propertyName;
	}
}

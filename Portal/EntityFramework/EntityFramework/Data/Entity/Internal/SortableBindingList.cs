using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Entity.Utilities;
using System.Reflection;
using System.Xml.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200012C RID: 300
	internal class SortableBindingList<T> : BindingList<T>
	{
		// Token: 0x0600149D RID: 5277 RVA: 0x00035E90 File Offset: 0x00034090
		public SortableBindingList(List<T> list)
			: base(list)
		{
		}

		// Token: 0x0600149E RID: 5278 RVA: 0x00035E9C File Offset: 0x0003409C
		protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
		{
			if (SortableBindingList<T>.PropertyComparer.CanSort(prop.PropertyType))
			{
				((List<T>)base.Items).Sort(new SortableBindingList<T>.PropertyComparer(prop, direction));
				this._sortDirection = direction;
				this._sortProperty = prop;
				this._isSorted = true;
				this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
			}
		}

		// Token: 0x0600149F RID: 5279 RVA: 0x00035EEF File Offset: 0x000340EF
		protected override void RemoveSortCore()
		{
			this._isSorted = false;
			this._sortProperty = null;
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x060014A0 RID: 5280 RVA: 0x00035EFF File Offset: 0x000340FF
		protected override bool IsSortedCore
		{
			get
			{
				return this._isSorted;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x060014A1 RID: 5281 RVA: 0x00035F07 File Offset: 0x00034107
		protected override ListSortDirection SortDirectionCore
		{
			get
			{
				return this._sortDirection;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x060014A2 RID: 5282 RVA: 0x00035F0F File Offset: 0x0003410F
		protected override PropertyDescriptor SortPropertyCore
		{
			get
			{
				return this._sortProperty;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x060014A3 RID: 5283 RVA: 0x00035F17 File Offset: 0x00034117
		protected override bool SupportsSortingCore
		{
			get
			{
				return true;
			}
		}

		// Token: 0x040009B2 RID: 2482
		private bool _isSorted;

		// Token: 0x040009B3 RID: 2483
		private ListSortDirection _sortDirection;

		// Token: 0x040009B4 RID: 2484
		private PropertyDescriptor _sortProperty;

		// Token: 0x0200080D RID: 2061
		internal class PropertyComparer : Comparer<T>
		{
			// Token: 0x06005973 RID: 22899 RVA: 0x0013A844 File Offset: 0x00138A44
			public PropertyComparer(PropertyDescriptor prop, ListSortDirection direction)
			{
				if (!prop.ComponentType.IsAssignableFrom(typeof(T)))
				{
					throw new MissingMemberException(typeof(T).Name, prop.Name);
				}
				this._prop = prop;
				this._direction = direction;
				if (SortableBindingList<T>.PropertyComparer.CanSortWithIComparable(prop.PropertyType))
				{
					PropertyInfo declaredProperty = typeof(Comparer<>).MakeGenericType(new Type[] { prop.PropertyType }).GetDeclaredProperty("Default");
					this._comparer = (IComparer)declaredProperty.GetValue(null, null);
					this._useToString = false;
					return;
				}
				this._comparer = StringComparer.CurrentCultureIgnoreCase;
				this._useToString = true;
			}

			// Token: 0x06005974 RID: 22900 RVA: 0x0013A8FC File Offset: 0x00138AFC
			public override int Compare(T left, T right)
			{
				object obj = this._prop.GetValue(left);
				object obj2 = this._prop.GetValue(right);
				if (this._useToString)
				{
					obj = ((obj != null) ? obj.ToString() : null);
					obj2 = ((obj2 != null) ? obj2.ToString() : null);
				}
				if (this._direction != ListSortDirection.Ascending)
				{
					return this._comparer.Compare(obj2, obj);
				}
				return this._comparer.Compare(obj, obj2);
			}

			// Token: 0x06005975 RID: 22901 RVA: 0x0013A972 File Offset: 0x00138B72
			public static bool CanSort(Type type)
			{
				return SortableBindingList<T>.PropertyComparer.CanSortWithToString(type) || SortableBindingList<T>.PropertyComparer.CanSortWithIComparable(type);
			}

			// Token: 0x06005976 RID: 22902 RVA: 0x0013A984 File Offset: 0x00138B84
			private static bool CanSortWithIComparable(Type type)
			{
				return type.GetInterface("IComparable") != null || (type.IsGenericType() && type.GetGenericTypeDefinition() == typeof(Nullable<>));
			}

			// Token: 0x06005977 RID: 22903 RVA: 0x0013A9BA File Offset: 0x00138BBA
			private static bool CanSortWithToString(Type type)
			{
				return type.Equals(typeof(XNode)) || type.IsSubclassOf(typeof(XNode));
			}

			// Token: 0x0400221B RID: 8731
			private readonly IComparer _comparer;

			// Token: 0x0400221C RID: 8732
			private readonly ListSortDirection _direction;

			// Token: 0x0400221D RID: 8733
			private readonly PropertyDescriptor _prop;

			// Token: 0x0400221E RID: 8734
			private readonly bool _useToString;
		}
	}
}

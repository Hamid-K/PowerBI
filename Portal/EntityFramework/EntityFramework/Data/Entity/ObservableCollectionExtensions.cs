using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;

namespace System.Data.Entity
{
	// Token: 0x0200006B RID: 107
	public static class ObservableCollectionExtensions
	{
		// Token: 0x0600036F RID: 879 RVA: 0x0000C138 File Offset: 0x0000A338
		public static BindingList<T> ToBindingList<T>(this ObservableCollection<T> source) where T : class
		{
			Check.NotNull<ObservableCollection<T>>(source, "source");
			DbLocalView<T> dbLocalView = source as DbLocalView<T>;
			if (dbLocalView == null)
			{
				return new ObservableBackedBindingList<T>(source);
			}
			return dbLocalView.BindingList;
		}
	}
}

using System;
using System.ComponentModel;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200040E RID: 1038
	internal interface IObjectView
	{
		// Token: 0x0600311D RID: 12573
		void EntityPropertyChanged(object sender, PropertyChangedEventArgs e);

		// Token: 0x0600311E RID: 12574
		void CollectionChanged(object sender, CollectionChangeEventArgs e);
	}
}

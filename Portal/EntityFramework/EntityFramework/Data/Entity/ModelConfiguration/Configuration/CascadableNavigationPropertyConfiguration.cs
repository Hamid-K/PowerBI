using System;
using System.ComponentModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration.Configuration.Properties.Navigation;

namespace System.Data.Entity.ModelConfiguration.Configuration
{
	// Token: 0x020001EB RID: 491
	public abstract class CascadableNavigationPropertyConfiguration
	{
		// Token: 0x060019D6 RID: 6614 RVA: 0x00045E48 File Offset: 0x00044048
		internal CascadableNavigationPropertyConfiguration(NavigationPropertyConfiguration navigationPropertyConfiguration)
		{
			this._navigationPropertyConfiguration = navigationPropertyConfiguration;
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x00045E57 File Offset: 0x00044057
		public void WillCascadeOnDelete()
		{
			this.WillCascadeOnDelete(true);
		}

		// Token: 0x060019D8 RID: 6616 RVA: 0x00045E60 File Offset: 0x00044060
		public void WillCascadeOnDelete(bool value)
		{
			this._navigationPropertyConfiguration.DeleteAction = new OperationAction?(value ? OperationAction.Cascade : OperationAction.None);
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x00045E79 File Offset: 0x00044079
		internal NavigationPropertyConfiguration NavigationPropertyConfiguration
		{
			get
			{
				return this._navigationPropertyConfiguration;
			}
		}

		// Token: 0x060019DA RID: 6618 RVA: 0x00045E81 File Offset: 0x00044081
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060019DB RID: 6619 RVA: 0x00045E89 File Offset: 0x00044089
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00045E92 File Offset: 0x00044092
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00045E9A File Offset: 0x0004409A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000A86 RID: 2694
		private readonly NavigationPropertyConfiguration _navigationPropertyConfiguration;
	}
}

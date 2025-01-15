using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000063 RID: 99
	public sealed class AdomdProperty : IXmlaProperty, IXmlaPropertyKey
	{
		// Token: 0x06000691 RID: 1681 RVA: 0x00022AE7 File Offset: 0x00020CE7
		public AdomdProperty(string name, object propertyValue)
			: this(name, null, propertyValue)
		{
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00022AF2 File Offset: 0x00020CF2
		public AdomdProperty(string name, string propertyNamespace, object propertyValue)
		{
			this.name = name;
			this.propertyValue = propertyValue;
			this.propertyNamespace = propertyNamespace;
		}

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000693 RID: 1683 RVA: 0x00022B0F File Offset: 0x00020D0F
		public object Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170001AB RID: 427
		// (get) Token: 0x06000694 RID: 1684 RVA: 0x00022B17 File Offset: 0x00020D17
		// (set) Token: 0x06000695 RID: 1685 RVA: 0x00022B1F File Offset: 0x00020D1F
		object IXmlaProperty.Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x170001AC RID: 428
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x00022B28 File Offset: 0x00020D28
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x00022B30 File Offset: 0x00020D30
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
				{
					if (this.Parent == null)
					{
						this.name = value;
						return;
					}
					((AdomdPropertyCollection)this.Parent).InternalCollection.ChangeName(this, value);
				}
			}
		}

		// Token: 0x170001AD RID: 429
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x00022B67 File Offset: 0x00020D67
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x00022B6F File Offset: 0x00020D6F
		string IXmlaPropertyKey.Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x00022B78 File Offset: 0x00020D78
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x00022B80 File Offset: 0x00020D80
		public object Value
		{
			get
			{
				return this.propertyValue;
			}
			set
			{
				if (this.propertyValue != value)
				{
					if (this.Parent != null && ((AdomdPropertyCollection)this.Parent).InternalCollection.IsReadOnly)
					{
						throw new InvalidOperationException(SR.Collection_IsReadOnly);
					}
					this.propertyValue = value;
				}
			}
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x00022BBC File Offset: 0x00020DBC
		// (set) Token: 0x0600069D RID: 1693 RVA: 0x00022BC4 File Offset: 0x00020DC4
		public string Namespace
		{
			get
			{
				return this.propertyNamespace;
			}
			set
			{
				if (this.propertyNamespace != value)
				{
					if (this.Parent == null)
					{
						this.propertyNamespace = value;
						return;
					}
					((AdomdPropertyCollection)this.Parent).InternalCollection.ChangeNamespace(this, value);
				}
			}
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x0600069E RID: 1694 RVA: 0x00022BFB File Offset: 0x00020DFB
		// (set) Token: 0x0600069F RID: 1695 RVA: 0x00022C03 File Offset: 0x00020E03
		string IXmlaPropertyKey.Namespace
		{
			get
			{
				return this.propertyNamespace;
			}
			set
			{
				this.propertyNamespace = value;
			}
		}

		// Token: 0x04000457 RID: 1111
		private object parent;

		// Token: 0x04000458 RID: 1112
		private string name;

		// Token: 0x04000459 RID: 1113
		private object propertyValue;

		// Token: 0x0400045A RID: 1114
		private string propertyNamespace;
	}
}

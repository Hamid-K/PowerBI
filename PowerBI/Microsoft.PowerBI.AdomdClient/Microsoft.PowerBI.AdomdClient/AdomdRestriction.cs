using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000066 RID: 102
	public sealed class AdomdRestriction : IXmlaProperty, IXmlaPropertyKey
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00022B17 File Offset: 0x00020D17
		public AdomdRestriction(string name, object restrictionValue)
			: this(name, null, restrictionValue)
		{
		}

		// Token: 0x060006B9 RID: 1721 RVA: 0x00022B22 File Offset: 0x00020D22
		public AdomdRestriction(string name, string restrictionNamespace, object restrictionValue)
		{
			this.name = name;
			this.restrictionValue = restrictionValue;
			this.restrictionNamespace = restrictionNamespace;
		}

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060006BA RID: 1722 RVA: 0x00022B3F File Offset: 0x00020D3F
		public object Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x060006BB RID: 1723 RVA: 0x00022B47 File Offset: 0x00020D47
		// (set) Token: 0x060006BC RID: 1724 RVA: 0x00022B4F File Offset: 0x00020D4F
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

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x060006BD RID: 1725 RVA: 0x00022B58 File Offset: 0x00020D58
		// (set) Token: 0x060006BE RID: 1726 RVA: 0x00022B60 File Offset: 0x00020D60
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
					((AdomdRestrictionCollection)this.Parent).InternalCollection.ChangeName(this, value);
				}
			}
		}

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x060006BF RID: 1727 RVA: 0x00022B97 File Offset: 0x00020D97
		// (set) Token: 0x060006C0 RID: 1728 RVA: 0x00022B9F File Offset: 0x00020D9F
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

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060006C1 RID: 1729 RVA: 0x00022BA8 File Offset: 0x00020DA8
		// (set) Token: 0x060006C2 RID: 1730 RVA: 0x00022BB0 File Offset: 0x00020DB0
		public object Value
		{
			get
			{
				return this.restrictionValue;
			}
			set
			{
				this.restrictionValue = value;
			}
		}

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060006C3 RID: 1731 RVA: 0x00022BB9 File Offset: 0x00020DB9
		// (set) Token: 0x060006C4 RID: 1732 RVA: 0x00022BC1 File Offset: 0x00020DC1
		public string Namespace
		{
			get
			{
				return this.restrictionNamespace;
			}
			set
			{
				if (this.restrictionNamespace != value)
				{
					if (this.Parent == null)
					{
						this.restrictionNamespace = value;
						return;
					}
					((AdomdRestrictionCollection)this.Parent).InternalCollection.ChangeNamespace(this, value);
				}
			}
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006C5 RID: 1733 RVA: 0x00022BF8 File Offset: 0x00020DF8
		// (set) Token: 0x060006C6 RID: 1734 RVA: 0x00022C00 File Offset: 0x00020E00
		string IXmlaPropertyKey.Namespace
		{
			get
			{
				return this.restrictionNamespace;
			}
			set
			{
				this.restrictionNamespace = value;
			}
		}

		// Token: 0x04000450 RID: 1104
		private object parent;

		// Token: 0x04000451 RID: 1105
		private string name;

		// Token: 0x04000452 RID: 1106
		private object restrictionValue;

		// Token: 0x04000453 RID: 1107
		private string restrictionNamespace;
	}
}

using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000066 RID: 102
	public sealed class AdomdRestriction : IXmlaProperty, IXmlaPropertyKey
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00022E47 File Offset: 0x00021047
		public AdomdRestriction(string name, object restrictionValue)
			: this(name, null, restrictionValue)
		{
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x00022E52 File Offset: 0x00021052
		public AdomdRestriction(string name, string restrictionNamespace, object restrictionValue)
		{
			this.name = name;
			this.restrictionValue = restrictionValue;
			this.restrictionNamespace = restrictionNamespace;
		}

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00022E6F File Offset: 0x0002106F
		public object Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x00022E77 File Offset: 0x00021077
		// (set) Token: 0x060006C9 RID: 1737 RVA: 0x00022E7F File Offset: 0x0002107F
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

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060006CA RID: 1738 RVA: 0x00022E88 File Offset: 0x00021088
		// (set) Token: 0x060006CB RID: 1739 RVA: 0x00022E90 File Offset: 0x00021090
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

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x00022EC7 File Offset: 0x000210C7
		// (set) Token: 0x060006CD RID: 1741 RVA: 0x00022ECF File Offset: 0x000210CF
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

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060006CE RID: 1742 RVA: 0x00022ED8 File Offset: 0x000210D8
		// (set) Token: 0x060006CF RID: 1743 RVA: 0x00022EE0 File Offset: 0x000210E0
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

		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x00022EE9 File Offset: 0x000210E9
		// (set) Token: 0x060006D1 RID: 1745 RVA: 0x00022EF1 File Offset: 0x000210F1
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

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060006D2 RID: 1746 RVA: 0x00022F28 File Offset: 0x00021128
		// (set) Token: 0x060006D3 RID: 1747 RVA: 0x00022F30 File Offset: 0x00021130
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

		// Token: 0x0400045D RID: 1117
		private object parent;

		// Token: 0x0400045E RID: 1118
		private string name;

		// Token: 0x0400045F RID: 1119
		private object restrictionValue;

		// Token: 0x04000460 RID: 1120
		private string restrictionNamespace;
	}
}

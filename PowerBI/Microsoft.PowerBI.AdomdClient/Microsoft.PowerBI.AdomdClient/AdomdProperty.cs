using System;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000063 RID: 99
	public sealed class AdomdProperty : IXmlaProperty, IXmlaPropertyKey
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x000227B7 File Offset: 0x000209B7
		public AdomdProperty(string name, object propertyValue)
			: this(name, null, propertyValue)
		{
		}

		// Token: 0x06000685 RID: 1669 RVA: 0x000227C2 File Offset: 0x000209C2
		public AdomdProperty(string name, string propertyNamespace, object propertyValue)
		{
			this.name = name;
			this.propertyValue = propertyValue;
			this.propertyNamespace = propertyNamespace;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000686 RID: 1670 RVA: 0x000227DF File Offset: 0x000209DF
		public object Parent
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000687 RID: 1671 RVA: 0x000227E7 File Offset: 0x000209E7
		// (set) Token: 0x06000688 RID: 1672 RVA: 0x000227EF File Offset: 0x000209EF
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

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000689 RID: 1673 RVA: 0x000227F8 File Offset: 0x000209F8
		// (set) Token: 0x0600068A RID: 1674 RVA: 0x00022800 File Offset: 0x00020A00
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

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x0600068B RID: 1675 RVA: 0x00022837 File Offset: 0x00020A37
		// (set) Token: 0x0600068C RID: 1676 RVA: 0x0002283F File Offset: 0x00020A3F
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

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600068D RID: 1677 RVA: 0x00022848 File Offset: 0x00020A48
		// (set) Token: 0x0600068E RID: 1678 RVA: 0x00022850 File Offset: 0x00020A50
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

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0002288C File Offset: 0x00020A8C
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00022894 File Offset: 0x00020A94
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

		// Token: 0x170001AA RID: 426
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x000228CB File Offset: 0x00020ACB
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x000228D3 File Offset: 0x00020AD3
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

		// Token: 0x0400044A RID: 1098
		private object parent;

		// Token: 0x0400044B RID: 1099
		private string name;

		// Token: 0x0400044C RID: 1100
		private object propertyValue;

		// Token: 0x0400044D RID: 1101
		private string propertyNamespace;
	}
}

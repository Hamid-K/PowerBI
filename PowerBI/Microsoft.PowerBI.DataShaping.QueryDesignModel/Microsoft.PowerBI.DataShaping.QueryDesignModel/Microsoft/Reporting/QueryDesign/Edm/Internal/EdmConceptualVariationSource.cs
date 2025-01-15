using System;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001EC RID: 492
	internal sealed class EdmConceptualVariationSource : IConceptualVariationSource, IEquatable<IConceptualVariationSource>
	{
		// Token: 0x0600176B RID: 5995 RVA: 0x00040120 File Offset: 0x0003E320
		internal EdmConceptualVariationSource(string name, bool isDefault, IConceptualNavigationProperty navigationProperty, IConceptualHierarchy defaultHierarchy, IConceptualProperty defaultProperty)
		{
			this._name = name;
			this._isDefault = isDefault;
			this._navigationProperty = navigationProperty;
			this._defaultHierarchy = defaultHierarchy;
			this._defaultProperty = defaultProperty;
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x0600176C RID: 5996 RVA: 0x0004014D File Offset: 0x0003E34D
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x0600176D RID: 5997 RVA: 0x00040155 File Offset: 0x0003E355
		public bool IsDefault
		{
			get
			{
				return this._isDefault;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x0600176E RID: 5998 RVA: 0x0004015D File Offset: 0x0003E35D
		public IConceptualNavigationProperty NavigationProperty
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x0600176F RID: 5999 RVA: 0x00040165 File Offset: 0x0003E365
		public IConceptualHierarchy DefaultHierarchy
		{
			get
			{
				return this._defaultHierarchy;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001770 RID: 6000 RVA: 0x0004016D File Offset: 0x0003E36D
		public IConceptualProperty DefaultProperty
		{
			get
			{
				return this._defaultProperty;
			}
		}

		// Token: 0x06001771 RID: 6001 RVA: 0x00040175 File Offset: 0x0003E375
		public bool Equals(IConceptualVariationSource other)
		{
			return this == other;
		}

		// Token: 0x04000C73 RID: 3187
		private readonly string _name;

		// Token: 0x04000C74 RID: 3188
		private readonly bool _isDefault;

		// Token: 0x04000C75 RID: 3189
		private readonly IConceptualNavigationProperty _navigationProperty;

		// Token: 0x04000C76 RID: 3190
		private readonly IConceptualHierarchy _defaultHierarchy;

		// Token: 0x04000C77 RID: 3191
		private readonly IConceptualProperty _defaultProperty;
	}
}

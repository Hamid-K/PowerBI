using System;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E7 RID: 487
	internal sealed class EdmConceptualNavigationProperty : IConceptualNavigationProperty
	{
		// Token: 0x0600172C RID: 5932 RVA: 0x0003FA2F File Offset: 0x0003DC2F
		internal EdmConceptualNavigationProperty(EdmNavigationProperty navigationProperty, bool isActive, CrossFilterDirection crossFilterDirection, IConceptualEntity targetEntity, ConceptualNavigationBehavior behavior)
		{
			this._navigationProperty = navigationProperty;
			this.IsActive = isActive;
			this.CrossFilterDirection = crossFilterDirection;
			this.TargetEntity = targetEntity;
			this.Behavior = behavior;
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600172D RID: 5933 RVA: 0x0003FA5C File Offset: 0x0003DC5C
		public string Name
		{
			get
			{
				return this._navigationProperty.ReferenceName;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600172E RID: 5934 RVA: 0x0003FA69 File Offset: 0x0003DC69
		public string EdmName
		{
			get
			{
				return this._navigationProperty.Name;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600172F RID: 5935 RVA: 0x0003FA76 File Offset: 0x0003DC76
		public bool IsActive { get; }

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06001730 RID: 5936 RVA: 0x0003FA7E File Offset: 0x0003DC7E
		public CrossFilterDirection CrossFilterDirection { get; }

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x06001731 RID: 5937 RVA: 0x0003FA86 File Offset: 0x0003DC86
		public IConceptualColumn SourceColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001732 RID: 5938 RVA: 0x0003FA8D File Offset: 0x0003DC8D
		public IConceptualColumn TargetColumn
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x06001733 RID: 5939 RVA: 0x0003FA94 File Offset: 0x0003DC94
		public IConceptualEntity TargetEntity { get; }

		// Token: 0x1700063B RID: 1595
		// (get) Token: 0x06001734 RID: 5940 RVA: 0x0003FA9C File Offset: 0x0003DC9C
		public ConceptualMultiplicity SourceMultiplicity
		{
			get
			{
				return (ConceptualMultiplicity)this._navigationProperty.SourceMultiplicity;
			}
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x06001735 RID: 5941 RVA: 0x0003FAA9 File Offset: 0x0003DCA9
		public ConceptualMultiplicity TargetMultiplicity
		{
			get
			{
				return (ConceptualMultiplicity)this._navigationProperty.TargetMultipicity;
			}
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x06001736 RID: 5942 RVA: 0x0003FAB6 File Offset: 0x0003DCB6
		public ConceptualNavigationBehavior Behavior { get; }

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06001737 RID: 5943 RVA: 0x0003FABE File Offset: 0x0003DCBE
		internal string AssociationName
		{
			get
			{
				return this._navigationProperty.AssociationName;
			}
		}

		// Token: 0x04000C5D RID: 3165
		private readonly EdmNavigationProperty _navigationProperty;
	}
}

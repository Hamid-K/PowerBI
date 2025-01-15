using System;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001D6 RID: 470
	public sealed class AssociationSet : EntitySetBase
	{
		// Token: 0x060016AB RID: 5803 RVA: 0x0003E8D4 File Offset: 0x0003CAD4
		private AssociationSet(AssociationSet associationSet, AssociationType elementType)
			: base(associationSet)
		{
			this._elementType = ArgumentValidation.CheckNotNull<AssociationType>(elementType, "elementType");
			ArgumentValidation.CheckCondition(elementType.InternalAssociationType == associationSet.ElementType, "elementType");
			XElement xelementMetadataProperty = this.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:AssociationSet");
			this._state = xelementMetadataProperty.GetEnumAttributeOrDefault(Extensions.StateAttr, AssociationState.Active);
			this._behavior = xelementMetadataProperty.GetEnumAttributeOrDefault(Extensions.BehaviorAttr, AssociationBehavior.Default);
			this._crossFilterDirection = xelementMetadataProperty.GetEnumAttributeOrDefault(Extensions.CrossFilterDirectionAttr, CrossFilterDirection.Single);
		}

		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x060016AC RID: 5804 RVA: 0x0003E953 File Offset: 0x0003CB53
		public AssociationType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x060016AD RID: 5805 RVA: 0x0003E95B File Offset: 0x0003CB5B
		public AssociationSetEndCollection AssociationSetEnds
		{
			get
			{
				return this._assocSetEnds;
			}
		}

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x060016AE RID: 5806 RVA: 0x0003E963 File Offset: 0x0003CB63
		public AssociationState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x060016AF RID: 5807 RVA: 0x0003E96B File Offset: 0x0003CB6B
		public CrossFilterDirection CrossFilterDirection
		{
			get
			{
				return this._crossFilterDirection;
			}
		}

		// Token: 0x170005E9 RID: 1513
		// (get) Token: 0x060016B0 RID: 5808 RVA: 0x0003E973 File Offset: 0x0003CB73
		public AssociationBehavior Behavior
		{
			get
			{
				return this._behavior;
			}
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x060016B1 RID: 5809 RVA: 0x0003E97B File Offset: 0x0003CB7B
		internal AssociationSet InternalAssociationSet
		{
			get
			{
				return (AssociationSet)base.InternalEntitySetBase;
			}
		}

		// Token: 0x060016B2 RID: 5810 RVA: 0x0003E988 File Offset: 0x0003CB88
		private void InternalInit(EntitySetCollection entitySets)
		{
			AssociationSet internalAssociationSet = this.InternalAssociationSet;
			AssociationSetEnd[] array = new AssociationSetEnd[internalAssociationSet.AssociationSetEnds.Count];
			for (int i = 0; i < array.Length; i++)
			{
				AssociationSetEnd associationSetEnd = internalAssociationSet.AssociationSetEnds[i];
				EntitySet itemFromEdmSet = entitySets.GetItemFromEdmSet(associationSetEnd.EntitySet);
				AssociationEndMember associationEndMember = this._elementType.AssociationEndMembers[associationSetEnd.Name];
				array[i] = new AssociationSetEnd(associationSetEnd, itemFromEdmSet, this, associationEndMember);
			}
			this._assocSetEnds = new AssociationSetEndCollection(array);
		}

		// Token: 0x060016B3 RID: 5811 RVA: 0x0003EA08 File Offset: 0x0003CC08
		internal static AssociationSet Create(AssociationSet assocSet, AssociationType assocType, EntitySetCollection entitySets)
		{
			AssociationSet associationSet = new AssociationSet(assocSet, assocType);
			associationSet.InternalInit(entitySets);
			return associationSet;
		}

		// Token: 0x04000C21 RID: 3105
		private readonly AssociationType _elementType;

		// Token: 0x04000C22 RID: 3106
		private readonly AssociationState _state;

		// Token: 0x04000C23 RID: 3107
		private readonly CrossFilterDirection _crossFilterDirection;

		// Token: 0x04000C24 RID: 3108
		private readonly AssociationBehavior _behavior;

		// Token: 0x04000C25 RID: 3109
		private AssociationSetEndCollection _assocSetEnds;
	}
}

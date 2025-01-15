using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace DocumentFormat.OpenXml.Internal.SchemaValidation
{
	// Token: 0x02003119 RID: 12569
	[DebuggerDisplay("OpenXmlTypeId={OpenXmlTypeId}")]
	internal class SchemaTypeData
	{
		// Token: 0x0601B419 RID: 111641 RVA: 0x00373E14 File Offset: 0x00372014
		internal SchemaTypeData(int openxmlTypeId, AttributeConstraint[] attributeConstraints)
		{
			this.OpenXmlTypeId = openxmlTypeId;
			this.AttributeConstraints = attributeConstraints;
		}

		// Token: 0x0601B41A RID: 111642 RVA: 0x00373E2A File Offset: 0x0037202A
		internal SchemaTypeData(int openxmlTypeId, AttributeConstraint[] attributeConstraints, ParticleConstraint particleConstraint)
			: this(openxmlTypeId, attributeConstraints)
		{
			this.ParticleConstraint = particleConstraint;
		}

		// Token: 0x0601B41B RID: 111643 RVA: 0x00373E3B File Offset: 0x0037203B
		internal SchemaTypeData(int openxmlTypeId, AttributeConstraint[] attributeConstraints, SimpleTypeRestriction simpleTypeConstraint)
			: this(openxmlTypeId, attributeConstraints)
		{
			this.SimpleTypeConstraint = simpleTypeConstraint;
		}

		// Token: 0x170098DE RID: 39134
		// (get) Token: 0x0601B41C RID: 111644 RVA: 0x00373E4C File Offset: 0x0037204C
		// (set) Token: 0x0601B41D RID: 111645 RVA: 0x00373E54 File Offset: 0x00372054
		internal int OpenXmlTypeId { get; private set; }

		// Token: 0x170098DF RID: 39135
		// (get) Token: 0x0601B41E RID: 111646 RVA: 0x00373E5D File Offset: 0x0037205D
		// (set) Token: 0x0601B41F RID: 111647 RVA: 0x00373E65 File Offset: 0x00372065
		internal ParticleConstraint ParticleConstraint { get; private set; }

		// Token: 0x170098E0 RID: 39136
		// (get) Token: 0x0601B420 RID: 111648 RVA: 0x00373E6E File Offset: 0x0037206E
		// (set) Token: 0x0601B421 RID: 111649 RVA: 0x00373E76 File Offset: 0x00372076
		internal SimpleTypeRestriction SimpleTypeConstraint { get; private set; }

		// Token: 0x170098E1 RID: 39137
		// (get) Token: 0x0601B422 RID: 111650 RVA: 0x00373E7F File Offset: 0x0037207F
		// (set) Token: 0x0601B423 RID: 111651 RVA: 0x00373E87 File Offset: 0x00372087
		internal IList<AttributeConstraint> AttributeConstraints { get; private set; }

		// Token: 0x170098E2 RID: 39138
		// (get) Token: 0x0601B424 RID: 111652 RVA: 0x00373E90 File Offset: 0x00372090
		internal bool HasAttributeConstraints
		{
			get
			{
				return this.AttributeConstraints != null && this.AttributeConstraints.Count > 0;
			}
		}

		// Token: 0x170098E3 RID: 39139
		// (get) Token: 0x0601B425 RID: 111653 RVA: 0x00373EAA File Offset: 0x003720AA
		internal int AttributeConstraintsCount
		{
			get
			{
				if (this.AttributeConstraints != null)
				{
					return this.AttributeConstraints.Count;
				}
				return 0;
			}
		}

		// Token: 0x170098E4 RID: 39140
		// (get) Token: 0x0601B426 RID: 111654 RVA: 0x00373EC1 File Offset: 0x003720C1
		internal bool IsCompositeType
		{
			get
			{
				return this.ParticleConstraint != null;
			}
		}

		// Token: 0x170098E5 RID: 39141
		// (get) Token: 0x0601B427 RID: 111655 RVA: 0x00373ECF File Offset: 0x003720CF
		internal bool IsSimpleContent
		{
			get
			{
				return this.SimpleTypeConstraint != null;
			}
		}
	}
}

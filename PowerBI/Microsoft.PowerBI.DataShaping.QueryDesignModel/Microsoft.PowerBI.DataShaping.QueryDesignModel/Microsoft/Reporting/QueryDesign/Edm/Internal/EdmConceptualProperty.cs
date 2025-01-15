using System;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x020001E8 RID: 488
	internal abstract class EdmConceptualProperty : IConceptualProperty, IConceptualDisplayItem, IEquatable<IConceptualProperty>
	{
		// Token: 0x06001738 RID: 5944 RVA: 0x0003FACC File Offset: 0x0003DCCC
		protected EdmConceptualProperty(EdmProperty property)
		{
			this._property = property;
			this._conceptualDataType = property.ConceptualType.ConceptualDataType;
			this._type = DataTypeExtensions.GetTypeForPrimitive(this._conceptualDataType);
			EdmField edmField = property as EdmField;
			if (edmField != null)
			{
				this._conceptualDataCategory = EdmConceptualTypeConverter.ConvertFieldContentTypeToConceptualDataCategory(edmField.Contents);
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06001739 RID: 5945 RVA: 0x0003FB23 File Offset: 0x0003DD23
		public IConceptualEntity Entity
		{
			get
			{
				return this._entity;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x0600173A RID: 5946 RVA: 0x0003FB2B File Offset: 0x0003DD2B
		public DataType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0003FB33 File Offset: 0x0003DD33
		public PropertyDataCategory DataCategory
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x0600173C RID: 5948 RVA: 0x0003FB3A File Offset: 0x0003DD3A
		public string Description
		{
			get
			{
				return this._property.Description;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0003FB47 File Offset: 0x0003DD47
		public string EdmName
		{
			get
			{
				return this._property.Name;
			}
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x0600173E RID: 5950 RVA: 0x0003FB54 File Offset: 0x0003DD54
		public string Name
		{
			get
			{
				return this._property.ReferenceName;
			}
		}

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0003FB61 File Offset: 0x0003DD61
		public string DisplayName
		{
			get
			{
				return this._property.Caption;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001740 RID: 5952 RVA: 0x0003FB6E File Offset: 0x0003DD6E
		public bool IsHidden
		{
			get
			{
				return this._property.Hidden;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0003FB7B File Offset: 0x0003DD7B
		public bool IsPrivate
		{
			get
			{
				return this._property.IsPrivate;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001742 RID: 5954 RVA: 0x0003FB88 File Offset: 0x0003DD88
		public int Ordinal
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06001743 RID: 5955 RVA: 0x0003FB8F File Offset: 0x0003DD8F
		public string FormatString
		{
			get
			{
				return this._property.FormatString;
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001744 RID: 5956 RVA: 0x0003FB9C File Offset: 0x0003DD9C
		public ConceptualPrimitiveType ConceptualDataType
		{
			get
			{
				return this._conceptualDataType;
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06001745 RID: 5957 RVA: 0x0003FBA4 File Offset: 0x0003DDA4
		public ConceptualDataCategory ConceptualDataCategory
		{
			get
			{
				return this._conceptualDataCategory;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06001746 RID: 5958 RVA: 0x0003FBAC File Offset: 0x0003DDAC
		protected EdmProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x1700064D RID: 1613
		// (get) Token: 0x06001747 RID: 5959 RVA: 0x0003FBB4 File Offset: 0x0003DDB4
		public string StableName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700064E RID: 1614
		// (get) Token: 0x06001748 RID: 5960 RVA: 0x0003FBB7 File Offset: 0x0003DDB7
		public bool IsStable
		{
			get
			{
				return this._property.Stability == Stability.Stable;
			}
		}

		// Token: 0x06001749 RID: 5961 RVA: 0x0003FBC7 File Offset: 0x0003DDC7
		public string GetFullName()
		{
			return this.Entity.GetFullName() + "." + this.EdmName;
		}

		// Token: 0x0600174A RID: 5962 RVA: 0x0003FBE4 File Offset: 0x0003DDE4
		public bool Equals(IConceptualProperty other)
		{
			return this == other;
		}

		// Token: 0x0600174B RID: 5963 RVA: 0x0003FBEA File Offset: 0x0003DDEA
		internal virtual void CompleteInitialization(EdmConceptualPropertyInitContext context)
		{
			this._entity = context.Entity;
		}

		// Token: 0x0600174C RID: 5964 RVA: 0x0003FBF8 File Offset: 0x0003DDF8
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000C62 RID: 3170
		private readonly EdmProperty _property;

		// Token: 0x04000C63 RID: 3171
		private readonly DataType _type;

		// Token: 0x04000C64 RID: 3172
		private readonly ConceptualPrimitiveType _conceptualDataType;

		// Token: 0x04000C65 RID: 3173
		private readonly ConceptualDataCategory _conceptualDataCategory;

		// Token: 0x04000C66 RID: 3174
		private IConceptualEntity _entity;
	}
}

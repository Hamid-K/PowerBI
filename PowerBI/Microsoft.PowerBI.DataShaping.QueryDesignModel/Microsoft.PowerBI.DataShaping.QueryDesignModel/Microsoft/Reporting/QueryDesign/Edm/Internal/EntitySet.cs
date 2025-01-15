using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;
using Microsoft.Reporting.Common.Internal;
using Microsoft.Reporting.QueryDesign.Edm.ExtendedProperties.Internal;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200023C RID: 572
	public sealed class EntitySet : EntitySetBase, ISupportsReferenceName
	{
		// Token: 0x0600193E RID: 6462 RVA: 0x00044D98 File Offset: 0x00042F98
		internal EntitySet(EntitySet entitySet, EntityType elementType)
			: base(entitySet)
		{
			this._elementType = ArgumentValidation.CheckNotNull<EntityType>(elementType, "elementType");
			this._qname = QualifiedName.Root.Append(base.FullName);
			XElement xelementMetadataProperty = this.GetXElementMetadataProperty("http://schemas.microsoft.com/sqlbi/2010/10/edm/extensions:EntitySet");
			this._caption = xelementMetadataProperty.GetStringAttributeOrDefault(Extensions.CaptionAttr, null);
			this._referenceName = xelementMetadataProperty.GetStringAttributeOrDefault(Extensions.ReferenceNameAttr, null);
			this._hidden = xelementMetadataProperty.GetBooleanAttributeOrDefault(Extensions.HiddenAttr, false);
			this._showAsVariationsOnly = xelementMetadataProperty.GetBooleanAttributeOrDefault(Extensions.ShowAsVariationsOnlyAttr, false);
			this._private = xelementMetadataProperty.GetBooleanAttributeOrDefault(Extensions.PrivateAttr, false);
		}

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x0600193F RID: 6463 RVA: 0x00044E3C File Offset: 0x0004303C
		public QualifiedName QualifiedName
		{
			get
			{
				return this._qname;
			}
		}

		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x06001940 RID: 6464 RVA: 0x00044E44 File Offset: 0x00043044
		public string Caption
		{
			get
			{
				return this._caption ?? base.Name;
			}
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001941 RID: 6465 RVA: 0x00044E56 File Offset: 0x00043056
		internal EntityType ElementType
		{
			get
			{
				return this._elementType;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001942 RID: 6466 RVA: 0x00044E5E File Offset: 0x0004305E
		public bool Hidden
		{
			get
			{
				return this._hidden;
			}
		}

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06001943 RID: 6467 RVA: 0x00044E66 File Offset: 0x00043066
		public bool ShowAsVariationsOnly
		{
			get
			{
				return this._showAsVariationsOnly;
			}
		}

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06001944 RID: 6468 RVA: 0x00044E6E File Offset: 0x0004306E
		public bool Private
		{
			get
			{
				return this._private;
			}
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06001945 RID: 6469 RVA: 0x00044E76 File Offset: 0x00043076
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? base.Name;
			}
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x06001946 RID: 6470 RVA: 0x00044E88 File Offset: 0x00043088
		internal EntitySet InternalEntitySet
		{
			get
			{
				return (EntitySet)base.InternalEntitySetBase;
			}
		}

		// Token: 0x06001947 RID: 6471 RVA: 0x00044E95 File Offset: 0x00043095
		public IEnumerable<EdmField> GetDisplayKeyFields()
		{
			return this.ElementType.DisplayKey.OfType<EdmField>();
		}

		// Token: 0x04000DC4 RID: 3524
		private readonly EntityType _elementType;

		// Token: 0x04000DC5 RID: 3525
		private readonly QualifiedName _qname;

		// Token: 0x04000DC6 RID: 3526
		private readonly string _caption;

		// Token: 0x04000DC7 RID: 3527
		private readonly string _referenceName;

		// Token: 0x04000DC8 RID: 3528
		private readonly bool _hidden;

		// Token: 0x04000DC9 RID: 3529
		private readonly bool _showAsVariationsOnly;

		// Token: 0x04000DCA RID: 3530
		private readonly bool _private;
	}
}

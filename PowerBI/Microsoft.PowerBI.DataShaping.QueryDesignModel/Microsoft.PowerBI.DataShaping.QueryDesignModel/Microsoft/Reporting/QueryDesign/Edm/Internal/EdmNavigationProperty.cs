using System;
using System.Xml.Linq;
using Microsoft.Data.Metadata.Edm;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200020E RID: 526
	public sealed class EdmNavigationProperty : EdmItem, IEntityMemberItem
	{
		// Token: 0x06001890 RID: 6288 RVA: 0x00043425 File Offset: 0x00041625
		internal EdmNavigationProperty(NavigationProperty navigationProperty, XElement extensionElem)
		{
			this._navigationProperty = ArgumentValidation.CheckNotNull<NavigationProperty>(navigationProperty, "navigationProperty");
			this._referenceName = extensionElem.GetStringAttributeOrDefault(Extensions.ReferenceNameAttr, null);
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001891 RID: 6289 RVA: 0x00043450 File Offset: 0x00041650
		public string Name
		{
			get
			{
				return this._navigationProperty.Name;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001892 RID: 6290 RVA: 0x0004345D File Offset: 0x0004165D
		public string ReferenceName
		{
			get
			{
				return this._referenceName ?? this.Name;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001893 RID: 6291 RVA: 0x0004346F File Offset: 0x0004166F
		public string AssociationName
		{
			get
			{
				return this._navigationProperty.RelationshipType.FullName;
			}
		}

		// Token: 0x170006D8 RID: 1752
		// (get) Token: 0x06001894 RID: 6292 RVA: 0x00043481 File Offset: 0x00041681
		public string AssociationSourceName
		{
			get
			{
				return this._navigationProperty.FromEndMember.Name;
			}
		}

		// Token: 0x170006D9 RID: 1753
		// (get) Token: 0x06001895 RID: 6293 RVA: 0x00043493 File Offset: 0x00041693
		public string AssociationTargetName
		{
			get
			{
				return this._navigationProperty.ToEndMember.Name;
			}
		}

		// Token: 0x170006DA RID: 1754
		// (get) Token: 0x06001896 RID: 6294 RVA: 0x000434A5 File Offset: 0x000416A5
		public RelationshipMultiplicity SourceMultiplicity
		{
			get
			{
				return (RelationshipMultiplicity)this._navigationProperty.FromEndMember.RelationshipMultiplicity;
			}
		}

		// Token: 0x170006DB RID: 1755
		// (get) Token: 0x06001897 RID: 6295 RVA: 0x000434B7 File Offset: 0x000416B7
		public RelationshipMultiplicity TargetMultipicity
		{
			get
			{
				return (RelationshipMultiplicity)this._navigationProperty.ToEndMember.RelationshipMultiplicity;
			}
		}

		// Token: 0x170006DC RID: 1756
		// (get) Token: 0x06001898 RID: 6296 RVA: 0x000434C9 File Offset: 0x000416C9
		internal override MetadataItem InternalEdmItem
		{
			get
			{
				return this._navigationProperty;
			}
		}

		// Token: 0x04000D13 RID: 3347
		private readonly NavigationProperty _navigationProperty;

		// Token: 0x04000D14 RID: 3348
		private readonly string _referenceName;
	}
}

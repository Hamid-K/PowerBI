using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Objects.DataClasses
{
	// Token: 0x02000483 RID: 1155
	[Serializable]
	internal class RelationshipNavigation
	{
		// Token: 0x0600390C RID: 14604 RVA: 0x000BCDC4 File Offset: 0x000BAFC4
		internal RelationshipNavigation(string relationshipName, string from, string to, NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			Check.NotEmpty(relationshipName, "relationshipName");
			Check.NotEmpty(from, "from");
			Check.NotEmpty(to, "to");
			this._relationshipName = relationshipName;
			this._from = from;
			this._to = to;
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x0600390D RID: 14605 RVA: 0x000BCE20 File Offset: 0x000BB020
		internal RelationshipNavigation(AssociationType associationType, string from, string to, NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			this._associationType = associationType;
			this._relationshipName = associationType.FullName;
			this._from = from;
			this._to = to;
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x0600390E RID: 14606 RVA: 0x000BCE59 File Offset: 0x000BB059
		internal AssociationType AssociationType
		{
			get
			{
				return this._associationType;
			}
		}

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x0600390F RID: 14607 RVA: 0x000BCE61 File Offset: 0x000BB061
		internal string RelationshipName
		{
			get
			{
				return this._relationshipName;
			}
		}

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06003910 RID: 14608 RVA: 0x000BCE69 File Offset: 0x000BB069
		internal string From
		{
			get
			{
				return this._from;
			}
		}

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06003911 RID: 14609 RVA: 0x000BCE71 File Offset: 0x000BB071
		internal string To
		{
			get
			{
				return this._to;
			}
		}

		// Token: 0x17000AE7 RID: 2791
		// (get) Token: 0x06003912 RID: 14610 RVA: 0x000BCE79 File Offset: 0x000BB079
		internal NavigationPropertyAccessor ToPropertyAccessor
		{
			get
			{
				return this._toAccessor;
			}
		}

		// Token: 0x17000AE8 RID: 2792
		// (get) Token: 0x06003913 RID: 14611 RVA: 0x000BCE81 File Offset: 0x000BB081
		internal bool IsInitialized
		{
			get
			{
				return this._toAccessor != null && this._fromAccessor != null;
			}
		}

		// Token: 0x06003914 RID: 14612 RVA: 0x000BCE96 File Offset: 0x000BB096
		internal void InitializeAccessors(NavigationPropertyAccessor fromAccessor, NavigationPropertyAccessor toAccessor)
		{
			this._fromAccessor = fromAccessor;
			this._toAccessor = toAccessor;
		}

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06003915 RID: 14613 RVA: 0x000BCEA8 File Offset: 0x000BB0A8
		internal RelationshipNavigation Reverse
		{
			get
			{
				if (this._reverse == null || !this._reverse.IsInitialized)
				{
					this._reverse = ((this._associationType != null) ? new RelationshipNavigation(this._associationType, this._to, this._from, this._toAccessor, this._fromAccessor) : new RelationshipNavigation(this._relationshipName, this._to, this._from, this._toAccessor, this._fromAccessor));
				}
				return this._reverse;
			}
		}

		// Token: 0x06003916 RID: 14614 RVA: 0x000BCF28 File Offset: 0x000BB128
		public override bool Equals(object obj)
		{
			RelationshipNavigation relationshipNavigation = obj as RelationshipNavigation;
			return this == relationshipNavigation || (this != null && relationshipNavigation != null && this.RelationshipName == relationshipNavigation.RelationshipName && this.From == relationshipNavigation.From && this.To == relationshipNavigation.To);
		}

		// Token: 0x06003917 RID: 14615 RVA: 0x000BCF81 File Offset: 0x000BB181
		public override int GetHashCode()
		{
			return this.RelationshipName.GetHashCode();
		}

		// Token: 0x06003918 RID: 14616 RVA: 0x000BCF8E File Offset: 0x000BB18E
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "RelationshipNavigation: ({0},{1},{2})", new object[] { this._relationshipName, this._from, this._to });
		}

		// Token: 0x040012FF RID: 4863
		private readonly string _relationshipName;

		// Token: 0x04001300 RID: 4864
		private readonly string _from;

		// Token: 0x04001301 RID: 4865
		private readonly string _to;

		// Token: 0x04001302 RID: 4866
		[NonSerialized]
		private RelationshipNavigation _reverse;

		// Token: 0x04001303 RID: 4867
		[NonSerialized]
		private NavigationPropertyAccessor _fromAccessor;

		// Token: 0x04001304 RID: 4868
		[NonSerialized]
		private NavigationPropertyAccessor _toAccessor;

		// Token: 0x04001305 RID: 4869
		[NonSerialized]
		private readonly AssociationType _associationType;
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x02000527 RID: 1319
	public class EndPropertyMapping : PropertyMapping
	{
		// Token: 0x06004100 RID: 16640 RVA: 0x000DC512 File Offset: 0x000DA712
		public EndPropertyMapping(AssociationEndMember associationEnd)
		{
			Check.NotNull<AssociationEndMember>(associationEnd, "associationEnd");
			this._associationEnd = associationEnd;
		}

		// Token: 0x06004101 RID: 16641 RVA: 0x000DC538 File Offset: 0x000DA738
		internal EndPropertyMapping()
		{
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06004102 RID: 16642 RVA: 0x000DC54B File Offset: 0x000DA74B
		// (set) Token: 0x06004103 RID: 16643 RVA: 0x000DC553 File Offset: 0x000DA753
		public AssociationEndMember AssociationEnd
		{
			get
			{
				return this._associationEnd;
			}
			internal set
			{
				this._associationEnd = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06004104 RID: 16644 RVA: 0x000DC55C File Offset: 0x000DA75C
		public ReadOnlyCollection<ScalarPropertyMapping> PropertyMappings
		{
			get
			{
				return new ReadOnlyCollection<ScalarPropertyMapping>(this._properties);
			}
		}

		// Token: 0x17000CB8 RID: 3256
		// (get) Token: 0x06004105 RID: 16645 RVA: 0x000DC569 File Offset: 0x000DA769
		internal IEnumerable<EdmMember> StoreProperties
		{
			get
			{
				return this.PropertyMappings.Select((ScalarPropertyMapping propertyMap) => propertyMap.Column);
			}
		}

		// Token: 0x06004106 RID: 16646 RVA: 0x000DC595 File Offset: 0x000DA795
		public void AddPropertyMapping(ScalarPropertyMapping propertyMapping)
		{
			Check.NotNull<ScalarPropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this._properties.Add(propertyMapping);
		}

		// Token: 0x06004107 RID: 16647 RVA: 0x000DC5B5 File Offset: 0x000DA7B5
		public void RemovePropertyMapping(ScalarPropertyMapping propertyMapping)
		{
			Check.NotNull<ScalarPropertyMapping>(propertyMapping, "propertyMapping");
			base.ThrowIfReadOnly();
			this._properties.Remove(propertyMapping);
		}

		// Token: 0x06004108 RID: 16648 RVA: 0x000DC5D6 File Offset: 0x000DA7D6
		internal override void SetReadOnly()
		{
			this._properties.TrimExcess();
			MappingItem.SetReadOnly(this._properties);
			base.SetReadOnly();
		}

		// Token: 0x0400168C RID: 5772
		private AssociationEndMember _associationEnd;

		// Token: 0x0400168D RID: 5773
		private readonly List<ScalarPropertyMapping> _properties = new List<ScalarPropertyMapping>();
	}
}

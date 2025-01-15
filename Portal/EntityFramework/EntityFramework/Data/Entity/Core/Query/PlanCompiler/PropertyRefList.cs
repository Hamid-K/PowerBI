using System;
using System.Collections.Generic;

namespace System.Data.Entity.Core.Query.PlanCompiler
{
	// Token: 0x02000360 RID: 864
	internal class PropertyRefList
	{
		// Token: 0x06002A0F RID: 10767 RVA: 0x000890B6 File Offset: 0x000872B6
		internal PropertyRefList()
			: this(false)
		{
		}

		// Token: 0x06002A10 RID: 10768 RVA: 0x000890BF File Offset: 0x000872BF
		private PropertyRefList(bool allProps)
		{
			this.m_propertyReferences = new Dictionary<PropertyRef, PropertyRef>();
			if (allProps)
			{
				this.MakeAllProperties();
			}
		}

		// Token: 0x06002A11 RID: 10769 RVA: 0x000890DB File Offset: 0x000872DB
		private void MakeAllProperties()
		{
			this.m_allProperties = true;
			this.m_propertyReferences.Clear();
			this.m_propertyReferences.Add(AllPropertyRef.Instance, AllPropertyRef.Instance);
		}

		// Token: 0x06002A12 RID: 10770 RVA: 0x00089104 File Offset: 0x00087304
		internal void Add(PropertyRef property)
		{
			if (this.m_allProperties)
			{
				return;
			}
			if (property is AllPropertyRef)
			{
				this.MakeAllProperties();
				return;
			}
			this.m_propertyReferences[property] = property;
		}

		// Token: 0x06002A13 RID: 10771 RVA: 0x0008912C File Offset: 0x0008732C
		internal void Append(PropertyRefList propertyRefs)
		{
			if (this.m_allProperties)
			{
				return;
			}
			foreach (PropertyRef propertyRef in propertyRefs.m_propertyReferences.Keys)
			{
				this.Add(propertyRef);
			}
		}

		// Token: 0x17000873 RID: 2163
		// (get) Token: 0x06002A14 RID: 10772 RVA: 0x00089190 File Offset: 0x00087390
		internal bool AllProperties
		{
			get
			{
				return this.m_allProperties;
			}
		}

		// Token: 0x06002A15 RID: 10773 RVA: 0x00089198 File Offset: 0x00087398
		internal PropertyRefList Clone()
		{
			PropertyRefList propertyRefList = new PropertyRefList(this.m_allProperties);
			foreach (PropertyRef propertyRef in this.m_propertyReferences.Keys)
			{
				propertyRefList.Add(propertyRef);
			}
			return propertyRefList;
		}

		// Token: 0x06002A16 RID: 10774 RVA: 0x00089200 File Offset: 0x00087400
		internal bool Contains(PropertyRef p)
		{
			return this.m_allProperties || this.m_propertyReferences.ContainsKey(p);
		}

		// Token: 0x17000874 RID: 2164
		// (get) Token: 0x06002A17 RID: 10775 RVA: 0x00089218 File Offset: 0x00087418
		internal IEnumerable<PropertyRef> Properties
		{
			get
			{
				return this.m_propertyReferences.Keys;
			}
		}

		// Token: 0x06002A18 RID: 10776 RVA: 0x00089228 File Offset: 0x00087428
		public override string ToString()
		{
			string text = "{";
			foreach (PropertyRef propertyRef in this.m_propertyReferences.Keys)
			{
				string text2 = text;
				PropertyRef propertyRef2 = propertyRef;
				text = text2 + ((propertyRef2 != null) ? propertyRef2.ToString() : null) + ",";
			}
			text += "}";
			return text;
		}

		// Token: 0x04000E6D RID: 3693
		private readonly Dictionary<PropertyRef, PropertyRef> m_propertyReferences;

		// Token: 0x04000E6E RID: 3694
		private bool m_allProperties;

		// Token: 0x04000E6F RID: 3695
		internal static PropertyRefList All = new PropertyRefList(true);
	}
}

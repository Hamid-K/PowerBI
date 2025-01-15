using System;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020000B3 RID: 179
	internal sealed class GetPropertiesActionParameters : RSSoapActionParameters
	{
		// Token: 0x170002B0 RID: 688
		// (get) Token: 0x0600080F RID: 2063 RVA: 0x000212A8 File Offset: 0x0001F4A8
		// (set) Token: 0x06000810 RID: 2064 RVA: 0x000212B0 File Offset: 0x0001F4B0
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x170002B1 RID: 689
		// (get) Token: 0x06000811 RID: 2065 RVA: 0x000212B9 File Offset: 0x0001F4B9
		// (set) Token: 0x06000812 RID: 2066 RVA: 0x000212C1 File Offset: 0x0001F4C1
		public ItemNamespaceEnum ItemNamespace
		{
			get
			{
				return this.m_itemNamespace;
			}
			set
			{
				this.m_itemNamespace = value;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x000212CA File Offset: 0x0001F4CA
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x000212D2 File Offset: 0x0001F4D2
		public Property[] RequestedProperties
		{
			get
			{
				return this.m_requestedProperties;
			}
			set
			{
				this.m_requestedProperties = value;
			}
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000212DB File Offset: 0x0001F4DB
		// (set) Token: 0x06000816 RID: 2070 RVA: 0x000212E3 File Offset: 0x0001F4E3
		public bool AllowEditSessionSyntax
		{
			get
			{
				return this.m_allowEditSessionSyntax;
			}
			set
			{
				this.m_allowEditSessionSyntax = value;
			}
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000817 RID: 2071 RVA: 0x000212EC File Offset: 0x0001F4EC
		// (set) Token: 0x06000818 RID: 2072 RVA: 0x000212F4 File Offset: 0x0001F4F4
		public Property[] PropertyValues
		{
			get
			{
				return this.m_propertyValues;
			}
			set
			{
				this.m_propertyValues = value;
			}
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000819 RID: 2073 RVA: 0x000212FD File Offset: 0x0001F4FD
		internal override string InputTrace
		{
			get
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}, {1}", this.ItemPath, this.ItemNamespace);
			}
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0002131F File Offset: 0x0001F51F
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00021344 File Offset: 0x0001F544
		public string GetValue(string propertyName)
		{
			foreach (Property property in this.PropertyValues)
			{
				if (property.Name == propertyName)
				{
					return property.Value;
				}
			}
			return null;
		}

		// Token: 0x04000412 RID: 1042
		private string m_itemPath;

		// Token: 0x04000413 RID: 1043
		private ItemNamespaceEnum m_itemNamespace;

		// Token: 0x04000414 RID: 1044
		private Property[] m_requestedProperties;

		// Token: 0x04000415 RID: 1045
		private bool m_allowEditSessionSyntax;

		// Token: 0x04000416 RID: 1046
		private Property[] m_propertyValues;
	}
}

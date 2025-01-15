using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A7 RID: 167
	public class MetadataProperty : ReportObject
	{
		// Token: 0x06000740 RID: 1856 RVA: 0x0001AF6E File Offset: 0x0001916E
		public MetadataProperty()
		{
		}

		// Token: 0x06000741 RID: 1857 RVA: 0x0001AF76 File Offset: 0x00019176
		internal MetadataProperty(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001AF7F File Offset: 0x0001917F
		public MetadataProperty(string name, string description)
		{
			this.Name = name;
			this.Description = description;
		}

		// Token: 0x06000743 RID: 1859 RVA: 0x0001AF95 File Offset: 0x00019195
		public override void Initialize()
		{
			base.Initialize();
			this.Values = new MetadataValues();
		}

		// Token: 0x1700025F RID: 607
		// (get) Token: 0x06000744 RID: 1860 RVA: 0x0001AFA8 File Offset: 0x000191A8
		// (set) Token: 0x06000745 RID: 1861 RVA: 0x0001AFBB File Offset: 0x000191BB
		[XmlAttribute]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000260 RID: 608
		// (get) Token: 0x06000746 RID: 1862 RVA: 0x0001AFCA File Offset: 0x000191CA
		// (set) Token: 0x06000747 RID: 1863 RVA: 0x0001AFDD File Offset: 0x000191DD
		[XmlAttribute]
		public string Description
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000261 RID: 609
		// (get) Token: 0x06000748 RID: 1864 RVA: 0x0001AFEC File Offset: 0x000191EC
		// (set) Token: 0x06000749 RID: 1865 RVA: 0x0001AFFF File Offset: 0x000191FF
		[XmlArrayItem("Value", typeof(MetadataValue))]
		public MetadataValues Values
		{
			get
			{
				return (MetadataValues)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000262 RID: 610
		// (get) Token: 0x0600074A RID: 1866 RVA: 0x0001B00E File Offset: 0x0001920E
		// (set) Token: 0x0600074B RID: 1867 RVA: 0x0001B039 File Offset: 0x00019239
		[XmlIgnore]
		public string Value
		{
			get
			{
				if (this.Values != null && this.Values.Count > 0)
				{
					return this.Values[0].Value;
				}
				return null;
			}
			set
			{
				if (this.Values == null)
				{
					this.Values = new MetadataValues();
				}
				else
				{
					this.Values.Clear();
				}
				if (value != null)
				{
					this.Values.Add(value);
				}
			}
		}

		// Token: 0x0200035B RID: 859
		internal class Definition : DefinitionStore<MetadataProperty, MetadataProperty.Definition.Properties>
		{
			// Token: 0x0200047A RID: 1146
			internal enum Properties
			{
				// Token: 0x04000AAE RID: 2734
				Name,
				// Token: 0x04000AAF RID: 2735
				Description,
				// Token: 0x04000AB0 RID: 2736
				Values
			}
		}
	}
}

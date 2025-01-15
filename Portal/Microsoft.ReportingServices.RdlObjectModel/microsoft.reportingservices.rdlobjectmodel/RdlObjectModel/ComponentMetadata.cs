using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000A5 RID: 165
	public class ComponentMetadata : ReportObject
	{
		// Token: 0x0600072B RID: 1835 RVA: 0x0001AD44 File Offset: 0x00018F44
		public ComponentMetadata()
		{
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001AD4C File Offset: 0x00018F4C
		internal ComponentMetadata(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600072D RID: 1837 RVA: 0x0001AD55 File Offset: 0x00018F55
		public override void Initialize()
		{
			base.Initialize();
			this.UserProperties = new MetadataProperties();
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x0600072E RID: 1838 RVA: 0x0001AD68 File Offset: 0x00018F68
		// (set) Token: 0x0600072F RID: 1839 RVA: 0x0001AD7B File Offset: 0x00018F7B
		[XmlElement]
		public Guid? ComponentId
		{
			get
			{
				return (Guid?)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000730 RID: 1840 RVA: 0x0001AD8F File Offset: 0x00018F8F
		// (set) Token: 0x06000731 RID: 1841 RVA: 0x0001AD9D File Offset: 0x00018F9D
		[XmlElement]
		[DefaultValue(false)]
		public bool HideUpdateNotifications
		{
			get
			{
				return base.PropertyStore.GetBoolean(1);
			}
			set
			{
				base.PropertyStore.SetBoolean(1, value);
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000732 RID: 1842 RVA: 0x0001ADAC File Offset: 0x00018FAC
		// (set) Token: 0x06000733 RID: 1843 RVA: 0x0001ADBF File Offset: 0x00018FBF
		[XmlElement]
		public string SourcePath
		{
			get
			{
				return (string)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000734 RID: 1844 RVA: 0x0001ADCE File Offset: 0x00018FCE
		// (set) Token: 0x06000735 RID: 1845 RVA: 0x0001ADE1 File Offset: 0x00018FE1
		[XmlElement]
		public DateTime? SyncDate
		{
			get
			{
				return (DateTime?)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000736 RID: 1846 RVA: 0x0001ADF5 File Offset: 0x00018FF5
		// (set) Token: 0x06000737 RID: 1847 RVA: 0x0001AE08 File Offset: 0x00019008
		[XmlArrayItem("UserProperty", typeof(MetadataProperty))]
		public MetadataProperties UserProperties
		{
			get
			{
				return (MetadataProperties)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700025D RID: 605
		// (get) Token: 0x06000738 RID: 1848 RVA: 0x0001AE18 File Offset: 0x00019018
		// (set) Token: 0x06000739 RID: 1849 RVA: 0x0001AE41 File Offset: 0x00019041
		[XmlIgnore]
		public string Description
		{
			get
			{
				MetadataProperty metadataProperty = this.UserProperties["Description"];
				if (metadataProperty != null)
				{
					return metadataProperty.Value;
				}
				return null;
			}
			set
			{
				if (!string.Equals(this.Description, value, StringComparison.CurrentCulture))
				{
					this.UserProperties.GetProperty("Description", string.Empty).Value = value;
				}
			}
		}

		// Token: 0x04000121 RID: 289
		private const string DescriptionName = "Description";

		// Token: 0x0200035A RID: 858
		internal class Definition : DefinitionStore<ComponentMetadata, ComponentMetadata.Definition.Properties>
		{
			// Token: 0x02000479 RID: 1145
			internal enum Properties
			{
				// Token: 0x04000AA8 RID: 2728
				ComponentId,
				// Token: 0x04000AA9 RID: 2729
				HideUpdateNotifications,
				// Token: 0x04000AAA RID: 2730
				SourcePath,
				// Token: 0x04000AAB RID: 2731
				SyncDate,
				// Token: 0x04000AAC RID: 2732
				UserProperties
			}
		}
	}
}

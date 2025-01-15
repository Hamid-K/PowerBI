using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200059F RID: 1439
	public class SnaUserData : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06003203 RID: 12803 RVA: 0x000A72FC File Offset: 0x000A54FC
		// (set) Token: 0x06003204 RID: 12804 RVA: 0x000A7304 File Offset: 0x000A5504
		public bool IsTypeDefined
		{
			get
			{
				return this.isTypeDefined;
			}
			set
			{
				this.isTypeDefined = value;
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06003205 RID: 12805 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x06003206 RID: 12806 RVA: 0x000A30E4 File Offset: 0x000A12E4
		[ConfigurationProperty("localLuName", IsRequired = true)]
		[Browsable(false)]
		[StringValidator(MaxLength = 8)]
		public string LocalLuName
		{
			get
			{
				return (string)base["localLuName"];
			}
			set
			{
				base["localLuName"] = value;
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06003207 RID: 12807 RVA: 0x000A311F File Offset: 0x000A131F
		// (set) Token: 0x06003208 RID: 12808 RVA: 0x000A3131 File Offset: 0x000A1331
		[ConfigurationProperty("remoteLuName", IsRequired = true)]
		[Browsable(false)]
		[StringValidator(MaxLength = 8)]
		public string RemoteLuName
		{
			get
			{
				return (string)base["remoteLuName"];
			}
			set
			{
				base["remoteLuName"] = value;
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000A44D1 File Offset: 0x000A26D1
		// (set) Token: 0x0600320A RID: 12810 RVA: 0x000A44E3 File Offset: 0x000A26E3
		[ConfigurationProperty("modeName", IsRequired = true)]
		[Browsable(false)]
		[StringValidator(MaxLength = 8)]
		public string ModeName
		{
			get
			{
				return (string)base["modeName"];
			}
			set
			{
				base["modeName"] = value;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x0600320B RID: 12811 RVA: 0x000A44F1 File Offset: 0x000A26F1
		// (set) Token: 0x0600320C RID: 12812 RVA: 0x000A4503 File Offset: 0x000A2703
		[ConfigurationProperty("syncLevel2Supported", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		public bool SyncLevel2Supported
		{
			get
			{
				return (bool)base["syncLevel2Supported"];
			}
			set
			{
				base["syncLevel2Supported"] = value;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x0600320D RID: 12813 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x0600320E RID: 12814 RVA: 0x000A3748 File Offset: 0x000A1948
		[ConfigurationProperty("securityFromClientContext", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		public bool SecurityFromClientContext
		{
			get
			{
				return (bool)base["securityFromClientContext"];
			}
			set
			{
				base["securityFromClientContext"] = value;
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x0600320F RID: 12815 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x06003210 RID: 12816 RVA: 0x000A376D File Offset: 0x000A196D
		[ConfigurationProperty("essoAffiliateApplication", IsRequired = false)]
		[Browsable(false)]
		public string EssoAffiliateApplication
		{
			get
			{
				return (string)base["essoAffiliateApplication"];
			}
			set
			{
				base["essoAffiliateApplication"] = value;
			}
		}

		// Token: 0x06003211 RID: 12817 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x06003212 RID: 12818 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003213 RID: 12819 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003214 RID: 12820 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003215 RID: 12821 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003216 RID: 12822 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003217 RID: 12823 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003218 RID: 12824 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003219 RID: 12825 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600321A RID: 12826 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600321B RID: 12827 RVA: 0x000A7310 File Offset: 0x000A5510
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "SSL" || propertyDescriptor.Category == "IMS" || propertyDescriptor.Category == "SNA" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600321C RID: 12828 RVA: 0x000A7414 File Offset: 0x000A5614
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "SSL" || propertyDescriptor.Category == "IMS" || propertyDescriptor.Category == "SNA" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600321D RID: 12829 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C70 RID: 7280
		private bool isTypeDefined;
	}
}

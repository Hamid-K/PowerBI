using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200058D RID: 1421
	public class ImsLu62 : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x060030F6 RID: 12534 RVA: 0x000A44C0 File Offset: 0x000A26C0
		// (set) Token: 0x060030F7 RID: 12535 RVA: 0x000A44C8 File Offset: 0x000A26C8
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

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x060030F8 RID: 12536 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x060030F9 RID: 12537 RVA: 0x000A30E4 File Offset: 0x000A12E4
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

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x060030FA RID: 12538 RVA: 0x000A311F File Offset: 0x000A131F
		// (set) Token: 0x060030FB RID: 12539 RVA: 0x000A3131 File Offset: 0x000A1331
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

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x060030FC RID: 12540 RVA: 0x000A44D1 File Offset: 0x000A26D1
		// (set) Token: 0x060030FD RID: 12541 RVA: 0x000A44E3 File Offset: 0x000A26E3
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

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x060030FE RID: 12542 RVA: 0x000A44F1 File Offset: 0x000A26F1
		// (set) Token: 0x060030FF RID: 12543 RVA: 0x000A4503 File Offset: 0x000A2703
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

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06003100 RID: 12544 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x06003101 RID: 12545 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06003102 RID: 12546 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x06003103 RID: 12547 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x06003104 RID: 12548 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600310B RID: 12555 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600310D RID: 12557 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600310E RID: 12558 RVA: 0x000A4518 File Offset: 0x000A2718
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

		// Token: 0x0600310F RID: 12559 RVA: 0x000A461C File Offset: 0x000A281C
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

		// Token: 0x06003110 RID: 12560 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C51 RID: 7249
		private bool isTypeDefined;
	}
}

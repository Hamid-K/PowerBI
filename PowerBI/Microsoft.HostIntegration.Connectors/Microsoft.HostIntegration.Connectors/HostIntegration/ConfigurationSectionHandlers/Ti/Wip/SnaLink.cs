using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200059E RID: 1438
	public class SnaLink : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x060031E1 RID: 12769 RVA: 0x000A7079 File Offset: 0x000A5279
		// (set) Token: 0x060031E2 RID: 12770 RVA: 0x000A7081 File Offset: 0x000A5281
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

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x060031E3 RID: 12771 RVA: 0x000A30D2 File Offset: 0x000A12D2
		// (set) Token: 0x060031E4 RID: 12772 RVA: 0x000A30E4 File Offset: 0x000A12E4
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

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x060031E5 RID: 12773 RVA: 0x000A311F File Offset: 0x000A131F
		// (set) Token: 0x060031E6 RID: 12774 RVA: 0x000A3131 File Offset: 0x000A1331
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

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x060031E7 RID: 12775 RVA: 0x000A44D1 File Offset: 0x000A26D1
		// (set) Token: 0x060031E8 RID: 12776 RVA: 0x000A44E3 File Offset: 0x000A26E3
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

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x000A708A File Offset: 0x000A528A
		// (set) Token: 0x060031EA RID: 12778 RVA: 0x000A709C File Offset: 0x000A529C
		[ConfigurationProperty("allowExplicitSyncPoint", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		public bool AllowExplicitSyncPoint
		{
			get
			{
				return (bool)base["allowExplicitSyncPoint"];
			}
			set
			{
				base["allowExplicitSyncPoint"] = value;
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000A44F1 File Offset: 0x000A26F1
		// (set) Token: 0x060031EC RID: 12780 RVA: 0x000A4503 File Offset: 0x000A2703
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

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000A70AF File Offset: 0x000A52AF
		// (set) Token: 0x060031EE RID: 12782 RVA: 0x000A70C1 File Offset: 0x000A52C1
		[ConfigurationProperty("mirrorTransactionId", IsRequired = false)]
		[Browsable(false)]
		public string MirrorTransactionId
		{
			get
			{
				return (string)base["mirrorTransactionId"];
			}
			set
			{
				base["mirrorTransactionId"] = value;
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x000A70CF File Offset: 0x000A52CF
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x000A70E1 File Offset: 0x000A52E1
		[ConfigurationProperty("overrideSnaSourceTransactionProgram", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		public bool OverrideSnaSourceTransactionProgram
		{
			get
			{
				return (bool)base["overrideSnaSourceTransactionProgram"];
			}
			set
			{
				base["overrideSnaSourceTransactionProgram"] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x060031F5 RID: 12789 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x060031F6 RID: 12790 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060031F7 RID: 12791 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060031F8 RID: 12792 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060031F9 RID: 12793 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060031FB RID: 12795 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060031FC RID: 12796 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060031FD RID: 12797 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060031FE RID: 12798 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060031FF RID: 12799 RVA: 0x000A70F4 File Offset: 0x000A52F4
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

		// Token: 0x06003200 RID: 12800 RVA: 0x000A71F8 File Offset: 0x000A53F8
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

		// Token: 0x06003201 RID: 12801 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C6F RID: 7279
		private bool isTypeDefined;
	}
}

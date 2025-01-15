using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200059C RID: 1436
	public class RemoteEnvironment : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060031A1 RID: 12705 RVA: 0x000A6A58 File Offset: 0x000A4C58
		public RemoteEnvironmentType RemoteEnvironmentType
		{
			get
			{
				if (this.ElmLink.ElementInformation.IsPresent || this.ElmLink.IsTypeDefined)
				{
					return RemoteEnvironmentType.ElmLink;
				}
				if (this.ElmUserData.ElementInformation.IsPresent || this.ElmUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.ElmUserData;
				}
				if (this.TrmLink.ElementInformation.IsPresent || this.TrmLink.IsTypeDefined)
				{
					return RemoteEnvironmentType.TrmLink;
				}
				if (this.TrmUserData.ElementInformation.IsPresent || this.TrmUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.TrmUserData;
				}
				if (this.HttpLink.ElementInformation.IsPresent || this.HttpLink.IsTypeDefined)
				{
					return RemoteEnvironmentType.HttpLink;
				}
				if (this.HttpUserData.ElementInformation.IsPresent || this.HttpUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.HttpUserData;
				}
				if (this.DistributedProgramCall.ElementInformation.IsPresent || this.DistributedProgramCall.IsTypeDefined)
				{
					return RemoteEnvironmentType.DistributedProgramCall;
				}
				if (this.ImsConnect.ElementInformation.IsPresent || this.ImsConnect.IsTypeDefined)
				{
					return RemoteEnvironmentType.ImsConnect;
				}
				if (this.SnaLink.ElementInformation.IsPresent || this.SnaLink.IsTypeDefined)
				{
					return RemoteEnvironmentType.SnaLink;
				}
				if (this.SnaUserData.ElementInformation.IsPresent || this.SnaUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.SnaUserData;
				}
				if (this.ImsLu62.ElementInformation.IsPresent || this.ImsLu62.IsTypeDefined)
				{
					return RemoteEnvironmentType.ImsLu62;
				}
				if (this.SystemzSocketsLink.ElementInformation.IsPresent || this.SystemzSocketsLink.IsTypeDefined)
				{
					return RemoteEnvironmentType.SystemzSocketsLink;
				}
				if (this.SystemzSocketsUserData.ElementInformation.IsPresent || this.SystemzSocketsUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.SystemzSocketsUserData;
				}
				if (this.SystemiSocketsUserData.ElementInformation.IsPresent || this.SystemiSocketsUserData.IsTypeDefined)
				{
					return RemoteEnvironmentType.SystemiSocketsUserData;
				}
				return RemoteEnvironmentType.Unknown;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060031A2 RID: 12706 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x060031A3 RID: 12707 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[TypeConverter(typeof(RemoteEnvironmentNameValidation))]
		[Description("Name given to the Remote Environment.")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = false)]
		[DisplayName("Name")]
		[Browsable(true)]
		public string Name
		{
			get
			{
				return (string)base["name"];
			}
			set
			{
				base["name"] = value;
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x060031A4 RID: 12708 RVA: 0x000A6C3A File Offset: 0x000A4E3A
		// (set) Token: 0x060031A5 RID: 12709 RVA: 0x000A6C4C File Offset: 0x000A4E4C
		[Description("IsDefault indicates that this RE is the default RE to be used in the configuration file when multiple REs of the same type exist. Only one RE of the same type can have this attribute set to true.")]
		[Category("General")]
		[ConfigurationProperty("isDefault", IsRequired = false)]
		[DisplayName("Is Default")]
		public bool IsDefault
		{
			get
			{
				return (bool)base["isDefault"];
			}
			set
			{
				base["isDefault"] = value;
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060031A6 RID: 12710 RVA: 0x000A313F File Offset: 0x000A133F
		// (set) Token: 0x060031A7 RID: 12711 RVA: 0x000A3151 File Offset: 0x000A1351
		[Description("The code page used to transform the incoming and outgoing data to a form that can be used by the host application program. The default is 37.")]
		[Category("Conversion")]
		[ConfigurationProperty("codePage", IsRequired = false, DefaultValue = 37)]
		[DisplayName("Code Page")]
		public int CodePage
		{
			get
			{
				return (int)base["codePage"];
			}
			set
			{
				base["codePage"] = value;
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060031A8 RID: 12712 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x060031A9 RID: 12713 RVA: 0x00019FBE File Offset: 0x000181BE
		[Description("Type the number of seconds. The send and receive time-out values are used by the WIP runtime environment when it communicates with the host environment. The time-out values are used on transport-specific APIs to terminate the receive API function when no host data or acknowledgement is received in the specified amount of time. The number of seconds can be a maximum of 3600 and a minimum of 0.")]
		[Category("General")]
		[ConfigurationProperty("timeout", IsRequired = false, DefaultValue = 10)]
		[DisplayName("Timeout")]
		public int Timeout
		{
			get
			{
				return (int)base["timeout"];
			}
			set
			{
				base["timeout"] = value;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060031AA RID: 12714 RVA: 0x000A6C5F File Offset: 0x000A4E5F
		// (set) Token: 0x060031AB RID: 12715 RVA: 0x000A6C71 File Offset: 0x000A4E71
		[ConfigurationProperty("systemzSocketsLink", IsRequired = false)]
		[Browsable(false)]
		public SystemzSocketsLink SystemzSocketsLink
		{
			get
			{
				return (SystemzSocketsLink)base["systemzSocketsLink"];
			}
			set
			{
				base["systemzSocketsLink"] = value;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x060031AC RID: 12716 RVA: 0x000A6C7F File Offset: 0x000A4E7F
		// (set) Token: 0x060031AD RID: 12717 RVA: 0x000A6C91 File Offset: 0x000A4E91
		[ConfigurationProperty("systemzSocketsUserData", IsRequired = false)]
		[Browsable(false)]
		public SystemzSocketsUserData SystemzSocketsUserData
		{
			get
			{
				return (SystemzSocketsUserData)base["systemzSocketsUserData"];
			}
			set
			{
				base["systemzSocketsUserData"] = value;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060031AE RID: 12718 RVA: 0x000A6C9F File Offset: 0x000A4E9F
		// (set) Token: 0x060031AF RID: 12719 RVA: 0x000A6CB1 File Offset: 0x000A4EB1
		[ConfigurationProperty("systemiSocketsUserData", IsRequired = false)]
		[Browsable(false)]
		public SystemiSocketsUserData SystemiSocketsUserData
		{
			get
			{
				return (SystemiSocketsUserData)base["systemiSocketsUserData"];
			}
			set
			{
				base["systemiSocketsUserData"] = value;
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x060031B0 RID: 12720 RVA: 0x000A6CBF File Offset: 0x000A4EBF
		// (set) Token: 0x060031B1 RID: 12721 RVA: 0x000A2E28 File Offset: 0x000A1028
		[ConfigurationProperty("elmLink", IsRequired = false)]
		[Browsable(false)]
		public ElmLink ElmLink
		{
			get
			{
				return (ElmLink)base["elmLink"];
			}
			set
			{
				base["elmLink"] = value;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x060031B2 RID: 12722 RVA: 0x000A6CD1 File Offset: 0x000A4ED1
		// (set) Token: 0x060031B3 RID: 12723 RVA: 0x000A2E68 File Offset: 0x000A1068
		[ConfigurationProperty("elmUserData", IsRequired = false)]
		[Browsable(false)]
		public ElmUserData ElmUserData
		{
			get
			{
				return (ElmUserData)base["elmUserData"];
			}
			set
			{
				base["elmUserData"] = value;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x060031B4 RID: 12724 RVA: 0x000A6CE3 File Offset: 0x000A4EE3
		// (set) Token: 0x060031B5 RID: 12725 RVA: 0x000A2E48 File Offset: 0x000A1048
		[ConfigurationProperty("trmLink", IsRequired = false)]
		[Browsable(false)]
		public TrmLink TrmLink
		{
			get
			{
				return (TrmLink)base["trmLink"];
			}
			set
			{
				base["trmLink"] = value;
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x060031B6 RID: 12726 RVA: 0x000A6CF5 File Offset: 0x000A4EF5
		// (set) Token: 0x060031B7 RID: 12727 RVA: 0x000A6D07 File Offset: 0x000A4F07
		[ConfigurationProperty("trmUserData", IsRequired = false)]
		[Browsable(false)]
		public TrmUserData TrmUserData
		{
			get
			{
				return (TrmUserData)base["trmUserData"];
			}
			set
			{
				base["trmUserData"] = value;
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x060031B8 RID: 12728 RVA: 0x000A6D15 File Offset: 0x000A4F15
		// (set) Token: 0x060031B9 RID: 12729 RVA: 0x000A6D27 File Offset: 0x000A4F27
		[ConfigurationProperty("httpLink", IsRequired = false)]
		[Browsable(false)]
		public HttpLink HttpLink
		{
			get
			{
				return (HttpLink)base["httpLink"];
			}
			set
			{
				base["httpLink"] = value;
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x060031BA RID: 12730 RVA: 0x000A6D35 File Offset: 0x000A4F35
		// (set) Token: 0x060031BB RID: 12731 RVA: 0x000A6D47 File Offset: 0x000A4F47
		[ConfigurationProperty("httpUserData", IsRequired = false)]
		[Browsable(false)]
		public HttpUserData HttpUserData
		{
			get
			{
				return (HttpUserData)base["httpUserData"];
			}
			set
			{
				base["httpUserData"] = value;
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000A6D55 File Offset: 0x000A4F55
		// (set) Token: 0x060031BD RID: 12733 RVA: 0x000A6D67 File Offset: 0x000A4F67
		[ConfigurationProperty("distributedProgramCall", IsRequired = false)]
		[Browsable(false)]
		public DistributedProgramCall DistributedProgramCall
		{
			get
			{
				return (DistributedProgramCall)base["distributedProgramCall"];
			}
			set
			{
				base["distributedProgramCall"] = value;
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000A6D75 File Offset: 0x000A4F75
		// (set) Token: 0x060031BF RID: 12735 RVA: 0x000A6D87 File Offset: 0x000A4F87
		[ConfigurationProperty("imsConnect", IsRequired = false)]
		[Browsable(false)]
		public ImsConnect ImsConnect
		{
			get
			{
				return (ImsConnect)base["imsConnect"];
			}
			set
			{
				base["imsConnect"] = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x060031C0 RID: 12736 RVA: 0x000A6D95 File Offset: 0x000A4F95
		// (set) Token: 0x060031C1 RID: 12737 RVA: 0x000A2E88 File Offset: 0x000A1088
		[ConfigurationProperty("snaLink", IsRequired = false)]
		[Browsable(false)]
		public SnaLink SnaLink
		{
			get
			{
				return (SnaLink)base["snaLink"];
			}
			set
			{
				base["snaLink"] = value;
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x060031C2 RID: 12738 RVA: 0x000A6DA7 File Offset: 0x000A4FA7
		// (set) Token: 0x060031C3 RID: 12739 RVA: 0x000A2EC8 File Offset: 0x000A10C8
		[ConfigurationProperty("snaUserData", IsRequired = false)]
		[Browsable(false)]
		public SnaUserData SnaUserData
		{
			get
			{
				return (SnaUserData)base["snaUserData"];
			}
			set
			{
				base["snaUserData"] = value;
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x060031C4 RID: 12740 RVA: 0x000A6DB9 File Offset: 0x000A4FB9
		// (set) Token: 0x060031C5 RID: 12741 RVA: 0x000A6DCB File Offset: 0x000A4FCB
		[ConfigurationProperty("imsLu62", IsRequired = false)]
		[Browsable(false)]
		public ImsLu62 ImsLu62
		{
			get
			{
				return (ImsLu62)base["imsLu62"];
			}
			set
			{
				base["imsLu62"] = value;
			}
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x000A6DD9 File Offset: 0x000A4FD9
		public object GetElementKey()
		{
			return this.Name;
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000A6DE4 File Offset: 0x000A4FE4
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

		// Token: 0x060031D1 RID: 12753 RVA: 0x000A6EE8 File Offset: 0x000A50E8
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

		// Token: 0x060031D2 RID: 12754 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

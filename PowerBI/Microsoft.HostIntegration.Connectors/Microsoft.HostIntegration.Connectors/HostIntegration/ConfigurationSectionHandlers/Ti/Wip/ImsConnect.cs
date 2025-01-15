using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200058F RID: 1423
	public class ImsConnect : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06003112 RID: 12562 RVA: 0x000A4720 File Offset: 0x000A2920
		// (set) Token: 0x06003113 RID: 12563 RVA: 0x000A4728 File Offset: 0x000A2928
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

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x000A33EA File Offset: 0x000A15EA
		[ConfigurationProperty("ipAddress", IsRequired = true)]
		[ReadOnly(false)]
		[Browsable(false)]
		public string IpAddress
		{
			get
			{
				return (string)base["ipAddress"];
			}
			set
			{
				base["ipAddress"] = value;
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x0009B252 File Offset: 0x00099452
		[ConfigurationProperty("ports", IsRequired = true)]
		[ReadOnly(false)]
		[Browsable(false)]
		public string Ports
		{
			get
			{
				return (string)base["ports"];
			}
			set
			{
				base["ports"] = value;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x000A3632 File Offset: 0x000A1832
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x000A3644 File Offset: 0x000A1844
		[ConfigurationProperty("certificateCommonName", IsRequired = false)]
		[ReadOnly(true)]
		[Browsable(false)]
		public string CertificateCommonName
		{
			get
			{
				return (string)base["certificateCommonName"];
			}
			set
			{
				base["certificateCommonName"] = value;
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600311A RID: 12570 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x0600311B RID: 12571 RVA: 0x000A3664 File Offset: 0x000A1864
		[ConfigurationProperty("serverVerificationRequired", IsRequired = false, DefaultValue = false)]
		[ReadOnly(true)]
		[Browsable(false)]
		public bool ServerVerificationRequired
		{
			get
			{
				return (bool)base["serverVerificationRequired"];
			}
			set
			{
				base["serverVerificationRequired"] = value;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x0600311D RID: 12573 RVA: 0x000A4734 File Offset: 0x000A2934
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
		[ReadOnly(false)]
		[Browsable(false)]
		public bool UseSsl
		{
			get
			{
				return (bool)base["useSsl"];
			}
			set
			{
				base["useSsl"] = value;
				ReadOnlyAttribute readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetProperties(base.GetType())["ServerVerificationRequired"].Attributes[typeof(ReadOnlyAttribute)];
				readOnlyAttribute.GetType().GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(readOnlyAttribute, !value);
				readOnlyAttribute = (ReadOnlyAttribute)TypeDescriptor.GetProperties(base.GetType())["CertificateCommonName"].Attributes[typeof(ReadOnlyAttribute)];
				readOnlyAttribute.GetType().GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic).SetValue(readOnlyAttribute, !value);
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600311E RID: 12574 RVA: 0x000A47F2 File Offset: 0x000A29F2
		// (set) Token: 0x0600311F RID: 12575 RVA: 0x000A4804 File Offset: 0x000A2A04
		[ConfigurationProperty("imsSystemId", IsRequired = true, DefaultValue = "IMSID1")]
		[ReadOnly(false)]
		[Browsable(false)]
		public string ImsSystemId
		{
			get
			{
				return (string)base["imsSystemId"];
			}
			set
			{
				base["imsSystemId"] = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000A4812 File Offset: 0x000A2A12
		// (set) Token: 0x06003121 RID: 12577 RVA: 0x000A4824 File Offset: 0x000A2A24
		[ConfigurationProperty("itocExitName", IsRequired = true, DefaultValue = "*IRMREQ*")]
		[ReadOnly(false)]
		[Browsable(false)]
		public string ItocExitName
		{
			get
			{
				return (string)base["itocExitName"];
			}
			set
			{
				base["itocExitName"] = value;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06003122 RID: 12578 RVA: 0x000A4832 File Offset: 0x000A2A32
		// (set) Token: 0x06003123 RID: 12579 RVA: 0x000A4844 File Offset: 0x000A2A44
		[ConfigurationProperty("mfsModName", IsRequired = false, DefaultValue = "DFSMO1")]
		[ReadOnly(false)]
		[Browsable(false)]
		public string MfsModName
		{
			get
			{
				return (string)base["mfsModName"];
			}
			set
			{
				base["mfsModName"] = value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06003124 RID: 12580 RVA: 0x000A4852 File Offset: 0x000A2A52
		// (set) Token: 0x06003125 RID: 12581 RVA: 0x000A4864 File Offset: 0x000A2A64
		[ConfigurationProperty("imsInboundHeaderFormat", IsRequired = true, DefaultValue = "HWSIMSO0")]
		[ReadOnly(false)]
		[Browsable(false)]
		internal string ImsInboundHeaderFormat
		{
			get
			{
				return (string)base["imsInboundHeaderFormat"];
			}
			set
			{
				base["imsInboundHeaderFormat"] = value;
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06003126 RID: 12582 RVA: 0x000A4872 File Offset: 0x000A2A72
		// (set) Token: 0x06003127 RID: 12583 RVA: 0x000A48A6 File Offset: 0x000A2AA6
		[ConfigurationProperty("imsInboundHeaderFormat", IsRequired = true, DefaultValue = "HWSIMSO0")]
		[ReadOnly(false)]
		[Browsable(false)]
		public ImsConnectInboundHeaderFormat InboundHeaderFormat
		{
			get
			{
				if (this.ImsInboundHeaderFormat == "HWSIMSO0")
				{
					return ImsConnectInboundHeaderFormat.HWSIMSO0;
				}
				if (this.ImsInboundHeaderFormat == "HWSIMSO1")
				{
					return ImsConnectInboundHeaderFormat.HWSIMSO1;
				}
				throw new Exception("Invalid ImsInboundHeaderFormat");
			}
			set
			{
				if (value == ImsConnectInboundHeaderFormat.HWSIMSO0)
				{
					this.ImsInboundHeaderFormat = "HWSIMSO0";
					return;
				}
				if (value == ImsConnectInboundHeaderFormat.HWSIMSO1)
				{
					this.ImsInboundHeaderFormat = "HWSIMSO1";
					return;
				}
				throw new Exception("Invalid ImsInboundHeaderFormat");
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06003128 RID: 12584 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x06003129 RID: 12585 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600312A RID: 12586 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x0600312B RID: 12587 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x0600312C RID: 12588 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x0600312D RID: 12589 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x0600312E RID: 12590 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x0600312F RID: 12591 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003130 RID: 12592 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003131 RID: 12593 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003132 RID: 12594 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003133 RID: 12595 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003134 RID: 12596 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06003135 RID: 12597 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06003136 RID: 12598 RVA: 0x000A48D4 File Offset: 0x000A2AD4
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

		// Token: 0x06003137 RID: 12599 RVA: 0x000A49D8 File Offset: 0x000A2BD8
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

		// Token: 0x06003138 RID: 12600 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C55 RID: 7253
		private bool isTypeDefined;
	}
}

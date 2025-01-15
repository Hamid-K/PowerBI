using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200058A RID: 1418
	public class ElmUserData : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A3A RID: 2618
		// (get) Token: 0x0600308E RID: 12430 RVA: 0x000A3CC8 File Offset: 0x000A1EC8
		// (set) Token: 0x0600308F RID: 12431 RVA: 0x000A3CD0 File Offset: 0x000A1ED0
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

		// Token: 0x17000A3B RID: 2619
		// (get) Token: 0x06003090 RID: 12432 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x06003091 RID: 12433 RVA: 0x000A33EA File Offset: 0x000A15EA
		[ConfigurationProperty("ipAddress", IsRequired = true)]
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

		// Token: 0x17000A3C RID: 2620
		// (get) Token: 0x06003092 RID: 12434 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003093 RID: 12435 RVA: 0x0009B252 File Offset: 0x00099452
		[ConfigurationProperty("ports", IsRequired = true)]
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

		// Token: 0x17000A3D RID: 2621
		// (get) Token: 0x06003094 RID: 12436 RVA: 0x000A3632 File Offset: 0x000A1832
		// (set) Token: 0x06003095 RID: 12437 RVA: 0x000A3644 File Offset: 0x000A1844
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

		// Token: 0x17000A3E RID: 2622
		// (get) Token: 0x06003096 RID: 12438 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x06003097 RID: 12439 RVA: 0x000A3664 File Offset: 0x000A1864
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

		// Token: 0x17000A3F RID: 2623
		// (get) Token: 0x06003098 RID: 12440 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x06003099 RID: 12441 RVA: 0x000A3CDC File Offset: 0x000A1EDC
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

		// Token: 0x17000A40 RID: 2624
		// (get) Token: 0x0600309A RID: 12442 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x0600309B RID: 12443 RVA: 0x0009B284 File Offset: 0x00099484
		[ConfigurationProperty("requestHeaderFormat", IsRequired = false, DefaultValue = "Microsoft")]
		[Browsable(false)]
		internal string RequestHeaderFormat
		{
			get
			{
				return (string)base["requestHeaderFormat"];
			}
			set
			{
				base["requestHeaderFormat"] = value;
			}
		}

		// Token: 0x17000A41 RID: 2625
		// (get) Token: 0x0600309C RID: 12444 RVA: 0x000A3D9A File Offset: 0x000A1F9A
		// (set) Token: 0x0600309D RID: 12445 RVA: 0x000A3DCE File Offset: 0x000A1FCE
		[ConfigurationProperty("requestHeaderFormat", IsRequired = false, DefaultValue = TcpCicsRequestHeaderFormat.Microsoft)]
		[Browsable(false)]
		public TcpCicsRequestHeaderFormat TcpCicsRequestHeaderFormat
		{
			get
			{
				if (this.RequestHeaderFormat == "Microsoft")
				{
					return TcpCicsRequestHeaderFormat.Microsoft;
				}
				if (this.RequestHeaderFormat == "IBM supplied exit routine")
				{
					return TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine;
				}
				throw new Exception("Invalid RequestHeaderFormat");
			}
			set
			{
				if (value == TcpCicsRequestHeaderFormat.Microsoft)
				{
					this.RequestHeaderFormat = "Microsoft";
					return;
				}
				if (value == TcpCicsRequestHeaderFormat.IbmSuppliedExitRoutine)
				{
					this.RequestHeaderFormat = "IBM supplied exit routine";
					return;
				}
				throw new Exception("Invalid RequestHeaderFormat");
			}
		}

		// Token: 0x17000A42 RID: 2626
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x0600309F RID: 12447 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x060030A2 RID: 12450 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x060030A3 RID: 12451 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060030A4 RID: 12452 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060030A5 RID: 12453 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060030A6 RID: 12454 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060030A7 RID: 12455 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060030A8 RID: 12456 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060030A9 RID: 12457 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060030AA RID: 12458 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060030AB RID: 12459 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060030AC RID: 12460 RVA: 0x000A3DFC File Offset: 0x000A1FFC
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

		// Token: 0x060030AD RID: 12461 RVA: 0x000A3F00 File Offset: 0x000A2100
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

		// Token: 0x060030AE RID: 12462 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C4E RID: 7246
		private bool isTypeDefined;
	}
}

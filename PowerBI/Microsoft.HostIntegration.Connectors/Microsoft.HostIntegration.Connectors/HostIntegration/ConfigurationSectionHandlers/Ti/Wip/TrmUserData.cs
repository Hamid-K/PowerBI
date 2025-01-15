using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005A5 RID: 1445
	public class TrmUserData : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x000A81A8 File Offset: 0x000A63A8
		// (set) Token: 0x06003294 RID: 12948 RVA: 0x000A81B0 File Offset: 0x000A63B0
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

		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x06003295 RID: 12949 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x06003296 RID: 12950 RVA: 0x000A33EA File Offset: 0x000A15EA
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

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x06003297 RID: 12951 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003298 RID: 12952 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06003299 RID: 12953 RVA: 0x000A3632 File Offset: 0x000A1832
		// (set) Token: 0x0600329A RID: 12954 RVA: 0x000A3644 File Offset: 0x000A1844
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

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600329B RID: 12955 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x0600329C RID: 12956 RVA: 0x000A3664 File Offset: 0x000A1864
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

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x0600329D RID: 12957 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x0600329E RID: 12958 RVA: 0x000A81BC File Offset: 0x000A63BC
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

		// Token: 0x17000AC2 RID: 2754
		// (get) Token: 0x0600329F RID: 12959 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x060032A0 RID: 12960 RVA: 0x0009B284 File Offset: 0x00099484
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

		// Token: 0x17000AC3 RID: 2755
		// (get) Token: 0x060032A1 RID: 12961 RVA: 0x000A827A File Offset: 0x000A647A
		// (set) Token: 0x060032A2 RID: 12962 RVA: 0x000A82AE File Offset: 0x000A64AE
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

		// Token: 0x17000AC4 RID: 2756
		// (get) Token: 0x060032A3 RID: 12963 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x060032A4 RID: 12964 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000AC5 RID: 2757
		// (get) Token: 0x060032A5 RID: 12965 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x060032A6 RID: 12966 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x060032A7 RID: 12967 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x060032A8 RID: 12968 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060032A9 RID: 12969 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060032AA RID: 12970 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060032AB RID: 12971 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060032AC RID: 12972 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060032AD RID: 12973 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060032AF RID: 12975 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060032B0 RID: 12976 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x000A82DC File Offset: 0x000A64DC
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

		// Token: 0x060032B2 RID: 12978 RVA: 0x000A83E0 File Offset: 0x000A65E0
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

		// Token: 0x060032B3 RID: 12979 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C75 RID: 7285
		private bool isTypeDefined;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000589 RID: 1417
	public class ElmLink : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x0600306C RID: 12396 RVA: 0x000A398C File Offset: 0x000A1B8C
		// (set) Token: 0x0600306D RID: 12397 RVA: 0x000A3994 File Offset: 0x000A1B94
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

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x0600306E RID: 12398 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x0600306F RID: 12399 RVA: 0x000A33EA File Offset: 0x000A15EA
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

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x06003070 RID: 12400 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003071 RID: 12401 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x06003072 RID: 12402 RVA: 0x000A3632 File Offset: 0x000A1832
		// (set) Token: 0x06003073 RID: 12403 RVA: 0x000A3644 File Offset: 0x000A1844
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

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x06003074 RID: 12404 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x06003075 RID: 12405 RVA: 0x000A3664 File Offset: 0x000A1864
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

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x06003076 RID: 12406 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x06003077 RID: 12407 RVA: 0x000A39A0 File Offset: 0x000A1BA0
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

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x06003078 RID: 12408 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x06003079 RID: 12409 RVA: 0x0009B284 File Offset: 0x00099484
		[ConfigurationProperty("requestHeaderFormat", IsRequired = true, DefaultValue = "Microsoft")]
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

		// Token: 0x17000A37 RID: 2615
		// (get) Token: 0x0600307A RID: 12410 RVA: 0x000A3A5E File Offset: 0x000A1C5E
		// (set) Token: 0x0600307B RID: 12411 RVA: 0x000A3A92 File Offset: 0x000A1C92
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

		// Token: 0x17000A38 RID: 2616
		// (get) Token: 0x0600307C RID: 12412 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x0600307D RID: 12413 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A39 RID: 2617
		// (get) Token: 0x0600307E RID: 12414 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x0600307F RID: 12415 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x06003080 RID: 12416 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x000A3AC0 File Offset: 0x000A1CC0
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

		// Token: 0x0600308B RID: 12427 RVA: 0x000A3BC4 File Offset: 0x000A1DC4
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

		// Token: 0x0600308C RID: 12428 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C4D RID: 7245
		private bool isTypeDefined;
	}
}

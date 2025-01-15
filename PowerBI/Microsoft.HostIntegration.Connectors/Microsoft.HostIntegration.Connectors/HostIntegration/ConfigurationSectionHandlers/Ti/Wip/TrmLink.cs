using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005A4 RID: 1444
	public class TrmLink : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x000A7E6C File Offset: 0x000A606C
		// (set) Token: 0x06003272 RID: 12914 RVA: 0x000A7E74 File Offset: 0x000A6074
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

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x06003273 RID: 12915 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x06003274 RID: 12916 RVA: 0x000A33EA File Offset: 0x000A15EA
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

		// Token: 0x17000AB4 RID: 2740
		// (get) Token: 0x06003275 RID: 12917 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x06003276 RID: 12918 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000AB5 RID: 2741
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000A3632 File Offset: 0x000A1832
		// (set) Token: 0x06003278 RID: 12920 RVA: 0x000A3644 File Offset: 0x000A1844
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

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06003279 RID: 12921 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x0600327A RID: 12922 RVA: 0x000A3664 File Offset: 0x000A1864
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

		// Token: 0x17000AB7 RID: 2743
		// (get) Token: 0x0600327B RID: 12923 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x0600327C RID: 12924 RVA: 0x000A7E80 File Offset: 0x000A6080
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

		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x0600327D RID: 12925 RVA: 0x0009B272 File Offset: 0x00099472
		// (set) Token: 0x0600327E RID: 12926 RVA: 0x0009B284 File Offset: 0x00099484
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

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000A7F3E File Offset: 0x000A613E
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000A7F72 File Offset: 0x000A6172
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

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06003283 RID: 12931 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x06003284 RID: 12932 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x06003285 RID: 12933 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003289 RID: 12937 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600328A RID: 12938 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600328B RID: 12939 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600328C RID: 12940 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600328D RID: 12941 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600328E RID: 12942 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600328F RID: 12943 RVA: 0x000A7FA0 File Offset: 0x000A61A0
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

		// Token: 0x06003290 RID: 12944 RVA: 0x000A80A4 File Offset: 0x000A62A4
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

		// Token: 0x06003291 RID: 12945 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C74 RID: 7284
		private bool isTypeDefined;
	}
}

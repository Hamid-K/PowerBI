using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200058C RID: 1420
	public class HttpUserData : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000A42A4 File Offset: 0x000A24A4
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x000A42AC File Offset: 0x000A24AC
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

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x000A33EA File Offset: 0x000A15EA
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

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x060030D8 RID: 12504 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x060030D9 RID: 12505 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x060030DA RID: 12506 RVA: 0x000A4015 File Offset: 0x000A2215
		// (set) Token: 0x060030DB RID: 12507 RVA: 0x000A4027 File Offset: 0x000A2227
		[ConfigurationProperty("aliasTransactionId", IsRequired = false, DefaultValue = "MSTX")]
		[Browsable(false)]
		public string AliasTransactionId
		{
			get
			{
				return (string)base["aliasTransactionId"];
			}
			set
			{
				base["aliasTransactionId"] = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x060030DC RID: 12508 RVA: 0x000A4035 File Offset: 0x000A2235
		// (set) Token: 0x060030DD RID: 12509 RVA: 0x000A4047 File Offset: 0x000A2247
		[ConfigurationProperty("allowRedirects", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		public bool AllowRedirects
		{
			get
			{
				return (bool)base["allowRedirects"];
			}
			set
			{
				base["allowRedirects"] = value;
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x060030DE RID: 12510 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x060030DF RID: 12511 RVA: 0x00097370 File Offset: 0x00095570
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
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
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x060030E0 RID: 12512 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x060030E1 RID: 12513 RVA: 0x000A3664 File Offset: 0x000A1864
		[ConfigurationProperty("serverVerificationRequired", IsRequired = false, DefaultValue = false)]
		[Browsable(false)]
		[ReadOnly(true)]
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

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x060030E2 RID: 12514 RVA: 0x000A405A File Offset: 0x000A225A
		// (set) Token: 0x060030E3 RID: 12515 RVA: 0x000A406C File Offset: 0x000A226C
		[ConfigurationProperty("userAgent", IsRequired = false, DefaultValue = "")]
		[Browsable(false)]
		public string UserAgent
		{
			get
			{
				return (string)base["userAgent"];
			}
			set
			{
				base["userAgent"] = value;
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x060030E4 RID: 12516 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x060030E5 RID: 12517 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x060030E6 RID: 12518 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x060030E7 RID: 12519 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x060030E8 RID: 12520 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x060030E9 RID: 12521 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060030EA RID: 12522 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060030EB RID: 12523 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060030EC RID: 12524 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060030ED RID: 12525 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060030EE RID: 12526 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060030EF RID: 12527 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060030F0 RID: 12528 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060030F1 RID: 12529 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060030F2 RID: 12530 RVA: 0x000A42B8 File Offset: 0x000A24B8
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

		// Token: 0x060030F3 RID: 12531 RVA: 0x000A43BC File Offset: 0x000A25BC
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

		// Token: 0x060030F4 RID: 12532 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C50 RID: 7248
		private bool isTypeDefined;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x0200058B RID: 1419
	public class HttpLink : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x060030B0 RID: 12464 RVA: 0x000A4004 File Offset: 0x000A2204
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x000A400C File Offset: 0x000A220C
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

		// Token: 0x17000A45 RID: 2629
		// (get) Token: 0x060030B2 RID: 12466 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x060030B3 RID: 12467 RVA: 0x000A33EA File Offset: 0x000A15EA
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

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060030B4 RID: 12468 RVA: 0x0009B240 File Offset: 0x00099440
		// (set) Token: 0x060030B5 RID: 12469 RVA: 0x0009B252 File Offset: 0x00099452
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

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060030B6 RID: 12470 RVA: 0x000A4015 File Offset: 0x000A2215
		// (set) Token: 0x060030B7 RID: 12471 RVA: 0x000A4027 File Offset: 0x000A2227
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

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x060030B8 RID: 12472 RVA: 0x000A4035 File Offset: 0x000A2235
		// (set) Token: 0x060030B9 RID: 12473 RVA: 0x000A4047 File Offset: 0x000A2247
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

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x060030BA RID: 12474 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x060030BB RID: 12475 RVA: 0x00097370 File Offset: 0x00095570
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

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x060030BC RID: 12476 RVA: 0x000A3652 File Offset: 0x000A1852
		// (set) Token: 0x060030BD RID: 12477 RVA: 0x000A3664 File Offset: 0x000A1864
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

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x060030BE RID: 12478 RVA: 0x000A405A File Offset: 0x000A225A
		// (set) Token: 0x060030BF RID: 12479 RVA: 0x000A406C File Offset: 0x000A226C
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

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x060030C0 RID: 12480 RVA: 0x000A407A File Offset: 0x000A227A
		// (set) Token: 0x060030C1 RID: 12481 RVA: 0x000A408C File Offset: 0x000A228C
		[ConfigurationProperty("mirrorProgram", IsRequired = false, DefaultValue = "MSHMIRS")]
		[Browsable(false)]
		public string MirrorProgram
		{
			get
			{
				return (string)base["mirrorProgram"];
			}
			set
			{
				base["mirrorProgram"] = value;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x060030C2 RID: 12482 RVA: 0x000A3736 File Offset: 0x000A1936
		// (set) Token: 0x060030C3 RID: 12483 RVA: 0x000A3748 File Offset: 0x000A1948
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

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x060030C4 RID: 12484 RVA: 0x000A375B File Offset: 0x000A195B
		// (set) Token: 0x060030C5 RID: 12485 RVA: 0x000A376D File Offset: 0x000A196D
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

		// Token: 0x060030C6 RID: 12486 RVA: 0x000A377B File Offset: 0x000A197B
		public override string ToString()
		{
			return "";
		}

		// Token: 0x060030C7 RID: 12487 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x060030C8 RID: 12488 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x060030C9 RID: 12489 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x060030CA RID: 12490 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x060030CB RID: 12491 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x060030CC RID: 12492 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x060030CD RID: 12493 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x060030CE RID: 12494 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x060030CF RID: 12495 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x060030D0 RID: 12496 RVA: 0x000A409C File Offset: 0x000A229C
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

		// Token: 0x060030D1 RID: 12497 RVA: 0x000A41A0 File Offset: 0x000A23A0
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

		// Token: 0x060030D2 RID: 12498 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C4F RID: 7247
		private bool isTypeDefined;
	}
}

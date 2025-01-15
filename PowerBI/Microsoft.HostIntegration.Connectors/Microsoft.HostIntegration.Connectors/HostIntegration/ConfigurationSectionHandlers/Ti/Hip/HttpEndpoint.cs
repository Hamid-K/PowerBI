using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000566 RID: 1382
	public class HttpEndpoint : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x06002ED0 RID: 11984 RVA: 0x000A15E9 File Offset: 0x0009F7E9
		// (set) Token: 0x06002ED1 RID: 11985 RVA: 0x000A15FB File Offset: 0x0009F7FB
		[Description("TBD. Required.")]
		[Category("HTTP")]
		[ConfigurationProperty("webSite", IsRequired = true)]
		[DisplayName("Web Site")]
		public string WebSite
		{
			get
			{
				return (string)base["webSite"];
			}
			set
			{
				base["webSite"] = value;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x06002ED2 RID: 11986 RVA: 0x00097339 File Offset: 0x00095539
		// (set) Token: 0x06002ED3 RID: 11987 RVA: 0x0009734B File Offset: 0x0009554B
		[Description("TBD.")]
		[Category("HTTP")]
		[ConfigurationProperty("port", IsRequired = true)]
		[DisplayName("Port")]
		public int Port
		{
			get
			{
				return (int)base["port"];
			}
			set
			{
				base["port"] = value;
			}
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x0009735E File Offset: 0x0009555E
		// (set) Token: 0x06002ED5 RID: 11989 RVA: 0x00097370 File Offset: 0x00095570
		[Description("TBD.")]
		[Category("HTTP")]
		[ConfigurationProperty("useSsl", IsRequired = false, DefaultValue = false)]
		[DisplayName("Use SSL")]
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

		// Token: 0x06002ED6 RID: 11990 RVA: 0x000A160C File Offset: 0x0009F80C
		public object GetElementKey()
		{
			return this.WebSite + this.Port.ToString();
		}

		// Token: 0x06002ED7 RID: 11991 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002EDA RID: 11994 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002EDC RID: 11996 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002EDD RID: 11997 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002EDE RID: 11998 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002EDF RID: 11999 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002EE0 RID: 12000 RVA: 0x000A1634 File Offset: 0x0009F834
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "HTTP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002EE1 RID: 12001 RVA: 0x000A16B4 File Offset: 0x0009F8B4
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "HTTP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

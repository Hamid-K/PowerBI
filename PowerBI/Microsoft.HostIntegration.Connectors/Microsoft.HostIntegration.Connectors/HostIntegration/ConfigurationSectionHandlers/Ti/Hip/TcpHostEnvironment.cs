using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000584 RID: 1412
	public class TcpHostEnvironment : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x0600301B RID: 12315 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x0600301C RID: 12316 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[TypeConverter(typeof(TcpHostEnvironmentNameValidation))]
		[Description("A unique name that identifies this Host Environment.")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
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

		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x0600301D RID: 12317 RVA: 0x000A33D8 File Offset: 0x000A15D8
		// (set) Token: 0x0600301E RID: 12318 RVA: 0x000A33EA File Offset: 0x000A15EA
		[Description("IpAddress identifies a specific host system that is allowed to interact with the TI HIP Runtime using the TCP/IP protocol.")]
		[Category("TCP")]
		[ConfigurationProperty("ipAddress", IsRequired = true)]
		[DisplayName("IP Address")]
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

		// Token: 0x17000A1A RID: 2586
		// (get) Token: 0x0600301F RID: 12319 RVA: 0x000A313F File Offset: 0x000A133F
		// (set) Token: 0x06003020 RID: 12320 RVA: 0x000A3151 File Offset: 0x000A1351
		[Description("The code page used to transform the incoming and outgoing data to a form that can be used by the host application program. If this property is not specified, the default code page for the locale will be used.")]
		[Category("Conversion")]
		[ConfigurationProperty("codePage", IsRequired = true, DefaultValue = 37)]
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

		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003021 RID: 12321 RVA: 0x000A3164 File Offset: 0x000A1364
		// (set) Token: 0x06003022 RID: 12322 RVA: 0x000A3176 File Offset: 0x000A1376
		[Description("The type of data conversion that is performed on the incoming data. Different types of mainframes use different encoding schemas as presented by the available options.")]
		[Category("Conversion")]
		[ConfigurationProperty("dataConversion", IsRequired = true, DefaultValue = "OS390")]
		[DisplayName("Data Conversion")]
		public PrimitiveConverterTypes DataConversion
		{
			get
			{
				return (PrimitiveConverterTypes)base["dataConversion"];
			}
			set
			{
				base["dataConversion"] = value;
			}
		}

		// Token: 0x17000A1C RID: 2588
		// (get) Token: 0x06003023 RID: 12323 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x06003024 RID: 12324 RVA: 0x00019FBE File Offset: 0x000181BE
		[Description("Type the number of seconds. The time-out values are used by the HIP runtime environment when it communicates with the host environment. The time-out values are used on transport-specific APIs to terminate the receive API function when no host data or acknowledgement is received in the specified amount of time. The number of seconds can be a maximum of 3600 and a minimum of 0.")]
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

		// Token: 0x06003025 RID: 12325 RVA: 0x000A33F8 File Offset: 0x000A15F8
		public object GetElementKey()
		{
			return this.Name;
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600302D RID: 12333 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x000A3400 File Offset: 0x000A1600
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06003030 RID: 12336 RVA: 0x000A34A4 File Offset: 0x000A16A4
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06003031 RID: 12337 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

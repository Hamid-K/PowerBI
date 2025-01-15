using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057F RID: 1407
	public class SnaHostEnvironment : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002FE0 RID: 12256 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002FE1 RID: 12257 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[TypeConverter(typeof(SnaHostEnvironmentNameValidation))]
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

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000A311F File Offset: 0x000A131F
		// (set) Token: 0x06002FE3 RID: 12259 RVA: 0x000A3131 File Offset: 0x000A1331
		[Description("RemoteLuName identifies a specific host system that is allowed to interact with the TI HIP Runtime using the SNA protocol. The Remote LU Name can be determined using SNA Manager.")]
		[Category("SNA")]
		[ConfigurationProperty("remoteLuName", IsRequired = true)]
		[DisplayName("Remote LU Name")]
		[StringValidator(MaxLength = 8)]
		public string RemoteLuName
		{
			get
			{
				return (string)base["remoteLuName"];
			}
			set
			{
				base["remoteLuName"] = value;
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000A313F File Offset: 0x000A133F
		// (set) Token: 0x06002FE5 RID: 12261 RVA: 0x000A3151 File Offset: 0x000A1351
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

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000A3164 File Offset: 0x000A1364
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000A3176 File Offset: 0x000A1376
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

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x00019FAC File Offset: 0x000181AC
		// (set) Token: 0x06002FE9 RID: 12265 RVA: 0x00019FBE File Offset: 0x000181BE
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

		// Token: 0x06002FEA RID: 12266 RVA: 0x000A3189 File Offset: 0x000A1389
		public object GetElementKey()
		{
			return this.Name;
		}

		// Token: 0x06002FEB RID: 12267 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002FEC RID: 12268 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002FED RID: 12269 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000A3194 File Offset: 0x000A1394
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "TCP" || propertyDescriptor.Category == "SNA")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000A3248 File Offset: 0x000A1448
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "TCP" || propertyDescriptor.Category == "SNA")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000568 RID: 1384
	public class HipObject : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x170009CD RID: 2509
		// (get) Token: 0x06002EF0 RID: 12016 RVA: 0x000A178C File Offset: 0x0009F98C
		// (set) Token: 0x06002EF1 RID: 12017 RVA: 0x000A179E File Offset: 0x0009F99E
		[Description("The name of the .NET Assembly that contains the conversion metadata associated with the implementingClass. The location of the metadata is defined by the attribute assemblyPath on the service element.")]
		[Category("Meta Data")]
		[ConfigurationProperty("metaDataAssembly", IsRequired = true)]
		[DisplayName("Meta Data Assembly")]
		[ReadOnly(true)]
		public string MetaDataAssembly
		{
			get
			{
				return (string)base["metaDataAssembly"];
			}
			set
			{
				base["metaDataAssembly"] = value;
			}
		}

		// Token: 0x170009CE RID: 2510
		// (get) Token: 0x06002EF2 RID: 12018 RVA: 0x000A17AC File Offset: 0x0009F9AC
		// (set) Token: 0x06002EF3 RID: 12019 RVA: 0x000A17BE File Offset: 0x0009F9BE
		[Description("interface represents the .NET interface definition that is used to implement the HIP server object. The interface definition is constructed with the help of the TI Designer Visual Studio tool.")]
		[Category("Meta Data")]
		[ConfigurationProperty("metaDataInterface", IsRequired = true)]
		[DisplayName("Meta Data Interface")]
		[ReadOnly(true)]
		public string MetaDataInterface
		{
			get
			{
				return (string)base["metaDataInterface"];
			}
			set
			{
				base["metaDataInterface"] = value;
			}
		}

		// Token: 0x170009CF RID: 2511
		// (get) Token: 0x06002EF4 RID: 12020 RVA: 0x000A17CC File Offset: 0x0009F9CC
		// (set) Token: 0x06002EF5 RID: 12021 RVA: 0x000A17DE File Offset: 0x0009F9DE
		[Description("implementingClass is the namespace.classid that implements the interface defined using TI Designer.")]
		[Category("Implementation")]
		[ConfigurationProperty("implementingClass", IsRequired = false)]
		[DisplayName("Implementing Class")]
		[ReadOnly(true)]
		public string ImplementingClass
		{
			get
			{
				return (string)base["implementingClass"];
			}
			set
			{
				base["implementingClass"] = value;
			}
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000A17EC File Offset: 0x0009F9EC
		// (set) Token: 0x06002EF7 RID: 12023 RVA: 0x000A17FE File Offset: 0x0009F9FE
		[Description("The name of the .NET Assembly that contains the implementation of the class defined in implementingClass. The location of the assembly is defined by the attribute assemblyPath on the service element.")]
		[Category("Implementation")]
		[ConfigurationProperty("implementingAssembly", IsRequired = false)]
		[DisplayName("Implementing Assembly")]
		[ReadOnly(true)]
		public string ImplementingAssembly
		{
			get
			{
				return (string)base["implementingAssembly"];
			}
			set
			{
				base["implementingAssembly"] = value;
			}
		}

		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06002EF8 RID: 12024 RVA: 0x000A180C File Offset: 0x0009FA0C
		// (set) Token: 0x06002EF9 RID: 12025 RVA: 0x000A181E File Offset: 0x0009FA1E
		[Description("TBD.")]
		[Category("WCF")]
		[ConfigurationProperty("wcfServiceUrl", IsRequired = false)]
		[DisplayName("WCF Service URL")]
		[ReadOnly(false)]
		public string WcfServiceUrl
		{
			get
			{
				return (string)base["wcfServiceUrl"];
			}
			set
			{
				base["wcfServiceUrl"] = value;
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x000A182C File Offset: 0x0009FA2C
		public object GetElementKey()
		{
			return this.MetaDataInterface;
		}

		// Token: 0x06002EFB RID: 12027 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002EFC RID: 12028 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002EFD RID: 12029 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002EFE RID: 12030 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002EFF RID: 12031 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002F00 RID: 12032 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x000A1834 File Offset: 0x0009FA34
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Meta Data" || propertyDescriptor.Category == "WCF" || propertyDescriptor.Category == "Implementation")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x000A18D8 File Offset: 0x0009FAD8
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Meta Data" || propertyDescriptor.Category == "WCF" || propertyDescriptor.Category == "Implementation")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

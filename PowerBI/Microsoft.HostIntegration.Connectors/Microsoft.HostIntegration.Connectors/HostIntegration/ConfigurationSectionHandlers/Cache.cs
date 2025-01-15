using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers
{
	// Token: 0x0200050E RID: 1294
	public class Cache : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x170008A1 RID: 2209
		// (get) Token: 0x06002BC3 RID: 11203 RVA: 0x00096A10 File Offset: 0x00094C10
		// (set) Token: 0x06002BC4 RID: 11204 RVA: 0x00096A22 File Offset: 0x00094C22
		[Description("An identifier that defines the actual name of the AppFabric Cache.")]
		[Category("General")]
		[ConfigurationProperty("cacheName", IsRequired = true, DefaultValue = "TICache")]
		[DisplayName("Cache Name")]
		public string CacheName
		{
			get
			{
				return (string)base["cacheName"];
			}
			set
			{
				base["cacheName"] = value;
			}
		}

		// Token: 0x170008A2 RID: 2210
		// (get) Token: 0x06002BC5 RID: 11205 RVA: 0x00020600 File Offset: 0x0001E800
		// (set) Token: 0x06002BC6 RID: 11206 RVA: 0x00020612 File Offset: 0x0001E812
		[Description("An identifier that defines the cached configuration information.")]
		[Category("General")]
		[ConfigurationProperty("key", IsRequired = true, DefaultValue = "TICacheKey")]
		[DisplayName("Key")]
		public string Key
		{
			get
			{
				return (string)base["key"];
			}
			set
			{
				base["key"] = value;
			}
		}

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x06002BC7 RID: 11207 RVA: 0x00096A30 File Offset: 0x00094C30
		// (set) Token: 0x06002BC8 RID: 11208 RVA: 0x00096A42 File Offset: 0x00094C42
		[Description("An identifier that defines where in the cache the configuration object resides.")]
		[Category("General")]
		[ConfigurationProperty("region", IsRequired = true, DefaultValue = "HostIntegrationServerCacheRegionTransactionIntegrator")]
		[DisplayName("Region")]
		public string Region
		{
			get
			{
				return (string)base["region"];
			}
			set
			{
				base["region"] = value;
			}
		}

		// Token: 0x06002BC9 RID: 11209 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002BCA RID: 11210 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002BCB RID: 11211 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002BCC RID: 11212 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002BCD RID: 11213 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002BCE RID: 11214 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002BCF RID: 11215 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002BD0 RID: 11216 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002BD1 RID: 11217 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002BD2 RID: 11218 RVA: 0x00096A50 File Offset: 0x00094C50
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002BD3 RID: 11219 RVA: 0x00096AD0 File Offset: 0x00094CD0
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002BD4 RID: 11220 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

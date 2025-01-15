using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Drawing.Design;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x0200057B RID: 1403
	public class Service : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x170009F3 RID: 2547
		// (get) Token: 0x06002FA6 RID: 12198 RVA: 0x000A2C80 File Offset: 0x000A0E80
		public ProgrammingModel ProgrammingModel
		{
			get
			{
				if (this.Http != null && (this.Http.ElementInformation.IsPresent || this.Http.IsTypeDefined))
				{
					return ProgrammingModel.Http;
				}
				if (this.ElmLink != null && (this.ElmLink.ElementInformation.IsPresent || this.ElmLink.IsTypeDefined))
				{
					return ProgrammingModel.ElmLink;
				}
				if (this.ElmUserData != null && (this.ElmUserData.ElementInformation.IsPresent || this.ElmUserData.IsTypeDefined))
				{
					return ProgrammingModel.ElmUserData;
				}
				if (this.TrmLink != null && (this.TrmLink.ElementInformation.IsPresent || this.TrmLink.IsTypeDefined))
				{
					return ProgrammingModel.TrmLink;
				}
				if (this.SnaLink != null && (this.SnaLink.ElementInformation.IsPresent || this.SnaLink.IsTypeDefined))
				{
					return ProgrammingModel.SnaLink;
				}
				if (this.SnaUserData != null && (this.SnaUserData.ElementInformation.IsPresent || this.SnaUserData.IsTypeDefined))
				{
					return ProgrammingModel.SnaUserData;
				}
				if (this.SnaEndpoint != null && (this.SnaEndpoint.ElementInformation.IsPresent || this.SnaEndpoint.IsTypeDefined))
				{
					return ProgrammingModel.SnaEndpoint;
				}
				throw new Exception("Programming Model is invalid IsPresent and IsTypeDefined are both false for all programming models");
			}
		}

		// Token: 0x170009F4 RID: 2548
		// (get) Token: 0x06002FA7 RID: 12199 RVA: 0x000A2DB6 File Offset: 0x000A0FB6
		// (set) Token: 0x06002FA8 RID: 12200 RVA: 0x000A2DC8 File Offset: 0x000A0FC8
		[TypeConverter(typeof(SericeNameValidation))]
		[Description("The name of the Windows service. The name can be a maximum of 25 alpha-numeric characters. The first character in the name must be a letter, and the name cannot contain any embedded spaces. Required.")]
		[Category("Service")]
		[ConfigurationProperty("serviceName", IsRequired = true)]
		[DisplayName("Service Name")]
		public string ServiceName
		{
			get
			{
				return (string)base["serviceName"];
			}
			set
			{
				base["serviceName"] = value;
			}
		}

		// Token: 0x170009F5 RID: 2549
		// (get) Token: 0x06002FA9 RID: 12201 RVA: 0x000A2DD6 File Offset: 0x000A0FD6
		// (set) Token: 0x06002FAA RID: 12202 RVA: 0x000A2DE8 File Offset: 0x000A0FE8
		[ConfigurationProperty("assemblyPath", IsRequired = true, DefaultValue = "%SNARoot%\\HIP Implementing Assemblies")]
		[Description("The location where the HIP .NET Assemblies are located.")]
		[Category("Service")]
		[DisplayName("Assembly Path")]
		[Editor(typeof(FileNameEditor), typeof(UITypeEditor))]
		public string AssemblyPath
		{
			get
			{
				return (string)base["assemblyPath"];
			}
			set
			{
				base["assemblyPath"] = value;
			}
		}

		// Token: 0x170009F6 RID: 2550
		// (get) Token: 0x06002FAB RID: 12203 RVA: 0x000A2DF6 File Offset: 0x000A0FF6
		// (set) Token: 0x06002FAC RID: 12204 RVA: 0x000A2E08 File Offset: 0x000A1008
		[ConfigurationProperty("http")]
		[Browsable(false)]
		public Http Http
		{
			get
			{
				return (Http)base["http"];
			}
			set
			{
				base["http"] = value;
			}
		}

		// Token: 0x170009F7 RID: 2551
		// (get) Token: 0x06002FAD RID: 12205 RVA: 0x000A2E16 File Offset: 0x000A1016
		// (set) Token: 0x06002FAE RID: 12206 RVA: 0x000A2E28 File Offset: 0x000A1028
		[ConfigurationProperty("elmLink")]
		[Browsable(false)]
		public ElmLink ElmLink
		{
			get
			{
				return (ElmLink)base["elmLink"];
			}
			set
			{
				base["elmLink"] = value;
			}
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002FAF RID: 12207 RVA: 0x000A2E36 File Offset: 0x000A1036
		// (set) Token: 0x06002FB0 RID: 12208 RVA: 0x000A2E48 File Offset: 0x000A1048
		[ConfigurationProperty("trmLink")]
		[Browsable(false)]
		public TrmLink TrmLink
		{
			get
			{
				return (TrmLink)base["trmLink"];
			}
			set
			{
				base["trmLink"] = value;
			}
		}

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000A2E56 File Offset: 0x000A1056
		// (set) Token: 0x06002FB2 RID: 12210 RVA: 0x000A2E68 File Offset: 0x000A1068
		[ConfigurationProperty("elmUserData")]
		[Browsable(false)]
		public ElmUserData ElmUserData
		{
			get
			{
				return (ElmUserData)base["elmUserData"];
			}
			set
			{
				base["elmUserData"] = value;
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000A2E76 File Offset: 0x000A1076
		// (set) Token: 0x06002FB4 RID: 12212 RVA: 0x000A2E88 File Offset: 0x000A1088
		[ConfigurationProperty("snaLink")]
		[Browsable(false)]
		public SnaLink SnaLink
		{
			get
			{
				return (SnaLink)base["snaLink"];
			}
			set
			{
				base["snaLink"] = value;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000A2E96 File Offset: 0x000A1096
		// (set) Token: 0x06002FB6 RID: 12214 RVA: 0x000A2EA8 File Offset: 0x000A10A8
		[ConfigurationProperty("snaEndpoint")]
		[Browsable(false)]
		public SnaEndpoint SnaEndpoint
		{
			get
			{
				return (SnaEndpoint)base["snaEndpoint"];
			}
			set
			{
				base["snaEndpoint"] = value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000A2EB6 File Offset: 0x000A10B6
		// (set) Token: 0x06002FB8 RID: 12216 RVA: 0x000A2EC8 File Offset: 0x000A10C8
		[ConfigurationProperty("snaUserData")]
		[Browsable(false)]
		public SnaUserData SnaUserData
		{
			get
			{
				return (SnaUserData)base["snaUserData"];
			}
			set
			{
				base["snaUserData"] = value;
			}
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x000A2ED6 File Offset: 0x000A10D6
		public object GetElementKey()
		{
			return this.ServiceName;
		}

		// Token: 0x06002FBA RID: 12218 RVA: 0x000968BC File Offset: 0x00094ABC
		public virtual AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x000968C5 File Offset: 0x00094AC5
		public virtual string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x000968CE File Offset: 0x00094ACE
		public virtual string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x000968D7 File Offset: 0x00094AD7
		public virtual TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002FBE RID: 12222 RVA: 0x000968E0 File Offset: 0x00094AE0
		public virtual EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x000968E9 File Offset: 0x00094AE9
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x000968F2 File Offset: 0x00094AF2
		public virtual object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x000968FC File Offset: 0x00094AFC
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00096906 File Offset: 0x00094B06
		public virtual EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x000A2EE0 File Offset: 0x000A10E0
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Service" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "HTTP" || propertyDescriptor.Category == "TCP" || propertyDescriptor.Category == "SNA")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x000A2FA8 File Offset: 0x000A11A8
		public virtual PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Service" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "HTTP" || propertyDescriptor.Category == "TCP" || propertyDescriptor.Category == "SNA")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x00008948 File Offset: 0x00006B48
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

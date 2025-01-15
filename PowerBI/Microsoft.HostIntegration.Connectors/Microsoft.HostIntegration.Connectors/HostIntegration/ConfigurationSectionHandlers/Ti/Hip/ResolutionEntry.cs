using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000572 RID: 1394
	public class ResolutionEntry : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x06002F46 RID: 12102 RVA: 0x000A2408 File Offset: 0x000A0608
		public void SetHipObjectCallBack(GetHipObjectCollectionCallbackType getHipObjectsCallback)
		{
			this._getHipObjectsCallback = getHipObjectsCallback;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x000A2411 File Offset: 0x000A0611
		public HipObjectCollection GetHipObjects()
		{
			return this._getHipObjectsCallback();
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x000A241E File Offset: 0x000A061E
		public void SetEssoSecurityPolicyCallBack(GetEssoSecurityPolicyCollectionCallbackType getEssoSecurityPoliciesCallback)
		{
			this._getEssoSecurityPoliciesCallback = getEssoSecurityPoliciesCallback;
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x000A2427 File Offset: 0x000A0627
		public EssoSecurityPolicyCollection GetEssoSecurityPolicies()
		{
			return this._getEssoSecurityPoliciesCallback();
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x06002F4A RID: 12106 RVA: 0x000A2434 File Offset: 0x000A0634
		// (set) Token: 0x06002F4B RID: 12107 RVA: 0x000A243C File Offset: 0x000A063C
		public Service ResolutionEntryService
		{
			get
			{
				return this._resolutionEntryService;
			}
			set
			{
				this._resolutionEntryService = value;
			}
		}

		// Token: 0x170009DC RID: 2524
		// (get) Token: 0x06002F4C RID: 12108 RVA: 0x000A2448 File Offset: 0x000A0648
		public ResolutionType ResolutionType
		{
			get
			{
				if (!string.IsNullOrEmpty(this.LinkToProgram) || !string.IsNullOrEmpty(this.PrefixProgramId))
				{
					return ResolutionType.Program;
				}
				if (!string.IsNullOrEmpty(this.Data) && this.Position >= 0)
				{
					return ResolutionType.Data;
				}
				if (string.IsNullOrEmpty(this.LinkToProgram) && (string.IsNullOrEmpty(this.Data) || this.Position < 0))
				{
					return ResolutionType.Endpoint;
				}
				throw new Exception("HIP Resolution type is invalid");
			}
		}

		// Token: 0x170009DD RID: 2525
		// (get) Token: 0x06002F4D RID: 12109 RVA: 0x000A24B8 File Offset: 0x000A06B8
		public string InterfaceMethod
		{
			get
			{
				return (string)base["interfaceName"] + "." + (string)base["method"];
			}
		}

		// Token: 0x170009DE RID: 2526
		// (get) Token: 0x06002F4E RID: 12110 RVA: 0x000A24E4 File Offset: 0x000A06E4
		// (set) Token: 0x06002F4F RID: 12111 RVA: 0x000A24F6 File Offset: 0x000A06F6
		[Description("InterfaceName represents the name given to one of the .NET object elements found under the objects parent node. Required.")]
		[Category("General")]
		[ConfigurationProperty("interfaceName", IsRequired = true)]
		[DisplayName("Interface Name")]
		[ReadOnly(true)]
		public string InterfaceName
		{
			get
			{
				return (string)base["interfaceName"];
			}
			set
			{
				base["interfaceName"] = value;
			}
		}

		// Token: 0x170009DF RID: 2527
		// (get) Token: 0x06002F50 RID: 12112 RVA: 0x000A2504 File Offset: 0x000A0704
		// (set) Token: 0x06002F51 RID: 12113 RVA: 0x000A2516 File Offset: 0x000A0716
		[Description("The name of a method on the object that will be executed.")]
		[Category("General")]
		[ConfigurationProperty("method", IsRequired = true)]
		[DisplayName("Method")]
		[ReadOnly(true)]
		public string Method
		{
			get
			{
				return (string)base["method"];
			}
			set
			{
				base["method"] = value;
			}
		}

		// Token: 0x170009E0 RID: 2528
		// (get) Token: 0x06002F52 RID: 12114 RVA: 0x000A2524 File Offset: 0x000A0724
		// (set) Token: 0x06002F53 RID: 12115 RVA: 0x000A2536 File Offset: 0x000A0736
		[Description("EssoSecurityPolicyName represents the name given to one of the Enterprise Single Signon (ESSO) essoSecurityPolicy elements found under the essoSecurityPolicies parent node. When present, the TI runtime will use ESSO to translate mainframe credentials to Windows credentials. If this attribute is not set, then the credentials of the HIP Service will be used.")]
		[Category("Security")]
		[ConfigurationProperty("essoSecurityPolicyName", IsRequired = false)]
		[DisplayName("ESSO Security Policy Name")]
		[TypeConverter(typeof(SecurityPolicyDropDownList))]
		public string EssoSecurityPolicyName
		{
			get
			{
				return (string)base["essoSecurityPolicyName"];
			}
			set
			{
				base["essoSecurityPolicyName"] = value;
			}
		}

		// Token: 0x170009E1 RID: 2529
		// (get) Token: 0x06002F54 RID: 12116 RVA: 0x000A2544 File Offset: 0x000A0744
		// (set) Token: 0x06002F55 RID: 12117 RVA: 0x000A2556 File Offset: 0x000A0756
		[ConfigurationProperty("endpoint", IsRequired = false)]
		[Browsable(false)]
		public virtual string Endpoint
		{
			get
			{
				return (string)base["endpoint"];
			}
			set
			{
				base["endpoint"] = value;
			}
		}

		// Token: 0x170009E2 RID: 2530
		// (get) Token: 0x06002F56 RID: 12118 RVA: 0x000A2564 File Offset: 0x000A0764
		// (set) Token: 0x06002F57 RID: 12119 RVA: 0x000A2576 File Offset: 0x000A0776
		[ConfigurationProperty("linkToProgram", IsRequired = false)]
		[Browsable(false)]
		public virtual string LinkToProgram
		{
			get
			{
				return (string)base["linkToProgram"];
			}
			set
			{
				base["linkToProgram"] = value;
			}
		}

		// Token: 0x170009E3 RID: 2531
		// (get) Token: 0x06002F58 RID: 12120 RVA: 0x000A2584 File Offset: 0x000A0784
		// (set) Token: 0x06002F59 RID: 12121 RVA: 0x000A2596 File Offset: 0x000A0796
		[ConfigurationProperty("data", IsRequired = false)]
		[Browsable(false)]
		public virtual string Data
		{
			get
			{
				return (string)base["data"];
			}
			set
			{
				base["data"] = value;
			}
		}

		// Token: 0x170009E4 RID: 2532
		// (get) Token: 0x06002F5A RID: 12122 RVA: 0x000A25A4 File Offset: 0x000A07A4
		// (set) Token: 0x06002F5B RID: 12123 RVA: 0x000A25B6 File Offset: 0x000A07B6
		[ConfigurationProperty("position", IsRequired = false, DefaultValue = 1)]
		[Browsable(false)]
		public virtual int Position
		{
			get
			{
				return (int)base["position"];
			}
			set
			{
				base["position"] = value;
			}
		}

		// Token: 0x170009E5 RID: 2533
		// (get) Token: 0x06002F5C RID: 12124 RVA: 0x000A25C9 File Offset: 0x000A07C9
		// (set) Token: 0x06002F5D RID: 12125 RVA: 0x000A25DB File Offset: 0x000A07DB
		[ConfigurationProperty("prefixProgramId", IsRequired = false)]
		[Browsable(false)]
		public virtual string PrefixProgramId
		{
			get
			{
				return (string)base["prefixProgramId"];
			}
			set
			{
				base["prefixProgramId"] = value;
			}
		}

		// Token: 0x06002F5E RID: 12126 RVA: 0x000A25EC File Offset: 0x000A07EC
		public object GetElementKey()
		{
			string text = this.InterfaceName + this.Method;
			switch (this.ResolutionType)
			{
			case ResolutionType.Data:
				text = text + this.Data + this.Position.ToString(CultureInfo.InvariantCulture);
				break;
			case ResolutionType.Program:
				if (!string.IsNullOrEmpty(this.PrefixProgramId))
				{
					text += this.PrefixProgramId;
				}
				else
				{
					text += this.LinkToProgram;
				}
				break;
			}
			return text;
		}

		// Token: 0x06002F5F RID: 12127 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002F60 RID: 12128 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002F61 RID: 12129 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002F62 RID: 12130 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002F63 RID: 12131 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002F64 RID: 12132 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002F65 RID: 12133 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002F66 RID: 12134 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002F67 RID: 12135 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002F68 RID: 12136 RVA: 0x000A2674 File Offset: 0x000A0874
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "HTTP" || propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "Resolution")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F69 RID: 12137 RVA: 0x000A2728 File Offset: 0x000A0928
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "HTTP" || propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "Resolution")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F6A RID: 12138 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C2A RID: 7210
		private GetHipObjectCollectionCallbackType _getHipObjectsCallback;

		// Token: 0x04001C2B RID: 7211
		private Service _resolutionEntryService;

		// Token: 0x04001C2C RID: 7212
		private GetEssoSecurityPolicyCollectionCallbackType _getEssoSecurityPoliciesCallback;
	}
}

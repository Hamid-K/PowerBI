using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Hip
{
	// Token: 0x02000575 RID: 1397
	public class EssoSecurityPolicy : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x06002F7C RID: 12156 RVA: 0x000A294B File Offset: 0x000A0B4B
		public void SetEssoSecurityPolicyCallBack(GetEssoSecurityPolicyCollectionCallbackType getEssoSecurityPoliciesCallback)
		{
			this._getEssoSecurityPoliciesCallback = getEssoSecurityPoliciesCallback;
		}

		// Token: 0x06002F7D RID: 12157 RVA: 0x000A2954 File Offset: 0x000A0B54
		public EssoSecurityPolicyCollection GetEssoSecurityPolicies()
		{
			return this._getEssoSecurityPoliciesCallback();
		}

		// Token: 0x170009EB RID: 2539
		// (get) Token: 0x06002F7E RID: 12158 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06002F7F RID: 12159 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[TypeConverter(typeof(EssoSecurityPolicyNameValidation))]
		[Description("The name given to the Enterprise Single Signon Security Policy.")]
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

		// Token: 0x170009EC RID: 2540
		// (get) Token: 0x06002F80 RID: 12160 RVA: 0x000A2961 File Offset: 0x000A0B61
		// (set) Token: 0x06002F81 RID: 12161 RVA: 0x000A2973 File Offset: 0x000A0B73
		[Description("The Credential Group is used to create a default Windows login environment when a host user name is not available from the protocol data stream.")]
		[Category("Security")]
		[ConfigurationProperty("defaultCredentialsGroup", IsRequired = false)]
		[DisplayName("Default Credentials Group")]
		public string DefaultCredentialsGroup
		{
			get
			{
				return (string)base["defaultCredentialsGroup"];
			}
			set
			{
				base["defaultCredentialsGroup"] = value;
			}
		}

		// Token: 0x170009ED RID: 2541
		// (get) Token: 0x06002F82 RID: 12162 RVA: 0x000A2981 File Offset: 0x000A0B81
		// (set) Token: 0x06002F83 RID: 12163 RVA: 0x000A2993 File Offset: 0x000A0B93
		[Description("SSO Administraiton has 3 types of groups: Group, Host Group and Individual. The affiliateApplication attribute must be one of a Host Group or Individual affiliate application. Specify the SSO affiliate application to be queried to gain access to the Windows credentials needed to execute methods on the server object.")]
		[Category("Security")]
		[ConfigurationProperty("affiliateApplication", IsRequired = true)]
		[DisplayName("Affiliate Application")]
		public string AffiliateApplication
		{
			get
			{
				return (string)base["affiliateApplication"];
			}
			set
			{
				base["affiliateApplication"] = value;
			}
		}

		// Token: 0x06002F84 RID: 12164 RVA: 0x000A29A1 File Offset: 0x000A0BA1
		public object GetElementKey()
		{
			return this.Name;
		}

		// Token: 0x06002F85 RID: 12165 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002F86 RID: 12166 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002F87 RID: 12167 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002F88 RID: 12168 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002F89 RID: 12169 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002F8A RID: 12170 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002F8B RID: 12171 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002F8C RID: 12172 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002F8D RID: 12173 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002F8E RID: 12174 RVA: 0x000A29AC File Offset: 0x000A0BAC
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x000A2A3C File Offset: 0x000A0C3C
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Security" || propertyDescriptor.Category == "General")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C2E RID: 7214
		private GetEssoSecurityPolicyCollectionCallbackType _getEssoSecurityPoliciesCallback;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x02000593 RID: 1427
	public class WipObject : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x06003146 RID: 12614 RVA: 0x000A4C6F File Offset: 0x000A2E6F
		public void SetRemoteEnvironmentCallBack(GetRemoteEnvironmentCollectionCallbackType getRemoteEnvironmentsCallback)
		{
			this._getRemoteEnvironmentsCallback = getRemoteEnvironmentsCallback;
		}

		// Token: 0x06003147 RID: 12615 RVA: 0x000A4C78 File Offset: 0x000A2E78
		public RemoteEnvironmentCollection GetRemoteEnvironments()
		{
			return this._getRemoteEnvironmentsCallback();
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06003148 RID: 12616 RVA: 0x00017DAF File Offset: 0x00015FAF
		// (set) Token: 0x06003149 RID: 12617 RVA: 0x00017DC1 File Offset: 0x00015FC1
		[Description("The Namespace.ClassName in the TI Assembly when it was created by the HIS Designer.")]
		[Category("General")]
		[ConfigurationProperty("name", IsRequired = true)]
		[DisplayName("Name")]
		[TypeConverter(typeof(WipObjectNameValidation))]
		public virtual string Name
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

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x0600314A RID: 12618 RVA: 0x000A4C88 File Offset: 0x000A2E88
		// (set) Token: 0x0600314B RID: 12619 RVA: 0x000A4CB5 File Offset: 0x000A2EB5
		[TypeConverter(typeof(RemoteEnvironmentTypeDropDownList))]
		[Description("A string that identifies the Remote Environment Type of the TI Object.")]
		[Category("General")]
		[ConfigurationProperty("remoteEnvironmentTypeId", IsRequired = false, DefaultValue = "Unknown")]
		[DisplayName("Remote Environment Type")]
		public string RemoteEnvironmentTypeId
		{
			get
			{
				string text = (string)base["remoteEnvironmentTypeId"];
				if (!string.IsNullOrWhiteSpace(text))
				{
					return text;
				}
				return "Unknown";
			}
			set
			{
				base["remoteEnvironmentTypeId"] = value;
			}
		}

		// Token: 0x17000A6F RID: 2671
		// (get) Token: 0x0600314C RID: 12620 RVA: 0x000A4CC3 File Offset: 0x000A2EC3
		// (set) Token: 0x0600314D RID: 12621 RVA: 0x000A4CD5 File Offset: 0x000A2ED5
		[TypeConverter(typeof(RemoteEnvironmentDropDownList))]
		[Description("A unique identifier that identifies the Remote Environment that the TI Assembly is to be associated. If no Remote Environment is specified the default Remote Environment is used.")]
		[Category("General")]
		[ConfigurationProperty("remoteEnvironmentName", IsRequired = false)]
		[DisplayName("Remote Environment Name")]
		public string RemoteEnvironmentName
		{
			get
			{
				return (string)base["remoteEnvironmentName"];
			}
			set
			{
				base["remoteEnvironmentName"] = value;
			}
		}

		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x0600314E RID: 12622 RVA: 0x000A4CE4 File Offset: 0x000A2EE4
		// (set) Token: 0x0600314F RID: 12623 RVA: 0x000A4E10 File Offset: 0x000A3010
		public RemoteEnvironmentType RemoteEnvironmentType
		{
			get
			{
				if (this.RemoteEnvironmentTypeId == "ElmLink")
				{
					return RemoteEnvironmentType.ElmLink;
				}
				if (this.RemoteEnvironmentTypeId == "ElmUserData")
				{
					return RemoteEnvironmentType.ElmUserData;
				}
				if (this.RemoteEnvironmentTypeId == "TrmLink")
				{
					return RemoteEnvironmentType.TrmLink;
				}
				if (this.RemoteEnvironmentTypeId == "TrmUserData")
				{
					return RemoteEnvironmentType.TrmUserData;
				}
				if (this.RemoteEnvironmentTypeId == "HttpLink")
				{
					return RemoteEnvironmentType.HttpLink;
				}
				if (this.RemoteEnvironmentTypeId == "HttpUserData")
				{
					return RemoteEnvironmentType.HttpUserData;
				}
				if (this.RemoteEnvironmentTypeId == "DistributedProgramCall")
				{
					return RemoteEnvironmentType.DistributedProgramCall;
				}
				if (this.RemoteEnvironmentTypeId == "ImsConnect")
				{
					return RemoteEnvironmentType.ImsConnect;
				}
				if (this.RemoteEnvironmentTypeId == "SnaLink")
				{
					return RemoteEnvironmentType.SnaLink;
				}
				if (this.RemoteEnvironmentTypeId == "SnaUserData")
				{
					return RemoteEnvironmentType.SnaUserData;
				}
				if (this.RemoteEnvironmentTypeId == "ImsLu62")
				{
					return RemoteEnvironmentType.ImsLu62;
				}
				if (this.RemoteEnvironmentTypeId == "SystemzSocketsLink")
				{
					return RemoteEnvironmentType.SystemzSocketsLink;
				}
				if (this.RemoteEnvironmentTypeId == "SystemzSocketsUserData")
				{
					return RemoteEnvironmentType.SystemzSocketsUserData;
				}
				if (this.RemoteEnvironmentTypeId == "SystemiSocketsUserData")
				{
					return RemoteEnvironmentType.SystemiSocketsUserData;
				}
				return RemoteEnvironmentType.Unknown;
			}
			set
			{
				if (value == RemoteEnvironmentType.ElmLink)
				{
					this.RemoteEnvironmentTypeId = "ElmLink";
					return;
				}
				if (value == RemoteEnvironmentType.ElmUserData)
				{
					this.RemoteEnvironmentTypeId = "ElmUserData";
					return;
				}
				if (value == RemoteEnvironmentType.TrmLink)
				{
					this.RemoteEnvironmentTypeId = "TrmLink";
					return;
				}
				if (value == RemoteEnvironmentType.TrmUserData)
				{
					this.RemoteEnvironmentTypeId = "TrmUserData";
					return;
				}
				if (value == RemoteEnvironmentType.HttpLink)
				{
					this.RemoteEnvironmentTypeId = "HttpLink";
					return;
				}
				if (value == RemoteEnvironmentType.HttpUserData)
				{
					this.RemoteEnvironmentTypeId = "HttpUserData";
					return;
				}
				if (value == RemoteEnvironmentType.DistributedProgramCall)
				{
					this.RemoteEnvironmentTypeId = "DistributedProgramCall";
					return;
				}
				if (value == RemoteEnvironmentType.ImsConnect)
				{
					this.RemoteEnvironmentTypeId = "ImsConnect";
					return;
				}
				if (value == RemoteEnvironmentType.SnaLink)
				{
					this.RemoteEnvironmentTypeId = "SnaLink";
					return;
				}
				if (value == RemoteEnvironmentType.SnaUserData)
				{
					this.RemoteEnvironmentTypeId = "SnaUserData";
					return;
				}
				if (value == RemoteEnvironmentType.ImsLu62)
				{
					this.RemoteEnvironmentTypeId = "ImsLu62";
					return;
				}
				if (value == RemoteEnvironmentType.SystemzSocketsLink)
				{
					this.RemoteEnvironmentTypeId = "SystemzSocketsLink";
					return;
				}
				if (value == RemoteEnvironmentType.SystemzSocketsUserData)
				{
					this.RemoteEnvironmentTypeId = "SystemzSocketsUserData";
					return;
				}
				if (value == RemoteEnvironmentType.SystemiSocketsUserData)
				{
					this.RemoteEnvironmentTypeId = "SystemiSocketsUserData";
					return;
				}
				this.RemoteEnvironmentTypeId = "Unknown";
			}
		}

		// Token: 0x06003150 RID: 12624 RVA: 0x000A4F0E File Offset: 0x000A310E
		public object GetElementKey()
		{
			return this.Name;
		}

		// Token: 0x06003151 RID: 12625 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003152 RID: 12626 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003153 RID: 12627 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003154 RID: 12628 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003155 RID: 12629 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06003158 RID: 12632 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06003159 RID: 12633 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600315A RID: 12634 RVA: 0x000A4F18 File Offset: 0x000A3118
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

		// Token: 0x0600315B RID: 12635 RVA: 0x000A4F98 File Offset: 0x000A3198
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

		// Token: 0x0600315C RID: 12636 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}

		// Token: 0x04001C56 RID: 7254
		private GetRemoteEnvironmentCollectionCallbackType _getRemoteEnvironmentsCallback;
	}
}

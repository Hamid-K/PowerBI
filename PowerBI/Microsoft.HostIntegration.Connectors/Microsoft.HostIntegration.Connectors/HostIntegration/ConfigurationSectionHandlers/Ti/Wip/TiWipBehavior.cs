using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers.Ti.Wip
{
	// Token: 0x020005A3 RID: 1443
	public class TiWipBehavior : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x0600325B RID: 12891 RVA: 0x000A7B6C File Offset: 0x000A5D6C
		// (set) Token: 0x0600325C RID: 12892 RVA: 0x000A7B7E File Offset: 0x000A5D7E
		[Description("Instructs the TI Runtime to use the specified object to collect Accounting Information from the TI Runtime Execution.")]
		[Category("General")]
		[ConfigurationProperty("callAccountingProcessor", IsRequired = false)]
		[DisplayName("Call Accounting Processor")]
		public string CallAccountingProcessor
		{
			get
			{
				return (string)base["callAccountingProcessor"];
			}
			set
			{
				base["callAccountingProcessor"] = value;
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x0600325D RID: 12893 RVA: 0x000A7B8C File Offset: 0x000A5D8C
		// (set) Token: 0x0600325E RID: 12894 RVA: 0x000A7B9E File Offset: 0x000A5D9E
		[Description("Instructs the TI Runtime to allow 9 characters in the primary segment for the IMS Transaction ID.")]
		[Category("Conversion")]
		[ConfigurationProperty("nineCharacterImsTransactionId", IsRequired = false)]
		[DisplayName("Nine Character IMS Transaction Id")]
		public bool NineCharacterImsTransactionId
		{
			get
			{
				return (bool)base["nineCharacterImsTransactionId"];
			}
			set
			{
				base["nineCharacterImsTransactionId"] = value;
			}
		}

		// Token: 0x17000AB0 RID: 2736
		// (get) Token: 0x0600325F RID: 12895 RVA: 0x000A7BB1 File Offset: 0x000A5DB1
		// (set) Token: 0x06003260 RID: 12896 RVA: 0x000A7BC3 File Offset: 0x000A5DC3
		[Description("Instructs the TI Runtime to allow source TP Name Override for all SNA Link Methods.")]
		[Category("CICS")]
		[ConfigurationProperty("sourceTransactionProgramNameOverride", IsRequired = false)]
		[DisplayName("Source Transaction Program Name Override")]
		public bool SourceTransactionProgramNameOverride
		{
			get
			{
				return (bool)base["sourceTransactionProgramNameOverride"];
			}
			set
			{
				base["sourceTransactionProgramNameOverride"] = value;
			}
		}

		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x06003261 RID: 12897 RVA: 0x000A7BD6 File Offset: 0x000A5DD6
		// (set) Token: 0x06003262 RID: 12898 RVA: 0x000A7BE8 File Offset: 0x000A5DE8
		[Description("Instructs the TI Runtime to use SYNC Level 1 for non-transactional SNA calls.")]
		[Category("General")]
		[ConfigurationProperty("useSyncLevel1", IsRequired = false)]
		[DisplayName("Use SYNC Level 1")]
		public bool UseSyncLevel1
		{
			get
			{
				return (bool)base["useSyncLevel1"];
			}
			set
			{
				base["useSyncLevel1"] = value;
			}
		}

		// Token: 0x06003263 RID: 12899 RVA: 0x000A7BFC File Offset: 0x000A5DFC
		public Hashtable ToHashtable()
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in base.ElementInformation.Properties)
			{
				PropertyInformation propertyInformation = (PropertyInformation)obj;
				if (propertyInformation.ValueOrigin != PropertyValueOrigin.Default)
				{
					string name = propertyInformation.Name;
					if (name != null)
					{
						string text;
						if (!(name == "nineCharacterImsTransactionId"))
						{
							if (!(name == "sourceTransactionProgramNameOverride"))
							{
								if (!(name == "callAccountingProcessor"))
								{
									if (!(name == "useSyncLevel1"))
									{
										goto IL_009B;
									}
									text = "UseSyncLevel1";
								}
								else
								{
									text = "CallAccountingProcessor";
								}
							}
							else
							{
								text = "SourceTPNameOverride";
							}
						}
						else
						{
							text = "NineCharIMSTran";
						}
						hashtable.Add(text, base[propertyInformation.Name]);
						continue;
					}
					IL_009B:
					throw new ApplicationException("BugBug: PropertyName: '" + propertyInformation.Name + "' is invalid");
				}
			}
			return hashtable;
		}

		// Token: 0x06003264 RID: 12900 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06003265 RID: 12901 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06003266 RID: 12902 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06003267 RID: 12903 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x0600326B RID: 12907 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x0600326C RID: 12908 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x0600326D RID: 12909 RVA: 0x000A7D04 File Offset: 0x000A5F04
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600326E RID: 12910 RVA: 0x000A7DB8 File Offset: 0x000A5FB8
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "General" || propertyDescriptor.Category == "Conversion" || propertyDescriptor.Category == "CICS" || propertyDescriptor.Category == "TCP")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x0600326F RID: 12911 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

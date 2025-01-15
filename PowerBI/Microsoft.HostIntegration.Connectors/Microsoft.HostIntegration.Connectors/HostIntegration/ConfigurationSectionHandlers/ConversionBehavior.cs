using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace Microsoft.HostIntegration.ConfigurationSectionHandlers
{
	// Token: 0x0200050D RID: 1293
	public class ConversionBehavior : ConfigurationElement, ICustomTypeDescriptor
	{
		// Token: 0x17000898 RID: 2200
		// (get) Token: 0x06002BA3 RID: 11171 RVA: 0x00096524 File Offset: 0x00094724
		// (set) Token: 0x06002BA4 RID: 11172 RVA: 0x00096536 File Offset: 0x00094736
		[Description("Instructs Data Conversion to accept all bad numerical data values from host and regard as 0.")]
		[Category("Conversion")]
		[ConfigurationProperty("acceptAllInvalidNumerics", IsRequired = false)]
		[DisplayName("Accept All Invalid numerics")]
		public bool AcceptAllInvalidNumerics
		{
			get
			{
				return (bool)base["acceptAllInvalidNumerics"];
			}
			set
			{
				base["acceptAllInvalidNumerics"] = value;
			}
		}

		// Token: 0x17000899 RID: 2201
		// (get) Token: 0x06002BA5 RID: 11173 RVA: 0x00096549 File Offset: 0x00094749
		// (set) Token: 0x06002BA6 RID: 11174 RVA: 0x0009655B File Offset: 0x0009475B
		[Description("Instructs Data Conversion to accept Bad Sign in a COMP3 data value.")]
		[Category("Conversion")]
		[ConfigurationProperty("acceptBadCOMP3Sign", IsRequired = false)]
		[DisplayName("Accept Bad COMP3 Sign")]
		public bool AcceptBadCOMP3Sign
		{
			get
			{
				return (bool)base["acceptBadCOMP3Sign"];
			}
			set
			{
				base["acceptBadCOMP3Sign"] = value;
			}
		}

		// Token: 0x1700089A RID: 2202
		// (get) Token: 0x06002BA7 RID: 11175 RVA: 0x0009656E File Offset: 0x0009476E
		// (set) Token: 0x06002BA8 RID: 11176 RVA: 0x00096580 File Offset: 0x00094780
		[Description("Instructs Data Conversion to accept null values in Windows Data type during pack operations.")]
		[Category("Conversion")]
		[ConfigurationProperty("acceptNullPacked", IsRequired = false)]
		[DisplayName("Accept Null Packed")]
		public bool AcceptNullPacked
		{
			get
			{
				return (bool)base["acceptNullPacked"];
			}
			set
			{
				base["acceptNullPacked"] = value;
			}
		}

		// Token: 0x1700089B RID: 2203
		// (get) Token: 0x06002BA9 RID: 11177 RVA: 0x00096593 File Offset: 0x00094793
		// (set) Token: 0x06002BAA RID: 11178 RVA: 0x000965A5 File Offset: 0x000947A5
		[Description("Instructs Data Conversion to accept null Zoned Numeric data types during unpack operations.")]
		[Category("Conversion")]
		[ConfigurationProperty("acceptNullZoned", IsRequired = false)]
		[DisplayName("Accept Null Zoned")]
		public bool AcceptNullZoned
		{
			get
			{
				return (bool)base["acceptNullZoned"];
			}
			set
			{
				base["acceptNullZoned"] = value;
			}
		}

		// Token: 0x1700089C RID: 2204
		// (get) Token: 0x06002BAB RID: 11179 RVA: 0x000965B8 File Offset: 0x000947B8
		// (set) Token: 0x06002BAC RID: 11180 RVA: 0x000965CA File Offset: 0x000947CA
		[Description("Instructs Data Conversion to always check for a null on a Windows string when converting to COBOL PIC X data type.")]
		[Category("Conversion")]
		[ConfigurationProperty("alwaysCheckForNull", IsRequired = false)]
		[DisplayName("Always Check for Null")]
		public bool AlwaysCheckForNull
		{
			get
			{
				return (bool)base["alwaysCheckForNull"];
			}
			set
			{
				base["alwaysCheckForNull"] = value;
			}
		}

		// Token: 0x1700089D RID: 2205
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000965DD File Offset: 0x000947DD
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x000965EF File Offset: 0x000947EF
		[Description("Instructs Data Conversion to use space padding when converting strings to COBOL types and use to null termination on unpack.")]
		[Category("Conversion")]
		[ConfigurationProperty("stringsAreNullTerminatedAndSpacePadded", IsRequired = false)]
		[DisplayName("Strings are Null Terminated and Space Padded")]
		public bool StringsAreNullTerminatedAndSpacePadded
		{
			get
			{
				return (bool)base["stringsAreNullTerminatedAndSpacePadded"];
			}
			set
			{
				base["stringsAreNullTerminatedAndSpacePadded"] = value;
			}
		}

		// Token: 0x1700089E RID: 2206
		// (get) Token: 0x06002BAF RID: 11183 RVA: 0x00096602 File Offset: 0x00094802
		// (set) Token: 0x06002BB0 RID: 11184 RVA: 0x00096614 File Offset: 0x00094814
		[Description("Instructs Data Conversion to remove trailing nulls when packing a Windows string COBOL PIC X data type.")]
		[Category("Conversion")]
		[ConfigurationProperty("trimTrailingNulls", IsRequired = false)]
		[DisplayName("Trim Trailing Nulls")]
		public bool TrimTrailingNulls
		{
			get
			{
				return (bool)base["trimTrailingNulls"];
			}
			set
			{
				base["trimTrailingNulls"] = value;
			}
		}

		// Token: 0x1700089F RID: 2207
		// (get) Token: 0x06002BB1 RID: 11185 RVA: 0x00096627 File Offset: 0x00094827
		// (set) Token: 0x06002BB2 RID: 11186 RVA: 0x00096639 File Offset: 0x00094839
		[Description("Instructs Data Conversion to ignore null-termination and space-padding settings during unpack operations.")]
		[Category("Conversion")]
		[ConfigurationProperty("convertReceivedStringsAsIs", IsRequired = false)]
		[DisplayName("Convert Received Strings As Is")]
		public bool ConvertReceivedStringsAsIs
		{
			get
			{
				return (bool)base["convertReceivedStringsAsIs"];
			}
			set
			{
				base["convertReceivedStringsAsIs"] = value;
			}
		}

		// Token: 0x170008A0 RID: 2208
		// (get) Token: 0x06002BB3 RID: 11187 RVA: 0x0009664C File Offset: 0x0009484C
		// (set) Token: 0x06002BB4 RID: 11188 RVA: 0x0009665E File Offset: 0x0009485E
		[Description("Instructs Data Conversion to accept null values for Simple REDEFINE statements (Pack), and provide such (Unpack).")]
		[Category("Conversion")]
		[ConfigurationProperty("allowNullRedefines", IsRequired = false)]
		[DisplayName("Allow null for Simple Redefines")]
		public bool AllowNullRedefines
		{
			get
			{
				return (bool)base["allowNullRedefines"];
			}
			set
			{
				base["allowNullRedefines"] = value;
			}
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x00096674 File Offset: 0x00094874
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
						uint num = <fa2731a4-b8df-42ec-a59c-4417eb13e59d><PrivateImplementationDetails>.ComputeStringHash(name);
						string text;
						if (num <= 911478351U)
						{
							if (num <= 519286494U)
							{
								if (num != 462658592U)
								{
									if (num != 519286494U)
									{
										goto IL_01CC;
									}
									if (!(name == "stringsAreNullTerminatedAndSpacePadded"))
									{
										goto IL_01CC;
									}
									text = "StringsAreNullTerminatedAndSpacePadded";
								}
								else
								{
									if (!(name == "acceptNullPacked"))
									{
										goto IL_01CC;
									}
									text = "AcceptNullPacked";
								}
							}
							else if (num != 743387702U)
							{
								if (num != 911478351U)
								{
									goto IL_01CC;
								}
								if (!(name == "acceptAllInvalidNumerics"))
								{
									goto IL_01CC;
								}
								text = "AcceptAllInvalidNumerics";
							}
							else
							{
								if (!(name == "alwaysCheckForNull"))
								{
									goto IL_01CC;
								}
								text = "AlwaysCheckForNull";
							}
						}
						else if (num <= 1892735498U)
						{
							if (num != 1327296049U)
							{
								if (num != 1892735498U)
								{
									goto IL_01CC;
								}
								if (!(name == "allowNullRedefines"))
								{
									goto IL_01CC;
								}
								text = "AllowNullRedefines";
							}
							else
							{
								if (!(name == "acceptBadCOMP3Sign"))
								{
									goto IL_01CC;
								}
								text = "AcceptBadCOMP3Sign";
							}
						}
						else if (num != 2666201153U)
						{
							if (num != 3430375790U)
							{
								if (num != 3739022303U)
								{
									goto IL_01CC;
								}
								if (!(name == "convertReceivedStringsAsIs"))
								{
									goto IL_01CC;
								}
								text = "ConvertReceivedStringsAsIs";
							}
							else
							{
								if (!(name == "acceptNullZoned"))
								{
									goto IL_01CC;
								}
								text = "AcceptNullZoned";
							}
						}
						else
						{
							if (!(name == "trimTrailingNulls"))
							{
								goto IL_01CC;
							}
							text = "TrimTrailingNulls";
						}
						hashtable.Add(text, base[propertyInformation.Name]);
						continue;
					}
					IL_01CC:
					throw new ApplicationException("BugBug: PropertyName: '" + propertyInformation.Name + "' is invalid");
				}
			}
			return hashtable;
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000968BC File Offset: 0x00094ABC
		public AttributeCollection GetAttributes()
		{
			return TypeDescriptor.GetAttributes(this, true);
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000968C5 File Offset: 0x00094AC5
		public string GetClassName()
		{
			return TypeDescriptor.GetClassName(this, true);
		}

		// Token: 0x06002BB8 RID: 11192 RVA: 0x000968CE File Offset: 0x00094ACE
		public string GetComponentName()
		{
			return TypeDescriptor.GetComponentName(this, true);
		}

		// Token: 0x06002BB9 RID: 11193 RVA: 0x000968D7 File Offset: 0x00094AD7
		public TypeConverter GetConverter()
		{
			return TypeDescriptor.GetConverter(this, true);
		}

		// Token: 0x06002BBA RID: 11194 RVA: 0x000968E0 File Offset: 0x00094AE0
		public EventDescriptor GetDefaultEvent()
		{
			return TypeDescriptor.GetDefaultEvent(this, true);
		}

		// Token: 0x06002BBB RID: 11195 RVA: 0x000968E9 File Offset: 0x00094AE9
		public PropertyDescriptor GetDefaultProperty()
		{
			return TypeDescriptor.GetDefaultProperty(this, true);
		}

		// Token: 0x06002BBC RID: 11196 RVA: 0x000968F2 File Offset: 0x00094AF2
		public object GetEditor(Type editorBaseType)
		{
			return TypeDescriptor.GetEditor(this, editorBaseType, true);
		}

		// Token: 0x06002BBD RID: 11197 RVA: 0x000968FC File Offset: 0x00094AFC
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			return TypeDescriptor.GetEvents(this, attributes, true);
		}

		// Token: 0x06002BBE RID: 11198 RVA: 0x00096906 File Offset: 0x00094B06
		public EventDescriptorCollection GetEvents()
		{
			return TypeDescriptor.GetEvents(this, true);
		}

		// Token: 0x06002BBF RID: 11199 RVA: 0x00096910 File Offset: 0x00094B10
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, attributes, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Conversion")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002BC0 RID: 11200 RVA: 0x00096990 File Offset: 0x00094B90
		public PropertyDescriptorCollection GetProperties()
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this, true);
			List<PropertyDescriptor> list = new List<PropertyDescriptor>();
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				if (propertyDescriptor.Category == "Conversion")
				{
					list.Add(propertyDescriptor);
				}
			}
			return new PropertyDescriptorCollection(list.ToArray());
		}

		// Token: 0x06002BC1 RID: 11201 RVA: 0x00008948 File Offset: 0x00006B48
		public object GetPropertyOwner(PropertyDescriptor pd)
		{
			return this;
		}
	}
}

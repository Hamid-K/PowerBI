using System;
using System.Resources;
using FxResources.System.Configuration.ConfigurationManager;

namespace System
{
	// Token: 0x02000003 RID: 3
	internal static class SR
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		private static bool UsingResourceKeys()
		{
			return global::System.SR.s_usingResourceKeys;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		internal static string GetResourceString(string resourceKey)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceKey;
			}
			string text = null;
			try
			{
				text = global::System.SR.ResourceManager.GetString(resourceKey);
			}
			catch (MissingManifestResourceException)
			{
			}
			return text;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002094 File Offset: 0x00000294
		internal static string GetResourceString(string resourceKey, string defaultString)
		{
			string resourceString = global::System.SR.GetResourceString(resourceKey);
			if (!(resourceKey == resourceString) && resourceString != null)
			{
				return resourceString;
			}
			return defaultString;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020B7 File Offset: 0x000002B7
		internal static string Format(string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(resourceFormat, p1);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020E0 File Offset: 0x000002E0
		internal static string Format(string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(resourceFormat, p1, p2);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000210E File Offset: 0x0000030E
		internal static string Format(string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(resourceFormat, p1, p2, p3);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002141 File Offset: 0x00000341
		internal static string Format(string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(resourceFormat, args);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000216D File Offset: 0x0000036D
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1 });
			}
			return string.Format(provider, resourceFormat, p1);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002197 File Offset: 0x00000397
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2 });
			}
			return string.Format(provider, resourceFormat, p1, p2);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C6 File Offset: 0x000003C6
		internal static string Format(IFormatProvider provider, string resourceFormat, object p1, object p2, object p3)
		{
			if (global::System.SR.UsingResourceKeys())
			{
				return string.Join(", ", new object[] { resourceFormat, p1, p2, p3 });
			}
			return string.Format(provider, resourceFormat, p1, p2, p3);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021FC File Offset: 0x000003FC
		internal static string Format(IFormatProvider provider, string resourceFormat, params object[] args)
		{
			if (args == null)
			{
				return resourceFormat;
			}
			if (global::System.SR.UsingResourceKeys())
			{
				return resourceFormat + ", " + string.Join(", ", args);
			}
			return string.Format(provider, resourceFormat, args);
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002229 File Offset: 0x00000429
		internal static ResourceManager ResourceManager
		{
			get
			{
				ResourceManager resourceManager;
				if ((resourceManager = global::System.SR.s_resourceManager) == null)
				{
					resourceManager = (global::System.SR.s_resourceManager = new ResourceManager(typeof(FxResources.System.Configuration.ConfigurationManager.SR)));
				}
				return resourceManager;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x00002249 File Offset: 0x00000449
		internal static string Parameter_Invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Parameter_Invalid");
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000E RID: 14 RVA: 0x00002255 File Offset: 0x00000455
		internal static string Parameter_NullOrEmpty
		{
			get
			{
				return global::System.SR.GetResourceString("Parameter_NullOrEmpty");
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000F RID: 15 RVA: 0x00002261 File Offset: 0x00000461
		internal static string Property_NullOrEmpty
		{
			get
			{
				return global::System.SR.GetResourceString("Property_NullOrEmpty");
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000010 RID: 16 RVA: 0x0000226D File Offset: 0x0000046D
		internal static string Property_Invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Property_Invalid");
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000011 RID: 17 RVA: 0x00002279 File Offset: 0x00000479
		internal static string Unexpected_Error
		{
			get
			{
				return global::System.SR.GetResourceString("Unexpected_Error");
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000012 RID: 18 RVA: 0x00002285 File Offset: 0x00000485
		internal static string Wrapped_exception_message
		{
			get
			{
				return global::System.SR.GetResourceString("Wrapped_exception_message");
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002291 File Offset: 0x00000491
		internal static string Config_error_loading_XML_file
		{
			get
			{
				return global::System.SR.GetResourceString("Config_error_loading_XML_file");
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000014 RID: 20 RVA: 0x0000229D File Offset: 0x0000049D
		internal static string Config_exception_creating_section_handler
		{
			get
			{
				return global::System.SR.GetResourceString("Config_exception_creating_section_handler");
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000015 RID: 21 RVA: 0x000022A9 File Offset: 0x000004A9
		internal static string Config_exception_creating_section
		{
			get
			{
				return global::System.SR.GetResourceString("Config_exception_creating_section");
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000016 RID: 22 RVA: 0x000022B5 File Offset: 0x000004B5
		internal static string Config_tag_name_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_tag_name_invalid");
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000017 RID: 23 RVA: 0x000022C1 File Offset: 0x000004C1
		internal static string Config_add_configurationsection_already_added
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsection_already_added");
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000018 RID: 24 RVA: 0x000022CD File Offset: 0x000004CD
		internal static string Config_add_configurationsection_already_exists
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsection_already_exists");
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000019 RID: 25 RVA: 0x000022D9 File Offset: 0x000004D9
		internal static string Config_add_configurationsection_in_location_config
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsection_in_location_config");
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600001A RID: 26 RVA: 0x000022E5 File Offset: 0x000004E5
		internal static string Config_add_configurationsectiongroup_already_added
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsectiongroup_already_added");
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600001B RID: 27 RVA: 0x000022F1 File Offset: 0x000004F1
		internal static string Config_add_configurationsectiongroup_already_exists
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsectiongroup_already_exists");
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600001C RID: 28 RVA: 0x000022FD File Offset: 0x000004FD
		internal static string Config_add_configurationsectiongroup_in_location_config
		{
			get
			{
				return global::System.SR.GetResourceString("Config_add_configurationsectiongroup_in_location_config");
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600001D RID: 29 RVA: 0x00002309 File Offset: 0x00000509
		internal static string Config_allow_exedefinition_error_application
		{
			get
			{
				return global::System.SR.GetResourceString("Config_allow_exedefinition_error_application");
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600001E RID: 30 RVA: 0x00002315 File Offset: 0x00000515
		internal static string Config_allow_exedefinition_error_machine
		{
			get
			{
				return global::System.SR.GetResourceString("Config_allow_exedefinition_error_machine");
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002321 File Offset: 0x00000521
		internal static string Config_allow_exedefinition_error_roaminguser
		{
			get
			{
				return global::System.SR.GetResourceString("Config_allow_exedefinition_error_roaminguser");
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000020 RID: 32 RVA: 0x0000232D File Offset: 0x0000052D
		internal static string Config_appsettings_declaration_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_appsettings_declaration_invalid");
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002339 File Offset: 0x00000539
		internal static string Config_base_attribute_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_attribute_locked");
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000022 RID: 34 RVA: 0x00002345 File Offset: 0x00000545
		internal static string Config_base_collection_item_locked_cannot_clear
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_item_locked_cannot_clear");
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000023 RID: 35 RVA: 0x00002351 File Offset: 0x00000551
		internal static string Config_base_collection_item_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_item_locked");
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000024 RID: 36 RVA: 0x0000235D File Offset: 0x0000055D
		internal static string Config_base_cannot_add_items_above_inherited_items
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_cannot_add_items_above_inherited_items");
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000025 RID: 37 RVA: 0x00002369 File Offset: 0x00000569
		internal static string Config_base_cannot_add_items_below_inherited_items
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_cannot_add_items_below_inherited_items");
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002375 File Offset: 0x00000575
		internal static string Config_base_cannot_remove_inherited_items
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_cannot_remove_inherited_items");
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002381 File Offset: 0x00000581
		internal static string Config_base_collection_elements_may_not_be_removed
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_elements_may_not_be_removed");
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000238D File Offset: 0x0000058D
		internal static string Config_base_collection_entry_already_exists
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_entry_already_exists");
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002399 File Offset: 0x00000599
		internal static string Config_base_collection_entry_already_removed
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_entry_already_removed");
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000023A5 File Offset: 0x000005A5
		internal static string Config_base_collection_entry_not_found
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_collection_entry_not_found");
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000023B1 File Offset: 0x000005B1
		internal static string Config_base_element_cannot_have_multiple_child_elements
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_element_cannot_have_multiple_child_elements");
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000023BD File Offset: 0x000005BD
		internal static string Config_base_element_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_element_locked");
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000023C9 File Offset: 0x000005C9
		internal static string Config_base_expected_to_find_element
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_expected_to_find_element");
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000023D5 File Offset: 0x000005D5
		internal static string Config_base_invalid_attribute_to_lock
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_invalid_attribute_to_lock");
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000023E1 File Offset: 0x000005E1
		internal static string Config_base_invalid_attribute_to_lock_by_add
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_invalid_attribute_to_lock_by_add");
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000023ED File Offset: 0x000005ED
		internal static string Config_base_invalid_element_key
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_invalid_element_key");
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000023F9 File Offset: 0x000005F9
		internal static string Config_base_invalid_element_to_lock
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_invalid_element_to_lock");
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002405 File Offset: 0x00000605
		internal static string Config_base_invalid_element_to_lock_by_add
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_invalid_element_to_lock_by_add");
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002411 File Offset: 0x00000611
		internal static string Config_base_property_is_not_a_configuration_element
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_property_is_not_a_configuration_element");
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000034 RID: 52 RVA: 0x0000241D File Offset: 0x0000061D
		internal static string Config_base_read_only
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_read_only");
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002429 File Offset: 0x00000629
		internal static string Config_base_required_attribute_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_required_attribute_locked");
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002435 File Offset: 0x00000635
		internal static string Config_base_required_attribute_lock_attempt
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_required_attribute_lock_attempt");
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002441 File Offset: 0x00000641
		internal static string Config_base_required_attribute_missing
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_required_attribute_missing");
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000038 RID: 56 RVA: 0x0000244D File Offset: 0x0000064D
		internal static string Config_base_section_invalid_content
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_section_invalid_content");
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00002459 File Offset: 0x00000659
		internal static string Config_base_unrecognized_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_unrecognized_attribute");
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002465 File Offset: 0x00000665
		internal static string Config_base_unrecognized_element
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_unrecognized_element");
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002471 File Offset: 0x00000671
		internal static string Config_base_unrecognized_element_name
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_unrecognized_element_name");
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000247D File Offset: 0x0000067D
		internal static string Config_base_value_cannot_contain
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_value_cannot_contain");
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00002489 File Offset: 0x00000689
		internal static string Config_cannot_edit_configurationsection_in_location_config
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_in_location_config");
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002495 File Offset: 0x00000695
		internal static string Config_cannot_edit_configurationsection_parentsection
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_parentsection");
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600003F RID: 63 RVA: 0x000024A1 File Offset: 0x000006A1
		internal static string Config_cannot_edit_configurationsection_when_location_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_when_location_locked");
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000040 RID: 64 RVA: 0x000024AD File Offset: 0x000006AD
		internal static string Config_cannot_edit_configurationsection_when_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_when_locked");
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000024B9 File Offset: 0x000006B9
		internal static string Config_cannot_edit_configurationsection_when_not_attached
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_when_not_attached");
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000042 RID: 66 RVA: 0x000024C5 File Offset: 0x000006C5
		internal static string Config_cannot_edit_configurationsection_when_it_is_implicit
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_when_it_is_implicit");
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000024D1 File Offset: 0x000006D1
		internal static string Config_cannot_edit_configurationsection_when_it_is_undeclared
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsection_when_it_is_undeclared");
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000024DD File Offset: 0x000006DD
		internal static string Config_cannot_edit_configurationsectiongroup_in_location_config
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsectiongroup_in_location_config");
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000024E9 File Offset: 0x000006E9
		internal static string Config_cannot_edit_configurationsectiongroup_when_not_attached
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_configurationsectiongroup_when_not_attached");
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000046 RID: 70 RVA: 0x000024F5 File Offset: 0x000006F5
		internal static string Config_cannot_edit_locationattriubtes
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_edit_locationattriubtes");
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00002501 File Offset: 0x00000701
		internal static string Config_cannot_open_config_source
		{
			get
			{
				return global::System.SR.GetResourceString("Config_cannot_open_config_source");
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000048 RID: 72 RVA: 0x0000250D File Offset: 0x0000070D
		internal static string Config_client_config_init_error
		{
			get
			{
				return global::System.SR.GetResourceString("Config_client_config_init_error");
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00002519 File Offset: 0x00000719
		internal static string Config_client_config_too_many_configsections_elements
		{
			get
			{
				return global::System.SR.GetResourceString("Config_client_config_too_many_configsections_elements");
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x0600004A RID: 74 RVA: 0x00002525 File Offset: 0x00000725
		internal static string Config_configmanager_open_noexe
		{
			get
			{
				return global::System.SR.GetResourceString("Config_configmanager_open_noexe");
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00002531 File Offset: 0x00000731
		internal static string Config_configsection_parentnotvalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_configsection_parentnotvalid");
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600004C RID: 76 RVA: 0x0000253D File Offset: 0x0000073D
		internal static string Config_connectionstrings_declaration_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_connectionstrings_declaration_invalid");
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600004D RID: 77 RVA: 0x00002549 File Offset: 0x00000749
		internal static string Config_data_read_count_mismatch
		{
			get
			{
				return global::System.SR.GetResourceString("Config_data_read_count_mismatch");
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00002555 File Offset: 0x00000755
		internal static string Config_element_no_context
		{
			get
			{
				return global::System.SR.GetResourceString("Config_element_no_context");
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00002561 File Offset: 0x00000761
		internal static string Config_empty_lock_attributes_except
		{
			get
			{
				return global::System.SR.GetResourceString("Config_empty_lock_attributes_except");
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x06000050 RID: 80 RVA: 0x0000256D File Offset: 0x0000076D
		internal static string Config_empty_lock_element_except
		{
			get
			{
				return global::System.SR.GetResourceString("Config_empty_lock_element_except");
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00002579 File Offset: 0x00000779
		internal static string Config_exception_in_config_section_handler
		{
			get
			{
				return global::System.SR.GetResourceString("Config_exception_in_config_section_handler");
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00002585 File Offset: 0x00000785
		internal static string Config_file_doesnt_have_root_configuration
		{
			get
			{
				return global::System.SR.GetResourceString("Config_file_doesnt_have_root_configuration");
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00002591 File Offset: 0x00000791
		internal static string Config_file_has_changed
		{
			get
			{
				return global::System.SR.GetResourceString("Config_file_has_changed");
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000054 RID: 84 RVA: 0x0000259D File Offset: 0x0000079D
		internal static string Config_getparentconfigurationsection_first_instance
		{
			get
			{
				return global::System.SR.GetResourceString("Config_getparentconfigurationsection_first_instance");
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000055 RID: 85 RVA: 0x000025A9 File Offset: 0x000007A9
		internal static string Config_inconsistent_location_attributes
		{
			get
			{
				return global::System.SR.GetResourceString("Config_inconsistent_location_attributes");
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000056 RID: 86 RVA: 0x000025B5 File Offset: 0x000007B5
		internal static string Config_invalid_attributes_for_write
		{
			get
			{
				return global::System.SR.GetResourceString("Config_invalid_attributes_for_write");
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000057 RID: 87 RVA: 0x000025C1 File Offset: 0x000007C1
		internal static string Config_invalid_boolean_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Config_invalid_boolean_attribute");
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000058 RID: 88 RVA: 0x000025CD File Offset: 0x000007CD
		internal static string Config_invalid_node_type
		{
			get
			{
				return global::System.SR.GetResourceString("Config_invalid_node_type");
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000059 RID: 89 RVA: 0x000025D9 File Offset: 0x000007D9
		internal static string Config_location_location_not_allowed
		{
			get
			{
				return global::System.SR.GetResourceString("Config_location_location_not_allowed");
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600005A RID: 90 RVA: 0x000025E5 File Offset: 0x000007E5
		internal static string Config_location_path_invalid_character
		{
			get
			{
				return global::System.SR.GetResourceString("Config_location_path_invalid_character");
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600005B RID: 91 RVA: 0x000025F1 File Offset: 0x000007F1
		internal static string Config_location_path_invalid_first_character
		{
			get
			{
				return global::System.SR.GetResourceString("Config_location_path_invalid_first_character");
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600005C RID: 92 RVA: 0x000025FD File Offset: 0x000007FD
		internal static string Config_location_path_invalid_last_character
		{
			get
			{
				return global::System.SR.GetResourceString("Config_location_path_invalid_last_character");
			}
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00002609 File Offset: 0x00000809
		internal static string Config_missing_required_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Config_missing_required_attribute");
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00002615 File Offset: 0x00000815
		internal static string Config_more_data_than_expected
		{
			get
			{
				return global::System.SR.GetResourceString("Config_more_data_than_expected");
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002621 File Offset: 0x00000821
		internal static string Config_name_value_file_section_file_invalid_root
		{
			get
			{
				return global::System.SR.GetResourceString("Config_name_value_file_section_file_invalid_root");
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000262D File Offset: 0x0000082D
		internal static string Config_namespace_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_namespace_invalid");
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00002639 File Offset: 0x00000839
		internal static string Config_no_stream_to_write
		{
			get
			{
				return global::System.SR.GetResourceString("Config_no_stream_to_write");
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002645 File Offset: 0x00000845
		internal static string Config_not_allowed_to_encrypt_this_section
		{
			get
			{
				return global::System.SR.GetResourceString("Config_not_allowed_to_encrypt_this_section");
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002651 File Offset: 0x00000851
		internal static string Config_object_is_null
		{
			get
			{
				return global::System.SR.GetResourceString("Config_object_is_null");
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000064 RID: 100 RVA: 0x0000265D File Offset: 0x0000085D
		internal static string Config_operation_not_runtime
		{
			get
			{
				return global::System.SR.GetResourceString("Config_operation_not_runtime");
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00002669 File Offset: 0x00000869
		internal static string Config_properties_may_not_be_derived_from_configuration_section
		{
			get
			{
				return global::System.SR.GetResourceString("Config_properties_may_not_be_derived_from_configuration_section");
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002675 File Offset: 0x00000875
		internal static string Config_provider_must_implement_type
		{
			get
			{
				return global::System.SR.GetResourceString("Config_provider_must_implement_type");
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00002681 File Offset: 0x00000881
		internal static string Config_root_section_group_cannot_be_edited
		{
			get
			{
				return global::System.SR.GetResourceString("Config_root_section_group_cannot_be_edited");
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x06000068 RID: 104 RVA: 0x0000268D File Offset: 0x0000088D
		internal static string Config_section_allow_definition_attribute_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_section_allow_definition_attribute_invalid");
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00002699 File Offset: 0x00000899
		internal static string Config_section_allow_exe_definition_attribute_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_section_allow_exe_definition_attribute_invalid");
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x0600006A RID: 106 RVA: 0x000026A5 File Offset: 0x000008A5
		internal static string Config_section_cannot_be_used_in_location
		{
			get
			{
				return global::System.SR.GetResourceString("Config_section_cannot_be_used_in_location");
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000026B1 File Offset: 0x000008B1
		internal static string Config_section_locked
		{
			get
			{
				return global::System.SR.GetResourceString("Config_section_locked");
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600006C RID: 108 RVA: 0x000026BD File Offset: 0x000008BD
		internal static string Config_sections_must_be_unique
		{
			get
			{
				return global::System.SR.GetResourceString("Config_sections_must_be_unique");
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000026C9 File Offset: 0x000008C9
		internal static string Config_source_cannot_be_shared
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_cannot_be_shared");
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600006E RID: 110 RVA: 0x000026D5 File Offset: 0x000008D5
		internal static string Config_source_parent_conflict
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_parent_conflict");
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600006F RID: 111 RVA: 0x000026E1 File Offset: 0x000008E1
		internal static string Config_source_file_format
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_file_format");
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000070 RID: 112 RVA: 0x000026ED File Offset: 0x000008ED
		internal static string Config_source_invalid_format
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_invalid_format");
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x06000071 RID: 113 RVA: 0x000026F9 File Offset: 0x000008F9
		internal static string Config_source_invalid_chars
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_invalid_chars");
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000072 RID: 114 RVA: 0x00002705 File Offset: 0x00000905
		internal static string Config_source_requires_file
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_requires_file");
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00002711 File Offset: 0x00000911
		internal static string Config_source_syntax_error
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_syntax_error");
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000074 RID: 116 RVA: 0x0000271D File Offset: 0x0000091D
		internal static string Config_system_already_set
		{
			get
			{
				return global::System.SR.GetResourceString("Config_system_already_set");
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00002729 File Offset: 0x00000929
		internal static string Config_tag_name_already_defined
		{
			get
			{
				return global::System.SR.GetResourceString("Config_tag_name_already_defined");
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000076 RID: 118 RVA: 0x00002735 File Offset: 0x00000935
		internal static string Config_tag_name_already_defined_at_this_level
		{
			get
			{
				return global::System.SR.GetResourceString("Config_tag_name_already_defined_at_this_level");
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00002741 File Offset: 0x00000941
		internal static string Config_tag_name_cannot_be_location
		{
			get
			{
				return global::System.SR.GetResourceString("Config_tag_name_cannot_be_location");
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000078 RID: 120 RVA: 0x0000274D File Offset: 0x0000094D
		internal static string Config_tag_name_cannot_begin_with_config
		{
			get
			{
				return global::System.SR.GetResourceString("Config_tag_name_cannot_begin_with_config");
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00002759 File Offset: 0x00000959
		internal static string Config_type_doesnt_inherit_from_type
		{
			get
			{
				return global::System.SR.GetResourceString("Config_type_doesnt_inherit_from_type");
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002765 File Offset: 0x00000965
		internal static string Config_unexpected_element_end
		{
			get
			{
				return global::System.SR.GetResourceString("Config_unexpected_element_end");
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00002771 File Offset: 0x00000971
		internal static string Config_unexpected_element_name
		{
			get
			{
				return global::System.SR.GetResourceString("Config_unexpected_element_name");
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600007C RID: 124 RVA: 0x0000277D File Offset: 0x0000097D
		internal static string Config_unexpected_node_type
		{
			get
			{
				return global::System.SR.GetResourceString("Config_unexpected_node_type");
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00002789 File Offset: 0x00000989
		internal static string Config_unrecognized_configuration_section
		{
			get
			{
				return global::System.SR.GetResourceString("Config_unrecognized_configuration_section");
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00002795 File Offset: 0x00000995
		internal static string Config_write_failed
		{
			get
			{
				return global::System.SR.GetResourceString("Config_write_failed");
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000027A1 File Offset: 0x000009A1
		internal static string Converter_timespan_not_in_second
		{
			get
			{
				return global::System.SR.GetResourceString("Converter_timespan_not_in_second");
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000080 RID: 128 RVA: 0x000027AD File Offset: 0x000009AD
		internal static string Converter_unsupported_value_type
		{
			get
			{
				return global::System.SR.GetResourceString("Converter_unsupported_value_type");
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000081 RID: 129 RVA: 0x000027B9 File Offset: 0x000009B9
		internal static string Decryption_failed
		{
			get
			{
				return global::System.SR.GetResourceString("Decryption_failed");
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000082 RID: 130 RVA: 0x000027C5 File Offset: 0x000009C5
		internal static string Default_value_conversion_error_from_string
		{
			get
			{
				return global::System.SR.GetResourceString("Default_value_conversion_error_from_string");
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000083 RID: 131 RVA: 0x000027D1 File Offset: 0x000009D1
		internal static string Default_value_wrong_type
		{
			get
			{
				return global::System.SR.GetResourceString("Default_value_wrong_type");
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000084 RID: 132 RVA: 0x000027DD File Offset: 0x000009DD
		internal static string DPAPI_bad_data
		{
			get
			{
				return global::System.SR.GetResourceString("DPAPI_bad_data");
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000085 RID: 133 RVA: 0x000027E9 File Offset: 0x000009E9
		internal static string Empty_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Empty_attribute");
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000086 RID: 134 RVA: 0x000027F5 File Offset: 0x000009F5
		internal static string EncryptedNode_not_found
		{
			get
			{
				return global::System.SR.GetResourceString("EncryptedNode_not_found");
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000087 RID: 135 RVA: 0x00002801 File Offset: 0x00000A01
		internal static string EncryptedNode_is_in_invalid_format
		{
			get
			{
				return global::System.SR.GetResourceString("EncryptedNode_is_in_invalid_format");
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000088 RID: 136 RVA: 0x0000280D File Offset: 0x00000A0D
		internal static string Encryption_failed
		{
			get
			{
				return global::System.SR.GetResourceString("Encryption_failed");
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000089 RID: 137 RVA: 0x00002819 File Offset: 0x00000A19
		internal static string IndexOutOfRange
		{
			get
			{
				return global::System.SR.GetResourceString("IndexOutOfRange");
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002825 File Offset: 0x00000A25
		internal static string Invalid_enum_value
		{
			get
			{
				return global::System.SR.GetResourceString("Invalid_enum_value");
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00002831 File Offset: 0x00000A31
		internal static string Must_add_to_config_before_protecting_it
		{
			get
			{
				return global::System.SR.GetResourceString("Must_add_to_config_before_protecting_it");
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600008C RID: 140 RVA: 0x0000283D File Offset: 0x00000A3D
		internal static string No_converter
		{
			get
			{
				return global::System.SR.GetResourceString("No_converter");
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00002849 File Offset: 0x00000A49
		internal static string No_exception_information_available
		{
			get
			{
				return global::System.SR.GetResourceString("No_exception_information_available");
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x0600008E RID: 142 RVA: 0x00002855 File Offset: 0x00000A55
		internal static string Property_name_reserved
		{
			get
			{
				return global::System.SR.GetResourceString("Property_name_reserved");
			}
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00002861 File Offset: 0x00000A61
		internal static string Item_name_reserved
		{
			get
			{
				return global::System.SR.GetResourceString("Item_name_reserved");
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000090 RID: 144 RVA: 0x0000286D File Offset: 0x00000A6D
		internal static string Basicmap_item_name_reserved
		{
			get
			{
				return global::System.SR.GetResourceString("Basicmap_item_name_reserved");
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00002879 File Offset: 0x00000A79
		internal static string ProtectedConfigurationProvider_not_found
		{
			get
			{
				return global::System.SR.GetResourceString("ProtectedConfigurationProvider_not_found");
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x06000092 RID: 146 RVA: 0x00002885 File Offset: 0x00000A85
		internal static string Regex_validator_error
		{
			get
			{
				return global::System.SR.GetResourceString("Regex_validator_error");
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002891 File Offset: 0x00000A91
		internal static string String_null_or_empty
		{
			get
			{
				return global::System.SR.GetResourceString("String_null_or_empty");
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000094 RID: 148 RVA: 0x0000289D File Offset: 0x00000A9D
		internal static string Subclass_validator_error
		{
			get
			{
				return global::System.SR.GetResourceString("Subclass_validator_error");
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000095 RID: 149 RVA: 0x000028A9 File Offset: 0x00000AA9
		internal static string Top_level_conversion_error_from_string
		{
			get
			{
				return global::System.SR.GetResourceString("Top_level_conversion_error_from_string");
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000028B5 File Offset: 0x00000AB5
		internal static string Top_level_conversion_error_to_string
		{
			get
			{
				return global::System.SR.GetResourceString("Top_level_conversion_error_to_string");
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000097 RID: 151 RVA: 0x000028C1 File Offset: 0x00000AC1
		internal static string Top_level_validation_error
		{
			get
			{
				return global::System.SR.GetResourceString("Top_level_validation_error");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000028CD File Offset: 0x00000ACD
		internal static string Type_cannot_be_resolved
		{
			get
			{
				return global::System.SR.GetResourceString("Type_cannot_be_resolved");
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000099 RID: 153 RVA: 0x000028D9 File Offset: 0x00000AD9
		internal static string TypeNotPublic
		{
			get
			{
				return global::System.SR.GetResourceString("TypeNotPublic");
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600009A RID: 154 RVA: 0x000028E5 File Offset: 0x00000AE5
		internal static string Unrecognized_initialization_value
		{
			get
			{
				return global::System.SR.GetResourceString("Unrecognized_initialization_value");
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600009B RID: 155 RVA: 0x000028F1 File Offset: 0x00000AF1
		internal static string Validation_scalar_range_violation_not_different
		{
			get
			{
				return global::System.SR.GetResourceString("Validation_scalar_range_violation_not_different");
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600009C RID: 156 RVA: 0x000028FD File Offset: 0x00000AFD
		internal static string Validation_scalar_range_violation_not_equal
		{
			get
			{
				return global::System.SR.GetResourceString("Validation_scalar_range_violation_not_equal");
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600009D RID: 157 RVA: 0x00002909 File Offset: 0x00000B09
		internal static string Validation_scalar_range_violation_not_in_range
		{
			get
			{
				return global::System.SR.GetResourceString("Validation_scalar_range_violation_not_in_range");
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600009E RID: 158 RVA: 0x00002915 File Offset: 0x00000B15
		internal static string Validation_scalar_range_violation_not_outside_range
		{
			get
			{
				return global::System.SR.GetResourceString("Validation_scalar_range_violation_not_outside_range");
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600009F RID: 159 RVA: 0x00002921 File Offset: 0x00000B21
		internal static string Validator_Attribute_param_not_validator
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_Attribute_param_not_validator");
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000292D File Offset: 0x00000B2D
		internal static string Validator_does_not_support_elem_type
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_does_not_support_elem_type");
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060000A1 RID: 161 RVA: 0x00002939 File Offset: 0x00000B39
		internal static string Validator_does_not_support_prop_type
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_does_not_support_prop_type");
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x00002945 File Offset: 0x00000B45
		internal static string Validator_element_not_valid
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_element_not_valid");
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00002951 File Offset: 0x00000B51
		internal static string Validator_method_not_found
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_method_not_found");
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x0000295D File Offset: 0x00000B5D
		internal static string Validator_min_greater_than_max
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_min_greater_than_max");
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00002969 File Offset: 0x00000B69
		internal static string Validator_scalar_resolution_violation
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_scalar_resolution_violation");
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x00002975 File Offset: 0x00000B75
		internal static string Validator_string_invalid_chars
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_string_invalid_chars");
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002981 File Offset: 0x00000B81
		internal static string Validator_string_max_length
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_string_max_length");
			}
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x0000298D File Offset: 0x00000B8D
		internal static string Validator_string_min_length
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_string_min_length");
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002999 File Offset: 0x00000B99
		internal static string Validator_value_type_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_value_type_invalid");
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000029A5 File Offset: 0x00000BA5
		internal static string Validator_multiple_validator_attributes
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_multiple_validator_attributes");
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060000AB RID: 171 RVA: 0x000029B1 File Offset: 0x00000BB1
		internal static string Validator_timespan_value_must_be_positive
		{
			get
			{
				return global::System.SR.GetResourceString("Validator_timespan_value_must_be_positive");
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000029BD File Offset: 0x00000BBD
		internal static string WrongType_of_Protected_provider
		{
			get
			{
				return global::System.SR.GetResourceString("WrongType_of_Protected_provider");
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060000AD RID: 173 RVA: 0x000029C9 File Offset: 0x00000BC9
		internal static string Config_element_locking_not_supported
		{
			get
			{
				return global::System.SR.GetResourceString("Config_element_locking_not_supported");
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000029D5 File Offset: 0x00000BD5
		internal static string Protection_provider_syntax_error
		{
			get
			{
				return global::System.SR.GetResourceString("Protection_provider_syntax_error");
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x060000AF RID: 175 RVA: 0x000029E1 File Offset: 0x00000BE1
		internal static string Protection_provider_invalid_format
		{
			get
			{
				return global::System.SR.GetResourceString("Protection_provider_invalid_format");
			}
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000029ED File Offset: 0x00000BED
		internal static string Cannot_declare_or_remove_implicit_section
		{
			get
			{
				return global::System.SR.GetResourceString("Cannot_declare_or_remove_implicit_section");
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x000029F9 File Offset: 0x00000BF9
		internal static string Config_reserved_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Config_reserved_attribute");
			}
		}

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00002A05 File Offset: 0x00000C05
		internal static string Filename_in_SaveAs_is_used_already
		{
			get
			{
				return global::System.SR.GetResourceString("Filename_in_SaveAs_is_used_already");
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00002A11 File Offset: 0x00000C11
		internal static string Provider_Already_Initialized
		{
			get
			{
				return global::System.SR.GetResourceString("Provider_Already_Initialized");
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x00002A1D File Offset: 0x00000C1D
		internal static string Config_provider_name_null_or_empty
		{
			get
			{
				return global::System.SR.GetResourceString("Config_provider_name_null_or_empty");
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00002A29 File Offset: 0x00000C29
		internal static string CollectionReadOnly
		{
			get
			{
				return global::System.SR.GetResourceString("CollectionReadOnly");
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x00002A35 File Offset: 0x00000C35
		internal static string Config_source_not_under_config_dir
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_not_under_config_dir");
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00002A41 File Offset: 0x00000C41
		internal static string Config_source_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_source_invalid");
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x00002A4D File Offset: 0x00000C4D
		internal static string Location_invalid_inheritInChildApplications_in_machine_or_root_web_config
		{
			get
			{
				return global::System.SR.GetResourceString("Location_invalid_inheritInChildApplications_in_machine_or_root_web_config");
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060000B9 RID: 185 RVA: 0x00002A59 File Offset: 0x00000C59
		internal static string Cannot_change_both_AllowOverride_and_OverrideMode
		{
			get
			{
				return global::System.SR.GetResourceString("Cannot_change_both_AllowOverride_and_OverrideMode");
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00002A65 File Offset: 0x00000C65
		internal static string Config_section_override_mode_attribute_invalid
		{
			get
			{
				return global::System.SR.GetResourceString("Config_section_override_mode_attribute_invalid");
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00002A71 File Offset: 0x00000C71
		internal static string Invalid_override_mode_declaration
		{
			get
			{
				return global::System.SR.GetResourceString("Invalid_override_mode_declaration");
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00002A7D File Offset: 0x00000C7D
		internal static string Machine_config_file_not_found
		{
			get
			{
				return global::System.SR.GetResourceString("Machine_config_file_not_found");
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00002A89 File Offset: 0x00000C89
		internal static string ObjectDisposed_StreamClosed
		{
			get
			{
				return global::System.SR.GetResourceString("ObjectDisposed_StreamClosed");
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00002A95 File Offset: 0x00000C95
		internal static string Unable_to_convert_type_from_string
		{
			get
			{
				return global::System.SR.GetResourceString("Unable_to_convert_type_from_string");
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00002AA1 File Offset: 0x00000CA1
		internal static string Unable_to_convert_type_to_string
		{
			get
			{
				return global::System.SR.GetResourceString("Unable_to_convert_type_to_string");
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x00002AAD File Offset: 0x00000CAD
		internal static string Could_not_create_from_default_value
		{
			get
			{
				return global::System.SR.GetResourceString("Could_not_create_from_default_value");
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002AB9 File Offset: 0x00000CB9
		internal static string Could_not_create_from_default_value_2
		{
			get
			{
				return global::System.SR.GetResourceString("Could_not_create_from_default_value_2");
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002AC5 File Offset: 0x00000CC5
		internal static string UserSettingsNotSupported
		{
			get
			{
				return global::System.SR.GetResourceString("UserSettingsNotSupported");
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002AD1 File Offset: 0x00000CD1
		internal static string SettingsSaveFailed
		{
			get
			{
				return global::System.SR.GetResourceString("SettingsSaveFailed");
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00002ADD File Offset: 0x00000CDD
		internal static string SettingsSaveFailedNoSection
		{
			get
			{
				return global::System.SR.GetResourceString("SettingsSaveFailedNoSection");
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002AE9 File Offset: 0x00000CE9
		internal static string UnknownUserLevel
		{
			get
			{
				return global::System.SR.GetResourceString("UnknownUserLevel");
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00002AF5 File Offset: 0x00000CF5
		internal static string BothScopeAttributes
		{
			get
			{
				return global::System.SR.GetResourceString("BothScopeAttributes");
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002B01 File Offset: 0x00000D01
		internal static string NoScopeAttributes
		{
			get
			{
				return global::System.SR.GetResourceString("NoScopeAttributes");
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00002B0D File Offset: 0x00000D0D
		internal static string SettingsPropertyNotFound
		{
			get
			{
				return global::System.SR.GetResourceString("SettingsPropertyNotFound");
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002B19 File Offset: 0x00000D19
		internal static string SettingsPropertyReadOnly
		{
			get
			{
				return global::System.SR.GetResourceString("SettingsPropertyReadOnly");
			}
		}

		// Token: 0x170000BF RID: 191
		// (get) Token: 0x060000CA RID: 202 RVA: 0x00002B25 File Offset: 0x00000D25
		internal static string SettingsPropertyWrongType
		{
			get
			{
				return global::System.SR.GetResourceString("SettingsPropertyWrongType");
			}
		}

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x060000CB RID: 203 RVA: 0x00002B31 File Offset: 0x00000D31
		internal static string ProviderInstantiationFailed
		{
			get
			{
				return global::System.SR.GetResourceString("ProviderInstantiationFailed");
			}
		}

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x060000CC RID: 204 RVA: 0x00002B3D File Offset: 0x00000D3D
		internal static string ProviderTypeLoadFailed
		{
			get
			{
				return global::System.SR.GetResourceString("ProviderTypeLoadFailed");
			}
		}

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00002B49 File Offset: 0x00000D49
		internal static string AppSettingsReaderNoKey
		{
			get
			{
				return global::System.SR.GetResourceString("AppSettingsReaderNoKey");
			}
		}

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00002B55 File Offset: 0x00000D55
		internal static string AppSettingsReaderCantParse
		{
			get
			{
				return global::System.SR.GetResourceString("AppSettingsReaderCantParse");
			}
		}

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00002B61 File Offset: 0x00000D61
		internal static string AppSettingsReaderEmptyString
		{
			get
			{
				return global::System.SR.GetResourceString("AppSettingsReaderEmptyString");
			}
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00002B6D File Offset: 0x00000D6D
		internal static string Config_invalid_integer_attribute
		{
			get
			{
				return global::System.SR.GetResourceString("Config_invalid_integer_attribute");
			}
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00002B79 File Offset: 0x00000D79
		internal static string Config_base_required_attribute_empty
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_required_attribute_empty");
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00002B85 File Offset: 0x00000D85
		internal static string Config_base_elements_only
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_elements_only");
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00002B91 File Offset: 0x00000D91
		internal static string Config_base_no_child_nodes
		{
			get
			{
				return global::System.SR.GetResourceString("Config_base_no_child_nodes");
			}
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00002B9D File Offset: 0x00000D9D
		internal static string InvalidNullEmptyArgument
		{
			get
			{
				return global::System.SR.GetResourceString("InvalidNullEmptyArgument");
			}
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060000D5 RID: 213 RVA: 0x00002BA9 File Offset: 0x00000DA9
		internal static string DuplicateFileName
		{
			get
			{
				return global::System.SR.GetResourceString("DuplicateFileName");
			}
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00002BB8 File Offset: 0x00000DB8
		// Note: this type is marked as 'beforefieldinit'.
		static SR()
		{
			bool flag;
			global::System.SR.s_usingResourceKeys = AppContext.TryGetSwitch("System.Resources.UseSystemResourceKeys", out flag) && flag;
		}

		// Token: 0x04000001 RID: 1
		private static readonly bool s_usingResourceKeys;

		// Token: 0x04000002 RID: 2
		private static ResourceManager s_resourceManager;
	}
}

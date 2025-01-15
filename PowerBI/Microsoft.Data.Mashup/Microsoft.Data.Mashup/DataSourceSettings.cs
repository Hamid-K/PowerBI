using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Data.Mashup.ProviderCommon;

namespace Microsoft.Data.Mashup
{
	// Token: 0x02000015 RID: 21
	public static class DataSourceSettings
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005AA9 File Offset: 0x00003CA9
		public static string Create(DataSource dataSource, DataSourceSetting dataSourceSetting)
		{
			return DataSourceSettings.Create(new Dictionary<DataSource, DataSourceSetting> { { dataSource, dataSourceSetting } });
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005AC0 File Offset: 0x00003CC0
		public static string Create(IDictionary<DataSource, DataSourceSetting> settings)
		{
			if (settings == null)
			{
				throw new ArgumentNullException("settings");
			}
			if (settings.Count == 0)
			{
				return null;
			}
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverter(new DataSourceSettings.DataSourceSettingsJsonConverter());
			IEnumerable<DataSourceSettings.JsonSerializableDataSourceSetting> enumerable = settings.Select((KeyValuePair<DataSource, DataSourceSetting> kvp) => new DataSourceSettings.JsonSerializableDataSourceSetting
			{
				DataSource = kvp.Key,
				Setting = kvp.Value
			});
			return javaScriptSerializer.Serialize(enumerable);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005B24 File Offset: 0x00003D24
		public static Dictionary<DataSource, DataSourceSetting> ToDictionary(string value)
		{
			Dictionary<DataSource, DataSourceSetting> dictionary = new Dictionary<DataSource, DataSourceSetting>();
			if (value != null)
			{
				JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
				javaScriptSerializer.RegisterConverter(new DataSourceSettings.DataSourceSettingsJsonConverter());
				DataSourceSettings.JsonSerializableDataSourceSetting[] array;
				try
				{
					array = javaScriptSerializer.Deserialize<DataSourceSettings.JsonSerializableDataSourceSetting[]>(value);
				}
				catch (MissingMethodException ex)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormatInner, ex);
				}
				catch (ArgumentException ex2)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormatInner, ex2);
				}
				catch (InvalidOperationException ex3)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormatInner, ex3);
				}
				if (array == null)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
				}
				foreach (DataSourceSettings.JsonSerializableDataSourceSetting jsonSerializableDataSourceSetting in array)
				{
					if (dictionary.ContainsKey(jsonSerializableDataSourceSetting.DataSource))
					{
						throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
					}
					dictionary[jsonSerializableDataSourceSetting.DataSource] = jsonSerializableDataSourceSetting.Setting;
				}
			}
			return dictionary;
		}

		// Token: 0x02000067 RID: 103
		private class JsonSerializableDataSourceSetting
		{
			// Token: 0x17000122 RID: 290
			// (get) Token: 0x06000457 RID: 1111 RVA: 0x00010394 File Offset: 0x0000E594
			// (set) Token: 0x06000458 RID: 1112 RVA: 0x0001039C File Offset: 0x0000E59C
			public DataSource DataSource { get; set; }

			// Token: 0x17000123 RID: 291
			// (get) Token: 0x06000459 RID: 1113 RVA: 0x000103A5 File Offset: 0x0000E5A5
			// (set) Token: 0x0600045A RID: 1114 RVA: 0x000103AD File Offset: 0x0000E5AD
			public DataSourceSetting Setting { get; set; }
		}

		// Token: 0x02000068 RID: 104
		private class DataSourceSettingsJsonConverter : JavaScriptConverter<DataSourceSettings.JsonSerializableDataSourceSetting>
		{
			// Token: 0x0600045C RID: 1116 RVA: 0x000103C0 File Offset: 0x0000E5C0
			public override IDictionary<string, object> Serialize(DataSourceSettings.JsonSerializableDataSourceSetting jsonSerializableDataSourceSetting)
			{
				IDictionary<string, object> dictionary = new Dictionary<string, object>();
				dictionary.Add("kind", jsonSerializableDataSourceSetting.DataSource.Kind);
				dictionary.Add("path", jsonSerializableDataSourceSetting.DataSource.Path);
				if (jsonSerializableDataSourceSetting.Setting.AuthenticationKind != null)
				{
					dictionary.Add("AuthenticationKind", jsonSerializableDataSourceSetting.Setting.AuthenticationKind);
				}
				foreach (KeyValuePair<string, object> keyValuePair in jsonSerializableDataSourceSetting.Setting.AuthenticationProperties)
				{
					dictionary.Add(keyValuePair);
				}
				if (jsonSerializableDataSourceSetting.Setting.PrivacySetting != null)
				{
					dictionary.Add("PrivacySetting", jsonSerializableDataSourceSetting.Setting.PrivacySetting);
				}
				if (jsonSerializableDataSourceSetting.Setting.PrivateGroupName != null)
				{
					dictionary.Add("PrivateGroupName", jsonSerializableDataSourceSetting.Setting.PrivateGroupName);
				}
				if (jsonSerializableDataSourceSetting.Setting.IsTrusted != null)
				{
					dictionary.Add("IsTrusted", jsonSerializableDataSourceSetting.Setting.IsTrusted);
				}
				if (jsonSerializableDataSourceSetting.Setting.HasPermissions())
				{
					foreach (IGrouping<string, object> grouping in jsonSerializableDataSourceSetting.Setting.Permissions.GroupBy((MashupPermission perm) => perm.Kind, delegate(MashupPermission perm)
					{
						if (perm.Properties.Count == 0)
						{
							return perm.Value;
						}
						return new Dictionary<string, object>(perm.Properties) { { "Value", perm.Value } };
					}))
					{
						dictionary.Add(grouping.Key, grouping.ToArray<object>());
					}
				}
				return dictionary;
			}

			// Token: 0x0600045D RID: 1117 RVA: 0x00010588 File Offset: 0x0000E788
			public override DataSourceSettings.JsonSerializableDataSourceSetting Deserialize(IDictionary<string, object> dictionary)
			{
				string text = null;
				string text2 = null;
				string text3 = null;
				IDictionary<string, object> dictionary2 = new Dictionary<string, object>();
				string text4 = null;
				string text5 = null;
				bool? flag = null;
				MashupPermissionSet mashupPermissionSet = new MashupPermissionSet();
				foreach (KeyValuePair<string, object> keyValuePair in dictionary)
				{
					if ("kind".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text = DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(keyValuePair.Value);
					}
					else if ("path".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text2 = DataSourceSettings.DataSourceSettingsJsonConverter.GetNullable<string>(keyValuePair.Value);
					}
					else if ("AuthenticationKind".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text3 = Util.NormalizeToKnownValue(AuthenticationKind.KnownAuthKinds, DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(keyValuePair.Value));
					}
					else if ("PrivacySetting".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text4 = Util.NormalizeToKnownValue(PrivacyGroup.KnownPrivacyGroups, DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(keyValuePair.Value));
					}
					else if ("PrivateGroupName".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						text5 = DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(keyValuePair.Value);
					}
					else if ("IsTrusted".Equals(keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
					{
						flag = DataSourceSettings.DataSourceSettingsJsonConverter.GetNullableStruct<bool>(keyValuePair.Value);
					}
					else
					{
						string text6;
						if (Util.TryNormalizeToKnownValue(MashupPermissionKind.KnownPermissionKinds, keyValuePair.Key, out text6))
						{
							using (IEnumerator enumerator2 = DataSourceSettings.DataSourceSettingsJsonConverter.Get<IEnumerable>(keyValuePair.Value).GetEnumerator())
							{
								while (enumerator2.MoveNext())
								{
									object obj = enumerator2.Current;
									IDictionary<string, object> dictionary3 = obj as IDictionary<string, object>;
									if (dictionary3 != null)
									{
										dictionary3 = new Dictionary<string, object>(dictionary3);
										object obj2;
										if (!dictionary3.TryGetValue("Value", out obj2))
										{
											throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
										}
										dictionary3.Remove("Value");
										mashupPermissionSet.Add(new MashupPermission(text6, DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(obj2), dictionary3));
									}
									else
									{
										mashupPermissionSet.Add(new MashupPermission(text6, DataSourceSettings.DataSourceSettingsJsonConverter.Get<string>(obj)));
									}
								}
								continue;
							}
						}
						dictionary2.Add(Util.NormalizeToKnownValue(CredentialProperty.KnownCredentialProperties, keyValuePair.Key), keyValuePair.Value);
					}
				}
				if (text == null || (text3 == null && dictionary2.Any<KeyValuePair<string, object>>()))
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
				}
				DataSourceSetting dataSourceSetting = new DataSourceSetting
				{
					AuthenticationKind = text3,
					AuthenticationProperties = dictionary2,
					PrivacySetting = text4,
					PrivateGroupName = text5,
					IsTrusted = flag
				};
				if (mashupPermissionSet.Any<MashupPermission>())
				{
					dataSourceSetting.Permissions = mashupPermissionSet;
				}
				DataSource dataSource = ((text2 != null) ? new DataSource(text, text2) : DataSource.DefaultForKind(text));
				return new DataSourceSettings.JsonSerializableDataSourceSetting
				{
					DataSource = dataSource,
					Setting = dataSourceSetting
				};
			}

			// Token: 0x0600045E RID: 1118 RVA: 0x00010878 File Offset: 0x0000EA78
			private static T Get<T>(object value) where T : class
			{
				T t = value as T;
				if (t == null)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
				}
				return t;
			}

			// Token: 0x0600045F RID: 1119 RVA: 0x00010898 File Offset: 0x0000EA98
			private static T GetNullable<T>(object value) where T : class
			{
				if (value == null)
				{
					return default(T);
				}
				T t = value as T;
				if (t == null)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
				}
				return t;
			}

			// Token: 0x06000460 RID: 1120 RVA: 0x000108D0 File Offset: 0x0000EAD0
			private static T? GetNullableStruct<T>(object value) where T : struct
			{
				if (value == null)
				{
					return null;
				}
				T? t = value as T?;
				if (t == null)
				{
					throw new MashupException(ProviderErrorStrings.NotExpectedJsonFormat);
				}
				return t;
			}

			// Token: 0x04000220 RID: 544
			private const string KindKeyword = "kind";

			// Token: 0x04000221 RID: 545
			private const string PathKeyword = "path";

			// Token: 0x04000222 RID: 546
			private const string AuthenticationKindKeyword = "AuthenticationKind";

			// Token: 0x04000223 RID: 547
			private const string PrivacySettingKeyword = "PrivacySetting";

			// Token: 0x04000224 RID: 548
			private const string PrivateGroupNameKeyword = "PrivateGroupName";

			// Token: 0x04000225 RID: 549
			private const string IsTrustedKeyword = "IsTrusted";
		}
	}
}

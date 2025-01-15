using System;
using System.Collections;
using System.Configuration;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.CommonGlobals;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x02000507 RID: 1287
	public class Globals
	{
		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x06002B38 RID: 11064 RVA: 0x00095073 File Offset: 0x00093273
		public static CommonHISEventLogging HISEventLogging
		{
			get
			{
				return new CommonHISEventLogging();
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x06002B39 RID: 11065 RVA: 0x0009507A File Offset: 0x0009327A
		public static CommonHISTracing HISTracing
		{
			get
			{
				return new CommonHISTracing(Globals.internalCommonHISTracing.Refresh(), Globals.internalCommonHISTracing);
			}
		}

		// Token: 0x06002B3A RID: 11066 RVA: 0x00095090 File Offset: 0x00093290
		public static object GetConfigValue(XmlNode reNode, string attrName, object defaultValue)
		{
			XmlAttribute xmlAttribute = reNode.Attributes[attrName];
			object obj = null;
			if (xmlAttribute != null)
			{
				obj = xmlAttribute.Value;
			}
			if (obj == null)
			{
				obj = defaultValue;
			}
			else if (defaultValue.GetType() == typeof(int))
			{
				int num;
				if (!int.TryParse((string)obj, out num))
				{
					num = (int)defaultValue;
				}
				obj = num;
			}
			return obj;
		}

		// Token: 0x06002B3B RID: 11067 RVA: 0x000950F4 File Offset: 0x000932F4
		public static Hashtable GetSpecialOverrides()
		{
			TIConfigContainer ticonfigContainer = null;
			try
			{
				if (ticonfigContainer == null)
				{
					ticonfigContainer = ConfigurationManager.GetSection("HostIntegration.TI") as TIConfigContainer;
				}
			}
			catch (Exception ex)
			{
				string message = ex.Message;
				ticonfigContainer = null;
			}
			if (ticonfigContainer != null)
			{
				return Globals.GetSpecialOverridesFromConfigFile(ticonfigContainer);
			}
			return Globals.GetSpecialOverridesFromRegistry();
		}

		// Token: 0x06002B3C RID: 11068 RVA: 0x00095144 File Offset: 0x00093344
		public static Hashtable GetSpecialOverridesFromXML(XmlNode section)
		{
			Hashtable hashtable = new Hashtable();
			foreach (object obj in section.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.Name == "RuntimeOverrides")
				{
					foreach (object obj2 in xmlNode.Attributes)
					{
						XmlAttribute xmlAttribute = (XmlAttribute)obj2;
						string name = xmlAttribute.Name;
						if (name != null)
						{
							uint num = <f611a610-6473-4894-8d18-e7e7c7ed19bb><PrivateImplementationDetails>.ComputeStringHash(name);
							if (num <= 1472710447U)
							{
								if (num <= 665351872U)
								{
									if (num <= 381002343U)
									{
										if (num != 85484543U)
										{
											if (num == 381002343U)
											{
												if (name == "SourceTPNameOverride")
												{
													if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
													{
														hashtable.Add(name, true);
													}
												}
											}
										}
										else if (name == "ConvertReceivedStringsAsIs")
										{
											if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
											{
												hashtable.Add(name, true);
											}
										}
									}
									else if (num != 435718286U)
									{
										if (num == 665351872U)
										{
											if (name == "AcceptNullPacked")
											{
												if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
												{
													hashtable.Add(name, true);
												}
											}
										}
									}
									else if (name == "AcceptNullZoned")
									{
										if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
										{
											hashtable.Add(name, true);
										}
									}
								}
								else if (num <= 1337056993U)
								{
									if (num != 1157278801U)
									{
										if (num == 1337056993U)
										{
											if (name == "TrimTrailingNulls")
											{
												if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
												{
													hashtable.Add(name, true);
												}
											}
										}
									}
									else if (name == "AcceptBadCOMP3Sign")
									{
										if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
										{
											hashtable.Add(name, true);
										}
									}
								}
								else if (num != 1448937169U)
								{
									if (num == 1472710447U)
									{
										if (name == "AcceptAllInvalidNumerics")
										{
											if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
											{
												hashtable.Add(name, true);
											}
										}
									}
								}
								else if (name == "CloseAbandonedGracefully")
								{
									if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
									{
										hashtable.Add(name, true);
									}
								}
							}
							else if (num <= 2461183970U)
							{
								if (num <= 2250068866U)
								{
									if (num != 1738640170U)
									{
										if (num == 2250068866U)
										{
											if (name == "CustomREDispenser")
											{
												string text = xmlAttribute.Value;
												if (text.Length < 1)
												{
													throw new Exception("Configuration file HostIntegration.TI/RuntimeOverrides section entry named " + name + " is missing FullName entry.");
												}
												hashtable.Add(name, text);
											}
										}
									}
									else if (name == "AllowNullRedefines")
									{
										if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
										{
											hashtable.Add(name, true);
										}
									}
								}
								else if (num != 2446352278U)
								{
									if (num == 2461183970U)
									{
										if (name == "UseSyncLevel1")
										{
											if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
											{
												hashtable.Add(name, true);
											}
										}
									}
								}
								else if (name == "AlwaysCheckForNull")
								{
									if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
									{
										hashtable.Add(name, true);
									}
								}
							}
							else if (num <= 2718816860U)
							{
								if (num != 2471710861U)
								{
									if (num == 2718816860U)
									{
										if (name == "CallAccountingProcessor")
										{
											string text = xmlAttribute.Value;
											if (text.Length < 1)
											{
												throw new Exception("Configuration file HostIntegration.TI/RuntimeOverrides section entry named " + name + " is missing FullName entry.");
											}
											hashtable.Add(name, text);
										}
									}
								}
								else if (name == "NineCharIMSTran")
								{
									if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
									{
										hashtable.Add(name, true);
									}
								}
							}
							else if (num != 2835719678U)
							{
								if (num == 3911179633U)
								{
									if (name == "PersistentConnections")
									{
										int num2 = int.Parse(xmlAttribute.Value);
										if (num2 > 0)
										{
											hashtable.Add(name, num2);
										}
									}
								}
							}
							else if (name == "StringsAreNullTerminatedAndSpacePadded")
							{
								if (xmlAttribute.Value.ToUpperInvariant() == "TRUE")
								{
									hashtable.Add(name, true);
								}
							}
						}
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06002B3D RID: 11069 RVA: 0x000957F0 File Offset: 0x000939F0
		private static Hashtable GetSpecialOverridesFromConfigFile(object ObjTIConfigContainer)
		{
			return Globals.GetSpecialOverridesFromXML((ObjTIConfigContainer as TIConfigContainer).Section);
		}

		// Token: 0x06002B3E RID: 11070 RVA: 0x00095804 File Offset: 0x00093A04
		private static Hashtable GetSpecialOverridesFromRegistry()
		{
			Hashtable hashtable = new Hashtable();
			RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Cedar\\Defaults", RegistryKeyPermissionCheck.ReadSubTree);
			if (registryKey != null)
			{
				foreach (string text in registryKey.GetSubKeyNames())
				{
					if (text != null)
					{
						uint num = <f611a610-6473-4894-8d18-e7e7c7ed19bb><PrivateImplementationDetails>.ComputeStringHash(text);
						if (num <= 1472710447U)
						{
							if (num <= 665351872U)
							{
								if (num <= 381002343U)
								{
									if (num != 85484543U)
									{
										if (num == 381002343U)
										{
											if (text == "SourceTPNameOverride")
											{
												hashtable.Add(text, "y");
											}
										}
									}
									else if (text == "ConvertReceivedStringsAsIs")
									{
										RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
										object value = registryKey2.GetValue("Activate");
										if (value == null)
										{
											throw new Exception(SR.MissingOverrideValue(text, "Activate"));
										}
										if (value.GetType() != typeof(string))
										{
											throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
										}
										string text2 = (string)value;
										if (text2 != null)
										{
											hashtable.Add(text, "y");
										}
									}
								}
								else if (num != 435718286U)
								{
									if (num == 665351872U)
									{
										if (text == "AcceptNullPacked")
										{
											RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
											object value2 = registryKey2.GetValue("Activate");
											if (value2 == null)
											{
												throw new Exception(SR.MissingOverrideValue(text, "Activate"));
											}
											if (value2.GetType() != typeof(string))
											{
												throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
											}
											string text2 = (string)value2;
											if (text2 != null)
											{
												hashtable.Add(text, "y");
											}
										}
									}
								}
								else if (text == "AcceptNullZoned")
								{
									RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
									object value3 = registryKey2.GetValue("Activate");
									if (value3 == null)
									{
										throw new Exception(SR.MissingOverrideValue(text, "Activate"));
									}
									if (value3.GetType() != typeof(string))
									{
										throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
									}
									string text2 = (string)value3;
									if (text2 != null)
									{
										hashtable.Add(text, "y");
									}
								}
							}
							else if (num <= 1337056993U)
							{
								if (num != 1157278801U)
								{
									if (num == 1337056993U)
									{
										if (text == "TrimTrailingNulls")
										{
											RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
											object value4 = registryKey2.GetValue("Activate");
											if (value4 == null)
											{
												throw new Exception(SR.MissingOverrideValue(text, "Activate"));
											}
											if (value4.GetType() != typeof(string))
											{
												throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
											}
											string text2 = (string)value4;
											if (text2 != null)
											{
												hashtable.Add(text, "y");
											}
										}
									}
								}
								else if (text == "AcceptBadCOMP3Sign")
								{
									RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
									object value5 = registryKey2.GetValue("Activate");
									if (value5 == null)
									{
										throw new Exception(SR.MissingOverrideValue(text, "Activate"));
									}
									if (value5.GetType() != typeof(string))
									{
										throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
									}
									string text2 = (string)value5;
									if (text2 != null)
									{
										hashtable.Add(text, "y");
									}
								}
							}
							else if (num != 1448937169U)
							{
								if (num == 1472710447U)
								{
									if (text == "AcceptAllInvalidNumerics")
									{
										RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
										object value6 = registryKey2.GetValue("Activate");
										if (value6 == null)
										{
											throw new Exception(SR.MissingOverrideValue(text, "Activate"));
										}
										if (value6.GetType() != typeof(string))
										{
											throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
										}
										string text2 = (string)value6;
										if (text2 != null)
										{
											hashtable.Add(text, "y");
										}
									}
								}
							}
							else if (text == "CloseAbandonedGracefully")
							{
								hashtable.Add(text, "y");
							}
						}
						else if (num <= 2471710861U)
						{
							if (num <= 2250068866U)
							{
								if (num != 1738640170U)
								{
									if (num == 2250068866U)
									{
										if (text == "CustomREDispenser")
										{
											RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
											object value7 = registryKey2.GetValue("FullName");
											if (value7 == null)
											{
												throw new Exception(SR.MissingOverrideValue(text, "FullName"));
											}
											if (value7.GetType() != typeof(string))
											{
												throw new Exception(SR.InvalidRegistryEntry(text, "FullName", "string"));
											}
											string text2 = (string)value7;
											if (text2 != null)
											{
												hashtable.Add(text, text2);
											}
										}
									}
								}
								else if (text == "AllowNullRedefines")
								{
									RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
									object value8 = registryKey2.GetValue("Activate");
									if (value8 == null)
									{
										throw new Exception(SR.MissingOverrideValue(text, "Activate"));
									}
									if (value8.GetType() != typeof(string))
									{
										throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
									}
									string text2 = (string)value8;
									if (text2 != null)
									{
										hashtable.Add(text, "y");
									}
								}
							}
							else if (num != 2446352278U)
							{
								if (num == 2471710861U)
								{
									if (text == "NineCharIMSTran")
									{
										hashtable.Add(text, "y");
									}
								}
							}
							else if (text == "AlwaysCheckForNull")
							{
								RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
								object value9 = registryKey2.GetValue("Activate");
								if (value9 == null)
								{
									throw new Exception(SR.MissingOverrideValue(text, "Activate"));
								}
								if (value9.GetType() != typeof(string))
								{
									throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
								}
								string text2 = (string)value9;
								if (text2 != null)
								{
									hashtable.Add(text, "y");
								}
							}
						}
						else if (num <= 2822153128U)
						{
							if (num != 2718816860U)
							{
								if (num == 2822153128U)
								{
									if (text == "SyncLevelOverride")
									{
										RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
										object value10 = registryKey2.GetValue("SyncLevel");
										if (value10 == null)
										{
											throw new Exception(SR.MissingOverrideValue(text, "SyncLevel"));
										}
										if (value10.GetType() != typeof(int))
										{
											throw new Exception(SR.InvalidRegistryEntry(text, "SyncLevel", "int"));
										}
										if ((int)value10 == 1)
										{
											hashtable.Add("UseSyncLevel1", true);
										}
									}
								}
							}
							else if (text == "CallAccountingProcessor")
							{
								RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
								object value11 = registryKey2.GetValue("FullName");
								if (value11 == null)
								{
									throw new Exception(SR.MissingOverrideValue(text, "FullName"));
								}
								if (value11.GetType() != typeof(string))
								{
									throw new Exception(SR.InvalidRegistryEntry(text, "FullName", "string"));
								}
								string text2 = (string)value11;
								if (text2 != null)
								{
									hashtable.Add(text, text2);
								}
							}
						}
						else if (num != 2835719678U)
						{
							if (num == 3911179633U)
							{
								if (text == "PersistentConnections")
								{
									RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
									int? num2 = new int?((int)registryKey2.GetValue("Timeout"));
									if (num2 != null)
									{
										int value12 = num2.Value;
										hashtable.Add(text, value12);
									}
								}
							}
						}
						else if (text == "StringsAreNullTerminatedAndSpacePadded")
						{
							RegistryKey registryKey2 = registryKey.OpenSubKey(text, RegistryKeyPermissionCheck.ReadSubTree);
							object value13 = registryKey2.GetValue("Activate");
							if (value13 == null)
							{
								throw new Exception(SR.MissingOverrideValue(text, "Activate"));
							}
							if (value13.GetType() != typeof(string))
							{
								throw new Exception(SR.InvalidRegistryEntry(text, "Activate", "string"));
							}
							string text2 = (string)value13;
							if (text2 != null)
							{
								hashtable.Add(text, "y");
							}
						}
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06002B3F RID: 11071 RVA: 0x00096110 File Offset: 0x00094310
		public static Type GetType(string typeFullName)
		{
			if (Globals.RunningInHisInstallation)
			{
				return Type.GetType(typeFullName);
			}
			int num = typeFullName.IndexOf(",");
			string text = typeFullName.Substring(0, num);
			return typeof(Globals).Assembly.GetType(text);
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x06002B40 RID: 11072 RVA: 0x00096155 File Offset: 0x00094355
		// (set) Token: 0x06002B41 RID: 11073 RVA: 0x0009615C File Offset: 0x0009435C
		public static bool RunningInHisInstallation { get; private set; } = typeof(Globals).Assembly.FullName.Contains("Microsoft.HostIntegration.Common.Globals");

		// Token: 0x04001B74 RID: 7028
		public const int MessageIdLength = 11;

		// Token: 0x04001B75 RID: 7029
		private static InternalCommonHISTracing internalCommonHISTracing = new InternalCommonHISTracing();
	}
}

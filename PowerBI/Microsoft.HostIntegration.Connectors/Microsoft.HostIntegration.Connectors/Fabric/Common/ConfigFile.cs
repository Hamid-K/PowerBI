using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Xml;
using System.Xml.XPath;

namespace Microsoft.Fabric.Common
{
	// Token: 0x020003CB RID: 971
	internal class ConfigFile : IConfigurationSectionHandler
	{
		// Token: 0x06002225 RID: 8741 RVA: 0x000693FA File Offset: 0x000675FA
		private ConfigFile()
		{
			this.m_dataTable = new Hashtable();
		}

		// Token: 0x06002226 RID: 8742 RVA: 0x0006940D File Offset: 0x0006760D
		public static void SetExternalConfigFilePath(string path)
		{
			ConfigFile.s_externalConfigFilePath = path;
		}

		// Token: 0x06002227 RID: 8743 RVA: 0x00069415 File Offset: 0x00067615
		public object GetValue(string key, bool needsFallback)
		{
			if (needsFallback)
			{
				return this.getValueWithFallback(key);
			}
			return this.GetValue(key);
		}

		// Token: 0x06002228 RID: 8744 RVA: 0x0006942C File Offset: 0x0006762C
		public string GetStringValue(string key, bool needsFallback, string defaultValue)
		{
			string text = (string)this.GetValue(key, needsFallback);
			if (text == null)
			{
				text = defaultValue;
			}
			return text;
		}

		// Token: 0x06002229 RID: 8745 RVA: 0x0006944D File Offset: 0x0006764D
		public int GetIntValue(string key, bool needsFallback, int defaultValue)
		{
			return Utility.ToInt(this.GetStringValue(key, needsFallback, null), defaultValue);
		}

		// Token: 0x0600222A RID: 8746 RVA: 0x0006945E File Offset: 0x0006765E
		public object GetValue(string key)
		{
			if (key == null)
			{
				return this.m_dataTable;
			}
			return this.m_dataTable[key];
		}

		// Token: 0x0600222B RID: 8747 RVA: 0x00069478 File Offset: 0x00067678
		public T GetValue<T>(string name, T defaultValue)
		{
			object obj = this.GetValue(name);
			if (obj == null)
			{
				return defaultValue;
			}
			string text = obj as string;
			if (text == null)
			{
				return (T)((object)obj);
			}
			Type typeFromHandle = typeof(T);
			if (typeFromHandle == typeof(int))
			{
				obj = int.Parse(text, CultureInfo.InvariantCulture);
			}
			else if (typeFromHandle == typeof(long))
			{
				obj = long.Parse(text, CultureInfo.InvariantCulture);
			}
			else if (typeFromHandle == typeof(double))
			{
				obj = double.Parse(text, CultureInfo.InvariantCulture);
			}
			else if (typeFromHandle == typeof(bool))
			{
				obj = bool.Parse(text);
			}
			else if (typeFromHandle == typeof(TimeSpan))
			{
				double num = double.Parse(text, CultureInfo.InvariantCulture);
				obj = TimeSpan.FromSeconds(num);
			}
			else if (typeFromHandle != typeof(string))
			{
				obj = defaultValue;
			}
			return (T)((object)obj);
		}

		// Token: 0x0600222C RID: 8748 RVA: 0x00069570 File Offset: 0x00067770
		private object getValueWithFallback(string key)
		{
			if (key == null)
			{
				return this.m_dataTable;
			}
			for (;;)
			{
				object obj = this.m_dataTable[key];
				if (obj == null)
				{
					int num = key.LastIndexOf('/');
					if (num <= 0 || num + 1 >= key.Length)
					{
						break;
					}
					int num2 = key.LastIndexOf('/', num - 1, num);
					key = key.Remove(num2 + 1, num - num2);
				}
				if (obj != null)
				{
					return obj;
				}
			}
			return null;
		}

		// Token: 0x0600222D RID: 8749 RVA: 0x000695D0 File Offset: 0x000677D0
		private static Hashtable createHashtable(XmlReader reader)
		{
			bool flag = true;
			string attribute = reader.GetAttribute("caseSensitive", "");
			if (attribute != null)
			{
				flag = Utility.IsAffirm(attribute);
			}
			Hashtable hashtable;
			if (flag)
			{
				hashtable = new Hashtable();
			}
			else
			{
				hashtable = new Hashtable(StringComparer.CurrentCultureIgnoreCase);
			}
			return hashtable;
		}

		// Token: 0x0600222E RID: 8750 RVA: 0x00069611 File Offset: 0x00067811
		private void Load(XmlReader reader)
		{
			this.m_dataTable = ConfigFile.createHashtable(reader);
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element && reader.Name == "section")
				{
					this.recursiveWalk(reader, "");
				}
			}
		}

		// Token: 0x0600222F RID: 8751 RVA: 0x00069650 File Offset: 0x00067850
		private void recursiveWalk(XmlReader reader, string path)
		{
			if (path.Length > 0)
			{
				path += "/";
			}
			path += reader.GetAttribute("name", "");
			if (reader.Name != "section")
			{
				object configValue = this.getConfigValue(reader);
				if (configValue != null)
				{
					this.m_dataTable[path] = configValue;
					return;
				}
			}
			else
			{
				string attribute = reader.GetAttribute("path", "");
				if (attribute != null)
				{
					path = attribute;
				}
				string name = reader.Name;
				while (reader.Read())
				{
					XmlNodeType nodeType = reader.NodeType;
					if (nodeType != XmlNodeType.Element)
					{
						if (nodeType == XmlNodeType.EndElement)
						{
							if (reader.Name == name)
							{
								return;
							}
						}
					}
					else
					{
						this.recursiveWalk(reader, path);
					}
				}
			}
		}

		// Token: 0x06002230 RID: 8752 RVA: 0x0006970C File Offset: 0x0006790C
		private object getConfigValue(XmlReader reader)
		{
			string name = reader.Name;
			string text;
			if ((text = name) != null)
			{
				if (text == "key")
				{
					string attribute = reader.GetAttribute("value", "");
					if (!reader.IsEmptyElement)
					{
						reader.Skip();
					}
					return attribute;
				}
				if (text == "varType")
				{
					Dictionary<string, string> dictionary = new Dictionary<string, string>();
					if (reader.HasAttributes)
					{
						for (int i = 0; i < reader.AttributeCount; i++)
						{
							reader.MoveToAttribute(i);
							dictionary[reader.Name] = reader.Value;
						}
						reader.MoveToElement();
					}
					return dictionary;
				}
				if (text == "collection")
				{
					return this.getCollection(reader);
				}
			}
			return this.getCustomType(reader);
		}

		// Token: 0x06002231 RID: 8753 RVA: 0x000697C8 File Offset: 0x000679C8
		private IEnumerable getCollection(XmlReader reader)
		{
			Hashtable hashtable = null;
			ArrayList arrayList = null;
			string attribute = reader.GetAttribute("collectionType", "");
			if (string.Compare(attribute, "list", StringComparison.OrdinalIgnoreCase) == 0)
			{
				arrayList = new ArrayList();
			}
			else
			{
				hashtable = ConfigFile.createHashtable(reader);
			}
			while (reader.Read())
			{
				if (reader.NodeType == XmlNodeType.Element)
				{
					string attribute2 = reader.GetAttribute("name", "");
					object configValue = this.getConfigValue(reader);
					if (configValue != null)
					{
						if (arrayList != null)
						{
							arrayList.Add(configValue);
						}
						else
						{
							hashtable[attribute2] = configValue;
						}
					}
				}
				else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "collection")
				{
					break;
				}
			}
			if (arrayList != null)
			{
				return arrayList;
			}
			return hashtable;
		}

		// Token: 0x06002232 RID: 8754 RVA: 0x00069874 File Offset: 0x00067A74
		private static bool setFieldValue(object obj, string fieldName, object val)
		{
			FieldInfo field = obj.GetType().GetField(fieldName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			if (val is string && !field.FieldType.Equals(typeof(string)))
			{
				MethodInfo method = field.FieldType.GetMethod("Parse", BindingFlags.Static | BindingFlags.Public, null, new Type[] { typeof(string) }, null);
				val = method.Invoke(null, new object[] { val });
			}
			field.SetValue(obj, val);
			return true;
		}

		// Token: 0x06002233 RID: 8755 RVA: 0x000698F8 File Offset: 0x00067AF8
		private object getCustomType(XmlReader reader)
		{
			string name = reader.Name;
			string text;
			if (name == "customType")
			{
				text = reader.GetAttribute("className", "");
			}
			else
			{
				text = name;
			}
			object obj = Utility.CreateInstanceByReflection(text);
			if (obj == null)
			{
				return null;
			}
			ICustomType customType = obj as ICustomType;
			if (customType != null)
			{
				if (!customType.Load(reader))
				{
					EventLogWriter.WriteError("ConfigFile", "Unable to load customType of class name: " + text, new object[0]);
					return null;
				}
			}
			else
			{
				if (reader.HasAttributes)
				{
					for (int i = 0; i < reader.AttributeCount; i++)
					{
						reader.MoveToAttribute(i);
						if (reader.Name != "className" && reader.Name != "name")
						{
							ConfigFile.setFieldValue(obj, reader.Name, reader.Value);
						}
					}
					reader.MoveToElement();
				}
				if (!reader.IsEmptyElement)
				{
					while (reader.Read())
					{
						if (reader.NodeType == XmlNodeType.Element)
						{
							string attribute = reader.GetAttribute("name", "");
							object configValue = this.getConfigValue(reader);
							if (configValue == null)
							{
								if (reader.Name.Length != 0)
								{
									ConfigFile.setFieldValue(obj, reader.Name, reader.ReadInnerXml());
								}
							}
							else
							{
								ConfigFile.setFieldValue(obj, attribute, configValue);
							}
						}
						else if (reader.NodeType == XmlNodeType.EndElement && reader.Name == name)
						{
							break;
						}
					}
				}
			}
			return obj;
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x06002234 RID: 8756 RVA: 0x00069A54 File Offset: 0x00067C54
		public static ConfigFile Config
		{
			get
			{
				if (ConfigFile.s_theConfig == null)
				{
					lock (ConfigFile.s_lockObject)
					{
						ConfigFile.s_theConfig = (ConfigFile)ConfigurationManager.GetSection("fabric");
						if (ConfigFile.s_theConfig == null)
						{
							ConfigFile configFile = new ConfigFile();
							try
							{
								configFile.Load();
							}
							catch (FileNotFoundException)
							{
							}
							ConfigFile.s_theConfig = configFile;
						}
					}
				}
				return ConfigFile.s_theConfig;
			}
		}

		// Token: 0x06002235 RID: 8757 RVA: 0x00069AD0 File Offset: 0x00067CD0
		private void Load()
		{
			string text = ConfigFile.s_externalConfigFilePath;
			if (string.IsNullOrEmpty(text))
			{
				text = Environment.GetEnvironmentVariable("FabricConfigFile");
			}
			if (string.IsNullOrEmpty(text))
			{
				if (File.Exists("fabric.config"))
				{
					text = "fabric.config";
				}
				else
				{
					string location = base.GetType().Assembly.Location;
					text = Path.Combine(Path.GetDirectoryName(location), "fabric.config");
					if (!File.Exists(text))
					{
						throw new FileNotFoundException("Can't find config file");
					}
				}
			}
			using (XmlTextReader xmlTextReader = new XmlTextReader(text))
			{
				this.Load(xmlTextReader);
			}
		}

		// Token: 0x06002236 RID: 8758 RVA: 0x00069B74 File Offset: 0x00067D74
		public object Create(object parent, object configContext, XmlNode section)
		{
			if (section == null)
			{
				throw new ArgumentNullException("section");
			}
			ConfigFile configFile = new ConfigFile();
			XPathNavigator xpathNavigator = section.CreateNavigator();
			using (XmlReader xmlReader = xpathNavigator.ReadSubtree())
			{
				configFile.Load(xmlReader);
			}
			return configFile;
		}

		// Token: 0x0400159D RID: 5533
		private const string s_theConfigSectionName = "fabric";

		// Token: 0x0400159E RID: 5534
		private const string s_externalConfigFile = "fabric.config";

		// Token: 0x0400159F RID: 5535
		private Hashtable m_dataTable;

		// Token: 0x040015A0 RID: 5536
		private static ConfigFile s_theConfig;

		// Token: 0x040015A1 RID: 5537
		private static string s_externalConfigFilePath = null;

		// Token: 0x040015A2 RID: 5538
		private static object s_lockObject = new object();
	}
}

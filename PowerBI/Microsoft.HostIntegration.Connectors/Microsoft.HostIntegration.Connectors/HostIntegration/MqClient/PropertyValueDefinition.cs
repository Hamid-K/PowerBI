using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using Microsoft.HostIntegration.MqClient.StrictResources.ClassLibrary;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000B42 RID: 2882
	public class PropertyValueDefinition
	{
		// Token: 0x170015FB RID: 5627
		// (get) Token: 0x06005B04 RID: 23300 RVA: 0x00176A93 File Offset: 0x00174C93
		// (set) Token: 0x06005B05 RID: 23301 RVA: 0x00176A9B File Offset: 0x00174C9B
		internal string FullName { get; private set; }

		// Token: 0x170015FC RID: 5628
		// (get) Token: 0x06005B06 RID: 23302 RVA: 0x00176AA4 File Offset: 0x00174CA4
		// (set) Token: 0x06005B07 RID: 23303 RVA: 0x00176AAC File Offset: 0x00174CAC
		public string Name { get; private set; }

		// Token: 0x170015FD RID: 5629
		// (get) Token: 0x06005B08 RID: 23304 RVA: 0x00176AB5 File Offset: 0x00174CB5
		// (set) Token: 0x06005B09 RID: 23305 RVA: 0x00176ABD File Offset: 0x00174CBD
		public PropertyType Type { get; internal set; }

		// Token: 0x170015FE RID: 5630
		// (get) Token: 0x06005B0A RID: 23306 RVA: 0x00176AC6 File Offset: 0x00174CC6
		// (set) Token: 0x06005B0B RID: 23307 RVA: 0x00176ACE File Offset: 0x00174CCE
		public PropertyContext Context
		{
			get
			{
				return this.context;
			}
			set
			{
				if (value != this.context)
				{
					if (this.Parent != null)
					{
						this.Parent.PropertyValueUpdated();
					}
					this.context = value;
				}
			}
		}

		// Token: 0x170015FF RID: 5631
		// (get) Token: 0x06005B0C RID: 23308 RVA: 0x00176AF3 File Offset: 0x00174CF3
		// (set) Token: 0x06005B0D RID: 23309 RVA: 0x00176AFC File Offset: 0x00174CFC
		public PropertyCopyOptions? CopyOptions
		{
			get
			{
				return this.copyOptions;
			}
			set
			{
				PropertyCopyOptions? propertyCopyOptions = value;
				PropertyCopyOptions? propertyCopyOptions2 = this.copyOptions;
				if (!((propertyCopyOptions.GetValueOrDefault() == propertyCopyOptions2.GetValueOrDefault()) & (propertyCopyOptions != null == (propertyCopyOptions2 != null))))
				{
					if (this.Parent != null)
					{
						this.Parent.PropertyValueUpdated();
					}
					this.copyOptions = value;
				}
			}
		}

		// Token: 0x17001600 RID: 5632
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x00176B4F File Offset: 0x00174D4F
		// (set) Token: 0x06005B0F RID: 23311 RVA: 0x00176B57 File Offset: 0x00174D57
		public object Value
		{
			get
			{
				return this.propertyValue;
			}
			set
			{
				if (value != this.propertyValue)
				{
					if (this.Parent != null)
					{
						this.Parent.PropertyValueUpdated();
					}
					this.propertyValue = value;
				}
			}
		}

		// Token: 0x17001601 RID: 5633
		// (get) Token: 0x06005B10 RID: 23312 RVA: 0x00176B7C File Offset: 0x00174D7C
		// (set) Token: 0x06005B11 RID: 23313 RVA: 0x00176B84 File Offset: 0x00174D84
		internal Rf2hFolderPropertyCollection Parent { get; set; }

		// Token: 0x17001602 RID: 5634
		// (get) Token: 0x06005B12 RID: 23314 RVA: 0x00176B8D File Offset: 0x00174D8D
		// (set) Token: 0x06005B13 RID: 23315 RVA: 0x00176B95 File Offset: 0x00174D95
		internal Rf2hFolderType InsertIntoFolderType { get; private set; }

		// Token: 0x17001603 RID: 5635
		// (get) Token: 0x06005B14 RID: 23316 RVA: 0x00176B9E File Offset: 0x00174D9E
		// (set) Token: 0x06005B15 RID: 23317 RVA: 0x00176BA6 File Offset: 0x00174DA6
		internal string InsertIntoPropertiesFolderTag { get; private set; }

		// Token: 0x06005B16 RID: 23318 RVA: 0x00176BB0 File Offset: 0x00174DB0
		internal PropertyValueDefinition(Rf2hFolderWithFieldsAndProperties futureParent, XmlNode valueNode, string fullName)
		{
			this.InsertIntoFolderType = futureParent.FolderType;
			this.InsertIntoPropertiesFolderTag = futureParent.FolderTag;
			XmlAttribute xmlAttribute = null;
			XmlAttribute xmlAttribute2 = null;
			XmlAttribute xmlAttribute3 = null;
			XmlAttribute xmlAttribute4 = null;
			XmlNode xmlNode = ((valueNode.NodeType != XmlNodeType.Text) ? valueNode : valueNode.ParentNode);
			if (xmlNode.Attributes != null)
			{
				xmlAttribute = xmlNode.Attributes["context"];
				xmlAttribute2 = xmlNode.Attributes["copy"];
				xmlAttribute3 = xmlNode.Attributes["dt"];
				xmlAttribute4 = xmlNode.Attributes["xsi:nil"];
			}
			string text = ((xmlAttribute3 == null) ? "string" : xmlAttribute3.Value.ToLowerInvariant());
			PropertyType propertyType = PropertyValueDefinition.dataTypesToPropertyType[text];
			if (xmlAttribute != null)
			{
				this.context = PropertyValueDefinition.contextsToPropertyContext[xmlAttribute.Value.ToLowerInvariant()];
			}
			if (xmlAttribute2 != null)
			{
				PropertyCopyOptions propertyCopyOptions = PropertyCopyOptions.None;
				foreach (string text2 in xmlAttribute2.Value.ToLowerInvariant().Split(new char[] { ',' }))
				{
					propertyCopyOptions |= PropertyValueDefinition.copysToCopyOption[text2];
				}
				this.copyOptions = new PropertyCopyOptions?(propertyCopyOptions);
			}
			if (this.InsertIntoFolderType == Rf2hFolderType.Usr)
			{
				this.Name = fullName.Substring(4);
			}
			else
			{
				this.Name = fullName;
			}
			this.FullName = fullName;
			this.Type = propertyType;
			if (xmlAttribute4 != null)
			{
				return;
			}
			if (string.IsNullOrEmpty(valueNode.InnerText))
			{
				if (propertyType == PropertyType.BinHex)
				{
					this.Value = new byte[0];
					return;
				}
				if (propertyType != PropertyType.String)
				{
					throw new CustomMqClientException(SR.EmptyPropertyValueXml);
				}
				this.Value = string.Empty;
				return;
			}
			else
			{
				string innerText = valueNode.InnerText;
				switch (propertyType)
				{
				case PropertyType.BinHex:
					this.Value = this.ParseHexString(innerText);
					return;
				case PropertyType.Boolean:
				{
					string text3 = innerText.ToLowerInvariant();
					if (text3 == "false" || text3 == "0")
					{
						this.Value = false;
						return;
					}
					if (text3 == "true" || text3 == "1")
					{
						this.Value = true;
						return;
					}
					throw new CustomMqClientException(SR.UnknownBoolXml);
				}
				case PropertyType.I1:
					this.Value = sbyte.Parse(innerText);
					return;
				case PropertyType.I2:
					this.Value = short.Parse(innerText);
					return;
				case PropertyType.I4:
					this.Value = int.Parse(innerText);
					return;
				case PropertyType.I8:
					this.Value = long.Parse(innerText);
					return;
				case PropertyType.R4:
					this.Value = float.Parse(innerText);
					return;
				case PropertyType.R8:
					this.Value = double.Parse(innerText);
					return;
				case PropertyType.String:
					this.Value = innerText;
					return;
				default:
					throw new InvalidOperationException("BUGBUG: propertyType is not one of the enums");
				}
			}
		}

		// Token: 0x06005B17 RID: 23319 RVA: 0x00176E8D File Offset: 0x0017508D
		public PropertyValueDefinition(string fullName, byte[] propertyValue)
			: this(fullName, PropertyType.BinHex, propertyValue)
		{
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x00176E98 File Offset: 0x00175098
		public PropertyValueDefinition(string fullName, bool propertyValue)
			: this(fullName, PropertyType.Boolean, propertyValue)
		{
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x00176EA8 File Offset: 0x001750A8
		public PropertyValueDefinition(string fullName, byte propertyValue)
			: this(fullName, PropertyType.I1, propertyValue)
		{
		}

		// Token: 0x06005B1A RID: 23322 RVA: 0x00176EB8 File Offset: 0x001750B8
		public PropertyValueDefinition(string fullName, short propertyValue)
			: this(fullName, PropertyType.I2, propertyValue)
		{
		}

		// Token: 0x06005B1B RID: 23323 RVA: 0x00176EC8 File Offset: 0x001750C8
		public PropertyValueDefinition(string fullName, int propertyValue)
			: this(fullName, PropertyType.I4, propertyValue)
		{
		}

		// Token: 0x06005B1C RID: 23324 RVA: 0x00176ED8 File Offset: 0x001750D8
		public PropertyValueDefinition(string fullName, long propertyValue)
			: this(fullName, PropertyType.I8, propertyValue)
		{
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x00176EE8 File Offset: 0x001750E8
		public PropertyValueDefinition(string fullName, float propertyValue)
			: this(fullName, PropertyType.R4, propertyValue)
		{
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x00176EF8 File Offset: 0x001750F8
		public PropertyValueDefinition(string fullName, double propertyValue)
			: this(fullName, PropertyType.R8, propertyValue)
		{
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x00176F08 File Offset: 0x00175108
		public PropertyValueDefinition(string fullName, string propertyValue)
			: this(fullName, PropertyType.String, propertyValue)
		{
		}

		// Token: 0x06005B20 RID: 23328 RVA: 0x00176F14 File Offset: 0x00175114
		private PropertyValueDefinition(string fullName, PropertyType propertyType, object propertyValue)
		{
			if (string.IsNullOrWhiteSpace(fullName))
			{
				throw new ArgumentNullException("fullName");
			}
			PropertyValueDefinition.CheckPropertyName(fullName);
			string text = null;
			Rf2hFolderType rf2hFolderType = Rf2hFolderType.Properties;
			PropertyValueDefinition.GetFolderTypeAndName(ref fullName, out rf2hFolderType, out text);
			if (!this.PropertyNameAllowed(rf2hFolderType, fullName))
			{
				throw new CustomMqClientException(SR.PropertyDotted);
			}
			if (rf2hFolderType == Rf2hFolderType.Usr)
			{
				this.Name = fullName.Substring(4);
			}
			else
			{
				this.Name = fullName;
			}
			this.FullName = fullName;
			this.Type = propertyType;
			this.Value = propertyValue;
			this.InsertIntoFolderType = rf2hFolderType;
			this.InsertIntoPropertiesFolderTag = text;
		}

		// Token: 0x06005B21 RID: 23329 RVA: 0x00176FA8 File Offset: 0x001751A8
		internal static void CheckPropertyName(string fullName)
		{
			if (fullName[0] == '.')
			{
				throw new CustomMqClientException(SR.PropertyNameInvalid);
			}
			if (fullName[fullName.Length - 1] == '.')
			{
				throw new CustomMqClientException(SR.PropertyNameInvalid);
			}
			if (fullName.IndexOf("..") != -1)
			{
				throw new CustomMqClientException(SR.PropertyNameInvalid);
			}
		}

		// Token: 0x06005B22 RID: 23330 RVA: 0x00177004 File Offset: 0x00175204
		private bool PropertyNameAllowed(Rf2hFolderType folderType, string propertyName)
		{
			if (folderType == Rf2hFolderType.Properties)
			{
				return true;
			}
			int num = propertyName.IndexOf('.');
			if (num == -1)
			{
				throw new Exception("BUGBUG: there is no '.' in property name");
			}
			int num2 = propertyName.LastIndexOf('.');
			return num == num2;
		}

		// Token: 0x06005B23 RID: 23331 RVA: 0x0017703C File Offset: 0x0017523C
		internal static void GetFolderTypeAndName(ref string propertyName, out Rf2hFolderType folderType, out string propertiesFolderName)
		{
			int num = propertyName.IndexOf('.');
			propertiesFolderName = null;
			folderType = Rf2hFolderType.Properties;
			if (num == -1)
			{
				folderType = Rf2hFolderType.Usr;
				propertiesFolderName = "usr";
				propertyName = "usr." + propertyName;
				return;
			}
			string text = propertyName.Substring(0, num);
			string text2 = text.ToLowerInvariant();
			if (text2 != null)
			{
				if (text2 == "usr")
				{
					throw new CustomMqClientException(SR.UsrPropertyDotted);
				}
				if (text2 == "mq_usr")
				{
					folderType = Rf2hFolderType.Mq_Usr;
					propertiesFolderName = "mq_usr";
					return;
				}
				if (text2 == "jms" || text2 == "mcd")
				{
					throw new CustomMqClientException(SR.PropertyNotInJmsMcd);
				}
			}
			propertiesFolderName = text;
		}

		// Token: 0x06005B24 RID: 23332 RVA: 0x001770E4 File Offset: 0x001752E4
		private sbyte[] ParseHexString(string hexString)
		{
			sbyte[] array = new sbyte[hexString.Length / 2];
			int i = 0;
			int num = 0;
			while (i < hexString.Length)
			{
				byte b = (byte)hexString[i++];
				if (b > 96)
				{
					b -= 87;
				}
				else if (b > 64)
				{
					b -= 55;
				}
				else
				{
					b -= 48;
				}
				byte b2 = (byte)hexString[i++];
				if (b2 > 96)
				{
					b2 -= 87;
				}
				else if (b2 > 64)
				{
					b2 -= 55;
				}
				else
				{
					b2 -= 48;
				}
				array[num++] = (sbyte)(((int)b << 4) + (int)b2);
			}
			return array;
		}

		// Token: 0x06005B25 RID: 23333 RVA: 0x00177180 File Offset: 0x00175380
		internal void AddToXml(StringBuilder sb)
		{
			int num = this.FullName.LastIndexOf('.');
			string text = this.FullName.Substring(num + 1);
			sb.Append(string.Concat(new string[]
			{
				"<",
				text,
				" dt='",
				PropertyValueDefinition.propertyTypesToDataType[(int)this.Type],
				"' context='",
				PropertyValueDefinition.propertyContextsToContext[(int)this.context],
				"'"
			}));
			if (this.copyOptions != null && this.copyOptions.Value != PropertyCopyOptions.Default)
			{
				sb.Append(" copy='" + PropertyValueDefinition.copyOptionsToCopy[(int)this.copyOptions.Value] + "'");
			}
			if (this.Value == null)
			{
				sb.Append(" xsi:nil='true'");
			}
			sb.Append(">");
			if (this.Value != null)
			{
				PropertyType type = this.Type;
				if (type != PropertyType.BinHex)
				{
					if (type != PropertyType.Boolean)
					{
						sb.Append(this.Value.ToString());
					}
					else
					{
						sb.Append(((bool)this.Value) ? '1' : '0');
					}
				}
				else
				{
					sb.Append(BitConverter.ToString((byte[])this.Value).Replace("-", string.Empty));
				}
			}
			sb.Append("</" + text + ">");
		}

		// Token: 0x040047CC RID: 18380
		public PropertyContext context = PropertyContext.User;

		// Token: 0x040047CD RID: 18381
		private PropertyCopyOptions? copyOptions;

		// Token: 0x040047CE RID: 18382
		private object propertyValue;

		// Token: 0x040047CF RID: 18383
		private static Dictionary<string, PropertyType> dataTypesToPropertyType = new Dictionary<string, PropertyType>
		{
			{
				"bin.hex",
				PropertyType.BinHex
			},
			{
				"boolean",
				PropertyType.Boolean
			},
			{
				"i1",
				PropertyType.I1
			},
			{
				"i2",
				PropertyType.I2
			},
			{
				"i4",
				PropertyType.I4
			},
			{
				"i8",
				PropertyType.I8
			},
			{
				"int",
				PropertyType.I8
			},
			{
				"r4",
				PropertyType.R4
			},
			{
				"r8",
				PropertyType.R8
			},
			{
				"string",
				PropertyType.String
			}
		};

		// Token: 0x040047D0 RID: 18384
		private static string[] propertyTypesToDataType = new string[] { "bin.hex", "boolean", "i1", "i2", "i4", "i8", "r4", "r8", "string" };

		// Token: 0x040047D1 RID: 18385
		private static Dictionary<string, PropertyContext> contextsToPropertyContext = new Dictionary<string, PropertyContext>
		{
			{
				"none",
				PropertyContext.None
			},
			{
				"user",
				PropertyContext.User
			}
		};

		// Token: 0x040047D2 RID: 18386
		private static string[] propertyContextsToContext = new string[] { "none", "user" };

		// Token: 0x040047D3 RID: 18387
		private static Dictionary<string, PropertyCopyOptions> copysToCopyOption = new Dictionary<string, PropertyCopyOptions>
		{
			{
				"none",
				PropertyCopyOptions.None
			},
			{
				"forward",
				PropertyCopyOptions.Forward
			},
			{
				"reply",
				PropertyCopyOptions.Reply
			},
			{
				"report",
				PropertyCopyOptions.Report
			},
			{
				"publish",
				PropertyCopyOptions.Publish
			},
			{
				"all",
				PropertyCopyOptions.All
			},
			{
				"default",
				PropertyCopyOptions.Default
			}
		};

		// Token: 0x040047D4 RID: 18388
		private static string[] copyOptionsToCopy = new string[]
		{
			"none", "forward", "reply", "forward,reply", "report", "report,forward", "report,reply", "report,forward,reply", "publish", "publish,forward",
			"publish,reply", "publish,forward,reply", "publish,report", "default", "publish,report,reply", "all"
		};
	}
}

using System;
using System.Collections;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Exceptions;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public sealed class ExtensionsConfiguration
	{
		// Token: 0x06000195 RID: 405 RVA: 0x00006930 File Offset: 0x00004B30
		static ExtensionsConfiguration()
		{
			ExtensionsConfiguration.m_semanticQueryEngine.Type = "Data";
			ExtensionsConfiguration.m_semanticQueryEngine.Name = "SemanticQueryEngine";
			ExtensionsConfiguration.m_semanticQueryEngine.Assembly = "Microsoft.ReportingServices.SemanticQueryEngine";
			ExtensionsConfiguration.m_semanticQueryEngine.Class = "Microsoft.ReportingServices.SemanticQueryEngine.SemanticQueryConnection";
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x06000196 RID: 406 RVA: 0x00006983 File Offset: 0x00004B83
		public ExtensionsConfiguration.ExtensionArray Delivery
		{
			get
			{
				return this.m_delivery;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x06000197 RID: 407 RVA: 0x0000698B File Offset: 0x00004B8B
		public ExtensionsConfiguration.ExtensionArray DeliveryUI
		{
			get
			{
				return this.m_deliveryUI;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00006993 File Offset: 0x00004B93
		public ExtensionsConfiguration.ExtensionArray Renderer
		{
			get
			{
				return this.m_renderer;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x06000199 RID: 409 RVA: 0x0000699B File Offset: 0x00004B9B
		public ExtensionsConfiguration.ExtensionArray Data
		{
			get
			{
				return this.m_data;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600019A RID: 410 RVA: 0x000069A3 File Offset: 0x00004BA3
		public Extension SemanticQueryEngine
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return ExtensionsConfiguration.m_semanticQueryEngine;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600019B RID: 411 RVA: 0x000069AA File Offset: 0x00004BAA
		public ExtensionsConfiguration.ExtensionArray SemanticQuery
		{
			get
			{
				return this.m_semanticQuery;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x0600019C RID: 412 RVA: 0x000069B2 File Offset: 0x00004BB2
		public ExtensionsConfiguration.ExtensionArray ModelGeneration
		{
			get
			{
				return this.m_modelGeneration;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x0600019D RID: 413 RVA: 0x000069BA File Offset: 0x00004BBA
		public ExtensionsConfiguration.ExtensionArray Security
		{
			get
			{
				return this.m_security;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x0600019E RID: 414 RVA: 0x000069C2 File Offset: 0x00004BC2
		public ExtensionsConfiguration.ExtensionArray Authentication
		{
			get
			{
				return this.m_authentication;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600019F RID: 415 RVA: 0x000069CA File Offset: 0x00004BCA
		public ExtensionsConfiguration.ExtensionArray Event
		{
			get
			{
				return this.m_event;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x000069D2 File Offset: 0x00004BD2
		public ExtensionsConfiguration.ExtensionArray Designer
		{
			get
			{
				return this.m_designer;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x000069DA File Offset: 0x00004BDA
		public ExtensionsConfiguration.ExtensionArray ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x000069E2 File Offset: 0x00004BE2
		public ExtensionsConfiguration.ExtensionArray ReportItemDesigner
		{
			get
			{
				if (this.m_reportItemDesigner == null)
				{
					this.m_reportItemDesigner = new ExtensionsConfiguration.ExtensionArray();
				}
				return this.m_reportItemDesigner;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x000069FD File Offset: 0x00004BFD
		public ArrayList ReportItemConverter
		{
			get
			{
				if (this.m_reportItemConverter == null)
				{
					this.m_reportItemConverter = new ArrayList();
				}
				return this.m_reportItemConverter;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006A18 File Offset: 0x00004C18
		public Extension ReportDefinitionCustomization
		{
			get
			{
				return this.m_reportDefinitionCustomization;
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x00006A20 File Offset: 0x00004C20
		internal void ParseXML(XmlNode node)
		{
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				ExtensionsConfiguration.ExtensionArray extensionArray = null;
				string name = xmlNode.Name;
				if (name == null)
				{
					goto IL_0282;
				}
				int length = name.Length;
				switch (length)
				{
				case 4:
					if (!(name == "Data"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_data;
					break;
				case 5:
				case 7:
				case 9:
				case 12:
				case 16:
				case 17:
					goto IL_0282;
				case 6:
					if (!(name == "Render"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_renderer;
					break;
				case 8:
				{
					char c = name[2];
					if (c != 'c')
					{
						if (c != 'l')
						{
							if (c != 's')
							{
								goto IL_0282;
							}
							if (!(name == "Designer"))
							{
								goto IL_0282;
							}
							extensionArray = this.m_designer;
						}
						else
						{
							if (!(name == "Delivery"))
							{
								goto IL_0282;
							}
							extensionArray = this.m_delivery;
						}
					}
					else
					{
						if (!(name == "Security"))
						{
							goto IL_0282;
						}
						extensionArray = this.m_security;
					}
					break;
				}
				case 10:
					if (!(name == "DeliveryUI"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_deliveryUI;
					break;
				case 11:
					if (!(name == "ReportItems"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_reportItems;
					break;
				case 13:
					if (!(name == "SemanticQuery"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_semanticQuery;
					break;
				case 14:
					if (!(name == "Authentication"))
					{
						goto IL_0282;
					}
					extensionArray = this.m_authentication;
					break;
				case 15:
				{
					char c = name[0];
					if (c != 'E')
					{
						if (c != 'M')
						{
							goto IL_0282;
						}
						if (!(name == "ModelGeneration"))
						{
							goto IL_0282;
						}
						extensionArray = this.m_modelGeneration;
					}
					else
					{
						if (!(name == "EventProcessing"))
						{
							goto IL_0282;
						}
						extensionArray = this.m_event;
					}
					break;
				}
				case 18:
					if (!(name == "ReportItemDesigner"))
					{
						goto IL_0282;
					}
					extensionArray = this.ReportItemDesigner;
					break;
				case 19:
					if (!(name == "ReportItemConverter"))
					{
						goto IL_0282;
					}
					this.ParseReportItemConverters(xmlNode, this.ReportItemConverter);
					extensionArray = null;
					break;
				default:
					if (length != 29)
					{
						goto IL_0282;
					}
					if (!(name == "ReportDefinitionCustomization"))
					{
						goto IL_0282;
					}
					this.ParseReportDefinitionCustomization(xmlNode);
					extensionArray = null;
					break;
				}
				IL_0296:
				if (extensionArray != null)
				{
					this.ParseExtensions(xmlNode, extensionArray);
					continue;
				}
				continue;
				IL_0282:
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
					goto IL_0296;
				}
				goto IL_0296;
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006D0C File Offset: 0x00004F0C
		private void ParseDeliveryExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			bool flag = false;
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					DeliveryExtension deliveryExtension = new DeliveryExtension();
					deliveryExtension.Type = child.Name;
					Extension extension = deliveryExtension;
					this.ParseExtensionElement(xmlNode, ref extension);
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("MaxRetries");
					if (xmlNode2 != null)
					{
						deliveryExtension.MaxRetries = Convert.ToInt32(xmlNode2.InnerText, CultureInfo.InvariantCulture);
					}
					xmlNode2 = xmlNode.SelectSingleNode("SecondsBeforeRetry");
					if (xmlNode2 != null)
					{
						deliveryExtension.SecondsBeforeRetry = Convert.ToInt32(xmlNode2.InnerText, CultureInfo.InvariantCulture);
					}
					xmlNode2 = xmlNode.SelectSingleNode("DefaultDeliveryExtension");
					if (xmlNode2 != null)
					{
						deliveryExtension.DefaultDeliveryExtension = Convert.ToBoolean(xmlNode2.InnerText, CultureInfo.InvariantCulture);
						if (deliveryExtension.DefaultDeliveryExtension && flag)
						{
							throw new ServerConfigurationErrorException(null, "More than one default delivery extension found");
						}
						if (deliveryExtension.DefaultDeliveryExtension)
						{
							flag = true;
						}
					}
					this.AddExtensionToArray(deliveryExtension, array);
				}
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006E34 File Offset: 0x00005034
		private void ParseEventExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					EventExtension eventExtension = new EventExtension();
					eventExtension.Type = child.Name;
					Extension extension = eventExtension;
					this.ParseExtensionElement(xmlNode, ref extension);
					XmlNode xmlNode2 = xmlNode.SelectSingleNode("MaxRetries");
					if (xmlNode2 != null)
					{
						eventExtension.MaxRetries = Convert.ToInt32(xmlNode2.InnerText, CultureInfo.InvariantCulture);
					}
					xmlNode2 = xmlNode.SelectSingleNode("SecondsBeforeRetry");
					if (xmlNode2 != null)
					{
						eventExtension.SecondsBeforeRetry = Convert.ToInt32(xmlNode2.InnerText, CultureInfo.InvariantCulture);
					}
					XmlNode xmlNode3 = xmlNode.SelectSingleNode("Event");
					if (xmlNode3 == null || xmlNode3.InnerText == "")
					{
						throw new Exception(ErrorStringsWrapper.NoEventForEventProcessor(extension.Name));
					}
					eventExtension.EventType = xmlNode3.InnerText;
					this.AddExtensionToArray(eventExtension, array);
				}
			}
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00006F54 File Offset: 0x00005154
		private void ParseRenderExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					RenderingExtension renderingExtension = new RenderingExtension();
					renderingExtension.Type = child.Name;
					Extension extension = renderingExtension;
					this.ParseExtensionElement(xmlNode, ref extension);
					this.ExtractRenderingExternsionOverrideNames(xmlNode, renderingExtension);
					this.ExtractRenderingExtensionDeviceInfo(xmlNode, renderingExtension);
					this.AddExtensionToArray(renderingExtension, array);
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x00006FE8 File Offset: 0x000051E8
		private void ExtractRenderingExternsionOverrideNames(XmlNode extensionElement, RenderingExtension p)
		{
			XmlNode xmlNode = extensionElement.SelectSingleNode("OverrideNames");
			if (xmlNode == null)
			{
				return;
			}
			foreach (object obj in xmlNode.ChildNodes)
			{
				XmlNode xmlNode2 = (XmlNode)obj;
				string innerText = xmlNode2.InnerText;
				string text = null;
				XmlAttribute xmlAttribute = xmlNode2.Attributes["Language"];
				if (xmlAttribute != null)
				{
					text = xmlAttribute.Value;
				}
				if (innerText != null && innerText.Trim() != string.Empty && text != null && text.Trim() != string.Empty && p.OverrideNames[text] == null)
				{
					p.OverrideNames.Add(text.Trim(), innerText.Trim());
				}
			}
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000070C8 File Offset: 0x000052C8
		private void ExtractRenderingExtensionDeviceInfo(XmlNode extensionElement, RenderingExtension p)
		{
			XmlNode xmlNode = extensionElement.SelectSingleNode("Configuration");
			if (xmlNode == null)
			{
				return;
			}
			XmlNode xmlNode2 = xmlNode.SelectSingleNode("DeviceInfo");
			if (xmlNode2 == null)
			{
				return;
			}
			foreach (object obj in xmlNode2.ChildNodes)
			{
				XmlNode xmlNode3 = (XmlNode)obj;
				string text = xmlNode3.Name;
				string text2 = xmlNode3.InnerText;
				if (text != null && text2 != null && text.Trim() != string.Empty)
				{
					text = text.Trim();
					if (text != "FieldDelimiter" && text != "RecordDelimiter")
					{
						text2 = text2.Trim();
					}
					if (text2 != string.Empty && p.DeviceInfo[text] == null)
					{
						p.DeviceInfo.Add(text, text2);
					}
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000071C0 File Offset: 0x000053C0
		private void ParseReportItemExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					Extension extension = new Extension();
					extension.Type = child.Name;
					this.ParseExtensionElement(xmlNode, ref extension, "ReportItem");
					this.AddExtensionToArray(extension, array);
				}
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x00007244 File Offset: 0x00005444
		private void ParseReportItemConverters(XmlNode child, ArrayList array)
		{
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					this.ParseReportItemConverter(xmlNode, array);
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000072A8 File Offset: 0x000054A8
		private void ParseReportItemConverter(XmlNode child, ArrayList array)
		{
			if (child.Name != "Converter")
			{
				XmlParseExceptions.ThrowInvalidFormat(child.Name);
			}
			ReportItemConverterExtension reportItemConverterExtension = new ReportItemConverterExtension();
			reportItemConverterExtension.Type = child.Name;
			XmlAttribute xmlAttribute = child.Attributes["Source"];
			if (xmlAttribute == null)
			{
				XmlParseExceptions.ThrowElementMissing("Source");
			}
			reportItemConverterExtension.Source = xmlAttribute.Value;
			xmlAttribute = child.Attributes["Target"];
			if (xmlAttribute == null)
			{
				XmlParseExceptions.ThrowElementMissing("Target");
			}
			reportItemConverterExtension.Target = xmlAttribute.Value;
			Extension extension = reportItemConverterExtension;
			this.ParseExtensionElementType(child, ref extension);
			array.Add(reportItemConverterExtension);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00007350 File Offset: 0x00005550
		private void ParseDataExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					Extension extension = new Extension();
					extension.Type = child.Name;
					this.ParseExtensionElement(xmlNode, ref extension);
					this.AddExtensionToArray(extension, array);
				}
			}
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000073D0 File Offset: 0x000055D0
		private void ParseExtensions(XmlNode child, ExtensionsConfiguration.ExtensionArray array)
		{
			string text = "Extension";
			if (child.Name == "Delivery" || child.Name == "DeliveryUI")
			{
				this.ParseDeliveryExtensions(child, array);
				return;
			}
			if (child.Name == "EventProcessing")
			{
				this.ParseEventExtensions(child, array);
				return;
			}
			if (child.Name == "Render")
			{
				this.ParseRenderExtensions(child, array);
				return;
			}
			if (child.Name == "Data")
			{
				this.ParseDataExtensions(child, array);
				return;
			}
			if (child.Name == "ReportItems")
			{
				this.ParseReportItemExtensions(child, array);
				return;
			}
			if (child.Name == "ReportItemDesigner")
			{
				text = "ReportItem";
			}
			foreach (object obj in child.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					Extension extension = new Extension();
					extension.Type = child.Name;
					this.ParseExtensionElement(xmlNode, ref extension, text);
					this.AddExtensionToArray(extension, array);
				}
			}
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000750C File Offset: 0x0000570C
		private void AddExtensionToArray(Extension extension, ExtensionsConfiguration.ExtensionArray array)
		{
			if (array[extension.Name] != null)
			{
				if (RSTrace.ConfigManagerTracer.TraceError)
				{
					this.TraceNonUniqueExtensionError(extension.Name);
				}
				return;
			}
			array.Add(extension);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000753D File Offset: 0x0000573D
		private void TraceNonUniqueExtensionError(string extensionName)
		{
			RSTrace.ConfigManagerTracer.Trace("More then one extension found with the name '{0}'. Only the first one found will be used.", new object[] { extensionName });
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x00007558 File Offset: 0x00005758
		private void ParseExtensionElement(XmlNode child, ref Extension extension)
		{
			this.ParseExtensionElement(child, ref extension, "Extension");
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x00007568 File Offset: 0x00005768
		private void ParseExtensionElement(XmlNode child, ref Extension extension, string childName)
		{
			if (child.Name != childName)
			{
				XmlParseExceptions.ThrowInvalidFormat(child.Name);
			}
			XmlAttribute xmlAttribute = child.Attributes["Name"];
			if (xmlAttribute == null)
			{
				XmlParseExceptions.ThrowElementMissing("Name");
			}
			extension.Name = xmlAttribute.Value;
			this.ParseExtensionElementType(child, ref extension);
			xmlAttribute = child.Attributes["Visible"];
			if (xmlAttribute != null)
			{
				extension.Visible = bool.Parse(xmlAttribute.Value);
			}
			xmlAttribute = child.Attributes["LogAllExecutionRequests"];
			if (xmlAttribute != null)
			{
				extension.LogAllExecutionRequests = bool.Parse(xmlAttribute.Value);
			}
			XmlNode xmlNode = child.SelectSingleNode("Configuration");
			if (xmlNode != null)
			{
				extension.Configuration = xmlNode.InnerXml;
			}
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000762C File Offset: 0x0000582C
		private void ParseExtensionElementType(XmlNode child, ref Extension extension)
		{
			XmlAttribute xmlAttribute = child.Attributes["Type"];
			if (xmlAttribute == null)
			{
				XmlParseExceptions.ThrowElementMissing("Type");
			}
			string value = xmlAttribute.Value;
			int num = value.IndexOf(',');
			if (num <= 0 || num == value.Length - 1)
			{
				throw new Exception(ErrorStrings.EmptyExtensionName);
			}
			extension.Class = value.Substring(0, num).Trim();
			extension.Assembly = value.Substring(num + 1).Trim();
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000076A8 File Offset: 0x000058A8
		private void ParseReportDefinitionCustomization(XmlNode child)
		{
			if (child.ChildNodes.Count == 0)
			{
				return;
			}
			if (1 != child.ChildNodes.Count)
			{
				throw new ServerConfigurationErrorException("More than one RDCE defined.");
			}
			this.m_reportDefinitionCustomization = new Extension();
			this.ParseExtensionElement(child.FirstChild, ref this.m_reportDefinitionCustomization);
		}

		// Token: 0x040000D8 RID: 216
		private ExtensionsConfiguration.ExtensionArray m_delivery = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000D9 RID: 217
		private ExtensionsConfiguration.ExtensionArray m_deliveryUI = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000DA RID: 218
		private ExtensionsConfiguration.ExtensionArray m_renderer = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000DB RID: 219
		private ExtensionsConfiguration.ExtensionArray m_data = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000DC RID: 220
		private static Extension m_semanticQueryEngine = new Extension();

		// Token: 0x040000DD RID: 221
		private ExtensionsConfiguration.ExtensionArray m_semanticQuery = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000DE RID: 222
		private ExtensionsConfiguration.ExtensionArray m_modelGeneration = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000DF RID: 223
		private ExtensionsConfiguration.ExtensionArray m_security = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000E0 RID: 224
		private ExtensionsConfiguration.ExtensionArray m_authentication = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000E1 RID: 225
		private ExtensionsConfiguration.ExtensionArray m_event = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000E2 RID: 226
		private ExtensionsConfiguration.ExtensionArray m_designer = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000E3 RID: 227
		private ExtensionsConfiguration.ExtensionArray m_reportItems = new ExtensionsConfiguration.ExtensionArray();

		// Token: 0x040000E4 RID: 228
		private ExtensionsConfiguration.ExtensionArray m_reportItemDesigner;

		// Token: 0x040000E5 RID: 229
		private ArrayList m_reportItemConverter;

		// Token: 0x040000E6 RID: 230
		private Extension m_reportDefinitionCustomization;

		// Token: 0x020000E2 RID: 226
		[Serializable]
		public sealed class ExtensionArray : ArrayList
		{
			// Token: 0x170002CC RID: 716
			public Extension this[string extensionName]
			{
				get
				{
					foreach (object obj in this)
					{
						Extension extension = (Extension)obj;
						if (string.Compare(extensionName, extension.Name, StringComparison.OrdinalIgnoreCase) == 0)
						{
							return extension;
						}
					}
					return null;
				}
			}

			// Token: 0x170002CD RID: 717
			// (get) Token: 0x060007A8 RID: 1960 RVA: 0x00014510 File Offset: 0x00012710
			public string[] Names
			{
				get
				{
					string[] array = new string[this.Count];
					for (int i = 0; i < this.Count; i++)
					{
						array[i] = ((Extension)this[i]).Name;
					}
					return array;
				}
			}
		}
	}
}

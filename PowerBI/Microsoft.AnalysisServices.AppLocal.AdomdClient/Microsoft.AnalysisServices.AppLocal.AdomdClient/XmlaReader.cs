using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.AnalysisServices.AdomdClient.Utilities;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000048 RID: 72
	internal class XmlaReader : XmlReader
	{
		// Token: 0x0600045E RID: 1118 RVA: 0x0001C630 File Offset: 0x0001A830
		internal XmlaReader(XmlReader baseReader, XmlaClient client, NamespacesMgr namespacesManager, bool isBinaryReader)
		{
			this.client = client;
			this.namespacesManager = namespacesManager;
			this.skipUnknownElements = true;
			this.isBinaryReader = isBinaryReader;
			if (isBinaryReader)
			{
				this.xmlReader = new BinaryXmlReader(baseReader);
			}
			else
			{
				this.xmlReader = baseReader;
			}
			XmlTextReader xmlTextReader = baseReader as XmlTextReader;
			if (xmlTextReader != null)
			{
				this.whiteSpaceHandlingRestorer = new XmlaReader.WhiteSpaceHandlingRestorer(xmlTextReader);
				return;
			}
			this.whiteSpaceHandlingRestorer = new XmlaReader.WhiteSpaceHandlingRestorerEmpty(baseReader);
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600045F RID: 1119 RVA: 0x0001C69C File Offset: 0x0001A89C
		public override XmlReaderSettings Settings
		{
			get
			{
				XmlReaderSettings settings;
				try
				{
					settings = this.xmlReader.Settings;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return settings;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06000460 RID: 1120 RVA: 0x0001C6D8 File Offset: 0x0001A8D8
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				IXmlSchemaInfo schemaInfo;
				try
				{
					schemaInfo = this.xmlReader.SchemaInfo;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return schemaInfo;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x06000461 RID: 1121 RVA: 0x0001C714 File Offset: 0x0001A914
		public override Type ValueType
		{
			get
			{
				Type valueType;
				try
				{
					valueType = this.xmlReader.ValueType;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return valueType;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000462 RID: 1122 RVA: 0x0001C750 File Offset: 0x0001A950
		public override XmlNodeType NodeType
		{
			get
			{
				XmlNodeType nodeType;
				try
				{
					nodeType = this.xmlReader.NodeType;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return nodeType;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x0001C78C File Offset: 0x0001A98C
		public override string Name
		{
			get
			{
				string name;
				try
				{
					name = this.xmlReader.Name;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return name;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000464 RID: 1124 RVA: 0x0001C7C8 File Offset: 0x0001A9C8
		public override string LocalName
		{
			get
			{
				string localName;
				try
				{
					localName = this.xmlReader.LocalName;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return localName;
			}
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000465 RID: 1125 RVA: 0x0001C804 File Offset: 0x0001AA04
		public override string NamespaceURI
		{
			get
			{
				string namespaceURI;
				try
				{
					namespaceURI = this.xmlReader.NamespaceURI;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return namespaceURI;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000466 RID: 1126 RVA: 0x0001C840 File Offset: 0x0001AA40
		public override string Prefix
		{
			get
			{
				string prefix;
				try
				{
					prefix = this.xmlReader.Prefix;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return prefix;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000467 RID: 1127 RVA: 0x0001C87C File Offset: 0x0001AA7C
		public override bool HasValue
		{
			get
			{
				bool hasValue;
				try
				{
					hasValue = this.xmlReader.HasValue;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return hasValue;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x06000468 RID: 1128 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		public override string Value
		{
			get
			{
				string value;
				try
				{
					value = this.xmlReader.Value;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return value;
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x06000469 RID: 1129 RVA: 0x0001C8F4 File Offset: 0x0001AAF4
		public override int Depth
		{
			get
			{
				int num;
				try
				{
					num = this.xmlReader.Depth - this.topNodeDepth;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return num;
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x0600046A RID: 1130 RVA: 0x0001C938 File Offset: 0x0001AB38
		public override string BaseURI
		{
			get
			{
				string baseURI;
				try
				{
					baseURI = this.xmlReader.BaseURI;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return baseURI;
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x0600046B RID: 1131 RVA: 0x0001C974 File Offset: 0x0001AB74
		public override bool IsEmptyElement
		{
			get
			{
				bool isEmptyElement;
				try
				{
					isEmptyElement = this.xmlReader.IsEmptyElement;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return isEmptyElement;
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x0600046C RID: 1132 RVA: 0x0001C9B0 File Offset: 0x0001ABB0
		public override bool IsDefault
		{
			get
			{
				bool isDefault;
				try
				{
					isDefault = this.xmlReader.IsDefault;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return isDefault;
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x0600046D RID: 1133 RVA: 0x0001C9EC File Offset: 0x0001ABEC
		public override char QuoteChar
		{
			get
			{
				char quoteChar;
				try
				{
					quoteChar = this.xmlReader.QuoteChar;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return quoteChar;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x0600046E RID: 1134 RVA: 0x0001CA28 File Offset: 0x0001AC28
		public override XmlSpace XmlSpace
		{
			get
			{
				XmlSpace xmlSpace;
				try
				{
					xmlSpace = this.xmlReader.XmlSpace;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return xmlSpace;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600046F RID: 1135 RVA: 0x0001CA64 File Offset: 0x0001AC64
		public override string XmlLang
		{
			get
			{
				string xmlLang;
				try
				{
					xmlLang = this.xmlReader.XmlLang;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return xmlLang;
			}
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x06000470 RID: 1136 RVA: 0x0001CAA0 File Offset: 0x0001ACA0
		public override int AttributeCount
		{
			get
			{
				int attributeCount;
				try
				{
					attributeCount = this.xmlReader.AttributeCount;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return attributeCount;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x06000471 RID: 1137 RVA: 0x0001CADC File Offset: 0x0001ACDC
		public override bool CanResolveEntity
		{
			get
			{
				bool canResolveEntity;
				try
				{
					canResolveEntity = this.xmlReader.CanResolveEntity;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return canResolveEntity;
			}
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x06000472 RID: 1138 RVA: 0x0001CB18 File Offset: 0x0001AD18
		public override bool EOF
		{
			get
			{
				bool flag;
				try
				{
					if (this.MaskEndOfStream)
					{
						flag = this.xmlReader.EOF || this.ReachedClosingReturn();
					}
					else
					{
						flag = this.xmlReader.EOF;
					}
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return flag;
			}
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0001CB78 File Offset: 0x0001AD78
		public override ReadState ReadState
		{
			get
			{
				ReadState readState;
				try
				{
					if (this.MaskEndOfStream && this.ReachedClosingReturn())
					{
						this.SkipToTheEnd();
					}
					readState = this.xmlReader.ReadState;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return readState;
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000474 RID: 1140 RVA: 0x0001CBCC File Offset: 0x0001ADCC
		public override bool HasAttributes
		{
			get
			{
				bool hasAttributes;
				try
				{
					hasAttributes = this.xmlReader.HasAttributes;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return hasAttributes;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000475 RID: 1141 RVA: 0x0001CC08 File Offset: 0x0001AE08
		public override XmlNameTable NameTable
		{
			get
			{
				XmlNameTable nameTable;
				try
				{
					nameTable = this.xmlReader.NameTable;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return nameTable;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000476 RID: 1142 RVA: 0x0001CC44 File Offset: 0x0001AE44
		public override bool CanReadBinaryContent
		{
			get
			{
				bool canReadBinaryContent;
				try
				{
					canReadBinaryContent = this.xmlReader.CanReadBinaryContent;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return canReadBinaryContent;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0001CC80 File Offset: 0x0001AE80
		public override bool CanReadValueChunk
		{
			get
			{
				bool canReadValueChunk;
				try
				{
					canReadValueChunk = this.xmlReader.CanReadValueChunk;
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return canReadValueChunk;
			}
		}

		// Token: 0x1700011B RID: 283
		public override string this[int i]
		{
			get
			{
				string text;
				try
				{
					text = this.xmlReader[i];
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return text;
			}
		}

		// Token: 0x1700011C RID: 284
		public override string this[string name]
		{
			get
			{
				string text;
				try
				{
					text = this.xmlReader[name];
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return text;
			}
		}

		// Token: 0x1700011D RID: 285
		public override string this[string name, string namespaceURI]
		{
			get
			{
				string text;
				try
				{
					text = this.xmlReader[name, namespaceURI];
				}
				catch (Exception ex)
				{
					if ((ex = this.HandleException(ex)) != null)
					{
						throw ex;
					}
					throw;
				}
				return text;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001CD7C File Offset: 0x0001AF7C
		internal bool IsBinaryReader
		{
			get
			{
				return this.isBinaryReader;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0001CD84 File Offset: 0x0001AF84
		// (set) Token: 0x0600047D RID: 1149 RVA: 0x0001CD8C File Offset: 0x0001AF8C
		internal bool MaskEndOfStream
		{
			get
			{
				return this.maskEndOfStream;
			}
			set
			{
				this.maskEndOfStream = value;
				if (this.maskEndOfStream)
				{
					this.topNodeDepth = this.Depth;
					return;
				}
				this.topNodeDepth = 0;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0001CDB1 File Offset: 0x0001AFB1
		// (set) Token: 0x0600047F RID: 1151 RVA: 0x0001CDB9 File Offset: 0x0001AFB9
		internal bool SkipElements
		{
			get
			{
				return this.skipUnknownElements;
			}
			set
			{
				this.skipUnknownElements = value;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0001CDC2 File Offset: 0x0001AFC2
		internal bool HasExtendedErrorInfoBeenRead
		{
			get
			{
				return this.hasExtendedErrorInfoBeenRead;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0001CDCA File Offset: 0x0001AFCA
		internal bool IsReaderDetached
		{
			get
			{
				return this.detached;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0001CDD2 File Offset: 0x0001AFD2
		private bool ShouldSkipUnknownElements
		{
			get
			{
				return this.skipUnknownElements && this.namespacesManager != null;
			}
		}

		// Token: 0x06000483 RID: 1155 RVA: 0x0001CDE8 File Offset: 0x0001AFE8
		public override string GetAttribute(string name)
		{
			string attribute;
			try
			{
				attribute = this.xmlReader.GetAttribute(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return attribute;
		}

		// Token: 0x06000484 RID: 1156 RVA: 0x0001CE28 File Offset: 0x0001B028
		public override string GetAttribute(string name, string namespaceURI)
		{
			string attribute;
			try
			{
				attribute = this.xmlReader.GetAttribute(name, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return attribute;
		}

		// Token: 0x06000485 RID: 1157 RVA: 0x0001CE68 File Offset: 0x0001B068
		public override string GetAttribute(int i)
		{
			string attribute;
			try
			{
				attribute = this.xmlReader.GetAttribute(i);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return attribute;
		}

		// Token: 0x06000486 RID: 1158 RVA: 0x0001CEA8 File Offset: 0x0001B0A8
		public override bool MoveToAttribute(string name)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.MoveToAttribute(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06000487 RID: 1159 RVA: 0x0001CEE8 File Offset: 0x0001B0E8
		public override bool MoveToAttribute(string name, string ns)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.MoveToAttribute(name, ns);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06000488 RID: 1160 RVA: 0x0001CF28 File Offset: 0x0001B128
		public override void MoveToAttribute(int i)
		{
			try
			{
				this.xmlReader.MoveToAttribute(i);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x06000489 RID: 1161 RVA: 0x0001CF64 File Offset: 0x0001B164
		public override bool MoveToFirstAttribute()
		{
			bool flag;
			try
			{
				flag = this.xmlReader.MoveToFirstAttribute();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x0600048A RID: 1162 RVA: 0x0001CFA0 File Offset: 0x0001B1A0
		public override bool MoveToNextAttribute()
		{
			bool flag;
			try
			{
				flag = this.xmlReader.MoveToNextAttribute();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x0600048B RID: 1163 RVA: 0x0001CFDC File Offset: 0x0001B1DC
		public override bool MoveToElement()
		{
			bool flag;
			try
			{
				flag = this.xmlReader.MoveToElement();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0001D018 File Offset: 0x0001B218
		public override bool Read()
		{
			bool flag2;
			try
			{
				if (this.MaskEndOfStream)
				{
					bool flag = this.xmlReader.Read();
					if (this.ReachedClosingReturn())
					{
						this.SkipToTheEnd();
						flag = false;
					}
					flag2 = flag;
				}
				else
				{
					flag2 = this.xmlReader.Read();
				}
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag2;
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0001D07C File Offset: 0x0001B27C
		public override void Close()
		{
			if (this.xmlReader != null)
			{
				if (this.IsReaderDetached)
				{
					try
					{
						this.ReturnReader(true);
						return;
					}
					catch (Exception ex)
					{
						if ((ex = this.HandleException(ex)) != null)
						{
							throw ex;
						}
						throw;
					}
					finally
					{
						this.xmlReader.Close();
					}
				}
				this.xmlReader.Close();
			}
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x0001D0E8 File Offset: 0x0001B2E8
		public override void Skip()
		{
			try
			{
				this.xmlReader.Skip();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x0001D124 File Offset: 0x0001B324
		public override string ReadString()
		{
			string text;
			try
			{
				text = this.xmlReader.ReadString();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001D160 File Offset: 0x0001B360
		public override XmlNodeType MoveToContent()
		{
			XmlNodeType xmlNodeType;
			try
			{
				if (this.ShouldSkipUnknownElements)
				{
					while (this.xmlReader.MoveToContent() == XmlNodeType.Element)
					{
						if (!this.namespacesManager.IsNamespaceSkippable(this.NamespaceURI))
						{
							return this.NodeType;
						}
						this.xmlReader.Skip();
					}
					xmlNodeType = this.NodeType;
				}
				else
				{
					xmlNodeType = this.xmlReader.MoveToContent();
				}
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return xmlNodeType;
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001D1E8 File Offset: 0x0001B3E8
		public override void ReadStartElement()
		{
			try
			{
				this.xmlReader.ReadStartElement();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x0001D224 File Offset: 0x0001B424
		public override void ReadStartElement(string name)
		{
			try
			{
				this.xmlReader.ReadStartElement(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x0001D260 File Offset: 0x0001B460
		public override void ReadStartElement(string localname, string ns)
		{
			try
			{
				this.xmlReader.ReadStartElement(localname, ns);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001D29C File Offset: 0x0001B49C
		public override string ReadElementString()
		{
			string text;
			try
			{
				this.MoveToContent();
				text = this.xmlReader.ReadElementString();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x0001D2E0 File Offset: 0x0001B4E0
		public override string ReadElementString(string name)
		{
			string text;
			try
			{
				this.MoveToContent();
				text = this.xmlReader.ReadElementString(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x0001D324 File Offset: 0x0001B524
		public override string ReadElementString(string localname, string ns)
		{
			string text;
			try
			{
				this.MoveToContent();
				text = this.xmlReader.ReadElementString(localname, ns);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x0001D36C File Offset: 0x0001B56C
		public override void ReadEndElement()
		{
			try
			{
				this.MoveToContent();
				this.xmlReader.ReadEndElement();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x0001D3B0 File Offset: 0x0001B5B0
		public override bool IsStartElement()
		{
			bool flag;
			try
			{
				this.MoveToContent();
				flag = this.xmlReader.IsStartElement();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x0001D3F4 File Offset: 0x0001B5F4
		public override bool IsStartElement(string name)
		{
			bool flag;
			try
			{
				this.MoveToContent();
				flag = this.xmlReader.IsStartElement(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x0001D438 File Offset: 0x0001B638
		public override bool IsStartElement(string localname, string ns)
		{
			bool flag;
			try
			{
				this.MoveToContent();
				flag = this.xmlReader.IsStartElement(localname, ns);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x0001D480 File Offset: 0x0001B680
		public override string ReadInnerXml()
		{
			string text;
			try
			{
				text = this.xmlReader.ReadInnerXml();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x0001D4BC File Offset: 0x0001B6BC
		public override string ReadOuterXml()
		{
			string text;
			try
			{
				text = this.xmlReader.ReadOuterXml();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x0001D4F8 File Offset: 0x0001B6F8
		public override string LookupNamespace(string prefix)
		{
			string text;
			try
			{
				text = this.xmlReader.LookupNamespace(prefix);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001D538 File Offset: 0x0001B738
		public override void ResolveEntity()
		{
			try
			{
				this.xmlReader.ResolveEntity();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001D574 File Offset: 0x0001B774
		public override bool ReadAttributeValue()
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadAttributeValue();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001D5B0 File Offset: 0x0001B7B0
		public override DateTime ReadContentAsDateTime()
		{
			DateTime dateTime;
			try
			{
				dateTime = this.xmlReader.ReadContentAsDateTime();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return dateTime;
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001D5EC File Offset: 0x0001B7EC
		public override double ReadContentAsDouble()
		{
			double num;
			try
			{
				num = this.xmlReader.ReadContentAsDouble();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001D628 File Offset: 0x0001B828
		public override int ReadContentAsInt()
		{
			int num;
			try
			{
				num = this.xmlReader.ReadContentAsInt();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001D664 File Offset: 0x0001B864
		public override long ReadContentAsLong()
		{
			long num;
			try
			{
				num = this.xmlReader.ReadContentAsLong();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001D6A0 File Offset: 0x0001B8A0
		public override object ReadContentAsObject()
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadContentAsObject();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001D6DC File Offset: 0x0001B8DC
		public override object ReadContentAs(Type type, IXmlNamespaceResolver resolver)
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadContentAs(type, resolver);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001D71C File Offset: 0x0001B91C
		public override int ReadContentAsBase64(byte[] buffer, int index, int count)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadContentAsBase64(buffer, index, count);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001D75C File Offset: 0x0001B95C
		public override int ReadContentAsBinHex(byte[] buffer, int index, int count)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadContentAsBinHex(buffer, index, count);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001D79C File Offset: 0x0001B99C
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver)
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadElementContentAs(returnType, namespaceResolver);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001D7DC File Offset: 0x0001B9DC
		public override object ReadElementContentAs(Type returnType, IXmlNamespaceResolver namespaceResolver, string localName, string namespaceURI)
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadElementContentAs(returnType, namespaceResolver, localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001D820 File Offset: 0x0001BA20
		public override int ReadElementContentAsBase64(byte[] buffer, int index, int count)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadElementContentAsBase64(buffer, index, count);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001D860 File Offset: 0x0001BA60
		public override int ReadElementContentAsBinHex(byte[] buffer, int index, int count)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadElementContentAsBinHex(buffer, index, count);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001D8A0 File Offset: 0x0001BAA0
		public override bool ReadElementContentAsBoolean()
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadElementContentAsBoolean();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001D8DC File Offset: 0x0001BADC
		public override bool ReadElementContentAsBoolean(string localName, string namespaceURI)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadElementContentAsBoolean(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001D91C File Offset: 0x0001BB1C
		public override DateTime ReadElementContentAsDateTime()
		{
			DateTime dateTime;
			try
			{
				dateTime = this.xmlReader.ReadElementContentAsDateTime();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return dateTime;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001D958 File Offset: 0x0001BB58
		public override DateTime ReadElementContentAsDateTime(string localName, string namespaceURI)
		{
			DateTime dateTime;
			try
			{
				dateTime = this.xmlReader.ReadElementContentAsDateTime(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return dateTime;
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001D998 File Offset: 0x0001BB98
		public override double ReadElementContentAsDouble()
		{
			double num;
			try
			{
				num = this.xmlReader.ReadElementContentAsDouble();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001D9D4 File Offset: 0x0001BBD4
		public override double ReadElementContentAsDouble(string localName, string namespaceURI)
		{
			double num;
			try
			{
				num = this.xmlReader.ReadElementContentAsDouble(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001DA14 File Offset: 0x0001BC14
		public override int ReadElementContentAsInt()
		{
			int num;
			try
			{
				num = this.xmlReader.ReadElementContentAsInt();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001DA50 File Offset: 0x0001BC50
		public override int ReadElementContentAsInt(string localName, string namespaceURI)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadElementContentAsInt(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001DA90 File Offset: 0x0001BC90
		public override long ReadElementContentAsLong()
		{
			long num;
			try
			{
				num = this.xmlReader.ReadElementContentAsLong();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001DACC File Offset: 0x0001BCCC
		public override long ReadElementContentAsLong(string localName, string namespaceURI)
		{
			long num;
			try
			{
				num = this.xmlReader.ReadElementContentAsLong(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004B6 RID: 1206 RVA: 0x0001DB0C File Offset: 0x0001BD0C
		public override object ReadElementContentAsObject()
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadElementContentAsObject();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004B7 RID: 1207 RVA: 0x0001DB48 File Offset: 0x0001BD48
		public override object ReadElementContentAsObject(string localName, string namespaceURI)
		{
			object obj;
			try
			{
				obj = this.xmlReader.ReadElementContentAsObject(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return obj;
		}

		// Token: 0x060004B8 RID: 1208 RVA: 0x0001DB88 File Offset: 0x0001BD88
		public override string ReadElementContentAsString()
		{
			string text;
			try
			{
				text = this.xmlReader.ReadElementContentAsString();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x060004B9 RID: 1209 RVA: 0x0001DBC4 File Offset: 0x0001BDC4
		public override string ReadElementContentAsString(string localName, string namespaceURI)
		{
			string text;
			try
			{
				text = this.xmlReader.ReadElementContentAsString(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return text;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001DC04 File Offset: 0x0001BE04
		public override bool ReadToFollowing(string name)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToFollowing(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001DC44 File Offset: 0x0001BE44
		public override bool ReadToFollowing(string localName, string namespaceURI)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToFollowing(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001DC84 File Offset: 0x0001BE84
		public override bool ReadToNextSibling(string name)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToNextSibling(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001DCC4 File Offset: 0x0001BEC4
		public override bool ReadToNextSibling(string localName, string namespaceURI)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToNextSibling(localName, namespaceURI);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001DD04 File Offset: 0x0001BF04
		public override bool ReadToDescendant(string name)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToDescendant(name);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001DD44 File Offset: 0x0001BF44
		public override bool ReadToDescendant(string localName, string ns)
		{
			bool flag;
			try
			{
				flag = this.xmlReader.ReadToDescendant(localName, ns);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return flag;
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001DD84 File Offset: 0x0001BF84
		public override XmlReader ReadSubtree()
		{
			XmlReader xmlReader;
			try
			{
				xmlReader = this.xmlReader.ReadSubtree();
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return xmlReader;
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public override int ReadValueChunk(char[] buffer, int index, int count)
		{
			int num;
			try
			{
				num = this.xmlReader.ReadValueChunk(buffer, index, count);
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return num;
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001DE00 File Offset: 0x0001C000
		internal XmlSchema ReadSchema()
		{
			XmlSchema xmlSchema2;
			try
			{
				XmlSchema xmlSchema = null;
				if (!this.ShouldSkipUnknownElements)
				{
					xmlSchema = XmlSchema.Read(this.xmlReader, null);
					this.xmlReader.ReadEndElement();
				}
				else
				{
					XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(this.NameTable);
					IXmlNamespaceResolver xmlNamespaceResolver = this.xmlReader as IXmlNamespaceResolver;
					if (xmlNamespaceResolver != null)
					{
						IEnumerator<KeyValuePair<string, string>> enumerator = xmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope.ExcludeXml).GetEnumerator();
						while (enumerator.MoveNext())
						{
							XmlNamespaceManager xmlNamespaceManager2 = xmlNamespaceManager;
							KeyValuePair<string, string> keyValuePair = enumerator.Current;
							string key = keyValuePair.Key;
							keyValuePair = enumerator.Current;
							xmlNamespaceManager2.AddNamespace(key, keyValuePair.Value);
						}
					}
					XmlParserContext xmlParserContext = new XmlParserContext(this.NameTable, xmlNamespaceManager, this.XmlLang, this.XmlSpace);
					XmlReaderSettings xmlReaderSettings = new XmlReaderSettings();
					xmlReaderSettings.ConformanceLevel = ConformanceLevel.Fragment;
					XmlReader xmlReader = XmlReader.Create(new StringReader(this.GetCleanedUpSchemaDefinition()), xmlReaderSettings, xmlParserContext);
					try
					{
						xmlSchema = XmlSchema.Read(xmlReader, null);
					}
					finally
					{
						xmlReader.Close();
					}
				}
				xmlSchema2 = xmlSchema;
			}
			catch (Exception ex)
			{
				if ((ex = this.HandleException(ex)) != null)
				{
					throw ex;
				}
				throw;
			}
			return xmlSchema2;
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001DF18 File Offset: 0x0001C118
		internal string GetExtendedErrorInfo()
		{
			this.hasExtendedErrorInfoBeenRead = true;
			XmlaClient xmlaClient = this.client;
			if (((xmlaClient != null) ? xmlaClient.xmlaStream : null) != null)
			{
				using (new XmlaReader.ClientLocaleHelper(this.client))
				{
					return this.client.xmlaStream.GetExtendedErrorInfo();
				}
			}
			return string.Empty;
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001DF80 File Offset: 0x0001C180
		internal IDisposable GetWhitespaceHandlingRestorer(WhitespaceHandling handling)
		{
			this.whiteSpaceHandlingRestorer.Initialize(handling);
			return this.whiteSpaceHandlingRestorer;
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001DF94 File Offset: 0x0001C194
		internal void CloseWithoutEndReceival()
		{
			XmlReader xmlReader = this.xmlReader;
			if (xmlReader != null)
			{
				xmlReader.Close();
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001DFB1 File Offset: 0x0001C1B1
		internal void DetachReader()
		{
			if (!this.IsReaderDetached)
			{
				this.detached = true;
			}
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001DFC4 File Offset: 0x0001C1C4
		private bool ReachedClosingReturn()
		{
			return !this.xmlReader.EOF && this.NodeType == XmlNodeType.EndElement && this.LocalName == XmlaReader.ReturnElement && this.LookupNamespace(this.Prefix) == XmlaReader.ReturnElementNamespace;
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x0001E012 File Offset: 0x0001C212
		private void SkipToTheEnd()
		{
			while (!this.xmlReader.EOF)
			{
				this.xmlReader.Read();
			}
		}

		// Token: 0x060004C9 RID: 1225 RVA: 0x0001E030 File Offset: 0x0001C230
		private string GetCleanedUpSchemaDefinition()
		{
			if (this.IsStartElement(XmlaReader.SchemaElement, "http://www.w3.org/2001/XMLSchema"))
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);
				try
				{
					xmlTextWriter.QuoteChar = this.QuoteChar;
					int depth = this.Depth;
					xmlTextWriter.WriteStartElement(this.xmlReader.Prefix, this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
					xmlTextWriter.WriteAttributes(this, true);
					if (this.xmlReader.IsEmptyElement)
					{
						xmlTextWriter.WriteEndElement();
					}
					else
					{
						bool flag = !this.xmlReader.Read();
						while (!flag && this.xmlReader.Depth > depth)
						{
							XmlNodeType nodeType = this.xmlReader.NodeType;
							switch (nodeType)
							{
							case XmlNodeType.Element:
								if (this.namespacesManager.IsNamespaceSkippable(this.xmlReader.NamespaceURI))
								{
									this.xmlReader.Skip();
									flag = this.EOF;
									continue;
								}
								xmlTextWriter.WriteStartElement(this.xmlReader.Prefix, this.xmlReader.LocalName, this.xmlReader.NamespaceURI);
								xmlTextWriter.WriteAttributes(this, true);
								if (this.xmlReader.IsEmptyElement)
								{
									xmlTextWriter.WriteEndElement();
								}
								break;
							case XmlNodeType.Attribute:
							case XmlNodeType.CDATA:
								break;
							case XmlNodeType.Text:
								xmlTextWriter.WriteString(this.xmlReader.Value);
								break;
							case XmlNodeType.EntityReference:
								xmlTextWriter.WriteEntityRef(this.xmlReader.Name);
								break;
							default:
								if (nodeType == XmlNodeType.EndElement)
								{
									xmlTextWriter.WriteFullEndElement();
								}
								break;
							}
							flag = !this.xmlReader.Read();
						}
						if (depth == this.xmlReader.Depth && this.xmlReader.NodeType == XmlNodeType.EndElement)
						{
							xmlTextWriter.WriteFullEndElement();
							this.xmlReader.Read();
						}
					}
				}
				finally
				{
					xmlTextWriter.Close();
				}
				return stringWriter.ToString();
			}
			throw new AdomdUnknownResponseException(XmlaSR.MissingElement(XmlaReader.SchemaElement, "http://www.w3.org/2001/XMLSchema"), "");
		}

		// Token: 0x060004CA RID: 1226 RVA: 0x0001E240 File Offset: 0x0001C440
		private void ReturnReader(bool callEndReceival)
		{
			if (this.IsReaderDetached)
			{
				this.detached = false;
				if (this.ReadState != ReadState.Closed && callEndReceival && this.client != null)
				{
					if (this.client.IsConnected)
					{
						this.client.EndReceival(false);
					}
					this.client = null;
				}
			}
		}

		// Token: 0x060004CB RID: 1227 RVA: 0x0001E290 File Offset: 0x0001C490
		private Exception HandleException(Exception ex)
		{
			if (!this.IsReaderDetached || ex is XmlException || ex is XmlSchemaException || ex is AdomdUnknownResponseException)
			{
				return null;
			}
			try
			{
				if (this.client != null)
				{
					this.ReturnReader(false);
					if (this.xmlReader != null)
					{
						this.xmlReader.Close();
					}
					this.client.Disconnect(false);
					this.client = null;
				}
			}
			finally
			{
				if (this.xmlReader != null)
				{
					this.xmlReader.Close();
				}
				this.client = null;
			}
			for (Exception ex2 = ex; ex2 != null; ex2 = ex2.InnerException)
			{
				if (ex2 is IOException)
				{
					return new AdomdConnectionException(XmlaSR.ConnectionBroken, ex, ConnectionExceptionCause.DataStreamingInterrupted);
				}
			}
			return null;
		}

		// Token: 0x040003BF RID: 959
		private static string ReturnElement = "return";

		// Token: 0x040003C0 RID: 960
		private static string ReturnElementNamespace = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x040003C1 RID: 961
		private static string SchemaElement = "schema";

		// Token: 0x040003C2 RID: 962
		private XmlaClient client;

		// Token: 0x040003C3 RID: 963
		private NamespacesMgr namespacesManager;

		// Token: 0x040003C4 RID: 964
		private bool skipUnknownElements;

		// Token: 0x040003C5 RID: 965
		private XmlReader xmlReader;

		// Token: 0x040003C6 RID: 966
		private bool detached;

		// Token: 0x040003C7 RID: 967
		private bool maskEndOfStream;

		// Token: 0x040003C8 RID: 968
		private int topNodeDepth;

		// Token: 0x040003C9 RID: 969
		private XmlaReader.IWhiteSpaceHandlingRestorer whiteSpaceHandlingRestorer;

		// Token: 0x040003CA RID: 970
		private bool hasExtendedErrorInfoBeenRead;

		// Token: 0x040003CB RID: 971
		private bool isBinaryReader;

		// Token: 0x02000199 RID: 409
		private interface IWhiteSpaceHandlingRestorer : IDisposable
		{
			// Token: 0x0600125C RID: 4700
			void Initialize(WhitespaceHandling handling);
		}

		// Token: 0x0200019A RID: 410
		private sealed class WhiteSpaceHandlingRestorer : XmlaReader.IWhiteSpaceHandlingRestorer, IDisposable
		{
			// Token: 0x0600125D RID: 4701 RVA: 0x00040A51 File Offset: 0x0003EC51
			internal WhiteSpaceHandlingRestorer(XmlTextReader reader)
			{
				this.baseTextReader = reader;
			}

			// Token: 0x0600125E RID: 4702 RVA: 0x00040A67 File Offset: 0x0003EC67
			public void Initialize(WhitespaceHandling handling)
			{
				this.handling = this.baseTextReader.WhitespaceHandling;
				if (this.baseTextReader != null && this.baseTextReader.ReadState != ReadState.Closed)
				{
					this.baseTextReader.WhitespaceHandling = handling;
				}
			}

			// Token: 0x0600125F RID: 4703 RVA: 0x00040A9C File Offset: 0x0003EC9C
			public void Dispose()
			{
				if (this.baseTextReader != null && this.baseTextReader.ReadState != ReadState.Closed)
				{
					this.baseTextReader.WhitespaceHandling = this.handling;
				}
				this.handling = WhitespaceHandling.None;
			}

			// Token: 0x04000C82 RID: 3202
			private WhitespaceHandling handling = WhitespaceHandling.None;

			// Token: 0x04000C83 RID: 3203
			private XmlTextReader baseTextReader;
		}

		// Token: 0x0200019B RID: 411
		private sealed class WhiteSpaceHandlingRestorerEmpty : XmlaReader.IWhiteSpaceHandlingRestorer, IDisposable
		{
			// Token: 0x06001260 RID: 4704 RVA: 0x00040ACC File Offset: 0x0003ECCC
			internal WhiteSpaceHandlingRestorerEmpty(XmlReader reader)
			{
			}

			// Token: 0x06001261 RID: 4705 RVA: 0x00040AD4 File Offset: 0x0003ECD4
			public void Initialize(WhitespaceHandling handling)
			{
			}

			// Token: 0x06001262 RID: 4706 RVA: 0x00040AD6 File Offset: 0x0003ECD6
			public void Dispose()
			{
			}
		}

		// Token: 0x0200019C RID: 412
		private sealed class ClientLocaleHelper : Disposable
		{
			// Token: 0x06001263 RID: 4707 RVA: 0x00040AD8 File Offset: 0x0003ECD8
			public ClientLocaleHelper(XmlaClient client)
			{
				if (client == null || client.ConnectionInfo == null)
				{
					return;
				}
				object obj = client.ConnectionInfo.ExtendedProperties["LocaleIdentifier"];
				if (obj == null)
				{
					return;
				}
				int num = XmlConvert.ToInt32((string)obj);
				if (num != Thread.CurrentThread.CurrentUICulture.LCID)
				{
					try
					{
						this.prevUICulture = Thread.CurrentThread.CurrentUICulture;
						Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo(num);
					}
					catch (Exception)
					{
						this.prevUICulture = null;
					}
				}
			}

			// Token: 0x06001264 RID: 4708 RVA: 0x00040B6C File Offset: 0x0003ED6C
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.prevUICulture != null)
				{
					Thread.CurrentThread.CurrentUICulture = this.prevUICulture;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000C84 RID: 3204
			private readonly CultureInfo prevUICulture;
		}
	}
}

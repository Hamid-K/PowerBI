using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Xml;
using System.Xml.Schema;
using Microsoft.AnalysisServices.Utilities;

namespace Microsoft.AnalysisServices
{
	// Token: 0x02000065 RID: 101
	internal class XmlaReader : XmlReader
	{
		// Token: 0x06000517 RID: 1303 RVA: 0x0001FFF4 File Offset: 0x0001E1F4
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

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000518 RID: 1304 RVA: 0x00020060 File Offset: 0x0001E260
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

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06000519 RID: 1305 RVA: 0x0002009C File Offset: 0x0001E29C
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

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x000200D8 File Offset: 0x0001E2D8
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

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600051B RID: 1307 RVA: 0x00020114 File Offset: 0x0001E314
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

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600051C RID: 1308 RVA: 0x00020150 File Offset: 0x0001E350
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

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600051D RID: 1309 RVA: 0x0002018C File Offset: 0x0001E38C
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

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600051E RID: 1310 RVA: 0x000201C8 File Offset: 0x0001E3C8
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

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600051F RID: 1311 RVA: 0x00020204 File Offset: 0x0001E404
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

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x00020240 File Offset: 0x0001E440
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

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000521 RID: 1313 RVA: 0x0002027C File Offset: 0x0001E47C
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

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000522 RID: 1314 RVA: 0x000202B8 File Offset: 0x0001E4B8
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

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000523 RID: 1315 RVA: 0x000202FC File Offset: 0x0001E4FC
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

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x00020338 File Offset: 0x0001E538
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

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x06000525 RID: 1317 RVA: 0x00020374 File Offset: 0x0001E574
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

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000526 RID: 1318 RVA: 0x000203B0 File Offset: 0x0001E5B0
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

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000527 RID: 1319 RVA: 0x000203EC File Offset: 0x0001E5EC
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

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x00020428 File Offset: 0x0001E628
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

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x00020464 File Offset: 0x0001E664
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

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x000204A0 File Offset: 0x0001E6A0
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

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x0600052B RID: 1323 RVA: 0x000204DC File Offset: 0x0001E6DC
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

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0002053C File Offset: 0x0001E73C
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

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x00020590 File Offset: 0x0001E790
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

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x000205CC File Offset: 0x0001E7CC
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

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x00020608 File Offset: 0x0001E808
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

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x06000530 RID: 1328 RVA: 0x00020644 File Offset: 0x0001E844
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

		// Token: 0x17000129 RID: 297
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

		// Token: 0x1700012A RID: 298
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

		// Token: 0x1700012B RID: 299
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

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x00020740 File Offset: 0x0001E940
		internal bool IsBinaryReader
		{
			get
			{
				return this.isBinaryReader;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x00020748 File Offset: 0x0001E948
		// (set) Token: 0x06000536 RID: 1334 RVA: 0x00020750 File Offset: 0x0001E950
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

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x00020775 File Offset: 0x0001E975
		// (set) Token: 0x06000538 RID: 1336 RVA: 0x0002077D File Offset: 0x0001E97D
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

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x00020786 File Offset: 0x0001E986
		internal bool HasExtendedErrorInfoBeenRead
		{
			get
			{
				return this.hasExtendedErrorInfoBeenRead;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0002078E File Offset: 0x0001E98E
		internal bool IsReaderDetached
		{
			get
			{
				return this.detached;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x00020796 File Offset: 0x0001E996
		private bool ShouldSkipUnknownElements
		{
			get
			{
				return this.skipUnknownElements && this.namespacesManager != null;
			}
		}

		// Token: 0x0600053C RID: 1340 RVA: 0x000207AC File Offset: 0x0001E9AC
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

		// Token: 0x0600053D RID: 1341 RVA: 0x000207EC File Offset: 0x0001E9EC
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

		// Token: 0x0600053E RID: 1342 RVA: 0x0002082C File Offset: 0x0001EA2C
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

		// Token: 0x0600053F RID: 1343 RVA: 0x0002086C File Offset: 0x0001EA6C
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

		// Token: 0x06000540 RID: 1344 RVA: 0x000208AC File Offset: 0x0001EAAC
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

		// Token: 0x06000541 RID: 1345 RVA: 0x000208EC File Offset: 0x0001EAEC
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

		// Token: 0x06000542 RID: 1346 RVA: 0x00020928 File Offset: 0x0001EB28
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

		// Token: 0x06000543 RID: 1347 RVA: 0x00020964 File Offset: 0x0001EB64
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

		// Token: 0x06000544 RID: 1348 RVA: 0x000209A0 File Offset: 0x0001EBA0
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

		// Token: 0x06000545 RID: 1349 RVA: 0x000209DC File Offset: 0x0001EBDC
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

		// Token: 0x06000546 RID: 1350 RVA: 0x00020A40 File Offset: 0x0001EC40
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

		// Token: 0x06000547 RID: 1351 RVA: 0x00020AAC File Offset: 0x0001ECAC
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

		// Token: 0x06000548 RID: 1352 RVA: 0x00020AE8 File Offset: 0x0001ECE8
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

		// Token: 0x06000549 RID: 1353 RVA: 0x00020B24 File Offset: 0x0001ED24
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

		// Token: 0x0600054A RID: 1354 RVA: 0x00020BAC File Offset: 0x0001EDAC
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

		// Token: 0x0600054B RID: 1355 RVA: 0x00020BE8 File Offset: 0x0001EDE8
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

		// Token: 0x0600054C RID: 1356 RVA: 0x00020C24 File Offset: 0x0001EE24
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

		// Token: 0x0600054D RID: 1357 RVA: 0x00020C60 File Offset: 0x0001EE60
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

		// Token: 0x0600054E RID: 1358 RVA: 0x00020CA4 File Offset: 0x0001EEA4
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

		// Token: 0x0600054F RID: 1359 RVA: 0x00020CE8 File Offset: 0x0001EEE8
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

		// Token: 0x06000550 RID: 1360 RVA: 0x00020D30 File Offset: 0x0001EF30
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

		// Token: 0x06000551 RID: 1361 RVA: 0x00020D74 File Offset: 0x0001EF74
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

		// Token: 0x06000552 RID: 1362 RVA: 0x00020DB8 File Offset: 0x0001EFB8
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

		// Token: 0x06000553 RID: 1363 RVA: 0x00020DFC File Offset: 0x0001EFFC
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

		// Token: 0x06000554 RID: 1364 RVA: 0x00020E44 File Offset: 0x0001F044
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

		// Token: 0x06000555 RID: 1365 RVA: 0x00020E80 File Offset: 0x0001F080
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

		// Token: 0x06000556 RID: 1366 RVA: 0x00020EBC File Offset: 0x0001F0BC
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

		// Token: 0x06000557 RID: 1367 RVA: 0x00020EFC File Offset: 0x0001F0FC
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

		// Token: 0x06000558 RID: 1368 RVA: 0x00020F38 File Offset: 0x0001F138
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

		// Token: 0x06000559 RID: 1369 RVA: 0x00020F74 File Offset: 0x0001F174
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

		// Token: 0x0600055A RID: 1370 RVA: 0x00020FB0 File Offset: 0x0001F1B0
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

		// Token: 0x0600055B RID: 1371 RVA: 0x00020FEC File Offset: 0x0001F1EC
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

		// Token: 0x0600055C RID: 1372 RVA: 0x00021028 File Offset: 0x0001F228
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

		// Token: 0x0600055D RID: 1373 RVA: 0x00021064 File Offset: 0x0001F264
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

		// Token: 0x0600055E RID: 1374 RVA: 0x000210A0 File Offset: 0x0001F2A0
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

		// Token: 0x0600055F RID: 1375 RVA: 0x000210E0 File Offset: 0x0001F2E0
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

		// Token: 0x06000560 RID: 1376 RVA: 0x00021120 File Offset: 0x0001F320
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

		// Token: 0x06000561 RID: 1377 RVA: 0x00021160 File Offset: 0x0001F360
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

		// Token: 0x06000562 RID: 1378 RVA: 0x000211A0 File Offset: 0x0001F3A0
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

		// Token: 0x06000563 RID: 1379 RVA: 0x000211E4 File Offset: 0x0001F3E4
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

		// Token: 0x06000564 RID: 1380 RVA: 0x00021224 File Offset: 0x0001F424
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

		// Token: 0x06000565 RID: 1381 RVA: 0x00021264 File Offset: 0x0001F464
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

		// Token: 0x06000566 RID: 1382 RVA: 0x000212A0 File Offset: 0x0001F4A0
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

		// Token: 0x06000567 RID: 1383 RVA: 0x000212E0 File Offset: 0x0001F4E0
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

		// Token: 0x06000568 RID: 1384 RVA: 0x0002131C File Offset: 0x0001F51C
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

		// Token: 0x06000569 RID: 1385 RVA: 0x0002135C File Offset: 0x0001F55C
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

		// Token: 0x0600056A RID: 1386 RVA: 0x00021398 File Offset: 0x0001F598
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

		// Token: 0x0600056B RID: 1387 RVA: 0x000213D8 File Offset: 0x0001F5D8
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

		// Token: 0x0600056C RID: 1388 RVA: 0x00021414 File Offset: 0x0001F614
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

		// Token: 0x0600056D RID: 1389 RVA: 0x00021454 File Offset: 0x0001F654
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

		// Token: 0x0600056E RID: 1390 RVA: 0x00021490 File Offset: 0x0001F690
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

		// Token: 0x0600056F RID: 1391 RVA: 0x000214D0 File Offset: 0x0001F6D0
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

		// Token: 0x06000570 RID: 1392 RVA: 0x0002150C File Offset: 0x0001F70C
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

		// Token: 0x06000571 RID: 1393 RVA: 0x0002154C File Offset: 0x0001F74C
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

		// Token: 0x06000572 RID: 1394 RVA: 0x00021588 File Offset: 0x0001F788
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

		// Token: 0x06000573 RID: 1395 RVA: 0x000215C8 File Offset: 0x0001F7C8
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

		// Token: 0x06000574 RID: 1396 RVA: 0x00021608 File Offset: 0x0001F808
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

		// Token: 0x06000575 RID: 1397 RVA: 0x00021648 File Offset: 0x0001F848
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

		// Token: 0x06000576 RID: 1398 RVA: 0x00021688 File Offset: 0x0001F888
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

		// Token: 0x06000577 RID: 1399 RVA: 0x000216C8 File Offset: 0x0001F8C8
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

		// Token: 0x06000578 RID: 1400 RVA: 0x00021708 File Offset: 0x0001F908
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

		// Token: 0x06000579 RID: 1401 RVA: 0x00021748 File Offset: 0x0001F948
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

		// Token: 0x0600057A RID: 1402 RVA: 0x00021784 File Offset: 0x0001F984
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

		// Token: 0x0600057B RID: 1403 RVA: 0x000217C4 File Offset: 0x0001F9C4
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

		// Token: 0x0600057C RID: 1404 RVA: 0x000218DC File Offset: 0x0001FADC
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

		// Token: 0x0600057D RID: 1405 RVA: 0x00021944 File Offset: 0x0001FB44
		internal IDisposable GetWhitespaceHandlingRestorer(WhitespaceHandling handling)
		{
			this.whiteSpaceHandlingRestorer.Initialize(handling);
			return this.whiteSpaceHandlingRestorer;
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00021958 File Offset: 0x0001FB58
		internal void CloseWithoutEndReceival()
		{
			XmlReader xmlReader = this.xmlReader;
			if (xmlReader != null)
			{
				xmlReader.Close();
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00021975 File Offset: 0x0001FB75
		internal void DetachReader()
		{
			if (!this.IsReaderDetached)
			{
				this.detached = true;
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00021988 File Offset: 0x0001FB88
		private bool ReachedClosingReturn()
		{
			return !this.xmlReader.EOF && this.NodeType == XmlNodeType.EndElement && this.LocalName == XmlaReader.ReturnElement && this.LookupNamespace(this.Prefix) == XmlaReader.ReturnElementNamespace;
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x000219D6 File Offset: 0x0001FBD6
		private void SkipToTheEnd()
		{
			while (!this.xmlReader.EOF)
			{
				this.xmlReader.Read();
			}
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x000219F4 File Offset: 0x0001FBF4
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
			throw new ResponseFormatException(XmlaSR.MissingElement(XmlaReader.SchemaElement, "http://www.w3.org/2001/XMLSchema"), "");
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00021C04 File Offset: 0x0001FE04
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

		// Token: 0x06000584 RID: 1412 RVA: 0x00021C54 File Offset: 0x0001FE54
		private Exception HandleException(Exception ex)
		{
			if (!this.IsReaderDetached || ex is XmlException || ex is XmlSchemaException || ex is ResponseFormatException)
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
					return new ConnectionException(XmlaSR.ConnectionBroken, ex, ConnectionExceptionCause.DataStreamingInterrupted);
				}
			}
			return null;
		}

		// Token: 0x040003EE RID: 1006
		private static string ReturnElement = "return";

		// Token: 0x040003EF RID: 1007
		private static string ReturnElementNamespace = "urn:schemas-microsoft-com:xml-analysis";

		// Token: 0x040003F0 RID: 1008
		private static string SchemaElement = "schema";

		// Token: 0x040003F1 RID: 1009
		private XmlaClient client;

		// Token: 0x040003F2 RID: 1010
		private NamespacesMgr namespacesManager;

		// Token: 0x040003F3 RID: 1011
		private bool skipUnknownElements;

		// Token: 0x040003F4 RID: 1012
		private XmlReader xmlReader;

		// Token: 0x040003F5 RID: 1013
		private bool detached;

		// Token: 0x040003F6 RID: 1014
		private bool maskEndOfStream;

		// Token: 0x040003F7 RID: 1015
		private int topNodeDepth;

		// Token: 0x040003F8 RID: 1016
		private XmlaReader.IWhiteSpaceHandlingRestorer whiteSpaceHandlingRestorer;

		// Token: 0x040003F9 RID: 1017
		private bool hasExtendedErrorInfoBeenRead;

		// Token: 0x040003FA RID: 1018
		private bool isBinaryReader;

		// Token: 0x02000195 RID: 405
		private interface IWhiteSpaceHandlingRestorer : IDisposable
		{
			// Token: 0x060012F8 RID: 4856
			void Initialize(WhitespaceHandling handling);
		}

		// Token: 0x02000196 RID: 406
		private sealed class WhiteSpaceHandlingRestorer : XmlaReader.IWhiteSpaceHandlingRestorer, IDisposable
		{
			// Token: 0x060012F9 RID: 4857 RVA: 0x00042FBD File Offset: 0x000411BD
			internal WhiteSpaceHandlingRestorer(XmlTextReader reader)
			{
				this.baseTextReader = reader;
			}

			// Token: 0x060012FA RID: 4858 RVA: 0x00042FD3 File Offset: 0x000411D3
			public void Initialize(WhitespaceHandling handling)
			{
				this.handling = this.baseTextReader.WhitespaceHandling;
				if (this.baseTextReader != null && this.baseTextReader.ReadState != ReadState.Closed)
				{
					this.baseTextReader.WhitespaceHandling = handling;
				}
			}

			// Token: 0x060012FB RID: 4859 RVA: 0x00043008 File Offset: 0x00041208
			public void Dispose()
			{
				if (this.baseTextReader != null && this.baseTextReader.ReadState != ReadState.Closed)
				{
					this.baseTextReader.WhitespaceHandling = this.handling;
				}
				this.handling = WhitespaceHandling.None;
			}

			// Token: 0x04000C3F RID: 3135
			private WhitespaceHandling handling = WhitespaceHandling.None;

			// Token: 0x04000C40 RID: 3136
			private XmlTextReader baseTextReader;
		}

		// Token: 0x02000197 RID: 407
		private sealed class WhiteSpaceHandlingRestorerEmpty : XmlaReader.IWhiteSpaceHandlingRestorer, IDisposable
		{
			// Token: 0x060012FC RID: 4860 RVA: 0x00043038 File Offset: 0x00041238
			internal WhiteSpaceHandlingRestorerEmpty(XmlReader reader)
			{
			}

			// Token: 0x060012FD RID: 4861 RVA: 0x00043040 File Offset: 0x00041240
			public void Initialize(WhitespaceHandling handling)
			{
			}

			// Token: 0x060012FE RID: 4862 RVA: 0x00043042 File Offset: 0x00041242
			public void Dispose()
			{
			}
		}

		// Token: 0x02000198 RID: 408
		private sealed class ClientLocaleHelper : Disposable
		{
			// Token: 0x060012FF RID: 4863 RVA: 0x00043044 File Offset: 0x00041244
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

			// Token: 0x06001300 RID: 4864 RVA: 0x000430D8 File Offset: 0x000412D8
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.prevUICulture != null)
				{
					Thread.CurrentThread.CurrentUICulture = this.prevUICulture;
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000C41 RID: 3137
			private readonly CultureInfo prevUICulture;
		}
	}
}

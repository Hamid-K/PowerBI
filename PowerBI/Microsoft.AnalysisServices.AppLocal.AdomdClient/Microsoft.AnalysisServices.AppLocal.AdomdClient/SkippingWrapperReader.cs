using System;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F0 RID: 240
	internal class SkippingWrapperReader : XmlReader
	{
		// Token: 0x06000CF3 RID: 3315 RVA: 0x0002FD6E File Offset: 0x0002DF6E
		internal SkippingWrapperReader(XmlReader reader)
		{
			this.reader = reader;
			this.namespacesManager = new NamespacesMgr();
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002FD88 File Offset: 0x0002DF88
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002FD95 File Offset: 0x0002DF95
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002FDA2 File Offset: 0x0002DFA2
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002FDAF File Offset: 0x0002DFAF
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002FDBC File Offset: 0x0002DFBC
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002FDC9 File Offset: 0x0002DFC9
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002FDD6 File Offset: 0x0002DFD6
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000CFB RID: 3323 RVA: 0x0002FDE3 File Offset: 0x0002DFE3
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000CFC RID: 3324 RVA: 0x0002FDF0 File Offset: 0x0002DFF0
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x0002FDFD File Offset: 0x0002DFFD
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000CFE RID: 3326 RVA: 0x0002FE0A File Offset: 0x0002E00A
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CFF RID: 3327 RVA: 0x0002FE17 File Offset: 0x0002E017
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000D00 RID: 3328 RVA: 0x0002FE24 File Offset: 0x0002E024
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x0002FE31 File Offset: 0x0002E031
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x0002FE3E File Offset: 0x0002E03E
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x06000D03 RID: 3331 RVA: 0x0002FE4B File Offset: 0x0002E04B
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x17000505 RID: 1285
		// (get) Token: 0x06000D04 RID: 3332 RVA: 0x0002FE58 File Offset: 0x0002E058
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x17000506 RID: 1286
		// (get) Token: 0x06000D05 RID: 3333 RVA: 0x0002FE65 File Offset: 0x0002E065
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x17000507 RID: 1287
		// (get) Token: 0x06000D06 RID: 3334 RVA: 0x0002FE72 File Offset: 0x0002E072
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x17000508 RID: 1288
		// (get) Token: 0x06000D07 RID: 3335 RVA: 0x0002FE7F File Offset: 0x0002E07F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000509 RID: 1289
		public override string this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x1700050A RID: 1290
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x1700050B RID: 1291
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.reader[name, namespaceURI];
			}
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002FEB7 File Offset: 0x0002E0B7
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002FEC5 File Offset: 0x0002E0C5
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002FED4 File Offset: 0x0002E0D4
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002FEE2 File Offset: 0x0002E0E2
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002FEF0 File Offset: 0x0002E0F0
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002FEFF File Offset: 0x0002E0FF
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002FF0D File Offset: 0x0002E10D
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002FF1A File Offset: 0x0002E11A
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002FF27 File Offset: 0x0002E127
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002FF34 File Offset: 0x0002E134
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002FF41 File Offset: 0x0002E141
		public override void Close()
		{
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002FF43 File Offset: 0x0002E143
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002FF50 File Offset: 0x0002E150
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002FF5D File Offset: 0x0002E15D
		public override XmlNodeType MoveToContent()
		{
			while (this.reader.MoveToContent() == XmlNodeType.Element)
			{
				if (!this.namespacesManager.IsNamespaceSkippable(this.NamespaceURI))
				{
					return this.NodeType;
				}
				this.reader.Skip();
			}
			return this.NodeType;
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002FF9C File Offset: 0x0002E19C
		public override void ReadStartElement()
		{
			this.MoveToContent();
			this.reader.ReadStartElement();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002FFB0 File Offset: 0x0002E1B0
		public override void ReadStartElement(string name)
		{
			this.MoveToContent();
			this.reader.ReadStartElement(name);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x0002FFC5 File Offset: 0x0002E1C5
		public override void ReadStartElement(string localname, string ns)
		{
			this.MoveToContent();
			this.reader.ReadStartElement(localname, ns);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x0002FFDB File Offset: 0x0002E1DB
		public override string ReadElementString()
		{
			this.MoveToContent();
			return this.reader.ReadElementString();
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x0002FFEF File Offset: 0x0002E1EF
		public override string ReadElementString(string name)
		{
			this.MoveToContent();
			return this.reader.ReadElementString(name);
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00030004 File Offset: 0x0002E204
		public override string ReadElementString(string localname, string ns)
		{
			this.MoveToContent();
			return this.reader.ReadElementString(localname, ns);
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x0003001A File Offset: 0x0002E21A
		public override void ReadEndElement()
		{
			this.MoveToContent();
			this.reader.ReadEndElement();
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x0003002E File Offset: 0x0002E22E
		public override bool IsStartElement()
		{
			this.MoveToContent();
			return this.reader.IsStartElement();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00030042 File Offset: 0x0002E242
		public override bool IsStartElement(string name)
		{
			this.MoveToContent();
			return this.reader.IsStartElement(name);
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00030057 File Offset: 0x0002E257
		public override bool IsStartElement(string localname, string ns)
		{
			this.MoveToContent();
			return this.reader.IsStartElement(localname, ns);
		}

		// Token: 0x06000D23 RID: 3363 RVA: 0x0003006D File Offset: 0x0002E26D
		public override string ReadInnerXml()
		{
			this.MoveToContent();
			return this.reader.ReadInnerXml();
		}

		// Token: 0x06000D24 RID: 3364 RVA: 0x00030081 File Offset: 0x0002E281
		public override string ReadOuterXml()
		{
			this.MoveToContent();
			return this.reader.ReadOuterXml();
		}

		// Token: 0x06000D25 RID: 3365 RVA: 0x00030095 File Offset: 0x0002E295
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000D26 RID: 3366 RVA: 0x000300A3 File Offset: 0x0002E2A3
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06000D27 RID: 3367 RVA: 0x000300B0 File Offset: 0x0002E2B0
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x0400084B RID: 2123
		private XmlReader reader;

		// Token: 0x0400084C RID: 2124
		private NamespacesMgr namespacesManager;
	}
}

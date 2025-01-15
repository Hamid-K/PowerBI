using System;
using System.Xml;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x020000F0 RID: 240
	internal class SkippingWrapperReader : XmlReader
	{
		// Token: 0x06000CE6 RID: 3302 RVA: 0x0002FA3E File Offset: 0x0002DC3E
		internal SkippingWrapperReader(XmlReader reader)
		{
			this.reader = reader;
			this.namespacesManager = new NamespacesMgr();
		}

		// Token: 0x170004EF RID: 1263
		// (get) Token: 0x06000CE7 RID: 3303 RVA: 0x0002FA58 File Offset: 0x0002DC58
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x170004F0 RID: 1264
		// (get) Token: 0x06000CE8 RID: 3304 RVA: 0x0002FA65 File Offset: 0x0002DC65
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x170004F1 RID: 1265
		// (get) Token: 0x06000CE9 RID: 3305 RVA: 0x0002FA72 File Offset: 0x0002DC72
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x170004F2 RID: 1266
		// (get) Token: 0x06000CEA RID: 3306 RVA: 0x0002FA7F File Offset: 0x0002DC7F
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x170004F3 RID: 1267
		// (get) Token: 0x06000CEB RID: 3307 RVA: 0x0002FA8C File Offset: 0x0002DC8C
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x170004F4 RID: 1268
		// (get) Token: 0x06000CEC RID: 3308 RVA: 0x0002FA99 File Offset: 0x0002DC99
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x0002FAA6 File Offset: 0x0002DCA6
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06000CEE RID: 3310 RVA: 0x0002FAB3 File Offset: 0x0002DCB3
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06000CEF RID: 3311 RVA: 0x0002FAC0 File Offset: 0x0002DCC0
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06000CF0 RID: 3312 RVA: 0x0002FACD File Offset: 0x0002DCCD
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000CF1 RID: 3313 RVA: 0x0002FADA File Offset: 0x0002DCDA
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000CF2 RID: 3314 RVA: 0x0002FAE7 File Offset: 0x0002DCE7
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000CF3 RID: 3315 RVA: 0x0002FAF4 File Offset: 0x0002DCF4
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000CF4 RID: 3316 RVA: 0x0002FB01 File Offset: 0x0002DD01
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000CF5 RID: 3317 RVA: 0x0002FB0E File Offset: 0x0002DD0E
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000CF6 RID: 3318 RVA: 0x0002FB1B File Offset: 0x0002DD1B
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000CF7 RID: 3319 RVA: 0x0002FB28 File Offset: 0x0002DD28
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06000CF8 RID: 3320 RVA: 0x0002FB35 File Offset: 0x0002DD35
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x06000CF9 RID: 3321 RVA: 0x0002FB42 File Offset: 0x0002DD42
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x06000CFA RID: 3322 RVA: 0x0002FB4F File Offset: 0x0002DD4F
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000503 RID: 1283
		public override string this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x17000504 RID: 1284
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17000505 RID: 1285
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.reader[name, namespaceURI];
			}
		}

		// Token: 0x06000CFE RID: 3326 RVA: 0x0002FB87 File Offset: 0x0002DD87
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002FB95 File Offset: 0x0002DD95
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002FBA4 File Offset: 0x0002DDA4
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x0002FBB2 File Offset: 0x0002DDB2
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002FBC0 File Offset: 0x0002DDC0
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x0002FBCF File Offset: 0x0002DDCF
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x0002FBDD File Offset: 0x0002DDDD
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x0002FBEA File Offset: 0x0002DDEA
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06000D06 RID: 3334 RVA: 0x0002FBF7 File Offset: 0x0002DDF7
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06000D07 RID: 3335 RVA: 0x0002FC04 File Offset: 0x0002DE04
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06000D08 RID: 3336 RVA: 0x0002FC11 File Offset: 0x0002DE11
		public override void Close()
		{
		}

		// Token: 0x06000D09 RID: 3337 RVA: 0x0002FC13 File Offset: 0x0002DE13
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06000D0A RID: 3338 RVA: 0x0002FC20 File Offset: 0x0002DE20
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06000D0B RID: 3339 RVA: 0x0002FC2D File Offset: 0x0002DE2D
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

		// Token: 0x06000D0C RID: 3340 RVA: 0x0002FC6C File Offset: 0x0002DE6C
		public override void ReadStartElement()
		{
			this.MoveToContent();
			this.reader.ReadStartElement();
		}

		// Token: 0x06000D0D RID: 3341 RVA: 0x0002FC80 File Offset: 0x0002DE80
		public override void ReadStartElement(string name)
		{
			this.MoveToContent();
			this.reader.ReadStartElement(name);
		}

		// Token: 0x06000D0E RID: 3342 RVA: 0x0002FC95 File Offset: 0x0002DE95
		public override void ReadStartElement(string localname, string ns)
		{
			this.MoveToContent();
			this.reader.ReadStartElement(localname, ns);
		}

		// Token: 0x06000D0F RID: 3343 RVA: 0x0002FCAB File Offset: 0x0002DEAB
		public override string ReadElementString()
		{
			this.MoveToContent();
			return this.reader.ReadElementString();
		}

		// Token: 0x06000D10 RID: 3344 RVA: 0x0002FCBF File Offset: 0x0002DEBF
		public override string ReadElementString(string name)
		{
			this.MoveToContent();
			return this.reader.ReadElementString(name);
		}

		// Token: 0x06000D11 RID: 3345 RVA: 0x0002FCD4 File Offset: 0x0002DED4
		public override string ReadElementString(string localname, string ns)
		{
			this.MoveToContent();
			return this.reader.ReadElementString(localname, ns);
		}

		// Token: 0x06000D12 RID: 3346 RVA: 0x0002FCEA File Offset: 0x0002DEEA
		public override void ReadEndElement()
		{
			this.MoveToContent();
			this.reader.ReadEndElement();
		}

		// Token: 0x06000D13 RID: 3347 RVA: 0x0002FCFE File Offset: 0x0002DEFE
		public override bool IsStartElement()
		{
			this.MoveToContent();
			return this.reader.IsStartElement();
		}

		// Token: 0x06000D14 RID: 3348 RVA: 0x0002FD12 File Offset: 0x0002DF12
		public override bool IsStartElement(string name)
		{
			this.MoveToContent();
			return this.reader.IsStartElement(name);
		}

		// Token: 0x06000D15 RID: 3349 RVA: 0x0002FD27 File Offset: 0x0002DF27
		public override bool IsStartElement(string localname, string ns)
		{
			this.MoveToContent();
			return this.reader.IsStartElement(localname, ns);
		}

		// Token: 0x06000D16 RID: 3350 RVA: 0x0002FD3D File Offset: 0x0002DF3D
		public override string ReadInnerXml()
		{
			this.MoveToContent();
			return this.reader.ReadInnerXml();
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x0002FD51 File Offset: 0x0002DF51
		public override string ReadOuterXml()
		{
			this.MoveToContent();
			return this.reader.ReadOuterXml();
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x0002FD65 File Offset: 0x0002DF65
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x0002FD73 File Offset: 0x0002DF73
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x0002FD80 File Offset: 0x0002DF80
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x0400083E RID: 2110
		private XmlReader reader;

		// Token: 0x0400083F RID: 2111
		private NamespacesMgr namespacesManager;
	}
}

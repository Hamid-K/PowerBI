using System;
using System.Xml;

namespace Microsoft.Reporting
{
	// Token: 0x020000CC RID: 204
	internal abstract class XmlReaderWrapper : XmlReader
	{
		// Token: 0x06000D0A RID: 3338 RVA: 0x00021CD4 File Offset: 0x0001FED4
		protected XmlReaderWrapper(XmlReader xr)
		{
			this._xr = ArgumentValidation.CheckNotNull<XmlReader>(xr, "xr");
		}

		// Token: 0x1700043F RID: 1087
		// (get) Token: 0x06000D0B RID: 3339 RVA: 0x00021CED File Offset: 0x0001FEED
		public override int AttributeCount
		{
			get
			{
				return this._xr.AttributeCount;
			}
		}

		// Token: 0x17000440 RID: 1088
		// (get) Token: 0x06000D0C RID: 3340 RVA: 0x00021CFA File Offset: 0x0001FEFA
		public override string BaseURI
		{
			get
			{
				return this._xr.BaseURI;
			}
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x06000D0D RID: 3341 RVA: 0x00021D07 File Offset: 0x0001FF07
		public override int Depth
		{
			get
			{
				return this._xr.Depth;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x06000D0E RID: 3342 RVA: 0x00021D14 File Offset: 0x0001FF14
		public override bool EOF
		{
			get
			{
				return this._xr.EOF;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x06000D0F RID: 3343 RVA: 0x00021D21 File Offset: 0x0001FF21
		public override bool IsEmptyElement
		{
			get
			{
				return this._xr.IsEmptyElement;
			}
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x06000D10 RID: 3344 RVA: 0x00021D2E File Offset: 0x0001FF2E
		public override string LocalName
		{
			get
			{
				return this._xr.LocalName;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06000D11 RID: 3345 RVA: 0x00021D3B File Offset: 0x0001FF3B
		public override XmlNameTable NameTable
		{
			get
			{
				return this._xr.NameTable;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06000D12 RID: 3346 RVA: 0x00021D48 File Offset: 0x0001FF48
		public override string NamespaceURI
		{
			get
			{
				return this._xr.NamespaceURI;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06000D13 RID: 3347 RVA: 0x00021D55 File Offset: 0x0001FF55
		public override XmlNodeType NodeType
		{
			get
			{
				return this._xr.NodeType;
			}
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06000D14 RID: 3348 RVA: 0x00021D62 File Offset: 0x0001FF62
		public override string Prefix
		{
			get
			{
				return this._xr.Prefix;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06000D15 RID: 3349 RVA: 0x00021D6F File Offset: 0x0001FF6F
		public override ReadState ReadState
		{
			get
			{
				return this._xr.ReadState;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06000D16 RID: 3350 RVA: 0x00021D7C File Offset: 0x0001FF7C
		public override string Value
		{
			get
			{
				return this._xr.Value;
			}
		}

		// Token: 0x06000D17 RID: 3351 RVA: 0x00021D89 File Offset: 0x0001FF89
		public override string GetAttribute(int i)
		{
			return this._xr.GetAttribute(i);
		}

		// Token: 0x06000D18 RID: 3352 RVA: 0x00021D97 File Offset: 0x0001FF97
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this._xr.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06000D19 RID: 3353 RVA: 0x00021DA6 File Offset: 0x0001FFA6
		public override string GetAttribute(string name)
		{
			return this._xr.GetAttribute(name);
		}

		// Token: 0x06000D1A RID: 3354 RVA: 0x00021DB4 File Offset: 0x0001FFB4
		public override string LookupNamespace(string prefix)
		{
			return this._xr.LookupNamespace(prefix);
		}

		// Token: 0x06000D1B RID: 3355 RVA: 0x00021DC2 File Offset: 0x0001FFC2
		public override bool MoveToAttribute(string name, string ns)
		{
			return this._xr.MoveToAttribute(name, ns);
		}

		// Token: 0x06000D1C RID: 3356 RVA: 0x00021DD1 File Offset: 0x0001FFD1
		public override bool MoveToAttribute(string name)
		{
			return this._xr.MoveToAttribute(name);
		}

		// Token: 0x06000D1D RID: 3357 RVA: 0x00021DDF File Offset: 0x0001FFDF
		public override bool MoveToElement()
		{
			return this._xr.MoveToElement();
		}

		// Token: 0x06000D1E RID: 3358 RVA: 0x00021DEC File Offset: 0x0001FFEC
		public override bool MoveToFirstAttribute()
		{
			return this._xr.MoveToFirstAttribute();
		}

		// Token: 0x06000D1F RID: 3359 RVA: 0x00021DF9 File Offset: 0x0001FFF9
		public override bool MoveToNextAttribute()
		{
			return this._xr.MoveToNextAttribute();
		}

		// Token: 0x06000D20 RID: 3360 RVA: 0x00021E06 File Offset: 0x00020006
		public override bool Read()
		{
			return this._xr.Read();
		}

		// Token: 0x06000D21 RID: 3361 RVA: 0x00021E13 File Offset: 0x00020013
		public override bool ReadAttributeValue()
		{
			return this._xr.ReadAttributeValue();
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x00021E20 File Offset: 0x00020020
		public override void ResolveEntity()
		{
			this._xr.ResolveEntity();
		}

		// Token: 0x04000972 RID: 2418
		private readonly XmlReader _xr;
	}
}

using System;
using System.Xml;

namespace DocumentFormat.OpenXml
{
	// Token: 0x02002102 RID: 8450
	public struct OpenXmlAttribute : IEquatable<OpenXmlAttribute>
	{
		// Token: 0x0600D037 RID: 53303 RVA: 0x0029682B File Offset: 0x00294A2B
		public OpenXmlAttribute(string qualifiedName, string namespaceUri, string value)
		{
			if (string.IsNullOrEmpty(qualifiedName))
			{
				throw new ArgumentNullException("qualifiedName");
			}
			this._namespaceUri = namespaceUri;
			OpenXmlElement.SplitName(qualifiedName, out this._prefix, out this._tagName);
			this._value = value;
		}

		// Token: 0x0600D038 RID: 53304 RVA: 0x00296860 File Offset: 0x00294A60
		public OpenXmlAttribute(string prefix, string localName, string namespaceUri, string value)
		{
			if (string.IsNullOrEmpty(localName))
			{
				throw new ArgumentNullException("localName");
			}
			this._namespaceUri = namespaceUri;
			this._tagName = localName;
			this._prefix = prefix;
			this._value = value;
		}

		// Token: 0x1700324F RID: 12879
		// (get) Token: 0x0600D039 RID: 53305 RVA: 0x00296892 File Offset: 0x00294A92
		// (set) Token: 0x0600D03A RID: 53306 RVA: 0x002968A8 File Offset: 0x00294AA8
		public string NamespaceUri
		{
			get
			{
				if (this._namespaceUri == null)
				{
					return string.Empty;
				}
				return this._namespaceUri;
			}
			set
			{
				this._namespaceUri = value;
			}
		}

		// Token: 0x17003250 RID: 12880
		// (get) Token: 0x0600D03B RID: 53307 RVA: 0x002968B1 File Offset: 0x00294AB1
		// (set) Token: 0x0600D03C RID: 53308 RVA: 0x002968B9 File Offset: 0x00294AB9
		public string LocalName
		{
			get
			{
				return this._tagName;
			}
			set
			{
				this._tagName = value;
			}
		}

		// Token: 0x17003251 RID: 12881
		// (get) Token: 0x0600D03D RID: 53309 RVA: 0x002968C2 File Offset: 0x00294AC2
		// (set) Token: 0x0600D03E RID: 53310 RVA: 0x002968CA File Offset: 0x00294ACA
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
			set
			{
				this._prefix = value;
			}
		}

		// Token: 0x17003252 RID: 12882
		// (get) Token: 0x0600D03F RID: 53311 RVA: 0x002968D3 File Offset: 0x00294AD3
		// (set) Token: 0x0600D040 RID: 53312 RVA: 0x002968DB File Offset: 0x00294ADB
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x17003253 RID: 12883
		// (get) Token: 0x0600D041 RID: 53313 RVA: 0x002968E4 File Offset: 0x00294AE4
		public XmlQualifiedName XmlQualifiedName
		{
			get
			{
				return new XmlQualifiedName(this._tagName, this._namespaceUri);
			}
		}

		// Token: 0x0600D042 RID: 53314 RVA: 0x002968F8 File Offset: 0x00294AF8
		public bool Equals(OpenXmlAttribute other)
		{
			return this.LocalName == other.LocalName && this.NamespaceUri == other.NamespaceUri && this.Prefix == other.Prefix && this.Value == other.Value;
		}

		// Token: 0x0600D043 RID: 53315 RVA: 0x00296955 File Offset: 0x00294B55
		public static bool operator ==(OpenXmlAttribute attribute1, OpenXmlAttribute attribute2)
		{
			return attribute1.Equals(attribute2);
		}

		// Token: 0x0600D044 RID: 53316 RVA: 0x0029695F File Offset: 0x00294B5F
		public static bool operator !=(OpenXmlAttribute attribute1, OpenXmlAttribute attribute2)
		{
			return !(attribute1 == attribute2);
		}

		// Token: 0x0600D045 RID: 53317 RVA: 0x0029696C File Offset: 0x00294B6C
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is OpenXmlAttribute))
			{
				return false;
			}
			OpenXmlAttribute openXmlAttribute = (OpenXmlAttribute)obj;
			return this.Equals(openXmlAttribute);
		}

		// Token: 0x0600D046 RID: 53318 RVA: 0x00296994 File Offset: 0x00294B94
		public override int GetHashCode()
		{
			int num = 0;
			if (this.LocalName != null)
			{
				num ^= this.LocalName.GetHashCode();
			}
			if (this.NamespaceUri != null)
			{
				num ^= this.NamespaceUri.GetHashCode();
			}
			if (this.Prefix != null)
			{
				num ^= this.Prefix.GetHashCode();
			}
			if (this.Value != null)
			{
				num ^= this.Value.GetHashCode();
			}
			return num;
		}

		// Token: 0x040068E7 RID: 26855
		private string _namespaceUri;

		// Token: 0x040068E8 RID: 26856
		private string _tagName;

		// Token: 0x040068E9 RID: 26857
		private string _prefix;

		// Token: 0x040068EA RID: 26858
		private string _value;
	}
}

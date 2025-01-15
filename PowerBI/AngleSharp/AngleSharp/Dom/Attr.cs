using System;
using AngleSharp.Dom.Collections;
using AngleSharp.Extensions;
using AngleSharp.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000149 RID: 329
	internal sealed class Attr : IAttr, IEquatable<IAttr>
	{
		// Token: 0x060009FC RID: 2556 RVA: 0x00040787 File Offset: 0x0003E987
		internal Attr(string localName)
			: this(localName, string.Empty)
		{
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x00040795 File Offset: 0x0003E995
		internal Attr(string localName, string value)
		{
			this._localName = localName;
			this._value = value;
		}

		// Token: 0x060009FE RID: 2558 RVA: 0x000407AB File Offset: 0x0003E9AB
		internal Attr(string prefix, string localName, string value, string namespaceUri)
		{
			this._prefix = prefix;
			this._localName = localName;
			this._value = value;
			this._namespace = namespaceUri;
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x000407D0 File Offset: 0x0003E9D0
		// (set) Token: 0x06000A00 RID: 2560 RVA: 0x000407D8 File Offset: 0x0003E9D8
		internal NamedNodeMap Container { get; set; }

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000A01 RID: 2561 RVA: 0x000407E1 File Offset: 0x0003E9E1
		public string Prefix
		{
			get
			{
				return this._prefix;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x000407E9 File Offset: 0x0003E9E9
		public bool IsId
		{
			get
			{
				return this._prefix == null && this._localName.Isi(AttributeNames.Id);
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000A03 RID: 2563 RVA: 0x00040805 File Offset: 0x0003EA05
		public bool Specified
		{
			get
			{
				return !string.IsNullOrEmpty(this._value);
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00040815 File Offset: 0x0003EA15
		public string Name
		{
			get
			{
				if (this._prefix != null)
				{
					return this._prefix + ":" + this._localName;
				}
				return this._localName;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000A05 RID: 2565 RVA: 0x0004083C File Offset: 0x0003EA3C
		// (set) Token: 0x06000A06 RID: 2566 RVA: 0x00040844 File Offset: 0x0003EA44
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				string value2 = this._value;
				this._value = value;
				NamedNodeMap container = this.Container;
				if (container == null)
				{
					return;
				}
				container.RaiseChangedEvent(this, value, value2);
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000A07 RID: 2567 RVA: 0x00040872 File Offset: 0x0003EA72
		public string LocalName
		{
			get
			{
				return this._localName;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000A08 RID: 2568 RVA: 0x0004087A File Offset: 0x0003EA7A
		public string NamespaceUri
		{
			get
			{
				return this._namespace;
			}
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x00040882 File Offset: 0x0003EA82
		public bool Equals(IAttr other)
		{
			return this.Prefix.Is(other.Prefix) && this.NamespaceUri.Is(other.NamespaceUri) && this.Value.Is(other.Value);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000408C0 File Offset: 0x0003EAC0
		public override int GetHashCode()
		{
			return (((1 * 31 + this._localName.GetHashCode()) * 31 + (this._value ?? string.Empty).GetHashCode()) * 31 + (this._namespace ?? string.Empty).GetHashCode()) * 31 + (this._prefix ?? string.Empty).GetHashCode();
		}

		// Token: 0x04000908 RID: 2312
		private readonly string _localName;

		// Token: 0x04000909 RID: 2313
		private readonly string _prefix;

		// Token: 0x0400090A RID: 2314
		private readonly string _namespace;

		// Token: 0x0400090B RID: 2315
		private string _value;
	}
}

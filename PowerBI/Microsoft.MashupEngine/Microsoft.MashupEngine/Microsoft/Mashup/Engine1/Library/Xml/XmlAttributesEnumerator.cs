using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Xml
{
	// Token: 0x02000281 RID: 641
	internal class XmlAttributesEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x06001A64 RID: 6756 RVA: 0x0003500E File Offset: 0x0003320E
		public XmlAttributesEnumerator(XmlAttributeCollection attributes)
		{
			this.attributes = attributes;
			this.index = 0;
		}

		// Token: 0x17000CE7 RID: 3303
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x00035024 File Offset: 0x00033224
		public IValueReference Current
		{
			get
			{
				return this.current;
			}
		}

		// Token: 0x17000CE8 RID: 3304
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x0003502C File Offset: 0x0003322C
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x0000EE09 File Offset: 0x0000D009
		public void Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x0000336E File Offset: 0x0000156E
		public void Dispose()
		{
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x00035034 File Offset: 0x00033234
		public bool MoveNext()
		{
			while (this.index < this.attributes.Count)
			{
				XmlAttributeCollection xmlAttributeCollection = this.attributes;
				int num = this.index;
				this.index = num + 1;
				XmlAttribute xmlAttribute = xmlAttributeCollection[num];
				if (!(xmlAttribute.Prefix == "xml") && !(xmlAttribute.Prefix == "xmlns") && (xmlAttribute.Prefix.Length != 0 || !(xmlAttribute.LocalName == "xmlns")))
				{
					this.current = RecordValue.New(XmlAttributesEnumerator.AttributeKeys, new Value[]
					{
						TextValue.New(xmlAttribute.LocalName),
						TextValue.New(xmlAttribute.NamespaceURI),
						TextValue.New(xmlAttribute.Value)
					});
					return true;
				}
			}
			this.current = null;
			return false;
		}

		// Token: 0x040007CD RID: 1997
		private const string XML = "xml";

		// Token: 0x040007CE RID: 1998
		private const string XMLNS = "xmlns";

		// Token: 0x040007CF RID: 1999
		private static readonly Keys AttributeKeys = Keys.New("Name", "Namespace", "Value");

		// Token: 0x040007D0 RID: 2000
		public static readonly TableTypeValue TableType = TableTypeValue.New(RecordTypeValue.New(RecordValue.New(XmlAttributesEnumerator.AttributeKeys, new Value[]
		{
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			}),
			RecordValue.New(RecordTypeValue.RecordFieldKeys, new Value[]
			{
				TypeValue.Text,
				LogicalValue.False
			})
		}), false));

		// Token: 0x040007D1 RID: 2001
		private readonly XmlAttributeCollection attributes;

		// Token: 0x040007D2 RID: 2002
		private int index;

		// Token: 0x040007D3 RID: 2003
		private Value current;
	}
}

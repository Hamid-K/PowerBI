using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x0200008A RID: 138
	public sealed class StringList : ComplexProperty, IEnumerable<string>, IEnumerable, IJsonCollectionDeserializer
	{
		// Token: 0x06000622 RID: 1570 RVA: 0x00014FB8 File Offset: 0x00013FB8
		public StringList()
		{
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x00014FD6 File Offset: 0x00013FD6
		public StringList(IEnumerable<string> strings)
		{
			this.AddRange(strings);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x00014FFB File Offset: 0x00013FFB
		internal StringList(string itemXmlElementName)
		{
			this.itemXmlElementName = itemXmlElementName;
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x00015020 File Offset: 0x00014020
		internal override bool TryReadElementFromXml(EwsServiceXmlReader reader)
		{
			if (reader.LocalName == this.itemXmlElementName)
			{
				this.Add(reader.ReadValue());
				return true;
			}
			return false;
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x00015044 File Offset: 0x00014044
		void IJsonCollectionDeserializer.CreateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			foreach (object obj in jsonCollection)
			{
				this.Add(obj as string);
			}
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x00015071 File Offset: 0x00014071
		void IJsonCollectionDeserializer.UpdateFromJsonCollection(object[] jsonCollection, ExchangeService service)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00015078 File Offset: 0x00014078
		internal override void WriteElementsToXml(EwsServiceXmlWriter writer)
		{
			foreach (string text in this)
			{
				writer.WriteStartElement(XmlNamespace.Types, this.itemXmlElementName);
				writer.WriteValue(text, this.itemXmlElementName);
				writer.WriteEndElement();
			}
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x000150DC File Offset: 0x000140DC
		internal override object InternalToJson(ExchangeService service)
		{
			return new List<string>(this).ToArray();
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x000150E9 File Offset: 0x000140E9
		public void Add(string s)
		{
			this.items.Add(s);
			this.Changed();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00015100 File Offset: 0x00014100
		public void AddRange(IEnumerable<string> strings)
		{
			bool flag = false;
			foreach (string text in strings)
			{
				if (!this.Contains(text))
				{
					this.items.Add(text);
					flag = true;
				}
			}
			if (flag)
			{
				this.Changed();
			}
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00015164 File Offset: 0x00014164
		public bool Contains(string s)
		{
			return this.items.Contains(s);
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00015174 File Offset: 0x00014174
		public bool Remove(string s)
		{
			bool flag = this.items.Remove(s);
			if (flag)
			{
				this.Changed();
			}
			return flag;
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00015198 File Offset: 0x00014198
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			this.items.RemoveAt(index);
			this.Changed();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x000151CE File Offset: 0x000141CE
		public void Clear()
		{
			this.items.Clear();
			this.Changed();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x000151E1 File Offset: 0x000141E1
		public override string ToString()
		{
			return string.Join(",", this.items.ToArray());
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x000151F8 File Offset: 0x000141F8
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x17000156 RID: 342
		public string this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				return this.items[index];
			}
			set
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
				}
				if (this.items[index] != value)
				{
					this.items[index] = value;
					this.Changed();
				}
			}
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x0001528E File Offset: 0x0001428E
		public IEnumerator<string> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000635 RID: 1589 RVA: 0x000152A0 File Offset: 0x000142A0
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x06000636 RID: 1590 RVA: 0x000152B4 File Offset: 0x000142B4
		public override bool Equals(object obj)
		{
			StringList stringList = obj as StringList;
			return stringList != null && this.ToString().Equals(stringList.ToString());
		}

		// Token: 0x06000637 RID: 1591 RVA: 0x000152DE File Offset: 0x000142DE
		public override int GetHashCode()
		{
			return this.ToString().GetHashCode();
		}

		// Token: 0x04000207 RID: 519
		private List<string> items = new List<string>();

		// Token: 0x04000208 RID: 520
		private string itemXmlElementName = "String";
	}
}

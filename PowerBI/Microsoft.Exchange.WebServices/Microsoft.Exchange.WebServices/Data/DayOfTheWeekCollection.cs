using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020000AD RID: 173
	public sealed class DayOfTheWeekCollection : ComplexProperty, IEnumerable<DayOfTheWeek>, IEnumerable
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0001A5D4 File Offset: 0x000195D4
		internal DayOfTheWeekCollection()
		{
		}

		// Token: 0x060007B4 RID: 1972 RVA: 0x0001A5E8 File Offset: 0x000195E8
		private string ToString(string separator)
		{
			if (this.Count == 0)
			{
				return string.Empty;
			}
			string[] array = new string[this.Count];
			for (int i = 0; i < this.Count; i++)
			{
				array[i] = this[i].ToString();
			}
			return string.Join(separator, array);
		}

		// Token: 0x060007B5 RID: 1973 RVA: 0x0001A63C File Offset: 0x0001963C
		internal override void LoadFromXml(EwsServiceXmlReader reader, string xmlElementName)
		{
			reader.EnsureCurrentNodeIsStartElement(XmlNamespace.Types, xmlElementName);
			EwsUtilities.ParseEnumValueList<DayOfTheWeek>(this.items, reader.ReadElementValue(), new char[] { ' ' });
		}

		// Token: 0x060007B6 RID: 1974 RVA: 0x0001A66F File Offset: 0x0001966F
		internal override void LoadFromJson(JsonObject jsonProperty, ExchangeService service)
		{
			throw new InvalidOperationException();
		}

		// Token: 0x060007B7 RID: 1975 RVA: 0x0001A678 File Offset: 0x00019678
		internal void LoadFromJsonValue(string jsonValue)
		{
			EwsUtilities.ParseEnumValueList<DayOfTheWeek>(this.items, jsonValue, new char[] { ' ' });
		}

		// Token: 0x060007B8 RID: 1976 RVA: 0x0001A6A0 File Offset: 0x000196A0
		internal override void WriteToXml(EwsServiceXmlWriter writer, string xmlElementName)
		{
			string text = this.ToString(" ");
			if (!string.IsNullOrEmpty(text))
			{
				writer.WriteElementValue(XmlNamespace.Types, "DaysOfWeek", text);
			}
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x0001A6CE File Offset: 0x000196CE
		internal override object InternalToJson(ExchangeService service)
		{
			return this.ToString(" ");
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001A6DB File Offset: 0x000196DB
		public override string ToString()
		{
			return this.ToString(",");
		}

		// Token: 0x060007BB RID: 1979 RVA: 0x0001A6E8 File Offset: 0x000196E8
		public void Add(DayOfTheWeek dayOfTheWeek)
		{
			if (!this.items.Contains(dayOfTheWeek))
			{
				this.items.Add(dayOfTheWeek);
				this.Changed();
			}
		}

		// Token: 0x060007BC RID: 1980 RVA: 0x0001A70C File Offset: 0x0001970C
		public void AddRange(IEnumerable<DayOfTheWeek> daysOfTheWeek)
		{
			foreach (DayOfTheWeek dayOfTheWeek in daysOfTheWeek)
			{
				this.Add(dayOfTheWeek);
			}
		}

		// Token: 0x060007BD RID: 1981 RVA: 0x0001A754 File Offset: 0x00019754
		public void Clear()
		{
			if (this.Count > 0)
			{
				this.items.Clear();
				this.Changed();
			}
		}

		// Token: 0x060007BE RID: 1982 RVA: 0x0001A770 File Offset: 0x00019770
		public bool Remove(DayOfTheWeek dayOfTheWeek)
		{
			bool flag = this.items.Remove(dayOfTheWeek);
			if (flag)
			{
				this.Changed();
			}
			return flag;
		}

		// Token: 0x060007BF RID: 1983 RVA: 0x0001A794 File Offset: 0x00019794
		public void RemoveAt(int index)
		{
			if (index < 0 || index >= this.Count)
			{
				throw new ArgumentOutOfRangeException("index", Strings.IndexIsOutOfRange);
			}
			this.items.RemoveAt(index);
			this.Changed();
		}

		// Token: 0x170001E0 RID: 480
		public DayOfTheWeek this[int index]
		{
			get
			{
				return this.items[index];
			}
		}

		// Token: 0x170001E1 RID: 481
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0001A7D8 File Offset: 0x000197D8
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001A7E5 File Offset: 0x000197E5
		public IEnumerator<DayOfTheWeek> GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001A7F7 File Offset: 0x000197F7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.items.GetEnumerator();
		}

		// Token: 0x04000290 RID: 656
		private List<DayOfTheWeek> items = new List<DayOfTheWeek>();
	}
}

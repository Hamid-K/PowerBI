using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x0200019B RID: 411
	internal class XmlElementValueCollection : IEnumerable<XmlElementValue>, IEnumerable
	{
		// Token: 0x060007F3 RID: 2035 RVA: 0x00013ABE File Offset: 0x00011CBE
		private XmlElementValueCollection(IList<XmlElementValue> list, ILookup<string, XmlElementValue> nameMap)
		{
			this.values = list;
			this.nameLookup = nameMap;
		}

		// Token: 0x17000343 RID: 835
		// (get) Token: 0x060007F4 RID: 2036 RVA: 0x00013AD4 File Offset: 0x00011CD4
		internal XmlTextValue FirstText
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlTextValue>(this.values.OfText()) ?? XmlTextValue.Missing;
			}
		}

		// Token: 0x17000344 RID: 836
		internal XmlElementValue this[string elementName]
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlElementValue>(this.EnsureLookup()[elementName]) ?? XmlElementValueCollection.MissingXmlElementValue.Instance;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x00013B0B File Offset: 0x00011D0B
		public IEnumerator<XmlElementValue> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x060007F7 RID: 2039 RVA: 0x00013B18 File Offset: 0x00011D18
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00013B25 File Offset: 0x00011D25
		internal static XmlElementValueCollection FromList(IList<XmlElementValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return XmlElementValueCollection.empty;
			}
			return new XmlElementValueCollection(values, null);
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00013B3F File Offset: 0x00011D3F
		internal IEnumerable<XmlElementValue> FindByName(string elementName)
		{
			return this.EnsureLookup()[elementName];
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00013B4D File Offset: 0x00011D4D
		internal IEnumerable<XmlElementValue<TResult>> FindByName<TResult>(string elementName) where TResult : class
		{
			return this.FindByName(elementName).OfResultType<TResult>();
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00013B64 File Offset: 0x00011D64
		private ILookup<string, XmlElementValue> EnsureLookup()
		{
			ILookup<string, XmlElementValue> lookup;
			if ((lookup = this.nameLookup) == null)
			{
				lookup = (this.nameLookup = Enumerable.ToLookup<XmlElementValue, string>(this.values, (XmlElementValue value) => value.Name));
			}
			return lookup;
		}

		// Token: 0x04000416 RID: 1046
		private static readonly XmlElementValueCollection empty = new XmlElementValueCollection(new XmlElementValue[0], Enumerable.ToLookup<XmlElementValue, string>(new XmlElementValue[0], (XmlElementValue value) => value.Name));

		// Token: 0x04000417 RID: 1047
		private readonly IList<XmlElementValue> values;

		// Token: 0x04000418 RID: 1048
		private ILookup<string, XmlElementValue> nameLookup;

		// Token: 0x0200019D RID: 413
		internal sealed class MissingXmlElementValue : XmlElementValue
		{
			// Token: 0x06000809 RID: 2057 RVA: 0x00013C43 File Offset: 0x00011E43
			private MissingXmlElementValue()
				: base(null, null)
			{
			}

			// Token: 0x1700034B RID: 843
			// (get) Token: 0x0600080A RID: 2058 RVA: 0x00013C4D File Offset: 0x00011E4D
			internal override object UntypedValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x1700034C RID: 844
			// (get) Token: 0x0600080B RID: 2059 RVA: 0x00013C50 File Offset: 0x00011E50
			internal override bool IsUsed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400041D RID: 1053
			internal static readonly XmlElementValueCollection.MissingXmlElementValue Instance = new XmlElementValueCollection.MissingXmlElementValue();
		}
	}
}

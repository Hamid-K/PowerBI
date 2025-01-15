using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C4 RID: 452
	internal class XmlElementValueCollection : IEnumerable<XmlElementValue>, IEnumerable
	{
		// Token: 0x06000CFC RID: 3324 RVA: 0x000257BE File Offset: 0x000239BE
		private XmlElementValueCollection(IList<XmlElementValue> list, ILookup<string, XmlElementValue> nameMap)
		{
			this.values = list;
			this.nameLookup = nameMap;
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06000CFD RID: 3325 RVA: 0x000257D4 File Offset: 0x000239D4
		internal XmlTextValue FirstText
		{
			get
			{
				return this.values.OfText().FirstOrDefault<XmlTextValue>() ?? XmlTextValue.Missing;
			}
		}

		// Token: 0x17000436 RID: 1078
		internal XmlElementValue this[string elementName]
		{
			get
			{
				return this.EnsureLookup()[elementName].FirstOrDefault<XmlElementValue>() ?? XmlElementValueCollection.MissingXmlElementValue.Instance;
			}
		}

		// Token: 0x06000CFF RID: 3327 RVA: 0x0002580B File Offset: 0x00023A0B
		public IEnumerator<XmlElementValue> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06000D00 RID: 3328 RVA: 0x0002580B File Offset: 0x00023A0B
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06000D01 RID: 3329 RVA: 0x00025818 File Offset: 0x00023A18
		internal bool Remove(XmlElementValue value)
		{
			return value != null && this.values.Remove(value);
		}

		// Token: 0x06000D02 RID: 3330 RVA: 0x0002582B File Offset: 0x00023A2B
		internal static XmlElementValueCollection FromList(IList<XmlElementValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return XmlElementValueCollection.empty;
			}
			return new XmlElementValueCollection(values, null);
		}

		// Token: 0x06000D03 RID: 3331 RVA: 0x00025845 File Offset: 0x00023A45
		internal IEnumerable<XmlElementValue> FindByName(string elementName)
		{
			return this.EnsureLookup()[elementName];
		}

		// Token: 0x06000D04 RID: 3332 RVA: 0x00025853 File Offset: 0x00023A53
		internal IEnumerable<XmlElementValue<TResult>> FindByName<TResult>(string elementName) where TResult : class
		{
			return this.FindByName(elementName).OfResultType<TResult>();
		}

		// Token: 0x06000D05 RID: 3333 RVA: 0x00025864 File Offset: 0x00023A64
		private ILookup<string, XmlElementValue> EnsureLookup()
		{
			ILookup<string, XmlElementValue> lookup;
			if ((lookup = this.nameLookup) == null)
			{
				lookup = (this.nameLookup = this.values.ToLookup((XmlElementValue value) => value.Name));
			}
			return lookup;
		}

		// Token: 0x04000737 RID: 1847
		private static readonly XmlElementValueCollection empty = new XmlElementValueCollection(new XmlElementValue[0], new XmlElementValue[0].ToLookup((XmlElementValue value) => value.Name));

		// Token: 0x04000738 RID: 1848
		private readonly IList<XmlElementValue> values;

		// Token: 0x04000739 RID: 1849
		private ILookup<string, XmlElementValue> nameLookup;

		// Token: 0x02000303 RID: 771
		internal sealed class MissingXmlElementValue : XmlElementValue
		{
			// Token: 0x060011AD RID: 4525 RVA: 0x0002E9F3 File Offset: 0x0002CBF3
			private MissingXmlElementValue()
				: base(null, null)
			{
			}

			// Token: 0x170004F8 RID: 1272
			// (get) Token: 0x060011AE RID: 4526 RVA: 0x000026B0 File Offset: 0x000008B0
			internal override object UntypedValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170004F9 RID: 1273
			// (get) Token: 0x060011AF RID: 4527 RVA: 0x000026A6 File Offset: 0x000008A6
			internal override bool IsUsed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000913 RID: 2323
			internal static readonly XmlElementValueCollection.MissingXmlElementValue Instance = new XmlElementValueCollection.MissingXmlElementValue();
		}
	}
}

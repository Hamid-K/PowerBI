using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B7 RID: 439
	internal class XmlElementValueCollection : IEnumerable<XmlElementValue>, IEnumerable
	{
		// Token: 0x06000C4A RID: 3146 RVA: 0x000235F6 File Offset: 0x000217F6
		private XmlElementValueCollection(IList<XmlElementValue> list, ILookup<string, XmlElementValue> nameMap)
		{
			this.values = list;
			this.nameLookup = nameMap;
		}

		// Token: 0x170003EB RID: 1003
		// (get) Token: 0x06000C4B RID: 3147 RVA: 0x0002360C File Offset: 0x0002180C
		internal XmlTextValue FirstText
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlTextValue>(this.values.OfText()) ?? XmlTextValue.Missing;
			}
		}

		// Token: 0x170003EC RID: 1004
		internal XmlElementValue this[string elementName]
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlElementValue>(this.EnsureLookup()[elementName]) ?? XmlElementValueCollection.MissingXmlElementValue.Instance;
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00023643 File Offset: 0x00021843
		public IEnumerator<XmlElementValue> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00023643 File Offset: 0x00021843
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00023650 File Offset: 0x00021850
		internal bool Remove(XmlElementValue value)
		{
			return value != null && this.values.Remove(value);
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00023663 File Offset: 0x00021863
		internal static XmlElementValueCollection FromList(IList<XmlElementValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return XmlElementValueCollection.empty;
			}
			return new XmlElementValueCollection(values, null);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x0002367D File Offset: 0x0002187D
		internal IEnumerable<XmlElementValue> FindByName(string elementName)
		{
			return this.EnsureLookup()[elementName];
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x0002368B File Offset: 0x0002188B
		internal IEnumerable<XmlElementValue<TResult>> FindByName<TResult>(string elementName) where TResult : class
		{
			return this.FindByName(elementName).OfResultType<TResult>();
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x0002369C File Offset: 0x0002189C
		private ILookup<string, XmlElementValue> EnsureLookup()
		{
			ILookup<string, XmlElementValue> lookup;
			if ((lookup = this.nameLookup) == null)
			{
				lookup = (this.nameLookup = Enumerable.ToLookup<XmlElementValue, string>(this.values, (XmlElementValue value) => value.Name));
			}
			return lookup;
		}

		// Token: 0x040006BE RID: 1726
		private static readonly XmlElementValueCollection empty = new XmlElementValueCollection(new XmlElementValue[0], Enumerable.ToLookup<XmlElementValue, string>(new XmlElementValue[0], (XmlElementValue value) => value.Name));

		// Token: 0x040006BF RID: 1727
		private readonly IList<XmlElementValue> values;

		// Token: 0x040006C0 RID: 1728
		private ILookup<string, XmlElementValue> nameLookup;

		// Token: 0x020002EA RID: 746
		internal sealed class MissingXmlElementValue : XmlElementValue
		{
			// Token: 0x060010CA RID: 4298 RVA: 0x0002C070 File Offset: 0x0002A270
			private MissingXmlElementValue()
				: base(null, null)
			{
			}

			// Token: 0x170004BC RID: 1212
			// (get) Token: 0x060010CB RID: 4299 RVA: 0x00008D69 File Offset: 0x00006F69
			internal override object UntypedValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170004BD RID: 1213
			// (get) Token: 0x060010CC RID: 4300 RVA: 0x00008EC3 File Offset: 0x000070C3
			internal override bool IsUsed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x04000882 RID: 2178
			internal static readonly XmlElementValueCollection.MissingXmlElementValue Instance = new XmlElementValueCollection.MissingXmlElementValue();
		}
	}
}

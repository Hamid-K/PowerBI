using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x0200015A RID: 346
	internal class XmlElementValueCollection : IEnumerable<XmlElementValue>, IEnumerable
	{
		// Token: 0x060006C6 RID: 1734 RVA: 0x000116EA File Offset: 0x0000F8EA
		private XmlElementValueCollection(IList<XmlElementValue> list, ILookup<string, XmlElementValue> nameMap)
		{
			this.values = list;
			this.nameLookup = nameMap;
		}

		// Token: 0x170002CD RID: 717
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x00011700 File Offset: 0x0000F900
		internal XmlTextValue FirstText
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlTextValue>(this.values.OfText()) ?? XmlTextValue.Missing;
			}
		}

		// Token: 0x170002CE RID: 718
		internal XmlElementValue this[string elementName]
		{
			get
			{
				return Enumerable.FirstOrDefault<XmlElementValue>(this.EnsureLookup()[elementName]) ?? XmlElementValueCollection.MissingXmlElementValue.Instance;
			}
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00011737 File Offset: 0x0000F937
		public IEnumerator<XmlElementValue> GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00011744 File Offset: 0x0000F944
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.values.GetEnumerator();
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00011751 File Offset: 0x0000F951
		internal static XmlElementValueCollection FromList(IList<XmlElementValue> values)
		{
			if (values == null || values.Count == 0)
			{
				return XmlElementValueCollection.empty;
			}
			return new XmlElementValueCollection(values, null);
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x0001176B File Offset: 0x0000F96B
		internal IEnumerable<XmlElementValue> FindByName(string elementName)
		{
			return this.EnsureLookup()[elementName];
		}

		// Token: 0x060006CD RID: 1741 RVA: 0x00011779 File Offset: 0x0000F979
		internal IEnumerable<XmlElementValue<TResult>> FindByName<TResult>(string elementName) where TResult : class
		{
			return this.FindByName(elementName).OfResultType<TResult>();
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x00011790 File Offset: 0x0000F990
		private ILookup<string, XmlElementValue> EnsureLookup()
		{
			ILookup<string, XmlElementValue> lookup;
			if ((lookup = this.nameLookup) == null)
			{
				lookup = (this.nameLookup = Enumerable.ToLookup<XmlElementValue, string>(this.values, (XmlElementValue value) => value.Name));
			}
			return lookup;
		}

		// Token: 0x04000386 RID: 902
		private static readonly XmlElementValueCollection empty = new XmlElementValueCollection(new XmlElementValue[0], Enumerable.ToLookup<XmlElementValue, string>(new XmlElementValue[0], (XmlElementValue value) => value.Name));

		// Token: 0x04000387 RID: 903
		private readonly IList<XmlElementValue> values;

		// Token: 0x04000388 RID: 904
		private ILookup<string, XmlElementValue> nameLookup;

		// Token: 0x0200015C RID: 348
		internal sealed class MissingXmlElementValue : XmlElementValue
		{
			// Token: 0x060006DC RID: 1756 RVA: 0x0001186F File Offset: 0x0000FA6F
			private MissingXmlElementValue()
				: base(null, null)
			{
			}

			// Token: 0x170002D5 RID: 725
			// (get) Token: 0x060006DD RID: 1757 RVA: 0x00011879 File Offset: 0x0000FA79
			internal override object UntypedValue
			{
				get
				{
					return null;
				}
			}

			// Token: 0x170002D6 RID: 726
			// (get) Token: 0x060006DE RID: 1758 RVA: 0x0001187C File Offset: 0x0000FA7C
			internal override bool IsUsed
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0400038D RID: 909
			internal static readonly XmlElementValueCollection.MissingXmlElementValue Instance = new XmlElementValueCollection.MissingXmlElementValue();
		}
	}
}

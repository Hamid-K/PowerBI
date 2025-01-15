using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001B4 RID: 436
	internal static class XmlElementValueExtensions
	{
		// Token: 0x06000C3E RID: 3134 RVA: 0x000234CA File Offset: 0x000216CA
		internal static IEnumerable<XmlElementValue<T>> OfResultType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			foreach (XmlElementValue element in elements)
			{
				XmlElementValue<T> xmlElementValue = element as XmlElementValue<T>;
				if (xmlElementValue != null)
				{
					yield return xmlElementValue;
				}
				else if (element.UntypedValue is T)
				{
					yield return new XmlElementValue<T>(element.Name, element.Location, element.ValueAs<T>());
				}
				element = null;
			}
			IEnumerator<XmlElementValue> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000234DA File Offset: 0x000216DA
		internal static IEnumerable<T> ValuesOfType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			return Enumerable.Select<XmlElementValue<T>, T>(elements.OfResultType<T>(), (XmlElementValue<T> ev) => ev.Value);
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00023506 File Offset: 0x00021706
		internal static IEnumerable<XmlTextValue> OfText(this IEnumerable<XmlElementValue> elements)
		{
			foreach (XmlElementValue xmlElementValue in elements)
			{
				if (xmlElementValue.IsText)
				{
					yield return (XmlTextValue)xmlElementValue;
				}
			}
			IEnumerator<XmlElementValue> enumerator = null;
			yield break;
			yield break;
		}
	}
}

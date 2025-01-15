using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x020001C1 RID: 449
	internal static class XmlElementValueExtensions
	{
		// Token: 0x06000CF0 RID: 3312 RVA: 0x00025692 File Offset: 0x00023892
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

		// Token: 0x06000CF1 RID: 3313 RVA: 0x000256A2 File Offset: 0x000238A2
		internal static IEnumerable<T> ValuesOfType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			return from ev in elements.OfResultType<T>()
				select ev.Value;
		}

		// Token: 0x06000CF2 RID: 3314 RVA: 0x000256CE File Offset: 0x000238CE
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

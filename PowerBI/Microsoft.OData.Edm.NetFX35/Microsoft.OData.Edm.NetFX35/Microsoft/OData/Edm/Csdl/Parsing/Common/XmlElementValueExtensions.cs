using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Csdl.Parsing.Common
{
	// Token: 0x02000198 RID: 408
	internal static class XmlElementValueExtensions
	{
		// Token: 0x060007E5 RID: 2021 RVA: 0x000137E8 File Offset: 0x000119E8
		internal static IEnumerable<XmlElementValue<T>> OfResultType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			foreach (XmlElementValue element in elements)
			{
				XmlElementValue<T> result = element as XmlElementValue<T>;
				if (result != null)
				{
					yield return result;
				}
				else if (element.UntypedValue is T)
				{
					yield return new XmlElementValue<T>(element.Name, element.Location, element.ValueAs<T>());
				}
			}
			yield break;
		}

		// Token: 0x060007E6 RID: 2022 RVA: 0x0001380D File Offset: 0x00011A0D
		internal static IEnumerable<T> ValuesOfType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			return Enumerable.Select<XmlElementValue<T>, T>(elements.OfResultType<T>(), (XmlElementValue<T> ev) => ev.Value);
		}

		// Token: 0x060007E7 RID: 2023 RVA: 0x000139C8 File Offset: 0x00011BC8
		internal static IEnumerable<XmlTextValue> OfText(this IEnumerable<XmlElementValue> elements)
		{
			foreach (XmlElementValue element in elements)
			{
				if (element.IsText)
				{
					yield return (XmlTextValue)element;
				}
			}
			yield break;
		}
	}
}

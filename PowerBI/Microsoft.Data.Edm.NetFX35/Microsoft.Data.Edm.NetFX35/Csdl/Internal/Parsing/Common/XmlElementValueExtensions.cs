using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Common
{
	// Token: 0x02000157 RID: 343
	internal static class XmlElementValueExtensions
	{
		// Token: 0x060006B8 RID: 1720 RVA: 0x00011414 File Offset: 0x0000F614
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

		// Token: 0x060006B9 RID: 1721 RVA: 0x00011439 File Offset: 0x0000F639
		internal static IEnumerable<T> ValuesOfType<T>(this IEnumerable<XmlElementValue> elements) where T : class
		{
			return Enumerable.Select<XmlElementValue<T>, T>(elements.OfResultType<T>(), (XmlElementValue<T> ev) => ev.Value);
		}

		// Token: 0x060006BA RID: 1722 RVA: 0x000115F4 File Offset: 0x0000F7F4
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

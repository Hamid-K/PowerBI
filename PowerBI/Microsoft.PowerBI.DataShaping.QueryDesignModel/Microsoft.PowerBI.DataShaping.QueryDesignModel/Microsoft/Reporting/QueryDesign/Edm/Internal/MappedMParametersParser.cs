using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Microsoft.InfoNav;

namespace Microsoft.Reporting.QueryDesign.Edm.Internal
{
	// Token: 0x0200021E RID: 542
	internal static class MappedMParametersParser
	{
		// Token: 0x0600190B RID: 6411 RVA: 0x000444FC File Offset: 0x000426FC
		public static Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> Load(XElement mappedMParametersElement)
		{
			Dictionary<string, Dictionary<string, List<ConceptualMParameter>>> dictionary = new Dictionary<string, Dictionary<string, List<ConceptualMParameter>>>(EdmItem.IdentityComparer);
			foreach (XElement xelement in (from e in mappedMParametersElement.Elements()
				where e.Name.Equals(Extensions.MParameterElem)
				select e).ToList<XElement>())
			{
				string stringAttributeOrDefault = xelement.GetStringAttributeOrDefault(Extensions.NameAttr, null);
				if (!string.IsNullOrWhiteSpace(stringAttributeOrDefault))
				{
					string stringAttributeOrDefault2 = xelement.GetStringAttributeOrDefault(Extensions.ActualTypeAttr, null);
					XElement xelement2 = xelement.Element(Extensions.ParameterValuesColumnElem);
					if (xelement2 != null)
					{
						XElement xelement3 = xelement2.Element(Extensions.EntityRefElem);
						XElement xelement4 = xelement2.Element(Extensions.PropertyRefElem);
						if (xelement3 != null && xelement4 != null)
						{
							string stringAttributeOrDefault3 = xelement3.GetStringAttributeOrDefault(Extensions.NameAttr, null);
							string stringAttributeOrDefault4 = xelement4.GetStringAttributeOrDefault(Extensions.NameAttr, null);
							if (!string.IsNullOrWhiteSpace(stringAttributeOrDefault3) && !string.IsNullOrWhiteSpace(stringAttributeOrDefault4))
							{
								Dictionary<string, List<ConceptualMParameter>> dictionary2;
								if (!dictionary.TryGetValue(stringAttributeOrDefault3, out dictionary2))
								{
									dictionary2 = new Dictionary<string, List<ConceptualMParameter>>(EdmItem.IdentityComparer);
									dictionary.Add(stringAttributeOrDefault3, dictionary2);
								}
								List<ConceptualMParameter> list;
								if (!dictionary2.TryGetValue(stringAttributeOrDefault4, out list))
								{
									list = new List<ConceptualMParameter>();
									dictionary2.Add(stringAttributeOrDefault4, list);
								}
								list.Add(new ConceptualMParameter(stringAttributeOrDefault, stringAttributeOrDefault2));
							}
						}
					}
				}
			}
			return dictionary;
		}
	}
}

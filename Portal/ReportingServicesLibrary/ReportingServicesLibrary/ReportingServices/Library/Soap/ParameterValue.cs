using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library.Soap
{
	// Token: 0x02000322 RID: 802
	public class ParameterValue : ParameterValueOrFieldReference
	{
		// Token: 0x06001B4E RID: 6990 RVA: 0x0006F4F8 File Offset: 0x0006D6F8
		internal static NameValueCollection ThisArrayToNameValueCollection(ParameterValue[] parameters)
		{
			if (parameters == null)
			{
				return null;
			}
			NameValueCollection nameValueCollection = new NameValueCollection(parameters.Length, StringComparer.Ordinal);
			for (int i = 0; i < parameters.Length; i++)
			{
				if (parameters[i] != null)
				{
					string name = parameters[i].Name;
					if (name == null)
					{
						throw new MissingElementException("Name");
					}
					nameValueCollection.Add(name, parameters[i].Value);
				}
			}
			return nameValueCollection;
		}

		// Token: 0x06001B4F RID: 6991 RVA: 0x0006F554 File Offset: 0x0006D754
		internal static void WriteThisToXml(ParameterValue parameter, XmlTextWriter xml)
		{
			xml.WriteStartElement("ParameterValue");
			if (parameter.Name != null)
			{
				xml.WriteElementString("Name", parameter.Name);
			}
			if (parameter.Value != null)
			{
				xml.WriteElementString("Value", parameter.Value);
			}
			xml.WriteEndElement();
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x0006F5A4 File Offset: 0x0006D7A4
		internal static ParameterValue[] XmlToThisArray(string xml)
		{
			return (ParameterValue[])ParameterValueOrFieldReference.XmlToThisArray(xml, true);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x0006F5B4 File Offset: 0x0006D7B4
		internal static ParameterValue[] ParameterInfoCollectionToThisArray(ParameterInfoCollection collection)
		{
			if (collection == null)
			{
				return null;
			}
			ParameterValue[] array = new ParameterValue[collection.Count];
			int num = 0;
			foreach (object obj in collection)
			{
				ParameterInfo parameterInfo = (ParameterInfo)obj;
				ParameterValue parameterValue = new ParameterValue();
				parameterValue.Name = parameterInfo.Name;
				parameterValue.Value = null;
				parameterValue.Label = null;
				if (parameterInfo.Values != null && parameterInfo.Values.Length != 0)
				{
					parameterValue.Value = parameterInfo.CastToString(parameterInfo.Values[0], Localization.ClientPrimaryCulture);
				}
				if (parameterInfo.Labels != null && parameterInfo.Labels.Length != 0)
				{
					parameterValue.Label = parameterInfo.Labels[0];
				}
				array[num] = parameterValue;
				num++;
			}
			return array;
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x0006F698 File Offset: 0x0006D898
		internal static ParameterValue[] NameValueCollectionToThisArray(NameValueCollection collection)
		{
			if (collection == null)
			{
				return null;
			}
			List<ParameterValue> list = new List<ParameterValue>(collection.Count);
			foreach (string text in collection.AllKeys)
			{
				string[] values = collection.GetValues(text);
				if (values != null)
				{
					foreach (string text2 in values)
					{
						list.Add(new ParameterValue
						{
							Name = text,
							Value = text2,
							Label = null
						});
					}
				}
				else
				{
					list.Add(new ParameterValue
					{
						Name = text,
						Value = null,
						Label = null
					});
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000ADF RID: 2783
		public string Name;

		// Token: 0x04000AE0 RID: 2784
		public string Value;

		// Token: 0x04000AE1 RID: 2785
		public string Label;
	}
}

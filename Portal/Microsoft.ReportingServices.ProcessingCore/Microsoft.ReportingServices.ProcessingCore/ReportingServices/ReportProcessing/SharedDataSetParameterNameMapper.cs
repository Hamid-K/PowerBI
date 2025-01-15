using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000659 RID: 1625
	internal sealed class SharedDataSetParameterNameMapper
	{
		// Token: 0x06005A68 RID: 23144 RVA: 0x0017288C File Offset: 0x00170A8C
		internal static void MakeUnique(List<ParameterValue> queryParameters)
		{
			if (queryParameters == null)
			{
				return;
			}
			Dictionary<string, bool> dictionary = new Dictionary<string, bool>(StringComparer.Ordinal);
			int count = queryParameters.Count;
			for (int i = 0; i < count; i++)
			{
				string text = queryParameters[i].UniqueName;
				if (text == null)
				{
					text = queryParameters[i].Name;
				}
				bool flag;
				if (!dictionary.TryGetValue(text, out flag))
				{
					dictionary.Add(text, false);
				}
				else
				{
					if (!flag)
					{
						dictionary[text] = true;
					}
					text = SharedDataSetParameterNameMapper.MakeNameUnique(dictionary, i + 1, text);
					dictionary.Add(text, false);
					queryParameters[i].UniqueName = text;
				}
			}
			for (int j = 0; j < count; j++)
			{
				string text2 = queryParameters[j].UniqueName;
				if (text2 == null)
				{
					text2 = queryParameters[j].Name;
				}
				if (dictionary[text2])
				{
					string text3 = SharedDataSetParameterNameMapper.MakeNameUnique(dictionary, j + 1, text2);
					dictionary.Remove(text2);
					dictionary.Add(text3, false);
					queryParameters[j].UniqueName = text3;
				}
			}
		}

		// Token: 0x06005A69 RID: 23145 RVA: 0x00172984 File Offset: 0x00170B84
		private static string MakeNameUnique(Dictionary<string, bool> parameterNameDuplicates, int position, string uniqueName)
		{
			uniqueName = SharedDataSetParameterNameMapper.AppendPosition(uniqueName, position);
			while (parameterNameDuplicates.ContainsKey(uniqueName))
			{
				uniqueName = SharedDataSetParameterNameMapper.AppendPosition(uniqueName, position);
			}
			return uniqueName;
		}

		// Token: 0x06005A6A RID: 23146 RVA: 0x001729A4 File Offset: 0x00170BA4
		private static string AppendPosition(string originalName, int position)
		{
			return originalName + "_" + position.ToString(CultureInfo.InvariantCulture);
		}
	}
}

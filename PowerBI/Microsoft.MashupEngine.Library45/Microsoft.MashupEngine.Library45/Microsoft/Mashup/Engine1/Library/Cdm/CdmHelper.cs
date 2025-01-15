using System;
using System.Collections.Generic;
using Microsoft.Mashup.Cdm;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdm
{
	// Token: 0x02000010 RID: 16
	public static class CdmHelper
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003510 File Offset: 0x00001710
		public static BinaryValue GetBinaryFileContent(TableValue table, string filePath)
		{
			Uri uri;
			string text;
			if (!Utils.TryParsePath(filePath, ref uri, ref text))
			{
				throw ValueException.NewDataSourceError<Message1>(Resources.Cdm_InvalidFileName(filePath), TextValue.New(filePath), null);
			}
			string text2 = Utils.ConvertToMashupUri(uri);
			text = Uri.UnescapeDataString(text);
			bool flag = false;
			BinaryValue binaryValue = null;
			using (IEnumerator<IValueReference> enumerator = table.GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					string asString = enumerator.Current.Value.AsRecord["Folder Path"].AsString;
					if (text2.StartsWith(asString, StringComparison.Ordinal))
					{
						int num = text2.IndexOf(asString, StringComparison.Ordinal);
						if (num != -1)
						{
							string text3 = text2.Substring(num + asString.Length).Trim(new char[] { Utils.GetSeparator(uri) });
							text3 = Uri.UnescapeDataString(text3);
							flag = CdmHelper.TryGetBinaryFileContent(string.IsNullOrEmpty(text3) ? EmptyArray<string>.Instance : text3.Split(new char[] { Utils.GetSeparator(uri) }), 0, text, table, out binaryValue);
						}
					}
				}
			}
			if (flag && binaryValue != null)
			{
				return binaryValue;
			}
			throw ValueException.NewDataSourceError<Message1>(Resources.Cdm_FileNotFound(filePath), TextValue.New(filePath), null);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x0000364C File Offset: 0x0000184C
		public static List<string> FetchAllFiles(TableValue table, string folderPath)
		{
			List<string> list = new List<string>();
			TableValue tableValue = table;
			Uri uri;
			if (Uri.TryCreate(folderPath, UriKind.Absolute, out uri))
			{
				folderPath = Utils.ConvertToMashupUri(uri);
				Value[] array = table["Folder Path"].AsList.ToArray();
				if (array.Length != 0)
				{
					string asString = array[0].AsString;
					int num = folderPath.IndexOf(asString, StringComparison.Ordinal);
					if (num != -1)
					{
						string text = folderPath.Substring(num + asString.Length).Trim(new char[] { Utils.GetSeparator(uri) });
						text = Uri.UnescapeDataString(text);
						string[] array2 = (string.IsNullOrEmpty(text) ? new string[0] : text.Split(new char[] { Utils.GetSeparator(uri) }));
						for (int i = 0; i < array2.Length; i++)
						{
							Value value;
							if (!tableValue.TryGetValue(RecordValue.New(new NamedValue[]
							{
								new NamedValue("Name", TextValue.New(array2[i]))
							}), out value))
							{
								return list;
							}
							tableValue = value["Content"] as TableValue;
						}
						CdmHelper.TryFetchAllFiles(tableValue, list, uri);
					}
				}
			}
			return list;
		}

		// Token: 0x06000056 RID: 86 RVA: 0x0000376C File Offset: 0x0000196C
		private static bool TryGetBinaryFileContent(string[] folders, int index, string fileName, TableValue table, out BinaryValue binary)
		{
			binary = null;
			if (folders.Length != 0 && index != folders.Length)
			{
				Value value;
				return table.TryGetValue(RecordValue.New(new NamedValue[]
				{
					new NamedValue("Name", TextValue.New(folders[index]))
				}), out value) && CdmHelper.TryGetBinaryFileContent(folders, index + 1, fileName, value["Content"] as TableValue, out binary);
			}
			Value value2;
			if (table.TryGetValue(RecordValue.New(new NamedValue[]
			{
				new NamedValue("Name", TextValue.New(fileName))
			}), out value2))
			{
				binary = value2["Content"] as BinaryValue;
				return true;
			}
			return false;
		}

		// Token: 0x06000057 RID: 87 RVA: 0x0000381C File Offset: 0x00001A1C
		private static void TryFetchAllFiles(TableValue table, List<string> result, Uri folderPathUri)
		{
			foreach (IValueReference valueReference in table)
			{
				Value value = valueReference.Value.AsRecord["Content"];
				Value value2 = valueReference.Value.AsRecord["Folder Path"];
				Value value3 = valueReference.Value.AsRecord["Name"];
				string text;
				if (Utils.TryConvertToMashupUri(value2.AsString, ref text))
				{
					string asString = value3.AsString;
					if (value.IsTable)
					{
						CdmHelper.TryFetchAllFiles(value.AsTable, result, folderPathUri);
					}
					else if (value.IsBinary)
					{
						string text2 = text + asString;
						if (folderPathUri.IsFile)
						{
							text2 = text2.Replace(Utils.FileSeparator, Utils.UrlSeparator);
						}
						result.Add(text2);
					}
				}
			}
		}
	}
}

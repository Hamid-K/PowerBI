using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Evaluator.Interface;

namespace Microsoft.Mashup.Evaluator
{
	// Token: 0x02001CF1 RID: 7409
	internal static class MemberLetPartitionKeySerializer
	{
		// Token: 0x0600B8FF RID: 47359 RVA: 0x00257EDC File Offset: 0x002560DC
		public static string Serialize(IMemberLetPartitionKey partitionKey)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(MemberLetPartitionKeySerializer.Escape(partitionKey.Section));
			stringBuilder.Append("/");
			stringBuilder.Append(MemberLetPartitionKeySerializer.Escape(partitionKey.Member));
			foreach (string text in partitionKey.Lets)
			{
				stringBuilder.Append("/");
				stringBuilder.Append(MemberLetPartitionKeySerializer.Escape(text));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600B900 RID: 47360 RVA: 0x00257F78 File Offset: 0x00256178
		public static IMemberLetPartitionKey Deserialize(string serializedString)
		{
			string text = null;
			string text2 = null;
			List<string> list = new List<string>();
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < serializedString.Length; i++)
			{
				char c = serializedString[i];
				if (c != '/')
				{
					if (c == '\\')
					{
						i++;
						stringBuilder.Append(serializedString[i]);
					}
					else
					{
						stringBuilder.Append(serializedString[i]);
					}
				}
				else
				{
					string text3 = stringBuilder.ToString();
					if (text == null)
					{
						text = text3;
					}
					else if (text2 == null)
					{
						text2 = text3;
					}
					else
					{
						list.Add(text3);
					}
					stringBuilder = new StringBuilder();
				}
			}
			string text4 = stringBuilder.ToString();
			if (text == null)
			{
				text = text4;
			}
			else if (text2 == null)
			{
				text2 = text4;
			}
			else
			{
				list.Add(text4);
			}
			if (text == null || text2 == null)
			{
				throw new ArgumentNullException();
			}
			return new MemberLetPartitionKey(text, text2, (list == null) ? EmptyArray<string>.Instance : list.ToArray());
		}

		// Token: 0x0600B901 RID: 47361 RVA: 0x00258051 File Offset: 0x00256251
		private static string Escape(string name)
		{
			return name.Replace("\\", "\\\\").Replace("/", "\\/");
		}
	}
}

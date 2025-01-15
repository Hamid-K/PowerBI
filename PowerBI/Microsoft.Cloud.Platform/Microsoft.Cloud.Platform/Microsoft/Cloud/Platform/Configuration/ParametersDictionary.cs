using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Xml;
using JetBrains.Annotations;
using Microsoft.Cloud.Platform.Common;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Configuration
{
	// Token: 0x02000434 RID: 1076
	[CannotApplyEqualityOperator]
	[Serializable]
	public sealed class ParametersDictionary : ConfigurationDictionary<string, string>
	{
		// Token: 0x06002125 RID: 8485 RVA: 0x0007C982 File Offset: 0x0007AB82
		public ParametersDictionary()
		{
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0007C995 File Offset: 0x0007AB95
		public ParametersDictionary(Dictionary<string, string> other)
			: base(other)
		{
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0007C9A9 File Offset: 0x0007ABA9
		public ParametersDictionary(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0007C9C0 File Offset: 0x0007ABC0
		public static ParametersDictionary Combine(ParametersDictionary first, ParametersDictionary second)
		{
			ParametersDictionary parametersDictionary = new ParametersDictionary();
			if (first != null)
			{
				parametersDictionary.AddRange(first);
				parametersDictionary.m_usageDictionary.AddRange(first.m_usageDictionary);
			}
			if (second != null)
			{
				parametersDictionary.AddRange(second);
				parametersDictionary.m_usageDictionary.AddRange(second.m_usageDictionary);
			}
			return parametersDictionary;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0007CA0C File Offset: 0x0007AC0C
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			ParametersDictionary parametersDictionary = (ParametersDictionary)obj;
			return this.Equivalent(parametersDictionary);
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0007CA41 File Offset: 0x0007AC41
		public override int GetHashCode()
		{
			return this.Aggregate(base.GetType().Name.GetHashCode(), (int h, KeyValuePair<string, string> kvp) => h ^ kvp.GetHashCode() ^ ((kvp.Value == null) ? 0 : kvp.GetHashCode()));
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0007CA78 File Offset: 0x0007AC78
		public override void ReadXml(XmlReader reader)
		{
			if (reader.IsEmptyElement)
			{
				reader.Read();
				return;
			}
			string name = reader.Name;
			reader.ReadStartElement();
			for (;;)
			{
				if (reader.IsEmptyElement)
				{
					reader.Skip();
				}
				else
				{
					if (reader.Name.Equals(name) && reader.NodeType == XmlNodeType.EndElement)
					{
						break;
					}
					string name2 = reader.Name;
					string text = reader.ReadInnerXml();
					text = this.ReplaceTokens(text);
					base.Add(name2, text);
					this.m_usageDictionary.Add(name2, false);
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0007CB00 File Offset: 0x0007AD00
		public override void WriteXml(XmlWriter writer)
		{
			foreach (KeyValuePair<string, string> keyValuePair in this)
			{
				ConfigurationClass.WriteRawStringProperty(keyValuePair.Key, keyValuePair.Value, writer);
			}
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0007CB5C File Offset: 0x0007AD5C
		public string ReplaceTokens(string value)
		{
			value = ParametersDictionary.c_elementWithTokenRegex.Replace(value, delegate(Match match)
			{
				string value2 = match.Groups[2].Value;
				string text;
				if (!base.TryGetValue(value2, out text))
				{
					throw new ManifestUnmappedParameterException(value2, value2);
				}
				this.m_usageDictionary[value2] = true;
				if (!string.IsNullOrEmpty(text))
				{
					return match.Groups[1].Value + text + match.Groups[3].Value;
				}
				return string.Empty;
			});
			return ParametersDictionary.c_tokenRegex.Replace(value, delegate(Match match)
			{
				string value3 = match.Groups[1].Value;
				string text2;
				if (base.TryGetValue(value3, out text2))
				{
					this.m_usageDictionary[value3] = true;
					return text2;
				}
				throw new ManifestUnmappedParameterException(value3, value3);
			});
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0007CB90 File Offset: 0x0007AD90
		public List<string> UnusedParameters
		{
			get
			{
				return (from kvp in this.m_usageDictionary
					where !kvp.Value
					select kvp.Key).ToList<string>();
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0007CBF0 File Offset: 0x0007ADF0
		public static Regex ElementWithTokenRegex
		{
			get
			{
				return ParametersDictionary.c_elementWithTokenRegex;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06002130 RID: 8496 RVA: 0x0007CBF7 File Offset: 0x0007ADF7
		public static Regex TokenRegex
		{
			get
			{
				return ParametersDictionary.c_tokenRegex;
			}
		}

		// Token: 0x04000B59 RID: 2905
		private static Regex c_tokenRegex = new Regex("#{([^}]+)}", RegexOptions.Compiled);

		// Token: 0x04000B5A RID: 2906
		private static Regex c_elementWithTokenRegex = new Regex("(<[^/>]+>)#{([^}]+)}(</[^>]+>)", RegexOptions.Compiled);

		// Token: 0x04000B5B RID: 2907
		private Dictionary<string, bool> m_usageDictionary = new Dictionary<string, bool>();
	}
}

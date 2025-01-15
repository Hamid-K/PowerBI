using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.ProgramSynthesis.Extraction.Web.Translation.CSS
{
	// Token: 0x02001191 RID: 4497
	public class NodeBasedCssSelector
	{
		// Token: 0x060085D9 RID: 34265 RVA: 0x001C1BC3 File Offset: 0x001BFDC3
		public NodeBasedCssSelector()
		{
			this.TagName = "";
			this._tagNames = new List<string>();
			this._classNames = new List<string>();
			this._attributeValues = new List<NodeBasedCssSelector.AttributeValueType>();
			this._extras = new List<string>();
		}

		// Token: 0x170016FC RID: 5884
		// (get) Token: 0x060085DA RID: 34266 RVA: 0x001C1C02 File Offset: 0x001BFE02
		// (set) Token: 0x060085DB RID: 34267 RVA: 0x001C1C0A File Offset: 0x001BFE0A
		public string TagName { get; private set; }

		// Token: 0x170016FD RID: 5885
		// (get) Token: 0x060085DC RID: 34268 RVA: 0x001C1C13 File Offset: 0x001BFE13
		public IReadOnlyList<string> TagNames
		{
			get
			{
				return this._tagNames;
			}
		}

		// Token: 0x170016FE RID: 5886
		// (get) Token: 0x060085DD RID: 34269 RVA: 0x001C1C1B File Offset: 0x001BFE1B
		public IReadOnlyList<string> ClassNames
		{
			get
			{
				return this._classNames;
			}
		}

		// Token: 0x170016FF RID: 5887
		// (get) Token: 0x060085DE RID: 34270 RVA: 0x001C1C23 File Offset: 0x001BFE23
		public IReadOnlyList<NodeBasedCssSelector.AttributeValueType> AttributeValues
		{
			get
			{
				return this._attributeValues;
			}
		}

		// Token: 0x17001700 RID: 5888
		// (get) Token: 0x060085DF RID: 34271 RVA: 0x001C1C2B File Offset: 0x001BFE2B
		public IReadOnlyList<string> Extras
		{
			get
			{
				return this._extras;
			}
		}

		// Token: 0x060085E0 RID: 34272 RVA: 0x001C1C33 File Offset: 0x001BFE33
		public void AddTagName(string tagName)
		{
			if (this.TagName != "")
			{
				throw new InvalidOperationException("Tagname was already set once, it can not be set again.");
			}
			this.TagName = tagName;
		}

		// Token: 0x060085E1 RID: 34273 RVA: 0x001C1C59 File Offset: 0x001BFE59
		public void AddTagNames(string[] tagNames)
		{
			this._tagNames = tagNames.ToList<string>();
		}

		// Token: 0x060085E2 RID: 34274 RVA: 0x001C1C67 File Offset: 0x001BFE67
		public void AddClass(string className)
		{
			this._classNames.Add(className);
		}

		// Token: 0x060085E3 RID: 34275 RVA: 0x001C1C75 File Offset: 0x001BFE75
		public void AddAttributeValue(string attr, string val, string op = "=")
		{
			this._attributeValues.Add(new NodeBasedCssSelector.AttributeValueType(attr, op, val));
		}

		// Token: 0x060085E4 RID: 34276 RVA: 0x001C1C8A File Offset: 0x001BFE8A
		public void AddExtra(string extra)
		{
			this._extras.Add(extra);
		}

		// Token: 0x060085E5 RID: 34277 RVA: 0x001C1C98 File Offset: 0x001BFE98
		private static string EscapeSpecialCharacters(string className)
		{
			return string.Concat(className.Select((char c) => (NodeBasedCssSelector.Specials.Contains(c) ? "\\" : string.Empty) + c.ToString())).Replace("--", "\\-\\-");
		}

		// Token: 0x060085E6 RID: 34278 RVA: 0x001C1CD3 File Offset: 0x001BFED3
		public IEnumerable<string> ToStringCases()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in this._classNames)
			{
				stringBuilder.Append('.');
				stringBuilder.Append(NodeBasedCssSelector.EscapeSpecialCharacters(text));
			}
			foreach (NodeBasedCssSelector.AttributeValueType attributeValueType in this._attributeValues)
			{
				string text2 = NodeBasedCssSelector.EscapeSpecialCharacters(attributeValueType.Attribute);
				string text3 = NodeBasedCssSelector.EscapeSpecialCharacters(attributeValueType.AttributeValue);
				stringBuilder.AppendFormat("[{0}{1}\"{2}\"]", text2, attributeValueType.Operator, text3);
			}
			stringBuilder.Append(string.Join(string.Empty, this._extras));
			if (!string.IsNullOrEmpty(this.TagName))
			{
				stringBuilder.Insert(0, this.TagName);
				yield return stringBuilder.ToString();
			}
			else if (this._tagNames.Count == 0)
			{
				if (stringBuilder.Length == 0)
				{
					yield return "*";
				}
				else
				{
					yield return stringBuilder.ToString();
				}
			}
			else
			{
				string baseString = stringBuilder.ToString();
				foreach (string text4 in this._tagNames)
				{
					yield return text4 + baseString;
				}
				List<string>.Enumerator enumerator3 = default(List<string>.Enumerator);
				baseString = null;
			}
			yield break;
			yield break;
		}

		// Token: 0x060085E7 RID: 34279 RVA: 0x001C1CE4 File Offset: 0x001BFEE4
		public override string ToString()
		{
			IEnumerable<string> enumerable = this.ToStringCases();
			return string.Join(",", enumerable);
		}

		// Token: 0x0400372F RID: 14127
		private static readonly HashSet<char> Specials = new HashSet<char>("!#$%&'()*+,./:;<=>?@[]^`{|}\"\\");

		// Token: 0x04003730 RID: 14128
		private readonly List<NodeBasedCssSelector.AttributeValueType> _attributeValues;

		// Token: 0x04003731 RID: 14129
		private readonly List<string> _classNames;

		// Token: 0x04003732 RID: 14130
		private readonly List<string> _extras;

		// Token: 0x04003733 RID: 14131
		private List<string> _tagNames;

		// Token: 0x02001192 RID: 4498
		public struct AttributeValueType
		{
			// Token: 0x060085E9 RID: 34281 RVA: 0x001C1D14 File Offset: 0x001BFF14
			internal AttributeValueType(string attribute, string operatorStr, string attributeValue)
			{
				this.Attribute = attribute;
				this.Operator = operatorStr;
				this.AttributeValue = attributeValue;
			}

			// Token: 0x04003735 RID: 14133
			internal string Attribute;

			// Token: 0x04003736 RID: 14134
			internal string AttributeValue;

			// Token: 0x04003737 RID: 14135
			internal string Operator;
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Plugins
{
	// Token: 0x02000021 RID: 33
	[DisplayName("Rtl")]
	[Description("Reverses some css when in rtl mode")]
	public class RtlPlugin : VisitorPlugin
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00004017 File Offset: 0x00002217
		public RtlPlugin(bool onlyReversePrefixedRules, bool forceRtlTransform)
			: this()
		{
			this.OnlyReversePrefixedRules = onlyReversePrefixedRules;
			this.ForceRtlTransform = forceRtlTransform;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00004030 File Offset: 0x00002230
		public RtlPlugin()
		{
			this.PropertiesToReverse = new List<string>
			{
				"border-left", "border-right", "border-top-left-radius", "border-top-right-radius", "border-bottom-left-radius", "border-bottom-right-radius", "border-width", "margin", "padding", "float",
				"right", "left", "text-align"
			};
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x000040DD File Offset: 0x000022DD
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x000040E5 File Offset: 0x000022E5
		public bool OnlyReversePrefixedRules { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x000040EE File Offset: 0x000022EE
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x000040F6 File Offset: 0x000022F6
		public bool ForceRtlTransform { get; set; }

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x000040FF File Offset: 0x000022FF
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00004107 File Offset: 0x00002307
		public IEnumerable<string> PropertiesToReverse { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00004110 File Offset: 0x00002310
		public override VisitorPluginType AppliesTo
		{
			get
			{
				return VisitorPluginType.AfterEvaluation;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004114 File Offset: 0x00002314
		public override void OnPreVisiting(Env env)
		{
			base.OnPreVisiting(env);
			bool flag = this.ForceRtlTransform || CultureInfo.CurrentCulture.TextInfo.IsRightToLeft;
			this.PrefixesToProcess = new List<RtlPlugin.Prefix>();
			if (!this.OnlyReversePrefixedRules && flag)
			{
				foreach (string text in this.PropertiesToReverse)
				{
					this.PrefixesToProcess.Add(new RtlPlugin.Prefix
					{
						KeepRule = true,
						PrefixString = text,
						RemovePrefix = false,
						Reverse = true
					});
				}
			}
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-rtl-reverse-",
				RemovePrefix = true,
				KeepRule = true,
				Reverse = flag
			});
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-ltr-reverse-",
				RemovePrefix = true,
				KeepRule = true,
				Reverse = !flag
			});
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-rtl-ltr-",
				RemovePrefix = true,
				KeepRule = true,
				Reverse = false
			});
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-ltr-rtl-",
				RemovePrefix = true,
				KeepRule = true,
				Reverse = false
			});
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-rtl-",
				RemovePrefix = true,
				KeepRule = flag
			});
			this.PrefixesToProcess.Add(new RtlPlugin.Prefix
			{
				PrefixString = "-ltr-",
				RemovePrefix = true,
				KeepRule = !flag
			});
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x000042D8 File Offset: 0x000024D8
		public override Node Execute(Node node, out bool visitDeeper)
		{
			Rule rule = node as Rule;
			if (rule != null)
			{
				visitDeeper = false;
				string text = (rule.Name ?? "").ToLowerInvariant();
				foreach (RtlPlugin.Prefix prefix in this.PrefixesToProcess)
				{
					if (text.StartsWith(prefix.PrefixString))
					{
						if (!prefix.KeepRule)
						{
							return null;
						}
						if (prefix.RemovePrefix)
						{
							rule.Name = rule.Name.Substring(prefix.PrefixString.Length);
						}
						if (!prefix.Reverse)
						{
							return rule;
						}
						if (rule.Name.IndexOf("right", StringComparison.InvariantCultureIgnoreCase) >= 0)
						{
							rule.Name = this.Replace(rule.Name, "right", "left", StringComparison.InvariantCultureIgnoreCase);
							return rule;
						}
						if (rule.Name.IndexOf("left", StringComparison.InvariantCultureIgnoreCase) >= 0)
						{
							rule.Name = this.Replace(rule.Name, "left", "right", StringComparison.InvariantCultureIgnoreCase);
							return rule;
						}
						if (rule.Name.IndexOf("top", StringComparison.InvariantCultureIgnoreCase) >= 0 || rule.Name.IndexOf("bottom", StringComparison.InvariantCultureIgnoreCase) >= 0)
						{
							return rule;
						}
						return new RtlPlugin.ValuesReverserVisitor().ReverseRule(rule);
					}
				}
			}
			visitDeeper = true;
			return node;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00004464 File Offset: 0x00002664
		private string Replace(string haystack, string needle, string replacement, StringComparison comparisonType)
		{
			int num = haystack.IndexOf(needle, comparisonType);
			if (num < 0)
			{
				return haystack;
			}
			return haystack.Substring(0, num) + replacement + haystack.Substring(num + needle.Length);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000CA RID: 202 RVA: 0x0000449D File Offset: 0x0000269D
		// (set) Token: 0x060000CB RID: 203 RVA: 0x000044A5 File Offset: 0x000026A5
		private List<RtlPlugin.Prefix> PrefixesToProcess { get; set; }

		// Token: 0x020000D9 RID: 217
		private class Prefix
		{
			// Token: 0x1700010E RID: 270
			// (get) Token: 0x060005FA RID: 1530 RVA: 0x00018A37 File Offset: 0x00016C37
			// (set) Token: 0x060005FB RID: 1531 RVA: 0x00018A3F File Offset: 0x00016C3F
			public string PrefixString { get; set; }

			// Token: 0x1700010F RID: 271
			// (get) Token: 0x060005FC RID: 1532 RVA: 0x00018A48 File Offset: 0x00016C48
			// (set) Token: 0x060005FD RID: 1533 RVA: 0x00018A50 File Offset: 0x00016C50
			public bool KeepRule { get; set; }

			// Token: 0x17000110 RID: 272
			// (get) Token: 0x060005FE RID: 1534 RVA: 0x00018A59 File Offset: 0x00016C59
			// (set) Token: 0x060005FF RID: 1535 RVA: 0x00018A61 File Offset: 0x00016C61
			public bool Reverse { get; set; }

			// Token: 0x17000111 RID: 273
			// (get) Token: 0x06000600 RID: 1536 RVA: 0x00018A6A File Offset: 0x00016C6A
			// (set) Token: 0x06000601 RID: 1537 RVA: 0x00018A72 File Offset: 0x00016C72
			public bool RemovePrefix { get; set; }
		}

		// Token: 0x020000DA RID: 218
		private class ValuesReverserVisitor : IVisitor
		{
			// Token: 0x06000603 RID: 1539 RVA: 0x00018A84 File Offset: 0x00016C84
			public Rule ReverseRule(Rule rule)
			{
				rule.Accept(this);
				string text = this._textContent.ToString();
				string text2 = "";
				Value value = rule.Value as Value;
				if (value != null)
				{
					text2 = value.Important;
				}
				bool flag = false;
				if (this._nodeContent.Count > 1)
				{
					if (this._nodeContent.Count == 4)
					{
						Node node = this._nodeContent[1];
						this._nodeContent[1] = this._nodeContent[3];
						this._nodeContent[3] = node;
						return new Rule(rule.Name, new Value(new Expression[]
						{
							new Expression(this._nodeContent)
						}, text2)).ReducedFrom<Rule>(new Node[] { rule });
					}
				}
				else
				{
					if (text == "left")
					{
						text = ("right " + text2).TrimEnd(Array.Empty<char>());
						flag = true;
					}
					else if (text == "right")
					{
						text = ("left " + text2).TrimEnd(Array.Empty<char>());
						flag = true;
					}
					else
					{
						string[] array = text.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
						if (array.Length == 4)
						{
							string text3 = array[1];
							array[1] = array[3];
							array[3] = text3;
							text = string.Join(" ", array);
							flag = true;
						}
					}
					if (flag)
					{
						return new Rule(rule.Name, new TextNode(text)).ReducedFrom<Rule>(new Node[] { rule });
					}
				}
				return rule;
			}

			// Token: 0x06000604 RID: 1540 RVA: 0x00018BFC File Offset: 0x00016DFC
			public Node Visit(Node node)
			{
				TextNode textNode = node as TextNode;
				if (textNode != null)
				{
					this._textContent.Append(textNode.Value);
					this._nodeContent.Add(textNode);
					return node;
				}
				Number number = node as Number;
				if (number != null)
				{
					this._nodeContent.Add(number);
					return node;
				}
				Keyword keyword = node as Keyword;
				if (keyword != null)
				{
					this._nodeContent.Add(keyword);
					this._textContent.Append(keyword.Value);
					return node;
				}
				node.Accept(this);
				return node;
			}

			// Token: 0x0400016C RID: 364
			private StringBuilder _textContent = new StringBuilder();

			// Token: 0x0400016D RID: 365
			private List<Node> _nodeContent = new List<Node>();
		}
	}
}

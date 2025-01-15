using System;
using System.IO;
using AngleSharp.Dom.Collections;
using AngleSharp.Html;
using AngleSharp.Network;
using AngleSharp.Parser.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000202 RID: 514
	internal sealed class CssStyleSheet : CssNode, ICssStyleSheet, IStyleSheet, IStyleFormattable, ICssNode, ICssRuleCreator
	{
		// Token: 0x0600135C RID: 4956 RVA: 0x0004A565 File Offset: 0x00048765
		internal CssStyleSheet(CssParser parser, string url, IElement owner)
		{
			this._media = new MediaList(parser);
			this._owner = owner;
			this._url = url;
			this._rules = new CssRuleList(this);
			this._parser = parser;
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x0004A59A File Offset: 0x0004879A
		internal CssStyleSheet(CssParser parser, string url, ICssStyleSheet parent)
			: this(parser, url, (parent != null) ? parent.OwnerNode : null)
		{
			this._parent = parent;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x0004A5B7 File Offset: 0x000487B7
		internal CssStyleSheet(CssParser parser)
			: this(parser, null, null)
		{
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x0004A5C2 File Offset: 0x000487C2
		internal CssStyleSheet(CssRule ownerRule, string url)
			: this(ownerRule.Parser, url, ownerRule.Owner)
		{
			this._ownerRule = ownerRule;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001360 RID: 4960 RVA: 0x0004A441 File Offset: 0x00048641
		public string Type
		{
			get
			{
				return MimeTypeNames.Css;
			}
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0004A5DE File Offset: 0x000487DE
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0004A5E6 File Offset: 0x000487E6
		public bool IsDisabled { get; set; }

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001363 RID: 4963 RVA: 0x0004A5EF File Offset: 0x000487EF
		public IElement OwnerNode
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001364 RID: 4964 RVA: 0x0004A5F7 File Offset: 0x000487F7
		public ICssStyleSheet Parent
		{
			get
			{
				return this._parent;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001365 RID: 4965 RVA: 0x0004A5FF File Offset: 0x000487FF
		public string Href
		{
			get
			{
				return this._url;
			}
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004A607 File Offset: 0x00048807
		public string Title
		{
			get
			{
				IElement owner = this._owner;
				if (owner == null)
				{
					return null;
				}
				return owner.GetAttribute(AttributeNames.Title);
			}
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001367 RID: 4967 RVA: 0x0004A61F File Offset: 0x0004881F
		public IMediaList Media
		{
			get
			{
				return this._media;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001368 RID: 4968 RVA: 0x0004A627 File Offset: 0x00048827
		ICssRuleList ICssStyleSheet.Rules
		{
			get
			{
				return this.Rules;
			}
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06001369 RID: 4969 RVA: 0x0004A62F File Offset: 0x0004882F
		public ICssRule OwnerRule
		{
			get
			{
				return this._ownerRule;
			}
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x0600136A RID: 4970 RVA: 0x0004A637 File Offset: 0x00048837
		internal CssRuleList Rules
		{
			get
			{
				return this._rules;
			}
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x0004A640 File Offset: 0x00048840
		public ICssRule AddNewRule(CssRuleType ruleType)
		{
			CssRule cssRule = this._parser.CreateRule(ruleType);
			this.Rules.Add(cssRule);
			return cssRule;
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x0004A667 File Offset: 0x00048867
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			writer.Write(formatter.Sheet(this.Rules));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x0004A67B File Offset: 0x0004887B
		public void RemoveAt(int index)
		{
			this.Rules.RemoveAt(index);
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x0004A68C File Offset: 0x0004888C
		public int Insert(string ruleText, int index)
		{
			CssRule cssRule = this._parser.ParseRule(ruleText);
			cssRule.Owner = this;
			this.Rules.Insert(index, cssRule);
			return index;
		}

		// Token: 0x04000A9C RID: 2716
		private readonly MediaList _media;

		// Token: 0x04000A9D RID: 2717
		private readonly string _url;

		// Token: 0x04000A9E RID: 2718
		private readonly IElement _owner;

		// Token: 0x04000A9F RID: 2719
		private readonly ICssStyleSheet _parent;

		// Token: 0x04000AA0 RID: 2720
		private readonly CssRuleList _rules;

		// Token: 0x04000AA1 RID: 2721
		private readonly CssParser _parser;

		// Token: 0x04000AA2 RID: 2722
		private readonly ICssRule _ownerRule;
	}
}

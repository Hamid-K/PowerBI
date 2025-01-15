using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Html.LinkRels;
using AngleSharp.Network;
using AngleSharp.Network.RequestProcessors;
using AngleSharp.Services;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200036C RID: 876
	internal sealed class HtmlLinkElement : HtmlElement, IHtmlLinkElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, ILinkStyle, ILinkImport, ILoadableElement
	{
		// Token: 0x06001B4E RID: 6990 RVA: 0x00053135 File Offset: 0x00051335
		public HtmlLinkElement(Document owner, string prefix = null)
			: base(owner, TagNames.Link, prefix, NodeFlags.SelfClosing | NodeFlags.Special)
		{
		}

		// Token: 0x170007BC RID: 1980
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x00053145 File Offset: 0x00051345
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0005314D File Offset: 0x0005134D
		internal bool IsVisited { get; set; }

		// Token: 0x170007BD RID: 1981
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x00053156 File Offset: 0x00051356
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0005315E File Offset: 0x0005135E
		internal bool IsActive { get; set; }

		// Token: 0x170007BE RID: 1982
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x00053167 File Offset: 0x00051367
		public IDownload CurrentDownload
		{
			get
			{
				BaseLinkRelation relation = this._relation;
				if (relation == null)
				{
					return null;
				}
				IRequestProcessor processor = relation.Processor;
				if (processor == null)
				{
					return null;
				}
				return processor.Download;
			}
		}

		// Token: 0x170007BF RID: 1983
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x00053185 File Offset: 0x00051385
		// (set) Token: 0x06001B55 RID: 6997 RVA: 0x0004FD9E File Offset: 0x0004DF9E
		public string Href
		{
			get
			{
				return this.GetUrlAttribute(AttributeNames.Href);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Href, value, false);
			}
		}

		// Token: 0x170007C0 RID: 1984
		// (get) Token: 0x06001B56 RID: 6998 RVA: 0x00053192 File Offset: 0x00051392
		// (set) Token: 0x06001B57 RID: 6999 RVA: 0x0005319F File Offset: 0x0005139F
		public string TargetLanguage
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.HrefLang);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.HrefLang, value, false);
			}
		}

		// Token: 0x170007C1 RID: 1985
		// (get) Token: 0x06001B58 RID: 7000 RVA: 0x0004FC8F File Offset: 0x0004DE8F
		// (set) Token: 0x06001B59 RID: 7001 RVA: 0x0004FC9C File Offset: 0x0004DE9C
		public string Charset
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Charset);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Charset, value, false);
			}
		}

		// Token: 0x170007C2 RID: 1986
		// (get) Token: 0x06001B5A RID: 7002 RVA: 0x000531AE File Offset: 0x000513AE
		// (set) Token: 0x06001B5B RID: 7003 RVA: 0x000531BB File Offset: 0x000513BB
		public string Relation
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rel);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rel, value, false);
			}
		}

		// Token: 0x170007C3 RID: 1987
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000531CA File Offset: 0x000513CA
		public ITokenList RelationList
		{
			get
			{
				if (this._relList == null)
				{
					this._relList = new TokenList(this.GetOwnAttribute(AttributeNames.Rel));
					this._relList.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Rel, value);
					};
				}
				return this._relList;
			}
		}

		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x00053207 File Offset: 0x00051407
		public ISettableTokenList Sizes
		{
			get
			{
				if (this._sizes == null)
				{
					this._sizes = new SettableTokenList(this.GetOwnAttribute(AttributeNames.Sizes));
					this._sizes.Changed += delegate(string value)
					{
						base.UpdateAttribute(AttributeNames.Sizes, value);
					};
				}
				return this._sizes;
			}
		}

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06001B5E RID: 7006 RVA: 0x00053244 File Offset: 0x00051444
		// (set) Token: 0x06001B5F RID: 7007 RVA: 0x00053251 File Offset: 0x00051451
		public string Rev
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Rev);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Rev, value, false);
			}
		}

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06001B60 RID: 7008 RVA: 0x00053260 File Offset: 0x00051460
		// (set) Token: 0x06001B61 RID: 7009 RVA: 0x00051B70 File Offset: 0x0004FD70
		public bool IsDisabled
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.Disabled);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.Disabled, value);
			}
		}

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06001B62 RID: 7010 RVA: 0x0004FDAD File Offset: 0x0004DFAD
		// (set) Token: 0x06001B63 RID: 7011 RVA: 0x0004FDBA File Offset: 0x0004DFBA
		public string Target
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Target);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Target, value, false);
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06001B64 RID: 7012 RVA: 0x0005326D File Offset: 0x0005146D
		// (set) Token: 0x06001B65 RID: 7013 RVA: 0x0005327A File Offset: 0x0005147A
		public string Media
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Media);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Media, value, false);
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06001B66 RID: 7014 RVA: 0x00051A27 File Offset: 0x0004FC27
		// (set) Token: 0x06001B67 RID: 7015 RVA: 0x0004FF58 File Offset: 0x0004E158
		public string Type
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Type);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Type, value, false);
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06001B68 RID: 7016 RVA: 0x00053289 File Offset: 0x00051489
		// (set) Token: 0x06001B69 RID: 7017 RVA: 0x00053296 File Offset: 0x00051496
		public string Integrity
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Integrity);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Integrity, value, false);
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06001B6A RID: 7018 RVA: 0x000532A5 File Offset: 0x000514A5
		public IStyleSheet Sheet
		{
			get
			{
				StyleSheetLinkRelation styleSheetLinkRelation = this._relation as StyleSheetLinkRelation;
				if (styleSheetLinkRelation == null)
				{
					return null;
				}
				return styleSheetLinkRelation.Sheet;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x000532BD File Offset: 0x000514BD
		public IDocument Import
		{
			get
			{
				ImportLinkRelation importLinkRelation = this._relation as ImportLinkRelation;
				if (importLinkRelation == null)
				{
					return null;
				}
				return importLinkRelation.Import;
			}
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x06001B6C RID: 7020 RVA: 0x000528DE File Offset: 0x00050ADE
		// (set) Token: 0x06001B6D RID: 7021 RVA: 0x000528EB File Offset: 0x00050AEB
		public string CrossOrigin
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.CrossOrigin);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.CrossOrigin, value, false);
			}
		}

		// Token: 0x06001B6E RID: 7022 RVA: 0x000532D8 File Offset: 0x000514D8
		internal override void SetupElement()
		{
			base.SetupElement();
			string ownAttribute = this.GetOwnAttribute(AttributeNames.Rel);
			if (ownAttribute != null)
			{
				this.UpdateRelation(ownAttribute);
			}
		}

		// Token: 0x06001B6F RID: 7023 RVA: 0x00053301 File Offset: 0x00051501
		internal void UpdateSizes(string value)
		{
			SettableTokenList sizes = this._sizes;
			if (sizes == null)
			{
				return;
			}
			sizes.Update(value);
		}

		// Token: 0x06001B70 RID: 7024 RVA: 0x00053314 File Offset: 0x00051514
		internal void UpdateMedia(string value)
		{
			IStyleSheet sheet = this.Sheet;
			if (sheet != null)
			{
				sheet.Media.MediaText = value;
			}
		}

		// Token: 0x06001B71 RID: 7025 RVA: 0x00053338 File Offset: 0x00051538
		internal void UpdateDisabled(string value)
		{
			IStyleSheet sheet = this.Sheet;
			if (sheet != null)
			{
				sheet.IsDisabled = value != null;
			}
		}

		// Token: 0x06001B72 RID: 7026 RVA: 0x00053359 File Offset: 0x00051559
		internal void UpdateRelation(string value)
		{
			TokenList relList = this._relList;
			if (relList != null)
			{
				relList.Update(value);
			}
			this._relation = this.CreateFirstLegalRelation();
			this.UpdateSource(this.GetOwnAttribute(AttributeNames.Href));
		}

		// Token: 0x06001B73 RID: 7027 RVA: 0x0005338C File Offset: 0x0005158C
		internal void UpdateSource(string value)
		{
			BaseLinkRelation relation = this._relation;
			Task task = ((relation != null) ? relation.LoadAsync() : null);
			Document owner = base.Owner;
			if (owner == null)
			{
				return;
			}
			owner.DelayLoad(task);
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x000533C0 File Offset: 0x000515C0
		private BaseLinkRelation CreateFirstLegalRelation()
		{
			IEnumerable<string> relationList = this.RelationList;
			Document owner = base.Owner;
			ILinkRelationFactory linkRelationFactory = ((owner != null) ? owner.Options.GetFactory<ILinkRelationFactory>() : null);
			foreach (string text in relationList)
			{
				BaseLinkRelation baseLinkRelation = linkRelationFactory.Create(this, text);
				if (baseLinkRelation != null)
				{
					return baseLinkRelation;
				}
			}
			return null;
		}

		// Token: 0x04000CDD RID: 3293
		private BaseLinkRelation _relation;

		// Token: 0x04000CDE RID: 3294
		private TokenList _relList;

		// Token: 0x04000CDF RID: 3295
		private SettableTokenList _sizes;
	}
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using AngleSharp.Dom.Collections;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Extensions;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp.Dom.Html
{
	// Token: 0x0200035B RID: 859
	internal sealed class HtmlFormElement : HtmlElement, IHtmlFormElement, IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers
	{
		// Token: 0x06001A71 RID: 6769 RVA: 0x00051CF6 File Offset: 0x0004FEF6
		public HtmlFormElement(Document owner, string prefix = null)
			: base(owner, TagNames.Form, prefix, NodeFlags.Special)
		{
		}

		// Token: 0x1700075F RID: 1887
		public IElement this[int index]
		{
			get
			{
				return this.Elements[index];
			}
		}

		// Token: 0x17000760 RID: 1888
		public IElement this[string name]
		{
			get
			{
				return this.Elements[name];
			}
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06001A74 RID: 6772 RVA: 0x0004FCAB File Offset: 0x0004DEAB
		// (set) Token: 0x06001A75 RID: 6773 RVA: 0x0004FCB8 File Offset: 0x0004DEB8
		public string Name
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Name);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Name, value, false);
			}
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x06001A76 RID: 6774 RVA: 0x00051D22 File Offset: 0x0004FF22
		public int Length
		{
			get
			{
				return this.Elements.Length;
			}
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00051D30 File Offset: 0x0004FF30
		public HtmlFormControlsCollection Elements
		{
			get
			{
				HtmlFormControlsCollection htmlFormControlsCollection;
				if ((htmlFormControlsCollection = this._elements) == null)
				{
					htmlFormControlsCollection = (this._elements = new HtmlFormControlsCollection(this, null));
				}
				return htmlFormControlsCollection;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x06001A78 RID: 6776 RVA: 0x00051D57 File Offset: 0x0004FF57
		IHtmlFormControlsCollection IHtmlFormElement.Elements
		{
			get
			{
				return this.Elements;
			}
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00051D5F File Offset: 0x0004FF5F
		// (set) Token: 0x06001A7A RID: 6778 RVA: 0x00051D6C File Offset: 0x0004FF6C
		public string AcceptCharset
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.AcceptCharset);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.AcceptCharset, value, false);
			}
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x06001A7B RID: 6779 RVA: 0x00051D7B File Offset: 0x0004FF7B
		// (set) Token: 0x06001A7C RID: 6780 RVA: 0x00051D88 File Offset: 0x0004FF88
		public string Action
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Action);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Action, value, false);
			}
		}

		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06001A7D RID: 6781 RVA: 0x00051D97 File Offset: 0x0004FF97
		// (set) Token: 0x06001A7E RID: 6782 RVA: 0x00051DA4 File Offset: 0x0004FFA4
		public string Autocomplete
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.AutoComplete);
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.AutoComplete, value, false);
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06001A7F RID: 6783 RVA: 0x00051DB3 File Offset: 0x0004FFB3
		// (set) Token: 0x06001A80 RID: 6784 RVA: 0x00051DC5 File Offset: 0x0004FFC5
		public string Enctype
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Enctype).ToEncodingType();
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Enctype, value.ToEncodingType(), false);
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06001A81 RID: 6785 RVA: 0x00051DD9 File Offset: 0x0004FFD9
		// (set) Token: 0x06001A82 RID: 6786 RVA: 0x00051DE1 File Offset: 0x0004FFE1
		public string Encoding
		{
			get
			{
				return this.Enctype;
			}
			set
			{
				this.Enctype = value;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06001A83 RID: 6787 RVA: 0x00051DEA File Offset: 0x0004FFEA
		// (set) Token: 0x06001A84 RID: 6788 RVA: 0x00051E00 File Offset: 0x00050000
		public string Method
		{
			get
			{
				return this.GetOwnAttribute(AttributeNames.Method) ?? string.Empty;
			}
			set
			{
				this.SetOwnAttribute(AttributeNames.Method, value, false);
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06001A85 RID: 6789 RVA: 0x00051E0F File Offset: 0x0005000F
		// (set) Token: 0x06001A86 RID: 6790 RVA: 0x00051E1C File Offset: 0x0005001C
		public bool NoValidate
		{
			get
			{
				return this.GetBoolAttribute(AttributeNames.NoValidate);
			}
			set
			{
				this.SetBoolAttribute(AttributeNames.NoValidate, value);
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001A87 RID: 6791 RVA: 0x0004FDAD File Offset: 0x0004DFAD
		// (set) Token: 0x06001A88 RID: 6792 RVA: 0x0004FDBA File Offset: 0x0004DFBA
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

		// Token: 0x06001A89 RID: 6793 RVA: 0x00051E2C File Offset: 0x0005002C
		public Task<IDocument> SubmitAsync()
		{
			DocumentRequest submission = this.GetSubmission();
			return this.NavigateToAsync(submission);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x00051E48 File Offset: 0x00050048
		public Task<IDocument> SubmitAsync(IHtmlElement sourceElement)
		{
			DocumentRequest submission = this.GetSubmission(sourceElement);
			return this.NavigateToAsync(submission);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x00051E64 File Offset: 0x00050064
		public DocumentRequest GetSubmission()
		{
			return this.SubmitForm(this, true);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x00051E6E File Offset: 0x0005006E
		public DocumentRequest GetSubmission(IHtmlElement sourceElement)
		{
			return this.SubmitForm(sourceElement ?? this, false);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x00051E80 File Offset: 0x00050080
		public void Reset()
		{
			foreach (HtmlFormControlElement htmlFormControlElement in this.Elements)
			{
				htmlFormControlElement.Reset();
			}
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x00051ECC File Offset: 0x000500CC
		public bool CheckValidity()
		{
			IEnumerable<HtmlFormControlElement> invalidControls = this.GetInvalidControls();
			bool flag = true;
			using (IEnumerator<HtmlFormControlElement> enumerator = invalidControls.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.FireSimpleEvent(EventNames.Invalid, false, true))
					{
						flag = false;
					}
				}
			}
			return flag;
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x00051F24 File Offset: 0x00050124
		private IEnumerable<HtmlFormControlElement> GetInvalidControls()
		{
			foreach (HtmlFormControlElement htmlFormControlElement in this.Elements)
			{
				if (htmlFormControlElement.WillValidate && !htmlFormControlElement.CheckValidity())
				{
					yield return htmlFormControlElement;
				}
			}
			IEnumerator<HtmlFormControlElement> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x00051F34 File Offset: 0x00050134
		public bool ReportValidity()
		{
			IEnumerable<HtmlFormControlElement> invalidControls = this.GetInvalidControls();
			bool flag = true;
			bool flag2 = false;
			foreach (HtmlFormControlElement htmlFormControlElement in invalidControls)
			{
				if (!htmlFormControlElement.FireSimpleEvent(EventNames.Invalid, false, true))
				{
					if (!flag2)
					{
						htmlFormControlElement.DoFocus();
						flag2 = true;
					}
					flag = false;
				}
			}
			return flag;
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x00003C25 File Offset: 0x00001E25
		public void RequestAutocomplete()
		{
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x00051F9C File Offset: 0x0005019C
		private DocumentRequest SubmitForm(IHtmlElement from, bool submittedFromSubmitMethod)
		{
			Document owner = base.Owner;
			if ((owner.ActiveSandboxing & Sandboxes.Forms) != Sandboxes.Forms)
			{
				if (submittedFromSubmitMethod || from.HasAttribute(AttributeNames.FormNoValidate) || this.NoValidate || this.CheckValidity())
				{
					Url url = (string.IsNullOrEmpty(this.Action) ? new Url(owner.DocumentUri) : this.HyperReference(this.Action));
					bool flag = false;
					IBrowsingContext context = owner.Context;
					string target = this.Target;
					DocumentReadyState readyState = owner.ReadyState;
					if (!string.IsNullOrEmpty(target))
					{
						flag = owner.GetTarget(target) == null;
					}
					if (flag)
					{
						owner.CreateTarget(target);
					}
					string scheme = url.Scheme;
					HttpMethod httpMethod = this.Method.ToEnum(HttpMethod.Get);
					return this.SubmitForm(httpMethod, scheme, url, from);
				}
				this.FireSimpleEvent(EventNames.Invalid, false, false);
			}
			return null;
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x00052070 File Offset: 0x00050270
		private DocumentRequest SubmitForm(HttpMethod method, string scheme, Url action, IHtmlElement submitter)
		{
			if (scheme.IsOneOf(ProtocolNames.Http, ProtocolNames.Https))
			{
				if (method == HttpMethod.Get)
				{
					return this.MutateActionUrl(action, submitter);
				}
				if (method == HttpMethod.Post)
				{
					return this.SubmitAsEntityBody(action, submitter);
				}
			}
			else if (scheme.Is(ProtocolNames.Data))
			{
				if (method == HttpMethod.Get)
				{
					return this.GetActionUrl(action);
				}
				if (method == HttpMethod.Post)
				{
					return this.PostToData(action, submitter);
				}
			}
			else if (scheme.Is(ProtocolNames.Mailto))
			{
				if (method == HttpMethod.Get)
				{
					return this.MailWithHeaders(action, submitter);
				}
				if (method == HttpMethod.Post)
				{
					return this.MailAsBody(action, submitter);
				}
			}
			else if (scheme.IsOneOf(ProtocolNames.Ftp, ProtocolNames.JavaScript))
			{
				return this.GetActionUrl(action);
			}
			return this.MutateActionUrl(action, submitter);
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x0005211C File Offset: 0x0005031C
		private DocumentRequest PostToData(Url action, IHtmlElement submitter)
		{
			string text = (string.IsNullOrEmpty(this.AcceptCharset) ? base.Owner.CharacterSet : this.AcceptCharset);
			FormDataSet formDataSet = this.ConstructDataSet(submitter);
			string enctype = this.Enctype;
			string text2 = string.Empty;
			using (StreamReader streamReader = new StreamReader(formDataSet.CreateBody(enctype, text)))
			{
				text2 = streamReader.ReadToEnd();
			}
			if (action.Href.Contains("%%%%"))
			{
				text2 = TextEncoding.UsAscii.GetBytes(text2).UrlEncode();
				action.Href = action.Href.ReplaceFirst("%%%%", text2);
			}
			else if (action.Href.Contains("%%"))
			{
				text2 = TextEncoding.Utf8.GetBytes(text2).UrlEncode();
				action.Href = action.Href.ReplaceFirst("%%", text2);
			}
			return this.GetActionUrl(action);
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x0005220C File Offset: 0x0005040C
		private DocumentRequest MailWithHeaders(Url action, IHtmlElement submitter)
		{
			Stream stream = this.ConstructDataSet(submitter).AsUrlEncoded(TextEncoding.UsAscii);
			string text = string.Empty;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				text = streamReader.ReadToEnd();
			}
			action.Query = text.Replace("+", "%20");
			return this.GetActionUrl(action);
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x00052278 File Offset: 0x00050478
		private DocumentRequest MailAsBody(Url action, IHtmlElement submitter)
		{
			FormDataSet formDataSet = this.ConstructDataSet(submitter);
			string enctype = this.Enctype;
			Encoding usAscii = TextEncoding.UsAscii;
			Stream stream = formDataSet.CreateBody(enctype, usAscii);
			string text = string.Empty;
			using (StreamReader streamReader = new StreamReader(stream))
			{
				text = streamReader.ReadToEnd();
			}
			action.Query = "body=" + usAscii.GetBytes(text).UrlEncode();
			return this.GetActionUrl(action);
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000522F4 File Offset: 0x000504F4
		private DocumentRequest GetActionUrl(Url action)
		{
			return DocumentRequest.Get(action, this, base.Owner.DocumentUri);
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x00052308 File Offset: 0x00050508
		private DocumentRequest SubmitAsEntityBody(Url target, IHtmlElement submitter)
		{
			string text = (string.IsNullOrEmpty(this.AcceptCharset) ? base.Owner.CharacterSet : this.AcceptCharset);
			FormDataSet formDataSet = this.ConstructDataSet(submitter);
			string text2 = this.Enctype;
			Stream stream = formDataSet.CreateBody(text2, text);
			if (text2.Isi(MimeTypeNames.MultipartForm))
			{
				text2 = MimeTypeNames.MultipartForm + "; boundary=" + formDataSet.Boundary;
			}
			return DocumentRequest.Post(target, stream, text2, this, base.Owner.DocumentUri);
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x00052388 File Offset: 0x00050588
		private DocumentRequest MutateActionUrl(Url action, IHtmlElement submitter)
		{
			string text = (string.IsNullOrEmpty(this.AcceptCharset) ? base.Owner.CharacterSet : this.AcceptCharset);
			FormDataSet formDataSet = this.ConstructDataSet(submitter);
			Encoding encoding = TextEncoding.Resolve(text);
			using (StreamReader streamReader = new StreamReader(formDataSet.AsUrlEncoded(encoding)))
			{
				action.Query = streamReader.ReadToEnd();
			}
			return this.GetActionUrl(action);
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x00052400 File Offset: 0x00050600
		private FormDataSet ConstructDataSet(IHtmlElement submitter)
		{
			FormDataSet formDataSet = new FormDataSet();
			foreach (HtmlFormControlElement htmlFormControlElement in this.GetElements(true, null))
			{
				if (!htmlFormControlElement.IsDisabled && !(htmlFormControlElement.ParentElement is IHtmlDataListElement) && htmlFormControlElement.Form == this)
				{
					htmlFormControlElement.ConstructDataSet(formDataSet, submitter);
				}
			}
			return formDataSet;
		}

		// Token: 0x04000CD1 RID: 3281
		private HtmlFormControlsCollection _elements;
	}
}

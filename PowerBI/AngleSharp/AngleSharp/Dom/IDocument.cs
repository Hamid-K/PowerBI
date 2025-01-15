using System;
using System.Net;
using AngleSharp.Attributes;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Html;

namespace AngleSharp.Dom
{
	// Token: 0x02000180 RID: 384
	[DomName("Document")]
	public interface IDocument : INode, IEventTarget, IMarkupFormattable, IParentNode, IGlobalEventHandlers, IDocumentStyle, INonElementParentNode, IDisposable
	{
		// Token: 0x1700027F RID: 639
		// (get) Token: 0x06000DBC RID: 3516
		[DomName("all")]
		IHtmlAllCollection All { get; }

		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000DBD RID: 3517
		[DomName("anchors")]
		IHtmlCollection<IHtmlAnchorElement> Anchors { get; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000DBE RID: 3518
		[DomName("implementation")]
		IImplementation Implementation { get; }

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000DBF RID: 3519
		// (set) Token: 0x06000DC0 RID: 3520
		[DomName("designMode")]
		string DesignMode { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000DC1 RID: 3521
		// (set) Token: 0x06000DC2 RID: 3522
		[DomName("dir")]
		string Direction { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x06000DC3 RID: 3523
		[DomName("documentURI")]
		string DocumentUri { get; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000DC4 RID: 3524
		[DomName("characterSet")]
		string CharacterSet { get; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x06000DC5 RID: 3525
		[DomName("compatMode")]
		string CompatMode { get; }

		// Token: 0x17000287 RID: 647
		// (get) Token: 0x06000DC6 RID: 3526
		[DomName("URL")]
		string Url { get; }

		// Token: 0x17000288 RID: 648
		// (get) Token: 0x06000DC7 RID: 3527
		[DomName("contentType")]
		string ContentType { get; }

		// Token: 0x06000DC8 RID: 3528
		[DomName("open")]
		IDocument Open(string type = "text/html", string replace = null);

		// Token: 0x06000DC9 RID: 3529
		[DomName("close")]
		void Close();

		// Token: 0x06000DCA RID: 3530
		[DomName("write")]
		void Write(string content);

		// Token: 0x06000DCB RID: 3531
		[DomName("writeln")]
		void WriteLine(string content);

		// Token: 0x06000DCC RID: 3532
		[DomName("load")]
		void Load(string url);

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000DCD RID: 3533
		[DomName("doctype")]
		IDocumentType Doctype { get; }

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000DCE RID: 3534
		[DomName("documentElement")]
		IElement DocumentElement { get; }

		// Token: 0x06000DCF RID: 3535
		[DomName("getElementsByName")]
		IHtmlCollection<IElement> GetElementsByName(string name);

		// Token: 0x06000DD0 RID: 3536
		[DomName("getElementsByClassName")]
		IHtmlCollection<IElement> GetElementsByClassName(string classNames);

		// Token: 0x06000DD1 RID: 3537
		[DomName("getElementsByTagName")]
		IHtmlCollection<IElement> GetElementsByTagName(string tagName);

		// Token: 0x06000DD2 RID: 3538
		[DomName("getElementsByTagNameNS")]
		IHtmlCollection<IElement> GetElementsByTagName(string namespaceUri, string tagName);

		// Token: 0x06000DD3 RID: 3539
		[DomName("createEvent")]
		Event CreateEvent(string type);

		// Token: 0x06000DD4 RID: 3540
		[DomName("createRange")]
		IRange CreateRange();

		// Token: 0x06000DD5 RID: 3541
		[DomName("createComment")]
		IComment CreateComment(string data);

		// Token: 0x06000DD6 RID: 3542
		[DomName("createDocumentFragment")]
		IDocumentFragment CreateDocumentFragment();

		// Token: 0x06000DD7 RID: 3543
		[DomName("createElement")]
		IElement CreateElement(string name);

		// Token: 0x06000DD8 RID: 3544
		[DomName("createElementNS")]
		IElement CreateElement(string namespaceUri, string name);

		// Token: 0x06000DD9 RID: 3545
		[DomName("createAttribute")]
		IAttr CreateAttribute(string name);

		// Token: 0x06000DDA RID: 3546
		[DomName("createAttributeNS")]
		IAttr CreateAttribute(string namespaceUri, string name);

		// Token: 0x06000DDB RID: 3547
		[DomName("createProcessingInstruction")]
		IProcessingInstruction CreateProcessingInstruction(string target, string data);

		// Token: 0x06000DDC RID: 3548
		[DomName("createTextNode")]
		IText CreateTextNode(string data);

		// Token: 0x06000DDD RID: 3549
		[DomName("createNodeIterator")]
		INodeIterator CreateNodeIterator(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null);

		// Token: 0x06000DDE RID: 3550
		[DomName("createTreeWalker")]
		ITreeWalker CreateTreeWalker(INode root, FilterSettings settings = FilterSettings.All, NodeFilter filter = null);

		// Token: 0x06000DDF RID: 3551
		[DomName("importNode")]
		INode Import(INode externalNode, bool deep = true);

		// Token: 0x06000DE0 RID: 3552
		[DomName("adoptNode")]
		INode Adopt(INode externalNode);

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000DE1 RID: 3553
		[DomName("lastModified")]
		string LastModified { get; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000DE2 RID: 3554
		[DomLenientThis]
		[DomName("readyState")]
		DocumentReadyState ReadyState { get; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000DE3 RID: 3555
		[DomName("location")]
		[DomPutForwards("href")]
		ILocation Location { get; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000DE4 RID: 3556
		[DomName("forms")]
		IHtmlCollection<IHtmlFormElement> Forms { get; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000DE5 RID: 3557
		[DomName("images")]
		IHtmlCollection<IHtmlImageElement> Images { get; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000DE6 RID: 3558
		[DomName("scripts")]
		IHtmlCollection<IHtmlScriptElement> Scripts { get; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000DE7 RID: 3559
		[DomName("embeds")]
		[DomName("plugins")]
		IHtmlCollection<IHtmlEmbedElement> Plugins { get; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000DE8 RID: 3560
		[DomName("commands")]
		IHtmlCollection<IElement> Commands { get; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000DE9 RID: 3561
		[DomName("links")]
		IHtmlCollection<IElement> Links { get; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000DEA RID: 3562
		// (set) Token: 0x06000DEB RID: 3563
		[DomName("title")]
		string Title { get; set; }

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000DEC RID: 3564
		[DomName("head")]
		IHtmlHeadElement Head { get; }

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000DED RID: 3565
		// (set) Token: 0x06000DEE RID: 3566
		[DomName("body")]
		IHtmlElement Body { get; set; }

		// Token: 0x17000297 RID: 663
		// (get) Token: 0x06000DEF RID: 3567
		// (set) Token: 0x06000DF0 RID: 3568
		[DomName("cookie")]
		string Cookie { get; set; }

		// Token: 0x17000298 RID: 664
		// (get) Token: 0x06000DF1 RID: 3569
		[DomName("origin")]
		string Origin { get; }

		// Token: 0x17000299 RID: 665
		// (get) Token: 0x06000DF2 RID: 3570
		// (set) Token: 0x06000DF3 RID: 3571
		[DomName("domain")]
		string Domain { get; set; }

		// Token: 0x1700029A RID: 666
		// (get) Token: 0x06000DF4 RID: 3572
		[DomName("referrer")]
		string Referrer { get; }

		// Token: 0x14000098 RID: 152
		// (add) Token: 0x06000DF5 RID: 3573
		// (remove) Token: 0x06000DF6 RID: 3574
		[DomName("onreadystatechange")]
		event DomEventHandler ReadyStateChanged;

		// Token: 0x1700029B RID: 667
		// (get) Token: 0x06000DF7 RID: 3575
		[DomName("activeElement")]
		IElement ActiveElement { get; }

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06000DF8 RID: 3576
		[DomName("currentScript")]
		IHtmlScriptElement CurrentScript { get; }

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000DF9 RID: 3577
		[DomName("defaultView")]
		IWindow DefaultView { get; }

		// Token: 0x06000DFA RID: 3578
		[DomName("hasFocus")]
		bool HasFocus();

		// Token: 0x06000DFB RID: 3579
		[DomName("execCommand")]
		bool ExecuteCommand(string commandId, bool showUserInterface = false, string value = "");

		// Token: 0x06000DFC RID: 3580
		[DomName("queryCommandEnabled")]
		bool IsCommandEnabled(string commandId);

		// Token: 0x06000DFD RID: 3581
		[DomName("queryCommandIndeterm")]
		bool IsCommandIndeterminate(string commandId);

		// Token: 0x06000DFE RID: 3582
		[DomName("queryCommandState")]
		bool IsCommandExecuted(string commandId);

		// Token: 0x06000DFF RID: 3583
		[DomName("queryCommandSupported")]
		bool IsCommandSupported(string commandId);

		// Token: 0x06000E00 RID: 3584
		[DomName("queryCommandValue")]
		string GetCommandValue(string commandId);

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000E01 RID: 3585
		IBrowsingContext Context { get; }

		// Token: 0x1700029F RID: 671
		// (get) Token: 0x06000E02 RID: 3586
		IDocument ImportAncestor { get; }

		// Token: 0x170002A0 RID: 672
		// (get) Token: 0x06000E03 RID: 3587
		TextSource Source { get; }

		// Token: 0x170002A1 RID: 673
		// (get) Token: 0x06000E04 RID: 3588
		HttpStatusCode StatusCode { get; }
	}
}

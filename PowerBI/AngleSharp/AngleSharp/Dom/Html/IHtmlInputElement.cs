using System;
using AngleSharp.Attributes;
using AngleSharp.Dom.Css;
using AngleSharp.Dom.Events;
using AngleSharp.Dom.Io;

namespace AngleSharp.Dom.Html
{
	// Token: 0x020003C8 RID: 968
	[DomName("HTMLInputElement")]
	public interface IHtmlInputElement : IHtmlElement, IElement, INode, IEventTarget, IMarkupFormattable, IParentNode, IChildNode, INonDocumentTypeChildNode, IElementCssInlineStyle, IGlobalEventHandlers, IValidation
	{
		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06001E8D RID: 7821
		// (set) Token: 0x06001E8E RID: 7822
		[DomName("autofocus")]
		bool Autofocus { get; set; }

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06001E8F RID: 7823
		// (set) Token: 0x06001E90 RID: 7824
		[DomName("accept")]
		string Accept { get; set; }

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06001E91 RID: 7825
		// (set) Token: 0x06001E92 RID: 7826
		[DomName("autocomplete")]
		string Autocomplete { get; set; }

		// Token: 0x17000935 RID: 2357
		// (get) Token: 0x06001E93 RID: 7827
		// (set) Token: 0x06001E94 RID: 7828
		[DomName("disabled")]
		bool IsDisabled { get; set; }

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x06001E95 RID: 7829
		[DomName("form")]
		IHtmlFormElement Form { get; }

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06001E96 RID: 7830
		[DomName("labels")]
		INodeList Labels { get; }

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06001E97 RID: 7831
		[DomName("files")]
		IFileList Files { get; }

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06001E98 RID: 7832
		// (set) Token: 0x06001E99 RID: 7833
		[DomName("name")]
		string Name { get; set; }

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06001E9A RID: 7834
		// (set) Token: 0x06001E9B RID: 7835
		[DomName("type")]
		string Type { get; set; }

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06001E9C RID: 7836
		// (set) Token: 0x06001E9D RID: 7837
		[DomName("required")]
		bool IsRequired { get; set; }

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06001E9E RID: 7838
		// (set) Token: 0x06001E9F RID: 7839
		[DomName("readOnly")]
		bool IsReadOnly { get; set; }

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06001EA0 RID: 7840
		// (set) Token: 0x06001EA1 RID: 7841
		[DomName("alt")]
		string AlternativeText { get; set; }

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06001EA2 RID: 7842
		// (set) Token: 0x06001EA3 RID: 7843
		[DomName("src")]
		string Source { get; set; }

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06001EA4 RID: 7844
		// (set) Token: 0x06001EA5 RID: 7845
		[DomName("max")]
		string Maximum { get; set; }

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06001EA6 RID: 7846
		// (set) Token: 0x06001EA7 RID: 7847
		[DomName("min")]
		string Minimum { get; set; }

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06001EA8 RID: 7848
		// (set) Token: 0x06001EA9 RID: 7849
		[DomName("pattern")]
		string Pattern { get; set; }

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06001EAA RID: 7850
		// (set) Token: 0x06001EAB RID: 7851
		[DomName("step")]
		string Step { get; set; }

		// Token: 0x06001EAC RID: 7852
		[DomName("stepUp")]
		void StepUp(int n = 1);

		// Token: 0x06001EAD RID: 7853
		[DomName("stepDown")]
		void StepDown(int n = 1);

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06001EAE RID: 7854
		[DomName("list")]
		IHtmlDataListElement List { get; }

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06001EAF RID: 7855
		// (set) Token: 0x06001EB0 RID: 7856
		[DomName("formAction")]
		string FormAction { get; set; }

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06001EB1 RID: 7857
		// (set) Token: 0x06001EB2 RID: 7858
		[DomName("formEncType")]
		string FormEncType { get; set; }

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06001EB3 RID: 7859
		// (set) Token: 0x06001EB4 RID: 7860
		[DomName("formMethod")]
		string FormMethod { get; set; }

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06001EB5 RID: 7861
		// (set) Token: 0x06001EB6 RID: 7862
		[DomName("formNoValidate")]
		bool FormNoValidate { get; set; }

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06001EB7 RID: 7863
		// (set) Token: 0x06001EB8 RID: 7864
		[DomName("formTarget")]
		string FormTarget { get; set; }

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06001EB9 RID: 7865
		// (set) Token: 0x06001EBA RID: 7866
		[DomName("defaultValue")]
		string DefaultValue { get; set; }

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06001EBB RID: 7867
		// (set) Token: 0x06001EBC RID: 7868
		[DomName("value")]
		string Value { get; set; }

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06001EBD RID: 7869
		bool HasValue { get; }

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06001EBE RID: 7870
		// (set) Token: 0x06001EBF RID: 7871
		[DomName("valueAsNumber")]
		double ValueAsNumber { get; set; }

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06001EC0 RID: 7872
		// (set) Token: 0x06001EC1 RID: 7873
		[DomName("valueAsDate")]
		DateTime? ValueAsDate { get; set; }

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06001EC2 RID: 7874
		// (set) Token: 0x06001EC3 RID: 7875
		[DomName("indeterminate")]
		bool IsIndeterminate { get; set; }

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06001EC4 RID: 7876
		// (set) Token: 0x06001EC5 RID: 7877
		[DomName("defaultChecked")]
		bool IsDefaultChecked { get; set; }

		// Token: 0x17000950 RID: 2384
		// (get) Token: 0x06001EC6 RID: 7878
		// (set) Token: 0x06001EC7 RID: 7879
		[DomName("checked")]
		bool IsChecked { get; set; }

		// Token: 0x17000951 RID: 2385
		// (get) Token: 0x06001EC8 RID: 7880
		// (set) Token: 0x06001EC9 RID: 7881
		[DomName("size")]
		int Size { get; set; }

		// Token: 0x17000952 RID: 2386
		// (get) Token: 0x06001ECA RID: 7882
		// (set) Token: 0x06001ECB RID: 7883
		[DomName("multiple")]
		bool IsMultiple { get; set; }

		// Token: 0x17000953 RID: 2387
		// (get) Token: 0x06001ECC RID: 7884
		// (set) Token: 0x06001ECD RID: 7885
		[DomName("maxLength")]
		int MaxLength { get; set; }

		// Token: 0x17000954 RID: 2388
		// (get) Token: 0x06001ECE RID: 7886
		// (set) Token: 0x06001ECF RID: 7887
		[DomName("placeholder")]
		string Placeholder { get; set; }

		// Token: 0x17000955 RID: 2389
		// (get) Token: 0x06001ED0 RID: 7888
		// (set) Token: 0x06001ED1 RID: 7889
		[DomName("width")]
		int DisplayWidth { get; set; }

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06001ED2 RID: 7890
		// (set) Token: 0x06001ED3 RID: 7891
		[DomName("height")]
		int DisplayHeight { get; set; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06001ED4 RID: 7892
		[DomName("selectionDirection")]
		string SelectionDirection { get; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06001ED5 RID: 7893
		// (set) Token: 0x06001ED6 RID: 7894
		[DomName("dirName")]
		string DirectionName { get; set; }

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06001ED7 RID: 7895
		// (set) Token: 0x06001ED8 RID: 7896
		[DomName("selectionStart")]
		int SelectionStart { get; set; }

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06001ED9 RID: 7897
		// (set) Token: 0x06001EDA RID: 7898
		[DomName("selectionEnd")]
		int SelectionEnd { get; set; }

		// Token: 0x06001EDB RID: 7899
		[DomName("select")]
		void SelectAll();

		// Token: 0x06001EDC RID: 7900
		[DomName("setSelectionRange")]
		void Select(int selectionStart, int selectionEnd, string selectionDirection = null);
	}
}

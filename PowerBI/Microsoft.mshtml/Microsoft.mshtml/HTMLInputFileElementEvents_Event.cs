using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000D16 RID: 3350
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLInputFileElementEvents\u0000), typeof(HTMLInputFileElementEvents_EventProvider\u0000))]
	public interface HTMLInputFileElementEvents_Event
	{
		// Token: 0x14002C36 RID: 11318
		// (add) Token: 0x06016E55 RID: 93781
		// (remove) Token: 0x06016E56 RID: 93782
		event HTMLInputFileElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14002C37 RID: 11319
		// (add) Token: 0x06016E57 RID: 93783
		// (remove) Token: 0x06016E58 RID: 93784
		event HTMLInputFileElementEvents_onclickEventHandler onclick;

		// Token: 0x14002C38 RID: 11320
		// (add) Token: 0x06016E59 RID: 93785
		// (remove) Token: 0x06016E5A RID: 93786
		event HTMLInputFileElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14002C39 RID: 11321
		// (add) Token: 0x06016E5B RID: 93787
		// (remove) Token: 0x06016E5C RID: 93788
		event HTMLInputFileElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14002C3A RID: 11322
		// (add) Token: 0x06016E5D RID: 93789
		// (remove) Token: 0x06016E5E RID: 93790
		event HTMLInputFileElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14002C3B RID: 11323
		// (add) Token: 0x06016E5F RID: 93791
		// (remove) Token: 0x06016E60 RID: 93792
		event HTMLInputFileElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14002C3C RID: 11324
		// (add) Token: 0x06016E61 RID: 93793
		// (remove) Token: 0x06016E62 RID: 93794
		event HTMLInputFileElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14002C3D RID: 11325
		// (add) Token: 0x06016E63 RID: 93795
		// (remove) Token: 0x06016E64 RID: 93796
		event HTMLInputFileElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14002C3E RID: 11326
		// (add) Token: 0x06016E65 RID: 93797
		// (remove) Token: 0x06016E66 RID: 93798
		event HTMLInputFileElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14002C3F RID: 11327
		// (add) Token: 0x06016E67 RID: 93799
		// (remove) Token: 0x06016E68 RID: 93800
		event HTMLInputFileElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14002C40 RID: 11328
		// (add) Token: 0x06016E69 RID: 93801
		// (remove) Token: 0x06016E6A RID: 93802
		event HTMLInputFileElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14002C41 RID: 11329
		// (add) Token: 0x06016E6B RID: 93803
		// (remove) Token: 0x06016E6C RID: 93804
		event HTMLInputFileElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14002C42 RID: 11330
		// (add) Token: 0x06016E6D RID: 93805
		// (remove) Token: 0x06016E6E RID: 93806
		event HTMLInputFileElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14002C43 RID: 11331
		// (add) Token: 0x06016E6F RID: 93807
		// (remove) Token: 0x06016E70 RID: 93808
		event HTMLInputFileElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14002C44 RID: 11332
		// (add) Token: 0x06016E71 RID: 93809
		// (remove) Token: 0x06016E72 RID: 93810
		event HTMLInputFileElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14002C45 RID: 11333
		// (add) Token: 0x06016E73 RID: 93811
		// (remove) Token: 0x06016E74 RID: 93812
		event HTMLInputFileElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14002C46 RID: 11334
		// (add) Token: 0x06016E75 RID: 93813
		// (remove) Token: 0x06016E76 RID: 93814
		event HTMLInputFileElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14002C47 RID: 11335
		// (add) Token: 0x06016E77 RID: 93815
		// (remove) Token: 0x06016E78 RID: 93816
		event HTMLInputFileElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14002C48 RID: 11336
		// (add) Token: 0x06016E79 RID: 93817
		// (remove) Token: 0x06016E7A RID: 93818
		event HTMLInputFileElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14002C49 RID: 11337
		// (add) Token: 0x06016E7B RID: 93819
		// (remove) Token: 0x06016E7C RID: 93820
		event HTMLInputFileElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002C4A RID: 11338
		// (add) Token: 0x06016E7D RID: 93821
		// (remove) Token: 0x06016E7E RID: 93822
		event HTMLInputFileElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002C4B RID: 11339
		// (add) Token: 0x06016E7F RID: 93823
		// (remove) Token: 0x06016E80 RID: 93824
		event HTMLInputFileElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14002C4C RID: 11340
		// (add) Token: 0x06016E81 RID: 93825
		// (remove) Token: 0x06016E82 RID: 93826
		event HTMLInputFileElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002C4D RID: 11341
		// (add) Token: 0x06016E83 RID: 93827
		// (remove) Token: 0x06016E84 RID: 93828
		event HTMLInputFileElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002C4E RID: 11342
		// (add) Token: 0x06016E85 RID: 93829
		// (remove) Token: 0x06016E86 RID: 93830
		event HTMLInputFileElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14002C4F RID: 11343
		// (add) Token: 0x06016E87 RID: 93831
		// (remove) Token: 0x06016E88 RID: 93832
		event HTMLInputFileElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14002C50 RID: 11344
		// (add) Token: 0x06016E89 RID: 93833
		// (remove) Token: 0x06016E8A RID: 93834
		event HTMLInputFileElementEvents_onblurEventHandler onblur;

		// Token: 0x14002C51 RID: 11345
		// (add) Token: 0x06016E8B RID: 93835
		// (remove) Token: 0x06016E8C RID: 93836
		event HTMLInputFileElementEvents_onresizeEventHandler onresize;

		// Token: 0x14002C52 RID: 11346
		// (add) Token: 0x06016E8D RID: 93837
		// (remove) Token: 0x06016E8E RID: 93838
		event HTMLInputFileElementEvents_ondragEventHandler ondrag;

		// Token: 0x14002C53 RID: 11347
		// (add) Token: 0x06016E8F RID: 93839
		// (remove) Token: 0x06016E90 RID: 93840
		event HTMLInputFileElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14002C54 RID: 11348
		// (add) Token: 0x06016E91 RID: 93841
		// (remove) Token: 0x06016E92 RID: 93842
		event HTMLInputFileElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14002C55 RID: 11349
		// (add) Token: 0x06016E93 RID: 93843
		// (remove) Token: 0x06016E94 RID: 93844
		event HTMLInputFileElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002C56 RID: 11350
		// (add) Token: 0x06016E95 RID: 93845
		// (remove) Token: 0x06016E96 RID: 93846
		event HTMLInputFileElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002C57 RID: 11351
		// (add) Token: 0x06016E97 RID: 93847
		// (remove) Token: 0x06016E98 RID: 93848
		event HTMLInputFileElementEvents_ondropEventHandler ondrop;

		// Token: 0x14002C58 RID: 11352
		// (add) Token: 0x06016E99 RID: 93849
		// (remove) Token: 0x06016E9A RID: 93850
		event HTMLInputFileElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002C59 RID: 11353
		// (add) Token: 0x06016E9B RID: 93851
		// (remove) Token: 0x06016E9C RID: 93852
		event HTMLInputFileElementEvents_oncutEventHandler oncut;

		// Token: 0x14002C5A RID: 11354
		// (add) Token: 0x06016E9D RID: 93853
		// (remove) Token: 0x06016E9E RID: 93854
		event HTMLInputFileElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002C5B RID: 11355
		// (add) Token: 0x06016E9F RID: 93855
		// (remove) Token: 0x06016EA0 RID: 93856
		event HTMLInputFileElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14002C5C RID: 11356
		// (add) Token: 0x06016EA1 RID: 93857
		// (remove) Token: 0x06016EA2 RID: 93858
		event HTMLInputFileElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002C5D RID: 11357
		// (add) Token: 0x06016EA3 RID: 93859
		// (remove) Token: 0x06016EA4 RID: 93860
		event HTMLInputFileElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14002C5E RID: 11358
		// (add) Token: 0x06016EA5 RID: 93861
		// (remove) Token: 0x06016EA6 RID: 93862
		event HTMLInputFileElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002C5F RID: 11359
		// (add) Token: 0x06016EA7 RID: 93863
		// (remove) Token: 0x06016EA8 RID: 93864
		event HTMLInputFileElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14002C60 RID: 11360
		// (add) Token: 0x06016EA9 RID: 93865
		// (remove) Token: 0x06016EAA RID: 93866
		event HTMLInputFileElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14002C61 RID: 11361
		// (add) Token: 0x06016EAB RID: 93867
		// (remove) Token: 0x06016EAC RID: 93868
		event HTMLInputFileElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14002C62 RID: 11362
		// (add) Token: 0x06016EAD RID: 93869
		// (remove) Token: 0x06016EAE RID: 93870
		event HTMLInputFileElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002C63 RID: 11363
		// (add) Token: 0x06016EAF RID: 93871
		// (remove) Token: 0x06016EB0 RID: 93872
		event HTMLInputFileElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14002C64 RID: 11364
		// (add) Token: 0x06016EB1 RID: 93873
		// (remove) Token: 0x06016EB2 RID: 93874
		event HTMLInputFileElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002C65 RID: 11365
		// (add) Token: 0x06016EB3 RID: 93875
		// (remove) Token: 0x06016EB4 RID: 93876
		event HTMLInputFileElementEvents_onpageEventHandler onpage;

		// Token: 0x14002C66 RID: 11366
		// (add) Token: 0x06016EB5 RID: 93877
		// (remove) Token: 0x06016EB6 RID: 93878
		event HTMLInputFileElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002C67 RID: 11367
		// (add) Token: 0x06016EB7 RID: 93879
		// (remove) Token: 0x06016EB8 RID: 93880
		event HTMLInputFileElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002C68 RID: 11368
		// (add) Token: 0x06016EB9 RID: 93881
		// (remove) Token: 0x06016EBA RID: 93882
		event HTMLInputFileElementEvents_onmoveEventHandler onmove;

		// Token: 0x14002C69 RID: 11369
		// (add) Token: 0x06016EBB RID: 93883
		// (remove) Token: 0x06016EBC RID: 93884
		event HTMLInputFileElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002C6A RID: 11370
		// (add) Token: 0x06016EBD RID: 93885
		// (remove) Token: 0x06016EBE RID: 93886
		event HTMLInputFileElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002C6B RID: 11371
		// (add) Token: 0x06016EBF RID: 93887
		// (remove) Token: 0x06016EC0 RID: 93888
		event HTMLInputFileElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002C6C RID: 11372
		// (add) Token: 0x06016EC1 RID: 93889
		// (remove) Token: 0x06016EC2 RID: 93890
		event HTMLInputFileElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002C6D RID: 11373
		// (add) Token: 0x06016EC3 RID: 93891
		// (remove) Token: 0x06016EC4 RID: 93892
		event HTMLInputFileElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002C6E RID: 11374
		// (add) Token: 0x06016EC5 RID: 93893
		// (remove) Token: 0x06016EC6 RID: 93894
		event HTMLInputFileElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002C6F RID: 11375
		// (add) Token: 0x06016EC7 RID: 93895
		// (remove) Token: 0x06016EC8 RID: 93896
		event HTMLInputFileElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002C70 RID: 11376
		// (add) Token: 0x06016EC9 RID: 93897
		// (remove) Token: 0x06016ECA RID: 93898
		event HTMLInputFileElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002C71 RID: 11377
		// (add) Token: 0x06016ECB RID: 93899
		// (remove) Token: 0x06016ECC RID: 93900
		event HTMLInputFileElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14002C72 RID: 11378
		// (add) Token: 0x06016ECD RID: 93901
		// (remove) Token: 0x06016ECE RID: 93902
		event HTMLInputFileElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002C73 RID: 11379
		// (add) Token: 0x06016ECF RID: 93903
		// (remove) Token: 0x06016ED0 RID: 93904
		event HTMLInputFileElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14002C74 RID: 11380
		// (add) Token: 0x06016ED1 RID: 93905
		// (remove) Token: 0x06016ED2 RID: 93906
		event HTMLInputFileElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14002C75 RID: 11381
		// (add) Token: 0x06016ED3 RID: 93907
		// (remove) Token: 0x06016ED4 RID: 93908
		event HTMLInputFileElementEvents_onchangeEventHandler onchange;

		// Token: 0x14002C76 RID: 11382
		// (add) Token: 0x06016ED5 RID: 93909
		// (remove) Token: 0x06016ED6 RID: 93910
		event HTMLInputFileElementEvents_onselectEventHandler onselect;

		// Token: 0x14002C77 RID: 11383
		// (add) Token: 0x06016ED7 RID: 93911
		// (remove) Token: 0x06016ED8 RID: 93912
		event HTMLInputFileElementEvents_onloadEventHandler onload;

		// Token: 0x14002C78 RID: 11384
		// (add) Token: 0x06016ED9 RID: 93913
		// (remove) Token: 0x06016EDA RID: 93914
		event HTMLInputFileElementEvents_onerrorEventHandler onerror;

		// Token: 0x14002C79 RID: 11385
		// (add) Token: 0x06016EDB RID: 93915
		// (remove) Token: 0x06016EDC RID: 93916
		event HTMLInputFileElementEvents_onabortEventHandler onabort;
	}
}

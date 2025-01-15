using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000A04 RID: 2564
	[ComEventInterface(typeof(HTMLTableEvents\u0000), typeof(HTMLTableEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLTableEvents_Event
	{
		// Token: 0x14001FF0 RID: 8176
		// (add) Token: 0x060108E9 RID: 67817
		// (remove) Token: 0x060108EA RID: 67818
		event HTMLTableEvents_onhelpEventHandler onhelp;

		// Token: 0x14001FF1 RID: 8177
		// (add) Token: 0x060108EB RID: 67819
		// (remove) Token: 0x060108EC RID: 67820
		event HTMLTableEvents_onclickEventHandler onclick;

		// Token: 0x14001FF2 RID: 8178
		// (add) Token: 0x060108ED RID: 67821
		// (remove) Token: 0x060108EE RID: 67822
		event HTMLTableEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14001FF3 RID: 8179
		// (add) Token: 0x060108EF RID: 67823
		// (remove) Token: 0x060108F0 RID: 67824
		event HTMLTableEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14001FF4 RID: 8180
		// (add) Token: 0x060108F1 RID: 67825
		// (remove) Token: 0x060108F2 RID: 67826
		event HTMLTableEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14001FF5 RID: 8181
		// (add) Token: 0x060108F3 RID: 67827
		// (remove) Token: 0x060108F4 RID: 67828
		event HTMLTableEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14001FF6 RID: 8182
		// (add) Token: 0x060108F5 RID: 67829
		// (remove) Token: 0x060108F6 RID: 67830
		event HTMLTableEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14001FF7 RID: 8183
		// (add) Token: 0x060108F7 RID: 67831
		// (remove) Token: 0x060108F8 RID: 67832
		event HTMLTableEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14001FF8 RID: 8184
		// (add) Token: 0x060108F9 RID: 67833
		// (remove) Token: 0x060108FA RID: 67834
		event HTMLTableEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14001FF9 RID: 8185
		// (add) Token: 0x060108FB RID: 67835
		// (remove) Token: 0x060108FC RID: 67836
		event HTMLTableEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14001FFA RID: 8186
		// (add) Token: 0x060108FD RID: 67837
		// (remove) Token: 0x060108FE RID: 67838
		event HTMLTableEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14001FFB RID: 8187
		// (add) Token: 0x060108FF RID: 67839
		// (remove) Token: 0x06010900 RID: 67840
		event HTMLTableEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14001FFC RID: 8188
		// (add) Token: 0x06010901 RID: 67841
		// (remove) Token: 0x06010902 RID: 67842
		event HTMLTableEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14001FFD RID: 8189
		// (add) Token: 0x06010903 RID: 67843
		// (remove) Token: 0x06010904 RID: 67844
		event HTMLTableEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14001FFE RID: 8190
		// (add) Token: 0x06010905 RID: 67845
		// (remove) Token: 0x06010906 RID: 67846
		event HTMLTableEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14001FFF RID: 8191
		// (add) Token: 0x06010907 RID: 67847
		// (remove) Token: 0x06010908 RID: 67848
		event HTMLTableEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14002000 RID: 8192
		// (add) Token: 0x06010909 RID: 67849
		// (remove) Token: 0x0601090A RID: 67850
		event HTMLTableEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14002001 RID: 8193
		// (add) Token: 0x0601090B RID: 67851
		// (remove) Token: 0x0601090C RID: 67852
		event HTMLTableEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14002002 RID: 8194
		// (add) Token: 0x0601090D RID: 67853
		// (remove) Token: 0x0601090E RID: 67854
		event HTMLTableEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14002003 RID: 8195
		// (add) Token: 0x0601090F RID: 67855
		// (remove) Token: 0x06010910 RID: 67856
		event HTMLTableEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002004 RID: 8196
		// (add) Token: 0x06010911 RID: 67857
		// (remove) Token: 0x06010912 RID: 67858
		event HTMLTableEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002005 RID: 8197
		// (add) Token: 0x06010913 RID: 67859
		// (remove) Token: 0x06010914 RID: 67860
		event HTMLTableEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14002006 RID: 8198
		// (add) Token: 0x06010915 RID: 67861
		// (remove) Token: 0x06010916 RID: 67862
		event HTMLTableEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14002007 RID: 8199
		// (add) Token: 0x06010917 RID: 67863
		// (remove) Token: 0x06010918 RID: 67864
		event HTMLTableEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14002008 RID: 8200
		// (add) Token: 0x06010919 RID: 67865
		// (remove) Token: 0x0601091A RID: 67866
		event HTMLTableEvents_onscrollEventHandler onscroll;

		// Token: 0x14002009 RID: 8201
		// (add) Token: 0x0601091B RID: 67867
		// (remove) Token: 0x0601091C RID: 67868
		event HTMLTableEvents_onfocusEventHandler onfocus;

		// Token: 0x1400200A RID: 8202
		// (add) Token: 0x0601091D RID: 67869
		// (remove) Token: 0x0601091E RID: 67870
		event HTMLTableEvents_onblurEventHandler onblur;

		// Token: 0x1400200B RID: 8203
		// (add) Token: 0x0601091F RID: 67871
		// (remove) Token: 0x06010920 RID: 67872
		event HTMLTableEvents_onresizeEventHandler onresize;

		// Token: 0x1400200C RID: 8204
		// (add) Token: 0x06010921 RID: 67873
		// (remove) Token: 0x06010922 RID: 67874
		event HTMLTableEvents_ondragEventHandler ondrag;

		// Token: 0x1400200D RID: 8205
		// (add) Token: 0x06010923 RID: 67875
		// (remove) Token: 0x06010924 RID: 67876
		event HTMLTableEvents_ondragendEventHandler ondragend;

		// Token: 0x1400200E RID: 8206
		// (add) Token: 0x06010925 RID: 67877
		// (remove) Token: 0x06010926 RID: 67878
		event HTMLTableEvents_ondragenterEventHandler ondragenter;

		// Token: 0x1400200F RID: 8207
		// (add) Token: 0x06010927 RID: 67879
		// (remove) Token: 0x06010928 RID: 67880
		event HTMLTableEvents_ondragoverEventHandler ondragover;

		// Token: 0x14002010 RID: 8208
		// (add) Token: 0x06010929 RID: 67881
		// (remove) Token: 0x0601092A RID: 67882
		event HTMLTableEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14002011 RID: 8209
		// (add) Token: 0x0601092B RID: 67883
		// (remove) Token: 0x0601092C RID: 67884
		event HTMLTableEvents_ondropEventHandler ondrop;

		// Token: 0x14002012 RID: 8210
		// (add) Token: 0x0601092D RID: 67885
		// (remove) Token: 0x0601092E RID: 67886
		event HTMLTableEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002013 RID: 8211
		// (add) Token: 0x0601092F RID: 67887
		// (remove) Token: 0x06010930 RID: 67888
		event HTMLTableEvents_oncutEventHandler oncut;

		// Token: 0x14002014 RID: 8212
		// (add) Token: 0x06010931 RID: 67889
		// (remove) Token: 0x06010932 RID: 67890
		event HTMLTableEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002015 RID: 8213
		// (add) Token: 0x06010933 RID: 67891
		// (remove) Token: 0x06010934 RID: 67892
		event HTMLTableEvents_oncopyEventHandler oncopy;

		// Token: 0x14002016 RID: 8214
		// (add) Token: 0x06010935 RID: 67893
		// (remove) Token: 0x06010936 RID: 67894
		event HTMLTableEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14002017 RID: 8215
		// (add) Token: 0x06010937 RID: 67895
		// (remove) Token: 0x06010938 RID: 67896
		event HTMLTableEvents_onpasteEventHandler onpaste;

		// Token: 0x14002018 RID: 8216
		// (add) Token: 0x06010939 RID: 67897
		// (remove) Token: 0x0601093A RID: 67898
		event HTMLTableEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14002019 RID: 8217
		// (add) Token: 0x0601093B RID: 67899
		// (remove) Token: 0x0601093C RID: 67900
		event HTMLTableEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400201A RID: 8218
		// (add) Token: 0x0601093D RID: 67901
		// (remove) Token: 0x0601093E RID: 67902
		event HTMLTableEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400201B RID: 8219
		// (add) Token: 0x0601093F RID: 67903
		// (remove) Token: 0x06010940 RID: 67904
		event HTMLTableEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x1400201C RID: 8220
		// (add) Token: 0x06010941 RID: 67905
		// (remove) Token: 0x06010942 RID: 67906
		event HTMLTableEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400201D RID: 8221
		// (add) Token: 0x06010943 RID: 67907
		// (remove) Token: 0x06010944 RID: 67908
		event HTMLTableEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x1400201E RID: 8222
		// (add) Token: 0x06010945 RID: 67909
		// (remove) Token: 0x06010946 RID: 67910
		event HTMLTableEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400201F RID: 8223
		// (add) Token: 0x06010947 RID: 67911
		// (remove) Token: 0x06010948 RID: 67912
		event HTMLTableEvents_onpageEventHandler onpage;

		// Token: 0x14002020 RID: 8224
		// (add) Token: 0x06010949 RID: 67913
		// (remove) Token: 0x0601094A RID: 67914
		event HTMLTableEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002021 RID: 8225
		// (add) Token: 0x0601094B RID: 67915
		// (remove) Token: 0x0601094C RID: 67916
		event HTMLTableEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002022 RID: 8226
		// (add) Token: 0x0601094D RID: 67917
		// (remove) Token: 0x0601094E RID: 67918
		event HTMLTableEvents_onmoveEventHandler onmove;

		// Token: 0x14002023 RID: 8227
		// (add) Token: 0x0601094F RID: 67919
		// (remove) Token: 0x06010950 RID: 67920
		event HTMLTableEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14002024 RID: 8228
		// (add) Token: 0x06010951 RID: 67921
		// (remove) Token: 0x06010952 RID: 67922
		event HTMLTableEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14002025 RID: 8229
		// (add) Token: 0x06010953 RID: 67923
		// (remove) Token: 0x06010954 RID: 67924
		event HTMLTableEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14002026 RID: 8230
		// (add) Token: 0x06010955 RID: 67925
		// (remove) Token: 0x06010956 RID: 67926
		event HTMLTableEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14002027 RID: 8231
		// (add) Token: 0x06010957 RID: 67927
		// (remove) Token: 0x06010958 RID: 67928
		event HTMLTableEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14002028 RID: 8232
		// (add) Token: 0x06010959 RID: 67929
		// (remove) Token: 0x0601095A RID: 67930
		event HTMLTableEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002029 RID: 8233
		// (add) Token: 0x0601095B RID: 67931
		// (remove) Token: 0x0601095C RID: 67932
		event HTMLTableEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x1400202A RID: 8234
		// (add) Token: 0x0601095D RID: 67933
		// (remove) Token: 0x0601095E RID: 67934
		event HTMLTableEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400202B RID: 8235
		// (add) Token: 0x0601095F RID: 67935
		// (remove) Token: 0x06010960 RID: 67936
		event HTMLTableEvents_onactivateEventHandler onactivate;

		// Token: 0x1400202C RID: 8236
		// (add) Token: 0x06010961 RID: 67937
		// (remove) Token: 0x06010962 RID: 67938
		event HTMLTableEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x1400202D RID: 8237
		// (add) Token: 0x06010963 RID: 67939
		// (remove) Token: 0x06010964 RID: 67940
		event HTMLTableEvents_onfocusinEventHandler onfocusin;

		// Token: 0x1400202E RID: 8238
		// (add) Token: 0x06010965 RID: 67941
		// (remove) Token: 0x06010966 RID: 67942
		event HTMLTableEvents_onfocusoutEventHandler onfocusout;
	}
}

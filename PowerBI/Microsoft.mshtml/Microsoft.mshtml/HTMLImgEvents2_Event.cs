using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020002C3 RID: 707
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLImgEvents2\u0000), typeof(HTMLImgEvents2_EventProvider\u0000))]
	public interface HTMLImgEvents2_Event
	{
		// Token: 0x14000441 RID: 1089
		// (add) Token: 0x06002E2D RID: 11821
		// (remove) Token: 0x06002E2E RID: 11822
		event HTMLImgEvents2_onhelpEventHandler onhelp;

		// Token: 0x14000442 RID: 1090
		// (add) Token: 0x06002E2F RID: 11823
		// (remove) Token: 0x06002E30 RID: 11824
		event HTMLImgEvents2_onclickEventHandler onclick;

		// Token: 0x14000443 RID: 1091
		// (add) Token: 0x06002E31 RID: 11825
		// (remove) Token: 0x06002E32 RID: 11826
		event HTMLImgEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14000444 RID: 1092
		// (add) Token: 0x06002E33 RID: 11827
		// (remove) Token: 0x06002E34 RID: 11828
		event HTMLImgEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14000445 RID: 1093
		// (add) Token: 0x06002E35 RID: 11829
		// (remove) Token: 0x06002E36 RID: 11830
		event HTMLImgEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14000446 RID: 1094
		// (add) Token: 0x06002E37 RID: 11831
		// (remove) Token: 0x06002E38 RID: 11832
		event HTMLImgEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x14000447 RID: 1095
		// (add) Token: 0x06002E39 RID: 11833
		// (remove) Token: 0x06002E3A RID: 11834
		event HTMLImgEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x14000448 RID: 1096
		// (add) Token: 0x06002E3B RID: 11835
		// (remove) Token: 0x06002E3C RID: 11836
		event HTMLImgEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x14000449 RID: 1097
		// (add) Token: 0x06002E3D RID: 11837
		// (remove) Token: 0x06002E3E RID: 11838
		event HTMLImgEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x1400044A RID: 1098
		// (add) Token: 0x06002E3F RID: 11839
		// (remove) Token: 0x06002E40 RID: 11840
		event HTMLImgEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x1400044B RID: 1099
		// (add) Token: 0x06002E41 RID: 11841
		// (remove) Token: 0x06002E42 RID: 11842
		event HTMLImgEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x1400044C RID: 1100
		// (add) Token: 0x06002E43 RID: 11843
		// (remove) Token: 0x06002E44 RID: 11844
		event HTMLImgEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x1400044D RID: 1101
		// (add) Token: 0x06002E45 RID: 11845
		// (remove) Token: 0x06002E46 RID: 11846
		event HTMLImgEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x1400044E RID: 1102
		// (add) Token: 0x06002E47 RID: 11847
		// (remove) Token: 0x06002E48 RID: 11848
		event HTMLImgEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x1400044F RID: 1103
		// (add) Token: 0x06002E49 RID: 11849
		// (remove) Token: 0x06002E4A RID: 11850
		event HTMLImgEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000450 RID: 1104
		// (add) Token: 0x06002E4B RID: 11851
		// (remove) Token: 0x06002E4C RID: 11852
		event HTMLImgEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000451 RID: 1105
		// (add) Token: 0x06002E4D RID: 11853
		// (remove) Token: 0x06002E4E RID: 11854
		event HTMLImgEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000452 RID: 1106
		// (add) Token: 0x06002E4F RID: 11855
		// (remove) Token: 0x06002E50 RID: 11856
		event HTMLImgEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14000453 RID: 1107
		// (add) Token: 0x06002E51 RID: 11857
		// (remove) Token: 0x06002E52 RID: 11858
		event HTMLImgEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14000454 RID: 1108
		// (add) Token: 0x06002E53 RID: 11859
		// (remove) Token: 0x06002E54 RID: 11860
		event HTMLImgEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000455 RID: 1109
		// (add) Token: 0x06002E55 RID: 11861
		// (remove) Token: 0x06002E56 RID: 11862
		event HTMLImgEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000456 RID: 1110
		// (add) Token: 0x06002E57 RID: 11863
		// (remove) Token: 0x06002E58 RID: 11864
		event HTMLImgEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000457 RID: 1111
		// (add) Token: 0x06002E59 RID: 11865
		// (remove) Token: 0x06002E5A RID: 11866
		event HTMLImgEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000458 RID: 1112
		// (add) Token: 0x06002E5B RID: 11867
		// (remove) Token: 0x06002E5C RID: 11868
		event HTMLImgEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000459 RID: 1113
		// (add) Token: 0x06002E5D RID: 11869
		// (remove) Token: 0x06002E5E RID: 11870
		event HTMLImgEvents2_onscrollEventHandler onscroll;

		// Token: 0x1400045A RID: 1114
		// (add) Token: 0x06002E5F RID: 11871
		// (remove) Token: 0x06002E60 RID: 11872
		event HTMLImgEvents2_onfocusEventHandler onfocus;

		// Token: 0x1400045B RID: 1115
		// (add) Token: 0x06002E61 RID: 11873
		// (remove) Token: 0x06002E62 RID: 11874
		event HTMLImgEvents2_onblurEventHandler onblur;

		// Token: 0x1400045C RID: 1116
		// (add) Token: 0x06002E63 RID: 11875
		// (remove) Token: 0x06002E64 RID: 11876
		event HTMLImgEvents2_onresizeEventHandler onresize;

		// Token: 0x1400045D RID: 1117
		// (add) Token: 0x06002E65 RID: 11877
		// (remove) Token: 0x06002E66 RID: 11878
		event HTMLImgEvents2_ondragEventHandler ondrag;

		// Token: 0x1400045E RID: 1118
		// (add) Token: 0x06002E67 RID: 11879
		// (remove) Token: 0x06002E68 RID: 11880
		event HTMLImgEvents2_ondragendEventHandler ondragend;

		// Token: 0x1400045F RID: 1119
		// (add) Token: 0x06002E69 RID: 11881
		// (remove) Token: 0x06002E6A RID: 11882
		event HTMLImgEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14000460 RID: 1120
		// (add) Token: 0x06002E6B RID: 11883
		// (remove) Token: 0x06002E6C RID: 11884
		event HTMLImgEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14000461 RID: 1121
		// (add) Token: 0x06002E6D RID: 11885
		// (remove) Token: 0x06002E6E RID: 11886
		event HTMLImgEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14000462 RID: 1122
		// (add) Token: 0x06002E6F RID: 11887
		// (remove) Token: 0x06002E70 RID: 11888
		event HTMLImgEvents2_ondropEventHandler ondrop;

		// Token: 0x14000463 RID: 1123
		// (add) Token: 0x06002E71 RID: 11889
		// (remove) Token: 0x06002E72 RID: 11890
		event HTMLImgEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000464 RID: 1124
		// (add) Token: 0x06002E73 RID: 11891
		// (remove) Token: 0x06002E74 RID: 11892
		event HTMLImgEvents2_oncutEventHandler oncut;

		// Token: 0x14000465 RID: 1125
		// (add) Token: 0x06002E75 RID: 11893
		// (remove) Token: 0x06002E76 RID: 11894
		event HTMLImgEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000466 RID: 1126
		// (add) Token: 0x06002E77 RID: 11895
		// (remove) Token: 0x06002E78 RID: 11896
		event HTMLImgEvents2_oncopyEventHandler oncopy;

		// Token: 0x14000467 RID: 1127
		// (add) Token: 0x06002E79 RID: 11897
		// (remove) Token: 0x06002E7A RID: 11898
		event HTMLImgEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14000468 RID: 1128
		// (add) Token: 0x06002E7B RID: 11899
		// (remove) Token: 0x06002E7C RID: 11900
		event HTMLImgEvents2_onpasteEventHandler onpaste;

		// Token: 0x14000469 RID: 1129
		// (add) Token: 0x06002E7D RID: 11901
		// (remove) Token: 0x06002E7E RID: 11902
		event HTMLImgEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400046A RID: 1130
		// (add) Token: 0x06002E7F RID: 11903
		// (remove) Token: 0x06002E80 RID: 11904
		event HTMLImgEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400046B RID: 1131
		// (add) Token: 0x06002E81 RID: 11905
		// (remove) Token: 0x06002E82 RID: 11906
		event HTMLImgEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400046C RID: 1132
		// (add) Token: 0x06002E83 RID: 11907
		// (remove) Token: 0x06002E84 RID: 11908
		event HTMLImgEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x1400046D RID: 1133
		// (add) Token: 0x06002E85 RID: 11909
		// (remove) Token: 0x06002E86 RID: 11910
		event HTMLImgEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x1400046E RID: 1134
		// (add) Token: 0x06002E87 RID: 11911
		// (remove) Token: 0x06002E88 RID: 11912
		event HTMLImgEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x1400046F RID: 1135
		// (add) Token: 0x06002E89 RID: 11913
		// (remove) Token: 0x06002E8A RID: 11914
		event HTMLImgEvents2_onpageEventHandler onpage;

		// Token: 0x14000470 RID: 1136
		// (add) Token: 0x06002E8B RID: 11915
		// (remove) Token: 0x06002E8C RID: 11916
		event HTMLImgEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14000471 RID: 1137
		// (add) Token: 0x06002E8D RID: 11917
		// (remove) Token: 0x06002E8E RID: 11918
		event HTMLImgEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14000472 RID: 1138
		// (add) Token: 0x06002E8F RID: 11919
		// (remove) Token: 0x06002E90 RID: 11920
		event HTMLImgEvents2_onactivateEventHandler onactivate;

		// Token: 0x14000473 RID: 1139
		// (add) Token: 0x06002E91 RID: 11921
		// (remove) Token: 0x06002E92 RID: 11922
		event HTMLImgEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14000474 RID: 1140
		// (add) Token: 0x06002E93 RID: 11923
		// (remove) Token: 0x06002E94 RID: 11924
		event HTMLImgEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000475 RID: 1141
		// (add) Token: 0x06002E95 RID: 11925
		// (remove) Token: 0x06002E96 RID: 11926
		event HTMLImgEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000476 RID: 1142
		// (add) Token: 0x06002E97 RID: 11927
		// (remove) Token: 0x06002E98 RID: 11928
		event HTMLImgEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x14000477 RID: 1143
		// (add) Token: 0x06002E99 RID: 11929
		// (remove) Token: 0x06002E9A RID: 11930
		event HTMLImgEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x14000478 RID: 1144
		// (add) Token: 0x06002E9B RID: 11931
		// (remove) Token: 0x06002E9C RID: 11932
		event HTMLImgEvents2_onmoveEventHandler onmove;

		// Token: 0x14000479 RID: 1145
		// (add) Token: 0x06002E9D RID: 11933
		// (remove) Token: 0x06002E9E RID: 11934
		event HTMLImgEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x1400047A RID: 1146
		// (add) Token: 0x06002E9F RID: 11935
		// (remove) Token: 0x06002EA0 RID: 11936
		event HTMLImgEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x1400047B RID: 1147
		// (add) Token: 0x06002EA1 RID: 11937
		// (remove) Token: 0x06002EA2 RID: 11938
		event HTMLImgEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x1400047C RID: 1148
		// (add) Token: 0x06002EA3 RID: 11939
		// (remove) Token: 0x06002EA4 RID: 11940
		event HTMLImgEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x1400047D RID: 1149
		// (add) Token: 0x06002EA5 RID: 11941
		// (remove) Token: 0x06002EA6 RID: 11942
		event HTMLImgEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x1400047E RID: 1150
		// (add) Token: 0x06002EA7 RID: 11943
		// (remove) Token: 0x06002EA8 RID: 11944
		event HTMLImgEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x1400047F RID: 1151
		// (add) Token: 0x06002EA9 RID: 11945
		// (remove) Token: 0x06002EAA RID: 11946
		event HTMLImgEvents2_onloadEventHandler onload;

		// Token: 0x14000480 RID: 1152
		// (add) Token: 0x06002EAB RID: 11947
		// (remove) Token: 0x06002EAC RID: 11948
		event HTMLImgEvents2_onerrorEventHandler onerror;

		// Token: 0x14000481 RID: 1153
		// (add) Token: 0x06002EAD RID: 11949
		// (remove) Token: 0x06002EAE RID: 11950
		event HTMLImgEvents2_onabortEventHandler onabort;
	}
}

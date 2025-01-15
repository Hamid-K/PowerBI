using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020004E4 RID: 1252
	[ComEventInterface(typeof(HTMLSelectElementEvents\u0000), typeof(HTMLSelectElementEvents_EventProvider\u0000))]
	[ComVisible(false)]
	public interface HTMLSelectElementEvents_Event
	{
		// Token: 0x14000F4E RID: 3918
		// (add) Token: 0x060083FF RID: 33791
		// (remove) Token: 0x06008400 RID: 33792
		event HTMLSelectElementEvents_onhelpEventHandler onhelp;

		// Token: 0x14000F4F RID: 3919
		// (add) Token: 0x06008401 RID: 33793
		// (remove) Token: 0x06008402 RID: 33794
		event HTMLSelectElementEvents_onclickEventHandler onclick;

		// Token: 0x14000F50 RID: 3920
		// (add) Token: 0x06008403 RID: 33795
		// (remove) Token: 0x06008404 RID: 33796
		event HTMLSelectElementEvents_ondblclickEventHandler ondblclick;

		// Token: 0x14000F51 RID: 3921
		// (add) Token: 0x06008405 RID: 33797
		// (remove) Token: 0x06008406 RID: 33798
		event HTMLSelectElementEvents_onkeypressEventHandler onkeypress;

		// Token: 0x14000F52 RID: 3922
		// (add) Token: 0x06008407 RID: 33799
		// (remove) Token: 0x06008408 RID: 33800
		event HTMLSelectElementEvents_onkeydownEventHandler onkeydown;

		// Token: 0x14000F53 RID: 3923
		// (add) Token: 0x06008409 RID: 33801
		// (remove) Token: 0x0600840A RID: 33802
		event HTMLSelectElementEvents_onkeyupEventHandler onkeyup;

		// Token: 0x14000F54 RID: 3924
		// (add) Token: 0x0600840B RID: 33803
		// (remove) Token: 0x0600840C RID: 33804
		event HTMLSelectElementEvents_onmouseoutEventHandler onmouseout;

		// Token: 0x14000F55 RID: 3925
		// (add) Token: 0x0600840D RID: 33805
		// (remove) Token: 0x0600840E RID: 33806
		event HTMLSelectElementEvents_onmouseoverEventHandler onmouseover;

		// Token: 0x14000F56 RID: 3926
		// (add) Token: 0x0600840F RID: 33807
		// (remove) Token: 0x06008410 RID: 33808
		event HTMLSelectElementEvents_onmousemoveEventHandler onmousemove;

		// Token: 0x14000F57 RID: 3927
		// (add) Token: 0x06008411 RID: 33809
		// (remove) Token: 0x06008412 RID: 33810
		event HTMLSelectElementEvents_onmousedownEventHandler onmousedown;

		// Token: 0x14000F58 RID: 3928
		// (add) Token: 0x06008413 RID: 33811
		// (remove) Token: 0x06008414 RID: 33812
		event HTMLSelectElementEvents_onmouseupEventHandler onmouseup;

		// Token: 0x14000F59 RID: 3929
		// (add) Token: 0x06008415 RID: 33813
		// (remove) Token: 0x06008416 RID: 33814
		event HTMLSelectElementEvents_onselectstartEventHandler onselectstart;

		// Token: 0x14000F5A RID: 3930
		// (add) Token: 0x06008417 RID: 33815
		// (remove) Token: 0x06008418 RID: 33816
		event HTMLSelectElementEvents_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14000F5B RID: 3931
		// (add) Token: 0x06008419 RID: 33817
		// (remove) Token: 0x0600841A RID: 33818
		event HTMLSelectElementEvents_ondragstartEventHandler ondragstart;

		// Token: 0x14000F5C RID: 3932
		// (add) Token: 0x0600841B RID: 33819
		// (remove) Token: 0x0600841C RID: 33820
		event HTMLSelectElementEvents_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14000F5D RID: 3933
		// (add) Token: 0x0600841D RID: 33821
		// (remove) Token: 0x0600841E RID: 33822
		event HTMLSelectElementEvents_onafterupdateEventHandler onafterupdate;

		// Token: 0x14000F5E RID: 3934
		// (add) Token: 0x0600841F RID: 33823
		// (remove) Token: 0x06008420 RID: 33824
		event HTMLSelectElementEvents_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14000F5F RID: 3935
		// (add) Token: 0x06008421 RID: 33825
		// (remove) Token: 0x06008422 RID: 33826
		event HTMLSelectElementEvents_onrowexitEventHandler onrowexit;

		// Token: 0x14000F60 RID: 3936
		// (add) Token: 0x06008423 RID: 33827
		// (remove) Token: 0x06008424 RID: 33828
		event HTMLSelectElementEvents_onrowenterEventHandler onrowenter;

		// Token: 0x14000F61 RID: 3937
		// (add) Token: 0x06008425 RID: 33829
		// (remove) Token: 0x06008426 RID: 33830
		event HTMLSelectElementEvents_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14000F62 RID: 3938
		// (add) Token: 0x06008427 RID: 33831
		// (remove) Token: 0x06008428 RID: 33832
		event HTMLSelectElementEvents_ondataavailableEventHandler ondataavailable;

		// Token: 0x14000F63 RID: 3939
		// (add) Token: 0x06008429 RID: 33833
		// (remove) Token: 0x0600842A RID: 33834
		event HTMLSelectElementEvents_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x14000F64 RID: 3940
		// (add) Token: 0x0600842B RID: 33835
		// (remove) Token: 0x0600842C RID: 33836
		event HTMLSelectElementEvents_onlosecaptureEventHandler onlosecapture;

		// Token: 0x14000F65 RID: 3941
		// (add) Token: 0x0600842D RID: 33837
		// (remove) Token: 0x0600842E RID: 33838
		event HTMLSelectElementEvents_onpropertychangeEventHandler onpropertychange;

		// Token: 0x14000F66 RID: 3942
		// (add) Token: 0x0600842F RID: 33839
		// (remove) Token: 0x06008430 RID: 33840
		event HTMLSelectElementEvents_onscrollEventHandler onscroll;

		// Token: 0x14000F67 RID: 3943
		// (add) Token: 0x06008431 RID: 33841
		// (remove) Token: 0x06008432 RID: 33842
		event HTMLSelectElementEvents_onfocusEventHandler onfocus;

		// Token: 0x14000F68 RID: 3944
		// (add) Token: 0x06008433 RID: 33843
		// (remove) Token: 0x06008434 RID: 33844
		event HTMLSelectElementEvents_onblurEventHandler onblur;

		// Token: 0x14000F69 RID: 3945
		// (add) Token: 0x06008435 RID: 33845
		// (remove) Token: 0x06008436 RID: 33846
		event HTMLSelectElementEvents_onresizeEventHandler onresize;

		// Token: 0x14000F6A RID: 3946
		// (add) Token: 0x06008437 RID: 33847
		// (remove) Token: 0x06008438 RID: 33848
		event HTMLSelectElementEvents_ondragEventHandler ondrag;

		// Token: 0x14000F6B RID: 3947
		// (add) Token: 0x06008439 RID: 33849
		// (remove) Token: 0x0600843A RID: 33850
		event HTMLSelectElementEvents_ondragendEventHandler ondragend;

		// Token: 0x14000F6C RID: 3948
		// (add) Token: 0x0600843B RID: 33851
		// (remove) Token: 0x0600843C RID: 33852
		event HTMLSelectElementEvents_ondragenterEventHandler ondragenter;

		// Token: 0x14000F6D RID: 3949
		// (add) Token: 0x0600843D RID: 33853
		// (remove) Token: 0x0600843E RID: 33854
		event HTMLSelectElementEvents_ondragoverEventHandler ondragover;

		// Token: 0x14000F6E RID: 3950
		// (add) Token: 0x0600843F RID: 33855
		// (remove) Token: 0x06008440 RID: 33856
		event HTMLSelectElementEvents_ondragleaveEventHandler ondragleave;

		// Token: 0x14000F6F RID: 3951
		// (add) Token: 0x06008441 RID: 33857
		// (remove) Token: 0x06008442 RID: 33858
		event HTMLSelectElementEvents_ondropEventHandler ondrop;

		// Token: 0x14000F70 RID: 3952
		// (add) Token: 0x06008443 RID: 33859
		// (remove) Token: 0x06008444 RID: 33860
		event HTMLSelectElementEvents_onbeforecutEventHandler onbeforecut;

		// Token: 0x14000F71 RID: 3953
		// (add) Token: 0x06008445 RID: 33861
		// (remove) Token: 0x06008446 RID: 33862
		event HTMLSelectElementEvents_oncutEventHandler oncut;

		// Token: 0x14000F72 RID: 3954
		// (add) Token: 0x06008447 RID: 33863
		// (remove) Token: 0x06008448 RID: 33864
		event HTMLSelectElementEvents_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14000F73 RID: 3955
		// (add) Token: 0x06008449 RID: 33865
		// (remove) Token: 0x0600844A RID: 33866
		event HTMLSelectElementEvents_oncopyEventHandler oncopy;

		// Token: 0x14000F74 RID: 3956
		// (add) Token: 0x0600844B RID: 33867
		// (remove) Token: 0x0600844C RID: 33868
		event HTMLSelectElementEvents_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x14000F75 RID: 3957
		// (add) Token: 0x0600844D RID: 33869
		// (remove) Token: 0x0600844E RID: 33870
		event HTMLSelectElementEvents_onpasteEventHandler onpaste;

		// Token: 0x14000F76 RID: 3958
		// (add) Token: 0x0600844F RID: 33871
		// (remove) Token: 0x06008450 RID: 33872
		event HTMLSelectElementEvents_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x14000F77 RID: 3959
		// (add) Token: 0x06008451 RID: 33873
		// (remove) Token: 0x06008452 RID: 33874
		event HTMLSelectElementEvents_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x14000F78 RID: 3960
		// (add) Token: 0x06008453 RID: 33875
		// (remove) Token: 0x06008454 RID: 33876
		event HTMLSelectElementEvents_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x14000F79 RID: 3961
		// (add) Token: 0x06008455 RID: 33877
		// (remove) Token: 0x06008456 RID: 33878
		event HTMLSelectElementEvents_oncellchangeEventHandler oncellchange;

		// Token: 0x14000F7A RID: 3962
		// (add) Token: 0x06008457 RID: 33879
		// (remove) Token: 0x06008458 RID: 33880
		event HTMLSelectElementEvents_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14000F7B RID: 3963
		// (add) Token: 0x06008459 RID: 33881
		// (remove) Token: 0x0600845A RID: 33882
		event HTMLSelectElementEvents_onbeforeeditfocusEventHandler onbeforeeditfocus;

		// Token: 0x14000F7C RID: 3964
		// (add) Token: 0x0600845B RID: 33883
		// (remove) Token: 0x0600845C RID: 33884
		event HTMLSelectElementEvents_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14000F7D RID: 3965
		// (add) Token: 0x0600845D RID: 33885
		// (remove) Token: 0x0600845E RID: 33886
		event HTMLSelectElementEvents_onpageEventHandler onpage;

		// Token: 0x14000F7E RID: 3966
		// (add) Token: 0x0600845F RID: 33887
		// (remove) Token: 0x06008460 RID: 33888
		event HTMLSelectElementEvents_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14000F7F RID: 3967
		// (add) Token: 0x06008461 RID: 33889
		// (remove) Token: 0x06008462 RID: 33890
		event HTMLSelectElementEvents_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14000F80 RID: 3968
		// (add) Token: 0x06008463 RID: 33891
		// (remove) Token: 0x06008464 RID: 33892
		event HTMLSelectElementEvents_onmoveEventHandler onmove;

		// Token: 0x14000F81 RID: 3969
		// (add) Token: 0x06008465 RID: 33893
		// (remove) Token: 0x06008466 RID: 33894
		event HTMLSelectElementEvents_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x14000F82 RID: 3970
		// (add) Token: 0x06008467 RID: 33895
		// (remove) Token: 0x06008468 RID: 33896
		event HTMLSelectElementEvents_onmovestartEventHandler onmovestart;

		// Token: 0x14000F83 RID: 3971
		// (add) Token: 0x06008469 RID: 33897
		// (remove) Token: 0x0600846A RID: 33898
		event HTMLSelectElementEvents_onmoveendEventHandler onmoveend;

		// Token: 0x14000F84 RID: 3972
		// (add) Token: 0x0600846B RID: 33899
		// (remove) Token: 0x0600846C RID: 33900
		event HTMLSelectElementEvents_onresizestartEventHandler onresizestart;

		// Token: 0x14000F85 RID: 3973
		// (add) Token: 0x0600846D RID: 33901
		// (remove) Token: 0x0600846E RID: 33902
		event HTMLSelectElementEvents_onresizeendEventHandler onresizeend;

		// Token: 0x14000F86 RID: 3974
		// (add) Token: 0x0600846F RID: 33903
		// (remove) Token: 0x06008470 RID: 33904
		event HTMLSelectElementEvents_onmouseenterEventHandler onmouseenter;

		// Token: 0x14000F87 RID: 3975
		// (add) Token: 0x06008471 RID: 33905
		// (remove) Token: 0x06008472 RID: 33906
		event HTMLSelectElementEvents_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14000F88 RID: 3976
		// (add) Token: 0x06008473 RID: 33907
		// (remove) Token: 0x06008474 RID: 33908
		event HTMLSelectElementEvents_onmousewheelEventHandler onmousewheel;

		// Token: 0x14000F89 RID: 3977
		// (add) Token: 0x06008475 RID: 33909
		// (remove) Token: 0x06008476 RID: 33910
		event HTMLSelectElementEvents_onactivateEventHandler onactivate;

		// Token: 0x14000F8A RID: 3978
		// (add) Token: 0x06008477 RID: 33911
		// (remove) Token: 0x06008478 RID: 33912
		event HTMLSelectElementEvents_ondeactivateEventHandler ondeactivate;

		// Token: 0x14000F8B RID: 3979
		// (add) Token: 0x06008479 RID: 33913
		// (remove) Token: 0x0600847A RID: 33914
		event HTMLSelectElementEvents_onfocusinEventHandler onfocusin;

		// Token: 0x14000F8C RID: 3980
		// (add) Token: 0x0600847B RID: 33915
		// (remove) Token: 0x0600847C RID: 33916
		event HTMLSelectElementEvents_onfocusoutEventHandler onfocusout;

		// Token: 0x14000F8D RID: 3981
		// (add) Token: 0x0600847D RID: 33917
		// (remove) Token: 0x0600847E RID: 33918
		event HTMLSelectElementEvents_onchangeEventHandler onchange;
	}
}

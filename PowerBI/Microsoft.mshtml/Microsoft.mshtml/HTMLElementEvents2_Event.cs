using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020000FA RID: 250
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLElementEvents2\u0000), typeof(HTMLElementEvents2_EventProvider\u0000))]
	public interface HTMLElementEvents2_Event
	{
		// Token: 0x140000BD RID: 189
		// (add) Token: 0x060012BD RID: 4797
		// (remove) Token: 0x060012BE RID: 4798
		event HTMLElementEvents2_onhelpEventHandler onhelp;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x060012BF RID: 4799
		// (remove) Token: 0x060012C0 RID: 4800
		event HTMLElementEvents2_onclickEventHandler onclick;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x060012C1 RID: 4801
		// (remove) Token: 0x060012C2 RID: 4802
		event HTMLElementEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x060012C3 RID: 4803
		// (remove) Token: 0x060012C4 RID: 4804
		event HTMLElementEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x060012C5 RID: 4805
		// (remove) Token: 0x060012C6 RID: 4806
		event HTMLElementEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060012C7 RID: 4807
		// (remove) Token: 0x060012C8 RID: 4808
		event HTMLElementEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060012C9 RID: 4809
		// (remove) Token: 0x060012CA RID: 4810
		event HTMLElementEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060012CB RID: 4811
		// (remove) Token: 0x060012CC RID: 4812
		event HTMLElementEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x060012CD RID: 4813
		// (remove) Token: 0x060012CE RID: 4814
		event HTMLElementEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x060012CF RID: 4815
		// (remove) Token: 0x060012D0 RID: 4816
		event HTMLElementEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x060012D1 RID: 4817
		// (remove) Token: 0x060012D2 RID: 4818
		event HTMLElementEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x060012D3 RID: 4819
		// (remove) Token: 0x060012D4 RID: 4820
		event HTMLElementEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x060012D5 RID: 4821
		// (remove) Token: 0x060012D6 RID: 4822
		event HTMLElementEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x060012D7 RID: 4823
		// (remove) Token: 0x060012D8 RID: 4824
		event HTMLElementEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x060012D9 RID: 4825
		// (remove) Token: 0x060012DA RID: 4826
		event HTMLElementEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x060012DB RID: 4827
		// (remove) Token: 0x060012DC RID: 4828
		event HTMLElementEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x060012DD RID: 4829
		// (remove) Token: 0x060012DE RID: 4830
		event HTMLElementEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060012DF RID: 4831
		// (remove) Token: 0x060012E0 RID: 4832
		event HTMLElementEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x060012E1 RID: 4833
		// (remove) Token: 0x060012E2 RID: 4834
		event HTMLElementEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x060012E3 RID: 4835
		// (remove) Token: 0x060012E4 RID: 4836
		event HTMLElementEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x060012E5 RID: 4837
		// (remove) Token: 0x060012E6 RID: 4838
		event HTMLElementEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x060012E7 RID: 4839
		// (remove) Token: 0x060012E8 RID: 4840
		event HTMLElementEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x060012E9 RID: 4841
		// (remove) Token: 0x060012EA RID: 4842
		event HTMLElementEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x060012EB RID: 4843
		// (remove) Token: 0x060012EC RID: 4844
		event HTMLElementEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x060012ED RID: 4845
		// (remove) Token: 0x060012EE RID: 4846
		event HTMLElementEvents2_onscrollEventHandler onscroll;

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x060012EF RID: 4847
		// (remove) Token: 0x060012F0 RID: 4848
		event HTMLElementEvents2_onfocusEventHandler onfocus;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x060012F1 RID: 4849
		// (remove) Token: 0x060012F2 RID: 4850
		event HTMLElementEvents2_onblurEventHandler onblur;

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x060012F3 RID: 4851
		// (remove) Token: 0x060012F4 RID: 4852
		event HTMLElementEvents2_onresizeEventHandler onresize;

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x060012F5 RID: 4853
		// (remove) Token: 0x060012F6 RID: 4854
		event HTMLElementEvents2_ondragEventHandler ondrag;

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x060012F7 RID: 4855
		// (remove) Token: 0x060012F8 RID: 4856
		event HTMLElementEvents2_ondragendEventHandler ondragend;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x060012F9 RID: 4857
		// (remove) Token: 0x060012FA RID: 4858
		event HTMLElementEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x060012FB RID: 4859
		// (remove) Token: 0x060012FC RID: 4860
		event HTMLElementEvents2_ondragoverEventHandler ondragover;

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x060012FD RID: 4861
		// (remove) Token: 0x060012FE RID: 4862
		event HTMLElementEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x060012FF RID: 4863
		// (remove) Token: 0x06001300 RID: 4864
		event HTMLElementEvents2_ondropEventHandler ondrop;

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x06001301 RID: 4865
		// (remove) Token: 0x06001302 RID: 4866
		event HTMLElementEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x06001303 RID: 4867
		// (remove) Token: 0x06001304 RID: 4868
		event HTMLElementEvents2_oncutEventHandler oncut;

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x06001305 RID: 4869
		// (remove) Token: 0x06001306 RID: 4870
		event HTMLElementEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x06001307 RID: 4871
		// (remove) Token: 0x06001308 RID: 4872
		event HTMLElementEvents2_oncopyEventHandler oncopy;

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x06001309 RID: 4873
		// (remove) Token: 0x0600130A RID: 4874
		event HTMLElementEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x0600130B RID: 4875
		// (remove) Token: 0x0600130C RID: 4876
		event HTMLElementEvents2_onpasteEventHandler onpaste;

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x0600130D RID: 4877
		// (remove) Token: 0x0600130E RID: 4878
		event HTMLElementEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x0600130F RID: 4879
		// (remove) Token: 0x06001310 RID: 4880
		event HTMLElementEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06001311 RID: 4881
		// (remove) Token: 0x06001312 RID: 4882
		event HTMLElementEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001313 RID: 4883
		// (remove) Token: 0x06001314 RID: 4884
		event HTMLElementEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06001315 RID: 4885
		// (remove) Token: 0x06001316 RID: 4886
		event HTMLElementEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x140000EA RID: 234
		// (add) Token: 0x06001317 RID: 4887
		// (remove) Token: 0x06001318 RID: 4888
		event HTMLElementEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x140000EB RID: 235
		// (add) Token: 0x06001319 RID: 4889
		// (remove) Token: 0x0600131A RID: 4890
		event HTMLElementEvents2_onpageEventHandler onpage;

		// Token: 0x140000EC RID: 236
		// (add) Token: 0x0600131B RID: 4891
		// (remove) Token: 0x0600131C RID: 4892
		event HTMLElementEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x140000ED RID: 237
		// (add) Token: 0x0600131D RID: 4893
		// (remove) Token: 0x0600131E RID: 4894
		event HTMLElementEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x140000EE RID: 238
		// (add) Token: 0x0600131F RID: 4895
		// (remove) Token: 0x06001320 RID: 4896
		event HTMLElementEvents2_onactivateEventHandler onactivate;

		// Token: 0x140000EF RID: 239
		// (add) Token: 0x06001321 RID: 4897
		// (remove) Token: 0x06001322 RID: 4898
		event HTMLElementEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x140000F0 RID: 240
		// (add) Token: 0x06001323 RID: 4899
		// (remove) Token: 0x06001324 RID: 4900
		event HTMLElementEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x140000F1 RID: 241
		// (add) Token: 0x06001325 RID: 4901
		// (remove) Token: 0x06001326 RID: 4902
		event HTMLElementEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x140000F2 RID: 242
		// (add) Token: 0x06001327 RID: 4903
		// (remove) Token: 0x06001328 RID: 4904
		event HTMLElementEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x140000F3 RID: 243
		// (add) Token: 0x06001329 RID: 4905
		// (remove) Token: 0x0600132A RID: 4906
		event HTMLElementEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x140000F4 RID: 244
		// (add) Token: 0x0600132B RID: 4907
		// (remove) Token: 0x0600132C RID: 4908
		event HTMLElementEvents2_onmoveEventHandler onmove;

		// Token: 0x140000F5 RID: 245
		// (add) Token: 0x0600132D RID: 4909
		// (remove) Token: 0x0600132E RID: 4910
		event HTMLElementEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x140000F6 RID: 246
		// (add) Token: 0x0600132F RID: 4911
		// (remove) Token: 0x06001330 RID: 4912
		event HTMLElementEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x140000F7 RID: 247
		// (add) Token: 0x06001331 RID: 4913
		// (remove) Token: 0x06001332 RID: 4914
		event HTMLElementEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x06001333 RID: 4915
		// (remove) Token: 0x06001334 RID: 4916
		event HTMLElementEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06001335 RID: 4917
		// (remove) Token: 0x06001336 RID: 4918
		event HTMLElementEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x06001337 RID: 4919
		// (remove) Token: 0x06001338 RID: 4920
		event HTMLElementEvents2_onmousewheelEventHandler onmousewheel;
	}
}

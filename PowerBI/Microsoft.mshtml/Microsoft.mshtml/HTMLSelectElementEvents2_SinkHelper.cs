using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DC6 RID: 3526
	[ClassInterface(0)]
	internal sealed class HTMLSelectElementEvents2_SinkHelper : HTMLSelectElementEvents2
	{
		// Token: 0x06017737 RID: 96055 RVA: 0x00017F9C File Offset: 0x00016F9C
		public override void onchange(IHTMLEventObj A_1)
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017738 RID: 96056 RVA: 0x00017FCC File Offset: 0x00016FCC
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017739 RID: 96057 RVA: 0x00017FFC File Offset: 0x00016FFC
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601773A RID: 96058 RVA: 0x0001802C File Offset: 0x0001702C
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x0601773B RID: 96059 RVA: 0x0001805C File Offset: 0x0001705C
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601773C RID: 96060 RVA: 0x0001808C File Offset: 0x0001708C
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x0601773D RID: 96061 RVA: 0x000180BC File Offset: 0x000170BC
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x0601773E RID: 96062 RVA: 0x000180EC File Offset: 0x000170EC
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601773F RID: 96063 RVA: 0x0001811C File Offset: 0x0001711C
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017740 RID: 96064 RVA: 0x0001814C File Offset: 0x0001714C
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017741 RID: 96065 RVA: 0x0001817C File Offset: 0x0001717C
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017742 RID: 96066 RVA: 0x000181AC File Offset: 0x000171AC
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06017743 RID: 96067 RVA: 0x000181DC File Offset: 0x000171DC
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017744 RID: 96068 RVA: 0x0001820C File Offset: 0x0001720C
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017745 RID: 96069 RVA: 0x0001823C File Offset: 0x0001723C
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017746 RID: 96070 RVA: 0x0001826C File Offset: 0x0001726C
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017747 RID: 96071 RVA: 0x0001829C File Offset: 0x0001729C
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017748 RID: 96072 RVA: 0x000182CC File Offset: 0x000172CC
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017749 RID: 96073 RVA: 0x000182FC File Offset: 0x000172FC
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601774A RID: 96074 RVA: 0x0001832C File Offset: 0x0001732C
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601774B RID: 96075 RVA: 0x0001835C File Offset: 0x0001735C
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601774C RID: 96076 RVA: 0x0001838C File Offset: 0x0001738C
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601774D RID: 96077 RVA: 0x000183BC File Offset: 0x000173BC
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x0601774E RID: 96078 RVA: 0x000183EC File Offset: 0x000173EC
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x0601774F RID: 96079 RVA: 0x0001841C File Offset: 0x0001741C
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06017750 RID: 96080 RVA: 0x0001844C File Offset: 0x0001744C
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06017751 RID: 96081 RVA: 0x0001847C File Offset: 0x0001747C
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06017752 RID: 96082 RVA: 0x000184AC File Offset: 0x000174AC
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06017753 RID: 96083 RVA: 0x000184DC File Offset: 0x000174DC
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06017754 RID: 96084 RVA: 0x0001850C File Offset: 0x0001750C
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06017755 RID: 96085 RVA: 0x0001853C File Offset: 0x0001753C
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017756 RID: 96086 RVA: 0x0001856C File Offset: 0x0001756C
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06017757 RID: 96087 RVA: 0x0001859C File Offset: 0x0001759C
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06017758 RID: 96088 RVA: 0x000185CC File Offset: 0x000175CC
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017759 RID: 96089 RVA: 0x000185FC File Offset: 0x000175FC
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x0601775A RID: 96090 RVA: 0x0001862C File Offset: 0x0001762C
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601775B RID: 96091 RVA: 0x0001865C File Offset: 0x0001765C
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601775C RID: 96092 RVA: 0x0001868C File Offset: 0x0001768C
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601775D RID: 96093 RVA: 0x000186BC File Offset: 0x000176BC
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601775E RID: 96094 RVA: 0x000186EC File Offset: 0x000176EC
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601775F RID: 96095 RVA: 0x0001871C File Offset: 0x0001771C
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017760 RID: 96096 RVA: 0x0001874C File Offset: 0x0001774C
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017761 RID: 96097 RVA: 0x0001877C File Offset: 0x0001777C
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017762 RID: 96098 RVA: 0x000187AC File Offset: 0x000177AC
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017763 RID: 96099 RVA: 0x000187DC File Offset: 0x000177DC
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017764 RID: 96100 RVA: 0x0001880C File Offset: 0x0001780C
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017765 RID: 96101 RVA: 0x0001883C File Offset: 0x0001783C
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017766 RID: 96102 RVA: 0x0001886C File Offset: 0x0001786C
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017767 RID: 96103 RVA: 0x0001889C File Offset: 0x0001789C
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017768 RID: 96104 RVA: 0x000188CC File Offset: 0x000178CC
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06017769 RID: 96105 RVA: 0x000188FC File Offset: 0x000178FC
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601776A RID: 96106 RVA: 0x0001892C File Offset: 0x0001792C
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x0601776B RID: 96107 RVA: 0x0001895C File Offset: 0x0001795C
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601776C RID: 96108 RVA: 0x0001898C File Offset: 0x0001798C
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601776D RID: 96109 RVA: 0x000189BC File Offset: 0x000179BC
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601776E RID: 96110 RVA: 0x000189EC File Offset: 0x000179EC
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x0601776F RID: 96111 RVA: 0x00018A1C File Offset: 0x00017A1C
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017770 RID: 96112 RVA: 0x00018A4C File Offset: 0x00017A4C
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017771 RID: 96113 RVA: 0x00018A7C File Offset: 0x00017A7C
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017772 RID: 96114 RVA: 0x00018AAC File Offset: 0x00017AAC
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017773 RID: 96115 RVA: 0x00018ADC File Offset: 0x00017ADC
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06017774 RID: 96116 RVA: 0x00018B0C File Offset: 0x00017B0C
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06017775 RID: 96117 RVA: 0x00018B3C File Offset: 0x00017B3C
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06017776 RID: 96118 RVA: 0x00018B6C File Offset: 0x00017B6C
		internal HTMLSelectElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onchangeDelegate = null;
			this.m_onmousewheelDelegate = null;
			this.m_onresizeendDelegate = null;
			this.m_onresizestartDelegate = null;
			this.m_onmoveendDelegate = null;
			this.m_onmovestartDelegate = null;
			this.m_oncontrolselectDelegate = null;
			this.m_onmoveDelegate = null;
			this.m_onfocusoutDelegate = null;
			this.m_onfocusinDelegate = null;
			this.m_onbeforeactivateDelegate = null;
			this.m_onbeforedeactivateDelegate = null;
			this.m_ondeactivateDelegate = null;
			this.m_onactivateDelegate = null;
			this.m_onmouseleaveDelegate = null;
			this.m_onmouseenterDelegate = null;
			this.m_onpageDelegate = null;
			this.m_onlayoutcompleteDelegate = null;
			this.m_onreadystatechangeDelegate = null;
			this.m_oncellchangeDelegate = null;
			this.m_onrowsinsertedDelegate = null;
			this.m_onrowsdeleteDelegate = null;
			this.m_oncontextmenuDelegate = null;
			this.m_onpasteDelegate = null;
			this.m_onbeforepasteDelegate = null;
			this.m_oncopyDelegate = null;
			this.m_onbeforecopyDelegate = null;
			this.m_oncutDelegate = null;
			this.m_onbeforecutDelegate = null;
			this.m_ondropDelegate = null;
			this.m_ondragleaveDelegate = null;
			this.m_ondragoverDelegate = null;
			this.m_ondragenterDelegate = null;
			this.m_ondragendDelegate = null;
			this.m_ondragDelegate = null;
			this.m_onresizeDelegate = null;
			this.m_onblurDelegate = null;
			this.m_onfocusDelegate = null;
			this.m_onscrollDelegate = null;
			this.m_onpropertychangeDelegate = null;
			this.m_onlosecaptureDelegate = null;
			this.m_ondatasetcompleteDelegate = null;
			this.m_ondataavailableDelegate = null;
			this.m_ondatasetchangedDelegate = null;
			this.m_onrowenterDelegate = null;
			this.m_onrowexitDelegate = null;
			this.m_onerrorupdateDelegate = null;
			this.m_onafterupdateDelegate = null;
			this.m_onbeforeupdateDelegate = null;
			this.m_ondragstartDelegate = null;
			this.m_onfilterchangeDelegate = null;
			this.m_onselectstartDelegate = null;
			this.m_onmouseupDelegate = null;
			this.m_onmousedownDelegate = null;
			this.m_onmousemoveDelegate = null;
			this.m_onmouseoverDelegate = null;
			this.m_onmouseoutDelegate = null;
			this.m_onkeyupDelegate = null;
			this.m_onkeydownDelegate = null;
			this.m_onkeypressDelegate = null;
			this.m_ondblclickDelegate = null;
			this.m_onclickDelegate = null;
			this.m_onhelpDelegate = null;
		}

		// Token: 0x04000591 RID: 1425
		public HTMLSelectElementEvents2_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x04000592 RID: 1426
		public HTMLSelectElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x04000593 RID: 1427
		public HTMLSelectElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x04000594 RID: 1428
		public HTMLSelectElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x04000595 RID: 1429
		public HTMLSelectElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x04000596 RID: 1430
		public HTMLSelectElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x04000597 RID: 1431
		public HTMLSelectElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x04000598 RID: 1432
		public HTMLSelectElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x04000599 RID: 1433
		public HTMLSelectElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x0400059A RID: 1434
		public HTMLSelectElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x0400059B RID: 1435
		public HTMLSelectElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x0400059C RID: 1436
		public HTMLSelectElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x0400059D RID: 1437
		public HTMLSelectElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x0400059E RID: 1438
		public HTMLSelectElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x0400059F RID: 1439
		public HTMLSelectElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040005A0 RID: 1440
		public HTMLSelectElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040005A1 RID: 1441
		public HTMLSelectElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x040005A2 RID: 1442
		public HTMLSelectElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040005A3 RID: 1443
		public HTMLSelectElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040005A4 RID: 1444
		public HTMLSelectElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040005A5 RID: 1445
		public HTMLSelectElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040005A6 RID: 1446
		public HTMLSelectElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040005A7 RID: 1447
		public HTMLSelectElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040005A8 RID: 1448
		public HTMLSelectElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040005A9 RID: 1449
		public HTMLSelectElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040005AA RID: 1450
		public HTMLSelectElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040005AB RID: 1451
		public HTMLSelectElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040005AC RID: 1452
		public HTMLSelectElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x040005AD RID: 1453
		public HTMLSelectElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040005AE RID: 1454
		public HTMLSelectElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x040005AF RID: 1455
		public HTMLSelectElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040005B0 RID: 1456
		public HTMLSelectElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040005B1 RID: 1457
		public HTMLSelectElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040005B2 RID: 1458
		public HTMLSelectElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040005B3 RID: 1459
		public HTMLSelectElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x040005B4 RID: 1460
		public HTMLSelectElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040005B5 RID: 1461
		public HTMLSelectElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x040005B6 RID: 1462
		public HTMLSelectElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040005B7 RID: 1463
		public HTMLSelectElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040005B8 RID: 1464
		public HTMLSelectElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040005B9 RID: 1465
		public HTMLSelectElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040005BA RID: 1466
		public HTMLSelectElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040005BB RID: 1467
		public HTMLSelectElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040005BC RID: 1468
		public HTMLSelectElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040005BD RID: 1469
		public HTMLSelectElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040005BE RID: 1470
		public HTMLSelectElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040005BF RID: 1471
		public HTMLSelectElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040005C0 RID: 1472
		public HTMLSelectElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040005C1 RID: 1473
		public HTMLSelectElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040005C2 RID: 1474
		public HTMLSelectElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040005C3 RID: 1475
		public HTMLSelectElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040005C4 RID: 1476
		public HTMLSelectElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040005C5 RID: 1477
		public HTMLSelectElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040005C6 RID: 1478
		public HTMLSelectElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040005C7 RID: 1479
		public HTMLSelectElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040005C8 RID: 1480
		public HTMLSelectElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040005C9 RID: 1481
		public HTMLSelectElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040005CA RID: 1482
		public HTMLSelectElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040005CB RID: 1483
		public HTMLSelectElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040005CC RID: 1484
		public HTMLSelectElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040005CD RID: 1485
		public HTMLSelectElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040005CE RID: 1486
		public HTMLSelectElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x040005CF RID: 1487
		public HTMLSelectElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040005D0 RID: 1488
		public int m_dwCookie;
	}
}

using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000DD0 RID: 3536
	[ClassInterface(0)]
	internal sealed class HTMLMarqueeElementEvents2_SinkHelper : HTMLMarqueeElementEvents2
	{
		// Token: 0x06017A77 RID: 96887 RVA: 0x000357EC File Offset: 0x000347EC
		public override void onstart(IHTMLEventObj A_1)
		{
			if (this.m_onstartDelegate != null)
			{
				this.m_onstartDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A78 RID: 96888 RVA: 0x0003581C File Offset: 0x0003481C
		public override void onfinish(IHTMLEventObj A_1)
		{
			if (this.m_onfinishDelegate != null)
			{
				this.m_onfinishDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A79 RID: 96889 RVA: 0x0003584C File Offset: 0x0003484C
		public override void onbounce(IHTMLEventObj A_1)
		{
			if (this.m_onbounceDelegate != null)
			{
				this.m_onbounceDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A7A RID: 96890 RVA: 0x0003587C File Offset: 0x0003487C
		public override void onselect(IHTMLEventObj A_1)
		{
			if (this.m_onselectDelegate != null)
			{
				this.m_onselectDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A7B RID: 96891 RVA: 0x000358AC File Offset: 0x000348AC
		public override void onchange(IHTMLEventObj A_1)
		{
			if (this.m_onchangeDelegate != null)
			{
				this.m_onchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A7C RID: 96892 RVA: 0x000358DC File Offset: 0x000348DC
		public override bool onmousewheel(IHTMLEventObj A_1)
		{
			return this.m_onmousewheelDelegate != null && this.m_onmousewheelDelegate(A_1);
		}

		// Token: 0x06017A7D RID: 96893 RVA: 0x0003590C File Offset: 0x0003490C
		public override void onresizeend(IHTMLEventObj A_1)
		{
			if (this.m_onresizeendDelegate != null)
			{
				this.m_onresizeendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A7E RID: 96894 RVA: 0x0003593C File Offset: 0x0003493C
		public override bool onresizestart(IHTMLEventObj A_1)
		{
			return this.m_onresizestartDelegate != null && this.m_onresizestartDelegate(A_1);
		}

		// Token: 0x06017A7F RID: 96895 RVA: 0x0003596C File Offset: 0x0003496C
		public override void onmoveend(IHTMLEventObj A_1)
		{
			if (this.m_onmoveendDelegate != null)
			{
				this.m_onmoveendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A80 RID: 96896 RVA: 0x0003599C File Offset: 0x0003499C
		public override bool onmovestart(IHTMLEventObj A_1)
		{
			return this.m_onmovestartDelegate != null && this.m_onmovestartDelegate(A_1);
		}

		// Token: 0x06017A81 RID: 96897 RVA: 0x000359CC File Offset: 0x000349CC
		public override bool oncontrolselect(IHTMLEventObj A_1)
		{
			return this.m_oncontrolselectDelegate != null && this.m_oncontrolselectDelegate(A_1);
		}

		// Token: 0x06017A82 RID: 96898 RVA: 0x000359FC File Offset: 0x000349FC
		public override void onmove(IHTMLEventObj A_1)
		{
			if (this.m_onmoveDelegate != null)
			{
				this.m_onmoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A83 RID: 96899 RVA: 0x00035A2C File Offset: 0x00034A2C
		public override void onfocusout(IHTMLEventObj A_1)
		{
			if (this.m_onfocusoutDelegate != null)
			{
				this.m_onfocusoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A84 RID: 96900 RVA: 0x00035A5C File Offset: 0x00034A5C
		public override void onfocusin(IHTMLEventObj A_1)
		{
			if (this.m_onfocusinDelegate != null)
			{
				this.m_onfocusinDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A85 RID: 96901 RVA: 0x00035A8C File Offset: 0x00034A8C
		public override bool onbeforeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeactivateDelegate != null && this.m_onbeforeactivateDelegate(A_1);
		}

		// Token: 0x06017A86 RID: 96902 RVA: 0x00035ABC File Offset: 0x00034ABC
		public override bool onbeforedeactivate(IHTMLEventObj A_1)
		{
			return this.m_onbeforedeactivateDelegate != null && this.m_onbeforedeactivateDelegate(A_1);
		}

		// Token: 0x06017A87 RID: 96903 RVA: 0x00035AEC File Offset: 0x00034AEC
		public override void ondeactivate(IHTMLEventObj A_1)
		{
			if (this.m_ondeactivateDelegate != null)
			{
				this.m_ondeactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A88 RID: 96904 RVA: 0x00035B1C File Offset: 0x00034B1C
		public override void onactivate(IHTMLEventObj A_1)
		{
			if (this.m_onactivateDelegate != null)
			{
				this.m_onactivateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A89 RID: 96905 RVA: 0x00035B4C File Offset: 0x00034B4C
		public override void onmouseleave(IHTMLEventObj A_1)
		{
			if (this.m_onmouseleaveDelegate != null)
			{
				this.m_onmouseleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8A RID: 96906 RVA: 0x00035B7C File Offset: 0x00034B7C
		public override void onmouseenter(IHTMLEventObj A_1)
		{
			if (this.m_onmouseenterDelegate != null)
			{
				this.m_onmouseenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8B RID: 96907 RVA: 0x00035BAC File Offset: 0x00034BAC
		public override void onpage(IHTMLEventObj A_1)
		{
			if (this.m_onpageDelegate != null)
			{
				this.m_onpageDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8C RID: 96908 RVA: 0x00035BDC File Offset: 0x00034BDC
		public override void onlayoutcomplete(IHTMLEventObj A_1)
		{
			if (this.m_onlayoutcompleteDelegate != null)
			{
				this.m_onlayoutcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8D RID: 96909 RVA: 0x00035C0C File Offset: 0x00034C0C
		public override void onreadystatechange(IHTMLEventObj A_1)
		{
			if (this.m_onreadystatechangeDelegate != null)
			{
				this.m_onreadystatechangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8E RID: 96910 RVA: 0x00035C3C File Offset: 0x00034C3C
		public override void oncellchange(IHTMLEventObj A_1)
		{
			if (this.m_oncellchangeDelegate != null)
			{
				this.m_oncellchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A8F RID: 96911 RVA: 0x00035C6C File Offset: 0x00034C6C
		public override void onrowsinserted(IHTMLEventObj A_1)
		{
			if (this.m_onrowsinsertedDelegate != null)
			{
				this.m_onrowsinsertedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A90 RID: 96912 RVA: 0x00035C9C File Offset: 0x00034C9C
		public override void onrowsdelete(IHTMLEventObj A_1)
		{
			if (this.m_onrowsdeleteDelegate != null)
			{
				this.m_onrowsdeleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A91 RID: 96913 RVA: 0x00035CCC File Offset: 0x00034CCC
		public override bool oncontextmenu(IHTMLEventObj A_1)
		{
			return this.m_oncontextmenuDelegate != null && this.m_oncontextmenuDelegate(A_1);
		}

		// Token: 0x06017A92 RID: 96914 RVA: 0x00035CFC File Offset: 0x00034CFC
		public override bool onpaste(IHTMLEventObj A_1)
		{
			return this.m_onpasteDelegate != null && this.m_onpasteDelegate(A_1);
		}

		// Token: 0x06017A93 RID: 96915 RVA: 0x00035D2C File Offset: 0x00034D2C
		public override bool onbeforepaste(IHTMLEventObj A_1)
		{
			return this.m_onbeforepasteDelegate != null && this.m_onbeforepasteDelegate(A_1);
		}

		// Token: 0x06017A94 RID: 96916 RVA: 0x00035D5C File Offset: 0x00034D5C
		public override bool oncopy(IHTMLEventObj A_1)
		{
			return this.m_oncopyDelegate != null && this.m_oncopyDelegate(A_1);
		}

		// Token: 0x06017A95 RID: 96917 RVA: 0x00035D8C File Offset: 0x00034D8C
		public override bool onbeforecopy(IHTMLEventObj A_1)
		{
			return this.m_onbeforecopyDelegate != null && this.m_onbeforecopyDelegate(A_1);
		}

		// Token: 0x06017A96 RID: 96918 RVA: 0x00035DBC File Offset: 0x00034DBC
		public override bool oncut(IHTMLEventObj A_1)
		{
			return this.m_oncutDelegate != null && this.m_oncutDelegate(A_1);
		}

		// Token: 0x06017A97 RID: 96919 RVA: 0x00035DEC File Offset: 0x00034DEC
		public override bool onbeforecut(IHTMLEventObj A_1)
		{
			return this.m_onbeforecutDelegate != null && this.m_onbeforecutDelegate(A_1);
		}

		// Token: 0x06017A98 RID: 96920 RVA: 0x00035E1C File Offset: 0x00034E1C
		public override bool ondrop(IHTMLEventObj A_1)
		{
			return this.m_ondropDelegate != null && this.m_ondropDelegate(A_1);
		}

		// Token: 0x06017A99 RID: 96921 RVA: 0x00035E4C File Offset: 0x00034E4C
		public override void ondragleave(IHTMLEventObj A_1)
		{
			if (this.m_ondragleaveDelegate != null)
			{
				this.m_ondragleaveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A9A RID: 96922 RVA: 0x00035E7C File Offset: 0x00034E7C
		public override bool ondragover(IHTMLEventObj A_1)
		{
			return this.m_ondragoverDelegate != null && this.m_ondragoverDelegate(A_1);
		}

		// Token: 0x06017A9B RID: 96923 RVA: 0x00035EAC File Offset: 0x00034EAC
		public override bool ondragenter(IHTMLEventObj A_1)
		{
			return this.m_ondragenterDelegate != null && this.m_ondragenterDelegate(A_1);
		}

		// Token: 0x06017A9C RID: 96924 RVA: 0x00035EDC File Offset: 0x00034EDC
		public override void ondragend(IHTMLEventObj A_1)
		{
			if (this.m_ondragendDelegate != null)
			{
				this.m_ondragendDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A9D RID: 96925 RVA: 0x00035F0C File Offset: 0x00034F0C
		public override bool ondrag(IHTMLEventObj A_1)
		{
			return this.m_ondragDelegate != null && this.m_ondragDelegate(A_1);
		}

		// Token: 0x06017A9E RID: 96926 RVA: 0x00035F3C File Offset: 0x00034F3C
		public override void onresize(IHTMLEventObj A_1)
		{
			if (this.m_onresizeDelegate != null)
			{
				this.m_onresizeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017A9F RID: 96927 RVA: 0x00035F6C File Offset: 0x00034F6C
		public override void onblur(IHTMLEventObj A_1)
		{
			if (this.m_onblurDelegate != null)
			{
				this.m_onblurDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA0 RID: 96928 RVA: 0x00035F9C File Offset: 0x00034F9C
		public override void onfocus(IHTMLEventObj A_1)
		{
			if (this.m_onfocusDelegate != null)
			{
				this.m_onfocusDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA1 RID: 96929 RVA: 0x00035FCC File Offset: 0x00034FCC
		public override void onscroll(IHTMLEventObj A_1)
		{
			if (this.m_onscrollDelegate != null)
			{
				this.m_onscrollDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA2 RID: 96930 RVA: 0x00035FFC File Offset: 0x00034FFC
		public override void onpropertychange(IHTMLEventObj A_1)
		{
			if (this.m_onpropertychangeDelegate != null)
			{
				this.m_onpropertychangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA3 RID: 96931 RVA: 0x0003602C File Offset: 0x0003502C
		public override void onlosecapture(IHTMLEventObj A_1)
		{
			if (this.m_onlosecaptureDelegate != null)
			{
				this.m_onlosecaptureDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA4 RID: 96932 RVA: 0x0003605C File Offset: 0x0003505C
		public override void ondatasetcomplete(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetcompleteDelegate != null)
			{
				this.m_ondatasetcompleteDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA5 RID: 96933 RVA: 0x0003608C File Offset: 0x0003508C
		public override void ondataavailable(IHTMLEventObj A_1)
		{
			if (this.m_ondataavailableDelegate != null)
			{
				this.m_ondataavailableDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA6 RID: 96934 RVA: 0x000360BC File Offset: 0x000350BC
		public override void ondatasetchanged(IHTMLEventObj A_1)
		{
			if (this.m_ondatasetchangedDelegate != null)
			{
				this.m_ondatasetchangedDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA7 RID: 96935 RVA: 0x000360EC File Offset: 0x000350EC
		public override void onrowenter(IHTMLEventObj A_1)
		{
			if (this.m_onrowenterDelegate != null)
			{
				this.m_onrowenterDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AA8 RID: 96936 RVA: 0x0003611C File Offset: 0x0003511C
		public override bool onrowexit(IHTMLEventObj A_1)
		{
			return this.m_onrowexitDelegate != null && this.m_onrowexitDelegate(A_1);
		}

		// Token: 0x06017AA9 RID: 96937 RVA: 0x0003614C File Offset: 0x0003514C
		public override bool onerrorupdate(IHTMLEventObj A_1)
		{
			return this.m_onerrorupdateDelegate != null && this.m_onerrorupdateDelegate(A_1);
		}

		// Token: 0x06017AAA RID: 96938 RVA: 0x0003617C File Offset: 0x0003517C
		public override void onafterupdate(IHTMLEventObj A_1)
		{
			if (this.m_onafterupdateDelegate != null)
			{
				this.m_onafterupdateDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AAB RID: 96939 RVA: 0x000361AC File Offset: 0x000351AC
		public override bool onbeforeupdate(IHTMLEventObj A_1)
		{
			return this.m_onbeforeupdateDelegate != null && this.m_onbeforeupdateDelegate(A_1);
		}

		// Token: 0x06017AAC RID: 96940 RVA: 0x000361DC File Offset: 0x000351DC
		public override bool ondragstart(IHTMLEventObj A_1)
		{
			return this.m_ondragstartDelegate != null && this.m_ondragstartDelegate(A_1);
		}

		// Token: 0x06017AAD RID: 96941 RVA: 0x0003620C File Offset: 0x0003520C
		public override void onfilterchange(IHTMLEventObj A_1)
		{
			if (this.m_onfilterchangeDelegate != null)
			{
				this.m_onfilterchangeDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AAE RID: 96942 RVA: 0x0003623C File Offset: 0x0003523C
		public override bool onselectstart(IHTMLEventObj A_1)
		{
			return this.m_onselectstartDelegate != null && this.m_onselectstartDelegate(A_1);
		}

		// Token: 0x06017AAF RID: 96943 RVA: 0x0003626C File Offset: 0x0003526C
		public override void onmouseup(IHTMLEventObj A_1)
		{
			if (this.m_onmouseupDelegate != null)
			{
				this.m_onmouseupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB0 RID: 96944 RVA: 0x0003629C File Offset: 0x0003529C
		public override void onmousedown(IHTMLEventObj A_1)
		{
			if (this.m_onmousedownDelegate != null)
			{
				this.m_onmousedownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB1 RID: 96945 RVA: 0x000362CC File Offset: 0x000352CC
		public override void onmousemove(IHTMLEventObj A_1)
		{
			if (this.m_onmousemoveDelegate != null)
			{
				this.m_onmousemoveDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB2 RID: 96946 RVA: 0x000362FC File Offset: 0x000352FC
		public override void onmouseover(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoverDelegate != null)
			{
				this.m_onmouseoverDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB3 RID: 96947 RVA: 0x0003632C File Offset: 0x0003532C
		public override void onmouseout(IHTMLEventObj A_1)
		{
			if (this.m_onmouseoutDelegate != null)
			{
				this.m_onmouseoutDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB4 RID: 96948 RVA: 0x0003635C File Offset: 0x0003535C
		public override void onkeyup(IHTMLEventObj A_1)
		{
			if (this.m_onkeyupDelegate != null)
			{
				this.m_onkeyupDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB5 RID: 96949 RVA: 0x0003638C File Offset: 0x0003538C
		public override void onkeydown(IHTMLEventObj A_1)
		{
			if (this.m_onkeydownDelegate != null)
			{
				this.m_onkeydownDelegate(A_1);
				return;
			}
		}

		// Token: 0x06017AB6 RID: 96950 RVA: 0x000363BC File Offset: 0x000353BC
		public override bool onkeypress(IHTMLEventObj A_1)
		{
			return this.m_onkeypressDelegate != null && this.m_onkeypressDelegate(A_1);
		}

		// Token: 0x06017AB7 RID: 96951 RVA: 0x000363EC File Offset: 0x000353EC
		public override bool ondblclick(IHTMLEventObj A_1)
		{
			return this.m_ondblclickDelegate != null && this.m_ondblclickDelegate(A_1);
		}

		// Token: 0x06017AB8 RID: 96952 RVA: 0x0003641C File Offset: 0x0003541C
		public override bool onclick(IHTMLEventObj A_1)
		{
			return this.m_onclickDelegate != null && this.m_onclickDelegate(A_1);
		}

		// Token: 0x06017AB9 RID: 96953 RVA: 0x0003644C File Offset: 0x0003544C
		public override bool onhelp(IHTMLEventObj A_1)
		{
			return this.m_onhelpDelegate != null && this.m_onhelpDelegate(A_1);
		}

		// Token: 0x06017ABA RID: 96954 RVA: 0x0003647C File Offset: 0x0003547C
		internal HTMLMarqueeElementEvents2_SinkHelper()
		{
			this.m_dwCookie = 0;
			this.m_onstartDelegate = null;
			this.m_onfinishDelegate = null;
			this.m_onbounceDelegate = null;
			this.m_onselectDelegate = null;
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

		// Token: 0x040006B2 RID: 1714
		public HTMLMarqueeElementEvents2_onstartEventHandler m_onstartDelegate;

		// Token: 0x040006B3 RID: 1715
		public HTMLMarqueeElementEvents2_onfinishEventHandler m_onfinishDelegate;

		// Token: 0x040006B4 RID: 1716
		public HTMLMarqueeElementEvents2_onbounceEventHandler m_onbounceDelegate;

		// Token: 0x040006B5 RID: 1717
		public HTMLMarqueeElementEvents2_onselectEventHandler m_onselectDelegate;

		// Token: 0x040006B6 RID: 1718
		public HTMLMarqueeElementEvents2_onchangeEventHandler m_onchangeDelegate;

		// Token: 0x040006B7 RID: 1719
		public HTMLMarqueeElementEvents2_onmousewheelEventHandler m_onmousewheelDelegate;

		// Token: 0x040006B8 RID: 1720
		public HTMLMarqueeElementEvents2_onresizeendEventHandler m_onresizeendDelegate;

		// Token: 0x040006B9 RID: 1721
		public HTMLMarqueeElementEvents2_onresizestartEventHandler m_onresizestartDelegate;

		// Token: 0x040006BA RID: 1722
		public HTMLMarqueeElementEvents2_onmoveendEventHandler m_onmoveendDelegate;

		// Token: 0x040006BB RID: 1723
		public HTMLMarqueeElementEvents2_onmovestartEventHandler m_onmovestartDelegate;

		// Token: 0x040006BC RID: 1724
		public HTMLMarqueeElementEvents2_oncontrolselectEventHandler m_oncontrolselectDelegate;

		// Token: 0x040006BD RID: 1725
		public HTMLMarqueeElementEvents2_onmoveEventHandler m_onmoveDelegate;

		// Token: 0x040006BE RID: 1726
		public HTMLMarqueeElementEvents2_onfocusoutEventHandler m_onfocusoutDelegate;

		// Token: 0x040006BF RID: 1727
		public HTMLMarqueeElementEvents2_onfocusinEventHandler m_onfocusinDelegate;

		// Token: 0x040006C0 RID: 1728
		public HTMLMarqueeElementEvents2_onbeforeactivateEventHandler m_onbeforeactivateDelegate;

		// Token: 0x040006C1 RID: 1729
		public HTMLMarqueeElementEvents2_onbeforedeactivateEventHandler m_onbeforedeactivateDelegate;

		// Token: 0x040006C2 RID: 1730
		public HTMLMarqueeElementEvents2_ondeactivateEventHandler m_ondeactivateDelegate;

		// Token: 0x040006C3 RID: 1731
		public HTMLMarqueeElementEvents2_onactivateEventHandler m_onactivateDelegate;

		// Token: 0x040006C4 RID: 1732
		public HTMLMarqueeElementEvents2_onmouseleaveEventHandler m_onmouseleaveDelegate;

		// Token: 0x040006C5 RID: 1733
		public HTMLMarqueeElementEvents2_onmouseenterEventHandler m_onmouseenterDelegate;

		// Token: 0x040006C6 RID: 1734
		public HTMLMarqueeElementEvents2_onpageEventHandler m_onpageDelegate;

		// Token: 0x040006C7 RID: 1735
		public HTMLMarqueeElementEvents2_onlayoutcompleteEventHandler m_onlayoutcompleteDelegate;

		// Token: 0x040006C8 RID: 1736
		public HTMLMarqueeElementEvents2_onreadystatechangeEventHandler m_onreadystatechangeDelegate;

		// Token: 0x040006C9 RID: 1737
		public HTMLMarqueeElementEvents2_oncellchangeEventHandler m_oncellchangeDelegate;

		// Token: 0x040006CA RID: 1738
		public HTMLMarqueeElementEvents2_onrowsinsertedEventHandler m_onrowsinsertedDelegate;

		// Token: 0x040006CB RID: 1739
		public HTMLMarqueeElementEvents2_onrowsdeleteEventHandler m_onrowsdeleteDelegate;

		// Token: 0x040006CC RID: 1740
		public HTMLMarqueeElementEvents2_oncontextmenuEventHandler m_oncontextmenuDelegate;

		// Token: 0x040006CD RID: 1741
		public HTMLMarqueeElementEvents2_onpasteEventHandler m_onpasteDelegate;

		// Token: 0x040006CE RID: 1742
		public HTMLMarqueeElementEvents2_onbeforepasteEventHandler m_onbeforepasteDelegate;

		// Token: 0x040006CF RID: 1743
		public HTMLMarqueeElementEvents2_oncopyEventHandler m_oncopyDelegate;

		// Token: 0x040006D0 RID: 1744
		public HTMLMarqueeElementEvents2_onbeforecopyEventHandler m_onbeforecopyDelegate;

		// Token: 0x040006D1 RID: 1745
		public HTMLMarqueeElementEvents2_oncutEventHandler m_oncutDelegate;

		// Token: 0x040006D2 RID: 1746
		public HTMLMarqueeElementEvents2_onbeforecutEventHandler m_onbeforecutDelegate;

		// Token: 0x040006D3 RID: 1747
		public HTMLMarqueeElementEvents2_ondropEventHandler m_ondropDelegate;

		// Token: 0x040006D4 RID: 1748
		public HTMLMarqueeElementEvents2_ondragleaveEventHandler m_ondragleaveDelegate;

		// Token: 0x040006D5 RID: 1749
		public HTMLMarqueeElementEvents2_ondragoverEventHandler m_ondragoverDelegate;

		// Token: 0x040006D6 RID: 1750
		public HTMLMarqueeElementEvents2_ondragenterEventHandler m_ondragenterDelegate;

		// Token: 0x040006D7 RID: 1751
		public HTMLMarqueeElementEvents2_ondragendEventHandler m_ondragendDelegate;

		// Token: 0x040006D8 RID: 1752
		public HTMLMarqueeElementEvents2_ondragEventHandler m_ondragDelegate;

		// Token: 0x040006D9 RID: 1753
		public HTMLMarqueeElementEvents2_onresizeEventHandler m_onresizeDelegate;

		// Token: 0x040006DA RID: 1754
		public HTMLMarqueeElementEvents2_onblurEventHandler m_onblurDelegate;

		// Token: 0x040006DB RID: 1755
		public HTMLMarqueeElementEvents2_onfocusEventHandler m_onfocusDelegate;

		// Token: 0x040006DC RID: 1756
		public HTMLMarqueeElementEvents2_onscrollEventHandler m_onscrollDelegate;

		// Token: 0x040006DD RID: 1757
		public HTMLMarqueeElementEvents2_onpropertychangeEventHandler m_onpropertychangeDelegate;

		// Token: 0x040006DE RID: 1758
		public HTMLMarqueeElementEvents2_onlosecaptureEventHandler m_onlosecaptureDelegate;

		// Token: 0x040006DF RID: 1759
		public HTMLMarqueeElementEvents2_ondatasetcompleteEventHandler m_ondatasetcompleteDelegate;

		// Token: 0x040006E0 RID: 1760
		public HTMLMarqueeElementEvents2_ondataavailableEventHandler m_ondataavailableDelegate;

		// Token: 0x040006E1 RID: 1761
		public HTMLMarqueeElementEvents2_ondatasetchangedEventHandler m_ondatasetchangedDelegate;

		// Token: 0x040006E2 RID: 1762
		public HTMLMarqueeElementEvents2_onrowenterEventHandler m_onrowenterDelegate;

		// Token: 0x040006E3 RID: 1763
		public HTMLMarqueeElementEvents2_onrowexitEventHandler m_onrowexitDelegate;

		// Token: 0x040006E4 RID: 1764
		public HTMLMarqueeElementEvents2_onerrorupdateEventHandler m_onerrorupdateDelegate;

		// Token: 0x040006E5 RID: 1765
		public HTMLMarqueeElementEvents2_onafterupdateEventHandler m_onafterupdateDelegate;

		// Token: 0x040006E6 RID: 1766
		public HTMLMarqueeElementEvents2_onbeforeupdateEventHandler m_onbeforeupdateDelegate;

		// Token: 0x040006E7 RID: 1767
		public HTMLMarqueeElementEvents2_ondragstartEventHandler m_ondragstartDelegate;

		// Token: 0x040006E8 RID: 1768
		public HTMLMarqueeElementEvents2_onfilterchangeEventHandler m_onfilterchangeDelegate;

		// Token: 0x040006E9 RID: 1769
		public HTMLMarqueeElementEvents2_onselectstartEventHandler m_onselectstartDelegate;

		// Token: 0x040006EA RID: 1770
		public HTMLMarqueeElementEvents2_onmouseupEventHandler m_onmouseupDelegate;

		// Token: 0x040006EB RID: 1771
		public HTMLMarqueeElementEvents2_onmousedownEventHandler m_onmousedownDelegate;

		// Token: 0x040006EC RID: 1772
		public HTMLMarqueeElementEvents2_onmousemoveEventHandler m_onmousemoveDelegate;

		// Token: 0x040006ED RID: 1773
		public HTMLMarqueeElementEvents2_onmouseoverEventHandler m_onmouseoverDelegate;

		// Token: 0x040006EE RID: 1774
		public HTMLMarqueeElementEvents2_onmouseoutEventHandler m_onmouseoutDelegate;

		// Token: 0x040006EF RID: 1775
		public HTMLMarqueeElementEvents2_onkeyupEventHandler m_onkeyupDelegate;

		// Token: 0x040006F0 RID: 1776
		public HTMLMarqueeElementEvents2_onkeydownEventHandler m_onkeydownDelegate;

		// Token: 0x040006F1 RID: 1777
		public HTMLMarqueeElementEvents2_onkeypressEventHandler m_onkeypressDelegate;

		// Token: 0x040006F2 RID: 1778
		public HTMLMarqueeElementEvents2_ondblclickEventHandler m_ondblclickDelegate;

		// Token: 0x040006F3 RID: 1779
		public HTMLMarqueeElementEvents2_onclickEventHandler m_onclickDelegate;

		// Token: 0x040006F4 RID: 1780
		public HTMLMarqueeElementEvents2_onhelpEventHandler m_onhelpDelegate;

		// Token: 0x040006F5 RID: 1781
		public int m_dwCookie;
	}
}

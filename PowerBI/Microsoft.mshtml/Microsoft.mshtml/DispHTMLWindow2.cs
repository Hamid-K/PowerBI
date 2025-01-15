using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x020007C1 RID: 1985
	[InterfaceType(2)]
	[DefaultMember("item")]
	[Guid("3050F55D-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4112)]
	[ComImport]
	public interface DispHTMLWindow2
	{
		// Token: 0x0600D772 RID: 55154
		[DispId(0)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object item([MarshalAs(27)] [In] ref object pvarIndex);

		// Token: 0x170047C9 RID: 18377
		// (get) Token: 0x0600D773 RID: 55155
		[DispId(1001)]
		int length
		{
			[DispId(1001)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047CA RID: 18378
		// (get) Token: 0x0600D774 RID: 55156
		[DispId(1100)]
		FramesCollection frames
		{
			[DispId(1100)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047CB RID: 18379
		// (get) Token: 0x0600D776 RID: 55158
		// (set) Token: 0x0600D775 RID: 55157
		[DispId(1101)]
		string defaultStatus
		{
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1101)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170047CC RID: 18380
		// (get) Token: 0x0600D778 RID: 55160
		// (set) Token: 0x0600D777 RID: 55159
		[DispId(1102)]
		string status
		{
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(1102)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x0600D779 RID: 55161
		[DispId(1104)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearTimeout([In] int timerID);

		// Token: 0x0600D77A RID: 55162
		[DispId(1105)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void alert([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D77B RID: 55163
		[DispId(1110)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool confirm([MarshalAs(19)] [In] string message = "");

		// Token: 0x0600D77C RID: 55164
		[DispId(1111)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object prompt([MarshalAs(19)] [In] string message = "", [MarshalAs(19)] [In] string defstr = "undefined");

		// Token: 0x170047CD RID: 18381
		// (get) Token: 0x0600D77D RID: 55165
		[DispId(1125)]
		HTMLImageElementFactory Image
		{
			[DispId(1125)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047CE RID: 18382
		// (get) Token: 0x0600D77E RID: 55166
		[DispId(14)]
		HTMLLocation location
		{
			[DispId(14)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047CF RID: 18383
		// (get) Token: 0x0600D77F RID: 55167
		[DispId(2)]
		HTMLHistory history
		{
			[DispId(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D780 RID: 55168
		[DispId(3)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void close();

		// Token: 0x170047D0 RID: 18384
		// (get) Token: 0x0600D782 RID: 55170
		// (set) Token: 0x0600D781 RID: 55169
		[DispId(4)]
		object opener
		{
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(4)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047D1 RID: 18385
		// (get) Token: 0x0600D783 RID: 55171
		[DispId(5)]
		HTMLNavigator navigator
		{
			[DispId(5)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047D2 RID: 18386
		// (get) Token: 0x0600D785 RID: 55173
		// (set) Token: 0x0600D784 RID: 55172
		[DispId(11)]
		string name
		{
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
			[DispId(11)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(19)]
			set;
		}

		// Token: 0x170047D3 RID: 18387
		// (get) Token: 0x0600D786 RID: 55174
		[DispId(12)]
		IHTMLWindow2 parent
		{
			[DispId(12)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D787 RID: 55175
		[DispId(13)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLWindow2 open([MarshalAs(19)] [In] string url = "", [MarshalAs(19)] [In] string name = "", [MarshalAs(19)] [In] string features = "", [In] bool replace = false);

		// Token: 0x170047D4 RID: 18388
		// (get) Token: 0x0600D788 RID: 55176
		[DispId(20)]
		IHTMLWindow2 self
		{
			[DispId(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047D5 RID: 18389
		// (get) Token: 0x0600D789 RID: 55177
		[DispId(21)]
		IHTMLWindow2 top
		{
			[DispId(21)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047D6 RID: 18390
		// (get) Token: 0x0600D78A RID: 55178
		[DispId(22)]
		IHTMLWindow2 window
		{
			[DispId(22)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D78B RID: 55179
		[DispId(25)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void navigate([MarshalAs(19)] [In] string url);

		// Token: 0x170047D7 RID: 18391
		// (get) Token: 0x0600D78D RID: 55181
		// (set) Token: 0x0600D78C RID: 55180
		[DispId(-2147412098)]
		object onfocus
		{
			[TypeLibFunc(20)]
			[DispId(-2147412098)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412098)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047D8 RID: 18392
		// (get) Token: 0x0600D78F RID: 55183
		// (set) Token: 0x0600D78E RID: 55182
		[DispId(-2147412097)]
		object onblur
		{
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412097)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047D9 RID: 18393
		// (get) Token: 0x0600D791 RID: 55185
		// (set) Token: 0x0600D790 RID: 55184
		[DispId(-2147412080)]
		object onload
		{
			[TypeLibFunc(20)]
			[DispId(-2147412080)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412080)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DA RID: 18394
		// (get) Token: 0x0600D793 RID: 55187
		// (set) Token: 0x0600D792 RID: 55186
		[DispId(-2147412073)]
		object onbeforeunload
		{
			[DispId(-2147412073)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412073)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DB RID: 18395
		// (get) Token: 0x0600D795 RID: 55189
		// (set) Token: 0x0600D794 RID: 55188
		[DispId(-2147412079)]
		object onunload
		{
			[DispId(-2147412079)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[TypeLibFunc(20)]
			[DispId(-2147412079)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DC RID: 18396
		// (get) Token: 0x0600D797 RID: 55191
		// (set) Token: 0x0600D796 RID: 55190
		[DispId(-2147412099)]
		object onhelp
		{
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412099)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DD RID: 18397
		// (get) Token: 0x0600D799 RID: 55193
		// (set) Token: 0x0600D798 RID: 55192
		[DispId(-2147412083)]
		object onerror
		{
			[TypeLibFunc(20)]
			[DispId(-2147412083)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412083)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DE RID: 18398
		// (get) Token: 0x0600D79B RID: 55195
		// (set) Token: 0x0600D79A RID: 55194
		[DispId(-2147412076)]
		object onresize
		{
			[TypeLibFunc(20)]
			[DispId(-2147412076)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412076)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047DF RID: 18399
		// (get) Token: 0x0600D79D RID: 55197
		// (set) Token: 0x0600D79C RID: 55196
		[DispId(-2147412081)]
		object onscroll
		{
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412081)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047E0 RID: 18400
		// (get) Token: 0x0600D79E RID: 55198
		[DispId(1151)]
		IHTMLDocument2 document
		{
			[DispId(1151)]
			[TypeLibFunc(2)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047E1 RID: 18401
		// (get) Token: 0x0600D79F RID: 55199
		[DispId(1152)]
		IHTMLEventObj @event
		{
			[DispId(1152)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047E2 RID: 18402
		// (get) Token: 0x0600D7A0 RID: 55200
		[DispId(1153)]
		object _newEnum
		{
			[TypeLibFunc(65)]
			[DispId(1153)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(25)]
			get;
		}

		// Token: 0x0600D7A1 RID: 55201
		[DispId(1154)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object showModalDialog([MarshalAs(19)] [In] string dialog, [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object varOptions);

		// Token: 0x0600D7A2 RID: 55202
		[DispId(1155)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void showHelp([MarshalAs(19)] [In] string helpURL, [MarshalAs(27)] [In] [Optional] object helpArg, [MarshalAs(19)] [In] string features = "");

		// Token: 0x170047E3 RID: 18403
		// (get) Token: 0x0600D7A3 RID: 55203
		[DispId(1156)]
		IHTMLScreen screen
		{
			[DispId(1156)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x170047E4 RID: 18404
		// (get) Token: 0x0600D7A4 RID: 55204
		[DispId(1157)]
		HTMLOptionElementFactory Option
		{
			[DispId(1157)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7A5 RID: 55205
		[DispId(1158)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void focus();

		// Token: 0x170047E5 RID: 18405
		// (get) Token: 0x0600D7A6 RID: 55206
		[DispId(23)]
		bool closed
		{
			[DispId(23)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D7A7 RID: 55207
		[DispId(1159)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void blur();

		// Token: 0x0600D7A8 RID: 55208
		[DispId(1160)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scroll([In] int x, [In] int y);

		// Token: 0x170047E6 RID: 18406
		// (get) Token: 0x0600D7A9 RID: 55209
		[DispId(1161)]
		HTMLNavigator clientInformation
		{
			[DispId(1161)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7AA RID: 55210
		[DispId(1163)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void clearInterval([In] int timerID);

		// Token: 0x170047E7 RID: 18407
		// (get) Token: 0x0600D7AC RID: 55212
		// (set) Token: 0x0600D7AB RID: 55211
		[DispId(1164)]
		object offscreenBuffering
		{
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(1164)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x0600D7AD RID: 55213
		[DispId(1165)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object execScript([MarshalAs(19)] [In] string code, [MarshalAs(19)] [In] string language = "JScript");

		// Token: 0x0600D7AE RID: 55214
		[DispId(1166)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(19)]
		string toString();

		// Token: 0x0600D7AF RID: 55215
		[DispId(1167)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollBy([In] int x, [In] int y);

		// Token: 0x0600D7B0 RID: 55216
		[DispId(1168)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void scrollTo([In] int x, [In] int y);

		// Token: 0x0600D7B1 RID: 55217
		[DispId(6)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void moveTo([In] int x, [In] int y);

		// Token: 0x0600D7B2 RID: 55218
		[DispId(7)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void moveBy([In] int x, [In] int y);

		// Token: 0x0600D7B3 RID: 55219
		[DispId(9)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void resizeTo([In] int x, [In] int y);

		// Token: 0x0600D7B4 RID: 55220
		[DispId(8)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void resizeBy([In] int x, [In] int y);

		// Token: 0x170047E8 RID: 18408
		// (get) Token: 0x0600D7B5 RID: 55221
		[DispId(1169)]
		object external
		{
			[DispId(1169)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(26)]
			get;
		}

		// Token: 0x170047E9 RID: 18409
		// (get) Token: 0x0600D7B6 RID: 55222
		[DispId(1170)]
		int screenLeft
		{
			[DispId(1170)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x170047EA RID: 18410
		// (get) Token: 0x0600D7B7 RID: 55223
		[DispId(1171)]
		int screenTop
		{
			[DispId(1171)]
			[MethodImpl(4224, MethodCodeType = 3)]
			get;
		}

		// Token: 0x0600D7B8 RID: 55224
		[DispId(-2147417605)]
		[MethodImpl(4224, MethodCodeType = 3)]
		bool attachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D7B9 RID: 55225
		[DispId(-2147417604)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void detachEvent([MarshalAs(19)] [In] string @event, [MarshalAs(26)] [In] object pdisp);

		// Token: 0x0600D7BA RID: 55226
		[DispId(1103)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int setTimeout([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D7BB RID: 55227
		[DispId(1162)]
		[MethodImpl(4224, MethodCodeType = 3)]
		int setInterval([MarshalAs(27)] [In] ref object expression, [In] int msec, [MarshalAs(27)] [In] [Optional] ref object language);

		// Token: 0x0600D7BC RID: 55228
		[DispId(1174)]
		[MethodImpl(4224, MethodCodeType = 3)]
		void print();

		// Token: 0x170047EB RID: 18411
		// (get) Token: 0x0600D7BE RID: 55230
		// (set) Token: 0x0600D7BD RID: 55229
		[DispId(-2147412046)]
		object onbeforeprint
		{
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412046)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047EC RID: 18412
		// (get) Token: 0x0600D7C0 RID: 55232
		// (set) Token: 0x0600D7BF RID: 55231
		[DispId(-2147412045)]
		object onafterprint
		{
			[TypeLibFunc(20)]
			[DispId(-2147412045)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
			[DispId(-2147412045)]
			[TypeLibFunc(20)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[param: MarshalAs(27)]
			set;
		}

		// Token: 0x170047ED RID: 18413
		// (get) Token: 0x0600D7C1 RID: 55233
		[DispId(1175)]
		IHTMLDataTransfer clipboardData
		{
			[DispId(1175)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}

		// Token: 0x0600D7C2 RID: 55234
		[DispId(1176)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(28)]
		IHTMLWindow2 showModelessDialog([MarshalAs(19)] [In] string url = "", [MarshalAs(27)] [In] [Optional] ref object varArgIn, [MarshalAs(27)] [In] [Optional] ref object options);

		// Token: 0x0600D7C3 RID: 55235
		[DispId(1180)]
		[MethodImpl(4224, MethodCodeType = 3)]
		[return: MarshalAs(26)]
		object createPopup([MarshalAs(27)] [In] [Optional] ref object varArgIn);

		// Token: 0x170047EE RID: 18414
		// (get) Token: 0x0600D7C4 RID: 55236
		[DispId(1181)]
		IHTMLFrameBase frameElement
		{
			[DispId(1181)]
			[MethodImpl(4224, MethodCodeType = 3)]
			[return: MarshalAs(28)]
			get;
		}
	}
}

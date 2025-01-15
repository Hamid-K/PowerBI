using System;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000AD7 RID: 2775
	[ComVisible(false)]
	[ComEventInterface(typeof(HTMLScriptEvents2\u0000), typeof(HTMLScriptEvents2_EventProvider\u0000))]
	public interface HTMLScriptEvents2_Event
	{
		// Token: 0x14002324 RID: 8996
		// (add) Token: 0x06012211 RID: 74257
		// (remove) Token: 0x06012212 RID: 74258
		event HTMLScriptEvents2_onhelpEventHandler onhelp;

		// Token: 0x14002325 RID: 8997
		// (add) Token: 0x06012213 RID: 74259
		// (remove) Token: 0x06012214 RID: 74260
		event HTMLScriptEvents2_onclickEventHandler onclick;

		// Token: 0x14002326 RID: 8998
		// (add) Token: 0x06012215 RID: 74261
		// (remove) Token: 0x06012216 RID: 74262
		event HTMLScriptEvents2_ondblclickEventHandler ondblclick;

		// Token: 0x14002327 RID: 8999
		// (add) Token: 0x06012217 RID: 74263
		// (remove) Token: 0x06012218 RID: 74264
		event HTMLScriptEvents2_onkeypressEventHandler onkeypress;

		// Token: 0x14002328 RID: 9000
		// (add) Token: 0x06012219 RID: 74265
		// (remove) Token: 0x0601221A RID: 74266
		event HTMLScriptEvents2_onkeydownEventHandler onkeydown;

		// Token: 0x14002329 RID: 9001
		// (add) Token: 0x0601221B RID: 74267
		// (remove) Token: 0x0601221C RID: 74268
		event HTMLScriptEvents2_onkeyupEventHandler onkeyup;

		// Token: 0x1400232A RID: 9002
		// (add) Token: 0x0601221D RID: 74269
		// (remove) Token: 0x0601221E RID: 74270
		event HTMLScriptEvents2_onmouseoutEventHandler onmouseout;

		// Token: 0x1400232B RID: 9003
		// (add) Token: 0x0601221F RID: 74271
		// (remove) Token: 0x06012220 RID: 74272
		event HTMLScriptEvents2_onmouseoverEventHandler onmouseover;

		// Token: 0x1400232C RID: 9004
		// (add) Token: 0x06012221 RID: 74273
		// (remove) Token: 0x06012222 RID: 74274
		event HTMLScriptEvents2_onmousemoveEventHandler onmousemove;

		// Token: 0x1400232D RID: 9005
		// (add) Token: 0x06012223 RID: 74275
		// (remove) Token: 0x06012224 RID: 74276
		event HTMLScriptEvents2_onmousedownEventHandler onmousedown;

		// Token: 0x1400232E RID: 9006
		// (add) Token: 0x06012225 RID: 74277
		// (remove) Token: 0x06012226 RID: 74278
		event HTMLScriptEvents2_onmouseupEventHandler onmouseup;

		// Token: 0x1400232F RID: 9007
		// (add) Token: 0x06012227 RID: 74279
		// (remove) Token: 0x06012228 RID: 74280
		event HTMLScriptEvents2_onselectstartEventHandler onselectstart;

		// Token: 0x14002330 RID: 9008
		// (add) Token: 0x06012229 RID: 74281
		// (remove) Token: 0x0601222A RID: 74282
		event HTMLScriptEvents2_onfilterchangeEventHandler onfilterchange;

		// Token: 0x14002331 RID: 9009
		// (add) Token: 0x0601222B RID: 74283
		// (remove) Token: 0x0601222C RID: 74284
		event HTMLScriptEvents2_ondragstartEventHandler ondragstart;

		// Token: 0x14002332 RID: 9010
		// (add) Token: 0x0601222D RID: 74285
		// (remove) Token: 0x0601222E RID: 74286
		event HTMLScriptEvents2_onbeforeupdateEventHandler onbeforeupdate;

		// Token: 0x14002333 RID: 9011
		// (add) Token: 0x0601222F RID: 74287
		// (remove) Token: 0x06012230 RID: 74288
		event HTMLScriptEvents2_onafterupdateEventHandler onafterupdate;

		// Token: 0x14002334 RID: 9012
		// (add) Token: 0x06012231 RID: 74289
		// (remove) Token: 0x06012232 RID: 74290
		event HTMLScriptEvents2_onerrorupdateEventHandler onerrorupdate;

		// Token: 0x14002335 RID: 9013
		// (add) Token: 0x06012233 RID: 74291
		// (remove) Token: 0x06012234 RID: 74292
		event HTMLScriptEvents2_onrowexitEventHandler onrowexit;

		// Token: 0x14002336 RID: 9014
		// (add) Token: 0x06012235 RID: 74293
		// (remove) Token: 0x06012236 RID: 74294
		event HTMLScriptEvents2_onrowenterEventHandler onrowenter;

		// Token: 0x14002337 RID: 9015
		// (add) Token: 0x06012237 RID: 74295
		// (remove) Token: 0x06012238 RID: 74296
		event HTMLScriptEvents2_ondatasetchangedEventHandler ondatasetchanged;

		// Token: 0x14002338 RID: 9016
		// (add) Token: 0x06012239 RID: 74297
		// (remove) Token: 0x0601223A RID: 74298
		event HTMLScriptEvents2_ondataavailableEventHandler ondataavailable;

		// Token: 0x14002339 RID: 9017
		// (add) Token: 0x0601223B RID: 74299
		// (remove) Token: 0x0601223C RID: 74300
		event HTMLScriptEvents2_ondatasetcompleteEventHandler ondatasetcomplete;

		// Token: 0x1400233A RID: 9018
		// (add) Token: 0x0601223D RID: 74301
		// (remove) Token: 0x0601223E RID: 74302
		event HTMLScriptEvents2_onlosecaptureEventHandler onlosecapture;

		// Token: 0x1400233B RID: 9019
		// (add) Token: 0x0601223F RID: 74303
		// (remove) Token: 0x06012240 RID: 74304
		event HTMLScriptEvents2_onpropertychangeEventHandler onpropertychange;

		// Token: 0x1400233C RID: 9020
		// (add) Token: 0x06012241 RID: 74305
		// (remove) Token: 0x06012242 RID: 74306
		event HTMLScriptEvents2_onscrollEventHandler onscroll;

		// Token: 0x1400233D RID: 9021
		// (add) Token: 0x06012243 RID: 74307
		// (remove) Token: 0x06012244 RID: 74308
		event HTMLScriptEvents2_onfocusEventHandler onfocus;

		// Token: 0x1400233E RID: 9022
		// (add) Token: 0x06012245 RID: 74309
		// (remove) Token: 0x06012246 RID: 74310
		event HTMLScriptEvents2_onblurEventHandler onblur;

		// Token: 0x1400233F RID: 9023
		// (add) Token: 0x06012247 RID: 74311
		// (remove) Token: 0x06012248 RID: 74312
		event HTMLScriptEvents2_onresizeEventHandler onresize;

		// Token: 0x14002340 RID: 9024
		// (add) Token: 0x06012249 RID: 74313
		// (remove) Token: 0x0601224A RID: 74314
		event HTMLScriptEvents2_ondragEventHandler ondrag;

		// Token: 0x14002341 RID: 9025
		// (add) Token: 0x0601224B RID: 74315
		// (remove) Token: 0x0601224C RID: 74316
		event HTMLScriptEvents2_ondragendEventHandler ondragend;

		// Token: 0x14002342 RID: 9026
		// (add) Token: 0x0601224D RID: 74317
		// (remove) Token: 0x0601224E RID: 74318
		event HTMLScriptEvents2_ondragenterEventHandler ondragenter;

		// Token: 0x14002343 RID: 9027
		// (add) Token: 0x0601224F RID: 74319
		// (remove) Token: 0x06012250 RID: 74320
		event HTMLScriptEvents2_ondragoverEventHandler ondragover;

		// Token: 0x14002344 RID: 9028
		// (add) Token: 0x06012251 RID: 74321
		// (remove) Token: 0x06012252 RID: 74322
		event HTMLScriptEvents2_ondragleaveEventHandler ondragleave;

		// Token: 0x14002345 RID: 9029
		// (add) Token: 0x06012253 RID: 74323
		// (remove) Token: 0x06012254 RID: 74324
		event HTMLScriptEvents2_ondropEventHandler ondrop;

		// Token: 0x14002346 RID: 9030
		// (add) Token: 0x06012255 RID: 74325
		// (remove) Token: 0x06012256 RID: 74326
		event HTMLScriptEvents2_onbeforecutEventHandler onbeforecut;

		// Token: 0x14002347 RID: 9031
		// (add) Token: 0x06012257 RID: 74327
		// (remove) Token: 0x06012258 RID: 74328
		event HTMLScriptEvents2_oncutEventHandler oncut;

		// Token: 0x14002348 RID: 9032
		// (add) Token: 0x06012259 RID: 74329
		// (remove) Token: 0x0601225A RID: 74330
		event HTMLScriptEvents2_onbeforecopyEventHandler onbeforecopy;

		// Token: 0x14002349 RID: 9033
		// (add) Token: 0x0601225B RID: 74331
		// (remove) Token: 0x0601225C RID: 74332
		event HTMLScriptEvents2_oncopyEventHandler oncopy;

		// Token: 0x1400234A RID: 9034
		// (add) Token: 0x0601225D RID: 74333
		// (remove) Token: 0x0601225E RID: 74334
		event HTMLScriptEvents2_onbeforepasteEventHandler onbeforepaste;

		// Token: 0x1400234B RID: 9035
		// (add) Token: 0x0601225F RID: 74335
		// (remove) Token: 0x06012260 RID: 74336
		event HTMLScriptEvents2_onpasteEventHandler onpaste;

		// Token: 0x1400234C RID: 9036
		// (add) Token: 0x06012261 RID: 74337
		// (remove) Token: 0x06012262 RID: 74338
		event HTMLScriptEvents2_oncontextmenuEventHandler oncontextmenu;

		// Token: 0x1400234D RID: 9037
		// (add) Token: 0x06012263 RID: 74339
		// (remove) Token: 0x06012264 RID: 74340
		event HTMLScriptEvents2_onrowsdeleteEventHandler onrowsdelete;

		// Token: 0x1400234E RID: 9038
		// (add) Token: 0x06012265 RID: 74341
		// (remove) Token: 0x06012266 RID: 74342
		event HTMLScriptEvents2_onrowsinsertedEventHandler onrowsinserted;

		// Token: 0x1400234F RID: 9039
		// (add) Token: 0x06012267 RID: 74343
		// (remove) Token: 0x06012268 RID: 74344
		event HTMLScriptEvents2_oncellchangeEventHandler oncellchange;

		// Token: 0x14002350 RID: 9040
		// (add) Token: 0x06012269 RID: 74345
		// (remove) Token: 0x0601226A RID: 74346
		event HTMLScriptEvents2_onreadystatechangeEventHandler onreadystatechange;

		// Token: 0x14002351 RID: 9041
		// (add) Token: 0x0601226B RID: 74347
		// (remove) Token: 0x0601226C RID: 74348
		event HTMLScriptEvents2_onlayoutcompleteEventHandler onlayoutcomplete;

		// Token: 0x14002352 RID: 9042
		// (add) Token: 0x0601226D RID: 74349
		// (remove) Token: 0x0601226E RID: 74350
		event HTMLScriptEvents2_onpageEventHandler onpage;

		// Token: 0x14002353 RID: 9043
		// (add) Token: 0x0601226F RID: 74351
		// (remove) Token: 0x06012270 RID: 74352
		event HTMLScriptEvents2_onmouseenterEventHandler onmouseenter;

		// Token: 0x14002354 RID: 9044
		// (add) Token: 0x06012271 RID: 74353
		// (remove) Token: 0x06012272 RID: 74354
		event HTMLScriptEvents2_onmouseleaveEventHandler onmouseleave;

		// Token: 0x14002355 RID: 9045
		// (add) Token: 0x06012273 RID: 74355
		// (remove) Token: 0x06012274 RID: 74356
		event HTMLScriptEvents2_onactivateEventHandler onactivate;

		// Token: 0x14002356 RID: 9046
		// (add) Token: 0x06012275 RID: 74357
		// (remove) Token: 0x06012276 RID: 74358
		event HTMLScriptEvents2_ondeactivateEventHandler ondeactivate;

		// Token: 0x14002357 RID: 9047
		// (add) Token: 0x06012277 RID: 74359
		// (remove) Token: 0x06012278 RID: 74360
		event HTMLScriptEvents2_onbeforedeactivateEventHandler onbeforedeactivate;

		// Token: 0x14002358 RID: 9048
		// (add) Token: 0x06012279 RID: 74361
		// (remove) Token: 0x0601227A RID: 74362
		event HTMLScriptEvents2_onbeforeactivateEventHandler onbeforeactivate;

		// Token: 0x14002359 RID: 9049
		// (add) Token: 0x0601227B RID: 74363
		// (remove) Token: 0x0601227C RID: 74364
		event HTMLScriptEvents2_onfocusinEventHandler onfocusin;

		// Token: 0x1400235A RID: 9050
		// (add) Token: 0x0601227D RID: 74365
		// (remove) Token: 0x0601227E RID: 74366
		event HTMLScriptEvents2_onfocusoutEventHandler onfocusout;

		// Token: 0x1400235B RID: 9051
		// (add) Token: 0x0601227F RID: 74367
		// (remove) Token: 0x06012280 RID: 74368
		event HTMLScriptEvents2_onmoveEventHandler onmove;

		// Token: 0x1400235C RID: 9052
		// (add) Token: 0x06012281 RID: 74369
		// (remove) Token: 0x06012282 RID: 74370
		event HTMLScriptEvents2_oncontrolselectEventHandler oncontrolselect;

		// Token: 0x1400235D RID: 9053
		// (add) Token: 0x06012283 RID: 74371
		// (remove) Token: 0x06012284 RID: 74372
		event HTMLScriptEvents2_onmovestartEventHandler onmovestart;

		// Token: 0x1400235E RID: 9054
		// (add) Token: 0x06012285 RID: 74373
		// (remove) Token: 0x06012286 RID: 74374
		event HTMLScriptEvents2_onmoveendEventHandler onmoveend;

		// Token: 0x1400235F RID: 9055
		// (add) Token: 0x06012287 RID: 74375
		// (remove) Token: 0x06012288 RID: 74376
		event HTMLScriptEvents2_onresizestartEventHandler onresizestart;

		// Token: 0x14002360 RID: 9056
		// (add) Token: 0x06012289 RID: 74377
		// (remove) Token: 0x0601228A RID: 74378
		event HTMLScriptEvents2_onresizeendEventHandler onresizeend;

		// Token: 0x14002361 RID: 9057
		// (add) Token: 0x0601228B RID: 74379
		// (remove) Token: 0x0601228C RID: 74380
		event HTMLScriptEvents2_onmousewheelEventHandler onmousewheel;

		// Token: 0x14002362 RID: 9058
		// (add) Token: 0x0601228D RID: 74381
		// (remove) Token: 0x0601228E RID: 74382
		event HTMLScriptEvents2_onerrorEventHandler onerror;
	}
}

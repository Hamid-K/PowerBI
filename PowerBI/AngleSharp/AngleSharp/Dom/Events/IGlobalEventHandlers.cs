using System;
using AngleSharp.Attributes;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F5 RID: 501
	[DomName("GlobalEventHandlers")]
	[DomNoInterfaceObject]
	public interface IGlobalEventHandlers
	{
		// Token: 0x140000B0 RID: 176
		// (add) Token: 0x06001097 RID: 4247
		// (remove) Token: 0x06001098 RID: 4248
		[DomName("onabort")]
		event DomEventHandler Aborted;

		// Token: 0x140000B1 RID: 177
		// (add) Token: 0x06001099 RID: 4249
		// (remove) Token: 0x0600109A RID: 4250
		[DomName("onblur")]
		event DomEventHandler Blurred;

		// Token: 0x140000B2 RID: 178
		// (add) Token: 0x0600109B RID: 4251
		// (remove) Token: 0x0600109C RID: 4252
		[DomName("oncancel")]
		event DomEventHandler Cancelled;

		// Token: 0x140000B3 RID: 179
		// (add) Token: 0x0600109D RID: 4253
		// (remove) Token: 0x0600109E RID: 4254
		[DomName("oncanplay")]
		event DomEventHandler CanPlay;

		// Token: 0x140000B4 RID: 180
		// (add) Token: 0x0600109F RID: 4255
		// (remove) Token: 0x060010A0 RID: 4256
		[DomName("oncanplaythrough")]
		event DomEventHandler CanPlayThrough;

		// Token: 0x140000B5 RID: 181
		// (add) Token: 0x060010A1 RID: 4257
		// (remove) Token: 0x060010A2 RID: 4258
		[DomName("onchange")]
		event DomEventHandler Changed;

		// Token: 0x140000B6 RID: 182
		// (add) Token: 0x060010A3 RID: 4259
		// (remove) Token: 0x060010A4 RID: 4260
		[DomName("onclick")]
		event DomEventHandler Clicked;

		// Token: 0x140000B7 RID: 183
		// (add) Token: 0x060010A5 RID: 4261
		// (remove) Token: 0x060010A6 RID: 4262
		[DomName("oncuechange")]
		event DomEventHandler CueChanged;

		// Token: 0x140000B8 RID: 184
		// (add) Token: 0x060010A7 RID: 4263
		// (remove) Token: 0x060010A8 RID: 4264
		[DomName("ondblclick")]
		event DomEventHandler DoubleClick;

		// Token: 0x140000B9 RID: 185
		// (add) Token: 0x060010A9 RID: 4265
		// (remove) Token: 0x060010AA RID: 4266
		[DomName("ondrag")]
		event DomEventHandler Drag;

		// Token: 0x140000BA RID: 186
		// (add) Token: 0x060010AB RID: 4267
		// (remove) Token: 0x060010AC RID: 4268
		[DomName("ondragend")]
		event DomEventHandler DragEnd;

		// Token: 0x140000BB RID: 187
		// (add) Token: 0x060010AD RID: 4269
		// (remove) Token: 0x060010AE RID: 4270
		[DomName("ondragenter")]
		event DomEventHandler DragEnter;

		// Token: 0x140000BC RID: 188
		// (add) Token: 0x060010AF RID: 4271
		// (remove) Token: 0x060010B0 RID: 4272
		[DomName("ondragexit")]
		event DomEventHandler DragExit;

		// Token: 0x140000BD RID: 189
		// (add) Token: 0x060010B1 RID: 4273
		// (remove) Token: 0x060010B2 RID: 4274
		[DomName("ondragleave")]
		event DomEventHandler DragLeave;

		// Token: 0x140000BE RID: 190
		// (add) Token: 0x060010B3 RID: 4275
		// (remove) Token: 0x060010B4 RID: 4276
		[DomName("ondragover")]
		event DomEventHandler DragOver;

		// Token: 0x140000BF RID: 191
		// (add) Token: 0x060010B5 RID: 4277
		// (remove) Token: 0x060010B6 RID: 4278
		[DomName("ondragstart")]
		event DomEventHandler DragStart;

		// Token: 0x140000C0 RID: 192
		// (add) Token: 0x060010B7 RID: 4279
		// (remove) Token: 0x060010B8 RID: 4280
		[DomName("ondrop")]
		event DomEventHandler Dropped;

		// Token: 0x140000C1 RID: 193
		// (add) Token: 0x060010B9 RID: 4281
		// (remove) Token: 0x060010BA RID: 4282
		[DomName("ondurationchange")]
		event DomEventHandler DurationChanged;

		// Token: 0x140000C2 RID: 194
		// (add) Token: 0x060010BB RID: 4283
		// (remove) Token: 0x060010BC RID: 4284
		[DomName("onemptied")]
		event DomEventHandler Emptied;

		// Token: 0x140000C3 RID: 195
		// (add) Token: 0x060010BD RID: 4285
		// (remove) Token: 0x060010BE RID: 4286
		[DomName("onended")]
		event DomEventHandler Ended;

		// Token: 0x140000C4 RID: 196
		// (add) Token: 0x060010BF RID: 4287
		// (remove) Token: 0x060010C0 RID: 4288
		[DomName("onerror")]
		event DomEventHandler Error;

		// Token: 0x140000C5 RID: 197
		// (add) Token: 0x060010C1 RID: 4289
		// (remove) Token: 0x060010C2 RID: 4290
		[DomName("onfocus")]
		event DomEventHandler Focused;

		// Token: 0x140000C6 RID: 198
		// (add) Token: 0x060010C3 RID: 4291
		// (remove) Token: 0x060010C4 RID: 4292
		[DomName("oninput")]
		event DomEventHandler Input;

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x060010C5 RID: 4293
		// (remove) Token: 0x060010C6 RID: 4294
		[DomName("oninvalid")]
		event DomEventHandler Invalid;

		// Token: 0x140000C8 RID: 200
		// (add) Token: 0x060010C7 RID: 4295
		// (remove) Token: 0x060010C8 RID: 4296
		[DomName("onkeydown")]
		event DomEventHandler KeyDown;

		// Token: 0x140000C9 RID: 201
		// (add) Token: 0x060010C9 RID: 4297
		// (remove) Token: 0x060010CA RID: 4298
		[DomName("onkeypress")]
		event DomEventHandler KeyPress;

		// Token: 0x140000CA RID: 202
		// (add) Token: 0x060010CB RID: 4299
		// (remove) Token: 0x060010CC RID: 4300
		[DomName("onkeyup")]
		event DomEventHandler KeyUp;

		// Token: 0x140000CB RID: 203
		// (add) Token: 0x060010CD RID: 4301
		// (remove) Token: 0x060010CE RID: 4302
		[DomName("onload")]
		event DomEventHandler Loaded;

		// Token: 0x140000CC RID: 204
		// (add) Token: 0x060010CF RID: 4303
		// (remove) Token: 0x060010D0 RID: 4304
		[DomName("onloadeddata")]
		event DomEventHandler LoadedData;

		// Token: 0x140000CD RID: 205
		// (add) Token: 0x060010D1 RID: 4305
		// (remove) Token: 0x060010D2 RID: 4306
		[DomName("onloadedmetadata")]
		event DomEventHandler LoadedMetadata;

		// Token: 0x140000CE RID: 206
		// (add) Token: 0x060010D3 RID: 4307
		// (remove) Token: 0x060010D4 RID: 4308
		[DomName("onloadstart")]
		event DomEventHandler Loading;

		// Token: 0x140000CF RID: 207
		// (add) Token: 0x060010D5 RID: 4309
		// (remove) Token: 0x060010D6 RID: 4310
		[DomName("onmousedown")]
		event DomEventHandler MouseDown;

		// Token: 0x140000D0 RID: 208
		// (add) Token: 0x060010D7 RID: 4311
		// (remove) Token: 0x060010D8 RID: 4312
		[DomLenientThis]
		[DomName("onmouseenter")]
		event DomEventHandler MouseEnter;

		// Token: 0x140000D1 RID: 209
		// (add) Token: 0x060010D9 RID: 4313
		// (remove) Token: 0x060010DA RID: 4314
		[DomLenientThis]
		[DomName("onmouseleave")]
		event DomEventHandler MouseLeave;

		// Token: 0x140000D2 RID: 210
		// (add) Token: 0x060010DB RID: 4315
		// (remove) Token: 0x060010DC RID: 4316
		[DomName("onmousemove")]
		event DomEventHandler MouseMove;

		// Token: 0x140000D3 RID: 211
		// (add) Token: 0x060010DD RID: 4317
		// (remove) Token: 0x060010DE RID: 4318
		[DomName("onmouseout")]
		event DomEventHandler MouseOut;

		// Token: 0x140000D4 RID: 212
		// (add) Token: 0x060010DF RID: 4319
		// (remove) Token: 0x060010E0 RID: 4320
		[DomName("onmouseover")]
		event DomEventHandler MouseOver;

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x060010E1 RID: 4321
		// (remove) Token: 0x060010E2 RID: 4322
		[DomName("onmouseup")]
		event DomEventHandler MouseUp;

		// Token: 0x140000D6 RID: 214
		// (add) Token: 0x060010E3 RID: 4323
		// (remove) Token: 0x060010E4 RID: 4324
		[DomName("onmousewheel")]
		event DomEventHandler MouseWheel;

		// Token: 0x140000D7 RID: 215
		// (add) Token: 0x060010E5 RID: 4325
		// (remove) Token: 0x060010E6 RID: 4326
		[DomName("onpause")]
		event DomEventHandler Paused;

		// Token: 0x140000D8 RID: 216
		// (add) Token: 0x060010E7 RID: 4327
		// (remove) Token: 0x060010E8 RID: 4328
		[DomName("onplay")]
		event DomEventHandler Played;

		// Token: 0x140000D9 RID: 217
		// (add) Token: 0x060010E9 RID: 4329
		// (remove) Token: 0x060010EA RID: 4330
		[DomName("onplaying")]
		event DomEventHandler Playing;

		// Token: 0x140000DA RID: 218
		// (add) Token: 0x060010EB RID: 4331
		// (remove) Token: 0x060010EC RID: 4332
		[DomName("onprogress")]
		event DomEventHandler Progress;

		// Token: 0x140000DB RID: 219
		// (add) Token: 0x060010ED RID: 4333
		// (remove) Token: 0x060010EE RID: 4334
		[DomName("onratechange")]
		event DomEventHandler RateChanged;

		// Token: 0x140000DC RID: 220
		// (add) Token: 0x060010EF RID: 4335
		// (remove) Token: 0x060010F0 RID: 4336
		[DomName("onreset")]
		event DomEventHandler Resetted;

		// Token: 0x140000DD RID: 221
		// (add) Token: 0x060010F1 RID: 4337
		// (remove) Token: 0x060010F2 RID: 4338
		[DomName("onresize")]
		event DomEventHandler Resized;

		// Token: 0x140000DE RID: 222
		// (add) Token: 0x060010F3 RID: 4339
		// (remove) Token: 0x060010F4 RID: 4340
		[DomName("onscroll")]
		event DomEventHandler Scrolled;

		// Token: 0x140000DF RID: 223
		// (add) Token: 0x060010F5 RID: 4341
		// (remove) Token: 0x060010F6 RID: 4342
		[DomName("onseeked")]
		event DomEventHandler Seeked;

		// Token: 0x140000E0 RID: 224
		// (add) Token: 0x060010F7 RID: 4343
		// (remove) Token: 0x060010F8 RID: 4344
		[DomName("onseeking")]
		event DomEventHandler Seeking;

		// Token: 0x140000E1 RID: 225
		// (add) Token: 0x060010F9 RID: 4345
		// (remove) Token: 0x060010FA RID: 4346
		[DomName("onselect")]
		event DomEventHandler Selected;

		// Token: 0x140000E2 RID: 226
		// (add) Token: 0x060010FB RID: 4347
		// (remove) Token: 0x060010FC RID: 4348
		[DomName("onshow")]
		event DomEventHandler Shown;

		// Token: 0x140000E3 RID: 227
		// (add) Token: 0x060010FD RID: 4349
		// (remove) Token: 0x060010FE RID: 4350
		[DomName("onstalled")]
		event DomEventHandler Stalled;

		// Token: 0x140000E4 RID: 228
		// (add) Token: 0x060010FF RID: 4351
		// (remove) Token: 0x06001100 RID: 4352
		[DomName("onsubmit")]
		event DomEventHandler Submitted;

		// Token: 0x140000E5 RID: 229
		// (add) Token: 0x06001101 RID: 4353
		// (remove) Token: 0x06001102 RID: 4354
		[DomName("onsuspend")]
		event DomEventHandler Suspended;

		// Token: 0x140000E6 RID: 230
		// (add) Token: 0x06001103 RID: 4355
		// (remove) Token: 0x06001104 RID: 4356
		[DomName("ontimeupdate")]
		event DomEventHandler TimeUpdated;

		// Token: 0x140000E7 RID: 231
		// (add) Token: 0x06001105 RID: 4357
		// (remove) Token: 0x06001106 RID: 4358
		[DomName("ontoggle")]
		event DomEventHandler Toggled;

		// Token: 0x140000E8 RID: 232
		// (add) Token: 0x06001107 RID: 4359
		// (remove) Token: 0x06001108 RID: 4360
		[DomName("onvolumechange")]
		event DomEventHandler VolumeChanged;

		// Token: 0x140000E9 RID: 233
		// (add) Token: 0x06001109 RID: 4361
		// (remove) Token: 0x0600110A RID: 4362
		[DomName("onwaiting")]
		event DomEventHandler Waiting;
	}
}

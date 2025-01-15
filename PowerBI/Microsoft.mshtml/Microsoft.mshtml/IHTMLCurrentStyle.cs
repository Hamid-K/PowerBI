using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace mshtml
{
	// Token: 0x02000060 RID: 96
	[Guid("3050F3DB-98B5-11CF-BB82-00AA00BDCE0B")]
	[TypeLibType(4160)]
	[ComImport]
	public interface IHTMLCurrentStyle
	{
		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x0600091D RID: 2333
		[DispId(-2147413022)]
		string position
		{
			[TypeLibFunc(20)]
			[DispId(-2147413022)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x0600091E RID: 2334
		[DispId(-2147413042)]
		string styleFloat
		{
			[DispId(-2147413042)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x0600091F RID: 2335
		[DispId(-2147413110)]
		object color
		{
			[TypeLibFunc(20)]
			[DispId(-2147413110)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06000920 RID: 2336
		[DispId(-501)]
		object backgroundColor
		{
			[TypeLibFunc(20)]
			[DispId(-501)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06000921 RID: 2337
		[DispId(-2147413094)]
		string fontFamily
		{
			[DispId(-2147413094)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000922 RID: 2338
		[DispId(-2147413088)]
		string fontStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413088)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000923 RID: 2339
		[DispId(-2147413087)]
		string fontVariant
		{
			[DispId(-2147413087)]
			[TypeLibFunc(84)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000924 RID: 2340
		[DispId(-2147413085)]
		object fontWeight
		{
			[DispId(-2147413085)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000925 RID: 2341
		[DispId(-2147413093)]
		object fontSize
		{
			[DispId(-2147413093)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000926 RID: 2342
		[DispId(-2147413111)]
		string backgroundImage
		{
			[DispId(-2147413111)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000927 RID: 2343
		[DispId(-2147413079)]
		object backgroundPositionX
		{
			[DispId(-2147413079)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000928 RID: 2344
		[DispId(-2147413078)]
		object backgroundPositionY
		{
			[DispId(-2147413078)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000929 RID: 2345
		[DispId(-2147413068)]
		string backgroundRepeat
		{
			[TypeLibFunc(20)]
			[DispId(-2147413068)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x0600092A RID: 2346
		[DispId(-2147413054)]
		object borderLeftColor
		{
			[DispId(-2147413054)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700048D RID: 1165
		// (get) Token: 0x0600092B RID: 2347
		[DispId(-2147413057)]
		object borderTopColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413057)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700048E RID: 1166
		// (get) Token: 0x0600092C RID: 2348
		[DispId(-2147413056)]
		object borderRightColor
		{
			[TypeLibFunc(20)]
			[DispId(-2147413056)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700048F RID: 1167
		// (get) Token: 0x0600092D RID: 2349
		[DispId(-2147413055)]
		object borderBottomColor
		{
			[DispId(-2147413055)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000490 RID: 1168
		// (get) Token: 0x0600092E RID: 2350
		[DispId(-2147413047)]
		string borderTopStyle
		{
			[DispId(-2147413047)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000491 RID: 1169
		// (get) Token: 0x0600092F RID: 2351
		[DispId(-2147413046)]
		string borderRightStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413046)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000492 RID: 1170
		// (get) Token: 0x06000930 RID: 2352
		[DispId(-2147413045)]
		string borderBottomStyle
		{
			[DispId(-2147413045)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06000931 RID: 2353
		[DispId(-2147413044)]
		string borderLeftStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413044)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06000932 RID: 2354
		[DispId(-2147413052)]
		object borderTopWidth
		{
			[DispId(-2147413052)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x06000933 RID: 2355
		[DispId(-2147413051)]
		object borderRightWidth
		{
			[DispId(-2147413051)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x06000934 RID: 2356
		[DispId(-2147413050)]
		object borderBottomWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413050)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000935 RID: 2357
		[DispId(-2147413049)]
		object borderLeftWidth
		{
			[TypeLibFunc(20)]
			[DispId(-2147413049)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000936 RID: 2358
		[DispId(-2147418109)]
		object left
		{
			[DispId(-2147418109)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000937 RID: 2359
		[DispId(-2147418108)]
		object top
		{
			[TypeLibFunc(20)]
			[DispId(-2147418108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000938 RID: 2360
		[DispId(-2147418107)]
		object width
		{
			[DispId(-2147418107)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049B RID: 1179
		// (get) Token: 0x06000939 RID: 2361
		[DispId(-2147418106)]
		object height
		{
			[DispId(-2147418106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x0600093A RID: 2362
		[DispId(-2147413097)]
		object paddingLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413097)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049D RID: 1181
		// (get) Token: 0x0600093B RID: 2363
		[DispId(-2147413100)]
		object paddingTop
		{
			[DispId(-2147413100)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049E RID: 1182
		// (get) Token: 0x0600093C RID: 2364
		[DispId(-2147413099)]
		object paddingRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413099)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x1700049F RID: 1183
		// (get) Token: 0x0600093D RID: 2365
		[DispId(-2147413098)]
		object paddingBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413098)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x0600093E RID: 2366
		[DispId(-2147418040)]
		string textAlign
		{
			[DispId(-2147418040)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x0600093F RID: 2367
		[DispId(-2147413077)]
		string textDecoration
		{
			[TypeLibFunc(20)]
			[DispId(-2147413077)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x06000940 RID: 2368
		[DispId(-2147413041)]
		string display
		{
			[TypeLibFunc(20)]
			[DispId(-2147413041)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004A3 RID: 1187
		// (get) Token: 0x06000941 RID: 2369
		[DispId(-2147413032)]
		string visibility
		{
			[TypeLibFunc(20)]
			[DispId(-2147413032)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004A4 RID: 1188
		// (get) Token: 0x06000942 RID: 2370
		[DispId(-2147413021)]
		object zIndex
		{
			[TypeLibFunc(20)]
			[DispId(-2147413021)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A5 RID: 1189
		// (get) Token: 0x06000943 RID: 2371
		[DispId(-2147413104)]
		object letterSpacing
		{
			[TypeLibFunc(20)]
			[DispId(-2147413104)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A6 RID: 1190
		// (get) Token: 0x06000944 RID: 2372
		[DispId(-2147413106)]
		object lineHeight
		{
			[DispId(-2147413106)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06000945 RID: 2373
		[DispId(-2147413105)]
		object textIndent
		{
			[DispId(-2147413105)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06000946 RID: 2374
		[DispId(-2147413064)]
		object verticalAlign
		{
			[TypeLibFunc(20)]
			[DispId(-2147413064)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06000947 RID: 2375
		[DispId(-2147413067)]
		string backgroundAttachment
		{
			[TypeLibFunc(20)]
			[DispId(-2147413067)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06000948 RID: 2376
		[DispId(-2147413075)]
		object marginTop
		{
			[DispId(-2147413075)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x06000949 RID: 2377
		[DispId(-2147413074)]
		object marginRight
		{
			[TypeLibFunc(20)]
			[DispId(-2147413074)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x0600094A RID: 2378
		[DispId(-2147413073)]
		object marginBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413073)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004AD RID: 1197
		// (get) Token: 0x0600094B RID: 2379
		[DispId(-2147413072)]
		object marginLeft
		{
			[DispId(-2147413072)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004AE RID: 1198
		// (get) Token: 0x0600094C RID: 2380
		[DispId(-2147413096)]
		string clear
		{
			[DispId(-2147413096)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004AF RID: 1199
		// (get) Token: 0x0600094D RID: 2381
		[DispId(-2147413040)]
		string listStyleType
		{
			[TypeLibFunc(20)]
			[DispId(-2147413040)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B0 RID: 1200
		// (get) Token: 0x0600094E RID: 2382
		[DispId(-2147413039)]
		string listStylePosition
		{
			[DispId(-2147413039)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B1 RID: 1201
		// (get) Token: 0x0600094F RID: 2383
		[DispId(-2147413038)]
		string listStyleImage
		{
			[DispId(-2147413038)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B2 RID: 1202
		// (get) Token: 0x06000950 RID: 2384
		[DispId(-2147413019)]
		object clipTop
		{
			[DispId(-2147413019)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004B3 RID: 1203
		// (get) Token: 0x06000951 RID: 2385
		[DispId(-2147413018)]
		object clipRight
		{
			[DispId(-2147413018)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004B4 RID: 1204
		// (get) Token: 0x06000952 RID: 2386
		[DispId(-2147413017)]
		object clipBottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147413017)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004B5 RID: 1205
		// (get) Token: 0x06000953 RID: 2387
		[DispId(-2147413016)]
		object clipLeft
		{
			[TypeLibFunc(20)]
			[DispId(-2147413016)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x06000954 RID: 2388
		[DispId(-2147413102)]
		string overflow
		{
			[DispId(-2147413102)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B7 RID: 1207
		// (get) Token: 0x06000955 RID: 2389
		[DispId(-2147413035)]
		string pageBreakBefore
		{
			[DispId(-2147413035)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B8 RID: 1208
		// (get) Token: 0x06000956 RID: 2390
		[DispId(-2147413034)]
		string pageBreakAfter
		{
			[TypeLibFunc(20)]
			[DispId(-2147413034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x06000957 RID: 2391
		[DispId(-2147413010)]
		string cursor
		{
			[DispId(-2147413010)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06000958 RID: 2392
		[DispId(-2147413014)]
		string tableLayout
		{
			[DispId(-2147413014)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004BB RID: 1211
		// (get) Token: 0x06000959 RID: 2393
		[DispId(-2147413028)]
		string borderCollapse
		{
			[DispId(-2147413028)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004BC RID: 1212
		// (get) Token: 0x0600095A RID: 2394
		[DispId(-2147412993)]
		string direction
		{
			[TypeLibFunc(20)]
			[DispId(-2147412993)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004BD RID: 1213
		// (get) Token: 0x0600095B RID: 2395
		[DispId(-2147412997)]
		string behavior
		{
			[TypeLibFunc(20)]
			[DispId(-2147412997)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x0600095C RID: 2396
		[DispId(-2147417610)]
		[MethodImpl(4096, MethodCodeType = 3)]
		[return: MarshalAs(27)]
		object getAttribute([MarshalAs(19)] [In] string strAttributeName, [In] int lFlags = 0);

		// Token: 0x170004BE RID: 1214
		// (get) Token: 0x0600095D RID: 2397
		[DispId(-2147412994)]
		string unicodeBidi
		{
			[TypeLibFunc(20)]
			[DispId(-2147412994)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004BF RID: 1215
		// (get) Token: 0x0600095E RID: 2398
		[DispId(-2147418035)]
		object right
		{
			[TypeLibFunc(20)]
			[DispId(-2147418035)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004C0 RID: 1216
		// (get) Token: 0x0600095F RID: 2399
		[DispId(-2147418034)]
		object bottom
		{
			[TypeLibFunc(20)]
			[DispId(-2147418034)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004C1 RID: 1217
		// (get) Token: 0x06000960 RID: 2400
		[DispId(-2147412992)]
		string imeMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412992)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x06000961 RID: 2401
		[DispId(-2147412991)]
		string rubyAlign
		{
			[DispId(-2147412991)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x06000962 RID: 2402
		[DispId(-2147412990)]
		string rubyPosition
		{
			[TypeLibFunc(20)]
			[DispId(-2147412990)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C4 RID: 1220
		// (get) Token: 0x06000963 RID: 2403
		[DispId(-2147412989)]
		string rubyOverhang
		{
			[DispId(-2147412989)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C5 RID: 1221
		// (get) Token: 0x06000964 RID: 2404
		[DispId(-2147412980)]
		string textAutospace
		{
			[DispId(-2147412980)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C6 RID: 1222
		// (get) Token: 0x06000965 RID: 2405
		[DispId(-2147412979)]
		string lineBreak
		{
			[DispId(-2147412979)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000966 RID: 2406
		[DispId(-2147412978)]
		string wordBreak
		{
			[TypeLibFunc(20)]
			[DispId(-2147412978)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06000967 RID: 2407
		[DispId(-2147412977)]
		string textJustify
		{
			[TypeLibFunc(20)]
			[DispId(-2147412977)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06000968 RID: 2408
		[DispId(-2147412976)]
		string textJustifyTrim
		{
			[DispId(-2147412976)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06000969 RID: 2409
		[DispId(-2147412975)]
		object textKashida
		{
			[DispId(-2147412975)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x0600096A RID: 2410
		[DispId(-2147412995)]
		string blockDirection
		{
			[DispId(-2147412995)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x0600096B RID: 2411
		[DispId(-2147412985)]
		object layoutGridChar
		{
			[DispId(-2147412985)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x0600096C RID: 2412
		[DispId(-2147412984)]
		object layoutGridLine
		{
			[TypeLibFunc(20)]
			[DispId(-2147412984)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(27)]
			get;
		}

		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x0600096D RID: 2413
		[DispId(-2147412983)]
		string layoutGridMode
		{
			[TypeLibFunc(20)]
			[DispId(-2147412983)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x0600096E RID: 2414
		[DispId(-2147412982)]
		string layoutGridType
		{
			[DispId(-2147412982)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x0600096F RID: 2415
		[DispId(-2147413048)]
		string borderStyle
		{
			[TypeLibFunc(20)]
			[DispId(-2147413048)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000970 RID: 2416
		[DispId(-2147413058)]
		string borderColor
		{
			[DispId(-2147413058)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D2 RID: 1234
		// (get) Token: 0x06000971 RID: 2417
		[DispId(-2147413053)]
		string borderWidth
		{
			[DispId(-2147413053)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D3 RID: 1235
		// (get) Token: 0x06000972 RID: 2418
		[DispId(-2147413101)]
		string padding
		{
			[DispId(-2147413101)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D4 RID: 1236
		// (get) Token: 0x06000973 RID: 2419
		[DispId(-2147413076)]
		string margin
		{
			[DispId(-2147413076)]
			[TypeLibFunc(20)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D5 RID: 1237
		// (get) Token: 0x06000974 RID: 2420
		[DispId(-2147412965)]
		string accelerator
		{
			[TypeLibFunc(20)]
			[DispId(-2147412965)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D6 RID: 1238
		// (get) Token: 0x06000975 RID: 2421
		[DispId(-2147412973)]
		string overflowX
		{
			[TypeLibFunc(20)]
			[DispId(-2147412973)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D7 RID: 1239
		// (get) Token: 0x06000976 RID: 2422
		[DispId(-2147412972)]
		string overflowY
		{
			[TypeLibFunc(20)]
			[DispId(-2147412972)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000977 RID: 2423
		[DispId(-2147413108)]
		string textTransform
		{
			[TypeLibFunc(20)]
			[DispId(-2147413108)]
			[MethodImpl(4096, MethodCodeType = 3)]
			[return: MarshalAs(19)]
			get;
		}
	}
}

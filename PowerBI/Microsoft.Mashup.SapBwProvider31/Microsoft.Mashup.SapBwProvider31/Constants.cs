using System;

namespace Microsoft.Mashup.SapBwProvider
{
	// Token: 0x0200002D RID: 45
	internal static class Constants
	{
		// Token: 0x0400011A RID: 282
		public const string DatsFormat = "yyyyMMdd";

		// Token: 0x0400011B RID: 283
		public const string DatsMinDate = "00000000";

		// Token: 0x0400011C RID: 284
		public const string TimsFormat = "hhmmss";

		// Token: 0x0400011D RID: 285
		public const string MdxCreateObjectParameter = "MDX";

		// Token: 0x0400011E RID: 286
		public const string MdxCreateObjectBapi = "RSR_MDX_CREATE_OBJECT";

		// Token: 0x0400011F RID: 287
		public const string CreateObjectBapi = "BAPI_MDDATASET_CREATE_OBJECT";

		// Token: 0x04000120 RID: 288
		public const string SelectDataBapi = "BAPI_MDDATASET_SELECT_DATA";

		// Token: 0x04000121 RID: 289
		public const string MdxGetAxisInfoBapi = "RSR_MDX_GET_AXIS_INFO";

		// Token: 0x04000122 RID: 290
		public const string MdxGetAxisDataBapi = "RSR_MDX_GET_AXIS_DATA";

		// Token: 0x04000123 RID: 291
		public const string MdxGetAxisDataShortBapi = "RSR_MDX_GET_AXIS_DATA_SHORT";

		// Token: 0x04000124 RID: 292
		public const string MdxGetCellBapi = "RSR_MDX_GET_CELL_DATA";

		// Token: 0x04000125 RID: 293
		public const string GetAxisInfoBapi = "BAPI_MDDATASET_GET_AXIS_INFO";

		// Token: 0x04000126 RID: 294
		public const string GetAxisDataBapi = "BAPI_MDDATASET_GET_AXIS_DATA";

		// Token: 0x04000127 RID: 295
		public const string GetAxisDataShortBapi = "BAPI_MDDATASET_GET_AXIS_DATA_SHORT";

		// Token: 0x04000128 RID: 296
		public const string GetCellBapi = "BAPI_MDDATASET_GET_CELL_DATA";

		// Token: 0x04000129 RID: 297
		public const string GetCellInfoBapi = "BAPI_MDDATASET_GET_CELL_INFO";

		// Token: 0x0400012A RID: 298
		public const string MdxGetFlatDataBapi = "RSR_MDX_GET_FLAT_DATA";

		// Token: 0x0400012B RID: 299
		public const string MdxGetFsDataBapi = "RSR_MDX_GET_FS_DATA";

		// Token: 0x0400012C RID: 300
		public const string GetFlatDataBapi = "BAPI_MDDATASET_GET_FLAT_DATA";

		// Token: 0x0400012D RID: 301
		public const string GetFsDataBapi = "BAPI_MDDATASET_GET_FS_DATA";

		// Token: 0x0400012E RID: 302
		public const string GetStreamDataParameter = "STREAM";

		// Token: 0x0400012F RID: 303
		public const string GetStreamDataBapi = "BAPI_MDDATASET_GET_STREAMDATA";

		// Token: 0x04000130 RID: 304
		public const string GetStreamInfoBapi = "BAPI_MDDATASET_GET_STREAMINFO";

		// Token: 0x04000131 RID: 305
		public const string BxmlGetDataParameter = "XML";

		// Token: 0x04000132 RID: 306
		public const string BxmlGetDataBapi = "RSR_MDX_BXML_GET_DATA";

		// Token: 0x04000133 RID: 307
		public const string BxmlGetGzipDataBapi = "RSR_MDX_BXML_GET_GZIP_DATA";

		// Token: 0x04000134 RID: 308
		public const string BxmlGetInfoBapi = "RSR_MDX_BXML_GET_INFO";

		// Token: 0x04000135 RID: 309
		public const string BxmlSetBindingBapi = "RSR_MDX_BXML_SET_BINDING";

		// Token: 0x04000136 RID: 310
		public const string DeleteObjectBapi = "BAPI_MDDATASET_DELETE_OBJECT";

		// Token: 0x04000137 RID: 311
		public const string GetDimensionsBapi = "BAPI_MDPROVIDER_GET_DIMENSIONS";

		// Token: 0x04000138 RID: 312
		public const string GetCatalogsBapi = "BAPI_MDPROVIDER_GET_CATALOGS";

		// Token: 0x04000139 RID: 313
		public const string GetCubesBapi = "BAPI_MDPROVIDER_GET_CUBES";

		// Token: 0x0400013A RID: 314
		public const string GetMeasuresBapi = "BAPI_MDPROVIDER_GET_MEASURES";

		// Token: 0x0400013B RID: 315
		public const string GetHierarchysBapi = "BAPI_MDPROVIDER_GET_HIERARCHYS";

		// Token: 0x0400013C RID: 316
		public const string GetLevelsBapi = "BAPI_MDPROVIDER_GET_LEVELS";

		// Token: 0x0400013D RID: 317
		public const string GetPropertiesBapi = "BAPI_MDPROVIDER_GET_PROPERTIES";

		// Token: 0x0400013E RID: 318
		public const string GetMembersBapi = "BAPI_MDPROVIDER_GET_MEMBERS";

		// Token: 0x0400013F RID: 319
		public const string GetVariablesBapi = "BAPI_MDPROVIDER_GET_VARIABLES";

		// Token: 0x04000140 RID: 320
		public const string InfoObjectDetailBapi = "BAPI_IOBJ_GETDETAIL";

		// Token: 0x04000141 RID: 321
		public const string UserDetailBapi = "BAPI_USER_GET_DETAIL";

		// Token: 0x04000142 RID: 322
		public const string RfcReadTableBapi = "RFC_READ_TABLE";

		// Token: 0x04000143 RID: 323
		public const string FormattedValueProperty = "FORMATTED_VALUE";

		// Token: 0x04000144 RID: 324
		public const string UnitOfMeasure = "UNIT_OF_MEASURE";

		// Token: 0x04000145 RID: 325
		public const string FormatStringProperty = "FORMAT_STRING";

		// Token: 0x04000146 RID: 326
		public const string CellStatusProperty = "CELL_STATUS";

		// Token: 0x04000147 RID: 327
		public const string CurrencyProperty = "CURRENCY";

		// Token: 0x04000148 RID: 328
		public const string UnitProperty = "UNIT";

		// Token: 0x04000149 RID: 329
		public const string ValueProperty = "VALUE";

		// Token: 0x0400014A RID: 330
		public const char AbapTrue = 'X';

		// Token: 0x0400014B RID: 331
		public const char AbapActive = 'A';

		// Token: 0x0400014C RID: 332
		public const string DatasetId = "DATASETID";

		// Token: 0x0400014D RID: 333
		public const string DataTypeName = "DataTypeName";

		// Token: 0x0400014E RID: 334
		public const string SchemaTable = "SchemaTable";

		// Token: 0x0400014F RID: 335
		public const string ErrorTypeValue = "E";

		// Token: 0x04000150 RID: 336
		public const string AbortTypeValue = "A";

		// Token: 0x04000151 RID: 337
		public const int DefaultBatchSize = 50000;

		// Token: 0x04000152 RID: 338
		public const string Item = "item";

		// Token: 0x04000153 RID: 339
		public const string Lines = "lines";

		// Token: 0x04000154 RID: 340
		public const int FltpAsCharLength = 22;

		// Token: 0x04000155 RID: 341
		public const byte OpenNamespace = 42;

		// Token: 0x04000156 RID: 342
		public const byte StringPart = 43;

		// Token: 0x04000157 RID: 343
		public const byte DeclNamespace = 58;

		// Token: 0x04000158 RID: 344
		public const byte Element = 60;

		// Token: 0x04000159 RID: 345
		public const byte EndElement = 62;

		// Token: 0x0400015A RID: 346
		public const byte Header = 63;

		// Token: 0x0400015B RID: 347
		public const byte Attribute = 64;

		// Token: 0x0400015C RID: 348
		public const byte AttributeText = 65;

		// Token: 0x0400015D RID: 349
		public const byte TextNode = 84;

		// Token: 0x0400015E RID: 350
		public const byte Marker1 = 1;

		// Token: 0x0400015F RID: 351
		public const byte Marker2 = 2;

		// Token: 0x04000160 RID: 352
		public const byte Marker3 = 3;

		// Token: 0x04000161 RID: 353
		public const byte Marker4 = 4;

		// Token: 0x04000162 RID: 354
		public const byte Marker5 = 5;

		// Token: 0x04000163 RID: 355
		public const byte Marker6 = 6;

		// Token: 0x04000164 RID: 356
		public const byte Marker7 = 7;

		// Token: 0x04000165 RID: 357
		public const byte Marker8 = 8;

		// Token: 0x04000166 RID: 358
		public const byte StartOfHeading = 1;

		// Token: 0x04000167 RID: 359
		public const byte StartOfText = 2;

		// Token: 0x04000168 RID: 360
		public const byte EndOfText = 3;

		// Token: 0x04000169 RID: 361
		public const byte EndOfTransmission = 4;

		// Token: 0x0400016A RID: 362
		public const byte Enquiry = 5;

		// Token: 0x0400016B RID: 363
		public const byte Bell = 7;

		// Token: 0x0400016C RID: 364
		public const byte Dash = 45;

		// Token: 0x0400016D RID: 365
		public const byte Dot = 46;

		// Token: 0x0400016E RID: 366
		public const byte Zero = 48;

		// Token: 0x0400016F RID: 367
		public const byte One = 49;

		// Token: 0x04000170 RID: 368
		public const byte Seven = 55;

		// Token: 0x04000171 RID: 369
		public const byte Eight = 56;

		// Token: 0x04000172 RID: 370
		public const byte CapitalA = 65;

		// Token: 0x04000173 RID: 371
		public const byte CapitalB = 66;

		// Token: 0x04000174 RID: 372
		public const byte CapitalC = 67;

		// Token: 0x04000175 RID: 373
		public const byte CapitalE = 69;

		// Token: 0x04000176 RID: 374
		public const byte CapitalL = 76;

		// Token: 0x04000177 RID: 375
		public const byte CapitalM = 77;

		// Token: 0x04000178 RID: 376
		public const byte CapitalN = 78;

		// Token: 0x04000179 RID: 377
		public const byte CapitalR = 82;

		// Token: 0x0400017A RID: 378
		public const byte CapitalV = 86;

		// Token: 0x0400017B RID: 379
		public const byte CapitalX = 88;

		// Token: 0x0400017C RID: 380
		public const byte SmallA = 97;

		// Token: 0x0400017D RID: 381
		public const byte SmallB = 98;

		// Token: 0x0400017E RID: 382
		public const byte SmallE = 101;

		// Token: 0x0400017F RID: 383
		public const byte SmallF = 102;

		// Token: 0x04000180 RID: 384
		public const byte SmallH = 104;

		// Token: 0x04000181 RID: 385
		public const byte SmallI = 105;

		// Token: 0x04000182 RID: 386
		public const byte SmallL = 108;

		// Token: 0x04000183 RID: 387
		public const byte SmallN = 110;

		// Token: 0x04000184 RID: 388
		public const byte SmallO = 111;

		// Token: 0x04000185 RID: 389
		public const byte SmallP = 112;

		// Token: 0x04000186 RID: 390
		public const byte SmallQ = 113;

		// Token: 0x04000187 RID: 391
		public const byte SmallR = 114;

		// Token: 0x04000188 RID: 392
		public const byte SmallS = 115;

		// Token: 0x04000189 RID: 393
		public const byte SmallT = 116;

		// Token: 0x0400018A RID: 394
		public const byte SmallU = 117;

		// Token: 0x0400018B RID: 395
		public const byte SmallV = 118;

		// Token: 0x0400018C RID: 396
		public const byte SmallX = 120;

		// Token: 0x0400018D RID: 397
		public const int Bits45678Dec = 31;

		// Token: 0x0400018E RID: 398
		public const int Bits45678Hex = 31;

		// Token: 0x0400018F RID: 399
		public const int Bits345678Dec = 63;

		// Token: 0x04000190 RID: 400
		public const int Bits345678Hex = 63;

		// Token: 0x04000191 RID: 401
		public const int Bit1Dec = 128;

		// Token: 0x04000192 RID: 402
		public const int Bit1Hex = 128;

		// Token: 0x04000193 RID: 403
		public const int Bits12Dec = 192;

		// Token: 0x04000194 RID: 404
		public const int Bits12Hex = 192;

		// Token: 0x04000195 RID: 405
		public const int Bits124Dec = 208;

		// Token: 0x04000196 RID: 406
		public const int Bits124Hex = 208;

		// Token: 0x04000197 RID: 407
		public const int Bits123Dec = 224;

		// Token: 0x04000198 RID: 408
		public const int Bits123Hex = 224;

		// Token: 0x04000199 RID: 409
		public const int Bits1234Dec = 240;

		// Token: 0x0400019A RID: 410
		public const int Bits1234Hex = 240;

		// Token: 0x0400019B RID: 411
		public const int Bits12345Dec = 248;

		// Token: 0x0400019C RID: 412
		public const int Bits12345Hex = 248;

		// Token: 0x0400019D RID: 413
		public const int Bits123456Dec = 252;

		// Token: 0x0400019E RID: 414
		public const int Bits123456Hex = 252;

		// Token: 0x0400019F RID: 415
		public const int Max1BitLengthDec = 128;

		// Token: 0x040001A0 RID: 416
		public const int Max1BitLengthHex = 128;

		// Token: 0x040001A1 RID: 417
		public const int Max2BitLengthDec = 2048;

		// Token: 0x040001A2 RID: 418
		public const int Max3BitLengthDec = 65536;

		// Token: 0x040001A3 RID: 419
		public const int Max4BitLengthDec = 2097152;

		// Token: 0x040001A4 RID: 420
		public const int Max5BitLengthDec = 67108864;
	}
}

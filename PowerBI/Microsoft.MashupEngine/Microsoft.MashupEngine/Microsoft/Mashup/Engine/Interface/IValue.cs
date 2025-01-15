using System;

namespace Microsoft.Mashup.Engine.Interface
{
	// Token: 0x020000E8 RID: 232
	public interface IValue
	{
		// Token: 0x1700013F RID: 319
		// (get) Token: 0x0600035E RID: 862
		ITypeValue Type { get; }

		// Token: 0x0600035F RID: 863
		IValue NewMeta(IRecordValue meta);

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000360 RID: 864
		IRecordValue MetaValue { get; }

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000361 RID: 865
		IValue SubtractMetaValue { get; }

		// Token: 0x06000362 RID: 866
		bool TryGetMetaField(string name, out IValue value);

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000363 RID: 867
		bool IsNumber { get; }

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x06000364 RID: 868
		bool IsText { get; }

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x06000365 RID: 869
		bool IsList { get; }

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x06000366 RID: 870
		bool IsTable { get; }

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x06000367 RID: 871
		bool IsBinary { get; }

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x06000368 RID: 872
		bool IsDate { get; }

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x06000369 RID: 873
		bool IsDateTime { get; }

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x0600036A RID: 874
		bool IsDateTimeZone { get; }

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x0600036B RID: 875
		bool IsTime { get; }

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x0600036C RID: 876
		bool IsNull { get; }

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x0600036D RID: 877
		bool IsLogical { get; }

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x0600036E RID: 878
		bool IsRecord { get; }

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x0600036F RID: 879
		bool IsFunction { get; }

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x06000370 RID: 880
		bool IsDuration { get; }

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x06000371 RID: 881
		bool IsType { get; }

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000372 RID: 882
		bool IsAction { get; }

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000373 RID: 883
		ITypeValue AsType { get; }

		// Token: 0x17000153 RID: 339
		// (get) Token: 0x06000374 RID: 884
		INumberValue AsNumber { get; }

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000375 RID: 885
		IDateValue AsDate { get; }

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000376 RID: 886
		IDateTime AsDateTime { get; }

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000377 RID: 887
		IDateTimeZone AsDateTimeZone { get; }

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000378 RID: 888
		ITimeValue AsTime { get; }

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000379 RID: 889
		IDurationValue AsDuration { get; }

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x0600037A RID: 890
		ITextValue AsText { get; }

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x0600037B RID: 891
		IListValue AsList { get; }

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x0600037C RID: 892
		ITableValue AsTable { get; }

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x0600037D RID: 893
		IBinaryValue AsBinary { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x0600037E RID: 894
		IRecordValue AsRecord { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x0600037F RID: 895
		IFunctionValue AsFunction { get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000380 RID: 896
		IActionValue AsAction { get; }

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x06000381 RID: 897
		bool AsBoolean { get; }

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x06000382 RID: 898
		string AsString { get; }

		// Token: 0x06000383 RID: 899
		IValue Concatenate(IValue value);

		// Token: 0x06000384 RID: 900
		IValue ReplaceType(ITypeValue type);

		// Token: 0x06000385 RID: 901
		string ToSource();
	}
}

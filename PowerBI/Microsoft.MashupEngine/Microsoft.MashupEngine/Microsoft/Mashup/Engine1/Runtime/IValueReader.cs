using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200135B RID: 4955
	public interface IValueReader
	{
		// Token: 0x17002327 RID: 8999
		// (get) Token: 0x06008237 RID: 33335
		Stream BaseStream { get; }

		// Token: 0x17002328 RID: 9000
		// (get) Token: 0x06008238 RID: 33336
		bool EndOfBinary { get; }

		// Token: 0x17002329 RID: 9001
		// (get) Token: 0x06008239 RID: 33337
		bool EndOfList { get; }

		// Token: 0x1700232A RID: 9002
		// (get) Token: 0x0600823A RID: 33338
		bool EndOfTable { get; }

		// Token: 0x0600823B RID: 33339
		void ReadStartValue(out ValueKind kind, out ValueFlags flags);

		// Token: 0x0600823C RID: 33340
		void ReadEndValue();

		// Token: 0x0600823D RID: 33341
		void ReadNull();

		// Token: 0x0600823E RID: 33342
		TimeSpan ReadTime();

		// Token: 0x0600823F RID: 33343
		DateTime ReadDate();

		// Token: 0x06008240 RID: 33344
		DateTime ReadDateTime();

		// Token: 0x06008241 RID: 33345
		DateTimeOffset ReadDateTimeZone();

		// Token: 0x06008242 RID: 33346
		TimeSpan ReadDuration();

		// Token: 0x06008243 RID: 33347
		NumberKind ReadNumberKind();

		// Token: 0x06008244 RID: 33348
		int ReadInt32Number();

		// Token: 0x06008245 RID: 33349
		decimal ReadDecimalNumber();

		// Token: 0x06008246 RID: 33350
		double ReadDoubleNumber();

		// Token: 0x06008247 RID: 33351
		bool ReadLogical();

		// Token: 0x06008248 RID: 33352
		string ReadText();

		// Token: 0x06008249 RID: 33353
		long ReadStartBinary();

		// Token: 0x0600824A RID: 33354
		byte[] ReadBinary(int count);

		// Token: 0x0600824B RID: 33355
		int ReadBinary(byte[] buffer, int index, int count);

		// Token: 0x0600824C RID: 33356
		void ReadEndBinary();

		// Token: 0x0600824D RID: 33357
		long ReadStartList();

		// Token: 0x0600824E RID: 33358
		void ReadEndList();

		// Token: 0x0600824F RID: 33359
		void ReadStartRecord(out Keys fieldNames);

		// Token: 0x06008250 RID: 33360
		void ReadEndRecord();

		// Token: 0x06008251 RID: 33361
		long ReadStartTable(out Keys columnNames);

		// Token: 0x06008252 RID: 33362
		void ReadStartRow();

		// Token: 0x06008253 RID: 33363
		void ReadEndRow();

		// Token: 0x06008254 RID: 33364
		void ReadEndTable();

		// Token: 0x06008255 RID: 33365
		void ReadFunction(out Keys paramNames, out int minParams);

		// Token: 0x06008256 RID: 33366
		void ReadAction();

		// Token: 0x06008257 RID: 33367
		void ReadStartType(out ValueKind kind, out bool nullable);

		// Token: 0x06008258 RID: 33368
		void ReadEndType();

		// Token: 0x06008259 RID: 33369
		void ReadStartRecordType(out Keys fieldNames, out bool open);

		// Token: 0x0600825A RID: 33370
		void ReadFieldType(out bool optional);

		// Token: 0x0600825B RID: 33371
		void ReadEndRecordType();

		// Token: 0x0600825C RID: 33372
		void ReadTableType(out TableKey[] keys);

		// Token: 0x0600825D RID: 33373
		void ReadStartFunctionType(out Keys paramNames, out int minParams);

		// Token: 0x0600825E RID: 33374
		void ReadEndFunctionType();

		// Token: 0x0600825F RID: 33375
		int ReadReference();
	}
}

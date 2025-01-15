using System;
using System.IO;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x0200135D RID: 4957
	public interface IValueWriter
	{
		// Token: 0x1700232D RID: 9005
		// (get) Token: 0x06008262 RID: 33378
		Stream BaseStream { get; }

		// Token: 0x06008263 RID: 33379
		void WriteStartValue(ValueKind kind, ValueFlags flags);

		// Token: 0x06008264 RID: 33380
		void WriteEndValue();

		// Token: 0x06008265 RID: 33381
		void WriteNull();

		// Token: 0x06008266 RID: 33382
		void WriteTime(TimeSpan time);

		// Token: 0x06008267 RID: 33383
		void WriteDate(DateTime date);

		// Token: 0x06008268 RID: 33384
		void WriteDateTime(DateTime dateTime);

		// Token: 0x06008269 RID: 33385
		void WriteDateTimeZone(DateTimeOffset dateTimeZone);

		// Token: 0x0600826A RID: 33386
		void WriteDuration(TimeSpan duration);

		// Token: 0x0600826B RID: 33387
		void WriteNumberKind(NumberKind numberKind);

		// Token: 0x0600826C RID: 33388
		void WriteNumber(int number);

		// Token: 0x0600826D RID: 33389
		void WriteNumber(decimal number);

		// Token: 0x0600826E RID: 33390
		void WriteNumber(double number);

		// Token: 0x0600826F RID: 33391
		void WriteLogical(bool logical);

		// Token: 0x06008270 RID: 33392
		void WriteText(string text);

		// Token: 0x06008271 RID: 33393
		void WriteStartBinary();

		// Token: 0x06008272 RID: 33394
		void WriteBinary(byte[] buffer);

		// Token: 0x06008273 RID: 33395
		void WriteBinary(byte[] buffer, int index, int count);

		// Token: 0x06008274 RID: 33396
		void WriteEndBinary();

		// Token: 0x06008275 RID: 33397
		void WriteStartList();

		// Token: 0x06008276 RID: 33398
		void WriteEndList();

		// Token: 0x06008277 RID: 33399
		void WriteStartRecord(Keys fieldNames);

		// Token: 0x06008278 RID: 33400
		void WriteEndRecord();

		// Token: 0x06008279 RID: 33401
		void WriteStartTable(Keys columnNames);

		// Token: 0x0600827A RID: 33402
		void WriteStartRow();

		// Token: 0x0600827B RID: 33403
		void WriteEndRow();

		// Token: 0x0600827C RID: 33404
		void WriteEndTable();

		// Token: 0x0600827D RID: 33405
		void WriteFunction(Keys paramNames, int minParams);

		// Token: 0x0600827E RID: 33406
		void WriteAction();

		// Token: 0x0600827F RID: 33407
		void WriteStartType(ValueKind kind, bool nullable);

		// Token: 0x06008280 RID: 33408
		void WriteEndType();

		// Token: 0x06008281 RID: 33409
		void WriteStartRecordType(Keys keys, bool open);

		// Token: 0x06008282 RID: 33410
		void WriteFieldType(bool optional);

		// Token: 0x06008283 RID: 33411
		void WriteEndRecordType();

		// Token: 0x06008284 RID: 33412
		void WriteTableType(TableKey[] keys);

		// Token: 0x06008285 RID: 33413
		void WriteStartFunctionType(Keys paramNames, int minParams);

		// Token: 0x06008286 RID: 33414
		void WriteEndFunctionType();

		// Token: 0x06008287 RID: 33415
		void WriteReference(int key);
	}
}

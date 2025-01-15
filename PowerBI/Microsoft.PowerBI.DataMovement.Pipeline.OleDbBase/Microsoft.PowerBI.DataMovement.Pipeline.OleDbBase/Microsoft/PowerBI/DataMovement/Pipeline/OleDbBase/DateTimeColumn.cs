using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000023 RID: 35
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class DateTimeColumn : Column<DateTime>
	{
		// Token: 0x0600010A RID: 266 RVA: 0x00003FF2 File Offset: 0x000021F2
		internal DateTimeColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600010B RID: 267 RVA: 0x00003FFB File Offset: 0x000021FB
		public override ColumnType Type
		{
			get
			{
				return ColumnType.DateTime;
			}
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003FFF File Offset: 0x000021FF
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(DateTime.FromOADate(*(double*)value));
		}

		// Token: 0x0600010D RID: 269 RVA: 0x0000400E File Offset: 0x0000220E
		public override DateTime GetDateTime(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00004018 File Offset: 0x00002218
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			DateTime value = base.GetValue(row);
			if (binding.DestType == DBTYPE.DATE || binding.DestType == DBTYPE.R8)
			{
				return base.ConvertDateTimeToDouble(value, destValue, out destLength);
			}
			if (binding.DestType == DBTYPE.VARIANT)
			{
				double? num = OleDbConvert.SafeToOADate(value);
				Variant.Init((VARIANT*)destValue);
				if (num != null)
				{
					double value2 = num.Value;
					((VARIANT*)destValue)->Type = VARTYPE.DATE;
					((VARIANT*)destValue)->Value64 = (ulong)(*(long*)(&value2));
				}
				else
				{
					((VARIANT*)destValue)->Type = VARTYPE.EMPTY;
				}
				destLength = DbLength.Variant;
				return DBSTATUS.S_OK;
			}
			DBTIMESTAMP timeStamp = OleDbConvert.GetTimeStamp(value);
			DBLENGTH timeStamp2 = DbLength.TimeStamp;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.DBTIMESTAMP, binding.DestType, timeStamp2, out destLength, (void*)(&timeStamp), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000040E2 File Offset: 0x000022E2
		protected override void Serialize(PageWriter writer, DateTime[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x06000110 RID: 272 RVA: 0x000040EE File Offset: 0x000022EE
		protected override void Deserialize(PageReader reader, DateTime[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}

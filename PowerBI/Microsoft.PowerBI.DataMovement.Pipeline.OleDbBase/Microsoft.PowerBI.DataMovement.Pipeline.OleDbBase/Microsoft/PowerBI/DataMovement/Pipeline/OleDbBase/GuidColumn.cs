using System;
using System.Runtime.CompilerServices;
using Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase.Serialization;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x02000028 RID: 40
	[global::System.Runtime.CompilerServices.NullableContext(1)]
	[global::System.Runtime.CompilerServices.Nullable(0)]
	internal class GuidColumn : Column<Guid>
	{
		// Token: 0x06000129 RID: 297 RVA: 0x0000436D File Offset: 0x0000256D
		internal GuidColumn(int maxRowCount)
			: base(maxRowCount)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600012A RID: 298 RVA: 0x00004376 File Offset: 0x00002576
		public override ColumnType Type
		{
			get
			{
				return ColumnType.Guid;
			}
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000437A File Offset: 0x0000257A
		[global::System.Runtime.CompilerServices.NullableContext(0)]
		public unsafe override void AddValue(DBTYPE type, void* value, int length)
		{
			base.AddValue(*(Guid*)value);
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00004388 File Offset: 0x00002588
		public override Guid GetGuid(int row)
		{
			return base.GetValue(row);
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00004394 File Offset: 0x00002594
		public unsafe override DBSTATUS GetValue(int row, IDataConvert dataConvert, Binding binding, [global::System.Runtime.CompilerServices.Nullable(0)] byte* destValue, out DBLENGTH destLength)
		{
			Guid value = base.GetValue(row);
			DBLENGTH guid = DbLength.Guid;
			DBSTATUS dbstatus;
			dataConvert.DataConvert(DBTYPE.GUID, binding.DestType, guid, out destLength, (void*)(&value), (void*)destValue, binding.DestMaxLength, DBSTATUS.S_OK, out dbstatus, binding.Precision, binding.Scale, DBDATACONVERT.DEFAULT);
			return dbstatus;
		}

		// Token: 0x0600012E RID: 302 RVA: 0x000043DC File Offset: 0x000025DC
		protected override void Serialize(PageWriter writer, Guid[] values, int offset, int count)
		{
			writer.WriteArray(values, offset, count);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x000043E8 File Offset: 0x000025E8
		protected override void Deserialize(PageReader reader, Guid[] values, int offset, int count)
		{
			reader.ReadArray(values, offset, count);
		}
	}
}

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009BD RID: 2493
	internal class DrdaColumnBinding : DrdaBinding
	{
		// Token: 0x1700129A RID: 4762
		// (get) Token: 0x06004D0D RID: 19725 RVA: 0x00134CFB File Offset: 0x00132EFB
		// (set) Token: 0x06004D0E RID: 19726 RVA: 0x00134D03 File Offset: 0x00132F03
		public string BaseTable { get; internal set; }

		// Token: 0x1700129B RID: 4763
		// (get) Token: 0x06004D0F RID: 19727 RVA: 0x00134D0C File Offset: 0x00132F0C
		// (set) Token: 0x06004D10 RID: 19728 RVA: 0x00134D14 File Offset: 0x00132F14
		public string Schema { get; internal set; }

		// Token: 0x1700129C RID: 4764
		// (get) Token: 0x06004D11 RID: 19729 RVA: 0x00134D1D File Offset: 0x00132F1D
		// (set) Token: 0x06004D12 RID: 19730 RVA: 0x00134D25 File Offset: 0x00132F25
		public string Catalog { get; internal set; }

		// Token: 0x1700129D RID: 4765
		// (get) Token: 0x06004D13 RID: 19731 RVA: 0x00134D2E File Offset: 0x00132F2E
		// (set) Token: 0x06004D14 RID: 19732 RVA: 0x00134D36 File Offset: 0x00132F36
		public bool IsKey { get; internal set; }

		// Token: 0x1700129E RID: 4766
		// (get) Token: 0x06004D15 RID: 19733 RVA: 0x00134D3F File Offset: 0x00132F3F
		// (set) Token: 0x06004D16 RID: 19734 RVA: 0x00134D47 File Offset: 0x00132F47
		public short GeneratedIdType { get; internal set; }

		// Token: 0x06004D17 RID: 19735 RVA: 0x00134D50 File Offset: 0x00132F50
		public bool Initialize(IColumnInfo columnInfo)
		{
			this._nullable = columnInfo.IsNullable;
			this._isLob = columnInfo.IsLob;
			this._name = columnInfo.ColumnName;
			this._size = columnInfo.Length;
			this._precision = columnInfo.Precision;
			this._scale = columnInfo.Scale;
			this._isValueSet = false;
			this._type = DrdaMetaType.GetMetaTypeForType(columnInfo.DrdaType);
			this._isTypeSet = true;
			this._isSizeSet = true;
			this._isPrecisionSet = true;
			this._isScaleSet = true;
			if (this._type.DrdaType != DrdaClientType.Numeric && this._type.DrdaType != DrdaClientType.Decimal && this._type.DrdaType != DrdaClientType.DecFloat)
			{
				base.Precision = (short)this._type.Precision;
				base.Scale = (short)this._type.Scale;
				if (this._type.IsFixed && this._type.FixedLength > 0)
				{
					base.Size = this._type.FixedLength;
				}
			}
			this.BaseTable = columnInfo.BaseTable;
			this.Schema = columnInfo.Schema;
			this.Catalog = columnInfo.Catalog;
			this.IsKey = columnInfo.IsKey;
			this.GeneratedIdType = columnInfo.GeneratedIdType;
			Trace.MessageVerboseTrace("DrdaColumn.Initialize(): described column as size = {0}, prec = {1}", this._size, this._precision);
			base.InferProperties(false);
			return true;
		}

		// Token: 0x06004D18 RID: 19736 RVA: 0x00134EB8 File Offset: 0x001330B8
		public async Task GetData(IResultSet resultSet, ushort fieldOrdinal, bool isAsync, CancellationToken cancellationToken)
		{
			object obj = await resultSet.GetColumnData((int)fieldOrdinal, isAsync, cancellationToken);
			this._value = obj;
		}
	}
}

using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x02000222 RID: 546
	internal struct DMOperationInfo
	{
		// Token: 0x06001210 RID: 4624 RVA: 0x000399B1 File Offset: 0x00037BB1
		internal DMOperationInfo(DMOperationType operationType, object searchKey, object data, IOperation operation, IObjectCreator objectCreator)
		{
			this._operationType = operationType;
			this._searchKey = searchKey;
			this._operation = operation;
			this._newdata = data;
			this._hashCode = this._searchKey.GetHashCode();
			this._oldData = null;
			this._objectCreator = objectCreator;
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000399F0 File Offset: 0x00037BF0
		public override string ToString()
		{
			return "(Key= " + this.SearchKey + ")";
		}

		// Token: 0x170003D9 RID: 985
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x00039A07 File Offset: 0x00037C07
		internal IObjectCreator ObjectCreator
		{
			get
			{
				return this._objectCreator;
			}
		}

		// Token: 0x170003DA RID: 986
		// (get) Token: 0x06001213 RID: 4627 RVA: 0x00039A0F File Offset: 0x00037C0F
		internal DMOperationType OperationType
		{
			get
			{
				return this._operationType;
			}
		}

		// Token: 0x170003DB RID: 987
		// (get) Token: 0x06001214 RID: 4628 RVA: 0x00039A17 File Offset: 0x00037C17
		internal object SearchKey
		{
			get
			{
				return this._searchKey;
			}
		}

		// Token: 0x170003DC RID: 988
		// (get) Token: 0x06001215 RID: 4629 RVA: 0x00039A1F File Offset: 0x00037C1F
		internal IOperation Operation
		{
			get
			{
				return this._operation;
			}
		}

		// Token: 0x170003DD RID: 989
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x00039A27 File Offset: 0x00037C27
		internal int HashCode
		{
			get
			{
				return this._hashCode;
			}
		}

		// Token: 0x170003DE RID: 990
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x00039A2F File Offset: 0x00037C2F
		internal object NewData
		{
			get
			{
				return this._newdata;
			}
		}

		// Token: 0x170003DF RID: 991
		// (get) Token: 0x06001218 RID: 4632 RVA: 0x00039A37 File Offset: 0x00037C37
		// (set) Token: 0x06001219 RID: 4633 RVA: 0x00039A3F File Offset: 0x00037C3F
		internal object OldData
		{
			get
			{
				return this._oldData;
			}
			set
			{
				this._oldData = value;
			}
		}

		// Token: 0x04000B19 RID: 2841
		private readonly DMOperationType _operationType;

		// Token: 0x04000B1A RID: 2842
		private readonly object _searchKey;

		// Token: 0x04000B1B RID: 2843
		private readonly IOperation _operation;

		// Token: 0x04000B1C RID: 2844
		private readonly object _newdata;

		// Token: 0x04000B1D RID: 2845
		private readonly int _hashCode;

		// Token: 0x04000B1E RID: 2846
		private object _oldData;

		// Token: 0x04000B1F RID: 2847
		private readonly IObjectCreator _objectCreator;
	}
}

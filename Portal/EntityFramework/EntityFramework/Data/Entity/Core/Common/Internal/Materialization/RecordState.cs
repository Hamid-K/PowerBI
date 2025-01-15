using System;
using System.Data.Entity.Core.Metadata.Edm;

namespace System.Data.Entity.Core.Common.Internal.Materialization
{
	// Token: 0x0200063B RID: 1595
	internal class RecordState
	{
		// Token: 0x06004CB4 RID: 19636 RVA: 0x0010EF24 File Offset: 0x0010D124
		internal RecordState(RecordStateFactory recordStateFactory, CoordinatorFactory coordinatorFactory)
		{
			this.RecordStateFactory = recordStateFactory;
			this.CoordinatorFactory = coordinatorFactory;
			this.CurrentColumnValues = new object[this.RecordStateFactory.ColumnCount];
			this.PendingColumnValues = new object[this.RecordStateFactory.ColumnCount];
		}

		// Token: 0x06004CB5 RID: 19637 RVA: 0x0010EF74 File Offset: 0x0010D174
		internal void AcceptPendingValues()
		{
			object[] currentColumnValues = this.CurrentColumnValues;
			this.CurrentColumnValues = this.PendingColumnValues;
			this.PendingColumnValues = currentColumnValues;
			this._currentEntityRecordInfo = this._pendingEntityRecordInfo;
			this._pendingEntityRecordInfo = null;
			this._currentIsNull = this._pendingIsNull;
			if (this.RecordStateFactory.HasNestedColumns)
			{
				for (int i = 0; i < this.CurrentColumnValues.Length; i++)
				{
					if (this.RecordStateFactory.IsColumnNested[i])
					{
						RecordState recordState = this.CurrentColumnValues[i] as RecordState;
						if (recordState != null)
						{
							recordState.AcceptPendingValues();
						}
					}
				}
			}
		}

		// Token: 0x17000ED9 RID: 3801
		// (get) Token: 0x06004CB6 RID: 19638 RVA: 0x0010F004 File Offset: 0x0010D204
		internal int ColumnCount
		{
			get
			{
				return this.RecordStateFactory.ColumnCount;
			}
		}

		// Token: 0x17000EDA RID: 3802
		// (get) Token: 0x06004CB7 RID: 19639 RVA: 0x0010F014 File Offset: 0x0010D214
		internal DataRecordInfo DataRecordInfo
		{
			get
			{
				DataRecordInfo dataRecordInfo = this._currentEntityRecordInfo;
				if (dataRecordInfo == null)
				{
					dataRecordInfo = this.RecordStateFactory.DataRecordInfo;
				}
				return dataRecordInfo;
			}
		}

		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06004CB8 RID: 19640 RVA: 0x0010F038 File Offset: 0x0010D238
		internal bool IsNull
		{
			get
			{
				return this._currentIsNull;
			}
		}

		// Token: 0x06004CB9 RID: 19641 RVA: 0x0010F040 File Offset: 0x0010D240
		internal long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
		{
			byte[] array = (byte[])this.CurrentColumnValues[ordinal];
			int num = array.Length;
			int num2 = (int)dataOffset;
			int num3 = num - num2;
			if (buffer != null)
			{
				num3 = Math.Min(num3, length);
				if (0 < num3)
				{
					Buffer.BlockCopy(array, num2, buffer, bufferOffset, num3);
				}
			}
			return (long)Math.Max(0, num3);
		}

		// Token: 0x06004CBA RID: 19642 RVA: 0x0010F088 File Offset: 0x0010D288
		internal long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
		{
			string text = this.CurrentColumnValues[ordinal] as string;
			char[] array;
			if (text != null)
			{
				array = text.ToCharArray();
			}
			else
			{
				array = (char[])this.CurrentColumnValues[ordinal];
			}
			int num = array.Length;
			int num2 = (int)dataOffset;
			int num3 = num - num2;
			if (buffer != null)
			{
				num3 = Math.Min(num3, length);
				if (0 < num3)
				{
					Buffer.BlockCopy(array, num2 * 2, buffer, bufferOffset * 2, num3 * 2);
				}
			}
			return (long)Math.Max(0, num3);
		}

		// Token: 0x06004CBB RID: 19643 RVA: 0x0010F0EF File Offset: 0x0010D2EF
		internal string GetName(int ordinal)
		{
			if (ordinal < 0 || ordinal >= this.RecordStateFactory.ColumnCount)
			{
				throw new ArgumentOutOfRangeException("ordinal");
			}
			return this.RecordStateFactory.ColumnNames[ordinal];
		}

		// Token: 0x06004CBC RID: 19644 RVA: 0x0010F11F File Offset: 0x0010D31F
		internal int GetOrdinal(string name)
		{
			return this.RecordStateFactory.FieldNameLookup.GetOrdinal(name);
		}

		// Token: 0x06004CBD RID: 19645 RVA: 0x0010F132 File Offset: 0x0010D332
		internal TypeUsage GetTypeUsage(int ordinal)
		{
			return this.RecordStateFactory.TypeUsages[ordinal];
		}

		// Token: 0x06004CBE RID: 19646 RVA: 0x0010F145 File Offset: 0x0010D345
		internal bool IsNestedObject(int ordinal)
		{
			return this.RecordStateFactory.IsColumnNested[ordinal];
		}

		// Token: 0x06004CBF RID: 19647 RVA: 0x0010F158 File Offset: 0x0010D358
		internal void ResetToDefaultState()
		{
			this._currentEntityRecordInfo = null;
		}

		// Token: 0x06004CC0 RID: 19648 RVA: 0x0010F161 File Offset: 0x0010D361
		internal RecordState GatherData(Shaper shaper)
		{
			this.RecordStateFactory.GatherData(shaper);
			this._pendingIsNull = false;
			return this;
		}

		// Token: 0x06004CC1 RID: 19649 RVA: 0x0010F17D File Offset: 0x0010D37D
		internal bool SetColumnValue(int ordinal, object value)
		{
			this.PendingColumnValues[ordinal] = value;
			return true;
		}

		// Token: 0x06004CC2 RID: 19650 RVA: 0x0010F189 File Offset: 0x0010D389
		internal bool SetEntityRecordInfo(EntityKey entityKey, EntitySet entitySet)
		{
			this._pendingEntityRecordInfo = new EntityRecordInfo(this.RecordStateFactory.DataRecordInfo, entityKey, entitySet);
			return true;
		}

		// Token: 0x06004CC3 RID: 19651 RVA: 0x0010F1A4 File Offset: 0x0010D3A4
		internal RecordState SetNullRecord()
		{
			for (int i = 0; i < this.PendingColumnValues.Length; i++)
			{
				this.PendingColumnValues[i] = DBNull.Value;
			}
			this._pendingEntityRecordInfo = null;
			this._pendingIsNull = true;
			return this;
		}

		// Token: 0x04001B37 RID: 6967
		private readonly RecordStateFactory RecordStateFactory;

		// Token: 0x04001B38 RID: 6968
		internal readonly CoordinatorFactory CoordinatorFactory;

		// Token: 0x04001B39 RID: 6969
		private bool _pendingIsNull;

		// Token: 0x04001B3A RID: 6970
		private bool _currentIsNull;

		// Token: 0x04001B3B RID: 6971
		private EntityRecordInfo _currentEntityRecordInfo;

		// Token: 0x04001B3C RID: 6972
		private EntityRecordInfo _pendingEntityRecordInfo;

		// Token: 0x04001B3D RID: 6973
		internal object[] CurrentColumnValues;

		// Token: 0x04001B3E RID: 6974
		internal object[] PendingColumnValues;
	}
}

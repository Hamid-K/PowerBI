using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000457 RID: 1111
	internal class ShapelessBufferedDataRecord : BufferedDataRecord
	{
		// Token: 0x0600364C RID: 13900 RVA: 0x000B021C File Offset: 0x000AE41C
		protected ShapelessBufferedDataRecord()
		{
		}

		// Token: 0x0600364D RID: 13901 RVA: 0x000B0224 File Offset: 0x000AE424
		internal static ShapelessBufferedDataRecord Initialize(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader)
		{
			ShapelessBufferedDataRecord shapelessBufferedDataRecord = new ShapelessBufferedDataRecord();
			shapelessBufferedDataRecord.ReadMetadata(providerManifestToken, providerServices, reader);
			int fieldCount = shapelessBufferedDataRecord.FieldCount;
			List<object[]> list = new List<object[]>();
			if (shapelessBufferedDataRecord._spatialDataReader != null)
			{
				while (reader.Read())
				{
					object[] array = new object[fieldCount];
					for (int i = 0; i < fieldCount; i++)
					{
						if (reader.IsDBNull(i))
						{
							array[i] = DBNull.Value;
						}
						else if (shapelessBufferedDataRecord._geographyColumns[i])
						{
							array[i] = shapelessBufferedDataRecord._spatialDataReader.GetGeography(i);
						}
						else if (shapelessBufferedDataRecord._geometryColumns[i])
						{
							array[i] = shapelessBufferedDataRecord._spatialDataReader.GetGeometry(i);
						}
						else
						{
							array[i] = reader.GetValue(i);
						}
					}
					list.Add(array);
				}
			}
			else
			{
				while (reader.Read())
				{
					object[] array2 = new object[fieldCount];
					reader.GetValues(array2);
					list.Add(array2);
				}
			}
			shapelessBufferedDataRecord._rowCount = list.Count;
			shapelessBufferedDataRecord._resultSet = list;
			return shapelessBufferedDataRecord;
		}

		// Token: 0x0600364E RID: 13902 RVA: 0x000B0318 File Offset: 0x000AE518
		internal static async Task<ShapelessBufferedDataRecord> InitializeAsync(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader, CancellationToken cancellationToken)
		{
			ShapelessBufferedDataRecord record = new ShapelessBufferedDataRecord();
			record.ReadMetadata(providerManifestToken, providerServices, reader);
			int fieldCount = record.FieldCount;
			List<object[]> resultSet = new List<object[]>();
			for (;;)
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					break;
				}
				object[] row = new object[fieldCount];
				for (int i = 0; i < fieldCount; i++)
				{
					cultureAwaiter = reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
					if (!cultureAwaiter.IsCompleted)
					{
						await cultureAwaiter;
						cultureAwaiter = cultureAwaiter2;
						cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
					}
					if (cultureAwaiter.GetResult())
					{
						row[i] = DBNull.Value;
					}
					else if (record._spatialDataReader != null && record._geographyColumns[i])
					{
						row[i] = await record._spatialDataReader.GetGeographyAsync(i, cancellationToken).WithCurrentCulture<DbGeography>();
					}
					else if (record._spatialDataReader != null && record._geometryColumns[i])
					{
						row[i] = await record._spatialDataReader.GetGeometryAsync(i, cancellationToken).WithCurrentCulture<DbGeometry>();
					}
					else
					{
						row[i] = await reader.GetFieldValueAsync<object>(i, cancellationToken).WithCurrentCulture<object>();
					}
				}
				resultSet.Add(row);
				row = null;
			}
			record._rowCount = resultSet.Count;
			record._resultSet = resultSet;
			return record;
		}

		// Token: 0x0600364F RID: 13903 RVA: 0x000B0378 File Offset: 0x000AE578
		protected override void ReadMetadata(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader)
		{
			base.ReadMetadata(providerManifestToken, providerServices, reader);
			int fieldCount = base.FieldCount;
			bool flag = false;
			DbSpatialDataReader dbSpatialDataReader = null;
			if (fieldCount > 0)
			{
				dbSpatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			if (dbSpatialDataReader != null)
			{
				this._geographyColumns = new bool[fieldCount];
				this._geometryColumns = new bool[fieldCount];
				for (int i = 0; i < fieldCount; i++)
				{
					this._geographyColumns[i] = dbSpatialDataReader.IsGeographyColumn(i);
					this._geometryColumns[i] = dbSpatialDataReader.IsGeometryColumn(i);
					flag = flag || this._geographyColumns[i] || this._geometryColumns[i];
				}
			}
			this._spatialDataReader = (flag ? dbSpatialDataReader : null);
		}

		// Token: 0x06003650 RID: 13904 RVA: 0x000B0411 File Offset: 0x000AE611
		public override bool GetBoolean(int ordinal)
		{
			return this.GetFieldValue<bool>(ordinal);
		}

		// Token: 0x06003651 RID: 13905 RVA: 0x000B041A File Offset: 0x000AE61A
		public override byte GetByte(int ordinal)
		{
			return this.GetFieldValue<byte>(ordinal);
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000B0423 File Offset: 0x000AE623
		public override char GetChar(int ordinal)
		{
			return this.GetFieldValue<char>(ordinal);
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000B042C File Offset: 0x000AE62C
		public override DateTime GetDateTime(int ordinal)
		{
			return this.GetFieldValue<DateTime>(ordinal);
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000B0435 File Offset: 0x000AE635
		public override decimal GetDecimal(int ordinal)
		{
			return this.GetFieldValue<decimal>(ordinal);
		}

		// Token: 0x06003655 RID: 13909 RVA: 0x000B043E File Offset: 0x000AE63E
		public override double GetDouble(int ordinal)
		{
			return this.GetFieldValue<double>(ordinal);
		}

		// Token: 0x06003656 RID: 13910 RVA: 0x000B0447 File Offset: 0x000AE647
		public override float GetFloat(int ordinal)
		{
			return this.GetFieldValue<float>(ordinal);
		}

		// Token: 0x06003657 RID: 13911 RVA: 0x000B0450 File Offset: 0x000AE650
		public override Guid GetGuid(int ordinal)
		{
			return this.GetFieldValue<Guid>(ordinal);
		}

		// Token: 0x06003658 RID: 13912 RVA: 0x000B0459 File Offset: 0x000AE659
		public override short GetInt16(int ordinal)
		{
			return this.GetFieldValue<short>(ordinal);
		}

		// Token: 0x06003659 RID: 13913 RVA: 0x000B0462 File Offset: 0x000AE662
		public override int GetInt32(int ordinal)
		{
			return this.GetFieldValue<int>(ordinal);
		}

		// Token: 0x0600365A RID: 13914 RVA: 0x000B046B File Offset: 0x000AE66B
		public override long GetInt64(int ordinal)
		{
			return this.GetFieldValue<long>(ordinal);
		}

		// Token: 0x0600365B RID: 13915 RVA: 0x000B0474 File Offset: 0x000AE674
		public override string GetString(int ordinal)
		{
			return this.GetFieldValue<string>(ordinal);
		}

		// Token: 0x0600365C RID: 13916 RVA: 0x000B047D File Offset: 0x000AE67D
		public override T GetFieldValue<T>(int ordinal)
		{
			return (T)((object)this._currentRow[ordinal]);
		}

		// Token: 0x0600365D RID: 13917 RVA: 0x000B048C File Offset: 0x000AE68C
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<T>((T)((object)this._currentRow[ordinal]));
		}

		// Token: 0x0600365E RID: 13918 RVA: 0x000B04A0 File Offset: 0x000AE6A0
		public override object GetValue(int ordinal)
		{
			return this.GetFieldValue<object>(ordinal);
		}

		// Token: 0x0600365F RID: 13919 RVA: 0x000B04AC File Offset: 0x000AE6AC
		public override int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, base.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this.GetValue(i);
			}
			return num;
		}

		// Token: 0x06003660 RID: 13920 RVA: 0x000B04DF File Offset: 0x000AE6DF
		public override bool IsDBNull(int ordinal)
		{
			return this._currentRow.Length == 0 || DBNull.Value == this._currentRow[ordinal];
		}

		// Token: 0x06003661 RID: 13921 RVA: 0x000B04FB File Offset: 0x000AE6FB
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.IsDBNull(ordinal));
		}

		// Token: 0x06003662 RID: 13922 RVA: 0x000B050C File Offset: 0x000AE70C
		public override bool Read()
		{
			int num = this._currentRowNumber + 1;
			this._currentRowNumber = num;
			if (num < this._rowCount)
			{
				this._currentRow = this._resultSet[this._currentRowNumber];
				base.IsDataReady = true;
			}
			else
			{
				this._currentRow = null;
				base.IsDataReady = false;
			}
			return base.IsDataReady;
		}

		// Token: 0x06003663 RID: 13923 RVA: 0x000B0566 File Offset: 0x000AE766
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.Read());
		}

		// Token: 0x0400119F RID: 4511
		private object[] _currentRow;

		// Token: 0x040011A0 RID: 4512
		private List<object[]> _resultSet;

		// Token: 0x040011A1 RID: 4513
		private DbSpatialDataReader _spatialDataReader;

		// Token: 0x040011A2 RID: 4514
		private bool[] _geographyColumns;

		// Token: 0x040011A3 RID: 4515
		private bool[] _geometryColumns;
	}
}

using System;
using System.Collections;
using System.Data.Common;
using System.Data.Entity.Core.Common;
using System.Data.Entity.Spatial;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Core.Objects.Internal
{
	// Token: 0x02000456 RID: 1110
	internal class ShapedBufferedDataRecord : BufferedDataRecord
	{
		// Token: 0x06003615 RID: 13845 RVA: 0x000AE890 File Offset: 0x000ACA90
		protected ShapedBufferedDataRecord()
		{
		}

		// Token: 0x06003616 RID: 13846 RVA: 0x000AE8A0 File Offset: 0x000ACAA0
		internal static BufferedDataRecord Initialize(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader, Type[] columnTypes, bool[] nullableColumns)
		{
			ShapedBufferedDataRecord shapedBufferedDataRecord = new ShapedBufferedDataRecord();
			shapedBufferedDataRecord.ReadMetadata(providerManifestToken, providerServices, reader);
			DbSpatialDataReader dbSpatialDataReader = null;
			if (columnTypes.Any((Type t) => t == typeof(DbGeography) || t == typeof(DbGeometry)))
			{
				dbSpatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			return shapedBufferedDataRecord.Initialize(reader, dbSpatialDataReader, columnTypes, nullableColumns);
		}

		// Token: 0x06003617 RID: 13847 RVA: 0x000AE8F8 File Offset: 0x000ACAF8
		internal static Task<BufferedDataRecord> InitializeAsync(string providerManifestToken, DbProviderServices providerServices, DbDataReader reader, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			cancellationToken.ThrowIfCancellationRequested();
			ShapedBufferedDataRecord shapedBufferedDataRecord = new ShapedBufferedDataRecord();
			shapedBufferedDataRecord.ReadMetadata(providerManifestToken, providerServices, reader);
			DbSpatialDataReader dbSpatialDataReader = null;
			if (columnTypes.Any((Type t) => t == typeof(DbGeography) || t == typeof(DbGeometry)))
			{
				dbSpatialDataReader = providerServices.GetSpatialDataReader(reader, providerManifestToken);
			}
			return shapedBufferedDataRecord.InitializeAsync(reader, dbSpatialDataReader, columnTypes, nullableColumns, cancellationToken);
		}

		// Token: 0x06003618 RID: 13848 RVA: 0x000AE958 File Offset: 0x000ACB58
		private BufferedDataRecord Initialize(DbDataReader reader, DbSpatialDataReader spatialDataReader, Type[] columnTypes, bool[] nullableColumns)
		{
			this.InitializeFields(columnTypes, nullableColumns);
			while (reader.Read())
			{
				this._currentRowNumber++;
				if (this._rowCapacity == this._currentRowNumber)
				{
					this.DoubleBufferCapacity();
				}
				int num = Math.Max(columnTypes.Length, nullableColumns.Length);
				for (int i = 0; i < num; i++)
				{
					if (i < this._columnTypeCases.Length)
					{
						switch (this._columnTypeCases[i])
						{
						case ShapedBufferedDataRecord.TypeCase.Empty:
							if (nullableColumns[i])
							{
								this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Bool:
							if (!nullableColumns[i])
							{
								this.ReadBool(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadBool(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Byte:
							if (!nullableColumns[i])
							{
								this.ReadByte(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadByte(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Char:
							if (!nullableColumns[i])
							{
								this.ReadChar(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadChar(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.DateTime:
							if (!nullableColumns[i])
							{
								this.ReadDateTime(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDateTime(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Decimal:
							if (!nullableColumns[i])
							{
								this.ReadDecimal(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDecimal(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Double:
							if (!nullableColumns[i])
							{
								this.ReadDouble(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadDouble(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Float:
							if (!nullableColumns[i])
							{
								this.ReadFloat(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadFloat(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Guid:
							if (!nullableColumns[i])
							{
								this.ReadGuid(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGuid(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Short:
							if (!nullableColumns[i])
							{
								this.ReadShort(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadShort(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Int:
							if (!nullableColumns[i])
							{
								this.ReadInt(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadInt(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.Long:
							if (!nullableColumns[i])
							{
								this.ReadLong(reader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadLong(reader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.DbGeography:
							if (!nullableColumns[i])
							{
								this.ReadGeography(spatialDataReader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGeography(spatialDataReader, i);
								goto IL_051A;
							}
							goto IL_051A;
						case ShapedBufferedDataRecord.TypeCase.DbGeometry:
							if (!nullableColumns[i])
							{
								this.ReadGeometry(spatialDataReader, i);
								goto IL_051A;
							}
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadGeometry(spatialDataReader, i);
								goto IL_051A;
							}
							goto IL_051A;
						}
						if (nullableColumns[i])
						{
							if (!(this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i)))
							{
								this.ReadObject(reader, i);
							}
						}
						else
						{
							this.ReadObject(reader, i);
						}
					}
					else if (nullableColumns[i])
					{
						this._tempNulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i]] = reader.IsDBNull(i);
					}
					IL_051A:;
				}
			}
			this._bools = new BitArray(this._tempBools);
			this._tempBools = null;
			this._nulls = new BitArray(this._tempNulls);
			this._tempNulls = null;
			this._rowCount = this._currentRowNumber + 1;
			this._currentRowNumber = -1;
			return this;
		}

		// Token: 0x06003619 RID: 13849 RVA: 0x000AEEDC File Offset: 0x000AD0DC
		private async Task<BufferedDataRecord> InitializeAsync(DbDataReader reader, DbSpatialDataReader spatialDataReader, Type[] columnTypes, bool[] nullableColumns, CancellationToken cancellationToken)
		{
			this.InitializeFields(columnTypes, nullableColumns);
			for (;;)
			{
				global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter = reader.ReadAsync(cancellationToken).WithCurrentCulture<bool>().GetAwaiter();
				if (!cultureAwaiter.IsCompleted)
				{
					await cultureAwaiter;
					global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool> cultureAwaiter2;
					cultureAwaiter = cultureAwaiter2;
					cultureAwaiter2 = default(global::System.Data.Entity.Utilities.TaskExtensions.CultureAwaiter<bool>);
				}
				if (!cultureAwaiter.GetResult())
				{
					break;
				}
				cancellationToken.ThrowIfCancellationRequested();
				this._currentRowNumber++;
				if (this._rowCapacity == this._currentRowNumber)
				{
					this.DoubleBufferCapacity();
				}
				int columnCount = ((columnTypes.Length > nullableColumns.Length) ? columnTypes.Length : nullableColumns.Length);
				int num;
				for (int i = 0; i < columnCount; i = num + 1)
				{
					if (i < this._columnTypeCases.Length)
					{
						switch (this._columnTypeCases[i])
						{
						case ShapedBufferedDataRecord.TypeCase.Empty:
							if (nullableColumns[i])
							{
								bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
								bool[] tempNulls = this._tempNulls;
								num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
								tempNulls[num] = flag;
								goto IL_1B66;
							}
							goto IL_1B66;
						case ShapedBufferedDataRecord.TypeCase.Bool:
						{
							if (!nullableColumns[i])
							{
								await this.ReadBoolAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls2 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num2 = num;
							bool flag2 = flag;
							tempNulls2[num2] = flag2;
							if (!flag2)
							{
								await this.ReadBoolAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Byte:
						{
							if (!nullableColumns[i])
							{
								await this.ReadByteAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls3 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num3 = num;
							bool flag3 = flag;
							tempNulls3[num3] = flag3;
							if (!flag3)
							{
								await this.ReadByteAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Char:
						{
							if (!nullableColumns[i])
							{
								await this.ReadCharAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls4 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num4 = num;
							bool flag4 = flag;
							tempNulls4[num4] = flag4;
							if (!flag4)
							{
								await this.ReadCharAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.DateTime:
						{
							if (!nullableColumns[i])
							{
								await this.ReadDateTimeAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls5 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num5 = num;
							bool flag5 = flag;
							tempNulls5[num5] = flag5;
							if (!flag5)
							{
								await this.ReadDateTimeAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Decimal:
						{
							if (!nullableColumns[i])
							{
								await this.ReadDecimalAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls6 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num6 = num;
							bool flag6 = flag;
							tempNulls6[num6] = flag6;
							if (!flag6)
							{
								await this.ReadDecimalAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Double:
						{
							if (!nullableColumns[i])
							{
								await this.ReadDoubleAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls7 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num7 = num;
							bool flag7 = flag;
							tempNulls7[num7] = flag7;
							if (!flag7)
							{
								await this.ReadDoubleAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Float:
						{
							if (!nullableColumns[i])
							{
								await this.ReadFloatAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls8 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num8 = num;
							bool flag8 = flag;
							tempNulls8[num8] = flag8;
							if (!flag8)
							{
								await this.ReadFloatAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Guid:
						{
							if (!nullableColumns[i])
							{
								await this.ReadGuidAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls9 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num9 = num;
							bool flag9 = flag;
							tempNulls9[num9] = flag9;
							if (!flag9)
							{
								await this.ReadGuidAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Short:
						{
							if (!nullableColumns[i])
							{
								await this.ReadShortAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls10 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num10 = num;
							bool flag10 = flag;
							tempNulls10[num10] = flag10;
							if (!flag10)
							{
								await this.ReadShortAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Int:
						{
							if (!nullableColumns[i])
							{
								await this.ReadIntAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls11 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num11 = num;
							bool flag11 = flag;
							tempNulls11[num11] = flag11;
							if (!flag11)
							{
								await this.ReadIntAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.Long:
						{
							if (!nullableColumns[i])
							{
								await this.ReadLongAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls12 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num12 = num;
							bool flag12 = flag;
							tempNulls12[num12] = flag12;
							if (!flag12)
							{
								await this.ReadLongAsync(reader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.DbGeography:
						{
							if (!nullableColumns[i])
							{
								await this.ReadGeographyAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls13 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num13 = num;
							bool flag13 = flag;
							tempNulls13[num13] = flag13;
							if (!flag13)
							{
								await this.ReadGeographyAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						case ShapedBufferedDataRecord.TypeCase.DbGeometry:
						{
							if (!nullableColumns[i])
							{
								await this.ReadGeometryAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls14 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num14 = num;
							bool flag14 = flag;
							tempNulls14[num14] = flag14;
							if (!flag14)
							{
								await this.ReadGeometryAsync(spatialDataReader, i, cancellationToken).WithCurrentCulture();
								goto IL_1B66;
							}
							goto IL_1B66;
						}
						}
						if (nullableColumns[i])
						{
							bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
							bool[] tempNulls15 = this._tempNulls;
							num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
							int num15 = num;
							bool flag15 = flag;
							tempNulls15[num15] = flag15;
							if (!flag15)
							{
								await this.ReadObjectAsync(reader, i, cancellationToken).WithCurrentCulture();
							}
						}
						else
						{
							await this.ReadObjectAsync(reader, i, cancellationToken).WithCurrentCulture();
						}
					}
					else if (nullableColumns[i])
					{
						bool flag = await reader.IsDBNullAsync(i, cancellationToken).WithCurrentCulture<bool>();
						bool[] tempNulls16 = this._tempNulls;
						num = this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[i];
						tempNulls16[num] = flag;
					}
					IL_1B66:
					num = i;
				}
			}
			this._bools = new BitArray(this._tempBools);
			this._tempBools = null;
			this._nulls = new BitArray(this._tempNulls);
			this._tempNulls = null;
			this._rowCount = this._currentRowNumber + 1;
			this._currentRowNumber = -1;
			return this;
		}

		// Token: 0x0600361A RID: 13850 RVA: 0x000AEF4C File Offset: 0x000AD14C
		private void InitializeFields(Type[] columnTypes, bool[] nullableColumns)
		{
			this._columnTypeCases = Enumerable.Repeat<ShapedBufferedDataRecord.TypeCase>(ShapedBufferedDataRecord.TypeCase.Empty, columnTypes.Length).ToArray<ShapedBufferedDataRecord.TypeCase>();
			int num = Math.Max(base.FieldCount, Math.Max(columnTypes.Length, nullableColumns.Length));
			this._ordinalToIndexMap = Enumerable.Repeat<int>(-1, num).ToArray<int>();
			for (int i = 0; i < columnTypes.Length; i++)
			{
				Type type = columnTypes[i];
				if (!(type == null))
				{
					if (type == typeof(bool))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Bool;
						this._ordinalToIndexMap[i] = this._boolCount;
						this._boolCount++;
					}
					else if (type == typeof(byte))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Byte;
						this._ordinalToIndexMap[i] = this._byteCount;
						this._byteCount++;
					}
					else if (type == typeof(char))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Char;
						this._ordinalToIndexMap[i] = this._charCount;
						this._charCount++;
					}
					else if (type == typeof(DateTime))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DateTime;
						this._ordinalToIndexMap[i] = this._dateTimeCount;
						this._dateTimeCount++;
					}
					else if (type == typeof(decimal))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Decimal;
						this._ordinalToIndexMap[i] = this._decimalCount;
						this._decimalCount++;
					}
					else if (type == typeof(double))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Double;
						this._ordinalToIndexMap[i] = this._doubleCount;
						this._doubleCount++;
					}
					else if (type == typeof(float))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Float;
						this._ordinalToIndexMap[i] = this._floatCount;
						this._floatCount++;
					}
					else if (type == typeof(Guid))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Guid;
						this._ordinalToIndexMap[i] = this._guidCount;
						this._guidCount++;
					}
					else if (type == typeof(short))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Short;
						this._ordinalToIndexMap[i] = this._shortCount;
						this._shortCount++;
					}
					else if (type == typeof(int))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Int;
						this._ordinalToIndexMap[i] = this._intCount;
						this._intCount++;
					}
					else if (type == typeof(long))
					{
						this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Long;
						this._ordinalToIndexMap[i] = this._longCount;
						this._longCount++;
					}
					else
					{
						if (type == typeof(DbGeography))
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DbGeography;
						}
						else if (type == typeof(DbGeometry))
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.DbGeometry;
						}
						else
						{
							this._columnTypeCases[i] = ShapedBufferedDataRecord.TypeCase.Object;
						}
						this._ordinalToIndexMap[i] = this._objectCount;
						this._objectCount++;
					}
				}
			}
			this._tempBools = new bool[this._rowCapacity * this._boolCount];
			this._bytes = new byte[this._rowCapacity * this._byteCount];
			this._chars = new char[this._rowCapacity * this._charCount];
			this._dateTimes = new DateTime[this._rowCapacity * this._dateTimeCount];
			this._decimals = new decimal[this._rowCapacity * this._decimalCount];
			this._doubles = new double[this._rowCapacity * this._doubleCount];
			this._floats = new float[this._rowCapacity * this._floatCount];
			this._guids = new Guid[this._rowCapacity * this._guidCount];
			this._shorts = new short[this._rowCapacity * this._shortCount];
			this._ints = new int[this._rowCapacity * this._intCount];
			this._longs = new long[this._rowCapacity * this._longCount];
			this._objects = new object[this._rowCapacity * this._objectCount];
			this._nullOrdinalToIndexMap = Enumerable.Repeat<int>(-1, num).ToArray<int>();
			for (int j = 0; j < nullableColumns.Length; j++)
			{
				if (nullableColumns[j])
				{
					this._nullOrdinalToIndexMap[j] = this._nullCount;
					this._nullCount++;
				}
			}
			this._tempNulls = new bool[this._rowCapacity * this._nullCount];
		}

		// Token: 0x0600361B RID: 13851 RVA: 0x000AF42C File Offset: 0x000AD62C
		private void DoubleBufferCapacity()
		{
			this._rowCapacity <<= 1;
			bool[] array = new bool[this._tempBools.Length << 1];
			Array.Copy(this._tempBools, array, this._tempBools.Length);
			this._tempBools = array;
			byte[] array2 = new byte[this._bytes.Length << 1];
			Array.Copy(this._bytes, array2, this._bytes.Length);
			this._bytes = array2;
			char[] array3 = new char[this._chars.Length << 1];
			Array.Copy(this._chars, array3, this._chars.Length);
			this._chars = array3;
			DateTime[] array4 = new DateTime[this._dateTimes.Length << 1];
			Array.Copy(this._dateTimes, array4, this._dateTimes.Length);
			this._dateTimes = array4;
			decimal[] array5 = new decimal[this._decimals.Length << 1];
			Array.Copy(this._decimals, array5, this._decimals.Length);
			this._decimals = array5;
			double[] array6 = new double[this._doubles.Length << 1];
			Array.Copy(this._doubles, array6, this._doubles.Length);
			this._doubles = array6;
			float[] array7 = new float[this._floats.Length << 1];
			Array.Copy(this._floats, array7, this._floats.Length);
			this._floats = array7;
			Guid[] array8 = new Guid[this._guids.Length << 1];
			Array.Copy(this._guids, array8, this._guids.Length);
			this._guids = array8;
			short[] array9 = new short[this._shorts.Length << 1];
			Array.Copy(this._shorts, array9, this._shorts.Length);
			this._shorts = array9;
			int[] array10 = new int[this._ints.Length << 1];
			Array.Copy(this._ints, array10, this._ints.Length);
			this._ints = array10;
			long[] array11 = new long[this._longs.Length << 1];
			Array.Copy(this._longs, array11, this._longs.Length);
			this._longs = array11;
			object[] array12 = new object[this._objects.Length << 1];
			Array.Copy(this._objects, array12, this._objects.Length);
			this._objects = array12;
			bool[] array13 = new bool[this._tempNulls.Length << 1];
			Array.Copy(this._tempNulls, array13, this._tempNulls.Length);
			this._tempNulls = array13;
		}

		// Token: 0x0600361C RID: 13852 RVA: 0x000AF691 File Offset: 0x000AD891
		private void ReadBool(DbDataReader reader, int ordinal)
		{
			this._tempBools[this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal]] = reader.GetBoolean(ordinal);
		}

		// Token: 0x0600361D RID: 13853 RVA: 0x000AF6B8 File Offset: 0x000AD8B8
		private async Task ReadBoolAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			bool flag = await reader.GetFieldValueAsync<bool>(ordinal, cancellationToken).WithCurrentCulture<bool>();
			bool[] tempBools = this._tempBools;
			int num = this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal];
			tempBools[num] = flag;
		}

		// Token: 0x0600361E RID: 13854 RVA: 0x000AF715 File Offset: 0x000AD915
		private void ReadByte(DbDataReader reader, int ordinal)
		{
			this._bytes[this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal]] = reader.GetByte(ordinal);
		}

		// Token: 0x0600361F RID: 13855 RVA: 0x000AF73C File Offset: 0x000AD93C
		private async Task ReadByteAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			byte b = await reader.GetFieldValueAsync<byte>(ordinal, cancellationToken).WithCurrentCulture<byte>();
			byte[] bytes = this._bytes;
			int num = this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal];
			bytes[num] = b;
		}

		// Token: 0x06003620 RID: 13856 RVA: 0x000AF799 File Offset: 0x000AD999
		private void ReadChar(DbDataReader reader, int ordinal)
		{
			this._chars[this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal]] = reader.GetChar(ordinal);
		}

		// Token: 0x06003621 RID: 13857 RVA: 0x000AF7C0 File Offset: 0x000AD9C0
		private async Task ReadCharAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			char c = await reader.GetFieldValueAsync<char>(ordinal, cancellationToken).WithCurrentCulture<char>();
			char[] chars = this._chars;
			int num = this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal];
			chars[num] = c;
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000AF81D File Offset: 0x000ADA1D
		private void ReadDateTime(DbDataReader reader, int ordinal)
		{
			this._dateTimes[this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal]] = reader.GetDateTime(ordinal);
		}

		// Token: 0x06003623 RID: 13859 RVA: 0x000AF848 File Offset: 0x000ADA48
		private async Task ReadDateTimeAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			DateTime dateTime = await reader.GetFieldValueAsync<DateTime>(ordinal, cancellationToken).WithCurrentCulture<DateTime>();
			DateTime[] dateTimes = this._dateTimes;
			int num = this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal];
			dateTimes[num] = dateTime;
		}

		// Token: 0x06003624 RID: 13860 RVA: 0x000AF8A5 File Offset: 0x000ADAA5
		private void ReadDecimal(DbDataReader reader, int ordinal)
		{
			this._decimals[this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal]] = reader.GetDecimal(ordinal);
		}

		// Token: 0x06003625 RID: 13861 RVA: 0x000AF8D0 File Offset: 0x000ADAD0
		private async Task ReadDecimalAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			decimal num = await reader.GetFieldValueAsync<decimal>(ordinal, cancellationToken).WithCurrentCulture<decimal>();
			decimal[] decimals = this._decimals;
			int num2 = this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal];
			decimals[num2] = num;
		}

		// Token: 0x06003626 RID: 13862 RVA: 0x000AF92D File Offset: 0x000ADB2D
		private void ReadDouble(DbDataReader reader, int ordinal)
		{
			this._doubles[this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal]] = reader.GetDouble(ordinal);
		}

		// Token: 0x06003627 RID: 13863 RVA: 0x000AF954 File Offset: 0x000ADB54
		private async Task ReadDoubleAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			double num = await reader.GetFieldValueAsync<double>(ordinal, cancellationToken).WithCurrentCulture<double>();
			double[] doubles = this._doubles;
			int num2 = this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal];
			doubles[num2] = num;
		}

		// Token: 0x06003628 RID: 13864 RVA: 0x000AF9B1 File Offset: 0x000ADBB1
		private void ReadFloat(DbDataReader reader, int ordinal)
		{
			this._floats[this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal]] = reader.GetFloat(ordinal);
		}

		// Token: 0x06003629 RID: 13865 RVA: 0x000AF9D8 File Offset: 0x000ADBD8
		private async Task ReadFloatAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			float num = await reader.GetFieldValueAsync<float>(ordinal, cancellationToken).WithCurrentCulture<float>();
			float[] floats = this._floats;
			int num2 = this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal];
			floats[num2] = num;
		}

		// Token: 0x0600362A RID: 13866 RVA: 0x000AFA35 File Offset: 0x000ADC35
		private void ReadGuid(DbDataReader reader, int ordinal)
		{
			this._guids[this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal]] = reader.GetGuid(ordinal);
		}

		// Token: 0x0600362B RID: 13867 RVA: 0x000AFA60 File Offset: 0x000ADC60
		private async Task ReadGuidAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			Guid guid = await reader.GetFieldValueAsync<Guid>(ordinal, cancellationToken).WithCurrentCulture<Guid>();
			Guid[] guids = this._guids;
			int num = this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal];
			guids[num] = guid;
		}

		// Token: 0x0600362C RID: 13868 RVA: 0x000AFABD File Offset: 0x000ADCBD
		private void ReadShort(DbDataReader reader, int ordinal)
		{
			this._shorts[this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt16(ordinal);
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000AFAE4 File Offset: 0x000ADCE4
		private async Task ReadShortAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			short num = await reader.GetFieldValueAsync<short>(ordinal, cancellationToken).WithCurrentCulture<short>();
			short[] shorts = this._shorts;
			int num2 = this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal];
			shorts[num2] = num;
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000AFB41 File Offset: 0x000ADD41
		private void ReadInt(DbDataReader reader, int ordinal)
		{
			this._ints[this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt32(ordinal);
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000AFB68 File Offset: 0x000ADD68
		private async Task ReadIntAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			int num = await reader.GetFieldValueAsync<int>(ordinal, cancellationToken).WithCurrentCulture<int>();
			int[] ints = this._ints;
			int num2 = this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal];
			ints[num2] = num;
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000AFBC5 File Offset: 0x000ADDC5
		private void ReadLong(DbDataReader reader, int ordinal)
		{
			this._longs[this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal]] = reader.GetInt64(ordinal);
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000AFBEC File Offset: 0x000ADDEC
		private async Task ReadLongAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			long num = await reader.GetFieldValueAsync<long>(ordinal, cancellationToken).WithCurrentCulture<long>();
			long[] longs = this._longs;
			int num2 = this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal];
			longs[num2] = num;
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000AFC49 File Offset: 0x000ADE49
		private void ReadObject(DbDataReader reader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = reader.GetValue(ordinal);
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000AFC70 File Offset: 0x000ADE70
		private async Task ReadObjectAsync(DbDataReader reader, int ordinal, CancellationToken cancellationToken)
		{
			object obj = await reader.GetFieldValueAsync<object>(ordinal, cancellationToken).WithCurrentCulture<object>();
			object[] objects = this._objects;
			int num = this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal];
			objects[num] = obj;
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000AFCCD File Offset: 0x000ADECD
		private void ReadGeography(DbSpatialDataReader spatialReader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = spatialReader.GetGeography(ordinal);
		}

		// Token: 0x06003635 RID: 13877 RVA: 0x000AFCF4 File Offset: 0x000ADEF4
		private async Task ReadGeographyAsync(DbSpatialDataReader spatialReader, int ordinal, CancellationToken cancellationToken)
		{
			DbGeography dbGeography = await spatialReader.GetGeographyAsync(ordinal, cancellationToken).WithCurrentCulture<DbGeography>();
			object[] objects = this._objects;
			int num = this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal];
			objects[num] = dbGeography;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000AFD51 File Offset: 0x000ADF51
		private void ReadGeometry(DbSpatialDataReader spatialReader, int ordinal)
		{
			this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]] = spatialReader.GetGeometry(ordinal);
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000AFD78 File Offset: 0x000ADF78
		private async Task ReadGeometryAsync(DbSpatialDataReader spatialReader, int ordinal, CancellationToken cancellationToken)
		{
			DbGeometry dbGeometry = await spatialReader.GetGeometryAsync(ordinal, cancellationToken).WithCurrentCulture<DbGeometry>();
			object[] objects = this._objects;
			int num = this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal];
			objects[num] = dbGeometry;
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000AFDD5 File Offset: 0x000ADFD5
		public override bool GetBoolean(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Bool)
			{
				return this._bools[this._currentRowNumber * this._boolCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<bool>(ordinal);
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000AFE0B File Offset: 0x000AE00B
		public override byte GetByte(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Byte)
			{
				return this._bytes[this._currentRowNumber * this._byteCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<byte>(ordinal);
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000AFE3D File Offset: 0x000AE03D
		public override char GetChar(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Char)
			{
				return this._chars[this._currentRowNumber * this._charCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<char>(ordinal);
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000AFE6F File Offset: 0x000AE06F
		public override DateTime GetDateTime(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.DateTime)
			{
				return this._dateTimes[this._currentRowNumber * this._dateTimeCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<DateTime>(ordinal);
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000AFEA5 File Offset: 0x000AE0A5
		public override decimal GetDecimal(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Decimal)
			{
				return this._decimals[this._currentRowNumber * this._decimalCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<decimal>(ordinal);
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000AFEDB File Offset: 0x000AE0DB
		public override double GetDouble(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Double)
			{
				return this._doubles[this._currentRowNumber * this._doubleCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<double>(ordinal);
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000AFF0D File Offset: 0x000AE10D
		public override float GetFloat(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Float)
			{
				return this._floats[this._currentRowNumber * this._floatCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<float>(ordinal);
		}

		// Token: 0x0600363F RID: 13887 RVA: 0x000AFF3F File Offset: 0x000AE13F
		public override Guid GetGuid(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Guid)
			{
				return this._guids[this._currentRowNumber * this._guidCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<Guid>(ordinal);
		}

		// Token: 0x06003640 RID: 13888 RVA: 0x000AFF76 File Offset: 0x000AE176
		public override short GetInt16(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Short)
			{
				return this._shorts[this._currentRowNumber * this._shortCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<short>(ordinal);
		}

		// Token: 0x06003641 RID: 13889 RVA: 0x000AFFA9 File Offset: 0x000AE1A9
		public override int GetInt32(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Int)
			{
				return this._ints[this._currentRowNumber * this._intCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<int>(ordinal);
		}

		// Token: 0x06003642 RID: 13890 RVA: 0x000AFFDC File Offset: 0x000AE1DC
		public override long GetInt64(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Long)
			{
				return this._longs[this._currentRowNumber * this._longCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<long>(ordinal);
		}

		// Token: 0x06003643 RID: 13891 RVA: 0x000B000F File Offset: 0x000AE20F
		public override string GetString(int ordinal)
		{
			if (this._columnTypeCases[ordinal] == ShapedBufferedDataRecord.TypeCase.Object)
			{
				return (string)this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]];
			}
			return this.GetFieldValue<string>(ordinal);
		}

		// Token: 0x06003644 RID: 13892 RVA: 0x000B0046 File Offset: 0x000AE246
		public override object GetValue(int ordinal)
		{
			return this.GetFieldValue<object>(ordinal);
		}

		// Token: 0x06003645 RID: 13893 RVA: 0x000B004F File Offset: 0x000AE24F
		public override int GetValues(object[] values)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06003646 RID: 13894 RVA: 0x000B0058 File Offset: 0x000AE258
		public override T GetFieldValue<T>(int ordinal)
		{
			switch (this._columnTypeCases[ordinal])
			{
			case ShapedBufferedDataRecord.TypeCase.Empty:
				return default(T);
			case ShapedBufferedDataRecord.TypeCase.Bool:
				return (T)((object)this.GetBoolean(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Byte:
				return (T)((object)this.GetByte(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Char:
				return (T)((object)this.GetChar(ordinal));
			case ShapedBufferedDataRecord.TypeCase.DateTime:
				return (T)((object)this.GetDateTime(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Decimal:
				return (T)((object)this.GetDecimal(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Double:
				return (T)((object)this.GetDouble(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Float:
				return (T)((object)this.GetFloat(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Guid:
				return (T)((object)this.GetGuid(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Short:
				return (T)((object)this.GetInt16(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Int:
				return (T)((object)this.GetInt32(ordinal));
			case ShapedBufferedDataRecord.TypeCase.Long:
				return (T)((object)this.GetInt64(ordinal));
			}
			return (T)((object)this._objects[this._currentRowNumber * this._objectCount + this._ordinalToIndexMap[ordinal]]);
		}

		// Token: 0x06003647 RID: 13895 RVA: 0x000B019F File Offset: 0x000AE39F
		public override Task<T> GetFieldValueAsync<T>(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<T>(this.GetFieldValue<T>(ordinal));
		}

		// Token: 0x06003648 RID: 13896 RVA: 0x000B01AD File Offset: 0x000AE3AD
		public override bool IsDBNull(int ordinal)
		{
			return this._nulls[this._currentRowNumber * this._nullCount + this._nullOrdinalToIndexMap[ordinal]];
		}

		// Token: 0x06003649 RID: 13897 RVA: 0x000B01D0 File Offset: 0x000AE3D0
		public override Task<bool> IsDBNullAsync(int ordinal, CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.IsDBNull(ordinal));
		}

		// Token: 0x0600364A RID: 13898 RVA: 0x000B01E0 File Offset: 0x000AE3E0
		public override bool Read()
		{
			int num = this._currentRowNumber + 1;
			this._currentRowNumber = num;
			return base.IsDataReady = num < this._rowCount;
		}

		// Token: 0x0600364B RID: 13899 RVA: 0x000B020F File Offset: 0x000AE40F
		public override Task<bool> ReadAsync(CancellationToken cancellationToken)
		{
			return Task.FromResult<bool>(this.Read());
		}

		// Token: 0x0400117F RID: 4479
		private int _rowCapacity = 1;

		// Token: 0x04001180 RID: 4480
		private BitArray _bools;

		// Token: 0x04001181 RID: 4481
		private bool[] _tempBools;

		// Token: 0x04001182 RID: 4482
		private int _boolCount;

		// Token: 0x04001183 RID: 4483
		private byte[] _bytes;

		// Token: 0x04001184 RID: 4484
		private int _byteCount;

		// Token: 0x04001185 RID: 4485
		private char[] _chars;

		// Token: 0x04001186 RID: 4486
		private int _charCount;

		// Token: 0x04001187 RID: 4487
		private DateTime[] _dateTimes;

		// Token: 0x04001188 RID: 4488
		private int _dateTimeCount;

		// Token: 0x04001189 RID: 4489
		private decimal[] _decimals;

		// Token: 0x0400118A RID: 4490
		private int _decimalCount;

		// Token: 0x0400118B RID: 4491
		private double[] _doubles;

		// Token: 0x0400118C RID: 4492
		private int _doubleCount;

		// Token: 0x0400118D RID: 4493
		private float[] _floats;

		// Token: 0x0400118E RID: 4494
		private int _floatCount;

		// Token: 0x0400118F RID: 4495
		private Guid[] _guids;

		// Token: 0x04001190 RID: 4496
		private int _guidCount;

		// Token: 0x04001191 RID: 4497
		private short[] _shorts;

		// Token: 0x04001192 RID: 4498
		private int _shortCount;

		// Token: 0x04001193 RID: 4499
		private int[] _ints;

		// Token: 0x04001194 RID: 4500
		private int _intCount;

		// Token: 0x04001195 RID: 4501
		private long[] _longs;

		// Token: 0x04001196 RID: 4502
		private int _longCount;

		// Token: 0x04001197 RID: 4503
		private object[] _objects;

		// Token: 0x04001198 RID: 4504
		private int _objectCount;

		// Token: 0x04001199 RID: 4505
		private int[] _ordinalToIndexMap;

		// Token: 0x0400119A RID: 4506
		private BitArray _nulls;

		// Token: 0x0400119B RID: 4507
		private bool[] _tempNulls;

		// Token: 0x0400119C RID: 4508
		private int _nullCount;

		// Token: 0x0400119D RID: 4509
		private int[] _nullOrdinalToIndexMap;

		// Token: 0x0400119E RID: 4510
		private ShapedBufferedDataRecord.TypeCase[] _columnTypeCases;

		// Token: 0x02000A5A RID: 2650
		private enum TypeCase
		{
			// Token: 0x04002A84 RID: 10884
			Empty,
			// Token: 0x04002A85 RID: 10885
			Object,
			// Token: 0x04002A86 RID: 10886
			Bool,
			// Token: 0x04002A87 RID: 10887
			Byte,
			// Token: 0x04002A88 RID: 10888
			Char,
			// Token: 0x04002A89 RID: 10889
			DateTime,
			// Token: 0x04002A8A RID: 10890
			Decimal,
			// Token: 0x04002A8B RID: 10891
			Double,
			// Token: 0x04002A8C RID: 10892
			Float,
			// Token: 0x04002A8D RID: 10893
			Guid,
			// Token: 0x04002A8E RID: 10894
			Short,
			// Token: 0x04002A8F RID: 10895
			Int,
			// Token: 0x04002A90 RID: 10896
			Long,
			// Token: 0x04002A91 RID: 10897
			DbGeography,
			// Token: 0x04002A92 RID: 10898
			DbGeometry
		}
	}
}

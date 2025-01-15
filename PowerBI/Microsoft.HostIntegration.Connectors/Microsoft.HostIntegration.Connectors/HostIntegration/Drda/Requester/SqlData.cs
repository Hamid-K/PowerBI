using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Tracing;

namespace Microsoft.HostIntegration.Drda.Requester
{
	// Token: 0x02000901 RID: 2305
	internal class SqlData
	{
		// Token: 0x060048BE RID: 18622 RVA: 0x001095C6 File Offset: 0x001077C6
		internal SqlData()
		{
			this.Value = null;
			this.Length = 0;
			this.DataLengthInExtdta = 0;
			this.IsDataReady = false;
			this.LobMode = 1;
		}

		// Token: 0x17001198 RID: 4504
		// (get) Token: 0x060048BF RID: 18623 RVA: 0x001095F1 File Offset: 0x001077F1
		// (set) Token: 0x060048C0 RID: 18624 RVA: 0x001095F9 File Offset: 0x001077F9
		internal object Value { get; set; }

		// Token: 0x17001199 RID: 4505
		// (get) Token: 0x060048C1 RID: 18625 RVA: 0x00109602 File Offset: 0x00107802
		// (set) Token: 0x060048C2 RID: 18626 RVA: 0x0010960A File Offset: 0x0010780A
		internal int Length { get; private set; }

		// Token: 0x1700119A RID: 4506
		// (get) Token: 0x060048C3 RID: 18627 RVA: 0x00109613 File Offset: 0x00107813
		// (set) Token: 0x060048C4 RID: 18628 RVA: 0x0010961B File Offset: 0x0010781B
		internal int DataLengthInExtdta { get; private set; }

		// Token: 0x1700119B RID: 4507
		// (get) Token: 0x060048C5 RID: 18629 RVA: 0x00109624 File Offset: 0x00107824
		// (set) Token: 0x060048C6 RID: 18630 RVA: 0x0010962C File Offset: 0x0010782C
		internal bool IsDataReady { get; set; }

		// Token: 0x1700119C RID: 4508
		// (get) Token: 0x060048C7 RID: 18631 RVA: 0x00109635 File Offset: 0x00107835
		// (set) Token: 0x060048C8 RID: 18632 RVA: 0x0010963D File Offset: 0x0010783D
		internal int LobMode { get; set; }

		// Token: 0x060048C9 RID: 18633 RVA: 0x00109648 File Offset: 0x00107848
		internal static async Task<SqlData> ConvertFromDrdaType(SqlColumnInfo columnInfo, Requester requester, bool isAsync, CancellationToken cancellationToken)
		{
			byte drdaType = columnInfo.DrdaServerType;
			int length = ((columnInfo.DataLength == 0) ? columnInfo.Length : columnInfo.DataLength);
			int codePage = (int)columnInfo.Ccsid;
			DdmReader reader = requester.ConnectionManager.DdmReader;
			SqlData data = new SqlData();
			bool isNullable = false;
			Exception exception = null;
			try
			{
				if (drdaType % 2 == 1)
				{
					TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<byte> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte>);
					}
					if (taskAwaiter.GetResult() == 255)
					{
						data.Value = DBNull.Value;
						data.Length = 1;
						data.IsDataReady = true;
						if (columnInfo.IsLob)
						{
							requester.LobLength.Add(0);
							requester.Mode.Add(1);
							if (requester.ProgRef == null)
							{
								requester.ProgRef = new byte[1][];
							}
							else
							{
								Array.Resize<byte[]>(ref requester.ProgRef, requester.ProgRef.Length + 1);
							}
						}
						return data;
					}
					isNullable = true;
				}
				data.IsDataReady = true;
				byte b = drdaType;
				int lengthData;
				switch (b)
				{
				case 2:
				case 3:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadInt32Async(isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 4:
				case 5:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadInt16Async(isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 6:
				case 7:
				case 8:
				case 9:
				case 18:
				case 19:
				case 20:
				case 21:
				case 24:
				case 25:
				case 26:
				case 27:
				case 28:
				case 29:
				case 44:
				case 45:
				case 46:
				case 47:
					goto IL_2110;
				case 10:
				case 11:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadDoubleAsync(length, requester.TypeDefinitionName, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 12:
				case 13:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadFloatAsync(length, requester.TypeDefinitionName, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 14:
				case 15:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadDecimalAsync(length, (int)columnInfo.Precision, (int)columnInfo.Scale, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 16:
				case 17:
				{
					if (length < (int)columnInfo.Precision)
					{
						length = (int)columnInfo.Precision;
					}
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadZonedDecimalAsync(length, (int)columnInfo.Precision, (int)columnInfo.Scale, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 22:
				case 23:
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadInt64Async(isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				case 30:
				case 31:
				case 40:
				case 41:
				case 42:
				case 43:
					goto IL_1610;
				case 32:
				case 33:
					try
					{
						bool flag = false;
						if (columnInfo.DrdaType == DrdaClientType.Char)
						{
							flag = true;
						}
						if (flag)
						{
							SqlData sqlData = data;
							sqlData.Value = await reader.ReadDateDateTimeAsCharAsync(codePage, isAsync, cancellationToken);
							sqlData = null;
							data.Length = length;
						}
						else
						{
							SqlData sqlData = data;
							sqlData.Value = await reader.ReadDateAsync(codePage, isAsync, cancellationToken);
							sqlData = null;
							data.Length = length;
						}
						goto IL_2142;
					}
					catch (Exception ex)
					{
						exception = requester.MakeException(RequesterResource.WrongDateValue(drdaType), "HY000", -343);
						throw ex;
					}
					break;
				case 34:
				case 35:
					break;
				case 36:
				case 37:
					goto IL_0D30;
				case 38:
				case 39:
					goto IL_1557;
				case 48:
				case 49:
				case 60:
				case 61:
					if (length != 0)
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadStringAsync(length, codePage, (int)drdaType, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
						goto IL_2142;
					}
					data.Value = string.Empty;
					goto IL_2142;
				case 50:
				case 51:
				case 52:
				case 53:
				case 62:
				case 63:
				case 64:
				case 65:
					lengthData = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					if (lengthData != 0)
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadStringAsync(lengthData, codePage, (int)drdaType, isAsync, cancellationToken);
						sqlData = null;
					}
					else
					{
						data.Value = string.Empty;
					}
					data.Length = lengthData + 2;
					goto IL_2142;
				case 54:
				case 55:
					goto IL_0F0B;
				case 56:
				case 57:
				case 58:
				case 59:
					if ((requester.HostType == HostType.DB2 || requester.HostType == HostType.MVS || requester.HostType == HostType.AS400 || requester.HostType == HostType.RS6000) && codePage == 1200)
					{
						codePage = 1201;
					}
					lengthData = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					if (lengthData != 0)
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadDSStringAsync(lengthData * 2, codePage, (int)drdaType, requester.Endian, isAsync, cancellationToken);
						sqlData = null;
					}
					else
					{
						data.Value = string.Empty;
					}
					data.Length = lengthData * 2 + 2;
					goto IL_2142;
				default:
					switch (b)
					{
					case 183:
						lengthData = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						if (lengthData != 0)
						{
							SqlData sqlData = data;
							sqlData.Value = await reader.ReadStringAsync(lengthData, codePage, isAsync, cancellationToken);
							sqlData = null;
						}
						else
						{
							data.Value = string.Empty;
						}
						data.Length = lengthData + 2;
						goto IL_2142;
					case 184:
					case 185:
					case 188:
					case 189:
					case 190:
					case 191:
						goto IL_2110;
					case 186:
					case 187:
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadDecimalFloatAsync(length, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
						goto IL_2142;
					}
					case 192:
					case 193:
						goto IL_1557;
					case 194:
					case 195:
						goto IL_1610;
					case 196:
					case 197:
					case 198:
					case 199:
					case 200:
					case 201:
					case 202:
					case 203:
					case 204:
					case 205:
					case 206:
					case 207:
						lengthData = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						await reader.SkipBytesAsync(3, isAsync, cancellationToken);
						if (requester.TracePoint.IsEnabled(TraceFlags.Verbose))
						{
							requester.TracePoint.Trace(TraceFlags.Verbose, "LOB data flag: " + lengthData.ToString());
						}
						if (requester.ProgRef == null)
						{
							requester.ProgRef = new byte[1][];
						}
						else if (!requester.BlobDataOverrun)
						{
							Array.Resize<byte[]>(ref requester.ProgRef, requester.ProgRef.Length + 1);
						}
						if (lengthData == 2 || lengthData == 3)
						{
							data.IsDataReady = false;
							await reader.ReadByteAsync(isAsync, cancellationToken);
							SqlData sqlData = data;
							sqlData.DataLengthInExtdta = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
							sqlData = null;
							if (lengthData == 2)
							{
								if (data.DataLengthInExtdta == 0 && SqlType.IsClob(columnInfo.SqlType))
								{
									data.Value = string.Empty;
								}
								data.Length = 9;
								data.LobMode = 2;
								requester.Mode.Add(2);
								requester.LobLength.Add(0);
							}
							else
							{
								byte[] array = await reader.ReadBytesAsync(8, isAsync, cancellationToken);
								requester.ProgRef[requester.ProgRef.Length - 1] = array;
								requester.LobLength.Add(data.DataLengthInExtdta);
								requester.Mode.Add(3);
								data.Length = 18;
								data.LobMode = 3;
								requester.AnyProgRef = 3;
							}
						}
						else
						{
							if (lengthData != 1)
							{
								exception = requester.MakeException(RequesterResource.LobNotSupported(lengthData), "HY000", -343);
								throw exception;
							}
							await reader.ReadByteAsync(isAsync, cancellationToken);
							int lengthData2 = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
							if (SqlType.IsClob(columnInfo.SqlType))
							{
								int num = (int)columnInfo.Ccsid;
								if (num <= 0 && (drdaType == 198 || drdaType == 199))
								{
									num = reader.Ccsid._ccsidxml;
								}
								if (drdaType == 204 || drdaType == 205)
								{
									if (num <= 0)
									{
										num = reader.Ccsid._ccsiddbc;
									}
									if (lengthData2 == 0)
									{
										data.Value = string.Empty;
									}
									else
									{
										lengthData2 *= 2;
										SqlData sqlData = data;
										sqlData.Value = await reader.ReadDSStringAsync(lengthData2, num, 54, EndianType.LittleEndian, isAsync, cancellationToken);
										sqlData = null;
									}
								}
								else
								{
									if (num <= 0)
									{
										num = reader.Ccsid._ccsidsbc;
									}
									if (lengthData2 != 0)
									{
										if ((drdaType == 198 || drdaType == 199) && requester.IsDb2Gateway)
										{
											SqlData sqlData = data;
											sqlData.Value = await reader.ReadStringXMLAsync(lengthData2, num, 48, isAsync, cancellationToken);
											sqlData = null;
										}
										else if ((drdaType == 198 || drdaType == 199) && requester.XMLAsBinary)
										{
											SqlData sqlData = data;
											sqlData.Value = await reader.ReadBytesAsync(lengthData2, isAsync, cancellationToken);
											sqlData = null;
										}
										else
										{
											SqlData sqlData = data;
											sqlData.Value = await reader.ReadStringAsync(lengthData2, num, 48, isAsync, cancellationToken);
											sqlData = null;
										}
									}
									else
									{
										data.Value = string.Empty;
									}
								}
								num = reader.Ccsid._ccsidsbc;
							}
							else
							{
								SqlData sqlData = data;
								sqlData.Value = await reader.ReadBytesAsync(lengthData2, isAsync, cancellationToken);
								sqlData = null;
							}
							requester.Mode.Add(1);
							requester.LobLength.Add(0);
							data.Length = 9 + lengthData2;
						}
						requester.BlobDataOverrun = false;
						goto IL_2142;
					default:
						goto IL_2110;
					}
					break;
				}
				try
				{
					bool flag2 = false;
					if (columnInfo.DrdaType == DrdaClientType.Char)
					{
						flag2 = true;
					}
					if (flag2)
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadTimeDateTimeAsCharAsync(codePage, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
					}
					else
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadTimeAsync(codePage, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
					}
					goto IL_2142;
				}
				catch (Exception ex2)
				{
					exception = requester.MakeException(RequesterResource.WrongTimeValue(drdaType), "HY000", -343);
					throw ex2;
				}
				IL_0D30:
				try
				{
					bool flag3 = false;
					int num2 = codePage;
					if (num2 == 0)
					{
						num2 = requester.CcsidHost._ccsidmbc;
					}
					if (columnInfo.DrdaType == DrdaClientType.Char)
					{
						flag3 = true;
					}
					if (flag3)
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadTimestampDateTimeAsCharAsync(num2, length, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
					}
					else
					{
						SqlData sqlData = data;
						sqlData.Value = await reader.ReadTimestampAsync(num2, length, isAsync, cancellationToken);
						sqlData = null;
						data.Length = length;
					}
					goto IL_2142;
				}
				catch (Exception ex3)
				{
					exception = requester.MakeException(RequesterResource.WrongDateTimeValue(drdaType), "HY000", -343);
					throw ex3;
				}
				IL_0F0B:
				if ((requester.HostType == HostType.DB2 || requester.HostType == HostType.MVS) && codePage == 1200)
				{
					codePage = 1201;
				}
				if (length != 0)
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadDSStringAsync(length * 2, codePage, (int)drdaType, requester.Endian, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length * 2;
					goto IL_2142;
				}
				data.Value = string.Empty;
				goto IL_2142;
				IL_1557:
				if (length != 0)
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadBytesAsync(length, isAsync, cancellationToken);
					sqlData = null;
					data.Length = length;
					goto IL_2142;
				}
				goto IL_2142;
				IL_1610:
				lengthData = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				if (lengthData > 0)
				{
					SqlData sqlData = data;
					sqlData.Value = await reader.ReadBytesAsync(lengthData, isAsync, cancellationToken);
					sqlData = null;
				}
				data.Length = lengthData + 2;
				goto IL_2142;
				IL_2110:
				exception = requester.MakeException(RequesterResource.TypeNotSupported(drdaType), "HY000", -343);
				throw exception;
				IL_2142:
				if (isNullable)
				{
					data.Length++;
				}
			}
			catch (DrdaException ex4)
			{
				if (ex4.ErrorCodePoint == ErrorCodePoint.SYNTAXRM && ex4.ErrorCode == 11)
				{
					requester.BlobDataOverrun = true;
					if (requester.TracePoint.IsEnabled(TraceFlags.Information))
					{
						requester.TracePoint.Trace(TraceFlags.Information, "Found partial row...");
					}
					throw ex4;
				}
				data.IsDataReady = false;
				throw (exception != null) ? exception : ex4;
			}
			return data;
		}
	}
}

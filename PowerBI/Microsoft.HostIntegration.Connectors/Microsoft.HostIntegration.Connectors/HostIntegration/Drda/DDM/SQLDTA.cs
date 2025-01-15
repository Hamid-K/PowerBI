using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008CE RID: 2254
	public class SQLDTA : AbstractDdmObject
	{
		// Token: 0x1700113D RID: 4413
		// (get) Token: 0x06004797 RID: 18327 RVA: 0x000FFBC6 File Offset: 0x000FDDC6
		// (set) Token: 0x06004798 RID: 18328 RVA: 0x000FFBCE File Offset: 0x000FDDCE
		public bool IsInsertMultipleRows { get; set; }

		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06004799 RID: 18329 RVA: 0x000FFBD7 File Offset: 0x000FDDD7
		// (set) Token: 0x0600479A RID: 18330 RVA: 0x000FFBDF File Offset: 0x000FDDDF
		public IEnumerable<object[]> Rows { get; set; }

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x0600479B RID: 18331 RVA: 0x000FFBE8 File Offset: 0x000FDDE8
		// (set) Token: 0x0600479C RID: 18332 RVA: 0x000FFBF0 File Offset: 0x000FDDF0
		public SQLDTA.BatchMode RowMode { get; set; }

		// Token: 0x17001140 RID: 4416
		// (get) Token: 0x0600479D RID: 18333 RVA: 0x000FFBF9 File Offset: 0x000FDDF9
		// (set) Token: 0x0600479E RID: 18334 RVA: 0x000FFC01 File Offset: 0x000FDE01
		public string TypeDefName { get; set; }

		// Token: 0x17001141 RID: 4417
		// (get) Token: 0x0600479F RID: 18335 RVA: 0x000FFC0A File Offset: 0x000FDE0A
		public List<List<DrdaParameterInfo>> ParmsList
		{
			get
			{
				return this.parmsList;
			}
		}

		// Token: 0x17001142 RID: 4418
		// (get) Token: 0x060047A0 RID: 18336 RVA: 0x000FFC12 File Offset: 0x000FDE12
		// (set) Token: 0x060047A1 RID: 18337 RVA: 0x000FFC1A File Offset: 0x000FDE1A
		public Func<ushort, ushort> CcsidConvert { get; set; }

		// Token: 0x060047A2 RID: 18338 RVA: 0x000FFC24 File Offset: 0x000FDE24
		public SQLDTA(IDatabase database, Ccsid ccsid)
			: base(database)
		{
			this._ccsid = ccsid;
			this.IsInsertMultipleRows = false;
			this.RowMode = SQLDTA.BatchMode.ColumnWide;
			this.Rows = null;
			this.CcsidConvert = null;
			if (database == null)
			{
				this.TypeDefName = "QTDSQLX86";
				return;
			}
			this.TypeDefName = database.Typdefnam;
		}

		// Token: 0x060047A3 RID: 18339 RVA: 0x000FFCAD File Offset: 0x000FDEAD
		public override string ToString()
		{
			return string.Format("SQLDTA[ccsid={0};parms={1}]", this._ccsid, this.GetParmsAsString());
		}

		// Token: 0x060047A4 RID: 18340 RVA: 0x000FFCC8 File Offset: 0x000FDEC8
		protected string GetParmsAsString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (DrdaParameterInfo drdaParameterInfo in this.parms)
			{
				stringBuilder.Append(string.Format("[precision={0};scale={1};length={2};sqltype={3};value={4}]", new object[]
				{
					drdaParameterInfo.Precision,
					drdaParameterInfo.Scale,
					drdaParameterInfo.Length,
					drdaParameterInfo.Type,
					(drdaParameterInfo.Value == null) ? "" : drdaParameterInfo.Value.ToString().Replace("{", "{{").Replace("}", "}}")
				}));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x17001143 RID: 4419
		// (get) Token: 0x060047A5 RID: 18341 RVA: 0x000FFDC4 File Offset: 0x000FDFC4
		// (set) Token: 0x060047A6 RID: 18342 RVA: 0x000FFE1F File Offset: 0x000FE01F
		public List<DrdaParameterInfo> Parms
		{
			get
			{
				if (!this.IsInsertMultipleRows)
				{
					return this.parms;
				}
				for (int i = 0; i < this.parmsList.Count; i++)
				{
					this.parms[i] = this.parmsList[i][0].Clone();
				}
				return this.parms;
			}
			set
			{
				this.parms = value;
			}
		}

		// Token: 0x060047A7 RID: 18343 RVA: 0x000FFE28 File Offset: 0x000FE028
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SQLDTA);
			if (this.IsInsertMultipleRows && this.RowMode == SQLDTA.BatchMode.ColumnWide)
			{
				this.WriteFDOEXT(writer);
			}
			writer.WriteBeginDdm(CodePoint.FDODSC);
			int count = this.parms.Count;
			if (count < 84)
			{
				writer.WriteByte(3 * count + 3);
			}
			else
			{
				writer.WriteByte(255);
			}
			writer.WriteByte(118);
			writer.WriteByte(208);
			int i = 0;
			while (i < count)
			{
				if (i > 0)
				{
					if (count - i < 84)
					{
						writer.WriteByte(3 + (count - i) * 3);
					}
					else
					{
						writer.WriteByte(255);
					}
					writer.WriteByte(127);
					writer.WriteByte(0);
				}
				int num = 0;
				while (num < 84 && i < count)
				{
					ushort num2 = this.parms[i].Length;
					int type = (int)this.parms[i].Type;
					if (type <= 17)
					{
						if (type - 14 > 1)
						{
							if (type - 16 <= 1)
							{
								DrdaParameterInfo drdaParameterInfo = this.parms[i];
								drdaParameterInfo.Type -= 2;
								num2 = (ushort)(((int)(this.parms[i].Precision & 255) << 8) | (int)(this.parms[i].Scale & 255));
							}
						}
						else
						{
							num2 = (ushort)(((int)(this.parms[i].Precision & 255) << 8) | (int)(this.parms[i].Scale & 255));
						}
					}
					else
					{
						switch (type)
						{
						case 32:
						case 33:
							num2 = 10;
							break;
						case 34:
						case 35:
							num2 = 8;
							break;
						case 36:
						case 37:
							num2 = 26;
							break;
						case 38:
						case 39:
						{
							DrdaParameterInfo drdaParameterInfo2 = this.parms[i];
							drdaParameterInfo2.Type += 2;
							break;
						}
						default:
							switch (type)
							{
							case 196:
							case 198:
								this.parms[i].Type = 202;
								num2 = 32777;
								break;
							case 197:
							case 199:
								this.parms[i].Type = 203;
								num2 = 32777;
								break;
							case 200:
							case 201:
							case 202:
							case 203:
							case 204:
							case 205:
							case 206:
							case 207:
								num2 = 32777;
								break;
							}
							break;
						}
					}
					if (this.parms[i].InOutType == 4 && (this.parms[i].Type & 1) != 1)
					{
						writer.WriteByte((int)(this.parms[i].Type | 1));
					}
					else
					{
						writer.WriteByte((int)this.parms[i].Type);
					}
					writer.WriteInt16((int)num2, EndianType.BigEndian);
					i++;
					num++;
				}
			}
			writer.WriteBytes(Constants.SQLDTA_RLO);
			if (this.IsInsertMultipleRows && this.RowMode == SQLDTA.BatchMode.RowWide)
			{
				writer.WriteBytes(Constants.SQLDTA_RLO_ROW_WIDE);
			}
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.FDODTA);
			Action<DrdaParameterInfo> action = delegate(DrdaParameterInfo parmInfo)
			{
				if (parmInfo.InOutType == 4)
				{
					writer.WriteByte(255);
					return;
				}
				if ((parmInfo.Type & 1) == 1)
				{
					if (parmInfo.Value == null || parmInfo.Value == DBNull.Value)
					{
						writer.WriteByte(255);
						return;
					}
					writer.WriteByte(0);
				}
				this.WriteParms(writer, parmInfo);
			};
			if (this.IsInsertMultipleRows)
			{
				if (this.RowMode == SQLDTA.BatchMode.ColumnWide)
				{
					this.sqldtaOffsets.Clear();
					writer.WriteByte(0);
					int dssLength = writer.DssLength;
					for (int j = 0; j < this.parms.Count; j++)
					{
						this.sqldtaOffsets.Add(writer.DssLength - dssLength);
						DrdaParameterInfo drdaParameterInfo3 = this.parms[j];
						foreach (object[] array in this.Rows)
						{
							drdaParameterInfo3.Value = array[j];
							action(drdaParameterInfo3);
						}
					}
				}
				else
				{
					foreach (object[] array2 in this.Rows)
					{
						writer.WriteByte(0);
						for (int k = 0; k < this.parms.Count; k++)
						{
							DrdaParameterInfo drdaParameterInfo4 = this.parms[k];
							drdaParameterInfo4.Value = array2[k];
							action(drdaParameterInfo4);
						}
					}
				}
				writer.WriteEndDdm();
			}
			else
			{
				writer.WriteByte(0);
				this.parms.ForEach(action);
				writer.WriteEndDdm();
			}
			if (this.IsInsertMultipleRows && this.RowMode == SQLDTA.BatchMode.ColumnWide)
			{
				this.WriteFDOOFF(writer);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060047A8 RID: 18344 RVA: 0x00100370 File Offset: 0x000FE570
		private void WriteParms(DdmWriter writer, DrdaParameterInfo parm)
		{
			int type = (int)parm.Type;
			ushort length = parm.Length;
			int num = (int)(parm.MDDoverride ? parm.CCSID : -1);
			EndianType endianType = EndianType.LittleEndian;
			if (type % 2 == 1 && (parm.Value == null || parm.Value == DBNull.Value))
			{
				writer.WriteByte(255);
				return;
			}
			switch (type)
			{
			case 2:
			case 3:
				writer.WriteInt32(Convert.ToInt32(parm.Value), endianType);
				return;
			case 4:
			case 5:
				writer.WriteInt16(Convert.ToInt32(parm.Value), endianType);
				return;
			case 6:
			case 7:
			case 8:
			case 9:
			case 16:
			case 17:
			case 18:
			case 19:
			case 20:
			case 21:
			case 30:
			case 31:
			case 44:
			case 45:
			case 46:
			case 47:
				goto IL_052A;
			case 10:
			case 11:
				writer.WriteDouble(Convert.ToDouble(parm.Value), "QTDSQLX86");
				return;
			case 12:
			case 13:
				writer.WriteFloat(Convert.ToSingle(parm.Value), "QTDSQLX86");
				return;
			case 14:
			case 15:
			{
				ushort num2 = parm.Precision;
				ushort num3 = parm.Scale;
				if ((length & 65280) != 0)
				{
					num2 = (ushort)((length >> 8) & 255);
					num3 = length & 255;
				}
				writer.WriteDecimal(Convert.ToDecimal(parm.Value), (int)num2, (int)num3);
				return;
			}
			case 22:
			case 23:
				writer.WriteInt64(Convert.ToInt64(parm.Value), endianType);
				return;
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
				writer.WriteInt32((int)parm.Value);
				return;
			case 32:
			case 33:
				try
				{
					writer.WriteDate(Convert.ToDateTime(parm.Value), (num > 0) ? num : writer.Ccsid._ccsidsbc);
					return;
				}
				catch (Exception)
				{
					throw new SqlExceptionInfo(new Exception("The syntax of the string representation of a datetime value is incorrect"), "-180", "22007", "The syntax of the string representation of a datetime value is incorrect");
				}
				break;
			case 34:
			case 35:
				break;
			case 36:
			case 37:
				goto IL_0347;
			case 38:
			case 39:
				goto IL_046A;
			case 40:
			case 41:
			case 42:
			case 43:
				goto IL_047C;
			case 48:
			case 49:
			case 60:
			case 61:
				writer.WriteStringSBCSFixedLength((string)parm.Value, length, num);
				return;
			case 50:
			case 51:
			case 52:
			case 53:
			case 62:
			case 63:
			case 64:
			case 65:
			{
				string text = (string)parm.Value;
				int num4 = (string.IsNullOrEmpty(text) ? 0 : text.Length);
				int num5;
				if (num > 0)
				{
					num5 = writer.GenerateStringMBCS(text, num);
				}
				else
				{
					num5 = writer.GenerateStringMBCS(text);
				}
				writer.WriteInt16(num5, EndianType.BigEndian);
				if (num5 != 0)
				{
					writer.WriteBytes(num5);
					return;
				}
				return;
			}
			case 54:
			case 55:
				writer.WriteStringDBCSFixedLength((string)parm.Value, EndianType.BigEndian, type, length, num);
				return;
			case 56:
			case 57:
			case 58:
			case 59:
			{
				string text2 = (string)parm.Value;
				int num6 = (string.IsNullOrEmpty(text2) ? 0 : text2.Length);
				writer.WriteInt16(num6, EndianType.BigEndian);
				if (num6 == 0)
				{
					return;
				}
				if (num > 0)
				{
					writer.WriteStringDBCS(text2, EndianType.BigEndian, type, num);
					return;
				}
				writer.WriteStringDBCS(text2, EndianType.BigEndian, type);
				return;
			}
			default:
				switch (type)
				{
				case 186:
				case 187:
					writer.WriteDecimalFloat(Convert.ToDecimal(parm.Value), (int)length);
					return;
				case 188:
				case 189:
				case 190:
				case 191:
					goto IL_052A;
				case 192:
				case 193:
					goto IL_046A;
				case 194:
				case 195:
					goto IL_047C;
				case 196:
				case 197:
				case 198:
				case 199:
				case 202:
				case 203:
				case 204:
				case 205:
				case 206:
				case 207:
				{
					parm.IsClob = true;
					writer.WriteByte(2);
					string text3 = parm.Value as string;
					long num7 = (long)((text3 == null) ? 0 : text3.Length);
					writer.WriteInt64(num7, EndianType.BigEndian);
					return;
				}
				case 200:
				case 201:
				{
					parm.IsClob = false;
					writer.WriteByte(2);
					byte[] array = parm.Value as byte[];
					long num8 = (long)((array == null) ? 0 : array.Length);
					writer.WriteInt64(num8, EndianType.BigEndian);
					return;
				}
				default:
					goto IL_052A;
				}
				break;
			}
			try
			{
				if (parm.Value is TimeSpan)
				{
					writer.WriteTime((TimeSpan)parm.Value, (num > 0) ? num : writer.Ccsid._ccsidsbc);
				}
				else
				{
					writer.WriteTime(Convert.ToDateTime(parm.Value) - DateTime.MinValue, (num > 0) ? num : writer.Ccsid._ccsidsbc);
				}
				return;
			}
			catch (Exception)
			{
				throw new SqlExceptionInfo(new Exception("The syntax of the string representation of a datetime value is incorrect"), "-180", "22007", "The syntax of the string representation of a datetime value is incorrect");
			}
			IL_0347:
			if (parm.Value is string)
			{
				writer.WriteStringSBCSFixedLength((string)parm.Value, length, (num > 0) ? num : writer.Ccsid._ccsidsbc);
				return;
			}
			writer.WriteTimestamp(Convert.ToDateTime(parm.Value), (num > 0) ? num : writer.Ccsid._ccsidsbc);
			return;
			IL_046A:
			writer.WriteBytes((byte[])parm.Value);
			return;
			IL_047C:
			byte[] array2 = (byte[])parm.Value;
			int num9 = ((array2 == null) ? 0 : array2.Length);
			writer.WriteInt16(num9, EndianType.BigEndian);
			if (num9 != 0)
			{
				writer.WriteBytes(array2);
				return;
			}
			return;
			IL_052A:
			throw new NotSupportedException("Type not supported when writing Parameters: " + type.ToString());
		}

		// Token: 0x060047A9 RID: 18345 RVA: 0x001008DC File Offset: 0x000FEADC
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			long parentDdmObjectLength = reader.DdmObjectLength;
			long childDdmObjectLengthSum = 0L;
			try
			{
				byte[] fdodtaBytes = null;
				IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
				IEnumerator<ObjectInfo> enumerator = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
				while (childDdmObjectLengthSum < parentDdmObjectLength && (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext()))
				{
					ObjectInfo objectInfo;
					if (isAsync)
					{
						objectInfo = await taskEnumerator.Current;
						if (objectInfo.Equals(ObjectInfo.InvalidInstance))
						{
							break;
						}
					}
					else
					{
						objectInfo = enumerator.Current;
					}
					CodePoint codepoint = objectInfo.Codepoint;
					childDdmObjectLengthSum += objectInfo.Length + 4L;
					base.LogCodePoint(codepoint);
					if (codepoint != CodePoint.FDODSC)
					{
						switch (codepoint)
						{
						case CodePoint.FDODTA:
							if (this.IsInsertMultipleRows)
							{
								ObjectInfo objectInfo2 = objectInfo;
								fdodtaBytes = await reader.ReadBytesAsync((int)objectInfo2.Length, isAsync, cancellationToken);
								continue;
							}
							await this.ParseFDODTAAsync(reader, this.parms, isAsync, cancellationToken);
							continue;
						case CodePoint.FDOEXT:
							await this.ParseFDOEXTAsync(reader, isAsync, cancellationToken);
							this.IsInsertMultipleRows = true;
							continue;
						case CodePoint.FDOOFF:
							await this.ParseFDOOFFAsync(reader, isAsync, cancellationToken);
							continue;
						}
						if (Logger.maxTracingLevel >= 4)
						{
							Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SQLDTA::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
						}
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					}
					else
					{
						await this.ParseFDODSCAsync(reader, this.parms, isAsync, cancellationToken);
					}
				}
				if (this.IsInsertMultipleRows)
				{
					DdmReader ddmReader = new DdmReader(new MemoryStream(fdodtaBytes)
					{
						Position = 0L
					}, reader.CcsidManager, reader.Converter, null, this._tracePoint);
					if (this._database.IsMsDrdaAr)
					{
						await this.ParseFDODTAMsDrdaAsync(ddmReader, fdodtaBytes.Length, this.parmsList, this.parms, isAsync, cancellationToken);
					}
					else
					{
						await this.ParseFDODTAAsync(ddmReader, fdodtaBytes.Length, this.parmsList, this.parms, isAsync, cancellationToken);
						int num = 0;
						foreach (List<DrdaParameterInfo> list in this.parmsList)
						{
							if (list.Count > num)
							{
								num = list.Count;
							}
						}
						foreach (List<DrdaParameterInfo> list2 in this.parmsList)
						{
							while (list2.Count < num)
							{
								list2.Add(list2[list2.Count - 1].Clone());
							}
						}
					}
				}
				fdodtaBytes = null;
				taskEnumerator = null;
				enumerator = null;
			}
			catch (Exception ex)
			{
				Logger.LogException(this._tracePoint, base.DatabaseSessionId, "SQLDTA::Read Message ", ex);
				throw;
			}
		}

		// Token: 0x060047AA RID: 18346 RVA: 0x0010093C File Offset: 0x000FEB3C
		public async Task ParseEXDTAAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			DrdaParameterInfo parameterInfo = null;
			while (this.parms.Count > this._currentExtdtaIndex)
			{
				DrdaParameterInfo drdaParameterInfo = this.parms[this._currentExtdtaIndex];
				this._currentExtdtaIndex++;
				if (drdaParameterInfo.LobPosition == 2)
				{
					parameterInfo = drdaParameterInfo;
					break;
				}
			}
			if (parameterInfo == null)
			{
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SQLDTA.ParseEXDTAAsync: Not find parameter info in SQLDTA for EXTDTA.", Array.Empty<object>());
				}
				throw new ArgumentException("LOB Data has not been described in SQLDTA yet.");
			}
			EXTDTA extdta = new EXTDTA();
			int num = this._ccsid._ccsidsbc;
			if (parameterInfo.MDDoverride)
			{
				num = (int)parameterInfo.CCSID;
			}
			else if (this._database != null && this._database.HostCodePageOverride > 0)
			{
				num = this._database.HostCodePageOverride;
			}
			extdta.Encoding = (ushort)num;
			extdta.IsBlob = !parameterInfo.IsClob;
			extdta.Length = parameterInfo.LobLength;
			extdta.IsNullable = parameterInfo.Type % 2 == 1;
			if (parameterInfo.Type == 205 || parameterInfo.Type == 204)
			{
				extdta.IsGraphic = true;
			}
			await extdta.ReadAsync(reader, isAsync, cancellationToken);
			if (parameterInfo.IsClob)
			{
				parameterInfo.Clob = extdta.Value as string;
			}
			else
			{
				parameterInfo.Blob = extdta.Value as byte[];
			}
		}

		// Token: 0x060047AB RID: 18347 RVA: 0x0010099C File Offset: 0x000FEB9C
		private async Task ParseFDODTAMsDrdaAsync(DdmReader reader, int ddmlength, List<List<DrdaParameterInfo>> parmsList, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			int numofParms = parms.Count;
			List<DrdaParameterInfo> paramListInRows = new List<DrdaParameterInfo>();
			for (int i = 0; i < this.Nbrrow; i++)
			{
				await reader.ReadByteAsync(isAsync, cancellationToken);
				int j = 0;
				while (j < numofParms)
				{
					DrdaParameterInfo parm = parms[j].Clone();
					paramListInRows.Add(parm);
					if (parm.Type % 2 != 1)
					{
						goto IL_0171;
					}
					TaskAwaiter<byte> taskAwaiter = reader.ReadByteAsync(isAsync, cancellationToken).GetAwaiter();
					if (!taskAwaiter.IsCompleted)
					{
						await taskAwaiter;
						TaskAwaiter<byte> taskAwaiter2;
						taskAwaiter = taskAwaiter2;
						taskAwaiter2 = default(TaskAwaiter<byte>);
					}
					if (taskAwaiter.GetResult() != 255)
					{
						goto IL_0171;
					}
					IL_01EB:
					j++;
					continue;
					IL_0171:
					await this.ReadAndSetParamsAsync(reader, parm, isAsync, cancellationToken);
					parm = null;
					goto IL_01EB;
				}
			}
			parmsList.Clear();
			for (int k = 0; k < numofParms; k++)
			{
				List<DrdaParameterInfo> list = new List<DrdaParameterInfo>();
				for (int l = 0; l < this.Nbrrow; l++)
				{
					list.Add(paramListInRows[l * numofParms + k]);
				}
				parmsList.Add(list);
			}
		}

		// Token: 0x060047AC RID: 18348 RVA: 0x00100A0C File Offset: 0x000FEC0C
		private async Task ParseFDODTAAsync(DdmReader reader, int sizeOfFDODTABuffer, List<List<DrdaParameterInfo>> parmsList, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			await reader.ReadByteAsync(isAsync, cancellationToken);
			int numofParms = this.sqldtaOffsets.Count;
			bool containsNumOfRowsVariable = false;
			if (this.arraySizes[this.arraySizes.Count - 1] < 0)
			{
				containsNumOfRowsVariable = true;
				numofParms--;
			}
			for (int i = 0; i < numofParms; i++)
			{
				List<DrdaParameterInfo> parmArray = new List<DrdaParameterInfo>();
				while ((i == numofParms - 1 && (containsNumOfRowsVariable ? (reader.Position < this.sqldtaOffsets[i + 1]) : (reader.Position < sizeOfFDODTABuffer))) || (i < numofParms - 1 && reader.Position < this.sqldtaOffsets[i + 1]))
				{
					DrdaParameterInfo parm = parms[i].Clone();
					parm.CCSID = -1;
					parm.MDDoverride = false;
					foreach (FdocscDrdaTypeMapping fdocscDrdaTypeMapping in this.drdaTypeMappings)
					{
						if (parm.Type == fdocscDrdaTypeMapping.TripletLID)
						{
							parm.Type = fdocscDrdaTypeMapping.FromDrdaType & byte.MaxValue;
							parm.MDDoverride = true;
							parm.CCSID = fdocscDrdaTypeMapping.CCSID;
						}
					}
					if ((parm.Type & 1) == 1)
					{
						TaskAwaiter<int> taskAwaiter = reader.ReadUnsignedByteAsync(isAsync, cancellationToken).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<int> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<int>);
						}
						if ((taskAwaiter.GetResult() & 255) == 255)
						{
							continue;
						}
					}
					await this.ReadAndSetParamsAsync(reader, parm, isAsync, cancellationToken);
					parmArray.Add(parm);
					parm = null;
				}
				parmsList.Add(parmArray);
				parmArray = null;
			}
		}

		// Token: 0x060047AD RID: 18349 RVA: 0x00100A84 File Offset: 0x000FEC84
		private async Task ParseFDOOFFAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this.sqldtaOffsets.Clear();
			while (reader.DdmObjectLength > 0L)
			{
				List<int> list = this.sqldtaOffsets;
				int num = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
				list.Add(num);
				list = null;
			}
		}

		// Token: 0x060047AE RID: 18350 RVA: 0x00100AE4 File Offset: 0x000FECE4
		private async Task ParseFDOEXTAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this.arraySizes.Clear();
			while (reader.DdmObjectLength > 0L)
			{
				List<int> list = this.arraySizes;
				int num = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
				list.Add(num);
				list = null;
			}
		}

		// Token: 0x060047AF RID: 18351 RVA: 0x00100B44 File Offset: 0x000FED44
		protected virtual async Task ParseFDODTAAsync(DdmReader reader, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			await reader.ReadByteAsync(isAsync, cancellationToken);
			int i = 0;
			while (i < parms.Count)
			{
				parms[i].CCSID = -1;
				parms[i].MDDoverride = false;
				foreach (FdocscDrdaTypeMapping fdocscDrdaTypeMapping in this.drdaTypeMappings)
				{
					if (parms[i].Type == fdocscDrdaTypeMapping.TripletLID)
					{
						parms[i].Type = fdocscDrdaTypeMapping.FromDrdaType & byte.MaxValue;
						parms[i].MDDoverride = true;
						parms[i].CCSID = fdocscDrdaTypeMapping.CCSID;
					}
				}
				if ((parms[i].Type & 1) != 1)
				{
					goto IL_020F;
				}
				TaskAwaiter<int> taskAwaiter = reader.ReadUnsignedByteAsync(isAsync, cancellationToken).GetAwaiter();
				if (!taskAwaiter.IsCompleted)
				{
					await taskAwaiter;
					TaskAwaiter<int> taskAwaiter2;
					taskAwaiter = taskAwaiter2;
					taskAwaiter2 = default(TaskAwaiter<int>);
				}
				if ((taskAwaiter.GetResult() & 255) != 255)
				{
					goto IL_020F;
				}
				IL_028D:
				i++;
				continue;
				IL_020F:
				await this.ReadAndSetParamsAsync(reader, parms[i], isAsync, cancellationToken);
				goto IL_028D;
			}
		}

		// Token: 0x060047B0 RID: 18352 RVA: 0x00100BAC File Offset: 0x000FEDAC
		protected virtual async Task ParseFDODSCAsync(DdmReader reader, List<DrdaParameterInfo> parms, bool isAsync, CancellationToken cancellationToken)
		{
			this.drdaTypeMappings.Clear();
			while (reader.DdmObjectLength > 6L)
			{
				int num = await reader.ReadUnsignedByteAsync(isAsync, cancellationToken);
				int dtaGrpLen = num;
				int numVarsInGrp = (dtaGrpLen - 3) / 3;
				int num2 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
				if (num2 == 120)
				{
					await reader.ReadInt16Async(isAsync, cancellationToken);
					int num3 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
					if (num3 == 1)
					{
						FdocscDrdaTypeMapping drdaTypeMapping = new FdocscDrdaTypeMapping();
						FdocscDrdaTypeMapping fdocscDrdaTypeMapping = drdaTypeMapping;
						fdocscDrdaTypeMapping.ReferencType = await reader.ReadByteAsync(isAsync, cancellationToken);
						fdocscDrdaTypeMapping = null;
						fdocscDrdaTypeMapping = drdaTypeMapping;
						fdocscDrdaTypeMapping.FromDrdaType = await reader.ReadByteAsync(isAsync, cancellationToken);
						fdocscDrdaTypeMapping = null;
						dtaGrpLen = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						num2 = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						if (num2 == 112)
						{
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.TripletLID = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.ToDrdaType = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							await reader.ReadInt16Async(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.CCSID = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							if (this.CcsidConvert != null)
							{
								drdaTypeMapping.CCSID = (short)this.CcsidConvert((ushort)drdaTypeMapping.CCSID);
							}
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.CharSize = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.Mode = await reader.ReadByteAsync(isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							fdocscDrdaTypeMapping = drdaTypeMapping;
							fdocscDrdaTypeMapping.Precision = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
							fdocscDrdaTypeMapping = null;
							this.drdaTypeMappings.Add(drdaTypeMapping);
						}
						else
						{
							if (Logger.maxTracingLevel >= 2)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, "MDD triplet does not have mapping SDA triplet type", Array.Empty<object>());
							}
							await reader.SkipBytesAsync(dtaGrpLen - 2, isAsync, cancellationToken);
							drdaTypeMapping = null;
						}
					}
					else if (num3 == 2)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						await reader.ReadByteAsync(isAsync, cancellationToken);
					}
					else if (num3 == 3)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						await reader.ReadByteAsync(isAsync, cancellationToken);
					}
				}
				else if (num2 == 118 || num2 == 127)
				{
					await reader.ReadByteAsync(isAsync, cancellationToken);
					for (int i = 0; i < numVarsInGrp; i = num + 1)
					{
						byte type = await reader.ReadByteAsync(isAsync, cancellationToken);
						parms.Add(new DrdaParameterInfo(type, await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken)));
						num = i;
					}
				}
				else if (num2 == 113)
				{
					this.IsInsertMultipleRows = true;
					await reader.SkipBytesAsync(dtaGrpLen - 2, isAsync, cancellationToken);
				}
				else
				{
					if (Logger.maxTracingLevel >= 2)
					{
						Logger.Warning(this._tracePoint, base.DatabaseSessionId, "FDOCDSC contains recognized triplet type: " + num2.ToString(), Array.Empty<object>());
					}
					await reader.SkipBytesAsync(dtaGrpLen - 2, isAsync, cancellationToken);
				}
			}
			await reader.ReadBytesAsync(6, isAsync, cancellationToken);
		}

		// Token: 0x060047B1 RID: 18353 RVA: 0x00100C14 File Offset: 0x000FEE14
		protected async Task ReadAndSetParamsAsync(DdmReader reader, DrdaParameterInfo parm, bool isAsync, CancellationToken cancellationToken)
		{
			int drdaType = (int)parm.Type;
			ushort paramLenNumBytes = parm.Length;
			int codePage = this._ccsid._ccsidsbc;
			if (parm.MDDoverride)
			{
				codePage = (int)parm.CCSID;
			}
			else if (this._database != null && this._database.HostCodePageOverride != -1)
			{
				codePage = this._database.HostCodePageOverride;
			}
			EndianType endianType = reader.EndianType;
			if (this._database != null)
			{
				endianType = this._database.ByteOrder;
			}
			int num = drdaType;
			DrdaParameterInfo drdaParameterInfo;
			switch (num)
			{
			case 2:
			case 3:
				drdaParameterInfo = parm;
				num = await reader.ReadInt32Async(endianType, isAsync, cancellationToken);
				drdaParameterInfo.Value = num;
				drdaParameterInfo = null;
				return;
			case 4:
			case 5:
			{
				drdaParameterInfo = parm;
				short num2 = await reader.ReadInt16Async(endianType, isAsync, cancellationToken);
				drdaParameterInfo.Value = num2;
				drdaParameterInfo = null;
				return;
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
			case 30:
			case 31:
			case 44:
			case 45:
			case 46:
			case 47:
				goto IL_1664;
			case 10:
			case 11:
				drdaParameterInfo = parm;
				drdaParameterInfo.Value = await reader.ReadDoubleAsync((int)paramLenNumBytes, this.TypeDefName, isAsync, cancellationToken);
				drdaParameterInfo = null;
				return;
			case 12:
			case 13:
				drdaParameterInfo = parm;
				drdaParameterInfo.Value = await reader.ReadFloatAsync((int)paramLenNumBytes, this.TypeDefName, isAsync, cancellationToken);
				drdaParameterInfo = null;
				return;
			case 14:
			case 15:
			{
				ushort precision = (ushort)((paramLenNumBytes >> 8) & 255);
				ushort scale = paramLenNumBytes & 255;
				ushort length = precision / 2 + 1;
				drdaParameterInfo = parm;
				drdaParameterInfo.Value = await reader.ReadDecimalAsync((int)length, (int)precision, (int)scale, isAsync, cancellationToken);
				drdaParameterInfo = null;
				parm.Precision = precision;
				parm.Scale = scale;
				parm.Length = length;
				return;
			}
			case 16:
			case 17:
			{
				ushort length = (ushort)((paramLenNumBytes >> 8) & 255);
				ushort scale = paramLenNumBytes & 255;
				ushort precision = length / 2 + 1;
				if (precision < length)
				{
					precision = length;
				}
				drdaParameterInfo = parm;
				drdaParameterInfo.Value = await reader.ReadZonedDecimalAsync((int)precision, (int)length, (int)scale, isAsync, cancellationToken);
				drdaParameterInfo = null;
				parm.Precision = length;
				parm.Scale = scale;
				parm.Length = precision;
				return;
			}
			case 22:
			case 23:
				drdaParameterInfo = parm;
				drdaParameterInfo.Value = await reader.ReadInt64Async(endianType, isAsync, cancellationToken);
				drdaParameterInfo = null;
				return;
			case 32:
			case 33:
				try
				{
					bool success = false;
					drdaParameterInfo = parm;
					drdaParameterInfo.OriginalDateTimeString = await reader.ReadStringAsync(10, codePage, isAsync, cancellationToken);
					drdaParameterInfo = null;
					parm.Value = reader.Converter.UnpackDate(parm.OriginalDateTimeString, ref success);
					if (!success)
					{
						throw new InvalidDataException();
					}
					return;
				}
				catch (Exception)
				{
					throw new SqlExceptionInfo(new Exception("The syntax of the string representation of a datetime value is incorrect"), "-180", "22007", "The syntax of the string representation of a datetime value is incorrect");
				}
				break;
			case 34:
			case 35:
				break;
			case 36:
			case 37:
				goto IL_0AAC;
			case 38:
			case 39:
				goto IL_113C;
			case 40:
			case 41:
			case 42:
			case 43:
				goto IL_11D9;
			case 48:
			case 49:
			case 60:
			case 61:
			{
				string text = await reader.ReadStringAsync((int)paramLenNumBytes, codePage, drdaType, isAsync, cancellationToken);
				if (!string.IsNullOrEmpty(text))
				{
					text = text.TrimEnd(new char[1]);
					string empty = string.Empty;
					if (reader.Converter.TryConvertToDateTime(text, text.Length, out empty))
					{
						parm.Value = empty;
						return;
					}
				}
				parm.Value = text;
				return;
			}
			case 50:
			case 51:
			case 52:
			case 53:
			{
				string strVal = string.Empty;
				short num2 = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				int length2 = (int)num2;
				if (length2 != 0)
				{
					strVal = await reader.ReadStringAsync(length2, codePage, drdaType, isAsync, cancellationToken);
					string empty2 = string.Empty;
					if (reader.Converter.TryConvertToDateTime(strVal, length2, out empty2))
					{
						parm.Value = empty2;
						return;
					}
				}
				parm.Value = strVal;
				return;
			}
			case 54:
			case 55:
				goto IL_0BB7;
			case 56:
			case 57:
			case 58:
			case 59:
			{
				string strVal = string.Empty;
				int num3 = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				if (num3 != 0)
				{
					strVal = await reader.ReadDSStringAsync(num3 * 2, this._ccsid._ccsiddbc, drdaType, EndianType.LittleEndian, isAsync, cancellationToken);
				}
				parm.Value = strVal;
				return;
			}
			case 62:
			case 63:
			case 64:
			case 65:
			{
				string strVal = string.Empty;
				short num2 = await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				int length2 = (int)num2;
				if (length2 != 0)
				{
					strVal = await reader.ReadStringAsync(length2, codePage, drdaType, isAsync, cancellationToken);
					string empty3 = string.Empty;
					if (reader.Converter.TryConvertToDateTime(strVal, length2, out empty3))
					{
						parm.Value = empty3;
						return;
					}
				}
				parm.Value = strVal;
				return;
			}
			default:
				switch (num)
				{
				case 186:
				case 187:
					drdaParameterInfo = parm;
					drdaParameterInfo.Value = await reader.ReadDecimalFloatAsync((int)paramLenNumBytes, isAsync, cancellationToken);
					drdaParameterInfo = null;
					return;
				case 188:
				case 189:
				case 190:
				case 191:
					goto IL_1664;
				case 192:
				case 193:
					goto IL_113C;
				case 194:
				case 195:
					goto IL_11D9;
				case 196:
				case 197:
				case 202:
				case 203:
				case 204:
				case 205:
				case 206:
				case 207:
					drdaParameterInfo = parm;
					num = await reader.ReadInt32Async(EndianType.LittleEndian, isAsync, cancellationToken);
					drdaParameterInfo.LobPosition = num;
					drdaParameterInfo = null;
					await reader.ReadByteAsync(isAsync, cancellationToken);
					drdaParameterInfo = parm;
					num = await this.readLobLengthAsync(reader, (int)paramLenNumBytes, isAsync, cancellationToken);
					drdaParameterInfo.LobLength = num;
					drdaParameterInfo = null;
					parm.Length = (ushort)parm.LobLength;
					parm.IsClob = true;
					return;
				case 198:
				case 199:
				case 200:
				case 201:
					drdaParameterInfo = parm;
					num = await reader.ReadInt32Async(EndianType.LittleEndian, isAsync, cancellationToken);
					drdaParameterInfo.LobPosition = num;
					drdaParameterInfo = null;
					await reader.ReadByteAsync(isAsync, cancellationToken);
					drdaParameterInfo = parm;
					num = await this.readLobLengthAsync(reader, (int)paramLenNumBytes, isAsync, cancellationToken);
					drdaParameterInfo.LobLength = num;
					drdaParameterInfo = null;
					parm.Length = (ushort)parm.LobLength;
					parm.IsClob = false;
					return;
				default:
					goto IL_1664;
				}
				break;
			}
			try
			{
				bool success = false;
				drdaParameterInfo = parm;
				drdaParameterInfo.OriginalDateTimeString = await reader.ReadStringAsync(8, codePage, isAsync, cancellationToken);
				drdaParameterInfo = null;
				parm.Value = reader.Converter.UnpackTime(parm.OriginalDateTimeString, ref success);
				if (!success)
				{
					throw new InvalidDataException();
				}
				return;
			}
			catch (Exception)
			{
				throw new SqlExceptionInfo(new Exception("The syntax of the string representation of a datetime value is incorrect"), "-180", "22007", "The syntax of the string representation of a datetime value is incorrect");
			}
			IL_0AAC:
			try
			{
				bool success = false;
				drdaParameterInfo = parm;
				drdaParameterInfo.OriginalDateTimeString = await reader.ReadStringAsync(26, codePage, isAsync, cancellationToken);
				drdaParameterInfo = null;
				parm.Value = reader.Converter.UnpackTimestamp(parm.OriginalDateTimeString, ref success);
				if (!success)
				{
					throw new InvalidDataException();
				}
				return;
			}
			catch (Exception)
			{
				throw new SqlExceptionInfo(new Exception("The syntax of the string representation of a datetime value is incorrect"), "-180", "22007", "The syntax of the string representation of a datetime value is incorrect");
			}
			IL_0BB7:
			parm.Value = await reader.ReadDSStringAsync((int)(paramLenNumBytes * 2), this._ccsid._ccsiddbc, drdaType, EndianType.LittleEndian, isAsync, cancellationToken);
			return;
			IL_113C:
			drdaParameterInfo = parm;
			drdaParameterInfo.Value = await reader.ReadBytesAsync((int)paramLenNumBytes, isAsync, cancellationToken);
			drdaParameterInfo = null;
			return;
			IL_11D9:
			int num4 = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			drdaParameterInfo = parm;
			drdaParameterInfo.Value = await reader.ReadBytesAsync(num4, isAsync, cancellationToken);
			drdaParameterInfo = null;
			return;
			IL_1664:
			throw new NotSupportedException("Type not supported when reading Parameters: " + drdaType.ToString());
		}

		// Token: 0x060047B2 RID: 18354 RVA: 0x00100C7C File Offset: 0x000FEE7C
		private async Task<int> readLobLengthAsync(DdmReader reader, int extLenIndicator, bool isAsync, CancellationToken cancellationToken)
		{
			int num = (int)((ushort)extLenIndicator & ushort.MaxValue);
			int num2;
			if (num != 32770)
			{
				if (num != 32772 && num != 32777)
				{
					DrdaException.ValueNotSupported(CodePoint.SQLDTA);
					num2 = 0;
				}
				else
				{
					num2 = await reader.ReadInt32Async(EndianType.BigEndian, isAsync, cancellationToken);
				}
			}
			else
			{
				num2 = (int)(await reader.ReadUInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
			}
			return num2;
		}

		// Token: 0x060047B3 RID: 18355 RVA: 0x00100CDC File Offset: 0x000FEEDC
		private void WriteFDOEXT(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.FDOEXT);
			int num = this.Rows.Count<object[]>();
			int num2 = this.Rows.First<object[]>().Length;
			for (int i = 0; i < num2; i++)
			{
				writer.WriteInt32(num);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060047B4 RID: 18356 RVA: 0x00100D28 File Offset: 0x000FEF28
		private void WriteFDOOFF(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.FDOOFF);
			this.sqldtaOffsets.ForEach(delegate(int offset)
			{
				writer.WriteInt32(offset);
			});
			writer.WriteEndDdm();
		}

		// Token: 0x17001144 RID: 4420
		// (get) Token: 0x060047B5 RID: 18357 RVA: 0x00100D74 File Offset: 0x000FEF74
		// (set) Token: 0x060047B6 RID: 18358 RVA: 0x00100D7C File Offset: 0x000FEF7C
		public int Nbrrow { get; set; }

		// Token: 0x040033DB RID: 13275
		private int _currentExtdtaIndex;

		// Token: 0x040033DC RID: 13276
		protected List<DrdaParameterInfo> parms = new List<DrdaParameterInfo>();

		// Token: 0x040033DD RID: 13277
		protected List<FdocscDrdaTypeMapping> drdaTypeMappings = new List<FdocscDrdaTypeMapping>();

		// Token: 0x040033DE RID: 13278
		protected Ccsid _ccsid;

		// Token: 0x040033DF RID: 13279
		protected List<int> arraySizes = new List<int>();

		// Token: 0x040033E0 RID: 13280
		protected List<int> sqldtaOffsets = new List<int>();

		// Token: 0x040033E1 RID: 13281
		protected List<List<DrdaParameterInfo>> parmsList = new List<List<DrdaParameterInfo>>();

		// Token: 0x020008CF RID: 2255
		public enum BatchMode : short
		{
			// Token: 0x040033E9 RID: 13289
			RowWide,
			// Token: 0x040033EA RID: 13290
			ColumnWide
		}
	}
}

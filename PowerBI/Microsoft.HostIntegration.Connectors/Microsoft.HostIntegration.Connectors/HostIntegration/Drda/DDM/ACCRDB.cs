using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000871 RID: 2161
	public class ACCRDB : AbstractDdmObject
	{
		// Token: 0x06004489 RID: 17545 RVA: 0x000E6DB6 File Offset: 0x000E4FB6
		public ACCRDB(IDatabase database, int level)
			: base(database, level)
		{
		}

		// Token: 0x0600448A RID: 17546 RVA: 0x000E6DE4 File Offset: 0x000E4FE4
		public ACCRDB(IDatabase database, int level, bool isIMSDB)
			: base(database, level)
		{
			this._isIMSDB = isIMSDB;
		}

		// Token: 0x0600448B RID: 17547 RVA: 0x000E6E19 File Offset: 0x000E5019
		public override string ToString()
		{
			return base.FixParenthis(string.Format("ACCRDB[rdbnam={0};typdefovr={1};typdefnam={1};prdid={2}]", new object[] { this._rdbnam, this._typedefovr, this._typdefnam, this._prdid }));
		}

		// Token: 0x17001035 RID: 4149
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x000E6E55 File Offset: 0x000E5055
		// (set) Token: 0x0600448D RID: 17549 RVA: 0x000E6E5D File Offset: 0x000E505D
		public TYPDEFOVR Typedefovr
		{
			get
			{
				return this._typedefovr;
			}
			set
			{
				this._typedefovr = value;
			}
		}

		// Token: 0x17001036 RID: 4150
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x000E6E66 File Offset: 0x000E5066
		// (set) Token: 0x0600448F RID: 17551 RVA: 0x000E6E6E File Offset: 0x000E506E
		public string Typdefnam
		{
			get
			{
				return this._typdefnam;
			}
			set
			{
				this._typdefnam = value;
			}
		}

		// Token: 0x17001037 RID: 4151
		// (get) Token: 0x06004490 RID: 17552 RVA: 0x000E6E77 File Offset: 0x000E5077
		// (set) Token: 0x06004491 RID: 17553 RVA: 0x000E6E7F File Offset: 0x000E507F
		public bool Trgdftrt
		{
			get
			{
				return this._trgdftrt;
			}
			set
			{
				this._trgdftrt = value;
			}
		}

		// Token: 0x17001038 RID: 4152
		// (get) Token: 0x06004492 RID: 17554 RVA: 0x000E6E88 File Offset: 0x000E5088
		// (set) Token: 0x06004493 RID: 17555 RVA: 0x000E6E90 File Offset: 0x000E5090
		public CodePoint Sttdecdel
		{
			get
			{
				return this._sttdecdel;
			}
			set
			{
				this._sttdecdel = value;
			}
		}

		// Token: 0x17001039 RID: 4153
		// (get) Token: 0x06004494 RID: 17556 RVA: 0x000E6E99 File Offset: 0x000E5099
		// (set) Token: 0x06004495 RID: 17557 RVA: 0x000E6EA1 File Offset: 0x000E50A1
		public CodePoint Sttstrdel
		{
			get
			{
				return this._sttstrdel;
			}
			set
			{
				this._sttstrdel = value;
			}
		}

		// Token: 0x1700103A RID: 4154
		// (get) Token: 0x06004496 RID: 17558 RVA: 0x000E6EAA File Offset: 0x000E50AA
		// (set) Token: 0x06004497 RID: 17559 RVA: 0x000E6EB2 File Offset: 0x000E50B2
		public byte[] Crrtkn
		{
			get
			{
				return this._crrtkn;
			}
			set
			{
				this._crrtkn = value;
			}
		}

		// Token: 0x1700103B RID: 4155
		// (get) Token: 0x06004498 RID: 17560 RVA: 0x000E6EBB File Offset: 0x000E50BB
		// (set) Token: 0x06004499 RID: 17561 RVA: 0x000E6EC3 File Offset: 0x000E50C3
		public bool Rdbalwupd
		{
			get
			{
				return this._rdbalwupd;
			}
			set
			{
				this._rdbalwupd = value;
			}
		}

		// Token: 0x1700103C RID: 4156
		// (get) Token: 0x0600449A RID: 17562 RVA: 0x000E6ECC File Offset: 0x000E50CC
		// (set) Token: 0x0600449B RID: 17563 RVA: 0x000E6ED4 File Offset: 0x000E50D4
		public RDBNAM RDBNAM
		{
			get
			{
				return this._rdbnam;
			}
			set
			{
				this._rdbnam = value;
			}
		}

		// Token: 0x1700103D RID: 4157
		// (get) Token: 0x0600449C RID: 17564 RVA: 0x000E6EDD File Offset: 0x000E50DD
		// (set) Token: 0x0600449D RID: 17565 RVA: 0x000E6EE5 File Offset: 0x000E50E5
		public string Prdid
		{
			get
			{
				return this._prdid;
			}
			set
			{
				this._prdid = value;
			}
		}

		// Token: 0x0600449E RID: 17566 RVA: 0x000E6EF0 File Offset: 0x000E50F0
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.ACCRDB);
			if (this._isIMSDB)
			{
				writer.WriteScalar(CodePoint.RDBNAM, this._rdbnam.Name);
			}
			else
			{
				writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbnam.Name, 18);
			}
			writer.WriteScalar2Bytes(CodePoint.RDBACCCL, 9223);
			writer.WriteScalar(CodePoint.PRDID, this._prdid);
			writer.WriteScalar(CodePoint.TYPDEFNAM, this._typdefnam);
			if (!this._isIMSDB)
			{
				writer.WriteScalar1Byte(CodePoint.RDBALWUPD, this._rdbalwupd ? 241 : 240);
				writer.WriteScalar2Bytes(CodePoint.STTDECDEL, 9246);
				writer.WriteScalar2Bytes(CodePoint.STTSTRDEL, 9246);
				writer.WriteScalar1Byte(CodePoint.TRGDFTRT, this._trgdftrt ? 241 : 240);
			}
			if (this._typedefovr != null)
			{
				this._typedefovr.Write(writer);
			}
			if (this._crrtkn != null)
			{
				writer.WriteScalarBytes(CodePoint.CRRTKN, this._crrtkn);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600449F RID: 17567 RVA: 0x000E7008 File Offset: 0x000E5208
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(5);
			requiredCodePoints.Add(CodePoint.PRDID);
			requiredCodePoints.Add(CodePoint.RDBNAM);
			requiredCodePoints.Add(CodePoint.TYPDEFNAM);
			requiredCodePoints.Add(CodePoint.TYPDEFOVR);
			IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
			IEnumerator<ObjectInfo> enumerator = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
			while (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext())
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
				base.LogCodePoint(codepoint);
				if (codepoint <= CodePoint.RDBNAM)
				{
					if (codepoint <= (CodePoint)1313)
					{
						if (codepoint == CodePoint.TYPDEFNAM)
						{
							this._typdefnam = await reader.ReadStringAsync(isAsync, cancellationToken);
							if (this._typdefnam.Length > 255)
							{
								DrdaException.TooBig(CodePoint.TYPDEFNAM);
							}
							requiredCodePoints.Remove(CodePoint.TYPDEFNAM);
							continue;
						}
						if (codepoint == CodePoint.TYPDEFOVR)
						{
							this._typedefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
							await this._typedefovr.ReadAsync(reader, isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.TYPDEFOVR);
							continue;
						}
						if (codepoint == (CodePoint)1313)
						{
							await reader.ReadByteAsync(isAsync, cancellationToken);
							continue;
						}
					}
					else if (codepoint <= CodePoint.PRDDTA)
					{
						if (codepoint == CodePoint.PRDID)
						{
							this._prdid = await reader.ReadStringAsync(isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.PRDID);
							continue;
						}
						if (codepoint == CodePoint.PRDDTA)
						{
							if (reader.DdmObjectLength > 255L)
							{
								DrdaException.TooBig(CodePoint.PRDDTA);
							}
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
							continue;
						}
					}
					else if (codepoint != CodePoint.RDBACCCL)
					{
						if (codepoint == CodePoint.RDBNAM)
						{
							this._rdbnam = new RDBNAM();
							await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.RDBNAM);
							continue;
						}
					}
					else
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.RDBACCCL, 2);
						TaskAwaiter<short> taskAwaiter = reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken).GetAwaiter();
						if (!taskAwaiter.IsCompleted)
						{
							await taskAwaiter;
							TaskAwaiter<short> taskAwaiter2;
							taskAwaiter = taskAwaiter2;
							taskAwaiter2 = default(TaskAwaiter<short>);
						}
						if (taskAwaiter.GetResult() != 9223)
						{
							DrdaException.InvalidValue(CodePoint.RDBACCCL);
							continue;
						}
						continue;
					}
				}
				else if (codepoint <= CodePoint.STTDECDEL)
				{
					if (codepoint == CodePoint.RDBALWUPD)
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.RDBALWUPD, 1);
						this._rdbalwupd = await reader.ReadBooleanAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.STTSTRDEL)
					{
						this._sttstrdel = (CodePoint)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						continue;
					}
					if (codepoint == CodePoint.STTDECDEL)
					{
						this._sttdecdel = (CodePoint)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						continue;
					}
				}
				else if (codepoint <= CodePoint.TRGDFTRT)
				{
					if (codepoint != CodePoint.CRRTKN)
					{
						if (codepoint == CodePoint.TRGDFTRT)
						{
							this._trgdftrt = await reader.ReadBooleanAsync(isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						this._crrtkn = await reader.ReadBytesAsync(isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.CRRTKN);
						if (this._crrtkn.Length > 255)
						{
							DrdaException.TooBig(CodePoint.CRRTKN);
							continue;
						}
						continue;
					}
				}
				else
				{
					if (codepoint == (CodePoint)8544)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == (CodePoint)8549)
					{
						await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "ACCRDB::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x04003033 RID: 12339
		protected string _prdid;

		// Token: 0x04003034 RID: 12340
		private RDBNAM _rdbnam;

		// Token: 0x04003035 RID: 12341
		private bool _rdbalwupd = true;

		// Token: 0x04003036 RID: 12342
		private byte[] _crrtkn;

		// Token: 0x04003037 RID: 12343
		private CodePoint _sttdecdel = CodePoint.DECDELPRD;

		// Token: 0x04003038 RID: 12344
		private CodePoint _sttstrdel = CodePoint.STRDELAP;

		// Token: 0x04003039 RID: 12345
		private bool _trgdftrt = true;

		// Token: 0x0400303A RID: 12346
		private bool _isIMSDB;

		// Token: 0x0400303B RID: 12347
		protected string _typdefnam;

		// Token: 0x0400303C RID: 12348
		protected TYPDEFOVR _typedefovr;
	}
}

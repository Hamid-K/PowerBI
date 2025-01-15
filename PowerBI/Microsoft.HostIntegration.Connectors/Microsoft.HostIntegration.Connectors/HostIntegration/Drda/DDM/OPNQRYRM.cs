using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200089B RID: 2203
	public class OPNQRYRM : AbstractDdmObject
	{
		// Token: 0x0600462A RID: 17962 RVA: 0x000F487E File Offset: 0x000F2A7E
		public OPNQRYRM(int requesterId, IStatement statement, int sqlamLevel)
		{
			this.statement = statement;
			this.sqlamLevel = sqlamLevel;
			this._requesterId = requesterId;
		}

		// Token: 0x0600462B RID: 17963 RVA: 0x000F489B File Offset: 0x000F2A9B
		public OPNQRYRM(IStatement statement, int sqlamLevel)
		{
			this.statement = statement;
			this.sqlamLevel = sqlamLevel;
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x0600462C RID: 17964 RVA: 0x000F48B1 File Offset: 0x000F2AB1
		// (set) Token: 0x0600462D RID: 17965 RVA: 0x000F48B9 File Offset: 0x000F2AB9
		public SeverityCode SeverityCode
		{
			get
			{
				return this._severityCode;
			}
			set
			{
				this._severityCode = value;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x0600462E RID: 17966 RVA: 0x000F48C2 File Offset: 0x000F2AC2
		// (set) Token: 0x0600462F RID: 17967 RVA: 0x000F48CA File Offset: 0x000F2ACA
		public bool Dupqryok
		{
			get
			{
				return this._dupqryok;
			}
			set
			{
				this._dupqryok = value;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06004630 RID: 17968 RVA: 0x000F48D3 File Offset: 0x000F2AD3
		// (set) Token: 0x06004631 RID: 17969 RVA: 0x000F48DB File Offset: 0x000F2ADB
		public string Pkgid
		{
			get
			{
				return this._pkgid;
			}
			set
			{
				this._pkgid = value;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06004632 RID: 17970 RVA: 0x000F48E4 File Offset: 0x000F2AE4
		// (set) Token: 0x06004633 RID: 17971 RVA: 0x000F48EC File Offset: 0x000F2AEC
		public long Qryinsid { get; private set; }

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x06004634 RID: 17972 RVA: 0x000F48F5 File Offset: 0x000F2AF5
		// (set) Token: 0x06004635 RID: 17973 RVA: 0x000F48FD File Offset: 0x000F2AFD
		public bool Qryattscr { get; private set; }

		// Token: 0x06004636 RID: 17974 RVA: 0x000F4908 File Offset: 0x000F2B08
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.OPNQRYRM);
			writer.WriteScalar2Bytes(CodePoint.SVRCOD, (int)this._severityCode);
			writer.WriteScalar2Bytes(CodePoint.QRYPRCTYP, 9239);
			bool flag = this._pkgid.Contains("SYSSN");
			bool flag2 = this._pkgid.Contains("SYSLN");
			if (flag || flag2 || this._dupqryok)
			{
				writer.WriteScalar1Byte(CodePoint.SQLCSRHLD, -16);
			}
			else
			{
				writer.WriteScalar1Byte(CodePoint.SQLCSRHLD, -15);
			}
			if (this.statement.HasCursor)
			{
				writer.WriteScalar1Byte(CodePoint.QRYATTSCR, -15);
				writer.WriteScalar1Byte(CodePoint.QRYATTSNS, 1);
				writer.WriteScalar1Byte(CodePoint.QRYATTSET, -15);
			}
			writer.WriteScalar1Byte(CodePoint.QRYATTUPD, 1);
			writer.WriteScalarHeader(CodePoint.QRYINSID, 8);
			writer.WriteInt32(0);
			writer.WriteInt32(0);
			writer.WriteScalar1Byte(CodePoint.DYNDTAFMT, 241);
			writer.WriteEndDdm();
		}

		// Token: 0x06004637 RID: 17975 RVA: 0x000F49FC File Offset: 0x000F2BFC
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(5);
			requiredCodePoints.Add(CodePoint.SVRCOD);
			long parentDdmObjectLength = reader.DdmObjectLength;
			long childDdmObjectLengthSum = 0L;
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
				if (codepoint <= CodePoint.QRYPRCTYP)
				{
					if (codepoint == CodePoint.SVRCOD)
					{
						this._svrcod = await reader.ReadInt16Async(isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.SVRCOD);
						continue;
					}
					if (codepoint == CodePoint.QRYPRCTYP)
					{
						this._qryprctyp = await reader.ReadInt16Async(isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.PRDID);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.SQLCSRHLD)
					{
						this._sqlcsrhld = await reader.ReadByteAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.QRYATTSCR)
					{
						this.Qryattscr = await reader.ReadByteAsync(isAsync, cancellationToken) == 241;
						continue;
					}
					if (codepoint == CodePoint.QRYINSID)
					{
						this.Qryinsid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, this._requesterId, 4, "OPNQRYRM::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x040031FF RID: 12799
		private SeverityCode _severityCode;

		// Token: 0x04003200 RID: 12800
		private IStatement statement;

		// Token: 0x04003201 RID: 12801
		private int sqlamLevel;

		// Token: 0x04003202 RID: 12802
		private short _svrcod;

		// Token: 0x04003203 RID: 12803
		private short _qryprctyp;

		// Token: 0x04003204 RID: 12804
		private byte _sqlcsrhld;

		// Token: 0x04003205 RID: 12805
		private int _requesterId;

		// Token: 0x04003206 RID: 12806
		private bool _dupqryok;

		// Token: 0x04003207 RID: 12807
		private string _pkgid;
	}
}

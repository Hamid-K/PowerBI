using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200087F RID: 2175
	public class CLSQRY : AbstractDdmObject
	{
		// Token: 0x06004514 RID: 17684 RVA: 0x000EC822 File Offset: 0x000EAA22
		public CLSQRY(IDatabase database, int level, string accrdbTypedefname)
			: base(database, level, accrdbTypedefname)
		{
		}

		// Token: 0x06004515 RID: 17685 RVA: 0x000EC830 File Offset: 0x000EAA30
		public override string ToString()
		{
			return base.FixParenthis(string.Format("CLSQRY[rdbnam={0};pkgnamcsn={1};qryinsid={2};cmdsrcid={3}]", new object[] { this._rdbnam, this._pkgnamcsn, this._qryinsid, this._cmdsrcid }));
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x000EC881 File Offset: 0x000EAA81
		public override void Reset()
		{
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._pkgnamcsn != null)
			{
				this._pkgnamcsn.Reset();
			}
			this._qryinsid = 0L;
		}

		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06004517 RID: 17687 RVA: 0x000EC8B1 File Offset: 0x000EAAB1
		// (set) Token: 0x06004518 RID: 17688 RVA: 0x000EC8B9 File Offset: 0x000EAAB9
		public long Qryinsid
		{
			get
			{
				return this._qryinsid;
			}
			set
			{
				this._qryinsid = value;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06004519 RID: 17689 RVA: 0x000EC8C2 File Offset: 0x000EAAC2
		// (set) Token: 0x0600451A RID: 17690 RVA: 0x000EC8CA File Offset: 0x000EAACA
		public RDBNAM Rdbnam
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

		// Token: 0x1700106A RID: 4202
		// (get) Token: 0x0600451B RID: 17691 RVA: 0x000EC8D3 File Offset: 0x000EAAD3
		// (set) Token: 0x0600451C RID: 17692 RVA: 0x000EC8DB File Offset: 0x000EAADB
		public PKGNAMCSN Pkgnamcsn
		{
			get
			{
				return this._pkgnamcsn;
			}
			set
			{
				this._pkgnamcsn = value;
			}
		}

		// Token: 0x0600451D RID: 17693 RVA: 0x000EC8E4 File Offset: 0x000EAAE4
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAMCSN);
			if (this._sqlamLevel >= 7)
			{
				requiredCodePoints.Add(CodePoint.QRYINSID);
			}
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
				if (codepoint <= CodePoint.CMDSRCID)
				{
					if (codepoint == CodePoint.MONITOR)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.CMDSRCID)
					{
						this._cmdsrcid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.RDBNAM)
					{
						this._rdbnam = new RDBNAM();
						await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.PKGNAMCSN)
					{
						this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
						await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.PKGNAMCSN);
						continue;
					}
					if (codepoint == CodePoint.QRYINSID)
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.QRYINSID, 8);
						this._qryinsid = await reader.ReadInt64Async(EndianType.BigEndian, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.QRYINSID);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "CLSQRY::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x0600451E RID: 17694 RVA: 0x000EC944 File Offset: 0x000EAB44
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.CLSQRY);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteBeginDdm(CodePoint.QRYINSID);
			writer.WriteInt64(this._qryinsid, EndianType.BigEndian);
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x040030ED RID: 12525
		private RDBNAM _rdbnam;

		// Token: 0x040030EE RID: 12526
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x040030EF RID: 12527
		private long _qryinsid;
	}
}

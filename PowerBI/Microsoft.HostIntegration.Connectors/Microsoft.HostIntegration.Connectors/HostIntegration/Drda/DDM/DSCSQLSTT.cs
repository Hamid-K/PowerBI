using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000885 RID: 2181
	public class DSCSQLSTT : AbstractDdmObject
	{
		// Token: 0x06004552 RID: 17746 RVA: 0x000EE612 File Offset: 0x000EC812
		public DSCSQLSTT(IDatabase database, int sqlamlevel, string accrdbTypedefname)
			: base(database, sqlamlevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004553 RID: 17747 RVA: 0x000EE62C File Offset: 0x000EC82C
		public override string ToString()
		{
			return base.FixParenthis(string.Format("DSCSQLSTT[rdbnam={0};pkgnamcsn={1};rtnoutput={2};typesqlda={3};cmdsrcid={4}]", new object[] { this._rdbnam, this._pkgnamcsn, this._rtnOutput, this._typsqlda, this._cmdsrcid }));
		}

		// Token: 0x06004554 RID: 17748 RVA: 0x000EE68B File Offset: 0x000EC88B
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
			this._typsqlda = TYPSQLDA.None;
			this._rtnOutput = true;
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x000EE6C2 File Offset: 0x000EC8C2
		// (set) Token: 0x06004556 RID: 17750 RVA: 0x000EE6CA File Offset: 0x000EC8CA
		public TYPSQLDA Typsqlda
		{
			get
			{
				return this._typsqlda;
			}
			set
			{
				this._typsqlda = value;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000EE6D3 File Offset: 0x000EC8D3
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x000EE6DB File Offset: 0x000EC8DB
		public bool RtnOutput
		{
			get
			{
				return this._rtnOutput;
			}
			set
			{
				this._rtnOutput = value;
			}
		}

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000EE6E4 File Offset: 0x000EC8E4
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x000EE6EC File Offset: 0x000EC8EC
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

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000EE6F5 File Offset: 0x000EC8F5
		// (set) Token: 0x0600455C RID: 17756 RVA: 0x000EE6FD File Offset: 0x000EC8FD
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

		// Token: 0x0600455D RID: 17757 RVA: 0x000EE708 File Offset: 0x000EC908
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.DSCSQLSTT);
			base.WriteCommandSourceId(writer);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this.Pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteScalar1Byte(CodePoint.TYPSQLDA, (int)this._typsqlda);
			writer.WriteEndDdm();
		}

		// Token: 0x0600455E RID: 17758 RVA: 0x000EE75C File Offset: 0x000EC95C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
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
						continue;
					}
					if (codepoint == CodePoint.TYPSQLDA)
					{
						await this.ParseTypsqldaAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "DSCSQLSTT::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
		}

		// Token: 0x0600455F RID: 17759 RVA: 0x000EE7BC File Offset: 0x000EC9BC
		private async Task ParseTypsqldaAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			AbstractDdmObject.CheckLength(reader, CodePoint.TYPSQLDA, 1);
			byte b = await reader.ReadByteAsync(isAsync, cancellationToken);
			this._typsqlda = (TYPSQLDA)b;
			switch (this._typsqlda)
			{
			case TYPSQLDA.StandardOutput:
			case TYPSQLDA.LightOutput:
			case TYPSQLDA.ExtendedOutput:
				this._rtnOutput = true;
				break;
			case TYPSQLDA.StandardInput:
			case TYPSQLDA.LightInput:
			case TYPSQLDA.ExtendedInput:
				this._rtnOutput = false;
				break;
			default:
				DrdaException.InvalidValue(CodePoint.TYPSQLDA);
				break;
			}
		}

		// Token: 0x0400312B RID: 12587
		private RDBNAM _rdbnam;

		// Token: 0x0400312C RID: 12588
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x0400312D RID: 12589
		private bool _rtnOutput = true;

		// Token: 0x0400312E RID: 12590
		private TYPSQLDA _typsqlda = TYPSQLDA.None;
	}
}

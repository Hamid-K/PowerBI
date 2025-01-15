using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000888 RID: 2184
	public class ENDBND : AbstractDdmObject
	{
		// Token: 0x06004564 RID: 17764 RVA: 0x000EEE76 File Offset: 0x000ED076
		public ENDBND(IDatabase database, int sqlamlevel, string accrdbTypedefname)
			: base(database, sqlamlevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004565 RID: 17765 RVA: 0x000EEE8C File Offset: 0x000ED08C
		public override string ToString()
		{
			return base.FixParenthis(string.Format("ENDBND[rdbnam={0};pkgnamct={1}]", this._rdbnam, this._pkgnamct));
		}

		// Token: 0x06004566 RID: 17766 RVA: 0x000EEEAA File Offset: 0x000ED0AA
		public override void Reset()
		{
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._pkgnamct != null)
			{
				this._pkgnamct.Reset();
			}
			this._maxsctnbr = 255;
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x000EEEDD File Offset: 0x000ED0DD
		// (set) Token: 0x06004568 RID: 17768 RVA: 0x000EEEE5 File Offset: 0x000ED0E5
		public int Maxsctnbr
		{
			get
			{
				return this._maxsctnbr;
			}
			set
			{
				this._maxsctnbr = value;
			}
		}

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06004569 RID: 17769 RVA: 0x000EEEEE File Offset: 0x000ED0EE
		// (set) Token: 0x0600456A RID: 17770 RVA: 0x000EEEF6 File Offset: 0x000ED0F6
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

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x0600456B RID: 17771 RVA: 0x000EEEFF File Offset: 0x000ED0FF
		// (set) Token: 0x0600456C RID: 17772 RVA: 0x000EEF07 File Offset: 0x000ED107
		public PKGNAMCT Pkgnamct
		{
			get
			{
				return this._pkgnamct;
			}
			set
			{
				this._pkgnamct = value;
			}
		}

		// Token: 0x0600456D RID: 17773 RVA: 0x000EEF10 File Offset: 0x000ED110
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.ENDBND);
			writer.WriteBeginDdm(CodePoint.PKGNAMCT);
			this._pkgnamct.Write(writer);
			writer.WriteEndDdm();
			if (this._maxsctnbr > 0)
			{
				writer.WriteScalar2Bytes(CodePoint.MAXSCTNBR, this._maxsctnbr);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x0600456E RID: 17774 RVA: 0x000EEF68 File Offset: 0x000ED168
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(1);
			requiredCodePoints.Add(CodePoint.PKGNAMCT);
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
				CodePoint cp = objectInfo.Codepoint;
				base.LogCodePoint(cp);
				CodePoint codePoint = cp;
				if (codePoint != CodePoint.RDBNAM)
				{
					if (codePoint != CodePoint.PKGNAMCT)
					{
						if (codePoint != CodePoint.MAXSCTNBR)
						{
							if (Logger.maxTracingLevel >= 4)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "ENDBND::Read CodePoint not supported in " + this.ToString() + ": " + cp.ToString(), Array.Empty<object>());
							}
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						}
						else
						{
							this._maxsctnbr = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
						}
					}
					else
					{
						this._pkgnamct = new PKGNAMCT(this._database.PkgnamcsnCcsid);
						await this._pkgnamct.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(cp);
					}
				}
				else
				{
					this._rdbnam = new RDBNAM();
					await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
				}
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x04003141 RID: 12609
		private RDBNAM _rdbnam;

		// Token: 0x04003142 RID: 12610
		private PKGNAMCT _pkgnamct;

		// Token: 0x04003143 RID: 12611
		private int _maxsctnbr = 255;
	}
}

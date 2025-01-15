using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000883 RID: 2179
	public class DRPPKG : AbstractDdmObject
	{
		// Token: 0x06004548 RID: 17736 RVA: 0x000EE176 File Offset: 0x000EC376
		public override string ToString()
		{
			return base.FixParenthis(string.Format("DRPPKG[pkgnamct={0};vrsnam={1}]", this._pkgnam, this._vrsnam));
		}

		// Token: 0x06004549 RID: 17737 RVA: 0x000EE194 File Offset: 0x000EC394
		public DRPPKG(IDatabase database, int sqlamLevel)
			: base(database, sqlamLevel)
		{
		}

		// Token: 0x0600454A RID: 17738 RVA: 0x000EE19E File Offset: 0x000EC39E
		public override void Reset()
		{
			if (this._pkgnam != null)
			{
				this._pkgnam.Reset();
			}
			if (this._vrsnam != null)
			{
				this._vrsnam.Reset();
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x0600454B RID: 17739 RVA: 0x000EE1C6 File Offset: 0x000EC3C6
		public VRSNAM Vrsnam
		{
			get
			{
				return this._vrsnam;
			}
		}

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x0600454C RID: 17740 RVA: 0x000E9653 File Offset: 0x000E7853
		// (set) Token: 0x0600454D RID: 17741 RVA: 0x000E965B File Offset: 0x000E785B
		public IDatabase Database
		{
			get
			{
				return this._database;
			}
			set
			{
				this._database = value;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x0600454E RID: 17742 RVA: 0x000EE1CE File Offset: 0x000EC3CE
		public PKGNAM Pkgnam
		{
			get
			{
				return this._pkgnam;
			}
		}

		// Token: 0x0600454F RID: 17743 RVA: 0x000EE1D8 File Offset: 0x000EC3D8
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(2);
			requiredCodePoints.Add(CodePoint.PKGNAM);
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
				if (codepoint != CodePoint.VRSNAM)
				{
					if (codepoint == CodePoint.PKGNAM)
					{
						if (this._pkgnam == null)
						{
							this._pkgnam = new PKGNAM(this._database.PkgnamcsnCcsid);
						}
						await this._pkgnam.ReadAsync(reader, isAsync, cancellationToken);
						requiredCodePoints.Remove(CodePoint.PKGNAM);
					}
					else
					{
						if (Logger.maxTracingLevel >= 4)
						{
							Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "BGNBND::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
						}
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					}
				}
				else
				{
					if (this._vrsnam == null)
					{
						this._vrsnam = new VRSNAM();
					}
					await this._vrsnam.ReadAsync(reader, isAsync, cancellationToken);
				}
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x0400311E RID: 12574
		private PKGNAM _pkgnam;

		// Token: 0x0400311F RID: 12575
		private VRSNAM _vrsnam;
	}
}

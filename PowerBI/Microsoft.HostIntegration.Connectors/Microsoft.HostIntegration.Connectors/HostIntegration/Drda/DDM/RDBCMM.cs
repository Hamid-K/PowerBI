using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008A4 RID: 2212
	public class RDBCMM : AbstractDdmObject
	{
		// Token: 0x06004670 RID: 18032 RVA: 0x000EC822 File Offset: 0x000EAA22
		public RDBCMM(IDatabase database, int sqlamlevel, string accrdbTypedefname)
			: base(database, sqlamlevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004671 RID: 18033 RVA: 0x000F6602 File Offset: 0x000F4802
		public override string ToString()
		{
			return string.Format("RDBCMM[rdbnam={0}]", this._rdbnam);
		}

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06004672 RID: 18034 RVA: 0x000F6614 File Offset: 0x000F4814
		// (set) Token: 0x06004673 RID: 18035 RVA: 0x000F661C File Offset: 0x000F481C
		public string RDBNAM
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

		// Token: 0x06004674 RID: 18036 RVA: 0x000F6625 File Offset: 0x000F4825
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.RDBCMM);
			writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbnam, 18);
			writer.WriteEndDdm();
		}

		// Token: 0x06004675 RID: 18037 RVA: 0x000F664C File Offset: 0x000F484C
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
				RDBNAM rdbnam;
				if (codepoint == CodePoint.RDBNAM)
				{
					rdbnam = new RDBNAM();
					await rdbnam.ReadAsync(reader, isAsync, cancellationToken);
					this._rdbnam = rdbnam.Name;
				}
				else
				{
					if (Logger.maxTracingLevel >= 4)
					{
						Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "RDBCMM::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
					}
					await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				}
				rdbnam = null;
			}
		}

		// Token: 0x04003250 RID: 12880
		private string _rdbnam;
	}
}

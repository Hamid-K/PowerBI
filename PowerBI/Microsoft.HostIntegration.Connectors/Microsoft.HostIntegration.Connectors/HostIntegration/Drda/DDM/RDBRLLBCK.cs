using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008A6 RID: 2214
	public class RDBRLLBCK : AbstractDdmObject
	{
		// Token: 0x06004678 RID: 18040 RVA: 0x000EC822 File Offset: 0x000EAA22
		public RDBRLLBCK(IDatabase database, int sqlamlevel, string accrdbTypedefname)
			: base(database, sqlamlevel, accrdbTypedefname)
		{
		}

		// Token: 0x06004679 RID: 18041 RVA: 0x000F69A2 File Offset: 0x000F4BA2
		public override string ToString()
		{
			return string.Format("REBRLLBCK[rdbnam={0}]", this._rdbnam);
		}

		// Token: 0x170010E3 RID: 4323
		// (get) Token: 0x0600467A RID: 18042 RVA: 0x000F69B4 File Offset: 0x000F4BB4
		// (set) Token: 0x0600467B RID: 18043 RVA: 0x000F69BC File Offset: 0x000F4BBC
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

		// Token: 0x0600467C RID: 18044 RVA: 0x000F69C5 File Offset: 0x000F4BC5
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.RDBRLLBCK);
			writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbnam, 18);
			writer.WriteEndDdm();
		}

		// Token: 0x0600467D RID: 18045 RVA: 0x000F69EC File Offset: 0x000F4BEC
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
						Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "RDBRLLBCK::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
					}
					await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				}
				rdbnam = null;
			}
		}

		// Token: 0x0400325C RID: 12892
		private string _rdbnam;
	}
}

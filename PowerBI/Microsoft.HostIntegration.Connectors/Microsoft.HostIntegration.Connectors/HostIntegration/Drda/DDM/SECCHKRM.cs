using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008AA RID: 2218
	public class SECCHKRM : SECCHK
	{
		// Token: 0x170010EC RID: 4332
		// (get) Token: 0x06004698 RID: 18072 RVA: 0x000F7556 File Offset: 0x000F5756
		// (set) Token: 0x06004699 RID: 18073 RVA: 0x000F755E File Offset: 0x000F575E
		public byte Secchkcd
		{
			get
			{
				return this._secchkcd;
			}
			set
			{
				this._secchkcd = value;
			}
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000F7568 File Offset: 0x000F5768
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
				if (codepoint != CodePoint.SVRCOD)
				{
					if (codepoint != CodePoint.SECCHKCD)
					{
						if (codepoint != CodePoint.SECTKN)
						{
							if (Logger.maxTracingLevel >= 4)
							{
								Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SECCHKCodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
							}
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						}
						else
						{
							this._sectkn = await reader.ReadBytesAsync(isAsync, cancellationToken);
						}
					}
					else
					{
						this._secchkcd = await reader.ReadByteAsync(isAsync, cancellationToken);
					}
				}
				else
				{
					this._svrcod = (int)(await reader.ReadInt16Async(isAsync, cancellationToken));
				}
			}
		}

		// Token: 0x0400327F RID: 12927
		private int _svrcod;

		// Token: 0x04003280 RID: 12928
		private byte _secchkcd;
	}
}

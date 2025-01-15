using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008E8 RID: 2280
	public class SYNCCRD : AbstractDdmObject
	{
		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x06004846 RID: 18502 RVA: 0x001073ED File Offset: 0x001055ED
		// (set) Token: 0x06004847 RID: 18503 RVA: 0x001073F5 File Offset: 0x001055F5
		public bool ReleaseConnection { get; set; }

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x06004848 RID: 18504 RVA: 0x001073FE File Offset: 0x001055FE
		public List<Xid> IndoubtList
		{
			get
			{
				return this._indoubtList;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x06004849 RID: 18505 RVA: 0x00107406 File Offset: 0x00105606
		// (set) Token: 0x0600484A RID: 18506 RVA: 0x0010740E File Offset: 0x0010560E
		public int XaReturnValue { get; set; }

		// Token: 0x0600484B RID: 18507 RVA: 0x00107417 File Offset: 0x00105617
		public SYNCCRD()
		{
			this.ReleaseConnection = true;
			this.XaReturnValue = 0;
		}

		// Token: 0x0600484C RID: 18508 RVA: 0x00107438 File Offset: 0x00105638
		public override string ToString()
		{
			return string.Format("SYNCCRD[XaReturnValue={0};ReleaseConnection={1};IndoubtList={2}]", this.XaReturnValue, this.ReleaseConnection, this.IndoubtList.Count);
		}

		// Token: 0x0600484D RID: 18509 RVA: 0x0010746C File Offset: 0x0010566C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
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
				if (codepoint != CodePoint.RLSCONV)
				{
					if (codepoint != CodePoint.XARETVAL)
					{
						if (codepoint != CodePoint.PRPHRCLST)
						{
							if (Logger.maxTracingLevel >= 4)
							{
								Logger.Warning(this._tracePoint, 0, 4, "SYNCCRD::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
							}
							await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						}
						else
						{
							await reader.ReadDdmObjectLengthAndCodePointAsync(isAsync, cancellationToken);
							this._indoubtList.Clear();
							int xidCount = (int)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
							for (int i = 0; i < xidCount; i++)
							{
								await reader.ReadDdmObjectLengthAndCodePointAsync(isAsync, cancellationToken);
								Xid xid = new Xid();
								await xid.ReadAsync(reader, isAsync, cancellationToken);
								this._indoubtList.Add(xid);
								xid = null;
							}
						}
					}
					else
					{
						this.XaReturnValue = await reader.ReadInt32Async(isAsync, cancellationToken);
					}
				}
				else
				{
					this.ReleaseConnection = await reader.ReadByteAsync(isAsync, cancellationToken) == 240;
				}
			}
		}

		// Token: 0x040034D6 RID: 13526
		private List<Xid> _indoubtList = new List<Xid>();
	}
}

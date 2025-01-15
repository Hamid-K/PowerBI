using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000873 RID: 2163
	public class ACCRDBRM : ACCRDB
	{
		// Token: 0x060044A2 RID: 17570 RVA: 0x000E7BD6 File Offset: 0x000E5DD6
		public ACCRDBRM(int requesterId, int sqlamlevel)
			: base(null, sqlamlevel)
		{
			this._requesterId = requesterId;
		}

		// Token: 0x060044A3 RID: 17571 RVA: 0x000E7BF2 File Offset: 0x000E5DF2
		public ACCRDBRM(int sqlamlevel)
			: base(null, sqlamlevel)
		{
		}

		// Token: 0x1700103E RID: 4158
		// (get) Token: 0x060044A4 RID: 17572 RVA: 0x000E7C07 File Offset: 0x000E5E07
		// (set) Token: 0x060044A5 RID: 17573 RVA: 0x000E7C0F File Offset: 0x000E5E0F
		public string UserId
		{
			get
			{
				return this._userId;
			}
			set
			{
				this._userId = value;
			}
		}

		// Token: 0x1700103F RID: 4159
		// (get) Token: 0x060044A6 RID: 17574 RVA: 0x000E7C18 File Offset: 0x000E5E18
		// (set) Token: 0x060044A7 RID: 17575 RVA: 0x000E7C20 File Offset: 0x000E5E20
		public SRVLST Srvlst
		{
			get
			{
				return this._srvlst;
			}
			set
			{
				this._srvlst = value;
			}
		}

		// Token: 0x17001040 RID: 4160
		// (get) Token: 0x060044A8 RID: 17576 RVA: 0x000E7C29 File Offset: 0x000E5E29
		// (set) Token: 0x060044A9 RID: 17577 RVA: 0x000E7C31 File Offset: 0x000E5E31
		public SeverityCode Svrcod
		{
			get
			{
				return this._svrcod;
			}
			set
			{
				this._svrcod = value;
			}
		}

		// Token: 0x17001041 RID: 4161
		// (get) Token: 0x060044AA RID: 17578 RVA: 0x000E7C3A File Offset: 0x000E5E3A
		// (set) Token: 0x060044AB RID: 17579 RVA: 0x000E7C42 File Offset: 0x000E5E42
		public byte[] RdbIntToken
		{
			get
			{
				return this._rdbIntToken;
			}
			set
			{
				this._rdbIntToken = value;
			}
		}

		// Token: 0x17001042 RID: 4162
		// (get) Token: 0x060044AC RID: 17580 RVA: 0x000E7C4B File Offset: 0x000E5E4B
		// (set) Token: 0x060044AD RID: 17581 RVA: 0x000E7C53 File Offset: 0x000E5E53
		public IPADDR IpAddresss
		{
			get
			{
				return this._ipAddr;
			}
			set
			{
				this._ipAddr = value;
			}
		}

		// Token: 0x17001043 RID: 4163
		// (get) Token: 0x060044AE RID: 17582 RVA: 0x000E7C5C File Offset: 0x000E5E5C
		// (set) Token: 0x060044AF RID: 17583 RVA: 0x000E7C64 File Offset: 0x000E5E64
		public string PkgDftCst
		{
			get
			{
				return this._pkgDftCst;
			}
			set
			{
				this._pkgDftCst = value;
			}
		}

		// Token: 0x060044B0 RID: 17584 RVA: 0x000E7C70 File Offset: 0x000E5E70
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(5);
			requiredCodePoints.Add(CodePoint.SVRCOD);
			requiredCodePoints.Add(CodePoint.PRDID);
			requiredCodePoints.Add(CodePoint.TYPDEFNAM);
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
				if (codepoint <= CodePoint.SVRCOD)
				{
					if (codepoint <= CodePoint.TYPDEFOVR)
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
							this._typedefovr = new TYPDEFOVR(null, -1);
							await this._typedefovr.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						if (codepoint == CodePoint.PRDID)
						{
							this._prdid = await reader.ReadStringAsync(isAsync, cancellationToken);
							requiredCodePoints.Remove(CodePoint.PRDID);
							continue;
						}
						if (codepoint == CodePoint.SVRCOD)
						{
							this._svrcod = (SeverityCode)(await reader.ReadInt16Async(isAsync, cancellationToken));
							requiredCodePoints.Remove(CodePoint.SVRCOD);
							continue;
						}
					}
				}
				else if (codepoint <= CodePoint.IPADDR)
				{
					if (codepoint == CodePoint.USRID)
					{
						this._userId = await reader.ReadStringAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.IPADDR)
					{
						this._ipAddr = new IPADDR((int)objectInfo.Length);
						await this._ipAddr.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.RDBINTTKN)
					{
						this._rdbIntToken = await reader.ReadBytesAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.PKGDFTCST)
					{
						this._pkgDftCst = await reader.ReadStringAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.SRVLST)
					{
						this._srvlst = new SRVLST();
						await this._srvlst.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, this._requesterId, 4, "ACCRDB::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x060044B1 RID: 17585 RVA: 0x000E7CD0 File Offset: 0x000E5ED0
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.ACCRDBRM);
			if (this._srvlst != null && this._srvlst.List.Count > 0)
			{
				SRVLST srvlst = this._srvlst;
				lock (srvlst)
				{
					this._srvlst.Write(writer);
					if (Logger.maxTracingLevel >= 4)
					{
						Logger.Verbose(this._tracePoint, 0, this._srvlst.ToString(), Array.Empty<object>());
					}
				}
			}
			writer.WriteScalar2Bytes(CodePoint.SVRCOD, (int)this.Svrcod);
			writer.WriteScalar(CodePoint.PRDID, base.Prdid);
			writer.WriteScalar(CodePoint.TYPDEFNAM, base.Typdefnam);
			base.Typedefovr.Write(writer);
			writer.WriteEndDdm();
		}

		// Token: 0x060044B2 RID: 17586 RVA: 0x000E7DA4 File Offset: 0x000E5FA4
		public override string ToString()
		{
			return base.FixParenthis(string.Format("ACCRDBRM[svrcod={0};typdefnam={1};typedefovr={2};SRVLIST={3}]", new object[]
			{
				this._svrcod,
				base.Typdefnam,
				base.Typedefovr,
				(this._srvlst == null) ? "" : this._srvlst.ToString()
			}));
		}

		// Token: 0x0400304D RID: 12365
		private SeverityCode _svrcod;

		// Token: 0x0400304E RID: 12366
		private string _userId = string.Empty;

		// Token: 0x0400304F RID: 12367
		private SRVLST _srvlst;

		// Token: 0x04003050 RID: 12368
		private int _requesterId;

		// Token: 0x04003051 RID: 12369
		private byte[] _rdbIntToken;

		// Token: 0x04003052 RID: 12370
		private IPADDR _ipAddr;

		// Token: 0x04003053 RID: 12371
		private string _pkgDftCst;
	}
}

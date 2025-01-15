using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000875 RID: 2165
	public class ACCSEC : AbstractDdmObject
	{
		// Token: 0x060044B5 RID: 17589 RVA: 0x000E8626 File Offset: 0x000E6826
		public ACCSEC(IDatabase dataBase)
			: base(dataBase)
		{
			this._encalg = EncryptionAlgorithm.EncAlgNone;
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060044B6 RID: 17590 RVA: 0x000E8636 File Offset: 0x000E6836
		// (set) Token: 0x060044B7 RID: 17591 RVA: 0x000E863E File Offset: 0x000E683E
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

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x060044B8 RID: 17592 RVA: 0x000E8647 File Offset: 0x000E6847
		// (set) Token: 0x060044B9 RID: 17593 RVA: 0x000E864F File Offset: 0x000E684F
		public SecurityMechanism Secmec
		{
			get
			{
				return this._secmec;
			}
			set
			{
				this._secmec = value;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060044BA RID: 17594 RVA: 0x000E8658 File Offset: 0x000E6858
		// (set) Token: 0x060044BB RID: 17595 RVA: 0x000E8660 File Offset: 0x000E6860
		public EncryptionAlgorithm EncAlg
		{
			get
			{
				return this._encalg;
			}
			set
			{
				this._encalg = value;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060044BC RID: 17596 RVA: 0x000E8669 File Offset: 0x000E6869
		// (set) Token: 0x060044BD RID: 17597 RVA: 0x000E8671 File Offset: 0x000E6871
		public byte[] Sectkn
		{
			get
			{
				return this._sectkn;
			}
			set
			{
				this._sectkn = value;
			}
		}

		// Token: 0x060044BE RID: 17598 RVA: 0x000E867C File Offset: 0x000E687C
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
				if (codepoint <= CodePoint.SECTKN)
				{
					if (codepoint != CodePoint.SECMEC)
					{
						if (codepoint != CodePoint.SECTKN)
						{
							goto IL_037D;
						}
						this._sectkn = await reader.ReadBytesAsync(isAsync, cancellationToken);
					}
					else
					{
						AbstractDdmObject.CheckLength(reader, CodePoint.SECMEC, 2);
						this._secmec = (SecurityMechanism)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
					}
				}
				else if (codepoint != CodePoint.ENCALG)
				{
					if (codepoint != CodePoint.RDBNAM)
					{
						goto IL_037D;
					}
					rdbnam = new RDBNAM();
					await rdbnam.ReadAsync(reader, isAsync, cancellationToken);
					this._rdbnam = rdbnam.Name;
				}
				else
				{
					this._encalg = (EncryptionAlgorithm)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				}
				IL_042D:
				rdbnam = null;
				continue;
				IL_037D:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "ACCSEC::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				goto IL_042D;
			}
			if (this._secmec == SecurityMechanism.Unknown)
			{
				DrdaException.MissingCodePoint(CodePoint.SECMEC);
			}
		}

		// Token: 0x060044BF RID: 17599 RVA: 0x000E86DC File Offset: 0x000E68DC
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.ACCSEC);
			if (!string.IsNullOrWhiteSpace(this._rdbnam))
			{
				writer.WriteScalarString(CodePoint.RDBNAM, this._rdbnam.PadRight(18), 500);
			}
			writer.WriteScalar2Bytes(CodePoint.SECMEC, (int)this.Secmec);
			if (this._sectkn != null && this._sectkn.Length != 0)
			{
				writer.WriteScalarBytes(CodePoint.SECTKN, this._sectkn);
			}
			if (this._encalg != EncryptionAlgorithm.EncAlgNone)
			{
				writer.WriteScalar2Bytes(CodePoint.ENCALG, (int)this._encalg);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060044C0 RID: 17600 RVA: 0x000E8770 File Offset: 0x000E6970
		public override string ToString()
		{
			return base.FixParenthis(string.Format("ACCSEC[rdbnam={0};secmec={1};sectkn={2};encalg={3}]", new object[]
			{
				this._rdbnam,
				this._secmec,
				BitUtils.ConvertToHexString(this._sectkn),
				this._encalg
			}));
		}

		// Token: 0x04003062 RID: 12386
		private string _rdbnam;

		// Token: 0x04003063 RID: 12387
		private SecurityMechanism _secmec;

		// Token: 0x04003064 RID: 12388
		private byte[] _sectkn;

		// Token: 0x04003065 RID: 12389
		private EncryptionAlgorithm _encalg;
	}
}

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x020008A8 RID: 2216
	public class SECCHK : AbstractDdmObject
	{
		// Token: 0x06004680 RID: 18048 RVA: 0x000F2D4C File Offset: 0x000F0F4C
		public SECCHK()
		{
		}

		// Token: 0x06004681 RID: 18049 RVA: 0x000F6D42 File Offset: 0x000F4F42
		public SECCHK(ACCSEC _accsec)
		{
			this._secmec = _accsec.Secmec;
		}

		// Token: 0x06004682 RID: 18050 RVA: 0x000F6D56 File Offset: 0x000F4F56
		public override string ToString()
		{
			return string.Format("SECCHK[rdbnam={0};usrid={1};secmec={2}]", this._rdbnam, this._usrid, this._secmec);
		}

		// Token: 0x170010E4 RID: 4324
		// (get) Token: 0x06004683 RID: 18051 RVA: 0x000F6D79 File Offset: 0x000F4F79
		// (set) Token: 0x06004684 RID: 18052 RVA: 0x000F6D90 File Offset: 0x000F4F90
		public string Usrid
		{
			get
			{
				if (this._usrid != null)
				{
					return this._usrid.Trim();
				}
				return null;
			}
			set
			{
				this._usrid = value;
			}
		}

		// Token: 0x170010E5 RID: 4325
		// (get) Token: 0x06004685 RID: 18053 RVA: 0x000F6D99 File Offset: 0x000F4F99
		// (set) Token: 0x06004686 RID: 18054 RVA: 0x000F6DB0 File Offset: 0x000F4FB0
		public string Password
		{
			get
			{
				if (this._password != null)
				{
					return this._password.Trim();
				}
				return null;
			}
			set
			{
				this._password = value;
			}
		}

		// Token: 0x170010E6 RID: 4326
		// (get) Token: 0x06004687 RID: 18055 RVA: 0x000F6DB9 File Offset: 0x000F4FB9
		// (set) Token: 0x06004688 RID: 18056 RVA: 0x000F6DD0 File Offset: 0x000F4FD0
		public string NewPassword
		{
			get
			{
				if (this._newPassword != null)
				{
					return this._newPassword.Trim();
				}
				return null;
			}
			set
			{
				this._newPassword = value;
			}
		}

		// Token: 0x170010E7 RID: 4327
		// (get) Token: 0x06004689 RID: 18057 RVA: 0x000F6DD9 File Offset: 0x000F4FD9
		// (set) Token: 0x0600468A RID: 18058 RVA: 0x000F6DE1 File Offset: 0x000F4FE1
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

		// Token: 0x170010E8 RID: 4328
		// (get) Token: 0x0600468B RID: 18059 RVA: 0x000F6DEA File Offset: 0x000F4FEA
		// (set) Token: 0x0600468C RID: 18060 RVA: 0x000F6DF2 File Offset: 0x000F4FF2
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

		// Token: 0x170010E9 RID: 4329
		// (get) Token: 0x0600468D RID: 18061 RVA: 0x000F6DFB File Offset: 0x000F4FFB
		// (set) Token: 0x0600468E RID: 18062 RVA: 0x000F6E03 File Offset: 0x000F5003
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

		// Token: 0x170010EA RID: 4330
		// (get) Token: 0x0600468F RID: 18063 RVA: 0x000F6E0C File Offset: 0x000F500C
		// (set) Token: 0x06004690 RID: 18064 RVA: 0x000F6E14 File Offset: 0x000F5014
		public byte[] Sectkn2
		{
			get
			{
				return this._sectkn2;
			}
			set
			{
				this._sectkn2 = value;
			}
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x000F6E20 File Offset: 0x000F5020
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			bool isUseridSet = false;
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
				if (codepoint <= CodePoint.SECMEC)
				{
					if (codepoint != CodePoint.SECMGRNM)
					{
						switch (codepoint)
						{
						case CodePoint.USRID:
							this._usrid = await reader.ReadStringAsync(isAsync, cancellationToken);
							break;
						case CodePoint.DDMPASSWORD:
							this._password = await reader.ReadStringAsync(isAsync, cancellationToken);
							break;
						case CodePoint.SECMEC:
							AbstractDdmObject.CheckLength(reader, CodePoint.SECMEC, 2);
							this._secmec = (SecurityMechanism)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
							break;
						default:
							goto IL_04AB;
						}
					}
					else
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
					}
				}
				else if (codepoint != CodePoint.SECTKN)
				{
					if (codepoint != CodePoint.RDBNAM)
					{
						goto IL_04AB;
					}
					rdbnam = new RDBNAM();
					await rdbnam.ReadAsync(reader, isAsync, cancellationToken);
					this._rdbnam = rdbnam.Name;
				}
				else
				{
					byte[] array = await reader.ReadBytesAsync(isAsync, cancellationToken);
					if (!isUseridSet)
					{
						this._sectkn = array;
						isUseridSet = true;
					}
					else
					{
						this._sectkn2 = array;
					}
				}
				IL_055B:
				rdbnam = null;
				continue;
				IL_04AB:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "SECCHKCodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				goto IL_055B;
			}
			if (this._secmec == SecurityMechanism.Unknown)
			{
				DrdaException.MissingCodePoint(CodePoint.SECMEC);
			}
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000F6E80 File Offset: 0x000F5080
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.SECCHK);
			writer.WriteScalar2Bytes(CodePoint.SECMEC, (int)this.Secmec);
			writer.WriteScalarPaddedString(CodePoint.RDBNAM, this._rdbnam, 18);
			if (this._sectkn != null)
			{
				writer.WriteScalarBytes(CodePoint.SECTKN, this._sectkn);
			}
			if (this._sectkn2 != null)
			{
				writer.WriteScalarBytes(CodePoint.SECTKN, this._sectkn2);
			}
			if (this._usrid != null)
			{
				writer.WriteScalar(CodePoint.USRID, this._usrid);
			}
			if (this._password != null)
			{
				writer.WriteScalar(CodePoint.DDMPASSWORD, this._password);
			}
			if (this._newPassword != null)
			{
				writer.WriteScalar(CodePoint.NEWPASSWORD, this._newPassword);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x170010EB RID: 4331
		// (get) Token: 0x06004693 RID: 18067 RVA: 0x000F6F3F File Offset: 0x000F513F
		// (set) Token: 0x06004694 RID: 18068 RVA: 0x000F6F47 File Offset: 0x000F5147
		public byte[] ServerPublicKey
		{
			get
			{
				return this._publicKey;
			}
			set
			{
				this._publicKey = value;
			}
		}

		// Token: 0x04003268 RID: 12904
		protected SecurityMechanism _secmec;

		// Token: 0x04003269 RID: 12905
		protected string _rdbnam;

		// Token: 0x0400326A RID: 12906
		protected string _password;

		// Token: 0x0400326B RID: 12907
		protected string _newPassword;

		// Token: 0x0400326C RID: 12908
		protected string _usrid;

		// Token: 0x0400326D RID: 12909
		protected byte[] _sectkn;

		// Token: 0x0400326E RID: 12910
		protected byte[] _sectkn2;

		// Token: 0x0400326F RID: 12911
		protected byte[] _publicKey;
	}
}

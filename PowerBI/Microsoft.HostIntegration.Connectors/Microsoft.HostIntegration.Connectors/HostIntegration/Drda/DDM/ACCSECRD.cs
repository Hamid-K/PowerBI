using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000877 RID: 2167
	public class ACCSECRD : ACCSEC
	{
		// Token: 0x060044C3 RID: 17603 RVA: 0x000E8C9A File Offset: 0x000E6E9A
		public ACCSECRD()
			: base(null)
		{
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060044C4 RID: 17604 RVA: 0x000E8CB9 File Offset: 0x000E6EB9
		// (set) Token: 0x060044C5 RID: 17605 RVA: 0x000E8CC1 File Offset: 0x000E6EC1
		public int Secchkcd
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

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x060044C6 RID: 17606 RVA: 0x000E8CCA File Offset: 0x000E6ECA
		public string ServerPrincipal
		{
			get
			{
				return this._principal;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060044C7 RID: 17607 RVA: 0x000E8CD2 File Offset: 0x000E6ED2
		public SortedSet<SecurityMechanism> SecurityMechanisms
		{
			get
			{
				return this._securityMechanisms;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x060044C8 RID: 17608 RVA: 0x000E8CDA File Offset: 0x000E6EDA
		public List<EncryptionAlgorithm> EncryptionAlgorithms
		{
			get
			{
				return this._encryptionAlgos;
			}
		}

		// Token: 0x060044C9 RID: 17609 RVA: 0x000E8CE4 File Offset: 0x000E6EE4
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this._securityMechanisms.Clear();
			this._encryptionAlgos.Clear();
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
				if (codepoint <= CodePoint.SECCHKCD)
				{
					if (codepoint == CodePoint.SECMEC)
					{
						int mechCount = (int)(reader.DdmObjectLength / 2L);
						for (int i = 0; i < mechCount; i++)
						{
							SortedSet<SecurityMechanism> sortedSet = this._securityMechanisms;
							sortedSet.Add((SecurityMechanism)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken)));
							sortedSet = null;
						}
						continue;
					}
					if (codepoint == CodePoint.SECCHKCD)
					{
						this._secchkcd = (int)(await reader.ReadByteAsync(isAsync, cancellationToken));
						continue;
					}
				}
				else
				{
					if (codepoint == CodePoint.SECTKN)
					{
						base.Sectkn = await reader.ReadBytesAsync(isAsync, cancellationToken);
						continue;
					}
					if (codepoint == CodePoint.ENCALG)
					{
						int algoCount = (int)(reader.DdmObjectLength / 2L);
						for (int i = 0; i < algoCount; i++)
						{
							List<EncryptionAlgorithm> list = this._encryptionAlgos;
							list.Add((EncryptionAlgorithm)(await reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken)));
							list = null;
						}
						continue;
					}
					if (codepoint == CodePoint.KERSECPPL)
					{
						this._principal = await reader.ReadStringAsync(isAsync, cancellationToken);
						continue;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "ACCSECRD::Read CodePoint not supported in " + this.ToString() + ": " + codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (this._securityMechanisms.Count == 0)
			{
				DrdaException.MissingCodePoint(CodePoint.SECMEC);
			}
		}

		// Token: 0x060044CA RID: 17610 RVA: 0x000E8D44 File Offset: 0x000E6F44
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.ACCSECRD);
			if (this.Secchkcd != 1 && this.Secchkcd != 27)
			{
				writer.WriteScalar2Bytes(CodePoint.SECMEC, (int)base.Secmec);
				if (base.Sectkn != null && base.Sectkn.Length != 0)
				{
					writer.WriteScalarBytes(CodePoint.SECTKN, base.Sectkn);
				}
				if (base.EncAlg == EncryptionAlgorithm.EncAlgAES)
				{
					writer.WriteScalar2Bytes(CodePoint.ENCALG, (int)base.EncAlg);
				}
			}
			else
			{
				writer.WriteBeginDdm(CodePoint.SECMEC);
				writer.WriteInt16(3, EndianType.BigEndian);
				writer.WriteInt16(9, EndianType.BigEndian);
				writer.WriteInt16(4, EndianType.BigEndian);
				writer.WriteEndDdm();
			}
			if (this.Secchkcd != 0)
			{
				writer.WriteScalar1Byte(CodePoint.SECCHKCD, this.Secchkcd);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x060044CB RID: 17611 RVA: 0x000E8E07 File Offset: 0x000E7007
		public override string ToString()
		{
			return string.Format("ACCSECRD[secchkcd={0}]", this.Secchkcd);
		}

		// Token: 0x04003073 RID: 12403
		private int _secchkcd;

		// Token: 0x04003074 RID: 12404
		private string _principal;

		// Token: 0x04003075 RID: 12405
		private SortedSet<SecurityMechanism> _securityMechanisms = new SortedSet<SecurityMechanism>();

		// Token: 0x04003076 RID: 12406
		private List<EncryptionAlgorithm> _encryptionAlgos = new List<EncryptionAlgorithm>();
	}
}

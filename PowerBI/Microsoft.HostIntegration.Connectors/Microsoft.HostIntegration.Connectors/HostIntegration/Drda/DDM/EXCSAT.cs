using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x0200088A RID: 2186
	public class EXCSAT : AbstractDdmObject
	{
		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06004571 RID: 17777 RVA: 0x000EF436 File Offset: 0x000ED636
		// (set) Token: 0x06004572 RID: 17778 RVA: 0x000EF43E File Offset: 0x000ED63E
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

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06004573 RID: 17779 RVA: 0x000EF447 File Offset: 0x000ED647
		// (set) Token: 0x06004574 RID: 17780 RVA: 0x000EF44F File Offset: 0x000ED64F
		public bool IsPing
		{
			get
			{
				return this._isPing;
			}
			set
			{
				this._isPing = value;
			}
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x000EF458 File Offset: 0x000ED658
		public override string ToString()
		{
			return base.FixParenthis(string.Format("EXCSAT[extnam={0};spvnam={1};srvclsnm={2};srvnam={3};srvrlslv={4};mgrlvls={5};srvlst={6}]", new object[]
			{
				this._extnam,
				this._spvnam,
				this._srvclsnm,
				this._srvnam,
				this._srvrlslv,
				this.GetMgrLevelsAsString(),
				this._srvlst
			}));
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x000EF4BC File Offset: 0x000ED6BC
		private object GetMgrLevelsAsString()
		{
			StringBuilder stringBuilder = new StringBuilder("[");
			foreach (ManagerCodePoint managerCodePoint in this._mgrLvlls.Keys)
			{
				stringBuilder.Append(string.Format("[{0}={1}]", managerCodePoint, this._mgrLvlls[managerCodePoint]));
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06004577 RID: 17783 RVA: 0x000EF554 File Offset: 0x000ED754
		// (set) Token: 0x06004578 RID: 17784 RVA: 0x000EF55C File Offset: 0x000ED75C
		public string Extnam
		{
			get
			{
				return this._extnam;
			}
			set
			{
				this._extnam = value;
			}
		}

		// Token: 0x17001088 RID: 4232
		// (get) Token: 0x06004579 RID: 17785 RVA: 0x000EF565 File Offset: 0x000ED765
		// (set) Token: 0x0600457A RID: 17786 RVA: 0x000EF56D File Offset: 0x000ED76D
		public string Spvnam
		{
			get
			{
				return this._spvnam;
			}
			set
			{
				this._spvnam = value;
			}
		}

		// Token: 0x17001089 RID: 4233
		// (get) Token: 0x0600457B RID: 17787 RVA: 0x000EF576 File Offset: 0x000ED776
		// (set) Token: 0x0600457C RID: 17788 RVA: 0x000EF57E File Offset: 0x000ED77E
		public string Srvclsnm
		{
			get
			{
				return this._srvclsnm;
			}
			set
			{
				this._srvclsnm = value;
			}
		}

		// Token: 0x1700108A RID: 4234
		// (get) Token: 0x0600457D RID: 17789 RVA: 0x000EF587 File Offset: 0x000ED787
		// (set) Token: 0x0600457E RID: 17790 RVA: 0x000EF58F File Offset: 0x000ED78F
		public string Srvnam
		{
			get
			{
				return this._srvnam;
			}
			set
			{
				this._srvnam = value;
			}
		}

		// Token: 0x1700108B RID: 4235
		// (get) Token: 0x0600457F RID: 17791 RVA: 0x000EF598 File Offset: 0x000ED798
		// (set) Token: 0x06004580 RID: 17792 RVA: 0x000EF5A0 File Offset: 0x000ED7A0
		public string Srvrlslv
		{
			get
			{
				return this._srvrlslv;
			}
			set
			{
				this._srvrlslv = value;
			}
		}

		// Token: 0x1700108C RID: 4236
		// (get) Token: 0x06004581 RID: 17793 RVA: 0x000EF5A9 File Offset: 0x000ED7A9
		public Dictionary<ManagerCodePoint, int> MgrLvlls
		{
			get
			{
				return this._mgrLvlls;
			}
		}

		// Token: 0x06004582 RID: 17794 RVA: 0x000EF5B4 File Offset: 0x000ED7B4
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this._isPing = true;
			IEnumerator<Task<ObjectInfo>> taskEnumerator = (isAsync ? reader.ReadDdmObjectsAsync(cancellationToken).GetEnumerator() : null);
			IEnumerator<ObjectInfo> enumerator = (isAsync ? null : reader.ReadDdmObjects().GetEnumerator());
			while (isAsync ? taskEnumerator.MoveNext() : enumerator.MoveNext())
			{
				ObjectInfo obj;
				if (isAsync)
				{
					ObjectInfo objectInfo = await taskEnumerator.Current;
					obj = objectInfo;
					if (obj.Equals(ObjectInfo.InvalidInstance))
					{
						break;
					}
				}
				else
				{
					obj = enumerator.Current;
				}
				this._isPing = false;
				base.LogCodePoint(obj.Codepoint);
				CodePoint codepoint = obj.Codepoint;
				if (codepoint <= CodePoint.EXTNAM)
				{
					if (codepoint != CodePoint.SRVCLSNM)
					{
						switch (codepoint)
						{
						case CodePoint.SRVRLSLV:
							this._srvrlslv = await reader.ReadStringAsync(isAsync, cancellationToken);
							if (this._srvrlslv.Length > 255)
							{
								DrdaException.TooBig(obj.Codepoint);
							}
							break;
						case CodePoint.CSRPOSST:
						case CodePoint.DTALCKST:
							goto IL_05BF;
						case CodePoint.SPVNAM:
							this._spvnam = await reader.ReadStringAsync(isAsync, cancellationToken);
							break;
						case CodePoint.EXTNAM:
							this._extnam = await reader.ReadStringAsync(isAsync, cancellationToken);
							if (this._extnam.Length > 255)
							{
								DrdaException.TooBig(obj.Codepoint);
							}
							break;
						default:
							goto IL_05BF;
						}
					}
					else
					{
						this._srvclsnm = await reader.ReadStringAsync(isAsync, cancellationToken);
						if (this._srvclsnm.Length > 255)
						{
							DrdaException.TooBig(obj.Codepoint);
						}
					}
				}
				else if (codepoint != CodePoint.SRVNAM)
				{
					if (codepoint != CodePoint.MGRLVLLS)
					{
						if (codepoint != CodePoint.SRVLST)
						{
							goto IL_05BF;
						}
						this._srvlst = new SRVLST();
						await this._srvlst.ReadAsync(reader, isAsync, cancellationToken);
					}
					else
					{
						await this.ParseMgrlvllsAsync(reader, isAsync, cancellationToken);
					}
				}
				else
				{
					this._srvnam = await reader.ReadStringAsync(isAsync, cancellationToken);
					if (this._srvnam.Length > 255)
					{
						DrdaException.TooBig(obj.Codepoint);
					}
				}
				IL_067C:
				obj = default(ObjectInfo);
				continue;
				IL_05BF:
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSAT::Read CodePoint not supported in " + this.ToString() + ": " + obj.Codepoint.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
				goto IL_067C;
			}
		}

		// Token: 0x06004583 RID: 17795 RVA: 0x000EF614 File Offset: 0x000ED814
		private async Task ParseMgrlvllsAsync(DdmReader _reader, bool isAsync, CancellationToken cancellationToken)
		{
			while (_reader.HasMoreDdmObjectData())
			{
				short num = await _reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken);
				ManagerCodePoint cp = (ManagerCodePoint)num;
				int num2 = (int)(await _reader.ReadInt16Async(EndianType.BigEndian, isAsync, cancellationToken));
				this._mgrLvlls.Add(cp, num2);
			}
		}

		// Token: 0x06004584 RID: 17796 RVA: 0x000EF674 File Offset: 0x000ED874
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXCSAT);
			if (this.Extnam != null)
			{
				writer.WriteScalar(CodePoint.EXTNAM, this.Extnam);
			}
			if (this.Srvnam != null)
			{
				writer.WriteScalar(CodePoint.SRVNAM, this.Srvnam);
			}
			if (this.Srvclsnm != null)
			{
				writer.WriteScalar(CodePoint.SRVCLSNM, this.Srvclsnm);
			}
			if (this.Srvrlslv != null)
			{
				writer.WriteScalar(CodePoint.SRVRLSLV, this.Srvrlslv);
			}
			if (this.MgrLvlls != null && this.MgrLvlls.Count > 0)
			{
				this.WriteMgrlvlls(writer);
			}
			if (this._srvlst != null && this._srvlst.List.Count > 0)
			{
				this._srvlst.Write(writer);
			}
			writer.WriteEndDdm();
		}

		// Token: 0x06004585 RID: 17797 RVA: 0x000EF73C File Offset: 0x000ED93C
		private void WriteManagerLevel(DdmWriter writer, ManagerCodePoint managerCodePoint)
		{
			int num = 0;
			if (this.MgrLvlls.TryGetValue(managerCodePoint, out num))
			{
				writer.WriteCodePointAnd2Bytes((CodePoint)managerCodePoint, num, EndianType.BigEndian);
			}
		}

		// Token: 0x06004586 RID: 17798 RVA: 0x000EF764 File Offset: 0x000ED964
		private void WriteMgrlvlls(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.MGRLVLLS);
			this.WriteManagerLevel(writer, ManagerCodePoint.CMNTCPIP);
			this.WriteManagerLevel(writer, ManagerCodePoint.AGENT);
			this.WriteManagerLevel(writer, ManagerCodePoint.SQLAM);
			this.WriteManagerLevel(writer, ManagerCodePoint.RDB);
			this.WriteManagerLevel(writer, ManagerCodePoint.SECMGR);
			this.WriteManagerLevel(writer, ManagerCodePoint.XAMGR);
			this.WriteManagerLevel(writer, ManagerCodePoint.SYNCPTMGR);
			this.WriteManagerLevel(writer, ManagerCodePoint.RSYNCMGR);
			this.WriteManagerLevel(writer, ManagerCodePoint.UNICODEMGR);
			writer.WriteEndDdm();
		}

		// Token: 0x04003151 RID: 12625
		private string _extnam;

		// Token: 0x04003152 RID: 12626
		private string _spvnam;

		// Token: 0x04003153 RID: 12627
		private string _srvclsnm;

		// Token: 0x04003154 RID: 12628
		private string _srvnam;

		// Token: 0x04003155 RID: 12629
		private string _srvrlslv;

		// Token: 0x04003156 RID: 12630
		private bool _isPing;

		// Token: 0x04003157 RID: 12631
		private Dictionary<ManagerCodePoint, int> _mgrLvlls = new Dictionary<ManagerCodePoint, int>();

		// Token: 0x04003158 RID: 12632
		private SRVLST _srvlst;
	}
}

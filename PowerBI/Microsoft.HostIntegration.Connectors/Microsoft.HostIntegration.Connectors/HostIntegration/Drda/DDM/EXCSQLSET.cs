using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;

namespace Microsoft.HostIntegration.Drda.DDM
{
	// Token: 0x02000890 RID: 2192
	public class EXCSQLSET : AbstractDdmObject
	{
		// Token: 0x060045A4 RID: 17828 RVA: 0x000F0BA2 File Offset: 0x000EEDA2
		public EXCSQLSET(IDatabase database, int sqlamLevel, string accrdbTypedefname)
			: base(database, sqlamLevel, accrdbTypedefname)
		{
			this._sqlstts = new ArrayList();
		}

		// Token: 0x060045A5 RID: 17829 RVA: 0x000F0BB8 File Offset: 0x000EEDB8
		public override string ToString()
		{
			return base.FixParenthis(string.Format("EXCSQLSET[rdbnam={0};pkgnamcsn={1};typedefovr={2};typedefnam={3};sqlstt={4};]", new object[]
			{
				this._rdbnam,
				this._pkgnamcsn,
				this._typedefovr,
				this._typdefnam,
				this.GetSqlsttsAsString()
			}));
		}

		// Token: 0x060045A6 RID: 17830 RVA: 0x000F0C08 File Offset: 0x000EEE08
		public string GetSqlsttsAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in this._sqlstts)
			{
				string text = (string)obj;
				stringBuilder.Append(text + ";");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060045A7 RID: 17831 RVA: 0x000F0C78 File Offset: 0x000EEE78
		public override void Reset()
		{
			if (this._pkgnamcsn != null)
			{
				this._pkgnamcsn.Reset();
			}
			if (this._rdbnam != null)
			{
				this._rdbnam.Reset();
			}
			if (this._typedefovr != null)
			{
				this._typedefovr.Reset();
			}
			this._typdefnam = null;
			this._sqlstts.Clear();
		}

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x060045A8 RID: 17832 RVA: 0x000F0CD0 File Offset: 0x000EEED0
		// (set) Token: 0x060045A9 RID: 17833 RVA: 0x000F0CD8 File Offset: 0x000EEED8
		public string Typdefnam
		{
			get
			{
				return this._typdefnam;
			}
			set
			{
				this._typdefnam = value;
			}
		}

		// Token: 0x17001095 RID: 4245
		// (get) Token: 0x060045AA RID: 17834 RVA: 0x000F0CE1 File Offset: 0x000EEEE1
		public ArrayList Sqlstts
		{
			get
			{
				return this._sqlstts;
			}
		}

		// Token: 0x17001096 RID: 4246
		// (get) Token: 0x060045AB RID: 17835 RVA: 0x000F0CE9 File Offset: 0x000EEEE9
		// (set) Token: 0x060045AC RID: 17836 RVA: 0x000F0CF1 File Offset: 0x000EEEF1
		public TYPDEFOVR Typedefovr
		{
			get
			{
				return this._typedefovr;
			}
			set
			{
				this._typedefovr = value;
			}
		}

		// Token: 0x17001097 RID: 4247
		// (get) Token: 0x060045AD RID: 17837 RVA: 0x000F0CFA File Offset: 0x000EEEFA
		// (set) Token: 0x060045AE RID: 17838 RVA: 0x000F0D02 File Offset: 0x000EEF02
		public PKGNAMCSN Pkgnamcsn
		{
			get
			{
				return this._pkgnamcsn;
			}
			set
			{
				this._pkgnamcsn = value;
			}
		}

		// Token: 0x17001098 RID: 4248
		// (get) Token: 0x060045AF RID: 17839 RVA: 0x000F0D0B File Offset: 0x000EEF0B
		// (set) Token: 0x060045B0 RID: 17840 RVA: 0x000F0D13 File Offset: 0x000EEF13
		public RDBNAM Rdbnam
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

		// Token: 0x060045B1 RID: 17841 RVA: 0x000F0D1C File Offset: 0x000EEF1C
		public override void Write(DdmWriter writer)
		{
			writer.WriteBeginDdm(CodePoint.EXCSQLSET);
			writer.WriteBeginDdm(CodePoint.PKGNAMCSN);
			this._pkgnamcsn.Write(writer);
			writer.WriteEndDdm();
			writer.WriteEndDdm();
		}

		// Token: 0x060045B2 RID: 17842 RVA: 0x000F0D4C File Offset: 0x000EEF4C
		public override async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			List<CodePoint> requiredCodePoints = new List<CodePoint>(1);
			requiredCodePoints.Add(CodePoint.SQLSTT);
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
				CodePoint cp = objectInfo.Codepoint;
				base.LogCodePoint(cp);
				CodePoint codePoint = cp;
				if (codePoint <= CodePoint.TYPDEFOVR)
				{
					if (codePoint != CodePoint.TYPDEFNAM)
					{
						if (codePoint == CodePoint.TYPDEFOVR)
						{
							if (this._typedefovr == null)
							{
								this._typedefovr = new TYPDEFOVR(this._database, this._sqlamLevel);
							}
							await this._typedefovr.ReadAsync(reader, isAsync, cancellationToken);
							continue;
						}
					}
					else
					{
						this._typdefnam = await reader.ReadStringAsync(isAsync, cancellationToken);
						if (this._typdefnam.Length > 255)
						{
							DrdaException.TooBig(CodePoint.TYPDEFNAM);
							continue;
						}
						continue;
					}
				}
				else
				{
					if (codePoint == CodePoint.MONITOR)
					{
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					}
					switch (codePoint)
					{
					case CodePoint.RDBNAM:
						if (this._rdbnam == null)
						{
							this._rdbnam = new RDBNAM();
						}
						await this._rdbnam.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					case CodePoint.OUTEXP:
						break;
					case CodePoint.PKGNAMCT:
						await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
						continue;
					case CodePoint.PKGNAMCSN:
						if (this._pkgnamcsn == null)
						{
							this._pkgnamcsn = new PKGNAMCSN(this._database.PkgnamcsnCcsid);
						}
						await this._pkgnamcsn.ReadAsync(reader, isAsync, cancellationToken);
						continue;
					default:
						if (codePoint == CodePoint.SQLSTT)
						{
							string text = ((this._typedefovr == null) ? (await base.ParseEncodedStringAsync(reader, this._database.Ccsid, this._sqlamLevel, isAsync, cancellationToken)) : (await base.ParseEncodedStringAsync(reader, this._typedefovr.Ccsid, this._sqlamLevel, isAsync, cancellationToken)));
							this._sqlstts.Add(text);
							requiredCodePoints.Remove(cp);
							continue;
						}
						break;
					}
				}
				if (Logger.maxTracingLevel >= 4)
				{
					Logger.Warning(this._tracePoint, base.DatabaseSessionId, 4, "EXCSQLSET::Read CodePoint not supported in " + this.ToString() + ": " + cp.ToString(), Array.Empty<object>());
				}
				await reader.SkipCurrentDdmObjectAsync(isAsync, cancellationToken);
			}
			if (requiredCodePoints.Count != 0)
			{
				DrdaException.MissingCodePoint(requiredCodePoints[0]);
			}
		}

		// Token: 0x04003183 RID: 12675
		private PKGNAMCSN _pkgnamcsn;

		// Token: 0x04003184 RID: 12676
		private RDBNAM _rdbnam;

		// Token: 0x04003185 RID: 12677
		private TYPDEFOVR _typedefovr;

		// Token: 0x04003186 RID: 12678
		private string _typdefnam;

		// Token: 0x04003187 RID: 12679
		private ArrayList _sqlstts;
	}
}

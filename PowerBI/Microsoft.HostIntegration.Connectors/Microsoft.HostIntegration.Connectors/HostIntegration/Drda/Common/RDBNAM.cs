using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000831 RID: 2097
	public class RDBNAM
	{
		// Token: 0x060042D3 RID: 17107 RVA: 0x00002061 File Offset: 0x00000261
		public RDBNAM()
		{
		}

		// Token: 0x060042D4 RID: 17108 RVA: 0x000E02E8 File Offset: 0x000DE4E8
		public void Reset()
		{
			this._name = null;
		}

		// Token: 0x060042D5 RID: 17109 RVA: 0x000E02F1 File Offset: 0x000DE4F1
		public RDBNAM(string name)
		{
			this._name = name;
		}

		// Token: 0x060042D6 RID: 17110 RVA: 0x000E0300 File Offset: 0x000DE500
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x000E0300 File Offset: 0x000DE500
		// (set) Token: 0x060042D8 RID: 17112 RVA: 0x000E0308 File Offset: 0x000DE508
		public string Name
		{
			get
			{
				return this._name;
			}
			set
			{
				this._name = value;
			}
		}

		// Token: 0x060042D9 RID: 17113 RVA: 0x000E0314 File Offset: 0x000DE514
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x060042DA RID: 17114 RVA: 0x000E033C File Offset: 0x000DE53C
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			string text = await reader.ReadStringAsync(isAsync, cancellationToken);
			this._name = text;
			if (string.IsNullOrEmpty(this._name))
			{
				DrdaException.RdbNotFound(CodePoint.RDBNAM, null);
			}
			if (this._name.Length < 18 || this._name.Length > 255)
			{
				DrdaException.BadObjectLength(CodePoint.RDBNAM);
			}
			this._name = this._name.Trim();
		}

		// Token: 0x04002EC0 RID: 11968
		private string _name;
	}
}

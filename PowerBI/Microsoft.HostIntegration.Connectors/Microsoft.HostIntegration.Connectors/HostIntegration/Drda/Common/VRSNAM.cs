using System;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000845 RID: 2117
	public class VRSNAM
	{
		// Token: 0x0600432F RID: 17199 RVA: 0x000E1530 File Offset: 0x000DF730
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x17001001 RID: 4097
		// (get) Token: 0x06004330 RID: 17200 RVA: 0x000E1530 File Offset: 0x000DF730
		// (set) Token: 0x06004331 RID: 17201 RVA: 0x000E1538 File Offset: 0x000DF738
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

		// Token: 0x06004332 RID: 17202 RVA: 0x000E1541 File Offset: 0x000DF741
		public void Reset()
		{
			this._name = null;
		}

		// Token: 0x06004333 RID: 17203 RVA: 0x000E154C File Offset: 0x000DF74C
		public void Read(DdmReader reader)
		{
			this.ReadAsync(reader, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004334 RID: 17204 RVA: 0x000E1574 File Offset: 0x000DF774
		public async Task ReadAsync(DdmReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			string text = await reader.ReadStringAsync(isAsync, cancellationToken);
			this._name = text;
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x000E15D4 File Offset: 0x000DF7D4
		public void Write(DdmWriter writer)
		{
			int num = this._name.Length;
			if (num < 255)
			{
				num = 255;
			}
			writer.WriteScalarHeader(CodePoint.RDBNAM, num);
			writer.WriteScalarPaddedString(this._name, num);
		}

		// Token: 0x04002F66 RID: 12134
		private string _name;
	}
}

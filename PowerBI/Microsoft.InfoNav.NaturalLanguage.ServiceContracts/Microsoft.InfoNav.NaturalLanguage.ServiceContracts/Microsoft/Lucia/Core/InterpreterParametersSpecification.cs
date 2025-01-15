using System;
using System.ComponentModel;
using Microsoft.InfoNav;

namespace Microsoft.Lucia.Core
{
	// Token: 0x0200009F RID: 159
	[ImmutableObject(true)]
	public sealed class InterpreterParametersSpecification
	{
		// Token: 0x060002EF RID: 751 RVA: 0x00006664 File Offset: 0x00004864
		public InterpreterParametersSpecification(string filePath)
		{
			Contract.CheckNonEmpty(filePath, "filePath");
			this._filePath = filePath;
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000667E File Offset: 0x0000487E
		public string FilePath
		{
			get
			{
				return this._filePath;
			}
		}

		// Token: 0x04000358 RID: 856
		private readonly string _filePath;
	}
}

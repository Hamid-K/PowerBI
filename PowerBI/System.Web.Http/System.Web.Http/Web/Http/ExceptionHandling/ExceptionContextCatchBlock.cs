using System;
using System.Diagnostics;

namespace System.Web.Http.ExceptionHandling
{
	// Token: 0x020000D4 RID: 212
	[DebuggerDisplay("Name: {Name}, IsTopLevel: {IsTopLevel}")]
	public class ExceptionContextCatchBlock
	{
		// Token: 0x06000585 RID: 1413 RVA: 0x0000E1AB File Offset: 0x0000C3AB
		public ExceptionContextCatchBlock(string name, bool isTopLevel, bool callsHandler)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this._name = name;
			this._isTopLevel = isTopLevel;
			this._callsHandler = callsHandler;
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x0000E1D6 File Offset: 0x0000C3D6
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000E1DE File Offset: 0x0000C3DE
		public bool IsTopLevel
		{
			get
			{
				return this._isTopLevel;
			}
		}

		// Token: 0x1700019D RID: 413
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000E1E6 File Offset: 0x0000C3E6
		public bool CallsHandler
		{
			get
			{
				return this._callsHandler;
			}
		}

		// Token: 0x06000589 RID: 1417 RVA: 0x0000E1D6 File Offset: 0x0000C3D6
		public override string ToString()
		{
			return this._name;
		}

		// Token: 0x0400013E RID: 318
		private readonly string _name;

		// Token: 0x0400013F RID: 319
		private readonly bool _isTopLevel;

		// Token: 0x04000140 RID: 320
		private readonly bool _callsHandler;
	}
}

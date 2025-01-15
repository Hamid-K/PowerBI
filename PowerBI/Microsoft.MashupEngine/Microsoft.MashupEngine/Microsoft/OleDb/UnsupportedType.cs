using System;

namespace Microsoft.OleDb
{
	// Token: 0x02001F32 RID: 7986
	public struct UnsupportedType
	{
		// Token: 0x0600C3A4 RID: 50084 RVA: 0x002730ED File Offset: 0x002712ED
		public UnsupportedType(string type)
		{
			this.type = type;
		}

		// Token: 0x17002FCB RID: 12235
		// (get) Token: 0x0600C3A5 RID: 50085 RVA: 0x002730F6 File Offset: 0x002712F6
		public string Value
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x040064A3 RID: 25763
		private string type;
	}
}

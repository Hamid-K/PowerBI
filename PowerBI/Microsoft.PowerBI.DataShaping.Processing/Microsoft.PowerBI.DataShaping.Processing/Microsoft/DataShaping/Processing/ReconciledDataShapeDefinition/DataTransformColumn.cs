using System;

namespace Microsoft.DataShaping.Processing.ReconciledDataShapeDefinition
{
	// Token: 0x02000039 RID: 57
	internal sealed class DataTransformColumn
	{
		// Token: 0x060001AB RID: 427 RVA: 0x00005AA6 File Offset: 0x00003CA6
		internal DataTransformColumn(string name, string role)
		{
			this._name = name;
			this._role = role;
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00005ABC File Offset: 0x00003CBC
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00005AC4 File Offset: 0x00003CC4
		internal string Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x04000106 RID: 262
		private readonly string _name;

		// Token: 0x04000107 RID: 263
		private readonly string _role;
	}
}

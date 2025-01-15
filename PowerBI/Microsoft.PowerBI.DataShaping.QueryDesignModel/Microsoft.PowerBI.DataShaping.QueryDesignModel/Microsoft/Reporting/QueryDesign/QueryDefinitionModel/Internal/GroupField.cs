using System;

namespace Microsoft.Reporting.QueryDesign.QueryDefinitionModel.Internal
{
	// Token: 0x020000DD RID: 221
	internal struct GroupField
	{
		// Token: 0x06000DC4 RID: 3524 RVA: 0x000233FC File Offset: 0x000215FC
		public GroupField(string name, GroupFieldType type)
		{
			this._name = ArgumentValidation.CheckNotNullOrEmpty(name, "name");
			this._type = type;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00023416 File Offset: 0x00021616
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x0002341E File Offset: 0x0002161E
		public GroupFieldType Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x040009A3 RID: 2467
		private readonly string _name;

		// Token: 0x040009A4 RID: 2468
		private readonly GroupFieldType _type;
	}
}

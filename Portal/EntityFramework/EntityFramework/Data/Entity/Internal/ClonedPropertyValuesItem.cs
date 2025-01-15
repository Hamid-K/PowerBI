using System;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000100 RID: 256
	internal class ClonedPropertyValuesItem : IPropertyValuesItem
	{
		// Token: 0x06001281 RID: 4737 RVA: 0x0003086A File Offset: 0x0002EA6A
		public ClonedPropertyValuesItem(string name, object value, Type type, bool isComplex)
		{
			this._name = name;
			this._type = type;
			this._isComplex = isComplex;
			this.Value = value;
		}

		// Token: 0x170004A7 RID: 1191
		// (get) Token: 0x06001282 RID: 4738 RVA: 0x0003088F File Offset: 0x0002EA8F
		// (set) Token: 0x06001283 RID: 4739 RVA: 0x00030897 File Offset: 0x0002EA97
		public object Value { get; set; }

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x06001284 RID: 4740 RVA: 0x000308A0 File Offset: 0x0002EAA0
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x06001285 RID: 4741 RVA: 0x000308A8 File Offset: 0x0002EAA8
		public bool IsComplex
		{
			get
			{
				return this._isComplex;
			}
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x06001286 RID: 4742 RVA: 0x000308B0 File Offset: 0x0002EAB0
		public Type Type
		{
			get
			{
				return this._type;
			}
		}

		// Token: 0x04000922 RID: 2338
		private readonly string _name;

		// Token: 0x04000923 RID: 2339
		private readonly bool _isComplex;

		// Token: 0x04000924 RID: 2340
		private readonly Type _type;
	}
}

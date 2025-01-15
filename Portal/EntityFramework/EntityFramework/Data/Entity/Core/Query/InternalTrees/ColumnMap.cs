using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Diagnostics;

namespace System.Data.Entity.Core.Query.InternalTrees
{
	// Token: 0x02000387 RID: 903
	internal abstract class ColumnMap
	{
		// Token: 0x06002BD2 RID: 11218 RVA: 0x0008DF59 File Offset: 0x0008C159
		internal ColumnMap(TypeUsage type, string name)
		{
			this._type = type;
			this._name = name;
		}

		// Token: 0x170008B2 RID: 2226
		// (get) Token: 0x06002BD3 RID: 11219 RVA: 0x0008DF6F File Offset: 0x0008C16F
		// (set) Token: 0x06002BD4 RID: 11220 RVA: 0x0008DF77 File Offset: 0x0008C177
		internal TypeUsage Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
			}
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x0008DF80 File Offset: 0x0008C180
		// (set) Token: 0x06002BD6 RID: 11222 RVA: 0x0008DF88 File Offset: 0x0008C188
		internal string Name
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

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x0008DF91 File Offset: 0x0008C191
		internal bool IsNamed
		{
			get
			{
				return this._name != null;
			}
		}

		// Token: 0x06002BD8 RID: 11224
		[DebuggerNonUserCode]
		internal abstract void Accept<TArgType>(ColumnMapVisitor<TArgType> visitor, TArgType arg);

		// Token: 0x06002BD9 RID: 11225
		[DebuggerNonUserCode]
		internal abstract TResultType Accept<TResultType, TArgType>(ColumnMapVisitorWithResults<TResultType, TArgType> visitor, TArgType arg);

		// Token: 0x04000EE7 RID: 3815
		private TypeUsage _type;

		// Token: 0x04000EE8 RID: 3816
		private string _name;

		// Token: 0x04000EE9 RID: 3817
		internal const string DefaultColumnName = "Value";
	}
}

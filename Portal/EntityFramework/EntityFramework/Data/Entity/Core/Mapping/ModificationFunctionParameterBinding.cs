using System;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Utilities;
using System.Globalization;

namespace System.Data.Entity.Core.Mapping
{
	// Token: 0x0200054F RID: 1359
	public sealed class ModificationFunctionParameterBinding : MappingItem
	{
		// Token: 0x060042B4 RID: 17076 RVA: 0x000E5922 File Offset: 0x000E3B22
		public ModificationFunctionParameterBinding(FunctionParameter parameter, ModificationFunctionMemberPath memberPath, bool isCurrent)
		{
			Check.NotNull<FunctionParameter>(parameter, "parameter");
			Check.NotNull<ModificationFunctionMemberPath>(memberPath, "memberPath");
			this._parameter = parameter;
			this._memberPath = memberPath;
			this._isCurrent = isCurrent;
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x060042B5 RID: 17077 RVA: 0x000E5957 File Offset: 0x000E3B57
		public FunctionParameter Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060042B6 RID: 17078 RVA: 0x000E595F File Offset: 0x000E3B5F
		public ModificationFunctionMemberPath MemberPath
		{
			get
			{
				return this._memberPath;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060042B7 RID: 17079 RVA: 0x000E5967 File Offset: 0x000E3B67
		public bool IsCurrent
		{
			get
			{
				return this._isCurrent;
			}
		}

		// Token: 0x060042B8 RID: 17080 RVA: 0x000E596F File Offset: 0x000E3B6F
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "@{0}->{1}{2}", new object[]
			{
				this.Parameter,
				this.IsCurrent ? "+" : "-",
				this.MemberPath
			});
		}

		// Token: 0x060042B9 RID: 17081 RVA: 0x000E59AF File Offset: 0x000E3BAF
		internal override void SetReadOnly()
		{
			MappingItem.SetReadOnly(this._memberPath);
			base.SetReadOnly();
		}

		// Token: 0x0400177E RID: 6014
		private readonly FunctionParameter _parameter;

		// Token: 0x0400177F RID: 6015
		private readonly ModificationFunctionMemberPath _memberPath;

		// Token: 0x04001780 RID: 6016
		private readonly bool _isCurrent;
	}
}

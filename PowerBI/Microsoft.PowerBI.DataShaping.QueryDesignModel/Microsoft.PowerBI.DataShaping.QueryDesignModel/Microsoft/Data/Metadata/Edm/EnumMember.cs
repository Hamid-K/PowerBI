using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200008D RID: 141
	internal sealed class EnumMember : MetadataItem
	{
		// Token: 0x06000A37 RID: 2615 RVA: 0x000184B8 File Offset: 0x000166B8
		internal EnumMember(string name)
			: base(MetadataItem.MetadataFlags.Readonly)
		{
			EntityUtil.CheckStringArgument(name, "name");
			this._name = name;
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x06000A38 RID: 2616 RVA: 0x000184D3 File Offset: 0x000166D3
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.EnumMember;
			}
		}

		// Token: 0x170003AE RID: 942
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x000184D7 File Offset: 0x000166D7
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x170003AF RID: 943
		// (get) Token: 0x06000A3A RID: 2618 RVA: 0x000184DF File Offset: 0x000166DF
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000184E7 File Offset: 0x000166E7
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x04000832 RID: 2098
		private readonly string _name;
	}
}

using System;
using System.Data;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x02000083 RID: 131
	public abstract class EdmMember : MetadataItem
	{
		// Token: 0x060009CB RID: 2507 RVA: 0x00017800 File Offset: 0x00015A00
		internal EdmMember(string name, TypeUsage memberTypeUsage)
		{
			EntityUtil.CheckStringArgument(name, "name");
			EntityUtil.GenericCheckArgumentNull<TypeUsage>(memberTypeUsage, "memberTypeUsage");
			this._name = name;
			this._typeUsage = memberTypeUsage;
		}

		// Token: 0x1700037E RID: 894
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x0001782D File Offset: 0x00015A2D
		internal override string Identity
		{
			get
			{
				return this.Name;
			}
		}

		// Token: 0x1700037F RID: 895
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x00017835 File Offset: 0x00015A35
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000380 RID: 896
		// (get) Token: 0x060009CE RID: 2510 RVA: 0x0001783D File Offset: 0x00015A3D
		public StructuralType DeclaringType
		{
			get
			{
				return this._declaringType;
			}
		}

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x00017845 File Offset: 0x00015A45
		[MetadataProperty(BuiltInTypeKind.TypeUsage, false)]
		public TypeUsage TypeUsage
		{
			get
			{
				return this._typeUsage;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x0001784D File Offset: 0x00015A4D
		public override string ToString()
		{
			return this.Name;
		}

		// Token: 0x060009D1 RID: 2513 RVA: 0x00017855 File Offset: 0x00015A55
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
			}
		}

		// Token: 0x060009D2 RID: 2514 RVA: 0x00017865 File Offset: 0x00015A65
		internal void ChangeDeclaringTypeWithoutCollectionFixup(StructuralType newDeclaringType)
		{
			this._declaringType = newDeclaringType;
		}

		// Token: 0x04000810 RID: 2064
		private TypeUsage _typeUsage;

		// Token: 0x04000811 RID: 2065
		private string _name;

		// Token: 0x04000812 RID: 2066
		private StructuralType _declaringType;
	}
}

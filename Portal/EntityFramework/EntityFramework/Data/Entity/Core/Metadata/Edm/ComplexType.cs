using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000491 RID: 1169
	public class ComplexType : StructuralType
	{
		// Token: 0x060039CC RID: 14796 RVA: 0x000BE5DD File Offset: 0x000BC7DD
		internal ComplexType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x060039CD RID: 14797 RVA: 0x000BE5E8 File Offset: 0x000BC7E8
		internal ComplexType()
		{
		}

		// Token: 0x060039CE RID: 14798 RVA: 0x000BE5F0 File Offset: 0x000BC7F0
		internal ComplexType(string name)
			: this(name, "Transient", DataSpace.CSpace)
		{
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x060039CF RID: 14799 RVA: 0x000BE5FF File Offset: 0x000BC7FF
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x060039D0 RID: 14800 RVA: 0x000BE602 File Offset: 0x000BC802
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				return new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty));
			}
		}

		// Token: 0x060039D1 RID: 14801 RVA: 0x000BE61B File Offset: 0x000BC81B
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x060039D2 RID: 14802 RVA: 0x000BE620 File Offset: 0x000BC820
		public static ComplexType Create(string name, string namespaceName, DataSpace dataSpace, IEnumerable<EdmMember> members, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotEmpty(name, "name");
			Check.NotEmpty(namespaceName, "namespaceName");
			Check.NotNull<IEnumerable<EdmMember>>(members, "members");
			ComplexType complexType = new ComplexType(name, namespaceName, dataSpace);
			foreach (EdmMember edmMember in members)
			{
				complexType.AddMember(edmMember);
			}
			if (metadataProperties != null)
			{
				complexType.AddMetadataProperties(metadataProperties);
			}
			complexType.SetReadOnly();
			return complexType;
		}
	}
}

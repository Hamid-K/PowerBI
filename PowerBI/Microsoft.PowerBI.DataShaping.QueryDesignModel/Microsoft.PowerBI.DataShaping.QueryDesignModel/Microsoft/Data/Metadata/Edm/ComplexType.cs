using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x0200007B RID: 123
	public class ComplexType : StructuralType
	{
		// Token: 0x06000957 RID: 2391 RVA: 0x00015775 File Offset: 0x00013975
		internal ComplexType(string name, string namespaceName, DataSpace dataSpace)
			: base(name, namespaceName, dataSpace)
		{
		}

		// Token: 0x06000958 RID: 2392 RVA: 0x00015780 File Offset: 0x00013980
		internal ComplexType()
		{
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06000959 RID: 2393 RVA: 0x00015788 File Offset: 0x00013988
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.ComplexType;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x0600095A RID: 2394 RVA: 0x0001578C File Offset: 0x0001398C
		public ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					ReadOnlyMetadataCollection<EdmMember> members = base.Members;
					Predicate<EdmMember> predicate;
					if ((predicate = ComplexType.<>O.<0>__IsEdmProperty) == null)
					{
						predicate = (ComplexType.<>O.<0>__IsEdmProperty = new Predicate<EdmMember>(Helper.IsEdmProperty));
					}
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(members, predicate), null);
				}
				return this._properties;
			}
		}

		// Token: 0x0600095B RID: 2395 RVA: 0x000157DA File Offset: 0x000139DA
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x04000765 RID: 1893
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x020002B4 RID: 692
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FA5 RID: 4005
			public static Predicate<EdmMember> <0>__IsEdmProperty;
		}
	}
}

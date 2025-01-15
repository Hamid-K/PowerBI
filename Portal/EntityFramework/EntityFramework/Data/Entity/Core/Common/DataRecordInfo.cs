using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Core.Common
{
	// Token: 0x020005E8 RID: 1512
	public class DataRecordInfo
	{
		// Token: 0x060049D8 RID: 18904 RVA: 0x00106052 File Offset: 0x00104252
		internal DataRecordInfo()
		{
		}

		// Token: 0x060049D9 RID: 18905 RVA: 0x0010605C File Offset: 0x0010425C
		public DataRecordInfo(TypeUsage metadata, IEnumerable<EdmMember> memberInfo)
		{
			Check.NotNull<TypeUsage>(metadata, "metadata");
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(metadata.EdmType);
			List<FieldMetadata> list = new List<FieldMetadata>(allStructuralMembers.Count);
			if (memberInfo != null)
			{
				foreach (EdmMember edmMember in memberInfo)
				{
					if (edmMember == null || 0 > allStructuralMembers.IndexOf(edmMember) || (BuiltInTypeKind.EdmProperty != edmMember.BuiltInTypeKind && edmMember.BuiltInTypeKind != BuiltInTypeKind.AssociationEndMember))
					{
						throw Error.InvalidEdmMemberInstance();
					}
					if (edmMember.DeclaringType != metadata.EdmType && !edmMember.DeclaringType.IsBaseTypeOf(metadata.EdmType))
					{
						throw new ArgumentException(Strings.EdmMembersDefiningTypeDoNotAgreeWithMetadataType);
					}
					list.Add(new FieldMetadata(list.Count, edmMember));
				}
			}
			if (Helper.IsStructuralType(metadata.EdmType) == 0 < list.Count)
			{
				this._fieldMetadata = new ReadOnlyCollection<FieldMetadata>(list);
				this._metadata = metadata;
				return;
			}
			throw Error.InvalidEdmMemberInstance();
		}

		// Token: 0x060049DA RID: 18906 RVA: 0x00106164 File Offset: 0x00104364
		internal DataRecordInfo(TypeUsage metadata)
		{
			IBaseList<EdmMember> allStructuralMembers = TypeHelpers.GetAllStructuralMembers(metadata);
			FieldMetadata[] array = new FieldMetadata[allStructuralMembers.Count];
			for (int i = 0; i < array.Length; i++)
			{
				EdmMember edmMember = allStructuralMembers[i];
				array[i] = new FieldMetadata(i, edmMember);
			}
			this._fieldMetadata = new ReadOnlyCollection<FieldMetadata>(array);
			this._metadata = metadata;
		}

		// Token: 0x060049DB RID: 18907 RVA: 0x001061C1 File Offset: 0x001043C1
		internal DataRecordInfo(DataRecordInfo recordInfo)
		{
			this._fieldMetadata = recordInfo._fieldMetadata;
			this._metadata = recordInfo._metadata;
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060049DC RID: 18908 RVA: 0x001061E1 File Offset: 0x001043E1
		public ReadOnlyCollection<FieldMetadata> FieldMetadata
		{
			get
			{
				return this._fieldMetadata;
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060049DD RID: 18909 RVA: 0x001061E9 File Offset: 0x001043E9
		public virtual TypeUsage RecordType
		{
			get
			{
				return this._metadata;
			}
		}

		// Token: 0x04001A0C RID: 6668
		private readonly ReadOnlyCollection<FieldMetadata> _fieldMetadata;

		// Token: 0x04001A0D RID: 6669
		private readonly TypeUsage _metadata;
	}
}

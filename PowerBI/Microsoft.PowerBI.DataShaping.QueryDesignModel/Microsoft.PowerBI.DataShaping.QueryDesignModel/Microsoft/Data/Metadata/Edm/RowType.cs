using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000AE RID: 174
	public sealed class RowType : StructuralType
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0001D430 File Offset: 0x0001B630
		internal RowType(IEnumerable<EdmProperty> properties)
			: base(RowType.GetRowTypeIdentityFromProperties(RowType.CheckProperties(properties)), "Transient", (DataSpace)(-1))
		{
			if (properties != null)
			{
				foreach (EdmProperty edmProperty in properties)
				{
					this.AddProperty(edmProperty);
				}
			}
			this.SetReadOnly();
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0001D498 File Offset: 0x0001B698
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RowType;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000B76 RID: 2934 RVA: 0x0001D49C File Offset: 0x0001B69C
		public ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					ReadOnlyMetadataCollection<EdmMember> members = base.Members;
					Predicate<EdmMember> predicate;
					if ((predicate = RowType.<>O.<0>__IsEdmProperty) == null)
					{
						predicate = (RowType.<>O.<0>__IsEdmProperty = new Predicate<EdmMember>(Helper.IsEdmProperty));
					}
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(members, predicate), null);
				}
				return this._properties;
			}
		}

		// Token: 0x06000B77 RID: 2935 RVA: 0x0001D4EA File Offset: 0x0001B6EA
		private void AddProperty(EdmProperty property)
		{
			EntityUtil.GenericCheckArgumentNull<EdmProperty>(property, "property");
			base.AddMember(property);
		}

		// Token: 0x06000B78 RID: 2936 RVA: 0x0001D4FF File Offset: 0x0001B6FF
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06000B79 RID: 2937 RVA: 0x0001D504 File Offset: 0x0001B704
		private static string GetRowTypeIdentityFromProperties(IEnumerable<EdmProperty> properties)
		{
			StringBuilder stringBuilder = new StringBuilder("rowtype[");
			if (properties != null)
			{
				int num = 0;
				foreach (EdmProperty edmProperty in properties)
				{
					if (num > 0)
					{
						stringBuilder.Append(",");
					}
					stringBuilder.Append("(");
					stringBuilder.Append(edmProperty.Name);
					stringBuilder.Append(",");
					edmProperty.TypeUsage.BuildIdentity(stringBuilder);
					stringBuilder.Append(")");
					num++;
				}
			}
			stringBuilder.Append("]");
			return stringBuilder.ToString();
		}

		// Token: 0x06000B7A RID: 2938 RVA: 0x0001D5B8 File Offset: 0x0001B7B8
		private static IEnumerable<EdmProperty> CheckProperties(IEnumerable<EdmProperty> properties)
		{
			if (properties != null)
			{
				using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw EntityUtil.CollectionParameterElementIsNull("properties");
						}
					}
				}
			}
			return properties;
		}

		// Token: 0x06000B7B RID: 2939 RVA: 0x0001D60C File Offset: 0x0001B80C
		internal override bool EdmEquals(MetadataItem item)
		{
			if (this == item)
			{
				return true;
			}
			if (item == null || BuiltInTypeKind.RowType != item.BuiltInTypeKind)
			{
				return false;
			}
			RowType rowType = (RowType)item;
			if (base.Members.Count != rowType.Members.Count)
			{
				return false;
			}
			for (int i = 0; i < base.Members.Count; i++)
			{
				EdmMember edmMember = base.Members[i];
				EdmMember edmMember2 = rowType.Members[i];
				if (!edmMember.EdmEquals(edmMember2) || !edmMember.TypeUsage.EdmEquals(edmMember2.TypeUsage))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x040008AE RID: 2222
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x020002C6 RID: 710
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x04000FDD RID: 4061
			public static Predicate<EdmMember> <0>__IsEdmProperty;
		}
	}
}

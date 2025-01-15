using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.ELinq;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F6 RID: 1270
	public class RowType : StructuralType
	{
		// Token: 0x06003EE0 RID: 16096 RVA: 0x000D113D File Offset: 0x000CF33D
		internal RowType()
		{
		}

		// Token: 0x06003EE1 RID: 16097 RVA: 0x000D1145 File Offset: 0x000CF345
		internal RowType(IEnumerable<EdmProperty> properties)
			: this(properties, null)
		{
		}

		// Token: 0x06003EE2 RID: 16098 RVA: 0x000D1150 File Offset: 0x000CF350
		internal RowType(IEnumerable<EdmProperty> properties, InitializerMetadata initializerMetadata)
			: base(RowType.GetRowTypeIdentityFromProperties(RowType.CheckProperties(properties), initializerMetadata), "Transient", (DataSpace)(-1))
		{
			if (properties != null)
			{
				foreach (EdmProperty edmProperty in properties)
				{
					this.AddProperty(edmProperty);
				}
			}
			this._initializerMetadata = initializerMetadata;
			this.SetReadOnly();
		}

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06003EE3 RID: 16099 RVA: 0x000D11C0 File Offset: 0x000CF3C0
		internal InitializerMetadata InitializerMetadata
		{
			get
			{
				return this._initializerMetadata;
			}
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06003EE4 RID: 16100 RVA: 0x000D11C8 File Offset: 0x000CF3C8
		public override BuiltInTypeKind BuiltInTypeKind
		{
			get
			{
				return BuiltInTypeKind.RowType;
			}
		}

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06003EE5 RID: 16101 RVA: 0x000D11CC File Offset: 0x000CF3CC
		public virtual ReadOnlyMetadataCollection<EdmProperty> Properties
		{
			get
			{
				if (this._properties == null)
				{
					Interlocked.CompareExchange<ReadOnlyMetadataCollection<EdmProperty>>(ref this._properties, new FilteredReadOnlyMetadataCollection<EdmProperty, EdmMember>(base.Members, new Predicate<EdmMember>(Helper.IsEdmProperty)), null);
				}
				return this._properties;
			}
		}

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06003EE6 RID: 16102 RVA: 0x000D1200 File Offset: 0x000CF400
		public ReadOnlyMetadataCollection<EdmProperty> DeclaredProperties
		{
			get
			{
				return base.GetDeclaredOnlyMembers<EdmProperty>();
			}
		}

		// Token: 0x06003EE7 RID: 16103 RVA: 0x000D1208 File Offset: 0x000CF408
		private void AddProperty(EdmProperty property)
		{
			Check.NotNull<EdmProperty>(property, "property");
			base.AddMember(property);
		}

		// Token: 0x06003EE8 RID: 16104 RVA: 0x000D121D File Offset: 0x000CF41D
		internal override void ValidateMemberForAdd(EdmMember member)
		{
		}

		// Token: 0x06003EE9 RID: 16105 RVA: 0x000D1220 File Offset: 0x000CF420
		private static string GetRowTypeIdentityFromProperties(IEnumerable<EdmProperty> properties, InitializerMetadata initializerMetadata)
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
			if (initializerMetadata != null)
			{
				stringBuilder.Append(",").Append(initializerMetadata.Identity);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06003EEA RID: 16106 RVA: 0x000D12F0 File Offset: 0x000CF4F0
		private static IEnumerable<EdmProperty> CheckProperties(IEnumerable<EdmProperty> properties)
		{
			if (properties != null)
			{
				int num = 0;
				using (IEnumerator<EdmProperty> enumerator = properties.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current == null)
						{
							throw new ArgumentException(Strings.ADP_CollectionParameterElementIsNull("properties"));
						}
						num++;
					}
				}
			}
			return properties;
		}

		// Token: 0x06003EEB RID: 16107 RVA: 0x000D134C File Offset: 0x000CF54C
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

		// Token: 0x06003EEC RID: 16108 RVA: 0x000D13E0 File Offset: 0x000CF5E0
		public static RowType Create(IEnumerable<EdmProperty> properties, IEnumerable<MetadataProperty> metadataProperties)
		{
			Check.NotNull<IEnumerable<EdmProperty>>(properties, "properties");
			RowType rowType = new RowType(properties);
			if (metadataProperties != null)
			{
				rowType.AddMetadataProperties(metadataProperties);
			}
			rowType.SetReadOnly();
			return rowType;
		}

		// Token: 0x04001578 RID: 5496
		private ReadOnlyMetadataCollection<EdmProperty> _properties;

		// Token: 0x04001579 RID: 5497
		private readonly InitializerMetadata _initializerMetadata;
	}
}

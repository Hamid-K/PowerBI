using System;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Navigation
{
	// Token: 0x02001171 RID: 4465
	public class HierarchyItem
	{
		// Token: 0x0600750C RID: 29964 RVA: 0x00191761 File Offset: 0x0018F961
		public HierarchyItem(string name, TextValue kind, Value description)
		{
			this.name = name;
			this.kind = kind;
			this.description = description;
		}

		// Token: 0x0600750D RID: 29965 RVA: 0x0019177E File Offset: 0x0018F97E
		public HierarchyItem(string name, TableType type, Value description)
		{
			this.name = name;
			this.type = type;
			this.kind = HierarchyItem.KindWithLeafMetadata(type.Kind, LogicalValue.True);
			this.description = description;
		}

		// Token: 0x17002081 RID: 8321
		// (get) Token: 0x0600750E RID: 29966 RVA: 0x001917B1 File Offset: 0x0018F9B1
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002082 RID: 8322
		// (get) Token: 0x0600750F RID: 29967 RVA: 0x001917B9 File Offset: 0x0018F9B9
		public TableType TableType
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x06007510 RID: 29968 RVA: 0x001917C1 File Offset: 0x0018F9C1
		public TextValue Kind
		{
			get
			{
				return this.kind;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x06007511 RID: 29969 RVA: 0x001917C9 File Offset: 0x0018F9C9
		public Value Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06007512 RID: 29970 RVA: 0x001917D1 File Offset: 0x0018F9D1
		public Restriction Restriction
		{
			get
			{
				return Restriction.To(this.Name);
			}
		}

		// Token: 0x06007513 RID: 29971 RVA: 0x001917DE File Offset: 0x0018F9DE
		public override int GetHashCode()
		{
			return ((this.Name == null) ? 139 : this.Name.GetHashCode()) ^ (this.Kind.GetHashCode() << 3);
		}

		// Token: 0x06007514 RID: 29972 RVA: 0x00191808 File Offset: 0x0018FA08
		private static TextValue KindWithLeafMetadata(string kind, LogicalValue isLeaf)
		{
			return TextValue.New(kind).NewMeta(RecordValue.New(new NamedValue[]
			{
				new NamedValue("NavigationTable.IsLeaf", isLeaf)
			})).AsText;
		}

		// Token: 0x04004053 RID: 16467
		public static readonly TextValue DatabaseKindValue = HierarchyItem.KindWithLeafMetadata("Database", LogicalValue.False);

		// Token: 0x04004054 RID: 16468
		public static readonly TextValue SchemaKindValue = HierarchyItem.KindWithLeafMetadata("Schema", LogicalValue.False);

		// Token: 0x04004055 RID: 16469
		public static readonly TextValue TableKindValue = HierarchyItem.KindWithLeafMetadata("Table", LogicalValue.True);

		// Token: 0x04004056 RID: 16470
		public static readonly TextValue ViewKindValue = HierarchyItem.KindWithLeafMetadata("View", LogicalValue.True);

		// Token: 0x04004057 RID: 16471
		private readonly string name;

		// Token: 0x04004058 RID: 16472
		private readonly TextValue kind;

		// Token: 0x04004059 RID: 16473
		private readonly TableType type;

		// Token: 0x0400405A RID: 16474
		private readonly Value description;
	}
}

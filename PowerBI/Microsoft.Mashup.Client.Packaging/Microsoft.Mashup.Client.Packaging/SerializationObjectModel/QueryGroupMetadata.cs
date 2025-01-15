using System;
using Microsoft.Mashup.Client.Packaging.BinarySerialization;

namespace Microsoft.Mashup.Client.Packaging.SerializationObjectModel
{
	// Token: 0x0200000D RID: 13
	public class QueryGroupMetadata : IBinarySerializable
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000028 RID: 40 RVA: 0x00002930 File Offset: 0x00000B30
		// (set) Token: 0x06000029 RID: 41 RVA: 0x00002938 File Offset: 0x00000B38
		public string Description
		{
			get
			{
				return this.description;
			}
			set
			{
				this.description = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002A RID: 42 RVA: 0x00002941 File Offset: 0x00000B41
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002949 File Offset: 0x00000B49
		public Guid Id
		{
			get
			{
				return this.id;
			}
			set
			{
				this.id = value;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002C RID: 44 RVA: 0x00002952 File Offset: 0x00000B52
		// (set) Token: 0x0600002D RID: 45 RVA: 0x0000295A File Offset: 0x00000B5A
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002E RID: 46 RVA: 0x00002963 File Offset: 0x00000B63
		// (set) Token: 0x0600002F RID: 47 RVA: 0x0000296B File Offset: 0x00000B6B
		public int Order
		{
			get
			{
				return this.order;
			}
			set
			{
				this.order = value;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00002974 File Offset: 0x00000B74
		// (set) Token: 0x06000031 RID: 49 RVA: 0x0000297C File Offset: 0x00000B7C
		public Guid? ParentId
		{
			get
			{
				return this.parentId;
			}
			set
			{
				this.parentId = value;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002988 File Offset: 0x00000B88
		public void Deserialize(BinarySerializationReader reader)
		{
			int num = reader.ReadInt();
			this.id = reader.ReadGuid();
			this.name = reader.ReadString();
			this.description = reader.ReadString();
			bool flag = reader.ReadBool();
			if (flag)
			{
				this.parentId = new Guid?(reader.ReadGuid());
			}
			this.order = reader.ReadInt();
		}

		// Token: 0x06000033 RID: 51 RVA: 0x000029E8 File Offset: 0x00000BE8
		public void Serialize(BinarySerializationWriter writer)
		{
			writer.WriteInt(0);
			writer.WriteGuid(this.id);
			writer.WriteString(this.name);
			writer.WriteString(this.description);
			writer.WriteBool(this.parentId != null);
			if (this.parentId != null)
			{
				writer.WriteGuid(this.parentId.Value);
			}
			writer.WriteInt(this.order);
		}

		// Token: 0x04000045 RID: 69
		private string description;

		// Token: 0x04000046 RID: 70
		private Guid id;

		// Token: 0x04000047 RID: 71
		private string name;

		// Token: 0x04000048 RID: 72
		private int order;

		// Token: 0x04000049 RID: 73
		private Guid? parentId;
	}
}

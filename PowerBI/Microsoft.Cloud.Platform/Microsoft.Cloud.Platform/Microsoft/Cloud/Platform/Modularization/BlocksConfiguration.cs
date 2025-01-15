using System;
using System.Configuration;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000C9 RID: 201
	[ConfigurationCollection(typeof(BlocksConfiguration), AddItemName = "Block", CollectionType = ConfigurationElementCollectionType.AddRemoveClearMap)]
	internal class BlocksConfiguration : ConfigurationElementCollection
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x000034FD File Offset: 0x000016FD
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x00014A94 File Offset: 0x00012C94
		protected override string ElementName
		{
			get
			{
				return "Block";
			}
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00014A9B File Offset: 0x00012C9B
		protected override ConfigurationElement CreateNewElement()
		{
			return new BlockConfiguration();
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00014AA2 File Offset: 0x00012CA2
		protected override object GetElementKey(ConfigurationElement element)
		{
			return (element as BlockConfiguration).Name;
		}

		// Token: 0x170000EB RID: 235
		public BlockConfiguration this[int index]
		{
			get
			{
				return (BlockConfiguration)base.BaseGet(index);
			}
		}
	}
}

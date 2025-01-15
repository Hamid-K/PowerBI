using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine1.Library.Cube;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000D9C RID: 3484
	internal class CdpaHierarchyLevel : CdpaDimensionAttribute
	{
		// Token: 0x06005EE6 RID: 24294 RVA: 0x00147B4C File Offset: 0x00145D4C
		public CdpaHierarchyLevel(CdpaHierarchy hierarchy, int level, string name, string caption, CdpaDimensionAttribute attribute)
			: base(hierarchy.Dimension)
		{
			this.hierarchy = hierarchy;
			this.attribute = attribute;
			this.level = level;
			this.name = QualifiedName.New(this.hierarchy.Name).Qualify(name).AsString;
			this.caption = caption;
			this.qualifiedName = this.hierarchy.QualifiedName.Qualify(name);
		}

		// Token: 0x17001BFF RID: 7167
		// (get) Token: 0x06005EE7 RID: 24295 RVA: 0x00147BBB File Offset: 0x00145DBB
		public override ICubeHierarchy Hierarchy
		{
			get
			{
				return this.hierarchy;
			}
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x06005EE8 RID: 24296 RVA: 0x00147BC3 File Offset: 0x00145DC3
		public override int Number
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17001C01 RID: 7169
		// (get) Token: 0x06005EE9 RID: 24297 RVA: 0x00147BCB File Offset: 0x00145DCB
		public override QualifiedName QualifiedName
		{
			get
			{
				return this.qualifiedName;
			}
		}

		// Token: 0x17001C02 RID: 7170
		// (get) Token: 0x06005EEA RID: 24298 RVA: 0x00147BD3 File Offset: 0x00145DD3
		public override string PropertyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17001C03 RID: 7171
		// (get) Token: 0x06005EEB RID: 24299 RVA: 0x00147BDB File Offset: 0x00145DDB
		public override string Caption
		{
			get
			{
				return this.caption;
			}
		}

		// Token: 0x17001C04 RID: 7172
		// (get) Token: 0x06005EEC RID: 24300 RVA: 0x00147BE3 File Offset: 0x00145DE3
		public override TypeValue Type
		{
			get
			{
				if (this.typeWithHierarchies == null)
				{
					this.typeWithHierarchies = this.AddHierarchyMetadata(this.attribute.Type);
				}
				return this.typeWithHierarchies;
			}
		}

		// Token: 0x17001C05 RID: 7173
		// (get) Token: 0x06005EED RID: 24301 RVA: 0x00147C0A File Offset: 0x00145E0A
		public CdpaDimensionAttribute Attribute
		{
			get
			{
				return this.attribute;
			}
		}

		// Token: 0x06005EEE RID: 24302 RVA: 0x00147C14 File Offset: 0x00145E14
		private TypeValue AddHierarchyMetadata(TypeValue type)
		{
			IList<CdpaHierarchyLevel> list;
			if (this.hierarchy.Dimension.Cube.AttributeLevels.TryGetValue(this.attribute, out list))
			{
				CubeHierarchiesMetadata.HierarchyInfo[] array = new CubeHierarchiesMetadata.HierarchyInfo[list.Count];
				for (int i = 0; i < array.Length; i++)
				{
					CdpaHierarchyLevel cdpaHierarchyLevel = list[i];
					CdpaHierarchy cdpaHierarchy = (CdpaHierarchy)cdpaHierarchyLevel.Hierarchy;
					array[i] = new CubeHierarchiesMetadata.HierarchyInfo
					{
						hierarchyId = cdpaHierarchy.QualifiedName.AsString,
						hierarchyCaption = cdpaHierarchy.Caption,
						dimensionId = cdpaHierarchy.Dimension.QualifiedName.AsString,
						dimensionCaption = cdpaHierarchy.Dimension.Caption,
						level = cdpaHierarchyLevel.Number,
						levelCaption = cdpaHierarchyLevel.Caption
					};
				}
				type = CubeHierarchiesMetadata.AddHierarchies(type, array);
			}
			return type;
		}

		// Token: 0x04003412 RID: 13330
		private readonly CdpaHierarchy hierarchy;

		// Token: 0x04003413 RID: 13331
		private readonly CdpaDimensionAttribute attribute;

		// Token: 0x04003414 RID: 13332
		private readonly int level;

		// Token: 0x04003415 RID: 13333
		private readonly string name;

		// Token: 0x04003416 RID: 13334
		private readonly string caption;

		// Token: 0x04003417 RID: 13335
		private readonly QualifiedName qualifiedName;

		// Token: 0x04003418 RID: 13336
		private TypeValue typeWithHierarchies;
	}
}

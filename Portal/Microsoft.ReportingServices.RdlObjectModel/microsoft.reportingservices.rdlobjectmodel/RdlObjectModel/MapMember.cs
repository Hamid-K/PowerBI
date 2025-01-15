using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001AF RID: 431
	public class MapMember : HierarchyMember, IHierarchy, IHierarchyMember
	{
		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000E2D RID: 3629 RVA: 0x0002307A File Offset: 0x0002127A
		// (set) Token: 0x06000E2E RID: 3630 RVA: 0x0002308D File Offset: 0x0002128D
		public override Group Group
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(0);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Group");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000E2F RID: 3631 RVA: 0x000230AA File Offset: 0x000212AA
		// (set) Token: 0x06000E30 RID: 3632 RVA: 0x000230BD File Offset: 0x000212BD
		[XmlElement("MapMember")]
		public MapMember ChildMapMember
		{
			get
			{
				return (MapMember)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000E31 RID: 3633 RVA: 0x000230CC File Offset: 0x000212CC
		// (set) Token: 0x06000E32 RID: 3634 RVA: 0x000230CF File Offset: 0x000212CF
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x000230D1 File Offset: 0x000212D1
		public MapMember()
		{
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x000230D9 File Offset: 0x000212D9
		internal MapMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000230E2 File Offset: 0x000212E2
		public override void Initialize()
		{
			base.Initialize();
			this.Group = new Group();
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000E36 RID: 3638 RVA: 0x000230F5 File Offset: 0x000212F5
		IEnumerable<IHierarchyMember> IHierarchyMember.Members
		{
			get
			{
				yield return this.ChildMapMember;
				yield break;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x00023105 File Offset: 0x00021305
		IEnumerable<IHierarchyMember> IHierarchy.Members
		{
			get
			{
				yield return this.ChildMapMember;
				yield break;
			}
		}

		// Token: 0x020003DB RID: 987
		internal class Definition : DefinitionStore<MapMember, MapMember.Definition.Properties>
		{
			// Token: 0x0600187F RID: 6271 RVA: 0x0003B779 File Offset: 0x00039979
			private Definition()
			{
			}

			// Token: 0x020004F3 RID: 1267
			internal enum Properties
			{
				// Token: 0x04001046 RID: 4166
				Group,
				// Token: 0x04001047 RID: 4167
				ChildMapMember,
				// Token: 0x04001048 RID: 4168
				PropertyCount
			}
		}
	}
}

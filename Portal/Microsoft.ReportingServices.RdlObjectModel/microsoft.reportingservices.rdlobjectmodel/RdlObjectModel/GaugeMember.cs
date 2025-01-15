using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200014A RID: 330
	public class GaugeMember : HierarchyMember, IHierarchy, IHierarchyMember
	{
		// Token: 0x1700032B RID: 811
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001DD95 File Offset: 0x0001BF95
		// (set) Token: 0x0600095E RID: 2398 RVA: 0x0001DDA8 File Offset: 0x0001BFA8
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
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700032C RID: 812
		// (get) Token: 0x0600095F RID: 2399 RVA: 0x0001DDC5 File Offset: 0x0001BFC5
		// (set) Token: 0x06000960 RID: 2400 RVA: 0x0001DDD8 File Offset: 0x0001BFD8
		[XmlElement(typeof(RdlCollection<SortExpression>))]
		public IList<SortExpression> SortExpressions
		{
			get
			{
				return (IList<SortExpression>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000961 RID: 2401 RVA: 0x0001DDE7 File Offset: 0x0001BFE7
		// (set) Token: 0x06000962 RID: 2402 RVA: 0x0001DDFA File Offset: 0x0001BFFA
		[XmlElement("GaugeMember")]
		public GaugeMember ChildGaugeMember
		{
			get
			{
				return (GaugeMember)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000963 RID: 2403 RVA: 0x0001DE09 File Offset: 0x0001C009
		public GaugeMember()
		{
		}

		// Token: 0x06000964 RID: 2404 RVA: 0x0001DE11 File Offset: 0x0001C011
		internal GaugeMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000965 RID: 2405 RVA: 0x0001DE1A File Offset: 0x0001C01A
		public override void Initialize()
		{
			base.Initialize();
			this.Group = new Group();
			this.SortExpressions = new RdlCollection<SortExpression>();
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000966 RID: 2406 RVA: 0x0001DE38 File Offset: 0x0001C038
		IEnumerable<IHierarchyMember> IHierarchyMember.Members
		{
			get
			{
				yield return this.ChildGaugeMember;
				yield break;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000967 RID: 2407 RVA: 0x0001DE48 File Offset: 0x0001C048
		IEnumerable<IHierarchyMember> IHierarchy.Members
		{
			get
			{
				yield return this.ChildGaugeMember;
				yield break;
			}
		}

		// Token: 0x02000379 RID: 889
		internal class Definition : DefinitionStore<GaugeMember, GaugeMember.Definition.Properties>
		{
			// Token: 0x0600180E RID: 6158 RVA: 0x0003B28C File Offset: 0x0003948C
			private Definition()
			{
			}

			// Token: 0x02000494 RID: 1172
			internal enum Properties
			{
				// Token: 0x04000BE4 RID: 3044
				Group,
				// Token: 0x04000BE5 RID: 3045
				SortExpressions,
				// Token: 0x04000BE6 RID: 3046
				ChildGaugeMember,
				// Token: 0x04000BE7 RID: 3047
				PropertyCount
			}
		}
	}
}

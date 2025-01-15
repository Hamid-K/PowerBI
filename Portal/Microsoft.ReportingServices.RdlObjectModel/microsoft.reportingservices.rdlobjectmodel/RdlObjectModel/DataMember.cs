using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B2 RID: 178
	public class DataMember : HierarchyMember, IHierarchyMember
	{
		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0001B703 File Offset: 0x00019903
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0001B716 File Offset: 0x00019916
		public override Group Group
		{
			get
			{
				return (Group)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0001B725 File Offset: 0x00019925
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0001B738 File Offset: 0x00019938
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

		// Token: 0x17000273 RID: 627
		// (get) Token: 0x06000784 RID: 1924 RVA: 0x0001B747 File Offset: 0x00019947
		// (set) Token: 0x06000785 RID: 1925 RVA: 0x0001B75A File Offset: 0x0001995A
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000274 RID: 628
		// (get) Token: 0x06000786 RID: 1926 RVA: 0x0001B769 File Offset: 0x00019969
		// (set) Token: 0x06000787 RID: 1927 RVA: 0x0001B77C File Offset: 0x0001997C
		[XmlElement(typeof(RdlCollection<DataMember>))]
		public IList<DataMember> DataMembers
		{
			get
			{
				return (IList<DataMember>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x06000788 RID: 1928 RVA: 0x0001B78B File Offset: 0x0001998B
		public DataMember()
		{
		}

		// Token: 0x06000789 RID: 1929 RVA: 0x0001B793 File Offset: 0x00019993
		internal DataMember(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600078A RID: 1930 RVA: 0x0001B79C File Offset: 0x0001999C
		public override void Initialize()
		{
			base.Initialize();
			this.SortExpressions = new RdlCollection<SortExpression>();
			this.CustomProperties = new RdlCollection<CustomProperty>();
			this.DataMembers = new RdlCollection<DataMember>();
		}

		// Token: 0x17000275 RID: 629
		// (get) Token: 0x0600078B RID: 1931 RVA: 0x0001B7C5 File Offset: 0x000199C5
		IEnumerable<IHierarchyMember> IHierarchyMember.Members
		{
			get
			{
				foreach (IHierarchyMember hierarchyMember in this.DataMembers)
				{
					yield return hierarchyMember;
				}
				IEnumerator<DataMember> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000362 RID: 866
		internal class Definition : DefinitionStore<DataMember, DataMember.Definition.Properties>
		{
			// Token: 0x060017ED RID: 6125 RVA: 0x0003ADBB File Offset: 0x00038FBB
			private Definition()
			{
			}

			// Token: 0x02000480 RID: 1152
			internal enum Properties
			{
				// Token: 0x04000AD7 RID: 2775
				Group,
				// Token: 0x04000AD8 RID: 2776
				SortExpressions,
				// Token: 0x04000AD9 RID: 2777
				CustomProperties,
				// Token: 0x04000ADA RID: 2778
				DataMembers,
				// Token: 0x04000ADB RID: 2779
				PropertyCount
			}
		}
	}
}

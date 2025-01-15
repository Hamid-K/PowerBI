using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000B1 RID: 177
	public class DataHierarchy : ReportObject, IHierarchy
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600077A RID: 1914 RVA: 0x0001B6AD File Offset: 0x000198AD
		// (set) Token: 0x0600077B RID: 1915 RVA: 0x0001B6C0 File Offset: 0x000198C0
		[XmlElement(typeof(RdlCollection<DataMember>))]
		public IList<DataMember> DataMembers
		{
			get
			{
				return (IList<DataMember>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x0600077C RID: 1916 RVA: 0x0001B6CF File Offset: 0x000198CF
		public DataHierarchy()
		{
		}

		// Token: 0x0600077D RID: 1917 RVA: 0x0001B6D7 File Offset: 0x000198D7
		internal DataHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600077E RID: 1918 RVA: 0x0001B6E0 File Offset: 0x000198E0
		public override void Initialize()
		{
			base.Initialize();
			this.DataMembers = new RdlCollection<DataMember>();
		}

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600077F RID: 1919 RVA: 0x0001B6F3 File Offset: 0x000198F3
		IEnumerable<IHierarchyMember> IHierarchy.Members
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

		// Token: 0x02000360 RID: 864
		internal class Definition : DefinitionStore<DataHierarchy, DataHierarchy.Definition.Properties>
		{
			// Token: 0x060017E3 RID: 6115 RVA: 0x0003AC3B File Offset: 0x00038E3B
			private Definition()
			{
			}

			// Token: 0x0200047F RID: 1151
			internal enum Properties
			{
				// Token: 0x04000AD5 RID: 2773
				DataMembers
			}
		}
	}
}

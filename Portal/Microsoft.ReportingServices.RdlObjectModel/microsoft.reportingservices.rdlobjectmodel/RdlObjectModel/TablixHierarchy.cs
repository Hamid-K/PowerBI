using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001FC RID: 508
	public class TablixHierarchy : ReportObject, IHierarchy
	{
		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001108 RID: 4360 RVA: 0x0002797B File Offset: 0x00025B7B
		// (set) Token: 0x06001109 RID: 4361 RVA: 0x0002798E File Offset: 0x00025B8E
		[XmlElement(typeof(RdlCollection<TablixMember>))]
		public IList<TablixMember> TablixMembers
		{
			get
			{
				return (IList<TablixMember>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x0002799D File Offset: 0x00025B9D
		public TablixHierarchy()
		{
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000279A5 File Offset: 0x00025BA5
		internal TablixHierarchy(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000279AE File Offset: 0x00025BAE
		public override void Initialize()
		{
			base.Initialize();
			this.TablixMembers = new RdlCollection<TablixMember>();
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x0600110D RID: 4365 RVA: 0x000279C1 File Offset: 0x00025BC1
		IEnumerable<IHierarchyMember> IHierarchy.Members
		{
			get
			{
				foreach (IHierarchyMember hierarchyMember in this.TablixMembers)
				{
					yield return hierarchyMember;
				}
				IEnumerator<TablixMember> enumerator = null;
				yield break;
				yield break;
			}
		}

		// Token: 0x02000400 RID: 1024
		internal class Definition : DefinitionStore<TablixHierarchy, TablixHierarchy.Definition.Properties>
		{
			// Token: 0x060018C9 RID: 6345 RVA: 0x0003BC5C File Offset: 0x00039E5C
			private Definition()
			{
			}

			// Token: 0x02000511 RID: 1297
			internal enum Properties
			{
				// Token: 0x04001109 RID: 4361
				TablixMembers
			}
		}
	}
}

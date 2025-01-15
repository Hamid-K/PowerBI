using System;
using Microsoft.ReportingServices.RdlObjectModel;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000008 RID: 8
	internal class StaticMember2005 : ReportObject
	{
		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006C RID: 108 RVA: 0x00002710 File Offset: 0x00000910
		// (set) Token: 0x0600006D RID: 109 RVA: 0x0000271E File Offset: 0x0000091E
		public ReportExpression Label
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600006E RID: 110 RVA: 0x00002732 File Offset: 0x00000932
		// (set) Token: 0x0600006F RID: 111 RVA: 0x00002745 File Offset: 0x00000945
		[XmlChildAttribute("Label", "LocID", "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string LabelLocID
		{
			get
			{
				return (string)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06000070 RID: 112 RVA: 0x00002754 File Offset: 0x00000954
		public StaticMember2005()
		{
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000275C File Offset: 0x0000095C
		public StaticMember2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002F1 RID: 753
		internal class Definition : DefinitionStore<StaticMember2005, StaticMember2005.Definition.Properties>
		{
			// Token: 0x060016ED RID: 5869 RVA: 0x00036402 File Offset: 0x00034602
			private Definition()
			{
			}

			// Token: 0x02000425 RID: 1061
			public enum Properties
			{
				// Token: 0x04000823 RID: 2083
				Label,
				// Token: 0x04000824 RID: 2084
				LabelLocID
			}
		}
	}
}

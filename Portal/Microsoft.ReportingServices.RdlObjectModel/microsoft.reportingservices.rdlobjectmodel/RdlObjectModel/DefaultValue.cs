using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001ED RID: 493
	public class DefaultValue : ReportObject
	{
		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600105C RID: 4188 RVA: 0x00026773 File Offset: 0x00024973
		// (set) Token: 0x0600105D RID: 4189 RVA: 0x00026786 File Offset: 0x00024986
		public DataSetReference DataSetReference
		{
			get
			{
				return (DataSetReference)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600105E RID: 4190 RVA: 0x00026795 File Offset: 0x00024995
		// (set) Token: 0x0600105F RID: 4191 RVA: 0x000267A8 File Offset: 0x000249A8
		[XmlElement(typeof(RdlCollection<ReportExpression?>))]
		[XmlArrayItem("Value", typeof(ReportExpression), IsNullable = true)]
		public IList<ReportExpression?> Values
		{
			get
			{
				return (IList<ReportExpression?>)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x06001060 RID: 4192 RVA: 0x000267B7 File Offset: 0x000249B7
		public DefaultValue()
		{
		}

		// Token: 0x06001061 RID: 4193 RVA: 0x000267BF File Offset: 0x000249BF
		internal DefaultValue(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001062 RID: 4194 RVA: 0x000267C8 File Offset: 0x000249C8
		public override void Initialize()
		{
			base.Initialize();
			this.Values = new RdlCollection<ReportExpression?>();
		}

		// Token: 0x020003F8 RID: 1016
		internal class Definition : DefinitionStore<DefaultValue, DefaultValue.Definition.Properties>
		{
			// Token: 0x060018BA RID: 6330 RVA: 0x0003BB8F File Offset: 0x00039D8F
			private Definition()
			{
			}

			// Token: 0x0200050A RID: 1290
			internal enum Properties
			{
				// Token: 0x040010D6 RID: 4310
				DataSetReference,
				// Token: 0x040010D7 RID: 4311
				Values
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E1 RID: 481
	public class Visibility : ReportObject
	{
		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x06000FFA RID: 4090 RVA: 0x000261CA File Offset: 0x000243CA
		// (set) Token: 0x06000FFB RID: 4091 RVA: 0x000261D8 File Offset: 0x000243D8
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Hidden
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x06000FFC RID: 4092 RVA: 0x000261EC File Offset: 0x000243EC
		// (set) Token: 0x06000FFD RID: 4093 RVA: 0x000261FF File Offset: 0x000243FF
		[DefaultValue("")]
		public string ToggleItem
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

		// Token: 0x06000FFE RID: 4094 RVA: 0x0002620E File Offset: 0x0002440E
		public Visibility()
		{
		}

		// Token: 0x06000FFF RID: 4095 RVA: 0x00026216 File Offset: 0x00024416
		internal Visibility(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06001000 RID: 4096 RVA: 0x0002621F File Offset: 0x0002441F
		internal override void UpdateNamedReferences(NameChanges nameChanges)
		{
			base.UpdateNamedReferences(nameChanges);
			this.ToggleItem = nameChanges.GetNewName(NameChanges.EntryType.ReportItem, this.ToggleItem);
		}

		// Token: 0x020003EF RID: 1007
		internal class Definition : DefinitionStore<Visibility, Visibility.Definition.Properties>
		{
			// Token: 0x060018B1 RID: 6321 RVA: 0x0003BB47 File Offset: 0x00039D47
			private Definition()
			{
			}

			// Token: 0x02000501 RID: 1281
			internal enum Properties
			{
				// Token: 0x040010AD RID: 4269
				Hidden,
				// Token: 0x040010AE RID: 4270
				ToggleItem
			}
		}
	}
}

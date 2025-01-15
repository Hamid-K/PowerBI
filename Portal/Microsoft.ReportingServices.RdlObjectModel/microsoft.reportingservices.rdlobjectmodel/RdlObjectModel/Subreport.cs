using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001F8 RID: 504
	public class Subreport : ReportItem
	{
		// Token: 0x170005D1 RID: 1489
		// (get) Token: 0x060010EF RID: 4335 RVA: 0x000277F4 File Offset: 0x000259F4
		// (set) Token: 0x060010F0 RID: 4336 RVA: 0x00027808 File Offset: 0x00025A08
		public string ReportName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(18);
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				base.PropertyStore.SetObject(18, value);
			}
		}

		// Token: 0x170005D2 RID: 1490
		// (get) Token: 0x060010F1 RID: 4337 RVA: 0x00027826 File Offset: 0x00025A26
		// (set) Token: 0x060010F2 RID: 4338 RVA: 0x0002783A File Offset: 0x00025A3A
		[XmlElement(typeof(RdlCollection<Parameter>))]
		public IList<Parameter> Parameters
		{
			get
			{
				return (IList<Parameter>)base.PropertyStore.GetObject(19);
			}
			set
			{
				base.PropertyStore.SetObject(19, value);
			}
		}

		// Token: 0x170005D3 RID: 1491
		// (get) Token: 0x060010F3 RID: 4339 RVA: 0x0002784A File Offset: 0x00025A4A
		// (set) Token: 0x060010F4 RID: 4340 RVA: 0x00027859 File Offset: 0x00025A59
		[ReportExpressionDefaultValue]
		public ReportExpression NoRowsMessage
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(20);
			}
			set
			{
				base.PropertyStore.SetObject(20, value);
			}
		}

		// Token: 0x170005D4 RID: 1492
		// (get) Token: 0x060010F5 RID: 4341 RVA: 0x0002786E File Offset: 0x00025A6E
		// (set) Token: 0x060010F6 RID: 4342 RVA: 0x0002787D File Offset: 0x00025A7D
		[DefaultValue(false)]
		public bool MergeTransactions
		{
			get
			{
				return base.PropertyStore.GetBoolean(21);
			}
			set
			{
				base.PropertyStore.SetBoolean(21, value);
			}
		}

		// Token: 0x170005D5 RID: 1493
		// (get) Token: 0x060010F7 RID: 4343 RVA: 0x0002788D File Offset: 0x00025A8D
		// (set) Token: 0x060010F8 RID: 4344 RVA: 0x0002789C File Offset: 0x00025A9C
		[DefaultValue(false)]
		public bool KeepTogether
		{
			get
			{
				return base.PropertyStore.GetBoolean(22);
			}
			set
			{
				base.PropertyStore.SetBoolean(22, value);
			}
		}

		// Token: 0x170005D6 RID: 1494
		// (get) Token: 0x060010F9 RID: 4345 RVA: 0x000278AC File Offset: 0x00025AAC
		// (set) Token: 0x060010FA RID: 4346 RVA: 0x000278BB File Offset: 0x00025ABB
		[DefaultValue(false)]
		public bool OmitBorderOnPageBreak
		{
			get
			{
				return base.PropertyStore.GetBoolean(23);
			}
			set
			{
				base.PropertyStore.SetBoolean(23, value);
			}
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000278CB File Offset: 0x00025ACB
		public Subreport()
		{
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000278D3 File Offset: 0x00025AD3
		internal Subreport(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000278DC File Offset: 0x00025ADC
		public override void Initialize()
		{
			base.Initialize();
			this.ReportName = "";
			this.Parameters = new RdlCollection<Parameter>();
		}

		// Token: 0x020003FD RID: 1021
		internal new class Definition : DefinitionStore<Subreport, Subreport.Definition.Properties>
		{
			// Token: 0x060018C6 RID: 6342 RVA: 0x0003BC44 File Offset: 0x00039E44
			private Definition()
			{
			}

			// Token: 0x0200050E RID: 1294
			internal enum Properties
			{
				// Token: 0x040010EB RID: 4331
				Style,
				// Token: 0x040010EC RID: 4332
				Name,
				// Token: 0x040010ED RID: 4333
				ActionInfo,
				// Token: 0x040010EE RID: 4334
				Top,
				// Token: 0x040010EF RID: 4335
				Left,
				// Token: 0x040010F0 RID: 4336
				Height,
				// Token: 0x040010F1 RID: 4337
				Width,
				// Token: 0x040010F2 RID: 4338
				ZIndex,
				// Token: 0x040010F3 RID: 4339
				Visibility,
				// Token: 0x040010F4 RID: 4340
				ToolTip,
				// Token: 0x040010F5 RID: 4341
				ToolTipLocID,
				// Token: 0x040010F6 RID: 4342
				DocumentMapLabel,
				// Token: 0x040010F7 RID: 4343
				DocumentMapLabelLocID,
				// Token: 0x040010F8 RID: 4344
				Bookmark,
				// Token: 0x040010F9 RID: 4345
				RepeatWith,
				// Token: 0x040010FA RID: 4346
				CustomProperties,
				// Token: 0x040010FB RID: 4347
				DataElementName,
				// Token: 0x040010FC RID: 4348
				DataElementOutput,
				// Token: 0x040010FD RID: 4349
				ReportName,
				// Token: 0x040010FE RID: 4350
				Parameters,
				// Token: 0x040010FF RID: 4351
				NoRowsMessage,
				// Token: 0x04001100 RID: 4352
				MergeTransactions,
				// Token: 0x04001101 RID: 4353
				KeepTogether,
				// Token: 0x04001102 RID: 4354
				OmitBorderOnPageBreak,
				// Token: 0x04001103 RID: 4355
				PropertyCount
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x0200002F RID: 47
	internal class Subtotal2005 : ReportObject
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003766 File Offset: 0x00001966
		// (set) Token: 0x0600018F RID: 399 RVA: 0x00003779 File Offset: 0x00001979
		[XmlElement(typeof(RdlCollection<ReportItem>))]
		public IList<ReportItem> ReportItems
		{
			get
			{
				return (IList<ReportItem>)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003788 File Offset: 0x00001988
		// (set) Token: 0x06000191 RID: 401 RVA: 0x0000379B File Offset: 0x0000199B
		public SubtotalStyle2005 Style
		{
			get
			{
				return (SubtotalStyle2005)base.PropertyStore.GetObject(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000192 RID: 402 RVA: 0x000037AA File Offset: 0x000019AA
		// (set) Token: 0x06000193 RID: 403 RVA: 0x000037B8 File Offset: 0x000019B8
		public SubtotalPositions Position
		{
			get
			{
				return (SubtotalPositions)base.PropertyStore.GetInteger(2);
			}
			set
			{
				base.PropertyStore.SetInteger(2, (int)value);
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000194 RID: 404 RVA: 0x000037C7 File Offset: 0x000019C7
		// (set) Token: 0x06000195 RID: 405 RVA: 0x000037DA File Offset: 0x000019DA
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000196 RID: 406 RVA: 0x000037E9 File Offset: 0x000019E9
		// (set) Token: 0x06000197 RID: 407 RVA: 0x000037F7 File Offset: 0x000019F7
		[DefaultValue(DataElementOutputTypes.NoOutput)]
		[ValidEnumValues(typeof(Constants2005), "Subtotal2005DataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(4);
			}
			set
			{
				((EnumProperty)DefinitionStore<Subtotal2005, Subtotal2005.Definition.Properties>.GetProperty(4)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(4, (int)value);
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00003818 File Offset: 0x00001A18
		public Subtotal2005()
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00003820 File Offset: 0x00001A20
		public Subtotal2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00003829 File Offset: 0x00001A29
		public override void Initialize()
		{
			base.Initialize();
			this.ReportItems = new RdlCollection<ReportItem>();
			this.DataElementOutput = DataElementOutputTypes.NoOutput;
		}

		// Token: 0x02000309 RID: 777
		internal class Definition : DefinitionStore<Subtotal2005, Subtotal2005.Definition.Properties>
		{
			// Token: 0x06001705 RID: 5893 RVA: 0x000364C2 File Offset: 0x000346C2
			private Definition()
			{
			}

			// Token: 0x0200043D RID: 1085
			public enum Properties
			{
				// Token: 0x04000898 RID: 2200
				ReportItems,
				// Token: 0x04000899 RID: 2201
				Style,
				// Token: 0x0400089A RID: 2202
				Position,
				// Token: 0x0400089B RID: 2203
				DataElementName,
				// Token: 0x0400089C RID: 2204
				DataElementOutput
			}
		}
	}
}

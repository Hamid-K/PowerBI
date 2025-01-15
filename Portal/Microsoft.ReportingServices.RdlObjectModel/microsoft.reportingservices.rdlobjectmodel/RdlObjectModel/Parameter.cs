using System;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001E0 RID: 480
	public class Parameter : ReportObject, INamedObject
	{
		// Token: 0x1700057F RID: 1407
		// (get) Token: 0x06000FF2 RID: 4082 RVA: 0x00026153 File Offset: 0x00024353
		// (set) Token: 0x06000FF3 RID: 4083 RVA: 0x00026166 File Offset: 0x00024366
		[XmlAttribute(typeof(string))]
		public string Name
		{
			get
			{
				return (string)base.PropertyStore.GetObject(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x06000FF4 RID: 4084 RVA: 0x00026175 File Offset: 0x00024375
		// (set) Token: 0x06000FF5 RID: 4085 RVA: 0x00026183 File Offset: 0x00024383
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(1);
			}
			set
			{
				base.PropertyStore.SetObject(1, value);
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x06000FF6 RID: 4086 RVA: 0x00026197 File Offset: 0x00024397
		// (set) Token: 0x06000FF7 RID: 4087 RVA: 0x000261A5 File Offset: 0x000243A5
		[ReportExpressionDefaultValue(typeof(bool), false)]
		public ReportExpression<bool> Omit
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<bool>>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000261B9 File Offset: 0x000243B9
		public Parameter()
		{
		}

		// Token: 0x06000FF9 RID: 4089 RVA: 0x000261C1 File Offset: 0x000243C1
		internal Parameter(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003EE RID: 1006
		internal class Definition : DefinitionStore<Parameter, Parameter.Definition.Properties>
		{
			// Token: 0x060018B0 RID: 6320 RVA: 0x0003BB3F File Offset: 0x00039D3F
			private Definition()
			{
			}

			// Token: 0x02000500 RID: 1280
			internal enum Properties
			{
				// Token: 0x040010A9 RID: 4265
				Name,
				// Token: 0x040010AA RID: 4266
				Value,
				// Token: 0x040010AB RID: 4267
				Omit
			}
		}
	}
}

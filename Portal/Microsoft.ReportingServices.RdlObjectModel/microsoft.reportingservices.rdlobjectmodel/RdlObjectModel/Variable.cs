using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001D0 RID: 464
	public class Variable : ReportObject, INamedObject
	{
		// Token: 0x17000550 RID: 1360
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x000248F3 File Offset: 0x00022AF3
		// (set) Token: 0x06000F22 RID: 3874 RVA: 0x00024906 File Offset: 0x00022B06
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

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06000F23 RID: 3875 RVA: 0x00024915 File Offset: 0x00022B15
		// (set) Token: 0x06000F24 RID: 3876 RVA: 0x00024923 File Offset: 0x00022B23
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

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06000F25 RID: 3877 RVA: 0x00024937 File Offset: 0x00022B37
		// (set) Token: 0x06000F26 RID: 3878 RVA: 0x00024945 File Offset: 0x00022B45
		[DefaultValue(false)]
		public bool Writable
		{
			get
			{
				return base.PropertyStore.GetBoolean(2);
			}
			set
			{
				base.PropertyStore.SetBoolean(2, value);
			}
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x00024954 File Offset: 0x00022B54
		public Variable()
		{
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x0002495C File Offset: 0x00022B5C
		internal Variable(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020003E7 RID: 999
		internal class Definition : DefinitionStore<Variable, Variable.Definition.Properties>
		{
			// Token: 0x060018A9 RID: 6313 RVA: 0x0003BB07 File Offset: 0x00039D07
			private Definition()
			{
			}

			// Token: 0x020004F9 RID: 1273
			internal enum Properties
			{
				// Token: 0x04001080 RID: 4224
				Name,
				// Token: 0x04001081 RID: 4225
				Value,
				// Token: 0x04001082 RID: 4226
				Writable
			}
		}
	}
}

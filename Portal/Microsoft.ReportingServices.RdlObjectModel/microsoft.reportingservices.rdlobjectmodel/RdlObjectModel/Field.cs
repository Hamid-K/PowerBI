using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020000BC RID: 188
	public class Field : ReportObject, INamedObject
	{
		// Token: 0x1700028F RID: 655
		// (get) Token: 0x060007D4 RID: 2004 RVA: 0x0001BC67 File Offset: 0x00019E67
		// (set) Token: 0x060007D5 RID: 2005 RVA: 0x0001BC7A File Offset: 0x00019E7A
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

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060007D6 RID: 2006 RVA: 0x0001BC89 File Offset: 0x00019E89
		// (set) Token: 0x060007D7 RID: 2007 RVA: 0x0001BC9C File Offset: 0x00019E9C
		public string DataField
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

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060007D8 RID: 2008 RVA: 0x0001BCAB File Offset: 0x00019EAB
		// (set) Token: 0x060007D9 RID: 2009 RVA: 0x0001BCB9 File Offset: 0x00019EB9
		[ReportExpressionDefaultValue]
		public ReportExpression Value
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060007DA RID: 2010 RVA: 0x0001BCCD File Offset: 0x00019ECD
		// (set) Token: 0x060007DB RID: 2011 RVA: 0x0001BCE0 File Offset: 0x00019EE0
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/2011/01/reportdefinition")]
		public string AggregateIndicatorField
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

		// Token: 0x060007DC RID: 2012 RVA: 0x0001BCEF File Offset: 0x00019EEF
		public Field()
		{
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0001BCF7 File Offset: 0x00019EF7
		internal Field(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x060007DE RID: 2014 RVA: 0x0001BD00 File Offset: 0x00019F00
		// (set) Token: 0x060007DF RID: 2015 RVA: 0x0001BD08 File Offset: 0x00019F08
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public string TypeName
		{
			get
			{
				return this.m_typeName;
			}
			set
			{
				if (this.m_typeName != value)
				{
					this.m_typeName = value;
				}
			}
		}

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x060007E0 RID: 2016 RVA: 0x0001BD1F File Offset: 0x00019F1F
		// (set) Token: 0x060007E1 RID: 2017 RVA: 0x0001BD27 File Offset: 0x00019F27
		[DefaultValue(false)]
		[XmlElement(Namespace = "http://schemas.microsoft.com/SQLServer/reporting/reportdesigner")]
		public bool UserDefined
		{
			get
			{
				return this.m_userDefined;
			}
			set
			{
				if (this.m_userDefined != value)
				{
					this.m_userDefined = value;
				}
			}
		}

		// Token: 0x04000157 RID: 343
		private string m_typeName;

		// Token: 0x04000158 RID: 344
		private bool m_userDefined;

		// Token: 0x02000368 RID: 872
		internal class Definition : DefinitionStore<Field, Field.Definition.Properties>
		{
			// Token: 0x02000485 RID: 1157
			internal enum Properties
			{
				// Token: 0x04000B0E RID: 2830
				Name,
				// Token: 0x04000B0F RID: 2831
				DataField,
				// Token: 0x04000B10 RID: 2832
				Value,
				// Token: 0x04000B11 RID: 2833
				AggregateIndicatorField
			}
		}
	}
}

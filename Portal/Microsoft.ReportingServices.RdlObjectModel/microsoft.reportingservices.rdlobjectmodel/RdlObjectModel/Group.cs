using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x0200016E RID: 366
	public class Group : ReportObject, IGlobalNamedObject, INamedObject
	{
		// Token: 0x17000419 RID: 1049
		// (get) Token: 0x06000B94 RID: 2964 RVA: 0x000204C0 File Offset: 0x0001E6C0
		// (set) Token: 0x06000B95 RID: 2965 RVA: 0x000204D3 File Offset: 0x0001E6D3
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

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x06000B96 RID: 2966 RVA: 0x000204E2 File Offset: 0x0001E6E2
		// (set) Token: 0x06000B97 RID: 2967 RVA: 0x000204F0 File Offset: 0x0001E6F0
		[ReportExpressionDefaultValue]
		public ReportExpression DocumentMapLabel
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

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x00020504 File Offset: 0x0001E704
		// (set) Token: 0x06000B99 RID: 2969 RVA: 0x00020517 File Offset: 0x0001E717
		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("GroupExpression", typeof(ReportExpression))]
		public IList<ReportExpression> GroupExpressions
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06000B9A RID: 2970 RVA: 0x00020526 File Offset: 0x0001E726
		// (set) Token: 0x06000B9B RID: 2971 RVA: 0x00020539 File Offset: 0x0001E739
		[XmlElement(typeof(RdlCollection<ReportExpression>))]
		[XmlArrayItem("GroupExpression", typeof(ReportExpression))]
		public IList<ReportExpression> ReGroupExpressions
		{
			get
			{
				return (IList<ReportExpression>)base.PropertyStore.GetObject(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x1700041D RID: 1053
		// (get) Token: 0x06000B9C RID: 2972 RVA: 0x00020548 File Offset: 0x0001E748
		// (set) Token: 0x06000B9D RID: 2973 RVA: 0x0002055B File Offset: 0x0001E75B
		public PageBreak PageBreak
		{
			get
			{
				return (PageBreak)base.PropertyStore.GetObject(5);
			}
			set
			{
				base.PropertyStore.SetObject(5, value);
			}
		}

		// Token: 0x1700041E RID: 1054
		// (get) Token: 0x06000B9E RID: 2974 RVA: 0x0002056A File Offset: 0x0001E76A
		// (set) Token: 0x06000B9F RID: 2975 RVA: 0x00020579 File Offset: 0x0001E779
		[ReportExpressionDefaultValue]
		public ReportExpression PageName
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(11);
			}
			set
			{
				base.PropertyStore.SetObject(11, value);
			}
		}

		// Token: 0x1700041F RID: 1055
		// (get) Token: 0x06000BA0 RID: 2976 RVA: 0x0002058E File Offset: 0x0001E78E
		// (set) Token: 0x06000BA1 RID: 2977 RVA: 0x000205A1 File Offset: 0x0001E7A1
		[XmlElement(typeof(RdlCollection<Filter>))]
		public IList<Filter> Filters
		{
			get
			{
				return (IList<Filter>)base.PropertyStore.GetObject(6);
			}
			set
			{
				base.PropertyStore.SetObject(6, value);
			}
		}

		// Token: 0x17000420 RID: 1056
		// (get) Token: 0x06000BA2 RID: 2978 RVA: 0x000205B0 File Offset: 0x0001E7B0
		// (set) Token: 0x06000BA3 RID: 2979 RVA: 0x000205BE File Offset: 0x0001E7BE
		[ReportExpressionDefaultValue]
		public new ReportExpression Parent
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(7);
			}
			set
			{
				base.PropertyStore.SetObject(7, value);
			}
		}

		// Token: 0x17000421 RID: 1057
		// (get) Token: 0x06000BA4 RID: 2980 RVA: 0x000205D2 File Offset: 0x0001E7D2
		// (set) Token: 0x06000BA5 RID: 2981 RVA: 0x000205E5 File Offset: 0x0001E7E5
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000422 RID: 1058
		// (get) Token: 0x06000BA6 RID: 2982 RVA: 0x000205F4 File Offset: 0x0001E7F4
		// (set) Token: 0x06000BA7 RID: 2983 RVA: 0x00020603 File Offset: 0x0001E803
		[DefaultValue(DataElementOutputTypes.Output)]
		[ValidEnumValues("GroupDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(9);
			}
			set
			{
				((EnumProperty)DefinitionStore<Group, Group.Definition.Properties>.GetProperty(9)).Validate(this, (int)value);
				base.PropertyStore.SetInteger(9, (int)value);
			}
		}

		// Token: 0x17000423 RID: 1059
		// (get) Token: 0x06000BA8 RID: 2984 RVA: 0x00020626 File Offset: 0x0001E826
		// (set) Token: 0x06000BA9 RID: 2985 RVA: 0x0002063A File Offset: 0x0001E83A
		[XmlElement(typeof(RdlCollection<Variable>))]
		public IList<Variable> Variables
		{
			get
			{
				return (IList<Variable>)base.PropertyStore.GetObject(10);
			}
			set
			{
				base.PropertyStore.SetObject(10, value);
			}
		}

		// Token: 0x17000424 RID: 1060
		// (get) Token: 0x06000BAA RID: 2986 RVA: 0x0002064A File Offset: 0x0001E84A
		// (set) Token: 0x06000BAB RID: 2987 RVA: 0x0002065E File Offset: 0x0001E85E
		[DefaultValue("")]
		public string DomainScope
		{
			get
			{
				return (string)base.PropertyStore.GetObject(12);
			}
			set
			{
				base.PropertyStore.SetObject(12, value);
			}
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x0002066E File Offset: 0x0001E86E
		public Group()
		{
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x00020676 File Offset: 0x0001E876
		internal Group(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0002067F File Offset: 0x0001E87F
		public override void Initialize()
		{
			base.Initialize();
			this.GroupExpressions = new RdlCollection<ReportExpression>();
			this.ReGroupExpressions = new RdlCollection<ReportExpression>();
			this.Filters = new RdlCollection<Filter>();
			this.DataElementOutput = DataElementOutputTypes.Output;
			this.Variables = new RdlCollection<Variable>();
		}

		// Token: 0x0200039F RID: 927
		internal class Definition : DefinitionStore<Group, Group.Definition.Properties>
		{
			// Token: 0x06001842 RID: 6210 RVA: 0x0003B53B File Offset: 0x0003973B
			private Definition()
			{
			}

			// Token: 0x020004B8 RID: 1208
			internal enum Properties
			{
				// Token: 0x04000DE7 RID: 3559
				Name,
				// Token: 0x04000DE8 RID: 3560
				DocumentMapLabel,
				// Token: 0x04000DE9 RID: 3561
				DocumentMapLabelLocID,
				// Token: 0x04000DEA RID: 3562
				GroupExpressions,
				// Token: 0x04000DEB RID: 3563
				ReGroupExpressions,
				// Token: 0x04000DEC RID: 3564
				PageBreak,
				// Token: 0x04000DED RID: 3565
				Filters,
				// Token: 0x04000DEE RID: 3566
				Parent,
				// Token: 0x04000DEF RID: 3567
				DataElementName,
				// Token: 0x04000DF0 RID: 3568
				DataElementOutput,
				// Token: 0x04000DF1 RID: 3569
				Variables,
				// Token: 0x04000DF2 RID: 3570
				PageName,
				// Token: 0x04000DF3 RID: 3571
				DomainScope,
				// Token: 0x04000DF4 RID: 3572
				PropertyCount
			}
		}
	}
}

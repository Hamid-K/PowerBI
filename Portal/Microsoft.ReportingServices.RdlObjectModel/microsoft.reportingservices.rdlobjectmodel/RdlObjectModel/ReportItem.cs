using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;
using Microsoft.ReportingServices.RdlObjectModel.Serialization;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001DC RID: 476
	[XmlElementClass("Line", typeof(Line))]
	[XmlElementClass("Rectangle", typeof(Rectangle))]
	[XmlElementClass("Textbox", typeof(Textbox))]
	[XmlElementClass("Image", typeof(Image))]
	[XmlElementClass("Subreport", typeof(Subreport))]
	[XmlElementClass("Chart", typeof(Chart))]
	[XmlElementClass("GaugePanel", typeof(GaugePanel))]
	[XmlElementClass("Map", typeof(Map))]
	[XmlElementClass("Tablix", typeof(Tablix))]
	[XmlElementClass("CustomReportItem", typeof(CustomReportItem))]
	public abstract class ReportItem : ReportElement, IGlobalNamedObject, INamedObject, IShouldSerialize
	{
		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06000FB9 RID: 4025 RVA: 0x00025D6B File Offset: 0x00023F6B
		// (set) Token: 0x06000FBA RID: 4026 RVA: 0x00025D7E File Offset: 0x00023F7E
		[XmlAttribute(typeof(string))]
		public string Name
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

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06000FBB RID: 4027 RVA: 0x00025D8D File Offset: 0x00023F8D
		// (set) Token: 0x06000FBC RID: 4028 RVA: 0x00025D95 File Offset: 0x00023F95
		[XmlElement(Namespace = "http://schemas.microsoft.com/sqlserver/reporting/webauthoring")]
		public string StyleName { get; set; }

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06000FBD RID: 4029 RVA: 0x00025D9E File Offset: 0x00023F9E
		// (set) Token: 0x06000FBE RID: 4030 RVA: 0x00025DB1 File Offset: 0x00023FB1
		public ActionInfo ActionInfo
		{
			get
			{
				return (ActionInfo)base.PropertyStore.GetObject(2);
			}
			set
			{
				base.PropertyStore.SetObject(2, value);
			}
		}

		// Token: 0x1700056C RID: 1388
		// (get) Token: 0x06000FBF RID: 4031 RVA: 0x00025DC0 File Offset: 0x00023FC0
		// (set) Token: 0x06000FC0 RID: 4032 RVA: 0x00025DCE File Offset: 0x00023FCE
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize Top
		{
			get
			{
				return base.PropertyStore.GetSize(3);
			}
			set
			{
				base.PropertyStore.SetSize(3, value);
			}
		}

		// Token: 0x1700056D RID: 1389
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x00025DDD File Offset: 0x00023FDD
		// (set) Token: 0x06000FC2 RID: 4034 RVA: 0x00025DEB File Offset: 0x00023FEB
		[DefaultValueConstant("DefaultZeroSize")]
		public ReportSize Left
		{
			get
			{
				return base.PropertyStore.GetSize(4);
			}
			set
			{
				base.PropertyStore.SetSize(4, value);
			}
		}

		// Token: 0x1700056E RID: 1390
		// (get) Token: 0x06000FC3 RID: 4035 RVA: 0x00025DFA File Offset: 0x00023FFA
		// (set) Token: 0x06000FC4 RID: 4036 RVA: 0x00025E08 File Offset: 0x00024008
		public ReportSize Height
		{
			get
			{
				return base.PropertyStore.GetSize(5);
			}
			set
			{
				base.PropertyStore.SetSize(5, value);
			}
		}

		// Token: 0x1700056F RID: 1391
		// (get) Token: 0x06000FC5 RID: 4037 RVA: 0x00025E17 File Offset: 0x00024017
		// (set) Token: 0x06000FC6 RID: 4038 RVA: 0x00025E25 File Offset: 0x00024025
		public ReportSize Width
		{
			get
			{
				return base.PropertyStore.GetSize(6);
			}
			set
			{
				base.PropertyStore.SetSize(6, value);
			}
		}

		// Token: 0x17000570 RID: 1392
		// (get) Token: 0x06000FC7 RID: 4039 RVA: 0x00025E34 File Offset: 0x00024034
		// (set) Token: 0x06000FC8 RID: 4040 RVA: 0x00025E42 File Offset: 0x00024042
		[DefaultValue(0)]
		[ValidValues(0, 2147483647)]
		public int ZIndex
		{
			get
			{
				return base.PropertyStore.GetInteger(7);
			}
			set
			{
				((IntProperty)DefinitionStore<ReportItem, ReportItem.Definition.Properties>.GetProperty(7)).Validate(this, value);
				base.PropertyStore.SetInteger(7, value);
			}
		}

		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x06000FC9 RID: 4041 RVA: 0x00025E63 File Offset: 0x00024063
		// (set) Token: 0x06000FCA RID: 4042 RVA: 0x00025E76 File Offset: 0x00024076
		public Visibility Visibility
		{
			get
			{
				return (Visibility)base.PropertyStore.GetObject(8);
			}
			set
			{
				base.PropertyStore.SetObject(8, value);
			}
		}

		// Token: 0x17000572 RID: 1394
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00025E85 File Offset: 0x00024085
		// (set) Token: 0x06000FCC RID: 4044 RVA: 0x00025E94 File Offset: 0x00024094
		[ReportExpressionDefaultValue]
		public ReportExpression ToolTip
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(9);
			}
			set
			{
				base.PropertyStore.SetObject(9, value);
			}
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00025EA9 File Offset: 0x000240A9
		// (set) Token: 0x06000FCE RID: 4046 RVA: 0x00025EB8 File Offset: 0x000240B8
		[ReportExpressionDefaultValue]
		public ReportExpression DocumentMapLabel
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

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00025ECD File Offset: 0x000240CD
		// (set) Token: 0x06000FD0 RID: 4048 RVA: 0x00025EDC File Offset: 0x000240DC
		[ReportExpressionDefaultValue]
		public ReportExpression Bookmark
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression>(13);
			}
			set
			{
				base.PropertyStore.SetObject(13, value);
			}
		}

		// Token: 0x17000575 RID: 1397
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00025EF1 File Offset: 0x000240F1
		// (set) Token: 0x06000FD2 RID: 4050 RVA: 0x00025F05 File Offset: 0x00024105
		[DefaultValue("")]
		public string RepeatWith
		{
			get
			{
				return (string)base.PropertyStore.GetObject(14);
			}
			set
			{
				base.PropertyStore.SetObject(14, value);
			}
		}

		// Token: 0x17000576 RID: 1398
		// (get) Token: 0x06000FD3 RID: 4051 RVA: 0x00025F15 File Offset: 0x00024115
		// (set) Token: 0x06000FD4 RID: 4052 RVA: 0x00025F29 File Offset: 0x00024129
		[XmlElement(typeof(RdlCollection<CustomProperty>))]
		public IList<CustomProperty> CustomProperties
		{
			get
			{
				return (IList<CustomProperty>)base.PropertyStore.GetObject(15);
			}
			set
			{
				base.PropertyStore.SetObject(15, value);
			}
		}

		// Token: 0x17000577 RID: 1399
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00025F39 File Offset: 0x00024139
		// (set) Token: 0x06000FD6 RID: 4054 RVA: 0x00025F4D File Offset: 0x0002414D
		[DefaultValue("")]
		public string DataElementName
		{
			get
			{
				return (string)base.PropertyStore.GetObject(16);
			}
			set
			{
				base.PropertyStore.SetObject(16, value);
			}
		}

		// Token: 0x17000578 RID: 1400
		// (get) Token: 0x06000FD7 RID: 4055 RVA: 0x00025F5D File Offset: 0x0002415D
		// (set) Token: 0x06000FD8 RID: 4056 RVA: 0x00025F6C File Offset: 0x0002416C
		[DefaultValue(DataElementOutputTypes.Auto)]
		[ValidEnumValues("ReportItemDataElementOutputTypes")]
		public DataElementOutputTypes DataElementOutput
		{
			get
			{
				return (DataElementOutputTypes)base.PropertyStore.GetInteger(17);
			}
			set
			{
				base.PropertyStore.SetInteger(17, (int)value);
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x00025F7C File Offset: 0x0002417C
		protected ReportItem()
		{
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x00025F84 File Offset: 0x00024184
		internal ReportItem(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x00025F8D File Offset: 0x0002418D
		public override void Initialize()
		{
			base.Initialize();
			this.CustomProperties = new RdlCollection<CustomProperty>();
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x00025FA0 File Offset: 0x000241A0
		bool IShouldSerialize.ShouldSerializeThis()
		{
			return true;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x00025FA4 File Offset: 0x000241A4
		SerializationMethod IShouldSerialize.ShouldSerializeProperty(string name)
		{
			if (!(name == "Top"))
			{
				if (name == "Left")
				{
					if (this.Left.Value == 0.0 && this.Left.Type == ReportSize.DefaultType)
					{
						return SerializationMethod.Never;
					}
				}
			}
			else if (this.Top.Value == 0.0 && this.Top.Type == ReportSize.DefaultType)
			{
				return SerializationMethod.Never;
			}
			return SerializationMethod.Auto;
		}

		// Token: 0x020003EA RID: 1002
		internal new class Definition : DefinitionStore<ReportItem, ReportItem.Definition.Properties>
		{
			// Token: 0x060018AC RID: 6316 RVA: 0x0003BB1F File Offset: 0x00039D1F
			private Definition()
			{
			}

			// Token: 0x020004FC RID: 1276
			internal enum Properties
			{
				// Token: 0x0400108A RID: 4234
				Style,
				// Token: 0x0400108B RID: 4235
				Name,
				// Token: 0x0400108C RID: 4236
				ActionInfo,
				// Token: 0x0400108D RID: 4237
				Top,
				// Token: 0x0400108E RID: 4238
				Left,
				// Token: 0x0400108F RID: 4239
				Height,
				// Token: 0x04001090 RID: 4240
				Width,
				// Token: 0x04001091 RID: 4241
				ZIndex,
				// Token: 0x04001092 RID: 4242
				Visibility,
				// Token: 0x04001093 RID: 4243
				ToolTip,
				// Token: 0x04001094 RID: 4244
				ToolTipLocID,
				// Token: 0x04001095 RID: 4245
				DocumentMapLabel,
				// Token: 0x04001096 RID: 4246
				DocumentMapLabelLocID,
				// Token: 0x04001097 RID: 4247
				Bookmark,
				// Token: 0x04001098 RID: 4248
				RepeatWith,
				// Token: 0x04001099 RID: 4249
				CustomProperties,
				// Token: 0x0400109A RID: 4250
				DataElementName,
				// Token: 0x0400109B RID: 4251
				DataElementOutput,
				// Token: 0x0400109C RID: 4252
				PropertyCount
			}
		}
	}
}

using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.RdlObjectModel
{
	// Token: 0x020001A6 RID: 422
	public class MapMarkerImage : ReportObject
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x00022B75 File Offset: 0x00020D75
		public MapMarkerImage()
		{
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00022B7D File Offset: 0x00020D7D
		internal MapMarkerImage(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000DDB RID: 3547 RVA: 0x00022B86 File Offset: 0x00020D86
		// (set) Token: 0x06000DDC RID: 3548 RVA: 0x00022B94 File Offset: 0x00020D94
		public ReportExpression<SourceType> Source
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<SourceType>>(0);
			}
			set
			{
				base.PropertyStore.SetObject(0, value);
			}
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x00022BA8 File Offset: 0x00020DA8
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x00022BB6 File Offset: 0x00020DB6
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

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x00022BCA File Offset: 0x00020DCA
		// (set) Token: 0x06000DE0 RID: 3552 RVA: 0x00022BD8 File Offset: 0x00020DD8
		public ReportExpression MIMEType
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

		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x06000DE1 RID: 3553 RVA: 0x00022BEC File Offset: 0x00020DEC
		// (set) Token: 0x06000DE2 RID: 3554 RVA: 0x00022BFA File Offset: 0x00020DFA
		public ReportExpression<ReportColor> TransparentColor
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<ReportColor>>(3);
			}
			set
			{
				base.PropertyStore.SetObject(3, value);
			}
		}

		// Token: 0x170004E3 RID: 1251
		// (get) Token: 0x06000DE3 RID: 3555 RVA: 0x00022C0E File Offset: 0x00020E0E
		// (set) Token: 0x06000DE4 RID: 3556 RVA: 0x00022C1C File Offset: 0x00020E1C
		[ReportExpressionDefaultValue(typeof(MapResizeModes), MapResizeModes.AutoFit)]
		public ReportExpression<MapResizeModes> ResizeMode
		{
			get
			{
				return base.PropertyStore.GetObject<ReportExpression<MapResizeModes>>(4);
			}
			set
			{
				base.PropertyStore.SetObject(4, value);
			}
		}

		// Token: 0x06000DE5 RID: 3557 RVA: 0x00022C30 File Offset: 0x00020E30
		public override void Initialize()
		{
			base.Initialize();
			this.ResizeMode = MapResizeModes.AutoFit;
		}

		// Token: 0x06000DE6 RID: 3558 RVA: 0x00022C44 File Offset: 0x00020E44
		protected override void GetDependenciesCore(IList<ReportObject> dependencies)
		{
			base.GetDependenciesCore(dependencies);
			Image.GetEmbeddedImgDependencies(base.GetAncestor<Report>(), dependencies, this.Source.Value, this.Value);
		}

		// Token: 0x020003D2 RID: 978
		internal class Definition : DefinitionStore<MapMarkerImage, MapMarkerImage.Definition.Properties>
		{
			// Token: 0x06001876 RID: 6262 RVA: 0x0003B731 File Offset: 0x00039931
			private Definition()
			{
			}

			// Token: 0x020004EA RID: 1258
			internal enum Properties
			{
				// Token: 0x04001019 RID: 4121
				Source,
				// Token: 0x0400101A RID: 4122
				Value,
				// Token: 0x0400101B RID: 4123
				MIMEType,
				// Token: 0x0400101C RID: 4124
				TransparentColor,
				// Token: 0x0400101D RID: 4125
				ResizeMode,
				// Token: 0x0400101E RID: 4126
				PropertyCount
			}
		}
	}
}

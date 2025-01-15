using System;
using System.ComponentModel;
using Microsoft.ReportingServices.RdlObjectModel;

namespace Microsoft.ReportingServices.RdlObjectModel2005
{
	// Token: 0x02000013 RID: 19
	internal class ThreeDProperties2005 : ChartThreeDProperties
	{
		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00002BF3 File Offset: 0x00000DF3
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00002C02 File Offset: 0x00000E02
		[DefaultValue(DrawingStyleTypes2005.Cube)]
		public DrawingStyleTypes2005 DrawingStyle
		{
			get
			{
				return (DrawingStyleTypes2005)base.PropertyStore.GetInteger(10);
			}
			set
			{
				base.PropertyStore.SetInteger(10, (int)value);
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x00002C12 File Offset: 0x00000E12
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00002C21 File Offset: 0x00000E21
		[DefaultValue(ProjectionModes2005.Perspective)]
		public new ProjectionModes2005 ProjectionMode
		{
			get
			{
				return (ProjectionModes2005)base.PropertyStore.GetInteger(11);
			}
			set
			{
				base.PropertyStore.SetInteger(11, (int)value);
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002C31 File Offset: 0x00000E31
		// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002C40 File Offset: 0x00000E40
		[DefaultValue(0)]
		public new int Rotation
		{
			get
			{
				return base.PropertyStore.GetInteger(12);
			}
			set
			{
				base.PropertyStore.SetInteger(12, value);
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000C7 RID: 199 RVA: 0x00002C50 File Offset: 0x00000E50
		// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002C5F File Offset: 0x00000E5F
		[DefaultValue(0)]
		public new int Inclination
		{
			get
			{
				return base.PropertyStore.GetInteger(13);
			}
			set
			{
				base.PropertyStore.SetInteger(13, value);
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000C9 RID: 201 RVA: 0x00002C6F File Offset: 0x00000E6F
		// (set) Token: 0x060000CA RID: 202 RVA: 0x00002C7E File Offset: 0x00000E7E
		[DefaultValue(0)]
		public new int WallThickness
		{
			get
			{
				return base.PropertyStore.GetInteger(14);
			}
			set
			{
				base.PropertyStore.SetInteger(14, value);
			}
		}

		// Token: 0x060000CB RID: 203 RVA: 0x00002C8E File Offset: 0x00000E8E
		public ThreeDProperties2005()
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00002C96 File Offset: 0x00000E96
		public override void Initialize()
		{
			base.Initialize();
			this.ProjectionMode = ProjectionModes2005.Perspective;
			base.Shading = ChartShadings.None;
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00002CB1 File Offset: 0x00000EB1
		public ThreeDProperties2005(IPropertyStore propertyStore)
			: base(propertyStore)
		{
		}

		// Token: 0x020002FC RID: 764
		internal new class Definition : DefinitionStore<ThreeDProperties2005, ThreeDProperties2005.Definition.Properties>
		{
			// Token: 0x060016F8 RID: 5880 RVA: 0x0003645A File Offset: 0x0003465A
			private Definition()
			{
			}

			// Token: 0x02000430 RID: 1072
			public enum Properties
			{
				// Token: 0x0400084F RID: 2127
				DrawingStyle = 10,
				// Token: 0x04000850 RID: 2128
				ProjectionMode,
				// Token: 0x04000851 RID: 2129
				Rotation,
				// Token: 0x04000852 RID: 2130
				Inclination,
				// Token: 0x04000853 RID: 2131
				WallThickness
			}
		}
	}
}

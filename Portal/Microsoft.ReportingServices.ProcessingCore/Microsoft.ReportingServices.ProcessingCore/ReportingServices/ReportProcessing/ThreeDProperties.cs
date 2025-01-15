using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000704 RID: 1796
	[Serializable]
	internal sealed class ThreeDProperties
	{
		// Token: 0x17002374 RID: 9076
		// (get) Token: 0x0600642E RID: 25646 RVA: 0x0018D455 File Offset: 0x0018B655
		// (set) Token: 0x0600642F RID: 25647 RVA: 0x0018D45D File Offset: 0x0018B65D
		internal bool Enabled
		{
			get
			{
				return this.m_enabled;
			}
			set
			{
				this.m_enabled = value;
			}
		}

		// Token: 0x17002375 RID: 9077
		// (get) Token: 0x06006430 RID: 25648 RVA: 0x0018D466 File Offset: 0x0018B666
		// (set) Token: 0x06006431 RID: 25649 RVA: 0x0018D46E File Offset: 0x0018B66E
		internal bool PerspectiveProjectionMode
		{
			get
			{
				return this.m_perspectiveProjectionMode;
			}
			set
			{
				this.m_perspectiveProjectionMode = value;
			}
		}

		// Token: 0x17002376 RID: 9078
		// (get) Token: 0x06006432 RID: 25650 RVA: 0x0018D477 File Offset: 0x0018B677
		// (set) Token: 0x06006433 RID: 25651 RVA: 0x0018D47F File Offset: 0x0018B67F
		internal int Rotation
		{
			get
			{
				return this.m_rotation;
			}
			set
			{
				this.m_rotation = value;
			}
		}

		// Token: 0x17002377 RID: 9079
		// (get) Token: 0x06006434 RID: 25652 RVA: 0x0018D488 File Offset: 0x0018B688
		// (set) Token: 0x06006435 RID: 25653 RVA: 0x0018D490 File Offset: 0x0018B690
		internal int Inclination
		{
			get
			{
				return this.m_inclination;
			}
			set
			{
				this.m_inclination = value;
			}
		}

		// Token: 0x17002378 RID: 9080
		// (get) Token: 0x06006436 RID: 25654 RVA: 0x0018D499 File Offset: 0x0018B699
		// (set) Token: 0x06006437 RID: 25655 RVA: 0x0018D4A1 File Offset: 0x0018B6A1
		internal int Perspective
		{
			get
			{
				return this.m_perspective;
			}
			set
			{
				this.m_perspective = value;
			}
		}

		// Token: 0x17002379 RID: 9081
		// (get) Token: 0x06006438 RID: 25656 RVA: 0x0018D4AA File Offset: 0x0018B6AA
		// (set) Token: 0x06006439 RID: 25657 RVA: 0x0018D4B2 File Offset: 0x0018B6B2
		internal int HeightRatio
		{
			get
			{
				return this.m_heightRatio;
			}
			set
			{
				this.m_heightRatio = value;
			}
		}

		// Token: 0x1700237A RID: 9082
		// (get) Token: 0x0600643A RID: 25658 RVA: 0x0018D4BB File Offset: 0x0018B6BB
		// (set) Token: 0x0600643B RID: 25659 RVA: 0x0018D4C3 File Offset: 0x0018B6C3
		internal int DepthRatio
		{
			get
			{
				return this.m_depthRatio;
			}
			set
			{
				this.m_depthRatio = value;
			}
		}

		// Token: 0x1700237B RID: 9083
		// (get) Token: 0x0600643C RID: 25660 RVA: 0x0018D4CC File Offset: 0x0018B6CC
		// (set) Token: 0x0600643D RID: 25661 RVA: 0x0018D4D4 File Offset: 0x0018B6D4
		internal ThreeDProperties.ShadingTypes Shading
		{
			get
			{
				return this.m_shading;
			}
			set
			{
				this.m_shading = value;
			}
		}

		// Token: 0x1700237C RID: 9084
		// (get) Token: 0x0600643E RID: 25662 RVA: 0x0018D4DD File Offset: 0x0018B6DD
		// (set) Token: 0x0600643F RID: 25663 RVA: 0x0018D4E5 File Offset: 0x0018B6E5
		internal int GapDepth
		{
			get
			{
				return this.m_gapDepth;
			}
			set
			{
				this.m_gapDepth = value;
			}
		}

		// Token: 0x1700237D RID: 9085
		// (get) Token: 0x06006440 RID: 25664 RVA: 0x0018D4EE File Offset: 0x0018B6EE
		// (set) Token: 0x06006441 RID: 25665 RVA: 0x0018D4F6 File Offset: 0x0018B6F6
		internal int WallThickness
		{
			get
			{
				return this.m_wallThickness;
			}
			set
			{
				this.m_wallThickness = value;
			}
		}

		// Token: 0x1700237E RID: 9086
		// (get) Token: 0x06006442 RID: 25666 RVA: 0x0018D4FF File Offset: 0x0018B6FF
		// (set) Token: 0x06006443 RID: 25667 RVA: 0x0018D507 File Offset: 0x0018B707
		internal bool DrawingStyleCube
		{
			get
			{
				return this.m_drawingStyleCube;
			}
			set
			{
				this.m_drawingStyleCube = value;
			}
		}

		// Token: 0x1700237F RID: 9087
		// (get) Token: 0x06006444 RID: 25668 RVA: 0x0018D510 File Offset: 0x0018B710
		// (set) Token: 0x06006445 RID: 25669 RVA: 0x0018D518 File Offset: 0x0018B718
		internal bool Clustered
		{
			get
			{
				return this.m_clustered;
			}
			set
			{
				this.m_clustered = value;
			}
		}

		// Token: 0x06006446 RID: 25670 RVA: 0x0018D521 File Offset: 0x0018B721
		internal void Initialize(InitializationContext context)
		{
		}

		// Token: 0x06006447 RID: 25671 RVA: 0x0018D524 File Offset: 0x0018B724
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Enabled, Token.Boolean),
				new MemberInfo(MemberName.PerspectiveProjectionMode, Token.Boolean),
				new MemberInfo(MemberName.Rotation, Token.Int32),
				new MemberInfo(MemberName.Inclination, Token.Int32),
				new MemberInfo(MemberName.Perspective, Token.Int32),
				new MemberInfo(MemberName.HeightRatio, Token.Int32),
				new MemberInfo(MemberName.DepthRatio, Token.Int32),
				new MemberInfo(MemberName.Shading, Token.Enum),
				new MemberInfo(MemberName.GapDepth, Token.Int32),
				new MemberInfo(MemberName.WallThickness, Token.Int32),
				new MemberInfo(MemberName.DrawingStyleCube, Token.Boolean),
				new MemberInfo(MemberName.Clustered, Token.Boolean)
			});
		}

		// Token: 0x04003243 RID: 12867
		private bool m_enabled;

		// Token: 0x04003244 RID: 12868
		private bool m_perspectiveProjectionMode = true;

		// Token: 0x04003245 RID: 12869
		private int m_rotation;

		// Token: 0x04003246 RID: 12870
		private int m_inclination;

		// Token: 0x04003247 RID: 12871
		private int m_perspective;

		// Token: 0x04003248 RID: 12872
		private int m_heightRatio;

		// Token: 0x04003249 RID: 12873
		private int m_depthRatio;

		// Token: 0x0400324A RID: 12874
		private ThreeDProperties.ShadingTypes m_shading;

		// Token: 0x0400324B RID: 12875
		private int m_gapDepth;

		// Token: 0x0400324C RID: 12876
		private int m_wallThickness;

		// Token: 0x0400324D RID: 12877
		private bool m_drawingStyleCube = true;

		// Token: 0x0400324E RID: 12878
		private bool m_clustered;

		// Token: 0x02000CD4 RID: 3284
		internal enum ShadingTypes
		{
			// Token: 0x04004EF0 RID: 20208
			None,
			// Token: 0x04004EF1 RID: 20209
			Simple,
			// Token: 0x04004EF2 RID: 20210
			Real
		}
	}
}

using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006F7 RID: 1783
	[Serializable]
	internal sealed class TableColumn
	{
		// Token: 0x170022FF RID: 8959
		// (get) Token: 0x060062FE RID: 25342 RVA: 0x00189FEC File Offset: 0x001881EC
		// (set) Token: 0x060062FF RID: 25343 RVA: 0x00189FF4 File Offset: 0x001881F4
		internal string Width
		{
			get
			{
				return this.m_width;
			}
			set
			{
				this.m_width = value;
			}
		}

		// Token: 0x17002300 RID: 8960
		// (get) Token: 0x06006300 RID: 25344 RVA: 0x00189FFD File Offset: 0x001881FD
		// (set) Token: 0x06006301 RID: 25345 RVA: 0x0018A005 File Offset: 0x00188205
		internal double WidthValue
		{
			get
			{
				return this.m_widthValue;
			}
			set
			{
				this.m_widthValue = value;
			}
		}

		// Token: 0x17002301 RID: 8961
		// (get) Token: 0x06006302 RID: 25346 RVA: 0x0018A00E File Offset: 0x0018820E
		// (set) Token: 0x06006303 RID: 25347 RVA: 0x0018A016 File Offset: 0x00188216
		internal Visibility Visibility
		{
			get
			{
				return this.m_visibility;
			}
			set
			{
				this.m_visibility = value;
			}
		}

		// Token: 0x17002302 RID: 8962
		// (get) Token: 0x06006304 RID: 25348 RVA: 0x0018A01F File Offset: 0x0018821F
		// (set) Token: 0x06006305 RID: 25349 RVA: 0x0018A027 File Offset: 0x00188227
		internal ReportSize WidthForRendering
		{
			get
			{
				return this.m_widthForRendering;
			}
			set
			{
				this.m_widthForRendering = value;
			}
		}

		// Token: 0x17002303 RID: 8963
		// (get) Token: 0x06006306 RID: 25350 RVA: 0x0018A030 File Offset: 0x00188230
		// (set) Token: 0x06006307 RID: 25351 RVA: 0x0018A038 File Offset: 0x00188238
		internal bool FixedHeader
		{
			get
			{
				return this.m_fixedHeader;
			}
			set
			{
				this.m_fixedHeader = value;
			}
		}

		// Token: 0x06006308 RID: 25352 RVA: 0x0018A041 File Offset: 0x00188241
		internal void Initialize(InitializationContext context)
		{
			this.m_widthValue = context.ValidateSize(ref this.m_width, "Width");
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, false, true);
			}
		}

		// Token: 0x06006309 RID: 25353 RVA: 0x0018A071 File Offset: 0x00188271
		internal void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, false);
			}
		}

		// Token: 0x0600630A RID: 25354 RVA: 0x0018A08C File Offset: 0x0018828C
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.Width, Token.String),
				new MemberInfo(MemberName.WidthValue, Token.Double),
				new MemberInfo(MemberName.Visibility, ObjectType.Visibility),
				new MemberInfo(MemberName.FixedHeader, Token.Boolean)
			});
		}

		// Token: 0x040031DE RID: 12766
		private string m_width;

		// Token: 0x040031DF RID: 12767
		private double m_widthValue;

		// Token: 0x040031E0 RID: 12768
		private Visibility m_visibility;

		// Token: 0x040031E1 RID: 12769
		private bool m_fixedHeader;

		// Token: 0x040031E2 RID: 12770
		[NonSerialized]
		private ReportSize m_widthForRendering;
	}
}

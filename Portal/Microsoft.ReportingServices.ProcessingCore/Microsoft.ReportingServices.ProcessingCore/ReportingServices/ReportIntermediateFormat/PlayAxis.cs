using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000476 RID: 1142
	[Serializable]
	internal sealed class PlayAxis : Navigation
	{
		// Token: 0x17001770 RID: 6000
		// (get) Token: 0x06003487 RID: 13447 RVA: 0x000E747A File Offset: 0x000E567A
		// (set) Token: 0x06003488 RID: 13448 RVA: 0x000E7482 File Offset: 0x000E5682
		internal Slider Slider
		{
			get
			{
				return this.m_slider;
			}
			set
			{
				this.m_slider = value;
			}
		}

		// Token: 0x17001771 RID: 6001
		// (get) Token: 0x06003489 RID: 13449 RVA: 0x000E748B File Offset: 0x000E568B
		// (set) Token: 0x0600348A RID: 13450 RVA: 0x000E7493 File Offset: 0x000E5693
		internal DockingOption DockingOption
		{
			get
			{
				return this.m_dockingOption;
			}
			set
			{
				this.m_dockingOption = value;
			}
		}

		// Token: 0x0600348B RID: 13451 RVA: 0x000E749C File Offset: 0x000E569C
		internal override void Initialize(Microsoft.ReportingServices.ReportIntermediateFormat.Tablix tablix, InitializationContext context)
		{
			if (this.m_slider != null)
			{
				this.m_slider.Initialize(tablix, context);
			}
		}

		// Token: 0x0600348C RID: 13452 RVA: 0x000E74B4 File Offset: 0x000E56B4
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PlayAxis, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Navigation, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Slider, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Slider),
				new MemberInfo(MemberName.DockingOption, Token.Enum)
			});
		}

		// Token: 0x0600348D RID: 13453 RVA: 0x000E7500 File Offset: 0x000E5700
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(PlayAxis.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DockingOption)
				{
					if (memberName == MemberName.Slider)
					{
						writer.Write(this.m_slider);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.WriteEnum((int)this.m_dockingOption);
				}
			}
		}

		// Token: 0x0600348E RID: 13454 RVA: 0x000E7574 File Offset: 0x000E5774
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(PlayAxis.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.DockingOption)
				{
					if (memberName == MemberName.Slider)
					{
						this.m_slider = reader.ReadRIFObject<Slider>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_dockingOption = (DockingOption)reader.ReadEnum();
				}
			}
		}

		// Token: 0x0600348F RID: 13455 RVA: 0x000E75E5 File Offset: 0x000E57E5
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003490 RID: 13456 RVA: 0x000E75E7 File Offset: 0x000E57E7
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PlayAxis;
		}

		// Token: 0x04001A0B RID: 6667
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = PlayAxis.GetDeclaration();

		// Token: 0x04001A0C RID: 6668
		private Slider m_slider;

		// Token: 0x04001A0D RID: 6669
		private DockingOption m_dockingOption;
	}
}

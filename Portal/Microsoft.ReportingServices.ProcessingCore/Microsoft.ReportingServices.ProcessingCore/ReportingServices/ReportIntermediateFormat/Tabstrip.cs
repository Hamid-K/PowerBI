using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000475 RID: 1141
	[Serializable]
	internal sealed class Tabstrip : Navigation
	{
		// Token: 0x1700176E RID: 5998
		// (get) Token: 0x0600347B RID: 13435 RVA: 0x000E72D2 File Offset: 0x000E54D2
		// (set) Token: 0x0600347C RID: 13436 RVA: 0x000E72DA File Offset: 0x000E54DA
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

		// Token: 0x1700176F RID: 5999
		// (get) Token: 0x0600347D RID: 13437 RVA: 0x000E72E3 File Offset: 0x000E54E3
		// (set) Token: 0x0600347E RID: 13438 RVA: 0x000E72EB File Offset: 0x000E54EB
		internal NavigationItem NavigationItem
		{
			get
			{
				return this.m_navigationItem;
			}
			set
			{
				this.m_navigationItem = value;
			}
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x000E72F4 File Offset: 0x000E54F4
		internal override void Initialize(Tablix tablix, InitializationContext context)
		{
			if (this.m_slider != null)
			{
				this.m_slider.Initialize(tablix, context);
			}
			if (this.m_navigationItem != null)
			{
				this.m_navigationItem.Initialize(tablix, context, "Tabstrip");
			}
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x000E7328 File Offset: 0x000E5528
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tabstrip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Navigation, new List<MemberInfo>
			{
				new MemberInfo(MemberName.NavigationItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NavigationItem),
				new MemberInfo(MemberName.Slider, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Slider)
			});
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x000E7378 File Offset: 0x000E5578
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Tabstrip.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Slider)
				{
					if (memberName == MemberName.NavigationItem)
					{
						writer.Write(this.m_navigationItem);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					writer.Write(this.m_slider);
				}
			}
		}

		// Token: 0x06003482 RID: 13442 RVA: 0x000E73EC File Offset: 0x000E55EC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Tabstrip.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Slider)
				{
					if (memberName == MemberName.NavigationItem)
					{
						this.m_navigationItem = reader.ReadRIFObject<NavigationItem>();
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
				else
				{
					this.m_slider = reader.ReadRIFObject<Slider>();
				}
			}
		}

		// Token: 0x06003483 RID: 13443 RVA: 0x000E745D File Offset: 0x000E565D
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003484 RID: 13444 RVA: 0x000E745F File Offset: 0x000E565F
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Tabstrip;
		}

		// Token: 0x04001A08 RID: 6664
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Tabstrip.GetDeclaration();

		// Token: 0x04001A09 RID: 6665
		private NavigationItem m_navigationItem;

		// Token: 0x04001A0A RID: 6666
		private Slider m_slider;
	}
}

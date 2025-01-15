using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000474 RID: 1140
	[Serializable]
	internal sealed class Coverflow : Navigation
	{
		// Token: 0x1700176C RID: 5996
		// (get) Token: 0x0600346F RID: 13423 RVA: 0x000E712D File Offset: 0x000E532D
		// (set) Token: 0x06003470 RID: 13424 RVA: 0x000E7135 File Offset: 0x000E5335
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

		// Token: 0x1700176D RID: 5997
		// (get) Token: 0x06003471 RID: 13425 RVA: 0x000E713E File Offset: 0x000E533E
		// (set) Token: 0x06003472 RID: 13426 RVA: 0x000E7146 File Offset: 0x000E5346
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

		// Token: 0x06003473 RID: 13427 RVA: 0x000E714F File Offset: 0x000E534F
		internal override void Initialize(Tablix tablix, InitializationContext context)
		{
			if (this.m_slider != null)
			{
				this.m_slider.Initialize(tablix, context);
			}
			if (this.m_navigationItem != null)
			{
				this.m_navigationItem.Initialize(tablix, context, "Coverflow");
			}
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x000E7180 File Offset: 0x000E5380
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Coverflow, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Navigation, new List<MemberInfo>
			{
				new MemberInfo(MemberName.NavigationItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NavigationItem),
				new MemberInfo(MemberName.Slider, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Slider)
			});
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x000E71D0 File Offset: 0x000E53D0
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Coverflow.m_Declaration);
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

		// Token: 0x06003476 RID: 13430 RVA: 0x000E7244 File Offset: 0x000E5444
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Coverflow.m_Declaration);
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

		// Token: 0x06003477 RID: 13431 RVA: 0x000E72B5 File Offset: 0x000E54B5
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x000E72B7 File Offset: 0x000E54B7
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Coverflow;
		}

		// Token: 0x04001A05 RID: 6661
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Coverflow.GetDeclaration();

		// Token: 0x04001A06 RID: 6662
		private NavigationItem m_navigationItem;

		// Token: 0x04001A07 RID: 6663
		private Slider m_slider;
	}
}

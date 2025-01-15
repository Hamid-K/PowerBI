using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000473 RID: 1139
	[Serializable]
	internal sealed class NavigationItem : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x1700176A RID: 5994
		// (get) Token: 0x06003463 RID: 13411 RVA: 0x000E6F65 File Offset: 0x000E5165
		// (set) Token: 0x06003464 RID: 13412 RVA: 0x000E6F6D File Offset: 0x000E516D
		internal string ReportItemReference
		{
			get
			{
				return this.m_reportItemReference;
			}
			set
			{
				this.m_reportItemReference = value;
			}
		}

		// Token: 0x1700176B RID: 5995
		// (get) Token: 0x06003465 RID: 13413 RVA: 0x000E6F76 File Offset: 0x000E5176
		// (set) Token: 0x06003466 RID: 13414 RVA: 0x000E6F7E File Offset: 0x000E517E
		internal BandNavigationCell BandNavigationCell
		{
			get
			{
				return this.m_bandNavigationCell;
			}
			set
			{
				this.m_bandNavigationCell = value;
			}
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x000E6F88 File Offset: 0x000E5188
		internal void Initialize(Tablix tablix, InitializationContext context, string NavigationType)
		{
			if (this.ReportItemReference != null && !tablix.ValidateBandReportItemReference(this.ReportItemReference))
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsInvalidBandNavigationReference, Severity.Error, context.ObjectType, context.ObjectName, NavigationType, new string[] { this.ReportItemReference });
			}
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x000E6FDC File Offset: 0x000E51DC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NavigationItem, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReportItemReference, Token.String),
				new MemberInfo(MemberName.BandNavigationCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BandNavigationCell)
			});
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x000E7028 File Offset: 0x000E5228
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(NavigationItem.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ReportItemReference)
				{
					if (memberName != MemberName.BandNavigationCell)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_bandNavigationCell);
					}
				}
				else
				{
					writer.Write(this.m_reportItemReference);
				}
			}
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x000E7094 File Offset: 0x000E5294
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(NavigationItem.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ReportItemReference)
				{
					if (memberName != MemberName.BandNavigationCell)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_bandNavigationCell = (BandNavigationCell)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_reportItemReference = reader.ReadString();
				}
			}
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x000E7105 File Offset: 0x000E5305
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x000E7112 File Offset: 0x000E5312
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.NavigationItem;
		}

		// Token: 0x04001A02 RID: 6658
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = NavigationItem.GetDeclaration();

		// Token: 0x04001A03 RID: 6659
		private string m_reportItemReference;

		// Token: 0x04001A04 RID: 6660
		private BandNavigationCell m_bandNavigationCell;
	}
}

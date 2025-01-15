using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000471 RID: 1137
	[Serializable]
	internal sealed class BandLayoutOptions : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001767 RID: 5991
		// (get) Token: 0x0600344D RID: 13389 RVA: 0x000E6CD6 File Offset: 0x000E4ED6
		// (set) Token: 0x0600344E RID: 13390 RVA: 0x000E6CDE File Offset: 0x000E4EDE
		internal int RowCount
		{
			get
			{
				return this.m_rowCount;
			}
			set
			{
				this.m_rowCount = value;
			}
		}

		// Token: 0x17001768 RID: 5992
		// (get) Token: 0x0600344F RID: 13391 RVA: 0x000E6CE7 File Offset: 0x000E4EE7
		// (set) Token: 0x06003450 RID: 13392 RVA: 0x000E6CEF File Offset: 0x000E4EEF
		internal int ColumnCount
		{
			get
			{
				return this.m_columnCount;
			}
			set
			{
				this.m_columnCount = value;
			}
		}

		// Token: 0x17001769 RID: 5993
		// (get) Token: 0x06003451 RID: 13393 RVA: 0x000E6CF8 File Offset: 0x000E4EF8
		// (set) Token: 0x06003452 RID: 13394 RVA: 0x000E6D00 File Offset: 0x000E4F00
		internal Navigation Navigation
		{
			get
			{
				return this.m_navigation;
			}
			set
			{
				this.m_navigation = value;
			}
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x000E6D09 File Offset: 0x000E4F09
		internal void Initialize(Tablix tablix, InitializationContext context)
		{
			if (this.m_navigation != null)
			{
				this.m_navigation.Initialize(tablix, context);
			}
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x000E6D20 File Offset: 0x000E4F20
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BandLayoutOptions, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.RowCount, Token.Int32),
				new MemberInfo(MemberName.ColumnCount, Token.Int32),
				new MemberInfo(MemberName.Navigation, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Navigation)
			});
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x000E6D80 File Offset: 0x000E4F80
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(BandLayoutOptions.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ColumnCount)
				{
					if (memberName != MemberName.RowCount)
					{
						if (memberName != MemberName.Navigation)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_navigation);
						}
					}
					else
					{
						writer.Write(this.m_rowCount);
					}
				}
				else
				{
					writer.Write(this.m_columnCount);
				}
			}
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x000E6E04 File Offset: 0x000E5004
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(BandLayoutOptions.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ColumnCount)
				{
					if (memberName != MemberName.RowCount)
					{
						if (memberName != MemberName.Navigation)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_navigation = (Navigation)reader.ReadRIFObject();
						}
					}
					else
					{
						this.m_rowCount = reader.ReadInt32();
					}
				}
				else
				{
					this.m_columnCount = reader.ReadInt32();
				}
			}
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x000E6E8C File Offset: 0x000E508C
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x000E6E8E File Offset: 0x000E508E
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BandLayoutOptions;
		}

		// Token: 0x040019FD RID: 6653
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = BandLayoutOptions.GetDeclaration();

		// Token: 0x040019FE RID: 6654
		private int m_rowCount = 1;

		// Token: 0x040019FF RID: 6655
		private int m_columnCount = 1;

		// Token: 0x04001A00 RID: 6656
		private Navigation m_navigation;
	}
}

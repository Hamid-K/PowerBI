using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000415 RID: 1045
	internal class CommonSubReportInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002D5A RID: 11610 RVA: 0x000CFB79 File Offset: 0x000CDD79
		internal CommonSubReportInfo()
		{
		}

		// Token: 0x170015B7 RID: 5559
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x000CFB81 File Offset: 0x000CDD81
		// (set) Token: 0x06002D5C RID: 11612 RVA: 0x000CFB89 File Offset: 0x000CDD89
		internal string ReportPath
		{
			get
			{
				return this.m_reportPath;
			}
			set
			{
				this.m_reportPath = value;
			}
		}

		// Token: 0x170015B8 RID: 5560
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x000CFB92 File Offset: 0x000CDD92
		// (set) Token: 0x06002D5E RID: 11614 RVA: 0x000CFB9A File Offset: 0x000CDD9A
		internal string OriginalCatalogPath
		{
			get
			{
				return this.m_originalCatalogPath;
			}
			set
			{
				this.m_originalCatalogPath = value;
			}
		}

		// Token: 0x170015B9 RID: 5561
		// (get) Token: 0x06002D5F RID: 11615 RVA: 0x000CFBA3 File Offset: 0x000CDDA3
		// (set) Token: 0x06002D60 RID: 11616 RVA: 0x000CFBAB File Offset: 0x000CDDAB
		internal string Description
		{
			get
			{
				return this.m_description;
			}
			set
			{
				this.m_description = value;
			}
		}

		// Token: 0x170015BA RID: 5562
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x000CFBB4 File Offset: 0x000CDDB4
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x000CFBBC File Offset: 0x000CDDBC
		internal ParameterInfoCollection ParametersFromCatalog
		{
			get
			{
				return this.m_parametersFromCatalog;
			}
			set
			{
				this.m_parametersFromCatalog = value;
			}
		}

		// Token: 0x170015BB RID: 5563
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x000CFBC5 File Offset: 0x000CDDC5
		// (set) Token: 0x06002D64 RID: 11620 RVA: 0x000CFBCD File Offset: 0x000CDDCD
		internal bool RetrievalFailed
		{
			get
			{
				return this.m_retrievalFailed;
			}
			set
			{
				this.m_retrievalFailed = value;
			}
		}

		// Token: 0x170015BC RID: 5564
		// (get) Token: 0x06002D65 RID: 11621 RVA: 0x000CFBD6 File Offset: 0x000CDDD6
		// (set) Token: 0x06002D66 RID: 11622 RVA: 0x000CFBDE File Offset: 0x000CDDDE
		internal string DefinitionUniqueName
		{
			get
			{
				return this.m_definitionUniqueName;
			}
			set
			{
				this.m_definitionUniqueName = value;
			}
		}

		// Token: 0x170015BD RID: 5565
		// (get) Token: 0x06002D67 RID: 11623 RVA: 0x000CFBE7 File Offset: 0x000CDDE7
		// (set) Token: 0x06002D68 RID: 11624 RVA: 0x000CFBEF File Offset: 0x000CDDEF
		internal IChunkFactory DefinitionChunkFactory
		{
			get
			{
				return this.m_definitionChunkFactory;
			}
			set
			{
				this.m_definitionChunkFactory = value;
			}
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000CFBF8 File Offset: 0x000CDDF8
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CommonSubReportInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ReportPath, Token.String),
				new MemberInfo(MemberName.ParametersFromCatalog, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterInfo),
				new MemberInfo(MemberName.RetrievalFailed, Token.Boolean),
				new MemberInfo(MemberName.Description, Token.String),
				new MemberInfo(MemberName.DefinitionUniqueName, Token.String),
				new MemberInfo(MemberName.OriginalCatalogPath, Token.String)
			});
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000CFC90 File Offset: 0x000CDE90
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(CommonSubReportInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Description)
				{
					if (memberName == MemberName.ReportPath)
					{
						writer.Write(this.m_reportPath);
						continue;
					}
					if (memberName == MemberName.ParametersFromCatalog)
					{
						writer.Write(this.m_parametersFromCatalog);
						continue;
					}
					if (memberName == MemberName.Description)
					{
						writer.Write(this.m_description);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RetrievalFailed)
					{
						writer.Write(this.m_retrievalFailed);
						continue;
					}
					if (memberName == MemberName.DefinitionUniqueName)
					{
						writer.Write(this.m_definitionUniqueName);
						continue;
					}
					if (memberName == MemberName.OriginalCatalogPath)
					{
						writer.Write(this.m_originalCatalogPath);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000CFD64 File Offset: 0x000CDF64
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(CommonSubReportInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Description)
				{
					if (memberName == MemberName.ReportPath)
					{
						this.m_reportPath = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.ParametersFromCatalog)
					{
						this.m_parametersFromCatalog = reader.ReadListOfRIFObjects<ParameterInfoCollection>();
						continue;
					}
					if (memberName == MemberName.Description)
					{
						this.m_description = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.RetrievalFailed)
					{
						this.m_retrievalFailed = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.DefinitionUniqueName)
					{
						this.m_definitionUniqueName = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.OriginalCatalogPath)
					{
						this.m_originalCatalogPath = reader.ReadString();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
			if (this.m_originalCatalogPath == null)
			{
				this.m_originalCatalogPath = this.m_reportPath;
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000CFE4A File Offset: 0x000CE04A
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000CFE57 File Offset: 0x000CE057
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CommonSubReportInfo;
		}

		// Token: 0x0400182E RID: 6190
		private string m_description;

		// Token: 0x0400182F RID: 6191
		private string m_reportPath;

		// Token: 0x04001830 RID: 6192
		private string m_originalCatalogPath;

		// Token: 0x04001831 RID: 6193
		private ParameterInfoCollection m_parametersFromCatalog;

		// Token: 0x04001832 RID: 6194
		private bool m_retrievalFailed;

		// Token: 0x04001833 RID: 6195
		private string m_definitionUniqueName;

		// Token: 0x04001834 RID: 6196
		[NonSerialized]
		private IChunkFactory m_definitionChunkFactory;

		// Token: 0x04001835 RID: 6197
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = CommonSubReportInfo.GetDeclaration();
	}
}

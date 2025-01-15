using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000524 RID: 1316
	internal sealed class TablixCell : TablixCellBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x060046F2 RID: 18162 RVA: 0x00129D94 File Offset: 0x00127F94
		internal TablixCell()
		{
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x00129DA3 File Offset: 0x00127FA3
		internal TablixCell(int id, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegion)
			: base(id, dataRegion)
		{
		}

		// Token: 0x17001D8D RID: 7565
		// (get) Token: 0x060046F4 RID: 18164 RVA: 0x00129DB4 File Offset: 0x00127FB4
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17001D8E RID: 7566
		// (get) Token: 0x060046F5 RID: 18165 RVA: 0x00129DB7 File Offset: 0x00127FB7
		// (set) Token: 0x060046F6 RID: 18166 RVA: 0x00129DBF File Offset: 0x00127FBF
		internal string DataElementName
		{
			get
			{
				return this.m_dataElementName;
			}
			set
			{
				this.m_dataElementName = value;
			}
		}

		// Token: 0x17001D8F RID: 7567
		// (get) Token: 0x060046F7 RID: 18167 RVA: 0x00129DC8 File Offset: 0x00127FC8
		// (set) Token: 0x060046F8 RID: 18168 RVA: 0x00129DD0 File Offset: 0x00127FD0
		internal DataElementOutputTypes DataElementOutput
		{
			get
			{
				return this.m_dataElementOutput;
			}
			set
			{
				this.m_dataElementOutput = value;
			}
		}

		// Token: 0x17001D90 RID: 7568
		// (get) Token: 0x060046F9 RID: 18169 RVA: 0x00129DD9 File Offset: 0x00127FD9
		// (set) Token: 0x060046FA RID: 18170 RVA: 0x00129DE1 File Offset: 0x00127FE1
		internal string CellIDForRendering
		{
			get
			{
				return this.m_cellIDForRendering;
			}
			set
			{
				this.m_cellIDForRendering = value;
			}
		}

		// Token: 0x17001D91 RID: 7569
		// (get) Token: 0x060046FB RID: 18171 RVA: 0x00129DEA File Offset: 0x00127FEA
		// (set) Token: 0x060046FC RID: 18172 RVA: 0x00129DF2 File Offset: 0x00127FF2
		internal ReportSize CellWidthForRendering
		{
			get
			{
				return this.m_cellWidthForRendering;
			}
			set
			{
				this.m_cellWidthForRendering = value;
			}
		}

		// Token: 0x17001D92 RID: 7570
		// (get) Token: 0x060046FD RID: 18173 RVA: 0x00129DFB File Offset: 0x00127FFB
		// (set) Token: 0x060046FE RID: 18174 RVA: 0x00129E03 File Offset: 0x00128003
		internal ReportSize CellHeightForRendering
		{
			get
			{
				return this.m_cellHeightForRendering;
			}
			set
			{
				this.m_cellHeightForRendering = value;
			}
		}

		// Token: 0x17001D93 RID: 7571
		// (get) Token: 0x060046FF RID: 18175 RVA: 0x00129E0C File Offset: 0x0012800C
		// (set) Token: 0x06004700 RID: 18176 RVA: 0x00129E14 File Offset: 0x00128014
		internal StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				return this.m_structureTypeOverwrite;
			}
			set
			{
				this.m_structureTypeOverwrite = value;
			}
		}

		// Token: 0x06004701 RID: 18177 RVA: 0x00129E1D File Offset: 0x0012801D
		internal override void DataRendererInitialize(InitializationContext context)
		{
			if (this.m_dataElementOutput == DataElementOutputTypes.Auto)
			{
				this.m_dataElementOutput = DataElementOutputTypes.ContentsOnly;
			}
			Microsoft.ReportingServices.ReportPublishing.CLSNameValidator.ValidateDataElementName(ref this.m_dataElementName, "Cell", context.ObjectType, context.ObjectName, "DataElementName", context.ErrorContext);
		}

		// Token: 0x06004702 RID: 18178 RVA: 0x00129E5C File Offset: 0x0012805C
		internal void InitializeRVDirectionDependentItems(InitializationContext context)
		{
			bool flag = false;
			if (context.HasUserSorts)
			{
				context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegionCellTopLevelItem | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InTablixCell;
				if (context.IsDataRegionCellScope)
				{
					flag = true;
					context.RegisterIndividualCellScope(this);
				}
			}
			if (this.m_cellContents != null)
			{
				this.m_cellContents.InitializeRVDirectionDependentItems(context);
			}
			if (this.m_altCellContents != null)
			{
				this.m_altCellContents.InitializeRVDirectionDependentItems(context);
			}
			if (flag)
			{
				context.UnRegisterIndividualCellScope(this);
			}
		}

		// Token: 0x06004703 RID: 18179 RVA: 0x00129ECD File Offset: 0x001280CD
		internal void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			if (this.m_cellContents != null)
			{
				this.m_cellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
			}
			if (this.m_altCellContents != null)
			{
				this.m_altCellContents.DetermineGroupingExprValueCount(context, groupingExprCount);
			}
		}

		// Token: 0x06004704 RID: 18180 RVA: 0x00129EFC File Offset: 0x001280FC
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			TablixCell tablixCell = (TablixCell)base.PublishClone(context);
			if (this.m_dataElementName != null)
			{
				tablixCell.m_dataElementName = (string)this.m_dataElementName.Clone();
			}
			return tablixCell;
		}

		// Token: 0x06004705 RID: 18181 RVA: 0x00129F38 File Offset: 0x00128138
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCellBase, new List<MemberInfo>
			{
				new MemberInfo(MemberName.DataElementName, Token.String),
				new MemberInfo(MemberName.DataElementOutput, Token.Enum),
				new MemberInfo(MemberName.StructureTypeOverwrite, Token.Enum)
			});
		}

		// Token: 0x06004706 RID: 18182 RVA: 0x00129F94 File Offset: 0x00128194
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(TablixCell.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.DataElementName)
				{
					if (memberName != MemberName.DataElementOutput)
					{
						if (memberName != MemberName.StructureTypeOverwrite)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.WriteEnum((int)this.m_structureTypeOverwrite);
						}
					}
					else
					{
						writer.WriteEnum((int)this.m_dataElementOutput);
					}
				}
				else
				{
					writer.Write(this.m_dataElementName);
				}
			}
		}

		// Token: 0x06004707 RID: 18183 RVA: 0x0012A020 File Offset: 0x00128220
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(TablixCell.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.DataElementName)
				{
					if (memberName != MemberName.DataElementOutput)
					{
						if (memberName != MemberName.StructureTypeOverwrite)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_structureTypeOverwrite = (StructureTypeOverwriteType)reader.ReadEnum();
						}
					}
					else
					{
						this.m_dataElementOutput = (DataElementOutputTypes)reader.ReadEnum();
					}
				}
				else
				{
					this.m_dataElementName = reader.ReadString();
				}
			}
		}

		// Token: 0x06004708 RID: 18184 RVA: 0x0012A0AA File Offset: 0x001282AA
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06004709 RID: 18185 RVA: 0x0012A0B4 File Offset: 0x001282B4
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TablixCell;
		}

		// Token: 0x0600470A RID: 18186 RVA: 0x0012A0BB File Offset: 0x001282BB
		internal void SetExprHost(TablixCellExprHost exprHost, ObjectModelImpl reportObjectModel)
		{
			base.BaseSetExprHost(exprHost, reportObjectModel);
		}

		// Token: 0x04001FC0 RID: 8128
		private string m_dataElementName;

		// Token: 0x04001FC1 RID: 8129
		private DataElementOutputTypes m_dataElementOutput = DataElementOutputTypes.Auto;

		// Token: 0x04001FC2 RID: 8130
		private StructureTypeOverwriteType m_structureTypeOverwrite;

		// Token: 0x04001FC3 RID: 8131
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = TablixCell.GetDeclaration();

		// Token: 0x04001FC4 RID: 8132
		[NonSerialized]
		private string m_cellIDForRendering;

		// Token: 0x04001FC5 RID: 8133
		[NonSerialized]
		private ReportSize m_cellWidthForRendering;

		// Token: 0x04001FC6 RID: 8134
		[NonSerialized]
		private ReportSize m_cellHeightForRendering;
	}
}

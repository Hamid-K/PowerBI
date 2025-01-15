using System;
using System.Diagnostics;
using System.IO;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200027A RID: 634
	public sealed class CustomReportItem : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, IDataRegion, IReportScope
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x00065671 File Offset: 0x00063871
		internal CustomReportItem(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
			this.m_indexIntoParentCollectionDef = indexIntoParentCollectionDef;
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0006568E File Offset: 0x0006388E
		internal CustomReportItem(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.CustomReportItem renderCri, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderCri, renderingContext)
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x000656A4 File Offset: 0x000638A4
		internal bool Initialize(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_exposeAs = null;
			if (renderingContext.IsRenderAsNativeCri(this.CriDef))
			{
				this.m_exposeAs = this;
			}
			else
			{
				if (!this.LoadGeneratedReportItemDefinition())
				{
					this.GenerateReportItemDefinition();
				}
				this.m_exposeAs = this.m_generatedReportItem;
			}
			return this.m_exposeAs != null;
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x000656F2 File Offset: 0x000638F2
		internal override Microsoft.ReportingServices.OnDemandReportRendering.ReportItem ExposeAs(Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			Global.Tracer.Assert(this.m_exposeAs != null, "m_exposeAs != null");
			return this.m_exposeAs;
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x00065714 File Offset: 0x00063914
		private bool LoadGeneratedReportItemDefinition()
		{
			string text;
			if (!base.RenderingContext.OdpContext.OdpMetadata.ReportSnapshot.TryGetGeneratedReportItemChunkName(this.GetGeneratedDefinitionChunkKey(), out text))
			{
				return false;
			}
			string text2;
			Stream chunk = base.RenderingContext.OdpContext.ChunkFactory.GetChunk(text, ReportProcessing.ReportChunkTypes.GeneratedReportItems, ChunkMode.Open, out text2);
			if (chunk == null)
			{
				return false;
			}
			using (chunk)
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader intermediateFormatReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(chunk, new ProcessingRIFObjectCreator((Microsoft.ReportingServices.ReportIntermediateFormat.IDOwner)this.m_reportItemDef.ParentInstancePath, this.m_reportItemDef.Parent));
				Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItem = (Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem)intermediateFormatReader.ReadRIFObject();
				Global.Tracer.Assert(!intermediateFormatReader.HasReferences, "!reader.HasReferences");
				reportItem.GlobalID = -this.CriDef.GlobalID;
				if (reportItem.StyleClass != null)
				{
					reportItem.StyleClass.InitializeForCRIGeneratedReportItem();
				}
				reportItem.Visibility = this.m_reportItemDef.Visibility;
				if (reportItem.ObjectType == Microsoft.ReportingServices.ReportProcessing.ObjectType.Image)
				{
					this.m_generatedReportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Image(this.ParentScope, base.ParentDefinitionPath, this.m_indexIntoParentCollectionDef, (Microsoft.ReportingServices.ReportIntermediateFormat.Image)reportItem, base.RenderingContext)
					{
						CriOwner = this,
						CriGenerationPhase = ReportElement.CriGenerationPhases.None
					};
				}
				else
				{
					Global.Tracer.Assert(false, "Unexpected CRI generated report item type: " + reportItem.ObjectType.ToString());
				}
			}
			return true;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00065894 File Offset: 0x00063A94
		private static string CreateChunkName()
		{
			return Guid.NewGuid().ToString("N");
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x000658B4 File Offset: 0x00063AB4
		private void GenerateReportItemDefinition()
		{
			this.m_generatedReportItem = null;
			ICustomReportItem controlInstance = base.RenderingContext.OdpContext.CriProcessingControls.GetControlInstance(this.CriDef.Type, base.RenderingContext.OdpContext.ExtFactory);
			if (controlInstance == null)
			{
				return;
			}
			try
			{
				controlInstance.GenerateReportItemDefinition(this);
			}
			catch (Exception ex)
			{
				base.RenderingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIProcessingError, Severity.Warning, new string[] { this.Name, this.Type });
				Global.Tracer.TraceException(TraceLevel.Error, RPResWrapper.rsCRIProcessingError(this.Name, this.Type) + " " + ex.ToString());
				return;
			}
			if (this.m_generatedReportItem == null)
			{
				base.RenderingContext.ErrorContext.Register(ProcessingErrorCode.rsCRIRenderItemNull, Severity.Warning, this.CriDef.ObjectType, this.Name, this.Type, Array.Empty<string>());
				return;
			}
			this.m_generatedReportItem.ConstructReportItemDefinition();
			this.m_generatedReportItem.CriGenerationPhase = ReportElement.CriGenerationPhases.None;
			string text = Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem.CreateChunkName();
			OnDemandProcessingContext odpContext = base.RenderingContext.OdpContext;
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot reportSnapshot = odpContext.OdpMetadata.ReportSnapshot;
			using (Stream stream = odpContext.ChunkFactory.CreateChunk(text, ReportProcessing.ReportChunkTypes.GeneratedReportItems, null))
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter intermediateFormatWriter = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(stream, odpContext.GetActiveCompatibilityVersion());
				Microsoft.ReportingServices.ReportIntermediateFormat.Visibility visibility = this.m_generatedReportItem.ReportItemDef.Visibility;
				this.m_generatedReportItem.ReportItemDef.Visibility = null;
				intermediateFormatWriter.Write(this.m_generatedReportItem.ReportItemDef);
				this.m_generatedReportItem.ReportItemDef.Visibility = visibility;
				stream.Flush();
			}
			reportSnapshot.AddGeneratedReportItemChunkName(this.GetGeneratedDefinitionChunkKey(), text);
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x00065A80 File Offset: 0x00063C80
		internal void EvaluateGeneratedReportItemInstance()
		{
			Global.Tracer.Assert(this.m_generatedReportItem.CriGenerationPhase == ReportElement.CriGenerationPhases.None);
			this.m_generatedReportItem.CriGenerationPhase = ReportElement.CriGenerationPhases.Instance;
			try
			{
				if (this.LoadGeneratedReportItemInstance())
				{
					return;
				}
				try
				{
					ICustomReportItem controlInstance = base.RenderingContext.OdpContext.CriProcessingControls.GetControlInstance(this.CriDef.Type, base.RenderingContext.OdpContext.ExtFactory);
					Global.Tracer.Assert(controlInstance != null, "(null != control)");
					controlInstance.EvaluateReportItemInstance(this);
					this.m_generatedReportItem.CompleteCriGeneratedInstanceEvaluation();
				}
				catch (Exception ex)
				{
					throw new RenderingObjectModelException(ErrorCode.rsCRIProcessingError, ex, new object[] { this.Name, this.Type });
				}
			}
			finally
			{
				this.m_generatedReportItem.CriGenerationPhase = ReportElement.CriGenerationPhases.None;
			}
			this.SaveGeneratedReportItemInstance();
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x00065B6C File Offset: 0x00063D6C
		private string GetGeneratedDefinitionChunkKey()
		{
			return this.DefinitionPath;
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x00065B74 File Offset: 0x00063D74
		private string GetGeneratedInstanceChunkKey()
		{
			return this.GetGeneratedDefinitionChunkKey() + "_II_" + base.Instance.UniqueName;
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00065B94 File Offset: 0x00063D94
		private bool LoadGeneratedReportItemInstance()
		{
			Global.Tracer.Assert(this.m_generatedReportItem != null && this.m_generatedReportItem.Instance != null, "m_generatedReportItem != null && m_generatedReportItem.Instance != null");
			if (this.m_dynamicWidth != null || this.m_dynamicHeight != null)
			{
				return false;
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot reportSnapshot = base.RenderingContext.OdpContext.OdpMetadata.ReportSnapshot;
			if (this.CriDef.RepeatWith != null)
			{
				string text;
				if (!reportSnapshot.TryGetImageChunkName(this.GetGeneratedInstanceChunkKey(), out text))
				{
					return false;
				}
				((ImageInstance)this.m_generatedReportItem.Instance).StreamName = text;
				return true;
			}
			else
			{
				string text;
				if (!reportSnapshot.TryGetGeneratedReportItemChunkName(this.GetGeneratedInstanceChunkKey(), out text))
				{
					return false;
				}
				string text2;
				Stream chunk = base.RenderingContext.OdpContext.ChunkFactory.GetChunk(text, ReportProcessing.ReportChunkTypes.GeneratedReportItems, ChunkMode.Open, out text2);
				if (chunk == null)
				{
					return false;
				}
				using (chunk)
				{
					ROMInstanceObjectCreator rominstanceObjectCreator = new ROMInstanceObjectCreator(this.m_generatedReportItem.Instance);
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader intermediateFormatReader = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader(chunk, rominstanceObjectCreator, rominstanceObjectCreator);
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable persistable = intermediateFormatReader.ReadRIFObject();
					Global.Tracer.Assert(persistable is ReportItemInstance, "reportItemInstance is ReportItemInstance");
					Global.Tracer.Assert(!intermediateFormatReader.HasReferences, "!reader.HasReferences");
				}
				return true;
			}
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00065CD8 File Offset: 0x00063ED8
		private void SaveGeneratedReportItemInstance()
		{
			if (this.m_dynamicWidth != null || this.m_dynamicHeight != null)
			{
				return;
			}
			OnDemandProcessingContext odpContext = base.RenderingContext.OdpContext;
			Microsoft.ReportingServices.ReportIntermediateFormat.ReportSnapshot reportSnapshot = odpContext.OdpMetadata.ReportSnapshot;
			IChunkFactory chunkFactory = odpContext.ChunkFactory;
			string text;
			if (this.CriDef.RepeatWith == null)
			{
				text = Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem.CreateChunkName();
				using (Stream stream = chunkFactory.CreateChunk(text, ReportProcessing.ReportChunkTypes.GeneratedReportItems, null))
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter intermediateFormatWriter = new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter(stream, odpContext.GetActiveCompatibilityVersion());
					intermediateFormatWriter.Write(this.m_generatedReportItem.Instance);
					stream.Flush();
				}
				reportSnapshot.AddGeneratedReportItemChunkName(this.GetGeneratedInstanceChunkKey(), text);
				return;
			}
			ImageInstance imageInstance = (ImageInstance)this.m_generatedReportItem.Instance;
			text = ImageHelper.StoreImageDataInChunk(ReportProcessing.ReportChunkTypes.Image, imageInstance.ImageData, imageInstance.MIMEType, base.RenderingContext.OdpContext.OdpMetadata, base.RenderingContext.OdpContext.ChunkFactory);
			imageInstance.StreamName = text;
			reportSnapshot.AddImageChunkName(this.GetGeneratedInstanceChunkKey(), text);
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00065DE4 File Offset: 0x00063FE4
		public void CreateCriImageDefinition()
		{
			if (this.m_generatedReportItem != null)
			{
				throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
			}
			Microsoft.ReportingServices.ReportIntermediateFormat.Image image = new Microsoft.ReportingServices.ReportIntermediateFormat.Image(-this.m_reportItemDef.ID, this.m_reportItemDef.Parent);
			image.ParentInstancePath = (Microsoft.ReportingServices.ReportIntermediateFormat.IDOwner)this.m_reportItemDef.ParentInstancePath;
			image.GlobalID = -this.CriDef.GlobalID;
			image.Name = "Image";
			this.m_reportItemDef.SetupCriRenderItemDef(image);
			image.Source = Microsoft.ReportingServices.OnDemandReportRendering.Image.SourceType.Database;
			image.Action = new Microsoft.ReportingServices.ReportIntermediateFormat.Action();
			this.m_generatedReportItem = new Microsoft.ReportingServices.OnDemandReportRendering.Image(this.ParentScope, base.ParentDefinitionPath, this.m_indexIntoParentCollectionDef, image, base.RenderingContext)
			{
				CriOwner = this,
				CriGenerationPhase = ReportElement.CriGenerationPhases.Definition
			};
		}

		// Token: 0x17000E2A RID: 3626
		// (set) Token: 0x060018C1 RID: 6337 RVA: 0x00065EA6 File Offset: 0x000640A6
		public ReportSize DynamicWidth
		{
			set
			{
				this.m_dynamicWidth = value;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (set) Token: 0x060018C2 RID: 6338 RVA: 0x00065EAF File Offset: 0x000640AF
		public ReportSize DynamicHeight
		{
			set
			{
				this.m_dynamicHeight = value;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x060018C3 RID: 6339 RVA: 0x00065EB8 File Offset: 0x000640B8
		public override ReportSize Width
		{
			get
			{
				return this.m_dynamicWidth ?? base.Width;
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x00065ECA File Offset: 0x000640CA
		public override ReportSize Height
		{
			get
			{
				return this.m_dynamicHeight ?? base.Height;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x060018C5 RID: 6341 RVA: 0x00065EDC File Offset: 0x000640DC
		public string Type
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.RenderCri.Type;
				}
				return this.CriDef.Type;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x00065EFD File Offset: 0x000640FD
		internal bool HasCustomData
		{
			get
			{
				return this.m_data != null;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x060018C7 RID: 6343 RVA: 0x00065F08 File Offset: 0x00064108
		public CustomData CustomData
		{
			get
			{
				if (this.m_data == null)
				{
					this.m_data = new CustomData(this);
				}
				return this.m_data;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x00065F24 File Offset: 0x00064124
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItem AltReportItem
		{
			get
			{
				if (this.m_altReportItem == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_altReportItem = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateShim(this, 0, this.m_inSubtotal, this.RenderCri.AltReportItem, this.m_renderingContext);
					}
					else
					{
						this.m_altReportItem = Microsoft.ReportingServices.OnDemandReportRendering.ReportItem.CreateItem(this.ParentScope, base.ParentDefinitionPath, this.CriDef.AltReportItemIndexInParentCollectionDef, this.CriDef.AltReportItem, this.m_renderingContext);
					}
				}
				return this.m_altReportItem;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x060018C9 RID: 6345 RVA: 0x00065FA0 File Offset: 0x000641A0
		public Microsoft.ReportingServices.OnDemandReportRendering.ReportItem GeneratedReportItem
		{
			get
			{
				return this.m_generatedReportItem;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x00065FA8 File Offset: 0x000641A8
		internal Microsoft.ReportingServices.ReportRendering.CustomReportItem RenderCri
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.ReportRendering.CustomReportItem)this.m_renderReportItem;
				}
				return null;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x060018CB RID: 6347 RVA: 0x00065FBF File Offset: 0x000641BF
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				if (this.CriDef.IsDataRegion)
				{
					return this.m_data;
				}
				return this.ParentScope.ReportScopeInstance;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x00065FE0 File Offset: 0x000641E0
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				if (this.CriDef.IsDataRegion)
				{
					return this.CriDef;
				}
				return this.ParentScope.RIFReportScope;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x060018CD RID: 6349 RVA: 0x00066001 File Offset: 0x00064201
		private IReportScope ParentScope
		{
			get
			{
				return base.ReportScope;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x00066009 File Offset: 0x00064209
		bool IDataRegion.HasDataCells
		{
			get
			{
				return this.m_data != null && this.m_data.HasDataRowCollection;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060018CF RID: 6351 RVA: 0x00066020 File Offset: 0x00064220
		IDataRegionRowCollection IDataRegion.RowCollection
		{
			get
			{
				if (this.m_data != null)
				{
					return this.m_data.RowCollection;
				}
				return null;
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x00066037 File Offset: 0x00064237
		internal Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem CriDef
		{
			get
			{
				return (Microsoft.ReportingServices.ReportIntermediateFormat.CustomReportItem)this.m_reportItemDef;
			}
		}

		// Token: 0x060018D1 RID: 6353 RVA: 0x00066044 File Offset: 0x00064244
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new CustomReportItemInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060018D2 RID: 6354 RVA: 0x00066060 File Offset: 0x00064260
		internal override void SetNewContextChildren()
		{
			if (this.m_data != null)
			{
				this.m_data.SetNewContext();
			}
			if (this.m_altReportItem != null && this.m_isOldSnapshot)
			{
				this.m_altReportItem.SetNewContext();
			}
			if (this.m_generatedReportItem != null)
			{
				this.m_generatedReportItem.SetNewContext();
			}
		}

		// Token: 0x060018D3 RID: 6355 RVA: 0x000660B0 File Offset: 0x000642B0
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			if (renderReportItem != null)
			{
				this.m_altReportItem = null;
				this.m_data = null;
				return;
			}
			if (this.m_data != null && this.m_data.DataColumnHierarchy != null)
			{
				this.m_data.DataColumnHierarchy.ResetContext();
			}
			if (this.m_data != null && this.m_data.DataRowHierarchy != null)
			{
				this.m_data.DataRowHierarchy.ResetContext();
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x00066120 File Offset: 0x00064320
		internal int GetCurrentMemberCellDefinitionIndex()
		{
			return this.m_memberCellDefinitionIndex;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x00066128 File Offset: 0x00064328
		internal int GetAndIncrementMemberCellDefinitionIndex()
		{
			int memberCellDefinitionIndex = this.m_memberCellDefinitionIndex;
			this.m_memberCellDefinitionIndex = memberCellDefinitionIndex + 1;
			return memberCellDefinitionIndex;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x00066146 File Offset: 0x00064346
		internal void ResetMemberCellDefinitionIndex(int startIndex)
		{
			this.m_memberCellDefinitionIndex = startIndex;
		}

		// Token: 0x04000C7E RID: 3198
		private const ReportProcessing.ReportChunkTypes ChunkType = ReportProcessing.ReportChunkTypes.GeneratedReportItems;

		// Token: 0x04000C7F RID: 3199
		private int m_indexIntoParentCollectionDef = -1;

		// Token: 0x04000C80 RID: 3200
		private int m_memberCellDefinitionIndex;

		// Token: 0x04000C81 RID: 3201
		private CustomData m_data;

		// Token: 0x04000C82 RID: 3202
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem m_altReportItem;

		// Token: 0x04000C83 RID: 3203
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem m_generatedReportItem;

		// Token: 0x04000C84 RID: 3204
		private Microsoft.ReportingServices.OnDemandReportRendering.ReportItem m_exposeAs;

		// Token: 0x04000C85 RID: 3205
		private ReportSize m_dynamicWidth;

		// Token: 0x04000C86 RID: 3206
		private ReportSize m_dynamicHeight;
	}
}

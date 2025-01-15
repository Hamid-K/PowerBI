using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020006FA RID: 1786
	[Serializable]
	internal sealed class TableRow : IDOwner
	{
		// Token: 0x06006353 RID: 25427 RVA: 0x0018AC59 File Offset: 0x00188E59
		internal TableRow()
		{
		}

		// Token: 0x06006354 RID: 25428 RVA: 0x0018AC61 File Offset: 0x00188E61
		internal TableRow(int id, int idForReportItems)
			: base(id)
		{
			this.m_reportItems = new ReportItemCollection(idForReportItems, false);
			this.m_colSpans = new IntList();
		}

		// Token: 0x1700231D RID: 8989
		// (get) Token: 0x06006355 RID: 25429 RVA: 0x0018AC82 File Offset: 0x00188E82
		// (set) Token: 0x06006356 RID: 25430 RVA: 0x0018AC8A File Offset: 0x00188E8A
		internal ReportItemCollection ReportItems
		{
			get
			{
				return this.m_reportItems;
			}
			set
			{
				this.m_reportItems = value;
			}
		}

		// Token: 0x1700231E RID: 8990
		// (get) Token: 0x06006357 RID: 25431 RVA: 0x0018AC93 File Offset: 0x00188E93
		// (set) Token: 0x06006358 RID: 25432 RVA: 0x0018AC9B File Offset: 0x00188E9B
		internal IntList IDs
		{
			get
			{
				return this.m_IDs;
			}
			set
			{
				this.m_IDs = value;
			}
		}

		// Token: 0x1700231F RID: 8991
		// (get) Token: 0x06006359 RID: 25433 RVA: 0x0018ACA4 File Offset: 0x00188EA4
		// (set) Token: 0x0600635A RID: 25434 RVA: 0x0018ACAC File Offset: 0x00188EAC
		internal IntList ColSpans
		{
			get
			{
				return this.m_colSpans;
			}
			set
			{
				this.m_colSpans = value;
			}
		}

		// Token: 0x17002320 RID: 8992
		// (get) Token: 0x0600635B RID: 25435 RVA: 0x0018ACB5 File Offset: 0x00188EB5
		// (set) Token: 0x0600635C RID: 25436 RVA: 0x0018ACBD File Offset: 0x00188EBD
		internal string Height
		{
			get
			{
				return this.m_height;
			}
			set
			{
				this.m_height = value;
			}
		}

		// Token: 0x17002321 RID: 8993
		// (get) Token: 0x0600635D RID: 25437 RVA: 0x0018ACC6 File Offset: 0x00188EC6
		// (set) Token: 0x0600635E RID: 25438 RVA: 0x0018ACCE File Offset: 0x00188ECE
		internal double HeightValue
		{
			get
			{
				return this.m_heightValue;
			}
			set
			{
				this.m_heightValue = value;
			}
		}

		// Token: 0x17002322 RID: 8994
		// (get) Token: 0x0600635F RID: 25439 RVA: 0x0018ACD7 File Offset: 0x00188ED7
		// (set) Token: 0x06006360 RID: 25440 RVA: 0x0018ACDF File Offset: 0x00188EDF
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

		// Token: 0x17002323 RID: 8995
		// (get) Token: 0x06006361 RID: 25441 RVA: 0x0018ACE8 File Offset: 0x00188EE8
		// (set) Token: 0x06006362 RID: 25442 RVA: 0x0018ACF0 File Offset: 0x00188EF0
		internal string RenderingModelID
		{
			get
			{
				return this.m_renderingModelID;
			}
			set
			{
				this.m_renderingModelID = value;
			}
		}

		// Token: 0x17002324 RID: 8996
		// (get) Token: 0x06006363 RID: 25443 RVA: 0x0018ACF9 File Offset: 0x00188EF9
		// (set) Token: 0x06006364 RID: 25444 RVA: 0x0018AD01 File Offset: 0x00188F01
		internal ReportSize HeightForRendering
		{
			get
			{
				return this.m_heightForRendering;
			}
			set
			{
				this.m_heightForRendering = value;
			}
		}

		// Token: 0x17002325 RID: 8997
		// (get) Token: 0x06006365 RID: 25445 RVA: 0x0018AD0A File Offset: 0x00188F0A
		// (set) Token: 0x06006366 RID: 25446 RVA: 0x0018AD12 File Offset: 0x00188F12
		internal string[] RenderingModelIDs
		{
			get
			{
				return this.m_renderingModelIDs;
			}
			set
			{
				this.m_renderingModelIDs = value;
			}
		}

		// Token: 0x17002326 RID: 8998
		// (get) Token: 0x06006367 RID: 25447 RVA: 0x0018AD1B File Offset: 0x00188F1B
		// (set) Token: 0x06006368 RID: 25448 RVA: 0x0018AD23 File Offset: 0x00188F23
		internal bool StartHidden
		{
			get
			{
				return this.m_startHidden;
			}
			set
			{
				this.m_startHidden = value;
			}
		}

		// Token: 0x06006369 RID: 25449 RVA: 0x0018AD2C File Offset: 0x00188F2C
		internal bool Initialize(bool registerRunningValues, int numberOfColumns, InitializationContext context, ref double tableHeight, bool[] tableColumnVisibility)
		{
			int num = 0;
			for (int i = 0; i < this.m_colSpans.Count; i++)
			{
				num += this.m_colSpans[i];
			}
			if (numberOfColumns != num)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsWrongNumberOfTableCells, Severity.Error, context.ObjectType, context.ObjectName, "TableCells", Array.Empty<string>());
			}
			this.m_heightValue = context.ValidateSize(ref this.m_height, "Height");
			tableHeight = Math.Round(tableHeight + this.m_heightValue, Validator.DecimalPrecision);
			if (this.m_visibility != null)
			{
				this.m_visibility.Initialize(context, true, true);
			}
			bool flag = this.m_reportItems.Initialize(context, registerRunningValues, tableColumnVisibility);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
			return flag;
		}

		// Token: 0x0600636A RID: 25450 RVA: 0x0018ADF7 File Offset: 0x00188FF7
		internal void RegisterReceiver(InitializationContext context)
		{
			if (this.m_visibility != null)
			{
				this.m_visibility.RegisterReceiver(context, true);
			}
			this.m_reportItems.RegisterReceiver(context);
			if (this.m_visibility != null)
			{
				this.m_visibility.UnRegisterReceiver(context);
			}
		}

		// Token: 0x0600636B RID: 25451 RVA: 0x0018AE30 File Offset: 0x00189030
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.IDOwner, new MemberInfoList
			{
				new MemberInfo(MemberName.ReportItems, ObjectType.ReportItemCollection),
				new MemberInfo(MemberName.IDs, ObjectType.IntList),
				new MemberInfo(MemberName.ColSpans, ObjectType.IntList),
				new MemberInfo(MemberName.Height, Token.String),
				new MemberInfo(MemberName.HeightValue, Token.Double),
				new MemberInfo(MemberName.Visibility, ObjectType.Visibility)
			});
		}

		// Token: 0x040031F9 RID: 12793
		private ReportItemCollection m_reportItems;

		// Token: 0x040031FA RID: 12794
		private IntList m_IDs;

		// Token: 0x040031FB RID: 12795
		private IntList m_colSpans;

		// Token: 0x040031FC RID: 12796
		private string m_height;

		// Token: 0x040031FD RID: 12797
		private double m_heightValue;

		// Token: 0x040031FE RID: 12798
		private Visibility m_visibility;

		// Token: 0x040031FF RID: 12799
		[NonSerialized]
		private bool m_startHidden;

		// Token: 0x04003200 RID: 12800
		[NonSerialized]
		private string m_renderingModelID;

		// Token: 0x04003201 RID: 12801
		[NonSerialized]
		private ReportSize m_heightForRendering;

		// Token: 0x04003202 RID: 12802
		[NonSerialized]
		private string[] m_renderingModelIDs;
	}
}

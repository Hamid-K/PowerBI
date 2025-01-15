using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x0200041C RID: 1052
	[Serializable]
	internal sealed class MapCell : Cell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002DF4 RID: 11764 RVA: 0x000D1DD7 File Offset: 0x000CFFD7
		internal MapCell()
		{
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x000D1DDF File Offset: 0x000CFFDF
		internal MapCell(int id, MapDataRegion mapDataRegion)
			: base(id, mapDataRegion)
		{
		}

		// Token: 0x170015E9 RID: 5609
		// (get) Token: 0x06002DF6 RID: 11766 RVA: 0x000D1DE9 File Offset: 0x000CFFE9
		protected override bool IsDataRegionBodyCell
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170015EA RID: 5610
		// (get) Token: 0x06002DF7 RID: 11767 RVA: 0x000D1DEC File Offset: 0x000CFFEC
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.MapCell;
			}
		}

		// Token: 0x170015EB RID: 5611
		// (get) Token: 0x06002DF8 RID: 11768 RVA: 0x000D1DF0 File Offset: 0x000CFFF0
		public override Microsoft.ReportingServices.ReportProcessing.ObjectType DataScopeObjectType
		{
			get
			{
				return this.ObjectType;
			}
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x000D1DF8 File Offset: 0x000CFFF8
		internal override void InternalInitialize(int parentRowID, int parentColumnID, int rowindex, int colIndex, InitializationContext context)
		{
		}

		// Token: 0x170015EC RID: 5612
		// (get) Token: 0x06002DFA RID: 11770 RVA: 0x000D1DFA File Offset: 0x000CFFFA
		protected override Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode ExprHostDataRegionMode
		{
			get
			{
				return Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.MapDataRegion;
			}
		}

		// Token: 0x06002DFB RID: 11771 RVA: 0x000D1E00 File Offset: 0x000D0000
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCell, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Cell, list);
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x000D1E23 File Offset: 0x000D0023
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.MapCell;
		}

		// Token: 0x04001856 RID: 6230
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = MapCell.GetDeclaration();
	}
}
